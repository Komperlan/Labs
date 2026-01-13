using Microsoft.Extensions.Configuration;

namespace Itmo.CSharpMicroservices.Lab2.Task2;

public class CustomConfigurationProvider : ConfigurationProvider
{
    public void Set(Dictionary<string, string?> configs)
    {
        if (Data.SequenceEqual(configs))
        {
            return;
        }

        Data = configs;
        OnReload();
    }
}
