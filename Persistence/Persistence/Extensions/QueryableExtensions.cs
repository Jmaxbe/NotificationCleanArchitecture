using System.Linq.Expressions;

namespace Persistence.Persistence.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(IQueryable<T> queryable, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? queryable.Where(predicate) : queryable;
    }
}