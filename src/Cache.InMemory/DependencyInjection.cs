using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.InMemory;

public static class DependencyInjection
{
    public static void AddCacheInMemory(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {

    }
}
