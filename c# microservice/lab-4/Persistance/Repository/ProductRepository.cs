using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Npgsql;

namespace Itmo.CSharpMicroservices.Lab4.Persistance.Repository;

public class ProductRepository : IProductRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public ProductRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            INSERT INTO products (product_name, product_price, product_deleted)
            VALUES (@name, @price, @isDeleted)
            RETURNING product_id;
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("name", product.Name);
        cmd.Parameters.AddWithValue("price", product.Price);
        cmd.Parameters.AddWithValue("isDeleted", product.IsDeleted);

        object? result = await cmd.ExecuteScalarAsync(cancellationToken);
        ArgumentNullException.ThrowIfNull(result);

        await connection.CloseAsync();

        return new Product((long)result, product.Name, product.Price, product.IsDeleted);
    }

    public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE products
            SET is_deleted = TRUE
            WHERE product_id = @Id;";

        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("Id", product.Id);

        await connection.CloseAsync();

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<Product?> FindProductById(long id, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        string sql = @"
            SELECT product_id, product_name, product_price, product_deleted FROM products
            WHERE (product_id = @ids)
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("ids", id);

        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

        await reader.ReadAsync(cancellationToken);

        Product? product = null;

        product = new Product(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetDecimal(2),
            reader.GetBoolean(3));

        await connection.CloseAsync();

        return product;
    }

    public async Task<IList<Product>> GetProductsAsync(long cursor, int pageSize, long[]? ids, decimal? minPrice, decimal? maxPrice, string? nameSubstring, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        string sql = @"
            SELECT product_id, product_name, product_price FROM products
            WHERE ( (@ids IS NULL) OR (product_id = ANY(@ids)) )
                AND ( (@minPrice IS NULL) OR (product_price >= @minPrice) )
                AND ( (@maxPrice IS NULL) OR (product_price <= @maxPrice) )
                AND ( (@nameSubstring IS NULL) OR (product_name ILIKE '%' || @nameSubstring || '%') )
            ORDER BY product_id
            OFFSET @cursor LIMIT @limit;
        ";

        object? minPriceObject = minPrice;

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("ids", ids ?? Array.Empty<long>());
        cmd.Parameters.AddWithValue("minPrice", minPrice ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("maxPrice", maxPrice ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("cursor", cursor);
        cmd.Parameters.AddWithValue("nameSubstring", (object?)nameSubstring ?? DBNull.Value);
        cmd.Parameters.AddWithValue("limit", pageSize);

        var list = new List<Product>();
        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync())
        {
            list.Add(new Product(
                reader.GetInt64(0),
                reader.GetString(1),
                reader.GetDecimal(2),
                reader.GetBoolean(3)));
        }

        return list;
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE products
            SET product_name = @Name,
                product_price = @Price,
                product_deleted = @isDeleted
            WHERE product_id = @Id;";
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("Name", product.Name);
        cmd.Parameters.AddWithValue("Price", product.Price);
        cmd.Parameters.AddWithValue("isDeleted", product.IsDeleted);
        cmd.Parameters.AddWithValue("Id", product.Id);

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}