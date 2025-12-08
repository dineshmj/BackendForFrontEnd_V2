using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FW.Landscape.Common;
using FW.Microservices.Products.BFFWeb.Entities;

namespace FW.Microservices.Products.BFFWeb.Controllers;

[ApiController]
[Route ("bff/products")]
[Authorize]
public sealed class ProductsController
	: ControllerBase
{
	private readonly IHttpClientFactory _httpClientFactory;

    public ProductsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
	public async Task<IActionResult> GetProducts ()
	{
        try
        {
            var accessToken = HttpContext.Session.GetString ("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }

            // Set the Authorization header with the Bearer token
            var client = _httpClientFactory.CreateClient(MicroserviceApiResources.PRODUCTS_API);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Call the protected Products API
            var productsFromMicroservice = await client.GetFromJsonAsync<List<Product>>("/api/products/get-all");

            var productDetails
                = productsFromMicroservice
                    .Select
                    (
                        p => new
                        {
                            p.Id,
                            p.Name,
                            p.Price,
                            CategoryName = p.Category.Name,
                            p.Inventory?.StockQuantity
                        }
                    )
                    .ToArray();

            return Ok(new
            {
                Message = "Products retrieved successfully.",
                Products = productDetails
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the products.");
        }
	}
}