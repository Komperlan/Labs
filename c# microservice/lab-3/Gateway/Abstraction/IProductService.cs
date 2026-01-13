using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken cancellationToken = default);

    Task<ProductDto?> FindProductById(long id, CancellationToken cancellationToken = default);
}
