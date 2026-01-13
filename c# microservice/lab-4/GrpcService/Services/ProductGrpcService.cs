using Grpc.Core;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Itmo.CSharpMicroservices.Lab4.Core.Service;
using Itmo.CSharpMicroservices.Lab4.Grpc;
using Itmo.CSharpMicroservices.Lab4.GrpcService.Mappers;

namespace Itmo.CSharpMicroservices.Lab4.GrpcService.Services;

public class ProductGrpcService : GrpcProductService.GrpcProductServiceBase
{
    private readonly ProductService _productService;

    public ProductGrpcService(ProductService productService)
    {
        _productService = productService;
    }

    public override async Task<ProductResponse> CreateProduct(
        CreateProductRequest request,
        ServerCallContext context)
    {
        var product = new Product(1, request.Name, ProductMapper.ToDecimal(request.Price), false);

        product = await _productService.CreateProductAsync(product, context.CancellationToken);

        return ProductMapper.ToProductResponse(product);
    }

    public override async Task<ProductResponse> GetProduct(
        GetProductRequest request,
        ServerCallContext context)
    {
        Product? product = await _productService.FindProductById(request.ProductId, context.CancellationToken);

        return ProductMapper.ToProductResponse(product);
    }
}