'use client';

import { useAuth } from '../hooks/useAuth';

interface AuthGuardProps {
  children: React.ReactNode;
  loadingComponent?: React.ReactNode;
}

/*
 * Component that protects routes requiring authentication
 * Automatically redirects to login if user is not authenticated
 */
export function AuthGuard({ children, loadingComponent }: AuthGuardProps) {
  const { isAuthenticated, isLoading } = useAuth(true);

  if (isLoading) {
    return (
      <>
        {loadingComponent || (
          <div style={{ 
            padding: '2rem', 
            fontFamily: 'system-ui, sans-serif',
            textAlign: 'center',
            marginTop: '4rem'
          }}>
            <div style={{
              display: 'inline-block',
              width: '50px',
              height: '50px',
              border: '5px solid #f3f3f3',
              borderTop: '5px solid #0070f3',
              borderRadius: '50%',
              animation: 'spin 1s linear infinite'
            }}></div>
            <p style={{ marginTop: '1rem', color: '#666' }}>Loading...</p>
            <style>{`
              @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
              }
            `}</style>
          </div>
        )}
      </>
    );
  }

  // If not authenticated, useAuth hook will redirect
  // Only render children if authenticated
  if (!isAuthenticated) {
    return null;
  }

  return <>{children}</>;
}
