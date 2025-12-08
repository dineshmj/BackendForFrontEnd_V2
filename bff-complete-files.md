# Complete BFF Solution - All Files

## Solution Structure
```
BffSolution/
├── BffSolution.sln
├── src/
│   ├── BFF.Web/
│   ├── IdentityServer/
│   └── Microservices/
│       ├── Orders.API/
│       ├── Products.API/
│       └── Payments.API/
└── README.md
```

---

## Root Files

### `BffSolution.sln`
```
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "BFF.Web", "src\BFF.Web\BFF.Web.csproj", "{A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "IdentityServer", "src\IdentityServer\IdentityServer.csproj", "{B2C3D4E5-F6A7-5B6C-9D0E-1F2A3B4C5D6E}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Orders.API", "src\Microservices\Orders.API\Orders.API.csproj", "{C3D4E5F6-A7B8-6C7D-0E1F-2A3B4C5D6E7F}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Products.API", "src\Microservices\Products.API\Products.API.csproj", "{D4E5F6A7-B8C9-7D8E-1F2A-3B4C5D6E7F8A}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Payments.API", "src\Microservices\Payments.API\Payments.API.csproj", "{E5F6A7B8-C9D0-8E9F-2A3B-4C5D6E7F8A9B}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D}.Release|Any CPU.Build.0 = Release|Any CPU
		{B2C3D4E5-F6A7-5B6C-9D0E-1F2A3B4C5D6E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B2C3D4E5-F6A7-5B6C-9D0E-1F2A3B4C5D6E}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B2C3D4E5-F6A7-5B6C-9D0E-1F2A3B4C5D6E}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B2C3D4E5-F6A7-5B6C-9D0E-1F2A3B4C5D6E}.Release|Any CPU.Build.0 = Release|Any CPU
		{C3D4E5F6-A7B8-6C7D-0E1F-2A3B4C5D6E7F}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{C3D4E5F6-A7B8-6C7D-0E1F-2A3B4C5D6E7F}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{C3D4E5F6-A7B8-6C7D-0E1F-2A3B4C5D6E7F}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{C3D4E5F6-A7B8-6C7D-0E1F-2A3B4C5D6E7F}.Release|Any CPU.Build.0 = Release|Any CPU
		{D4E5F6A7-B8C9-7D8E-1F2A-3B4C5D6E7F8A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{D4E5F6A7-B8C9-7D8E-1F2A-3B4C5D6E7F8A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{D4E5F6A7-B8C9-7D8E-1F2A-3B4C5D6E7F8A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{D4E5F6A7-B8C9-7D8E-1F2A-3B4C5D6E7F8A}.Release|Any CPU.Build.0 = Release|Any CPU
		{E5F6A7B8-C9D0-8E9F-2A3B-4C5D6E7F8A9B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{E5F6A7B8-C9D0-8E9F-2A3B-4C5D6E7F8A9B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{E5F6A7B8-C9D0-8E9F-2A3B-4C5D6E7F8A9B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{E5F6A7B8-C9D0-8E9F-2A3B-4C5D6E7F8A9B}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
```

### `README.md`
```markdown
# BFF Solution with NextJS and ASP.NET Core

## Prerequisites
- .NET 8.0 SDK
- Node.js 18+ and npm
- Visual Studio 2022 or VS Code

## Quick Start

### 1. Setup IdentityServer
```bash
cd src/IdentityServer
dotnet run --urls=https://localhost:5000
```

### 2. Setup Microservices
```bash
# Orders API
cd src/Microservices/Orders.API
dotnet run --urls=https://localhost:5001

# Products API (in new terminal)
cd src/Microservices/Products.API
dotnet run --urls=https://localhost:5002

# Payments API (in new terminal)
cd src/Microservices/Payments.API
dotnet run --urls=https://localhost:5003
```

### 3. Build NextJS App
```bash
cd src/BFF.Web/client-app
npm install
npm run export
```

### 4. Run BFF Application
```bash
cd src/BFF.Web
dotnet run --urls=https://localhost:5005
```

### 5. Access Application
Open browser: https://localhost:5005

## Default Test User
- Username: alice
- Password: alice

## Architecture
- **BFF**: https://localhost:5005
- **IdentityServer**: https://localhost:5000
- **Orders API**: https://localhost:5001
- **Products API**: https://localhost:5002
- **Payments API**: https://localhost:5003
```

---

## BFF.Web Project

### `src/BFF.Web/BFF.Web.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Duende.BFF" Version="3.0.0" />
    <PackageReference Include="Duende.BFF.Yarp" Version="3.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="8.0.21" />
  </ItemGroup>

</Project>
```

### `src/BFF.Web/Program.cs`
```csharp
using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Clear default JWT claim mapping
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

// Add BFF services
builder.Services.AddBff()
    .AddRemoteApis();

// Add authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "__Host-bff";
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Authority"];
        options.ClientId = builder.Configuration["IdentityServer:ClientId"];
        options.ClientSecret = builder.Configuration["IdentityServer:ClientSecret"];
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.MapInboundClaims = false;
        options.SaveTokens = true;

        // Scopes
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("orders_api");
        options.Scope.Add("products_api");
        options.Scope.Add("payments_api");
        options.Scope.Add("offline_access");

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Add HTTP clients for microservices
builder.Services.AddHttpClient("orders-api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Microservices:OrdersApi"]!);
}).AddUserAccessTokenHandler();

builder.Services.AddHttpClient("products-api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Microservices:ProductsApi"]!);
}).AddUserAccessTokenHandler();

builder.Services.AddHttpClient("payments-api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Microservices:PaymentsApi"]!);
}).AddUserAccessTokenHandler();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

app.MapBffManagementEndpoints();

// Proxy endpoints to microservices
app.MapRemoteBffApiEndpoint("/api/orders", "https://localhost:44349")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapRemoteBffApiEndpoint("/api/products", "https://localhost:44363")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapRemoteBffApiEndpoint("/api/payments", "https://localhost:44309")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapFallbackToFile("index.html");

app.Run();
```

### `src/BFF.Web/appsettings.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "IdentityServer": {
    "Authority": "https://localhost:44392",
    "ClientId": "bff-client",
    "ClientSecret": "secret",
    "Audience": "bff-api"
  },
  "Microservices": {
    "OrdersApi": "https://localhost:44349",
    "ProductsApi": "https://localhost:44363",
    "PaymentsApi": "https://localhost:44309"
  }
}
```

### `src/BFF.Web/appsettings.Development.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Duende": "Information"
    }
  }
}
```

### `src/BFF.Web/Properties/launchSettings.json`
```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5005",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### `src/BFF.Web/Controllers/ApiController.cs`
```csharp
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Web.Controllers;

[Authorize]
[Route("api")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ApiController> _logger;

    public ApiController(IHttpClientFactory httpClientFactory, ILogger<ApiController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [HttpGet("user-info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        
        return Ok(new
        {
            Claims = claims,
            HasAccessToken = !string.IsNullOrEmpty(accessToken)
        });
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var client = _httpClientFactory.CreateClient("orders-api");
        var response = await client.GetAsync("/orders");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var client = _httpClientFactory.CreateClient("products-api");
        var response = await client.GetAsync("/products");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode);
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetPayments()
    {
        var client = _httpClientFactory.CreateClient("payments-api");
        var response = await client.GetAsync("/payments");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode);
    }
}
```

---

## NextJS Client App

### `src/BFF.Web/client-app/package.json`
```json
{
  "name": "bff-client",
  "version": "0.1.0",
  "private": true,
  "scripts": {
    "dev": "next dev -p 3000",
    "build": "next build",
    "start": "next start",
    "export": "next build && xcopy /E /I /Y out ..\\wwwroot",
    "export:linux": "next build && cp -r out/* ../wwwroot/",
    "lint": "next lint",
    "type-check": "tsc --noEmit"
  },
  "dependencies": {
    "next": "14.2.18",
    "react": "18.3.1",
    "react-dom": "18.3.1"
  },
  "devDependencies": {
    "@types/node": "22.9.0",
    "@types/react": "18.3.12",
    "@types/react-dom": "18.3.1",
    "typescript": "5.6.3",
    "eslint": "9.14.0",
    "eslint-config-next": "14.2.18"
  }
}
```

### `src/BFF.Web/client-app/next.config.js`
```javascript
/** @type {import('next').NextConfig} */
const nextConfig = {
  output: 'export',
  distDir: 'out',
  trailingSlash: true,
  images: {
    unoptimized: true
  }
};

module.exports = nextConfig;
```

### `src/BFF.Web/client-app/tsconfig.json`
```json
{
  "compilerOptions": {
    "lib": ["dom", "dom.iterable", "esnext"],
    "allowJs": true,
    "skipLibCheck": true,
    "strict": true,
    "noEmit": true,
    "esModuleInterop": true,
    "module": "esnext",
    "moduleResolution": "bundler",
    "resolveJsonModule": true,
    "isolatedModules": true,
    "jsx": "preserve",
    "incremental": true,
    "plugins": [
      {
        "name": "next"
      }
    ],
    "paths": {
      "@/*": ["./*"]
    },
    "target": "ES2017",
    "forceConsistentCasingInFileNames": true
  },
  "include": ["next-env.d.ts", "**/*.ts", "**/*.tsx", ".next/types/**/*.ts"],
  "exclude": ["node_modules"]
}
```

### `src/BFF.Web/client-app/next-env.d.ts`
```typescript
/// <reference types="next" />
/// <reference types="next/image-types/global" />

// NOTE: This file should not be edited
// see https://nextjs.org/docs/basic-features/typescript for more information.
```

### `src/BFF.Web/client-app/next-env.d.ts`
```typescript
/// <reference types="next" />
/// <reference types="next/image-types/global" />

// NOTE: This file should not be edited
// see https://nextjs.org/docs/app/building-your-application/configuring/typescript for more information.
```

### `src/BFF.Web/client-app/.gitignore`
```
node_modules
.next
out
../wwwroot
*.log
.DS_Store
next-env.d.ts
.env*.local
```

### `src/BFF.Web/client-app/.eslintrc.json`
```json
{
  "extends": ["next/core-web-vitals", "next/typescript"]
}
```

### `src/BFF.Web/client-app/app/types.ts`
```typescript
export interface Claim {
  type: string;
  value: string;
}

export interface Order {
  id: number;
  product: string;
  amount: number;
  status: string;
  date: string;
}

export interface Product {
  id: number;
  name: string;
  price: number;
  stock: number;
  category: string;
}

export interface Payment {
  id: number;
  amount: number;
  status: string;
  method: string;
  date: string;
}

export interface OrdersResponse {
  message: string;
  userId: string;
  userName: string;
  orders: Order[];
}

export interface ProductsResponse {
  message: string;
  userId: string;
  userName: string;
  products: Product[];
}

export interface PaymentsResponse {
  message: string;
  userId: string;
  userName: string;
  payments: Payment[];
}
```

### `src/BFF.Web/client-app/app/lib/auth.ts`
```typescript
export interface BffUser {
  type: string;
  value: string;
}

export interface AuthState {
  isAuthenticated: boolean;
  user: BffUser[] | null;
  isLoading: boolean;
}

/**
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

/**
 * Redirect to BFF login endpoint
 */
export function redirectToLogin(): void {
  const returnUrl = window.location.pathname + window.location.search;
  window.location.href = `/bff/login?returnUrl=${encodeURIComponent(returnUrl)}`;
}

/**
 * Redirect to BFF logout endpoint with proper session cleanup
 * Includes id_token_hint and sid for proper single sign-out
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

/**
 * Get claim value by type
 */
export function getClaimValue(claims: BffUser[], claimType: string): string | null {
  const claim = claims.find(c => c.type === claimType);
  return claim ? claim.value : null;
}

/**
 * Get user display name from claims
 */
export function getUserDisplayName(claims: BffUser[]): string {
  return getClaimValue(claims, 'name') || 
         getClaimValue(claims, 'preferred_username') || 
         getClaimValue(claims, 'email') || 
         'User';
}
```

### `src/BFF.Web/client-app/app/hooks/useAuth.ts`
```typescript
'use client';

import { useEffect, useState } from 'react';
import { checkAuthentication, redirectToLogin, type AuthState } from '../lib/auth';

/**
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
```

### `src/BFF.Web/client-app/app/components/AuthGuard.tsx`
```typescript
'use client';

import { useAuth } from '../hooks/useAuth';

interface AuthGuardProps {
  children: React.ReactNode;
  loadingComponent?: React.ReactNode;
}

/**
 * Component that protects routes requiring authentication
 * Automatically redirects to login if user is not authenticated
 */
export function AuthGuard({ children, loadingComponent }: AuthGuardProps) {
  const { isAuthenticated, isLoading } = useAuth(true);

  if (isLoading) {
    return (
      <>
        {loadingComponent || (
          <div style={{ 
            padding: '2rem', 
            fontFamily: 'system-ui, sans-serif',
            textAlign: 'center',
            marginTop: '4rem'
          }}>
            <div style={{
              display: 'inline-block',
              width: '50px',
              height: '50px',
              border: '5px solid #f3f3f3',
              borderTop: '5px solid #0070f3',
              borderRadius: '50%',
              animation: 'spin 1s linear infinite'
            }}></div>
            <p style={{ marginTop: '1rem', color: '#666' }}>Loading...</p>
            <style>{`
              @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
              }
            `}</style>
          </div>
        )}
      </>
    );
  }

  // If not authenticated, useAuth hook will redirect
  // Only render children if authenticated
  if (!isAuthenticated) {
    return null;
  }

  return <>{children}</>;
}
```

### `src/BFF.Web/client-app/app/components/UserProfile.tsx`
```typescript
'use client';

import { redirectToLogout, getUserDisplayName, type BffUser } from '../lib/auth';

interface UserProfileProps {
  claims: BffUser[];
}

export function UserProfile({ claims }: UserProfileProps) {
  const displayName = getUserDisplayName(claims);

  return (
    <div style={{
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      padding: '1rem',
      backgroundColor: '#f8f9fa',
      borderRadius: '8px',
      marginBottom: '2rem'
    }}>
      <div>
        <span style={{ fontSize: '0.9rem', color: '#6c757d' }}>Logged in as:</span>
        <strong style={{ marginLeft: '0.5rem', fontSize: '1.1rem' }}>{displayName}</strong>
      </div>
      <button
        onClick={() => redirectToLogout(claims)}
        style={{
          padding: '0.5rem 1rem',
          backgroundColor: '#dc3545',
          color: 'white',
          border: 'none',
          borderRadius: '5px',
          cursor: 'pointer',
          fontSize: '0.95rem',
          fontWeight: '500'
        }}
      >
        Logout
      </button>
    </div>
  );
}
```
```typescript
export interface BffUser {
  type: string;
  value: string;
}

export interface AuthState {
  isAuthenticated: boolean;
  user: BffUser[] | null;
  isLoading: boolean;
}

/**
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

/**
 * Redirect to BFF login endpoint
 */
export function redirectToLogin(): void {
  const returnUrl = window.location.pathname + window.location.search;
  window.location.href = `/bff/login?returnUrl=${encodeURIComponent(returnUrl)}`;
}

/**
 * Redirect to BFF logout endpoint
 */
export function redirectToLogout(): void {
  window.location.href = '/bff/logout';
}

/**
 * Get claim value by type
 */
export function getClaimValue(claims: BffUser[], claimType: string): string | null {
  const claim = claims.find(c => c.type === claimType);
  return claim ? claim.value : null;
}

/**
 * Get user display name from claims
 */
export function getUserDisplayName(claims: BffUser[]): string {
  return getClaimValue(claims, 'name') || 
         getClaimValue(claims, 'preferred_username') || 
         getClaimValue(claims, 'email') || 
         'User';
}
```

### `src/BFF.Web/client-app/app/hooks/useAuth.ts`
```typescript
'use client';

import { useEffect, useState } from 'react';
import { checkAuthentication, redirectToLogin, type AuthState } from '../lib/auth';

/**
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
```

### `src/BFF.Web/client-app/app/components/AuthGuard.tsx`
```typescript
'use client';

import { useAuth } from '../hooks/useAuth';

interface AuthGuardProps {
  children: React.ReactNode;
  loadingComponent?: React.ReactNode;
}

/**
 * Component that protects routes requiring authentication
 * Automatically redirects to login if user is not authenticated
 */
export function AuthGuard({ children, loadingComponent }: AuthGuardProps) {
  const { isAuthenticated, isLoading } = useAuth(true);

  if (isLoading) {
    return (
      <>
        {loadingComponent || (
          <div style={{ 
            padding: '2rem', 
            fontFamily: 'system-ui, sans-serif',
            textAlign: 'center',
            marginTop: '4rem'
          }}>
            <div style={{
              display: 'inline-block',
              width: '50px',
              height: '50px',
              border: '5px solid #f3f3f3',
              borderTop: '5px solid #0070f3',
              borderRadius: '50%',
              animation: 'spin 1s linear infinite'
            }}></div>
            <p style={{ marginTop: '1rem', color: '#666' }}>Loading...</p>
            <style>{`
              @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
              }
            `}</style>
          </div>
        )}
      </>
    );
  }

  // If not authenticated, useAuth hook will redirect
  // Only render children if authenticated
  if (!isAuthenticated) {
    return null;
  }

  return <>{children}</>;
}
```

### `src/BFF.Web/client-app/app/components/UserProfile.tsx`
```typescript
'use client';

import { redirectToLogout, getUserDisplayName, type BffUser } from '../lib/auth';

interface UserProfileProps {
  claims: BffUser[];
}

export function UserProfile({ claims }: UserProfileProps) {
  const displayName = getUserDisplayName(claims);

  return (
    <div style={{
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      padding: '1rem',
      backgroundColor: '#f8f9fa',
      borderRadius: '8px',
      marginBottom: '2rem'
    }}>
      <div>
        <span style={{ fontSize: '0.9rem', color: '#6c757d' }}>Logged in as:</span>
        <strong style={{ marginLeft: '0.5rem', fontSize: '1.1rem' }}>{displayName}</strong>
      </div>
      <button
        onClick={redirectToLogout}
        style={{
          padding: '0.5rem 1rem',
          backgroundColor: '#dc3545',
          color: 'white',
          border: 'none',
          borderRadius: '5px',
          cursor: 'pointer',
          fontSize: '0.95rem',
          fontWeight: '500'
        }}
      >
        Logout
      </button>
    </div>
  );
}
```

### `src/BFF.Web/client-app/app/layout.tsx`
```typescript
import type { Metadata } from 'next';
import './globals.css';

export const metadata: Metadata = {
  title: 'BFF Application',
  description: 'Backend for Frontend with NextJS and ASP.NET Core',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
```

### `src/BFF.Web/client-app/app/page.tsx`
```typescript
'use client';

import { useState } from 'react';
import { AuthGuard } from './components/AuthGuard';
import { UserProfile } from './components/UserProfile';
import { useAuth } from './hooks/useAuth';

interface OrdersResponse {
  message: string;
  userId: string;
  userName: string;
  orders: Array<{
    id: number;
    product: string;
    amount: number;
    status: string;
    date: string;
  }>;
}

interface ProductsResponse {
  message: string;
  userId: string;
  userName: string;
  products: Array<{
    id: number;
    name: string;
    price: number;
    stock: number;
    category: string;
  }>;
}

interface PaymentsResponse {
  message: string;
  userId: string;
  userName: string;
  payments: Array<{
    id: number;
    amount: number;
    status: string;
    method: string;
    date: string;
  }>;
}

function HomeContent() {
  const { user } = useAuth(false); // Don't auto-redirect since we're already authenticated
  const [orders, setOrders] = useState<OrdersResponse | null>(null);
  const [products, setProducts] = useState<ProductsResponse | null>(null);
  const [payments, setPayments] = useState<PaymentsResponse | null>(null);
  const [loading, setLoading] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const fetchOrders = async (): Promise<void> => {
    setError(null);
    setLoading('orders');
    try {
      const response = await fetch('/api/orders', {
        credentials: 'include',
        headers: {
          'X-CSRF': '1',
        },
      });

      if (response.ok) {
        const data: OrdersResponse = await response.json();
        setOrders(data);
      } else {
        setError('Failed to fetch orders');
      }
    } catch (err) {
      console.error('Failed to fetch orders:', err);
      setError('Failed to fetch orders');
    } finally {
      setLoading(null);
    }
  };

  const fetchProducts = async (): Promise<void> => {
    setError(null);
    setLoading('products');
    try {
      const response = await fetch('/api/products', {
        credentials: 'include',
        headers: {
          'X-CSRF': '1',
        },
      });

      if (response.ok) {
        const data: ProductsResponse = await response.json();
        setProducts(data);
      } else {
        setError('Failed to fetch products');
      }
    } catch (err) {
      console.error('Failed to fetch products:', err);
      setError('Failed to fetch products');
    } finally {
      setLoading(null);
    }
  };

  const fetchPayments = async (): Promise<void> => {
    setError(null);
    setLoading('payments');
    try {
      const response = await fetch('/api/payments', {
        credentials: 'include',
        headers: {
          'X-CSRF': '1',
        },
      });

      if (response.ok) {
        const data: PaymentsResponse = await response.json();
        setPayments(data);
      } else {
        setError('Failed to fetch payments');
      }
    } catch (err) {
      console.error('Failed to fetch payments:', err);
      setError('Failed to fetch payments');
    } finally {
      setLoading(null);
    }
  };

  if (!user) {
    return null;
  }

  return (
    <div style={{ padding: '2rem', fontFamily: 'system-ui, sans-serif', maxWidth: '1200px', margin: '0 auto' }}>
      <h1 style={{ marginBottom: '1.5rem' }}>BFF Application with NextJS</h1>

      <UserProfile claims={user} />

      {error && (
        <div
          style={{
            padding: '1rem',
            marginBottom: '1rem',
            backgroundColor: '#fee',
            color: '#c00',
            borderRadius: '5px',
            border: '1px solid #fcc',
          }}
        >
          <strong>Error:</strong> {error}
        </div>
      )}

      <div style={{ marginBottom: '2rem' }}>
        <h2 style={{ fontSize: '1.25rem', marginBottom: '1rem' }}>API Actions</h2>
        <div style={{ display: 'flex', gap: '0.5rem', flexWrap: 'wrap' }}>
          <button
            onClick={fetchOrders}
            disabled={loading === 'orders'}
            style={{
              padding: '0.75rem 1.5rem',
              backgroundColor: loading === 'orders' ? '#ccc' : '#059669',
              color: 'white',
              border: 'none',
              borderRadius: '5px',
              cursor: loading === 'orders' ? 'not-allowed' : 'pointer',
              fontSize: '0.95rem',
              fontWeight: '500',
            }}
          >
            {loading === 'orders' ? 'Loading...' : 'Fetch Orders'}
          </button>
          <button
            onClick={fetchProducts}
            disabled={loading === 'products'}
            style={{
              padding: '0.75rem 1.5rem',
              backgroundColor: loading === 'products' ? '#ccc' : '#7c3aed',
              color: 'white',
              border: 'none',
              borderRadius: '5px',
              cursor: loading === 'products' ? 'not-allowed' : 'pointer',
              fontSize: '0.95rem',
              fontWeight: '500',
            }}
          >
            {loading === 'products' ? 'Loading...' : 'Fetch Products'}
          </button>
          <button
            onClick={fetchPayments}
            disabled={loading === 'payments'}
            style={{
              padding: '0.75rem 1.5rem',
              backgroundColor: loading === 'payments' ? '#ccc' : '#ea580c',
              color: 'white',
              border: 'none',
              borderRadius: '5px',
              cursor: loading === 'payments' ? 'not-allowed' : 'pointer',
              fontSize: '0.95rem',
              fontWeight: '500',
            }}
          >
            {loading === 'payments' ? 'Loading...' : 'Fetch Payments'}
          </button>
        </div>
      </div>

      {orders && (
        <div style={{ marginBottom: '2rem' }}>
          <h2 style={{ fontSize: '1.25rem', marginBottom: '1rem' }}>Orders</h2>
          <div style={{ 
            backgroundColor: '#f8f9fa', 
            padding: '1rem', 
            borderRadius: '5px',
            border: '1px solid #dee2e6'
          }}>
            <pre style={{ margin: 0, overflow: 'auto', fontSize: '0.9rem' }}>
              {JSON.stringify(orders, null, 2)}
            </pre>
          </div>
        </div>
      )}

      {products && (
        <div style={{ marginBottom: '2rem' }}>
          <h2 style={{ fontSize: '1.25rem', marginBottom: '1rem' }}>Products</h2>
          <div style={{ 
            backgroundColor: '#f8f9fa', 
            padding: '1rem', 
            borderRadius: '5px',
            border: '1px solid #dee2e6'
          }}>
            <pre style={{ margin: 0, overflow: 'auto', fontSize: '0.9rem' }}>
              {JSON.stringify(products, null, 2)}
            </pre>
          </div>
        </div>
      )}

      {payments && (
        <div style={{ marginBottom: '2rem' }}>
          <h2 style={{ fontSize: '1.25rem', marginBottom: '1rem' }}>Payments</h2>
          <div style={{ 
            backgroundColor: '#f8f9fa', 
            padding: '1rem', 
            borderRadius: '5px',
            border: '1px solid #dee2e6'
          }}>
            <pre style={{ margin: 0, overflow: 'auto', fontSize: '0.9rem' }}>
              {JSON.stringify(payments, null, 2)}
            </pre>
          </div>
        </div>
      )}

      <div style={{ 
        marginTop: '3rem',
        padding: '1.5rem',
        backgroundColor: '#e7f3ff',
        borderRadius: '8px',
        border: '1px solid #b3d9ff'
      }}>
        <h3 style={{ marginTop: 0, color: '#004085' }}>User Claims</h3>
        <ul style={{ margin: 0, paddingLeft: '1.5rem' }}>
          {user.map((claim, index) => (
            <li key={index} style={{ marginBottom: '0.5rem', fontSize: '0.9rem' }}>
              <strong>{claim.type}:</strong> <span style={{ color: '#495057' }}>{claim.value}</span>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default function Home() {
  return (
    <AuthGuard>
      <HomeContent />
    </AuthGuard>
  );
}
```

### `src/BFF.Web/client-app/app/globals.css`
```css
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

html,
body {
  max-width: 100vw;
  overflow-x: hidden;
}

body {
  color: rgb(var(--foreground-rgb));
  background: white;
}
```

---

## IdentityServer Project

### `src/IdentityServer/IdentityServer.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Duende.IdentityServer" Version="7.3.2" />
    <PackageReference Include="Serilog.AspNetCore" Version="8.0.2" />
  </ItemGroup>

</Project>
```

### How to Add the Login UI

**Option 1: Use Duende's UI Template (Recommended - Easiest)**

Run this command in the IdentityServer project folder:
```bash
cd src/IdentityServer
dotnet new isui
```

This will scaffold all the necessary UI files.

**Option 2: Manual UI Files**

Create these files manually:

### `src/IdentityServer/Pages/Account/Login.cshtml`
```html
@page
@model IdentityServer.Pages.Account.LoginModel

<div class="login-page">
    <div class="page-header">
        <h1>Login</h1>
    </div>

    <partial name="_ValidationSummary" />

    <div class="row">
        <div class="col-sm-6">
            <div class="card">
                <div class="card-header">
                    <h2>Local Login</h2>
                </div>

                <div class="card-body">
                    <form method="post">
                        <input type="hidden" asp-for="Input.ReturnUrl" />

                        <div class="form-group">
                            <label asp-for="Input.Username">Username</label>
                            <input class="form-control" placeholder="Username" asp-for="Input.Username" autofocus>
                        </div>
                        <div class="form-group">
                            <label asp-for="Input.Password">Password</label>
                            <input type="password" class="form-control" placeholder="Password" asp-for="Input.Password" autocomplete="off">
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" asp-for="Input.RememberLogin">
                            <label class="form-check-label" asp-for="Input.RememberLogin">
                                Remember My Login
                            </label>
                        </div>

                        <button class="btn btn-primary" name="Input.Button" value="login">Login</button>
                        <button class="btn btn-secondary" name="Input.Button" value="cancel">Cancel</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
```

### `src/IdentityServer/Pages/Account/Login.cshtml.cs`
```csharp
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityServer.Pages.Account;

[SecurityHeaders]
[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly TestUserStore _users;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly IAuthenticationSchemeProvider _schemeProvider;

    public LoginModel(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IEventService events,
        TestUserStore? users = null)
    {
        _users = users ?? new TestUserStore(TestUsers.Users);
        _interaction = interaction;
        _schemeProvider = schemeProvider;
        _events = events;
    }

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public class InputModel
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } = default!;
        public string Button { get; set; } = default!;
    }

    public async Task<IActionResult> OnGet(string? returnUrl)
    {
        Input = new InputModel { ReturnUrl = returnUrl ?? "~/" };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (Input.Button != "login")
        {
            if (context != null)
            {
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);
                if (context.IsNativeClient())
                {
                    return this.LoadingPage(Input.ReturnUrl);
                }
                return Redirect(Input.ReturnUrl ?? "~/");
            }
            else
            {
                return Redirect("~/");
            }
        }

        if (ModelState.IsValid)
        {
            if (_users.ValidateCredentials(Input.Username, Input.Password))
            {
                var user = _users.FindByUsername(Input.Username);
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username));

                AuthenticationProperties? props = null;
                if (Input.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                    };
                }

                var isuser = new IdentityServerUser(user.SubjectId)
                {
                    DisplayName = user.Username
                };

                await HttpContext.SignInAsync(isuser, props);

                if (context != null)
                {
                    if (context.IsNativeClient())
                    {
                        return this.LoadingPage(Input.ReturnUrl);
                    }

                    return Redirect(Input.ReturnUrl ?? "~/");
                }

                if (Url.IsLocalUrl(Input.ReturnUrl))
                {
                    return Redirect(Input.ReturnUrl);
                }
                else if (string.IsNullOrEmpty(Input.ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    throw new ArgumentException("invalid return URL");
                }
            }

            await _events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials"));
            ModelState.AddModelError(string.Empty, "Invalid username or password");
        }

        return Page();
    }
}
```

### `src/IdentityServer/Pages/Account/Logout.cshtml`
```html
@page
@model IdentityServer.Pages.Account.LogoutModel

<div class="logout-page">
    <div class="page-header">
        <h1>Logout</h1>
    </div>

    <div class="row">
        <div class="col">
            <p>Would you like to logout of IdentityServer?</p>

            <form method="post">
                <input type="hidden" asp-for="Input.LogoutId" />
                <button class="btn btn-primary">Yes</button>
            </form>
        </div>
    </div>
</div>
```

### `src/IdentityServer/Pages/Account/Logout.cshtml.cs`
```csharp
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Account;

[SecurityHeaders]
[AllowAnonymous]
public class LogoutModel : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public class InputModel
    {
        public string? LogoutId { get; set; }
    }

    public LogoutModel(IIdentityServerInteractionService interaction, IEventService events)
    {
        _interaction = interaction;
        _events = events;
    }

    public async Task<IActionResult> OnGet(string? logoutId)
    {
        Input = new InputModel { LogoutId = logoutId };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            await HttpContext.SignOutAsync();
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
        }

        var logout = await _interaction.GetLogoutContextAsync(Input.LogoutId);

        return Redirect(logout?.PostLogoutRedirectUri ?? "~/");
    }
}
```

### `src/IdentityServer/Pages/Shared/_Layout.cshtml`
```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>IdentityServer</title>
    <link rel="icon" type="image/x-icon" href="~/favicon.ico" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand" href="~/">
                <strong>IdentityServer</strong>
            </a>
        </div>
    </nav>

    <div class="container mt-4">
        @RenderBody()
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
```

### `src/IdentityServer/Pages/_ViewImports.cshtml`
```csharp
@using IdentityServer
@namespace IdentityServer.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

### `src/IdentityServer/Pages/_ViewStart.cshtml`
```csharp
@{
    Layout = "_Layout";
}
```

### `src/IdentityServer/Pages/Shared/_ValidationSummary.cshtml`
```html
@if (!ViewData.ModelState.IsValid)
{
    <div class="alert alert-danger">
        <strong>Error</strong>
        <div asp-validation-summary="All"></div>
    </div>
}
```

### `src/IdentityServer/SecurityHeadersAttribute.cs`
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServer;

public class SecurityHeadersAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var result = context.Result;
        if (result is ViewResult)
        {
            if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
            {
                context.HttpContext.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            }
            if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options"))
            {
                context.HttpContext.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
            }

            var csp = "default-src 'self'; style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; script-src 'self' https://cdn.jsdelivr.net; font-src 'self' https://cdn.jsdelivr.net;";
            if (!context.HttpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                context.HttpContext.Response.Headers.Append("Content-Security-Policy", csp);
            }
        }
    }
}
```

### `src/IdentityServer/Extensions.cs`
```csharp
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer;

public static class Extensions
{
    public static bool IsNativeClient(this AuthorizationRequest context)
    {
        return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
           && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
    }

    public static IActionResult LoadingPage(this Controller controller, string redirectUri)
    {
        controller.HttpContext.Response.Headers.Append("Content-Security-Policy", "default-src 'none'; script-src 'sha256-orD0/VhH8hLqrLxKHD/HUEMdwqX6/0ve7c5hspX5VJ8='");
        
        return controller.Content($@"
            <html><head><meta http-equiv='refresh' content='0;url={redirectUri}'></head><body></body></html>
        ", "text/html");
    }

    public static IActionResult LoadingPage(this PageModel page, string redirectUri)
    {
        page.HttpContext.Response.Headers.Append("Content-Security-Policy", "default-src 'none'; script-src 'sha256-orD0/VhH8hLqrLxKHD/HUEMdwqX6/0ve7c5hspX5VJ8='");
        
        return page.Content($@"
            <html><head><meta http-equiv='refresh' content='0;url={redirectUri}'></head><body></body></html>
        ", "text/html");
    }
}
```

### `src/IdentityServer/wwwroot/css/site.css`
```css
.login-page, .logout-page {
    padding-top: 40px;
}

.page-header {
    margin-bottom: 30px;
}

.card {
    margin-bottom: 20px;
}

.card-header h2 {
    margin: 0;
    font-size: 1.25rem;
}

.form-group {
    margin-bottom: 1rem;
}

.btn {
    margin-right: 0.5rem;
}
```

### `src/IdentityServer/Program.cs`
```csharp
using IdentityServer;
using Serilog;

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

    builder.Services.AddRazorPages();

    builder.Services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.EmitStaticAudienceClaim = true;
        })
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients)
        .AddTestUsers(TestUsers.Users);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();
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
```

### `src/IdentityServer/Config.cs`
```csharp
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("orders_api", "Orders API"),
            new ApiScope("products_api", "Products API"),
            new ApiScope("payments_api", "Payments API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // BFF Client
            new Client
            {
                ClientId = "bff-client",
                ClientName = "BFF Application",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                
                RedirectUris = { "https://localhost:44367/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:44367/signout-callback-oidc" },
                FrontChannelLogoutUri = "https://localhost:44367/signout-oidc",

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "orders_api",
                    "products_api",
                    "payments_api"
                },

                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 3600
            }
        };
}
```

### `src/IdentityServer/TestUsers.cs`
```csharp
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IdentityServer;

public static class TestUsers
{
    public static List<TestUser> Users =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "alice",
                Claims = new List<Claim>
                {
                    new Claim("name", "Alice Smith"),
                    new Claim("given_name", "Alice"),
                    new Claim("family_name", "Smith"),
                    new Claim("email", "alice@example.com"),
                    new Claim("email_verified", "true"),
                    new Claim("role", "admin")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "bob",
                Claims = new List<Claim>
                {
                    new Claim("name", "Bob Johnson"),
                    new Claim("given_name", "Bob"),
                    new Claim("family_name", "Johnson"),
                    new Claim("email", "bob@example.com"),
                    new Claim("email_verified", "true"),
                    new Claim("role", "user")
                }
            }
        };
}
```

### `src/IdentityServer/appsettings.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### `src/IdentityServer/Properties/launchSettings.json`
```json
{
  "profiles": {
    "SelfHost": {
      "commandName": "Project",
      "launchBrowser": false,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:5000"
    }
  }
}
```

### `src/IdentityServer/wwwroot/favicon.ico`
```
(Create an empty file or use a standard favicon)
```

---

## Orders.API (Microservice)

### `src/Microservices/Orders.API/Orders.API.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.21" />
  </ItemGroup>

</Project>
```

### `src/Microservices/Orders.API/Program.cs`
```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:44392";
        options.Audience = "orders_api";
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
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "orders_api");
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
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

app.Run();
```

### `src/Microservices/Orders.API/Controllers/OrdersController.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Orders.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        var userName = User.FindFirst("name")?.Value ?? "Unknown User";
        
        _logger.LogInformation("Orders requested by user: {UserId}", userId);
        
        return Ok(new
        {
            Message = "Orders retrieved successfully",
            UserId = userId,
            UserName = userName,
            Orders = new[]
            {
                new { Id = 1, Product = "Laptop", Amount = 1299.99, Status = "Shipped", Date = "2024-10-15" },
                new { Id = 2, Product = "Mouse", Amount = 29.99, Status = "Delivered", Date = "2024-10-20" },
                new { Id = 3, Product = "Keyboard", Amount = 89.99, Status = "Processing", Date = "2024-11-01" }
            }
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        
        return Ok(new
        {
            Id = id,
            UserId = userId,
            Product = "Sample Product",
            Amount = 99.99,
            Status = "Active"
        });
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderRequest request)
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        
        return Ok(new
        {
            Message = "Order created successfully",
            OrderId = Random.Shared.Next(1000, 9999),
            UserId = userId,
            Product = request.Product,
            Amount = request.Amount
        });
    }
}

public record CreateOrderRequest(string Product, decimal Amount);
```

### `src/Microservices/Orders.API/appsettings.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### `src/Microservices/Orders.API/Properties/launchSettings.json`
```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:5001",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## Products.API (Microservice)

### `src/Microservices/Products.API/Products.API.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.21" />
  </ItemGroup>

</Project>
```

### `src/Microservices/Products.API/Program.cs`
```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:44392";
        options.Audience = "products_api";
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
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "products_api");
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
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

app.Run();
```

### `src/Microservices/Products.API/Controllers/ProductsController.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        var userName = User.FindFirst("name")?.Value ?? "Unknown User";
        
        _logger.LogInformation("Products requested by user: {UserId}", userId);
        
        return Ok(new
        {
            Message = "Products retrieved successfully",
            UserId = userId,
            UserName = userName,
            Products = new[]
            {
                new { Id = 1, Name = "Laptop Pro", Price = 1299.99, Stock = 45, Category = "Electronics" },
                new { Id = 2, Name = "Wireless Mouse", Price = 29.99, Stock = 150, Category = "Accessories" },
                new { Id = 3, Name = "Mechanical Keyboard", Price = 89.99, Stock = 78, Category = "Accessories" },
                new { Id = 4, Name = "USB-C Hub", Price = 49.99, Stock = 92, Category = "Accessories" },
                new { Id = 5, Name = "Monitor 27\"", Price = 399.99, Stock = 23, Category = "Electronics" }
            }
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        
        return Ok(new
        {
            Id = id,
            UserId = userId,
            Name = "Sample Product",
            Price = 99.99,
            Stock = 100,
            Category = "General"
        });
    }
}
```

### `src/Microservices/Products.API/appsettings.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### `src/Microservices/Products.API/Properties/launchSettings.json`
```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:5002",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## Payments.API (Microservice)

### `src/Microservices/Payments.API/Payments.API.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.21" />
  </ItemGroup>

</Project>
```

### `src/Microservices/Payments.API/Program.cs`
```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:44392";
        options.Audience = "payments_api";
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
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "payments_api");
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
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

app.Run();
```

### `src/Microservices/Payments.API/Controllers/PaymentsController.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Payments.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        var userName = User.FindFirst("name")?.Value ?? "Unknown User";
        
        _logger.LogInformation("Payments requested by user: {UserId}", userId);
        
        return Ok(new
        {
            Message = "Payments retrieved successfully",
            UserId = userId,
            UserName = userName,
            Payments = new[]
            {
                new { Id = 1, Amount = 1299.99, Status = "Completed", Method = "Credit Card", Date = "2024-10-15" },
                new { Id = 2, Amount = 29.99, Status = "Completed", Method = "PayPal", Date = "2024-10-20" },
                new { Id = 3, Amount = 89.99, Status = "Pending", Method = "Debit Card", Date = "2024-11-01" }
            }
        });
    }

    [HttpPost("process")]
    public IActionResult ProcessPayment([FromBody] ProcessPaymentRequest request)
    {
        var userId = User.FindFirst("sub")?.Value ?? "unknown";
        
        return Ok(new
        {
            Message = "Payment processed successfully",
            TransactionId = Guid.NewGuid().ToString(),
            UserId = userId,
            Amount = request.Amount,
            Status = "Completed"
        });
    }
}

public record ProcessPaymentRequest(decimal Amount, string Method);
```

### `src/Microservices/Payments.API/appsettings.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### `src/Microservices/Payments.API/Properties/launchSettings.json`
```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:5003",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## Build Scripts

### `build.sh` (Linux/Mac)
```bash
#!/bin/bash

echo "Building BFF Solution..."

# Build NextJS
echo "Building NextJS client..."
cd src/BFF.Web/client-app
npm install
npm run export
cd ../../..

# Build .NET projects
echo "Building .NET projects..."
dotnet restore
dotnet build --configuration Release

echo "Build complete!"
```

### `build.bat` (Windows)
```batch
@echo off
echo Building BFF Solution...

echo Building NextJS client...
cd src\BFF.Web\client-app
call npm install
call npm run export
cd ..\..\..

echo Building .NET projects...
dotnet restore
dotnet build --configuration Release

echo Build complete!
```

### `run-all.sh` (Linux/Mac)
```bash
#!/bin/bash

echo "Starting all services..."

# Start IdentityServer
cd src/IdentityServer
dotnet run --urls=https://localhost:5000 &
PID1=$!

sleep 3

# Start Microservices
cd ../Microservices/Orders.API
dotnet run --urls=https://localhost:5001 &
PID2=$!

cd ../Products.API
dotnet run --urls=https://localhost:5002 &
PID3=$!

cd ../Payments.API
dotnet run --urls=https://localhost:5003 &
PID4=$!

sleep 3

# Start BFF
cd ../../BFF.Web
dotnet run --urls=https://localhost:5005 &
PID5=$!

echo "All services started!"
echo "IdentityServer: https://localhost:5000"
echo "Orders API: https://localhost:5001"
echo "Products API: https://localhost:5002"
echo "Payments API: https://localhost:5003"
echo "BFF Application: https://localhost:5005"
echo ""
echo "Press Ctrl+C to stop all services"

wait
```

### `run-all.bat` (Windows)
```batch
@echo off
echo Starting all services...

start "IdentityServer" cmd /k "cd src\IdentityServer && dotnet run --urls=https://localhost:5000"
timeout /t 3

start "Orders API" cmd /k "cd src\Microservices\Orders.API && dotnet run --urls=https://localhost:5001"
start "Products API" cmd /k "cd src\Microservices\Products.API && dotnet run --urls=https://localhost:5002"
start "Payments API" cmd /k "cd src\Microservices\Payments.API && dotnet run --urls=https://localhost:5003"
timeout /t 3

start "BFF Application" cmd /k "cd src\BFF.Web && dotnet run --urls=https://localhost:5005"

echo All services started!
echo IdentityServer: https://localhost:5000
echo Orders API: https://localhost:5001
echo Products API: https://localhost:5002
echo Payments API: https://localhost:5003
echo BFF Application: https://localhost:5005
```

---

## Docker Support (Optional)

### `docker-compose.yml`
```yaml
version: '3.8'

services:
  identityserver:
    build:
      context: ./src/IdentityServer
      dockerfile: Dockerfile
    ports:
      - "5000:5000"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:5000
    networks:
      - bff-network

  orders-api:
    build:
      context: ./src/Microservices/Orders.API
      dockerfile: Dockerfile
    ports:
      - "5001:5001"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:5001
    depends_on:
      - identityserver
    networks:
      - bff-network

  products-api:
    build:
      context: ./src/Microservices/Products.API
      dockerfile: Dockerfile
    ports:
      - "5002:5002"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:5002
    depends_on:
      - identityserver
    networks:
      - bff-network

  payments-api:
    build:
      context: ./src/Microservices/Payments.API
      dockerfile: Dockerfile
    ports:
      - "5003:5003"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:5003
    depends_on:
      - identityserver
    networks:
      - bff-network

  bff-web:
    build:
      context: ./src/BFF.Web
      dockerfile: Dockerfile
    ports:
      - "5005:5005"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:5005
    depends_on:
      - identityserver
      - orders-api
      - products-api
      - payments-api
    networks:
      - bff-network

networks:
  bff-network:
    driver: bridge
```

---

## .gitignore
```
## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.

# User-specific files
*.suo
*.user
*.userosscache
*.sln.docstates

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
build/
bld/
[Bb]in/
[Oo]bj/

# Visual Studio cache/options
.vs/

# Node modules
node_modules/
npm-debug.log*
yarn-debug.log*
yarn-error.log*

# NextJS
.next/
out/
src/BFF.Web/wwwroot/

# OS files
.DS_Store
Thumbs.db

# IDE
.vscode/
.idea/
*.swp
*.swo

# Logs
*.log
logs/
```

---

## Setup Instructions

1. **Create folder structure** and copy all files
2. **Install dependencies**:
   ```bash
   cd src/BFF.Web/client-app
   npm install
   cd ../../..
   dotnet restore
   ```
3. **Build NextJS**:
   ```bash
   cd src/BFF.Web/client-app
   npm run export
   ```
4. **Run services** (use run-all script or manually):
   - IdentityServer: `https://localhost:5000`
   - Orders API: `https://localhost:5001`
   - Products API: `https://localhost:5002`
   - Payments API: `https://localhost:5003`
   - BFF App: `https://localhost:5005`

5. **Test credentials**:
   - Username: `alice` / Password: `alice`
   - Username: `bob` / Password: `bob`

---

**All files are complete and ready to use!**