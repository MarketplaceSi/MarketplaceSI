using Kernel.Extensions;
using MarketplaceSI.Web.Api.Graphql.Errors;
using MarketplaceSI.Web.Api.Graphql.Queries;
using AppAny.HotChocolate.FluentValidation;
using Microsoft.Extensions.Options;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Graphql.Mutations;
using Microsoft.Extensions.DependencyInjection;
using MarketplaceSI.Web.Api.Graphql.Filters;

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
                .BindRuntimeType<Guid, IdType>()
                .BindRuntimeType<DateTime, DateType>()
                .BindRuntimeType<decimal, DecimalType>()
                .AddTypeConverter<DateTimeOffset, DateTime>(t => t.UtcDateTime)
                .AddTypeConverter<DateTime, DateTimeOffset>(
                        t => t.Kind is DateTimeKind.Unspecified
                            ? DateTime.SpecifyKind(t, DateTimeKind.Utc)
                            : t
                    )
                .AddAuthorization()
                    //.AddType<UserType>()
                    //.AddType<ProductType>()
                    //.AddType<ProductReviewType>()
                .AddQueryType(q => q.Name(OperationTypeNames.Query))
                    .AddTypeExtension<UserQueries>()
                //    .AddTypeExtension<CategoryQueries>()
                //    .AddTypeExtension<ProductQueries>()
                //    .AddTypeExtension<FavoriteQueries>()
                //    .AddTypeExtension<ReviewQueries>()
                //    .AddType<ProductCreateCommandType>()
                .AddFiltering<CustomFilteringConvention>()
                .AddConvention<IFilterConvention>(
                        new FilterConventionExtension(
                            x => x.AddProviderExtension(
                                new QueryableFilterProviderExtension(
                                    y => y.AddFieldHandler<QueryableStringInvariantContainsHandler>()))))
                .AddSorting()
                .AddMutationType(q => q.Name(OperationTypeNames.Mutation))
                    .AddTypeExtension<AccountMutations>()
                //    .AddTypeExtension<CategoryMutations>()
                //    .AddTypeExtension<ProductMutations>()
                //    .AddTypeExtension<FavoriteMutations>()
                //    .AddTypeExtension<UserMutations>()
                //    .AddTypeExtension<ReviewMutations>()
                //    .AddTypeExtension<AddressMutation>()

                //.AddSubscriptionType<UserSubscription>()
                .AddDataLoaders()
                .AddInMemorySubscriptions()
                .AddErrorFilter<GraphQLErrorFilter>(c => {
                    var context = c.GetRequiredService<IHttpContextAccessor>();
                    var opt = c.GetRequiredService<IOptions<ExceptionSettings>>();
                    return new GraphQLErrorFilter(opt, context);
                })
                .AddProjections()
                .InitializeOnStartup();
            return services;
        }
    }
}
