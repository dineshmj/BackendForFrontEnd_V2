'use client';

import { useEffect, useState } from 'react';
import { checkAuthentication, redirectToLogin, type AuthState } from '../lib/auth';

/*
 * Custom hook for authentication
 * Automatically checks auth status and redirects to login if not authenticated
 */
export function useAuth(autoRedirect: boolean = true) {
  const [authState, setAuthState] = useState<AuthState>({
    isAuthenticated: false,
    user: null,
    isLoading: true,
  });

  useEffect(() => {
    const checkAuth = async () => {
      const state = await checkAuthentication();
      setAuthState(state);

      // Auto-redirect to login if not authenticated
      if (autoRedirect && !state.isAuthenticated && !state.isLoading) {
        redirectToLogin();
      }
    };

    checkAuth();
  }, [autoRedirect]);

  return authState;
}