using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheRush.WebApp.Domain;
using TheRush.WebApp.Infrastructure.Filters;
using TheRush.WebApp.Infrastructure.Pagination;

namespace TheRush.WebApp.Filters
{
    public class PlayerRushingStatsFilter : IFilter<PlayerRushingStats>
    {
        public string SearchByPlayer { get; set; }
        public int Page { get; set; } = PaginationOptions.DefaultPage;
        public int PageSize { get; set; } = PaginationOptions.DefaultPageSize;
        public string Order { get; set; }

        public IQueryable<PlayerRushingStats> Filter(IQueryable<PlayerRushingStats> query)
        {
            return query.Where(x =>
                string.IsNullOrEmpty(SearchByPlayer) ||
                EF.Functions.Like(x.Player, $"%{SearchByPlayer}%"));
        }
    }
}