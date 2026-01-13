using Itmo.CSharpMicroservices.Lab4.Core.Models;

namespace Itmo.CSharpMicroservices.Lab4.Core.Abstractions;

public interface IProductService
{
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);

    Task<Product?> FindProductById(long id, CancellationToken cancellationToken = default);
}
