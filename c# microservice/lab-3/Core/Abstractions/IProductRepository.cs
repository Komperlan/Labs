using Itmo.CSharpMicroservices.Lab3.Core.Models;

namespace Itmo.CSharpMicroservices.Lab3.Core.Abstractions;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);

    Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default);

    Task DeleteProductAsync(Product product, CancellationToken cancellationToken = default);

    Task<IList<Product>> GetProductsAsync(long cursor, int pageSize, long[]? ids, decimal? minPrice, decimal? maxPrice, string? nameSubstring, CancellationToken cancellationToken = default);

    Task<Product?> FindProductById(long id, CancellationToken cancellationToken = default);
}