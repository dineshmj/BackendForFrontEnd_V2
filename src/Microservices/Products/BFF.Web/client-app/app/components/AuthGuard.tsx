'use client';

import { useAuth } from '../hooks/useAuth';
import appConfigData from '../../app.config.json';

interface AuthGuardProps {
  children: React.ReactNode;
}

/*
 * Component that protects routes requiring authentication
 * Automatically redirects to login if user is not authenticated
 */
export function AuthGuard({ children }: AuthGuardProps) {
  const { isAuthenticated, showLoginMessage } = useAuth(true);

  // If not authenticated, useAuth hook will redirect
  // Only render children if authenticated
  if (!isAuthenticated) {
    if (showLoginMessage) {
      const navigateToPmsLoginUrl = appConfigData.config.pmsLoginUrl;
      return (
        <div style={{ padding: '2rem', fontFamily: 'system-ui', textAlign: 'center' }}>
          <h1>Authentication Required</h1>
          
          <p>
            User is not authenticated. Please login from
             <a href={navigateToPmsLoginUrl} style={{ marginLeft: '0.5rem' }}>Platform Management System</a>.
          </p>
        </div>
      );
    }
    return null;
  }

  return <>{children}</>;
}