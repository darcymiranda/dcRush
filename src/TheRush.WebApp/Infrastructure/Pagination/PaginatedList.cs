using System.Collections;
using System.Collections.Generic;

namespace TheRush.WebApp.Infrastructure.Pagination
{
    public class PaginatedList<T> : IEnumerable<T>
    {
        public PaginatedList(long totalCount, IList<T> results, PaginationOptions pageOptions)
        {
            TotalCount = totalCount;
            Results = results;
            PageOptions = pageOptions;
        }

        public PaginationOptions PageOptions { get; }
        public IList<T> Results { get; }
        public long TotalCount { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return Results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}