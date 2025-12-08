using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FW.Microservices.Products.API.DBAccess;

namespace FW.Microservices.Products.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController
    : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;

    public CategoriesController(ILogger<ProductsController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> Get()
    {
        return Ok (_productRepository.GetAllCategoriesAsync().Result);
    }
}