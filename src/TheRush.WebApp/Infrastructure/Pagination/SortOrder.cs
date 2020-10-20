using System;
using System.Linq;

namespace TheRush.WebApp.Infrastructure.Pagination
{
    public class SortOrder
    {
        public string Property { get; set; }
        public SortType Type { get; set; } = SortType.Ascend;

        public static SortOrder Parse(string sortOrder)
        {
            if (sortOrder == null)
                return null;

            var split = sortOrder.Trim().Split(",")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
            var property = split.FirstOrDefault();
            
            if (split.Length != 2) 
                return null;

            if (!Enum.TryParse<SortType>(split[1], true, out var type))
            {
                type = SortType.Ascend;
            }

            return new SortOrder {Property = property, Type = type};
        }

        public IQueryable<T> OrderBy<T>(IQueryable<T> query)
        {
            if (string.IsNullOrEmpty(Property))
                return query;

            return query.OrderBy(Property, Type == SortType.Ascend);
        }
    }

    public enum SortType
    {
        Descend = 0,
        Ascend = 1
    }
}