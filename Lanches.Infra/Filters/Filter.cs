using System.Linq.Expressions;

namespace Lanches.API.Extensions
{
    public static class Filter
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (!condition)
            {
                return source.Where(predicate);
            }
            return source;
        }
    }
}
