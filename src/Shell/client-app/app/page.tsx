'use client';

import { useState, useEffect } from 'react';
import { AuthGuard } from './components/AuthGuard';
import { UserProfile } from './components/UserProfile';
import Image from 'next/image';
import { MicroserviceAccordion } from './components/MicroserviceAccordion';
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

function HomeContent() {
  const { user } = useAuth(false);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [menuData, setMenuData] = useState<MenuResponse | null>(null);
  const [expandedIndex, setExpandedIndex] = useState<number>(0);

  const handleToggle = (index: number) => {
    setExpandedIndex(index === expandedIndex ? -1 : index);
  };

  const handleMenuItemClick = (item: MenuItem) => {
    setError(null);
    setLoading(true);

    const iframe = document.getElementById('microservice-frame') as HTMLIFrameElement;

    if (!iframe) {
      setError('Internal error: iframe not found.');
      setLoading(false);
      return;
    }

    if (!item.baseURL) {
      setError('Internal error: baseURL not found for the selected microservice.');
      setLoading(false);
      return;
    }
    addVisitedMicroservice(item.baseURL);

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

      const allBaseUrls = data.microservices.map(ms => ms.baseURL);
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
        minHeight: '100vh',
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

      {/* MAIN CONTENT: Menu | Iframe */}
      <main
        style={{
          display: 'grid',
          gridTemplateColumns: '300px 1fr',
          flexGrow: 1,
          overflow: 'hidden',
        }}
      >
        {/* LEFT COLUMN: DYNAMIC MENU */}
        <div
          style={{
            padding: '1rem 2rem',
            borderRight: '1px solid #ccc',
            overflowY: 'auto',
          }}
        >
          {menuData?.microservices.map((microservice, index) => (
            <MicroserviceAccordion
              key={microservice.name}
              microservice={microservice}
              isExpanded={index === expandedIndex}
              onToggle={() => handleToggle(index)}
              handleMenuItemClick={handleMenuItemClick}
              loading={loading}
            />
          ))}
        </div>

        {/* RIGHT COLUMN: IFRAME SECTION & ERROR BOX */}
        <div style={{ padding: '1rem', overflow: 'hidden', display: 'flex', flexDirection: 'column' }}>
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
      <div style={{ flexGrow: 1 }}>
        <iframe
          id="microservice-frame"
          style={{
            width: '100%',
            height: '100%', // Use 100% height of the parent container
            border: '1px solid #ccc',
            borderRadius: '5px',
          }}
        />
      </div>
    </div>
  </main>
  </div>

  );
}