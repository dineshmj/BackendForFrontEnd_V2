'use client';

import { useEffect, useState } from 'react';
import styles from './NavigationGuardListener.module.css';

const SHELL_ORIGIN = process.env.NEXT_PUBLIC_SHELL_ORIGIN!;

// Reset to false — this was set to `true` earlier purely to test the guard.
let hasUnsavedChanges = true;
export function setUnsavedChanges(value: boolean) {
  hasUnsavedChanges = value;
}

interface PendingRequest {
  requestId: string;
  source: MessageEventSource;
  origin: string;
}

export function NavigationGuardListener() {
  const [pendingRequest, setPendingRequest] = useState<PendingRequest | null>(null);

  useEffect(() => {
    // Announce our real origin to the Shell as soon as we mount — the Shell
    // can't reliably infer this from the URL it assigned to the iframe.
    window.parent.postMessage({ type: 'PAS_MFE_READY' }, SHELL_ORIGIN);

    function handleMessage(event: MessageEvent) {
      if (event.origin !== SHELL_ORIGIN) return;
      if (event.data?.type !== 'PAS_NAVIGATION_REQUEST') return;
      if (!event.source) return;

      const { requestId } = event.data;

      if (!hasUnsavedChanges) {
        // No conflict — reply immediately, no modal needed.
        (event.source as Window)?.postMessage(
          { type: 'PAS_NAVIGATION_RESPONSE', requestId, allowed: true },
          event.origin,
        );
        return;
      }

      // Defer the reply until the user answers the modal below.
      setPendingRequest({ requestId, source: event.source, origin: event.origin });
    }

    window.addEventListener('message', handleMessage);
    return () => window.removeEventListener('message', handleMessage);
  }, []);

  const respond = (allowed: boolean) => {
    if (!pendingRequest) return;
    (pendingRequest.source as Window)?.postMessage(
      { type: 'PAS_NAVIGATION_RESPONSE', requestId: pendingRequest.requestId, allowed },
      pendingRequest.origin,
    );
    setPendingRequest(null);
  };

  if (!pendingRequest) return null;

  return (
    <div className={styles.overlay} role="presentation">
      <div
        className={styles.dialog}
        role="alertdialog"
        aria-modal="true"
        aria-labelledby="nav-guard-title"
        aria-describedby="nav-guard-desc"
      >
        <h2 id="nav-guard-title" className={styles.title}>
          Unsaved changes
        </h2>
        <p id="nav-guard-desc" className={styles.message}>
          You have unsaved changes on this page. If you leave now, they will be lost.
        </p>
        <div className={styles.actions}>
          <button
            type="button"
            className={styles.stayButton}
            onClick={() => respond(false)}
            autoFocus
          >
            Stay on this page
          </button>
          <button
            type="button"
            className={styles.leaveButton}
            onClick={() => respond(true)}
          >
            Leave without saving
          </button>
        </div>
      </div>
    </div>
  );
}