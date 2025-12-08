using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Duende.IdentityServer.Models;

namespace FW.IDP;

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