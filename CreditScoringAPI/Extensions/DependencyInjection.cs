using CreditScoringAPI.Data;
using CreditScoringAPI.Models.Enums;
using CreditScoringAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CreditScoringAPI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Cấu hình NpgsqlDataSource cho Native Enums
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            
            dataSourceBuilder.MapEnum<ContractType>("contract_type");
            dataSourceBuilder.MapEnum<GenderType>("gender_type");
            dataSourceBuilder.MapEnum<SuiteType>("suite_type");
            dataSourceBuilder.MapEnum<IncomeType>("income_type");
            dataSourceBuilder.MapEnum<EducationType>("education_type");
            dataSourceBuilder.MapEnum<FamilyStatus>("family_status");
            dataSourceBuilder.MapEnum<HousingType>("housing_type");
            dataSourceBuilder.MapEnum<OccupationType>("occupation_type");
            dataSourceBuilder.MapEnum<OrganizationType>("organization_type");
            dataSourceBuilder.MapEnum<WallsMaterial>("wallsmaterial");
            dataSourceBuilder.MapEnum<HouseTypeMode>("housetype_mode");
            dataSourceBuilder.MapEnum<FondKapremont>("fondkapremont");
            dataSourceBuilder.MapEnum<EmergencyState>("emergency_state");
            dataSourceBuilder.MapEnum<ContractStatus>("contract_status");
            dataSourceBuilder.MapEnum<PredictionResult>("prediction_result");

            var dataSource = dataSourceBuilder.Build();

            // PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(dataSource)
            );

            // Services
            services.AddScoped<ICreditService, CreditService>();

            return services;
        }
    }
}