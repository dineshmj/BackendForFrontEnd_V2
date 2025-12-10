using Duende.IdentityServer;
using Duende.IdentityServer.Models;

using FW.Landscape.Common;
using FW.Landscape.Common.Microservices;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new (name: "roles", displayName: "User Roles", userClaims: [ "role" ])
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new (MicroserviceApiResources.PRODUCTS_API, "Products API")
            {
                UserClaims = { "role", "name", "email" }
            },
			new (MicroserviceApiResources.ORDERS_API, "Orders API")
			{
				UserClaims = { "role", "name", "email" }
			}
		];

    public static IEnumerable<ApiResource> ApiResources =>
        [
            new (MicroserviceApiResources.PRODUCTS_API, "Products API")
            {
                Scopes = { MicroserviceApiResources.PRODUCTS_API },
                UserClaims = { "role", "name", "email" }
            },
		    new (MicroserviceApiResources.ORDERS_API, "Orders API")
			{
				Scopes = { MicroserviceApiResources.ORDERS_API },
				UserClaims = { "role", "name", "email" }
			}
		];

    public static IEnumerable<Client> Clients =>
        [
            // Shell BFF Client (BFF using ASP.NET Core 10)
            new()
            {
                ClientId = PASShellBFF.CLIENT_ID_FOR_IDP,
                ClientName = PASShellBFF.CLIENT_NAME_FOR_IDP,
                ClientSecrets = { new Secret(PASShellBFF.CLIENT_SECRET_FOR_IDP.Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                    // 🡡__ WHY   : The Authorization Code flow is the recommended OIDC flow for confidential server-side clients (BFFs).
                    //              It returns an authorization code to the server (via a browser redirection), which the server exchanges for tokens using its client secret.
                    //              This keeps access/refresh tokens off the browser and leverages server-side confidentiality.
                    // 🡡__ IF NOT: Using implicit or hybrid flows (or client-side flows) would expose tokens to the browser, increasing XSS risk.
                    //              If a non-confidential grant (e.g., Resource Owner Password) were used, it would require sending user credentials
                    //              to the client and reduce overall security. The client might also be unable to validate tokens or perform
                    //              secure token exchange in a standard way.

                
                RedirectUris = { $"{PASShellBFF.SHELL_BFF_CLIENT_BASE_URL }/signin-oidc" },
				PostLogoutRedirectUris = { $"{PASShellBFF.SHELL_BFF_CLIENT_BASE_URL}/signout-callback-oidc" },
				FrontChannelLogoutUri = $"{PASShellBFF.SHELL_BFF_CLIENT_BASE_URL }/signout-oidc",

				AllowOfflineAccess = true,
                    // 🡡__ WHY   : Allowing offline access enables issuance of refresh tokens (offline access RFC). BFFs or server-side
                    //              clients can use refresh tokens to silently obtain new access tokens without forcing the user to re-authenticate.
                    //              This is important for long-lived sessions, background jobs, or UX where silent re-auth is desired.
                    // 🡡__ IF NOT: If false, refresh tokens will not be issued. Clients must prompt the user to sign in again when the access token expires,
                    //              causing more frequent interactive logins and worse UX. Background/cron operations requiring API access without user interaction
                    //              would be impossible.

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "roles"
                },

                // For the Shell application, show the content page.
                RequireConsent = true,

                RefreshTokenUsage = TokenUsage.ReUse,
                    // 🡡__ WHY   : ReUse leaves the same refresh token valid for multiple refresh operations until it expires. This reduces storage
                    //              churn on the server and is simpler to implement. It is acceptable for many server-side clients where refresh tokens
                    //              are kept securely.
                    // 🡡__ IF NOT: If you choose TokenUsage.OneTimeOnly, the server issues a new refresh token each time the client uses the old one
                    //              (rotation). Rotation is more secure because stolen refresh tokens are invalidated after use, but it requires
                    //              tracking token rotation state and can increase complexity (and race-condition handling) on the server.


                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 3600
                    // 🡡__ WHY   : Sliding expiration extends the refresh token lifetime (by the configured window) each time it is used, enabling
                    //              long-lived sessions for active users while expiring tokens for inactive accounts. The Sliding lifetime here (3600)
                    //              is the renewal window (in seconds) applied on each use; adjust based on desired session duration and security posture.
                    // 🡡__ IF NOT: If you use Absolute expiration instead, refresh tokens will expire after a fixed period regardless of usage,
                    //              forcing reauthentication after that period. If Sliding is misconfigured (too long) you could unintentionally enable
                    //              excessively long sessions; if too short, users will be asked to re-login frequently.

            },

            // Products Microservice Client (BFF using ASP.NET Core 10)
            new()
            {
                ClientId = ProductsMicroservice.CLIENT_ID_FOR_IDP,
                ClientName = ProductsMicroservice.CLIENT_NAME_FOR_IDP,
                ClientSecrets = { new Secret(ProductsMicroservice.CLIENT_SECRET_FOR_IDP.Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                    // 🡡__ WHY   : The Products microservice (if acting as a confidential client or BFF) should use Authorization Code to keep tokens
                    //              private on the server and to benefit from the standard OIDC/OAuth flow, including PKCE if applicable.
                    // 🡡__ IF NOT: Using non-confidential or browser flows could expose tokens to the client-side, allowing token theft via XSS
                    //              and making secure API access more difficult to enforce.

                RedirectUris = { $"{ProductsMicroservice.BFF_CLIENT_BASE_URL }/signin-oidc" },
                PostLogoutRedirectUris = { $"{ProductsMicroservice.BFF_CLIENT_BASE_URL}/signout-callback-oidc" },
                FrontChannelLogoutUri = $"{ProductsMicroservice.BFF_CLIENT_BASE_URL }/signout-oidc",

                AllowOfflineAccess = true,
                    // 🡡__ WHY   : Products Microservice BFF frontend may need refresh tokens to maintain backend sessions or to act on behalf of the user without interactive login.
                    //              For server-to-server or long-running operations, refresh tokens enable seamless token renewal.
                    // 🡡__ IF NOT: Without offline access, the service cannot obtain refresh tokens and must force users to re-authenticate when access tokens expire.


                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "roles",
                    MicroserviceApiResources.PRODUCTS_API
                        // 🡡__ WHY   : Including the PRODUCTS_API scope permits the Products Microservice BFF client to request access tokens that include scope permissions for the
                        //              Products Microservice API. The Products Microservice API will validate the access token and require the corresponding scope to authorize API calls.
                        // 🡡__ IF NOT: If this scope is not included, tokens issued to the client will not be valid for calling the Products Microservice API, so Products Microservice API calls
                        //              will be denied (insufficient scope). The microservice would not be authorized to access protected endpoints.
                
                },

                RefreshTokenUsage = TokenUsage.ReUse,
                    // 🡡__ WHY   : ReUse simplifies server-side handling for refresh tokens and avoids the need to implement rotation/one-time logic.
                    //              Use ReUse for scenarios where the refresh token is stored securely and where you prefer simpler lifecycle management.
                    // 🡡__ IF NOT: OneTimeOnly (rotation) would increase security by invalidating refresh tokens after use, but requires additional server-side
                    //              bookkeeping and careful handling of concurrent refresh requests.

                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 3600
                    // 🡡__ WHY   : Sliding expiration helps keep active users authenticated without forcing frequent full re-authentication. The value
                    //              of 3600 seconds establishes the sliding window; each successful refresh within that window extends validity.
                    // 🡡__ IF NOT: Absolute expiration would set a hard timeout after which the refresh token is invalid regardless of usage. If sliding
                    //              is omitted and tokens are short-lived, clients must reauthenticate more often.            
            },

            // Orders Microservice Client (BFF using NestJS, and not ASP.NET Core 10).
            new()
			{
				ClientId = OrdersMicroservice.CLIENT_ID_FOR_IDP,
				ClientName = OrdersMicroservice.CLIENT_NAME_FOR_IDP,
				ClientSecrets = { new Secret(OrdersMicroservice.CLIENT_SECRET_FOR_IDP.Sha256()) },

				AllowedGrantTypes = GrantTypes.Code,
                    // 🡡__ WHY   : The Products microservice (if acting as a confidential client or BFF) should use Authorization Code to keep tokens
                    //              private on the server and to benefit from the standard OIDC/OAuth flow, including PKCE if applicable.
                    // 🡡__ IF NOT: Using non-confidential or browser flows could expose tokens to the client-side, allowing token theft via XSS
                    //              and making secure API access more difficult to enforce.

                RedirectUris = { $"{OrdersMicroservice.BFF_CLIENT_BASE_URL}/api/auth/callback" },
				PostLogoutRedirectUris = { $"{OrdersMicroservice.BFF_CLIENT_BASE_URL}/signout-callback-oidc" },
				// FrontChannelLogoutUri = $"{OrdersMicroservice.CLIENT_BASE_URL }/signout-oidc",

				AllowOfflineAccess = true,
                    // 🡡__ WHY   : Products Microservice BFF frontend may need refresh tokens to maintain backend sessions or to act on behalf of the user without interactive login.
                    //              For server-to-server or long-running operations, refresh tokens enable seamless token renewal.
                    // 🡡__ IF NOT: Without offline access, the service cannot obtain refresh tokens and must force users to re-authenticate when access tokens expire.


                AllowedScopes =
				{
					IdentityServerConstants.StandardScopes.OpenId,
					IdentityServerConstants.StandardScopes.Profile,
					IdentityServerConstants.StandardScopes.Email,
					"roles",
					MicroserviceApiResources.ORDERS_API
                        // 🡡__ WHY   : Including the PRODUCTS_API scope permits the Products Microservice BFF client to request access tokens that include scope permissions for the
                        //              Products Microservice API. The Products Microservice API will validate the access token and require the corresponding scope to authorize API calls.
                        // 🡡__ IF NOT: If this scope is not included, tokens issued to the client will not be valid for calling the Products Microservice API, so Products Microservice API calls
                        //              will be denied (insufficient scope). The microservice would not be authorized to access protected endpoints.
                
                },

				RefreshTokenUsage = TokenUsage.ReUse,
                    // 🡡__ WHY   : ReUse simplifies server-side handling for refresh tokens and avoids the need to implement rotation/one-time logic.
                    //              Use ReUse for scenarios where the refresh token is stored securely and where you prefer simpler lifecycle management.
                    // 🡡__ IF NOT: OneTimeOnly (rotation) would increase security by invalidating refresh tokens after use, but requires additional server-side
                    //              bookkeeping and careful handling of concurrent refresh requests.

                RefreshTokenExpiration = TokenExpiration.Sliding,
				SlidingRefreshTokenLifetime = 3600
                    // 🡡__ WHY   : Sliding expiration helps keep active users authenticated without forcing frequent full re-authentication. The value
                    //              of 3600 seconds establishes the sliding window; each successful refresh within that window extends validity.
                    // 🡡__ IF NOT: Absolute expiration would set a hard timeout after which the refresh token is invalid regardless of usage. If sliding
                    //              is omitted and tokens are short-lived, clients must reauthenticate more often.            
            }
		];
}