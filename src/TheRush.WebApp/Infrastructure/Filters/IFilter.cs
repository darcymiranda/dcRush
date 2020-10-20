using System.Linq;

namespace TheRush.WebApp.Infrastructure.Filters
{
    public interface IFilter<T>
    {
        public IQueryable<T> Filter(IQueryable<T> queryable);
    }
}