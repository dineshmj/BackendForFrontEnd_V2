using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;

using FW.IDP.DBAccess;

namespace FW.IDP.Pages.Account;

[SecurityHeaders]
[AllowAnonymous]
public sealed class LoginModel
    : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IUserRepository _userRepository;

    public LoginModel(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IUserRepository userRepository,
        IEventService events,
        TestUserStore? users = null)
    {
        _interaction = interaction;
        _schemeProvider = schemeProvider;
        _userRepository = userRepository;
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
            if (_userRepository.ValidateCredentialsAsync (Input.Username, Input.Password).Result)
            {
                var user = _userRepository.FindByUsernameAsync(Input.Username).Result;

                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, $"{user.ID}", $"{user.FirstName} {user.LastName}"));
				// 🡡__ WHY   : 
				// 🡡__ IF NOT: 
				    // 🡡__ WHY   : This event notifies IdentityServer's event pipeline that a login succeeded,
				    //             enabling auditing, diagnostics, monitoring hooks, and security log tracking.
				    // 🡡__ IF NOT: Successful logins will not appear in audit/event logs, reducing traceability
				    //             and making security incident investigations or login failure analysis harder.


				AuthenticationProperties? props = null;

                if (Input.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                    };
                }

                var isuser = new IdentityServerUser($"{user.ID}")
                {
                    DisplayName = $"{user.FirstName} {user.LastName}"
                };

                await HttpContext.SignInAsync(isuser, props);
				    // 🡡__ WHY   : This issues the authentication cookie for the user, creating their local login
				    //             session inside IdentityServer. All subsequent authorize requests depend on this
				    //             cookie to identify the user and provide SSO/session continuity.
				    // 🡡__ IF NOT: The user will not actually be signed in, meaning the authorization flow will
				    //             immediately fail or re-prompt for login. No Single-Sign-On will work, and all
				    //             downstream OIDC flows depending on an authenticated user will break.

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