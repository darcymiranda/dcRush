using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheRush.WebApp.Infrastructure.Pagination
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedList<T>(this IQueryable<T> queryable,
            PaginationOptions pageOptions)
        {
            return new PaginatedList<T>(
                await queryable.CountAsync(),
                await queryable.Skip((pageOptions.Page - 1) * pageOptions.PageSize)
                    .Take(pageOptions.PageSize)
                    .ToListAsync(),
                pageOptions
            );
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, SortOrder sortOrder)
        {
            if (sortOrder == null)
                return query;

            return sortOrder.OrderBy(query);
        }

        internal static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, bool ascending = false)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var property = typeof(T).GetProperty(ordering,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
            {
                return source;
            }

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable),
                ascending ? "OrderBy" : "OrderByDescending",
                new[] {type, property.PropertyType}, source.Expression,
                Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}