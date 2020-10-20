using System.Linq;

namespace TheRush.WebApp.Infrastructure.Filters
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, IFilter<T> filter)
        {
            return filter.Filter(queryable);
        }
    }
}