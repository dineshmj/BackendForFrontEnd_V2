using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FW.Microservices.Products.API.DBAccess;

namespace FW.Microservices.Products.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController
    : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> Get()
    {
        var result = _productRepository.GetAllProductsAsync().Result;
        return Ok (result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var matchingProduct = _productRepository.GetProductByIdAsync(id);

        if (matchingProduct == null)
        {
            return NotFound();
        }

        return Ok(matchingProduct);
    }
}