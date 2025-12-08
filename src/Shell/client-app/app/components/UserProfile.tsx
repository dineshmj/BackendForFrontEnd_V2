'use client';

import { redirectToLogout, getUserDisplayName, type BffUser } from '../lib/auth';
import { getDiscoveredMicroservices } from '../lib/auth-utils';

interface UserProfileProps {
  claims: BffUser[];
}

export function UserProfile({ claims }: UserProfileProps) {
  const displayName = getUserDisplayName(claims);

  const handleLogout = async () => {
    const microservices = getDiscoveredMicroservices();
    for (const baseURL of microservices) {
      try {
        const response = await fetch(`${baseURL}/api/auth/silent-logout`, {
          method: 'POST',
          credentials: 'include',
        });
        if (response.status !== 200) {
          console.warn(`Logout failed for ${baseURL} with status: ${response.status}`);
        }
      } catch (error) {
        console.error(`Error during logout for ${baseURL}:`, error);
      }
    }
    redirectToLogout(claims);
  };

  return (
    <table style={{
      borderCollapse: 'collapse',
      backgroundColor: '#f8f8f8',
      borderRadius: '8px',
      padding: '0.5rem',
      width: '100%',
    }}>
      <tbody>
        <tr>
          <td style={{ padding: '0.25rem 0', fontSize: '1rem', color: '#6c757d' }}>
            <strong style={{ marginLeft: '0.5rem', fontSize: '1rem', color: '#343a40' }}>{displayName}</strong>
          </td>
        </tr>
        <tr>
          <td style={{ padding: '0.25rem 0' }}>
            <button
              onClick={handleLogout}
              style={{
                padding: '0.5rem 1rem',
                backgroundColor: '#dc3545',
                color: 'white',
                border: 'none',
                borderRadius: '5px',
                cursor: 'pointer',
                fontSize: '0.95rem',
                fontWeight: '500',
                width: '100%',
              }}
            >
              Logout
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  );
}