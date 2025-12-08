'use client';

import { useEffect, useRef, useState } from 'react';
import {
  checkAuthentication,
  type AuthState,
  type BffUser,
} from '../lib/auth';

interface MinimalAuthResult {
  isAuthenticated: boolean;
  user?: unknown | null;
  isLoading?: boolean;
}

// Runtime type guard: checks that the value is an array and each item is a plain object.
// If you know more about BffUser shape (e.g. has `id: string`), check those fields here.

function isBffUserArray(value: unknown): value is BffUser[] {
  if (!Array.isArray(value)) return false;
  return value.every(item => typeof item === 'object' && item !== null);
}

function normalizeAuthResult(result: AuthState | MinimalAuthResult): AuthState {
  const userCandidate = (result as MinimalAuthResult).user ?? null;
  const user: BffUser[] | null = isBffUserArray(userCandidate) ? userCandidate : null;

  return {
    isAuthenticated: Boolean(result.isAuthenticated),
    user,
    isLoading: result.isLoading ?? false,
  };
}

export function useAuth(autoRedirect: boolean = true) {
  const mountedRef = useRef(true);

  const [authState, setAuthState] = useState<AuthState>({
    isAuthenticated: false,
    user: null,
    isLoading: true,
  });

  const [showLoginMessage, setShowLoginMessage] = useState(false);

  useEffect(() => {
    mountedRef.current = true;

    return () => {
      mountedRef.current = false;
    };
  }, []);

  useEffect(() => {
    const controller = new AbortController();

    const doAuthCheck = async () => {
      if (mountedRef.current) {
        setAuthState(prev => ({ ...prev, isLoading: true }));
      }

      try {
        const result = await checkAuthentication();
        const normalized = normalizeAuthResult(result);

        if (mountedRef.current) {
          setAuthState({
            ...normalized,
            isLoading: false,
          });
        }
      } catch (error) {
        console.error('useAuth: Error during authentication check:', error);
        
        if (mountedRef.current) {
          setAuthState({
            isAuthenticated: false,
            user: null,
            isLoading: false,
          });
        }
      } finally {
        if (mountedRef.current) {
          setAuthState(prev => ({ ...prev, isLoading: false }));
        }
      }
    };

    doAuthCheck();

    return () => {
      controller.abort();
    };
  }, [autoRedirect]);

  useEffect(() => {
    if (authState.isLoading) return;
    if (!authState.isAuthenticated && autoRedirect) {
      setShowLoginMessage(true);
    } else {
      setShowLoginMessage(false);
    }
  }, [authState.isLoading, authState.isAuthenticated, autoRedirect]);

  return {
    ...authState,
    showLoginMessage,
  };
}