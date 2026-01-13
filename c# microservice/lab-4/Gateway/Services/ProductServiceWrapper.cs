using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Services;

public class ProductServiceWrapper : IProductService
{
    private readonly GrpcProductService.GrpcProductServiceClient _grpcClient;

    public ProductServiceWrapper(GrpcProductService.GrpcProductServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken cancellationToken = default)
    {
        var request = new CreateProductRequest { Name = product.Name, Price = ProductMapper.ToDecimalValue(product.Price) };
        ProductResponse response = await _grpcClient.CreateProductAsync(request);
        return ProductMapper.ToProductDto(response);
    }

    public async Task<ProductDto?> FindProductById(long id, CancellationToken cancellationToken = default)
    {
        var request = new GetProductRequest { ProductId = id };
        ProductResponse response = await _grpcClient.GetProductAsync(request);
        return ProductMapper.ToProductDto(response);
    }
}
