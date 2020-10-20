using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheRush.WebApp.Filters;
using TheRush.WebApp.Infrastructure.Csv;
using TheRush.WebApp.Infrastructure.Database;
using TheRush.WebApp.Infrastructure.Filters;
using TheRush.WebApp.Infrastructure.Pagination;

namespace TheRush.WebApp.Controllers
{
    [Route("/api/playerRushingStats")]
    [ApiController]
    public class PlayerRushingStatsController : Controller
    {
        private readonly TheRushDatabaseContext _dbContext;

        public PlayerRushingStatsController(TheRushDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PlayerRushingStatsFilter filter)
        {
            var query = _dbContext.PlayerRushingStats.AsNoTracking()
                .Filter(filter)
                .OrderBy(SortOrder.Parse(filter.Order));

            if (Request.Headers.TryGetValue("accept", out var contentType) && contentType == "text/csv")
            {
                return File(
                    (await query.ToCsvStream(x => !x.Name.EndsWith("Id"))).ToArray(),
                    "text/csv",
                    "playerRushingStats.csv"
                );
            }

            var paginatedResults = await query.ToPaginatedList(new PaginationOptions(filter.Page, filter.PageSize));
            return Ok(new
            {
                Total = paginatedResults.TotalCount,
                Items = paginatedResults.Results,
            });
        }
    }
}