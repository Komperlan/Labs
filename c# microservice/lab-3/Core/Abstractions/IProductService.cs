using Itmo.CSharpMicroservices.Lab3.Core.Models;

namespace Itmo.CSharpMicroservices.Lab3.Core.Abstractions;

public interface IProductService
{
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);

    Task<Product?> FindProductById(long id, CancellationToken cancellationToken = default);
}
