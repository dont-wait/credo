using CreditScoringAPI.Data;
using CreditScoringAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CreditScoringAPI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Services
            services.AddScoped<ICreditService, CreditService>();

            return services;
        }
    }
}