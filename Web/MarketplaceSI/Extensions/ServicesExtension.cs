using Kernel.Extensions;
using MarketplaceSI.Core.Domain.Repositories.DataLoaders;
using MarketplaceSI.Graphql.Mutations;
using MarketplaceSI.Web.Api.Graphql.Errors;
using MarketplaceSI.Web.Api.Graphql.ObjectTypes;
using MarketplaceSI.Web.Api.Graphql.Queries;

namespace MarketplaceSI.Extensions
{
    public static class ServicesExtension
    {
        public static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                const string configurationsDirectory = "Configurations";

                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                foreach (var settings in new string[] {
                "cors",
                "exceptions",
                "exceptions.ua",
                "hashing",
                "urls",
            })
                {
                    config.AddJsonFile($"{configurationsDirectory}/{settings}.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"{configurationsDirectory}/{settings}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                }

                config.AddEnvironmentVariables();
            });

            return host;
        }
        public static IServiceCollection ConfigureGraphQl(this IServiceCollection services)
        {
            services.AddTransient<IErrorFilter, GraphQLErrorFilter>();

            services
                .AddGraphQLServer()
                .AddAuthorization()
                    .AddType<UserType>()
                .AddQueryType(q => q.Name(OperationTypeNames.Query))
                    .AddTypeExtension<UserQueries>()
                .AddMutationType(m => m.Name(OperationTypeNames.Mutation))
                .AddType<AccountMutations>()
                .AddDataLoader<IUserByIdDataLoader, UserByIdDataLoader>()
                .InitializeOnStartup();

            return services;
        }
    }
}
