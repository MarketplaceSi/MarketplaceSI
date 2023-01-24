using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;

namespace MarketplaceSI.Web.Api.Graphql.Filters;

public class CustomFilteringConvention : FilterConvention
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.AddDefaults();
        descriptor.Provider(
            new QueryableFilterProvider(
                x => x
                    .AddDefaultFieldHandlers()
                    .AddFieldHandler<QueryableStringInvariantContainsHandler>()));
    }
}