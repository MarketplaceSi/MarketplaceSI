using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketplaceSI.Core.Kernel.Configuration;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {

        
        return services;
    }
}