using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TheRush.WebApp.Infrastructure.Database;

namespace TheRush.WebApp.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            return services.AddDbContext<TheRushDatabaseContext>()
                .AddScoped<SeedTheRushDatabase>();
        }
    }
}