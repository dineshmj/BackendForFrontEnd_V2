using Microsoft.EntityFrameworkCore;

using Serilog;

using FW.IDP.DBAccess;
using FW.IDP.Security;
using FW.IDP.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddDbContext<IdentityDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("IdentityDbConnection")));
	        // 🡡__ WHY   : Establishes SQLite connection for identity data storage and initialzes the DB context..

	builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPasswordManager, PasswordManager>();

    builder.Services.AddRazorPages();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
		    // 🡡__ WHY   : Required when authentication involves cross-site navigations or third-party redirects (e.g.
		    //              OIDC/OAuth authorization callbacks, iframe-based flows or cross-origin BFFs). `SameSite=None`
		    //              explicitly permits the browser to send the cookie on cross-site requests when paired with Secure.
		    // 🡡__ IF NOT: Modern browsers will apply Lax/Strict defaults and may block the cookie during cross-site callbacks,
		    //              causing login, single-sign-on, or callback flows to fail (missing session cookie, login loop).

		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
    });

    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.None;
		    // 🡡__ WHY   : Ensures the app-level cookie policy does not downgrade or override per-cookie SameSite settings and
		    //              allows cookies to be sent in cross-site contexts (important for OAuth/OIDC redirect flows).
		    // 🡡__ IF NOT: The framework or middleware might enforce a more restrictive SameSite behavior globally, again
		    //              causing authentication cookies to be withheld on cross-site requests and breaking sign-in flows.
		options.Secure = CookieSecurePolicy.Always;
    });

    builder.Services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.EmitStaticAudienceClaim = true;

            options.Authentication.CookieSameSiteMode = SameSiteMode.None;
			    // 🡡__ WHY   : IdentityServer's internal authentication/session cookie must allow cross-site delivery for OIDC
			    //              authorization flows and external callback scenarios. Setting SameSite=None ensures the IDP's cookie
			    //              will be presented during the external redirect back from clients/IDPs.
			    // 🡡__ IF NOT: Callbacks from the /authorize endpoint and other cross-site requests may not include the IdentityServer
			    //              cookie, resulting in authentication failures, missing session state or repeated interactive login.
			options.Authentication.CookieLifetime = TimeSpan.FromHours(10);
        })
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryApiResources (Config.ApiResources)
        .AddInMemoryClients(Config.Clients)
        .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
		    // 🡡__ WHY   : Registers a custom Resource Owner Password validator to validate username/password for the
		    //              ROPC (resource owner password credentials) grant or any flow that delegates credential validation
		    //              to your custom logic (e.g., check user store, password hashing rules, lockout, MFA checks).
		    // 🡡__ IF NOT: Default validation will not run (or ROPC will be unsupported). If your system relies on custom
		    //              password verification or additional checks (lockout, claims mapping), omitting this will prevent
		    //              those checks and could mean users cannot authenticate via those flows.
		.AddProfileService<CustomProfileService>()
		    // 🡡__ WHY   : Adds a profile service used to populate identity tokens and the UserInfo endpoint with custom claims.
		    //              This is where you map data from your user store into claims, filter scopes, and provide dynamic claims.
		    // 🡡__ IF NOT: Tokens and UserInfo responses will only contain default, static claims. Any required custom claims
		    //              (roles, tenant ids, feature flags) will be missing which can break authorization decisions in clients/APIs.
		.AddDeveloperSigningCredential ();
	        // 🡡__ WHY   : Provides a temporary signing credential (for dev/local) so IdentityServer can sign tokens (JWTs).
	        //              It simplifies development by auto-generating a key if none is supplied.
	        // 🡡__ IF NOT: IdentityServer will fail to start or will be unable to issue signed tokens unless a proper certificate
	        //              or signing key is configured. For production, replace with a persisted certificate/KeyMaterial.


	var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseCookiePolicy();
    app.UseIdentityServer();
    app.UseAuthorization();
    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}