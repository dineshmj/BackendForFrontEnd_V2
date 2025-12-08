export interface BffUser {
  type: string;
  value: string;
}

export interface AuthState {
  isAuthenticated: boolean;
  user: BffUser[] | null;
  isLoading: boolean;
}

/*
 * Check if user is authenticated by calling BFF user endpoint
 */
export async function checkAuthentication(): Promise<AuthState> {
  try {
    const response = await fetch('/bff/user', {
      credentials: 'include',
      headers: {
        'X-CSRF': '1',
      },
    });

    if (response.ok) {
      const claims = await response.json();
      
      // BFF returns empty array if not authenticated
      if (Array.isArray(claims) && claims.length > 0) {
        return {
          isAuthenticated: true,
          user: claims,
          isLoading: false,
        };
      }
    }

    return {
      isAuthenticated: false,
      user: null,
      isLoading: false,
    };
  } catch (error) {
    console.error('Authentication check failed:', error);
    return {
      isAuthenticated: false,
      user: null,
      isLoading: false,
    };
  }
}

/*
 * Redirect to BFF login endpoint
 */
export function redirectToLogin(): void {
  const returnUrl = window.location.pathname + window.location.search;
  window.location.href = `/bff/login?returnUrl=${encodeURIComponent(returnUrl)}`;
}

/*
 * Redirect to BFF logout endpoint
 */
export function redirectToLogout(claims: BffUser[]): void {
  const idToken = getClaimValue(claims, 'id_token');
  const sid = getClaimValue(claims, 'sid');

  let logoutUrl = '/bff/logout';
  const params = new URLSearchParams();

  if (idToken) {
    params.append('id_token_hint', idToken);
  }

  if (sid) {
    params.append('sid', sid);
  }

  if (params.toString()) {
    logoutUrl += `?${params.toString()}`;
  }

  window.location.href = logoutUrl;
}

/*
 * Get claim value by type
 */
export function getClaimValue(claims: BffUser[], claimType: string): string | null {
  const claim = claims.find(c => c.type === claimType);
  return claim ? claim.value : null;
}

/*
 * Get user display name from claims
 */
export function getUserDisplayName(claims: BffUser[]): string {
  return getClaimValue(claims, 'name') || 
         getClaimValue(claims, 'preferred_username') || 
         getClaimValue(claims, 'email') || 
         'User';
}