using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        ProductDto product = await _productService.CreateProductAsync(productDto, cancellationToken);

        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(long id, CancellationToken cancellationToken)
    {
        ProductDto? product = await _productService.FindProductById(id, cancellationToken);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}