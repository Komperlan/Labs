namespace Itmo.CSharpMicroservices.Lab2.Task1;

public interface IGetAllAble
{
    IAsyncEnumerable<ConfigurationItemDto> GetAll(CancellationToken cancellationToken);
}
