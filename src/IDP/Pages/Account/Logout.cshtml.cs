using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;

namespace FW.IDP.Pages.Account;

[SecurityHeaders]
[AllowAnonymous]
public sealed class LogoutModel
    : PageModel
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
		return await OnPost ();
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