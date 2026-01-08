using FW.Landscape.Common;
using FW.Microservices.Products.BFFWeb.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace FW.Microservices.Products.BFFWeb.Controllers;

[ApiController]
[Route ("bff/categories")]
[Authorize]
public sealed class CategoriesController
	: ControllerBase
{
	private readonly IHttpClientFactory _httpClientFactory;

    public CategoriesController (IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
	public async Task<IActionResult> GetCategories ()
	{
        try
        {
			var accessToken = await HttpContext.GetTokenAsync ("access_token");

			if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }

            // Set the Authorization header with the Bearer token
            var client = _httpClientFactory.CreateClient(MicroserviceApiResources.PRODUCTS_API);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Call the protected Products API
            var categoriesFromMicroservice = await client.GetFromJsonAsync<List<Category>>("/api/categories/get-all");

            var categoriesDetails
                = categoriesFromMicroservice
					.Select
                    (
                        p => new
                        {
                            p.Id,
                            p.Name
                        }
                    )
                    .ToArray();

            return Ok(new
            {
                Message = "Categories retrieved successfully.",
                Categories = categoriesDetails
			});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the categories.");
        }
	}
}