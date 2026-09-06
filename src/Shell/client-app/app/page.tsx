'use client';

import { useState, useEffect, useRef } from 'react';
import { AuthGuard } from './components/AuthGuard';
import { UserProfile } from './components/UserProfile';
import Image from 'next/image';
import { TopNavMenu } from './components/TopNavMenu';
import { useAuth } from './hooks/useAuth';
import { MenuResponse, MenuItem } from './types';
import { addVisitedMicroservice, setDiscoveredMicroservices } from './lib/auth-utils';

export default function Home() {
    return (
        <AuthGuard>
            <HomeContent />
        </AuthGuard>
    );
}

function requestNavigationPermission(
    iframe: HTMLIFrameElement,
    currentOrigin: string,
): Promise<boolean> {
    return new Promise((resolve) => {
        const requestId = crypto.randomUUID();
        let settled = false;

        function handleResponse(event: MessageEvent) {
            if (event.origin !== currentOrigin) return;
            if (event.data?.type !== 'PAS_NAVIGATION_RESPONSE') return;
            if (event.data.requestId !== requestId) return;

            settled = true;
            window.removeEventListener('message', handleResponse);
            resolve(event.data.allowed !== false);
        }

        window.addEventListener('message', handleResponse);
        iframe.contentWindow?.postMessage({ type: 'PAS_NAVIGATION_REQUEST', requestId }, currentOrigin);

        // Fail-open: if the currently-loaded page doesn't implement this
        // protocol (or nothing is loaded yet), don't block navigation forever.
        // 30s comfortably outlasts a blocking window.confirm() dialog, which
        // the earlier 300ms value did not — that caused navigation to proceed
        // via this timeout before the user had even answered the prompt.
        setTimeout(() => {
            if (!settled) {
                window.removeEventListener('message', handleResponse);
                resolve(true);
            }
        }, 30000);
    });
}

function HomeContent() {
    const { user } = useAuth(false);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [menuData, setMenuData] = useState<MenuResponse | null>(null);

    // Tracks the ACTUAL origin of whatever is currently loaded in the iframe,
    // as self-reported by the microfrontend on mount. We can't reliably derive
    // this from `iframe.src` — that's the URL we *assigned* (often the BFF's
    // silent-login endpoint), not necessarily where the frame ends up after
    // any redirects (e.g. Orders' BFF and its Next.js dev server are on
    // different origins). event.origin, by contrast, is set by the browser
    // itself and can't be spoofed by the sender, so it's the source of truth.
    const currentFrameOriginRef = useRef<string | null>(null);

    useEffect(() => {
        function handleFrameReady(event: MessageEvent) {
            const iframe = document.getElementById('microservice-frame') as HTMLIFrameElement | null;
            if (!iframe || event.source !== iframe.contentWindow) return; // must genuinely be our iframe
            if (event.data?.type !== 'PAS_MFE_READY') return;
            currentFrameOriginRef.current = event.origin;
        }
        window.addEventListener('message', handleFrameReady);
        return () => window.removeEventListener('message', handleFrameReady);
    }, []);

    const handleMenuItemClick = async (item: MenuItem) => {

        setError(null);
        setLoading(true);

        const iframe = document.getElementById('microservice-frame') as HTMLIFrameElement;

        if (!iframe) {
            setError('Internal error: iframe not found.');
            setLoading(false);
            return;
        }

        const currentOrigin = currentFrameOriginRef.current;
        if (currentOrigin) {
            const canNavigate = await requestNavigationPermission(iframe, currentOrigin);
            if (!canNavigate) {
                setLoading(false);
                return; // user (or the microfrontend) blocked the navigation
            }
        }

        if (!item.baseURL) {
            setError('Internal error: baseURL not found for the selected microservice.');
            setLoading(false);
            return;
        }
        addVisitedMicroservice(item.baseURL);

        // Reset — the newly-loaded microfrontend will announce its own origin
        // once it mounts. Clearing this now prevents a rapid second click from
        // reusing the outgoing page's origin while the new one is still loading.
        currentFrameOriginRef.current = null;

        const silentLoginUrl = `${item.baseURL}/api/auth/silent-login?returnUrl=${encodeURIComponent(item.urlRelativePath)}`;
        iframe.src = silentLoginUrl;

        iframe.onload = () => {
            setLoading(false);
        };
    };

    const loadMenu = async () => {
        try {
            const response = await fetch('/bff/api/Menu/Authorized', {
                credentials: 'include', // Important for sending cookies/auth headers in BFF context
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data: MenuResponse = await response.json();
            setMenuData(data);

            const allBaseUrls = data.microservices.map((ms) => ms.baseURL);
            setDiscoveredMicroservices(allBaseUrls);
        } catch (e) {
            console.error('Failed to load menu:', e);
        }
    };

    useEffect(() => {
        loadMenu();
    }, []);

    if (!user) return null;

    return (
        <div
            style={{
                fontFamily: 'system-ui, sans-serif',
                height: '100vh',
                overflow: 'hidden',
                display: 'flex',
                flexDirection: 'column',
            }}
        >
            {/* HEADER: Logo | Title | Logout */}
            <header
                style={{
                    display: 'grid',
                    gridTemplateColumns: '100px 1fr auto',
                    alignItems: 'center',
                    padding: '1rem 2rem',
                    borderBottom: '1px solid #ccc',
                    backgroundColor: '#f8f8f8',
                }}
            >
                <div style={{ display: 'flex', alignItems: 'center' }}>
                    <Image
                        src="/res/PAS.png"
                        alt="Platform Administration System Logo"
                        style={{ height: '100px', width: 'auto', borderRadius: '12px' }}
                    />
                </div>
                <h1 style={{ margin: 1, marginLeft: '2rem', fontSize: '2rem' }}>
                    Platform Administration System
                </h1>
                {/* USER PROFILE (Contains Logout) */}
                <UserProfile claims={user} />
            </header>

            {/* NAV ROW: horizontal dropdown menu, directly below the branding strip */}
            {menuData && (
                <TopNavMenu
                    microservices={menuData.microservices}
                    handleMenuItemClick={handleMenuItemClick}
                    loading={loading}
                />
            )}

            {/* MAIN CONTENT: IFRAME now spans the full width */}
            <main
                style={{
                    flexGrow: 1,
                    minHeight: 0,
                    overflow: 'hidden',
                    padding: '1rem',
                    display: 'flex',
                    flexDirection: 'column',
                }}
            >
                {error && (
                    <div
                        style={{
                            padding: '1rem',
                            marginBottom: '1rem',
                            backgroundColor: '#fee',
                            color: '#c00',
                            borderRadius: '5px',
                            border: '1px solid #fcc',
                        }}
                    >
                        <strong>Error:</strong> {error}
                    </div>
                )}

                {/* IFRAME SECTION */}
                <div style={{ flexGrow: 1, minHeight: 0 }}>
                    <iframe
                        id="microservice-frame"
                        style={{
                            width: '100%',
                            height: '100%',
                            border: '1px solid #ccc',
                            borderRadius: '5px',
                        }}
                    />
                </div>
            </main>
        </div>
    );
}