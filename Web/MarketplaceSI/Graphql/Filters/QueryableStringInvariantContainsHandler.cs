using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace MarketplaceSI.Web.Api.Graphql.Filters;

public class QueryableStringInvariantContainsHandler : QueryableStringOperationHandler
{
    public QueryableStringInvariantContainsHandler(InputParser inputParser) : base(inputParser)
    {
    }
    private static readonly MethodInfo _toUpper = typeof(string).GetMethod("ToUpper", new Type[] { }) ?? throw new NullReferenceException();
    private static readonly MethodInfo _contains = typeof(string).GetMethod("Contains", new[] { typeof(string)}) ?? throw new NullReferenceException();

    protected override int Operation => DefaultFilterOperations.Contains;
    public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object parsedValue)
    {
        Expression property = context.GetInstance();
        if (parsedValue is string str)
        {
            return Expression.NotEqual(
                    Expression.Call(
                        Expression.Call(property, _toUpper),
                        _contains,
                        Expression.Call(Expression.Constant(str, typeof(string)), _toUpper)
                    ),
                    Expression.Constant(false, typeof(bool))
                );
        }
        throw new InvalidOperationException();
    }
}