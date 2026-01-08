using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

using FW.IDP.DBAccess;
using FW.IDP.Security;

namespace FW.IDP.Services;

public sealed class CustomResourceOwnerPasswordValidator
    : IResourceOwnerPasswordValidator
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;

    public CustomResourceOwnerPasswordValidator(IUserRepository userRepository, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userRepository.FindByUsernameAsync(context.UserName);

        if (user != null)
        {
            if (_passwordManager.VerifyPassword(user, user.HashedPassword, context.Password))
            {
                context.Result = new GrantValidationResult(
                    subject: user.ID.ToString(),
                    authenticationMethod: OidcConstants.AuthenticationMethods.Password
                );
                return;
            }
        }

        context.Result = new GrantValidationResult(
            TokenRequestErrors.InvalidGrant,
            "Invalid credentials. Please check your username and password."
        );
    }
}