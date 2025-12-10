using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using FW.Landscape.Common;
using FW.Microservices.Orders.API.DBAccess;
using FW.Microservices.Orders.API.GraphQL.Queries;

var builder = WebApplication.CreateBuilder (args);

builder.Services.AddDbContextFactory<OrderDbContext> (options =>
	options.UseSqlite (builder.Configuration.GetConnectionString ("OrdersDbConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository> ();

builder.Services
	.AddGraphQLServer ()
	.AddQueryType<OrderQuery> ()
	.AddProjections ()
	.AddFiltering ()
	.AddSorting ()
	.RegisterDbContextFactory<OrderDbContext> ()
	.AddAuthorization ();

builder.Services
	.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer (options =>
	{
		options.Authority = IDP.AUTHORITY;
		options.Audience = MicroserviceApiResources.ORDERS_API;

		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = true,
			ValidateIssuerSigningKey = true,
			ValidateLifetime = true
		};
	});

builder.Services.AddAuthorization (options =>
{
	options.AddPolicy ("ApiScope", policy =>
	{
		policy.RequireAuthenticatedUser ();
		policy.RequireClaim ("scope", MicroserviceApiResources.ORDERS_API);
	});
});

builder.Services.AddCors (options =>
{
	options.AddDefaultPolicy (policy =>
	{
		policy.AllowAnyOrigin ()
			  .AllowAnyHeader ()
			  .AllowAnyMethod ();
	});
});

var app = builder.Build ();

if (app.Environment.IsDevelopment ())
{
	app.UseDeveloperExceptionPage ();
}

app.UseHttpsRedirection ();
app.UseCors ();

app.UseAuthentication ();
app.UseAuthorization ();

app.MapGraphQL ()
   .RequireAuthorization ("ApiScope");

app.Run ();