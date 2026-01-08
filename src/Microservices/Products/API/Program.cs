using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using FW.Landscape.Common;
using FW.Microservices.Products.API.DBAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductsDbConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
		        // 🡡__ WHY   : Prevents JSON serialization failures or infinite loops when entity graphs contain cyclical references
                //             (e.g., parent -> children -> parent). It instructs System.Text.Json to ignore back-references while still
                //             serializing the forward relationships so the response can be produced successfully.
		        // 🡡__ IF NOT: Serializing entity graphs with cycles will throw a runtime exception or hang, causing API endpoints to fail
                //             when returning EF Core tracked entities that reference each other. You'd otherwise need DTOs or manual trimming.                
    });

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
	// 🡡__ WHY   : Registers the API explorer which discovers route metadata used by OpenAPI/Swagger tools and other tooling that
    //             reflect over endpoints to generate documentation or client code.
	// 🡡__ IF NOT: Tools like Swagger/Swashbuckle won't be able to enumerate your endpoints and produce an API specification automatically,
    //             making it harder for developers and integrators to explore and test your API.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
	    // 🡡__ WHY   : Configures JWT Bearer authentication so the microservice will accept and validate access tokens issued by the IDP.
        //             This middleware reads the Authorization: Bearer header, validates the token signature and claims, and populates the user principal.
	    // 🡡__ IF NOT: The API would not validate incoming bearer tokens and would either be unauthenticated (401) or would need a different auth mechanism.
    {
        options.Authority = IDP.AUTHORITY;
        options.Audience = MicroserviceApiResources.PRODUCTS_API;
		    // 🡡__ WHY   : The Authority tells the middleware where to fetch issuer metadata (discovery) and signing keys. Audience restricts tokens
            //             to those issued for this API (ensures the token's "aud" claim matches the API identifier).
		    // 🡡__ IF NOT: Without a correct Authority the middleware cannot validate issuer or keys; without an Audience check tokens intended for other APIs
            //             could be accepted, weakening security and enabling token replay across services.

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
	    // 🡡__ WHY   : Defines a named policy requiring an authenticated principal and a specific "scope" claim matching the API scope.
        //             This centralizes the scope requirement and allows applying it consistently across controllers/endpoints.
	    // 🡡__ IF NOT: You would have to manually check for scope claims inside controllers or risk endpoints being accessible without the required scope.
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", MicroserviceApiResources.PRODUCTS_API);
		    // 🡡__ WHY   : Ensures the access token contains the exact scope that grants access to this microservice, implementing scope-based authorization.
		    // 🡡__ IF NOT: Tokens lacking the PRODUCTS_API scope could be accepted and allowed to invoke endpoints, causing insufficient-scope security lapses.
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
		        // 🡡__ WHY   : Provides permissive CORS for development and simple integration testing so browsers can call this API from any origin.
		        // 🡡__ IF NOT: Strict CORS policies could block browser-based clients from making requests during development, but in production
		        //             you should restrict origins, headers and methods to prevent cross-origin abuse and reduce attack surface.


            // FIXME:
            // FIXME: Do you really need this CORS settings? The browser JavaScripts
            // FIXME: are NOT going to call this Microservice API, and the only
            // FIXME: consumer is the C# code on the Products BFF (ASP.NET Core 10).
            // FIXME: So, do you really need this?
            // FIXME:

    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("ApiScope");
    // 🡡__ WHY   : Applies the "ApiScope" policy to all controller routes, ensuring endpoints only respond to requests that present a valid
    //             token containing the PRODUCTS_API scope and are otherwise authenticated.
    // 🡡__ IF NOT: Controllers would be accessible without the centralized scope check, risking accidental exposure of protected functionality.

app.Run();