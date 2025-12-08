using System.Security.Claims;

using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using FW.IDP.DBAccess;

namespace FW.IDP.Services;

public sealed class CustomProfileService
    : IProfileService
{
    private readonly IUserRepository _userRepository;

    public CustomProfileService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userRepository.FindBySubjectIdAsync(sub);

        if (user == null)
        {
            context.IssuedClaims = [];
            return;
        }

        var claims = new List<Claim>
        {
            new(JwtClaimTypes.Subject, user.ID.ToString()),
            new(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(JwtClaimTypes.GivenName, user.FirstName),
            new(JwtClaimTypes.FamilyName, user.LastName),
            new(JwtClaimTypes.Email, user.Email),
            new(JwtClaimTypes.EmailVerified, "true")
        };

        if (context.RequestedClaimTypes.Contains(JwtClaimTypes.Role))
		    // 🡡__ WHY   : Roles must only be added when explicitly requested by the client,
		    //             which prevents unnecessary claim bloat and keeps tokens smaller and
		    //             more secure. It also aligns with IdentityServer’s principle that
		    //             only requested scopes/claims should be emitted.
		    // 🡡__ IF NOT: If roles are always included even when not requested, every token
		    //             will carry role information unnecessarily, increasing token size,
		    //             leaking role metadata to clients that did not ask for it, and
		    //             potentially violating least-privilege design or privacy rules.
		{
			var roles = await _userRepository.GetRolesByUserIDAsync(user.ID);

            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }
        }

        context.IssuedClaims = claims;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
		// Here you can implement custom logic to determine if the user is active (by checking if the user exists, is not locked out, etc.)
		context.IsActive = !string.IsNullOrWhiteSpace(context.Subject.GetSubjectId());
        return Task.CompletedTask;
    }
}