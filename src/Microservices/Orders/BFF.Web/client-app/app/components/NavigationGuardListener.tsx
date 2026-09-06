'use client';
import { useEffect } from 'react';

const SHELL_ORIGIN = process.env.NEXT_PUBLIC_SHELL_ORIGIN!; // e.g. https://localhost:44300

// Simple module-level flag. For multi-page correctness, wrap this in a
// React Context instead so it resets cleanly on route changes within the SPA.
let hasUnsavedChanges = true;
export function setUnsavedChanges(value: boolean) {
  hasUnsavedChanges = value;
}

// Mount this once, near the root layout of the microfrontend.
export function NavigationGuardListener() {
  useEffect(() => {
    // Announce our real origin to the Shell. This matters because our own
    // BFF origin and this SPA's origin aren't always the same thing (e.g.
    // Orders' BFF and its Next.js dev server run on different ports).
    window.parent.postMessage({ type: 'PAS_MFE_READY' }, SHELL_ORIGIN);

    function handleMessage(event: MessageEvent) {
      if (event.origin !== SHELL_ORIGIN) return; // reject anything not from the Shell
      if (event.data?.type !== 'PAS_NAVIGATION_REQUEST') return;

      const allowed = hasUnsavedChanges
        ? window.confirm('You have unsaved changes. Leave this page?')
        : true;

      (event.source as Window)?.postMessage(
        { type: 'PAS_NAVIGATION_RESPONSE', requestId: event.data.requestId, allowed },
        event.origin,
      );
    }

    window.addEventListener('message', handleMessage);
    return () => window.removeEventListener('message', handleMessage);
  }, []);

  return null;
}