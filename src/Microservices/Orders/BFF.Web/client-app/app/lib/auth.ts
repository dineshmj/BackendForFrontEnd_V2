import appConfigData from '../../app.config.json';

export interface BffUser {
  type: string;
  value: string;
}

export interface AuthState {
  isAuthenticated: boolean;
  user: BffUser[] | null;
  isLoading: boolean;
}

//
// Check if the user is authenticated, by calling the BFF user endpoint.
//
export async function checkAuthentication(): Promise<AuthState> {
  try {
    const response = await fetch(`"${appConfigData.config.ordersBffUrl}}/api/auth/user`, {
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