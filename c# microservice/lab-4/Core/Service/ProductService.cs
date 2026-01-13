using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Models;

namespace Itmo.CSharpMicroservices.Lab4.Core.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        return await _productRepository.CreateProductAsync(product, cancellationToken);
    }

    public async Task<Product?> FindProductById(long id, CancellationToken cancellationToken = default)
    {
        return await _productRepository.FindProductById(id, cancellationToken);
    }
}
