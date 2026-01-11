using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using Duende.Bff.Yarp;

using FW.Landscape.Common;
using FW.PAS.BFFWeb.DBAccess;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
	// 🡡__ WHY   : Prevents the JwtSecurityTokenHandler from remapping standard JWT claim types
	//              (e.g. "sub" -> ClaimTypes.NameIdentifier). Keeping the original claim names ensures
	//              consistent claim handling between IdentityServer, the OIDC middleware and downstream APIs.
	// 🡡__ IF NOT: The framework will remap JWT claim types to Microsoft-specific claim type names which can
	//              confuse token->claim mapping, cause mismatches when the application expects raw JWT claim
	//              names (like "sub" or "role"), and break authorization checks that rely on the canonical names.

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
	// 🡡__ WHY   : Session provides a server-side store for short-lived user data (e.g. temp state, correlation IDs,
	//              or small UI session values) and works with the BFF pattern to keep per-user state across requests.
	//				NOTE: In this BFF scenario, session is primarily used by the Duende BFF library to manage user sessions.
	//              The current logic written by the developer in this BFF does not directly use session, per se.
	// 🡡__ IF NOT: Without session, any logic that expects per-user server-side state via ISession will fail. Also
	//              certain middlewares or libraries that rely on session being present may throw or lose state.
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<MenuDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MenuDbConnection")));

builder.Services.AddBff()
	// 🡡__ WHY   : Registers the Duende BFF services which implement the Backend-For-Frontend pattern helpers,
	//              such as secure cookie-based user sessions, CSRF protection, and the lightweight API proxy helpers.
	// 🡡__ IF NOT: You would miss out on built-in BFF features (session handling, secure cookie patterns, anti-CSRF)
	//              and have to implement these aspects manually which increases security and implementation risk.
    .AddRemoteApis();
		// 🡡__ WHY   : Enables the BFF's remote API integration (YARP-backed) allowing the BFF to expose proxied endpoints
		//              that securely call backend microservices on behalf of the authenticated user.
		// 🡡__ IF NOT: You'd either need to implement proxying yourself or have clients call microservices directly,
		//              exposing tokens to the browser and removing the benefits of the BFF pattern.

builder.Services.AddScoped<IMenuRepository, MenuRepository>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "oidc";
			// 🡡__ WHY   : Sets the default challenge to the named OpenID Connect scheme so that
			//              unauthenticated requests trigger an OIDC authentication challenge (redirect to the IDP).
			// 🡡__ IF NOT: The system might fall back to a different challenge (or none), causing authentication flows
			//              to not redirect correctly to the identity provider and resulting in 401s or confusing behavior.
        options.DefaultSignOutScheme = "oidc";
			// 🡡__ WHY   : Ensures sign-out operations use the OIDC sign-out flow (so the user is logged out at the IDP).
			// 🡡__ IF NOT: Sign-out may only clear the local cookie without notifying the IDP, leaving SSO sessions active
			//              at the Identity Provider and causing stale sessions or surprising UX.
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = CookieNames.PAS_SHELL_HOST_BFF;

        options.Cookie.Path = "/";
			// 🡡__ WHY   : Ensures the cookie is sent for requests to all paths of the host, including the BFF proxy and SPA.
			//              This is important when the app serves multiple endpoints under different routes.
			// 🡡__ IF NOT: A more restrictive path would prevent the cookie from being included on some requests, breaking
			//              authentication for those routes (unexpected 401s).
        
        options.Cookie.SameSite = SameSiteMode.None;
			// 🡡__ WHY   : Required for OIDC redirect flows and cross-site requests involving the IDP (authorization callbacks).
			//              SameSite=None combined with Secure allows the cookie to be sent on cross-site redirects.
			// 🡡__ IF NOT: Browsers may block the cookie during the authorization callback or when used inside iframes,
			//              causing login/logout and SSO flows to fail.

        options.Cookie.HttpOnly = true;
			// 🡡__ WHY   : Prevents JavaScript from reading the cookie (mitigates XSS theft).
			// 🡡__ IF NOT: The cookie becomes accessible to client-side scripts, increasing the risk of token/session theft
			//              through XSS vulnerabilities.
        
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = IDP.AUTHORITY;
        options.ClientId = PASShellBFF.CLIENT_ID_FOR_IDP;
        options.ClientSecret = PASShellBFF.CLIENT_SECRET_FOR_IDP;

        options.ResponseType = "code";
			// 🡡__ WHY   : The authorization code response type enforces the Authorization Code flow where the server
			//              exchanges a code for tokens — suitable for confidential BFF-style clients and more secure.
			// 🡡__ IF NOT: Using implicit or token-based response types would expose tokens to the browser and increase
			//              the risk of interception and XSS-based token theft.

        options.ResponseMode = "query";
			// 🡡__ WHY   : Instructs the IDP to return the authorization response parameters in the query string for the OIDC callback,
			//              matching how the client expects to receive the authorization code.
			// 🡡__ IF NOT: A mismatch in response mode could cause the client to not find the authorization code (e.g., if the IDP returned it in form post).

        options.MapInboundClaims = false;
			// 🡡__ WHY   : Keeps claim names from being remapped by the middleware, preserving original JWT/OIDC claim names for consistency.
			// 🡡__ IF NOT: The handler will remap claim names to Microsoft-centric names which may break authorization logic expecting raw claim types.
        
        options.ClaimActions.MapJsonKey("role", "role", "role");
			// 🡡__ WHY   : Ensures the "role" claim from the UserInfo JSON payload or token is mapped into the principal so Role-based
			//              authorization works as expected within ASP.NET (and so TokenValidationParameters.RoleClaimType aligns).
			// 🡡__ IF NOT: Role information may be omitted from the created ClaimsPrincipal, causing role-based checks to fail.
        
        options.SaveTokens = true;
			// 🡡__ WHY   : Persists the access and refresh tokens in the authentication ticket so the BFF can use them to call APIs.
			// 🡡__ IF NOT: Tokens would not be available via the authentication properties making it harder for the server to call
			//              downstream APIs on behalf of the user (you'd need to implement manual token handling).

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("roles");
        options.Scope.Add("offline_access");

        options.CorrelationCookie.SameSite = SameSiteMode.None;
			// 🡡__ WHY   : Correlation cookies are used to tie the outgoing authentication request to the incoming response.
			//              SameSite=None ensures these cookies participate in cross-site OIDC redirects and callback flows.
			// 🡡__ IF NOT: The correlation cookie may be blocked on cross-site redirects, breaking CSRF protections and invalidating callbacks.
        
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
			// 🡡__ WHY   : Ensures the correlation cookie is only sent over HTTPS, preventing exposure over plaintext HTTP.
			// 🡡__ IF NOT: The cookie could be transmitted over insecure channels and be vulnerable to interception.

        options.NonceCookie.SameSite = SameSiteMode.None;
			// 🡡__ WHY   : The nonce cookie is used to prevent token replay. SameSite=None allows it to be sent during the OIDC callback.
			// 🡡__ IF NOT: The nonce cookie may be blocked during the callback, causing the middleware to reject the response due to missing nonce.

        options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
            // 🡡__ WHY   : Ensures the nonce cookie is only sent over HTTPS, reducing risk of interception.
			// 🡡__ IF NOT: Nonce could be leaked over HTTP and attackers could attempt replay attacks.

        options.GetClaimsFromUserInfoEndpoint = true;
			// 🡡__ WHY   : Fetches additional user claims from the UserInfo endpoint (useful if the ID token doesn't contain all claims).
			//              This is particularly useful when the IDP returns minimal claims in the ID token but exposes more via UserInfo.
			// 🡡__ IF NOT: You may have an incomplete ClaimsPrincipal (missing profile or custom claims) and may need to request more claims
			//               via scopes or rely solely on what the ID token contains.

        options.TokenValidationParameters.NameClaimType = "name";
			// 🡡__ WHY   : Tell the token validator which claim to use as the principal's name so ASP.NET identity APIs (User.Identity.Name)
			//              work as expected.
			// 🡡__ IF NOT: The framework might pick a different claim type as the name which can lead to unexpected values in User.Identity.Name.

        options.TokenValidationParameters.RoleClaimType = "role";
			// 🡡__ WHY   : Aligns role-based checks with the "role" claim emitted by the IDP so Authorize(Role=...) works correctly.
			// 🡡__ IF NOT: The runtime may look for a different claim type for roles and role checks will fail even if "role" claims are present.
    });

builder.Services.AddOpenIdConnectAccessTokenManagement ();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment ())
{
	app.UseDeveloperExceptionPage ();
}
else
{
	app.UseHsts ();
}

app.UseSession();
	// 🡡__ WHY   : Ensures the session middleware is part of the pipeline so server-side session values (ISession) are available to handlers.
	// 🡡__ IF NOT: Calls to HttpContext.Session will throw or return uninitialized data and any code relying on session state will fail.

app.UseHttpsRedirection();
	// 🡡__ WHY   : Redirects plain HTTP requests to HTTPS to guarantee transport security for cookies and token exchanges.
	// 🡡__ IF NOT: Sensitive data (cookies, tokens) could be transmitted over plaintext HTTP and be intercepted or modified.

app.UseDefaultFiles();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/bff/api"))
    {
        // Rewrite the path to remove /bff
        context.Request.Path = context.Request.Path.Value.Replace("/bff", "");

        context.SetEndpoint(null);
			// 🡡__ WHY   : Clearing the endpoint prevents endpoint routing from matching a previously set endpoint,
            //              allowing the proxied YARP pipeline (or controller routing) to re-evaluate the request path
            //              against the backend API routes after the rewrite.
			// 🡡__ IF NOT: The original endpoint may remain bound to the request which can cause incorrect routing,
            //              unnecessary authorization checks, or the request bypassing the intended proxy path resolution.
    }
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseBff();
	// 🡡__ WHY   : Enables BFF middleware which integrates authentication, anti-forgery, and proxy helpers into the pipeline.
    //               It wires up the route protection and the server-side token/session management used by remote API calls.
	// 🡡__ IF NOT: BFF-specific features (secure API proxy, built-in CSRF protections, BFF session helpers) won't run and
    //               your app would behave like a plain web app without the BFF security model.

// Protect all controllers by default except LogoutAllController.
app.MapControllers()
    .RequireAuthorization();
		// 🡡__ WHY   : Makes all controller endpoints require an authenticated user by default, enforcing a secure-by-default posture.
		// 🡡__ IF NOT: Controllers would be publicly accessible unless individually protected, increasing the risk of accidental exposure.

// LogoutAll action method must be public and hence it is not protected by default authorization.
app.MapControllerRoute(
    name: "logout-all",
    pattern: "bff/logout-all/{action=Index}",
    defaults: new { controller = "LogoutAll" }
);

app.MapBffManagementEndpoints();
	// 🡡__ WHY   : Registers management endpoints used by the BFF (e.g., to inspect or administrate remote API mappings, token
    //               management, and health checks). These are useful for development and operational diagnostics.
	// 🡡__ IF NOT: You will lack the BFF management endpoints which can make debugging and runtime diagnostics harder;
    //               however, consider restricting or disabling these in production if they expose sensitive operations.

//
// Intentionally commented out to enable this Microservice's SPA set directly to the iFrame of the Shell SPA application and avoid unnecessary redirects
// within the PAS Shell BFF.
//
// app.MapFallbackToFile("index.html");
//

app.Run();