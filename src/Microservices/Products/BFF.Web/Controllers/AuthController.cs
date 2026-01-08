using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FW.Common.WebUtilties.Helpers;

namespace FW.Microservices.Products.BFFWeb.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public sealed class AuthController
        : Controller
    {
        [HttpGet("silent-login")]
        public IActionResult SilentLogin([FromQuery] string returnUrl = "/")
        {
            if (returnUrl.DoesNextJsRelativePathExist () == false)
            {
				var htmlContent =  @"
                    <div style='padding:20px; border:1px solid #d0d8e8; background:#f7faff; color:#1a2c4e; border-radius:8px; font-family:Segoe UI,Roboto,sans-serif; max-width:500px;'>
                        <div style='display:flex; align-items:center; font-size:18px; font-weight:600; margin-bottom:10px;'>
                            <span style='font-size:22px; margin-right:8px;'>&#x2757;</span>
                            Microservice Task Not Implemented
                        </div>
                        <div style='font-size:14px; line-height:1.6; color:#2d3e5e;'>
                            This microservice task has not been implemented yet. Please check back later or contact the service owner.
                        </div>
                    </div>";

				return new ContentResult
				{
					Content = htmlContent,
					ContentType = "text/html",
					StatusCode = StatusCodes.Status404NotFound
				};
			}

			if (User?.Identity?.IsAuthenticated == true)
			{
				return LocalRedirect (returnUrl);
			}

			var properties = new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                Items =
                {
                    { "prompt", "none" }
                }
            };

            return Challenge(properties, "oidc");
        }

        [Authorize]
        [HttpPost("silent-logout")]
        public async Task<IActionResult> SilentLogout()
        {
            try
            {
                if (User.Identity?.IsAuthenticated != true)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "User was not authenticated",
                        AlreadyLoggedOut = true
                    });
                }

				var refreshToken = await HttpContext.GetTokenAsync ("refresh_token");

                if (!string.IsNullOrEmpty (refreshToken))
                {
					// This automatically finds the refresh token in your session and 
					// sends it to the Identity Server's revocation endpoint.
					await HttpContext.RevokeRefreshTokenAsync ();
				}

				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(new
                {
                    Success = true,
                    Message = "Successfully logged out from Products Microservice BFF",
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Logout failed",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("silent-logout")]
        public async Task<IActionResult> SilentLogoutGet()
        {
            try
            {
                if (User.Identity?.IsAuthenticated != true)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "User was not authenticated",
                        AlreadyLoggedOut = true
                    });
                }

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(new
                {
                    Success = true,
                    Message = "Successfully logged out from Products Microservice BFF",
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Logout failed",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("status")]
        [AllowAnonymous]
        public IActionResult GetAuthStatus()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                UserName = User.Identity?.Name,
                Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
            });
        }
    }
}