using HomeAutomation.ApplicationTier.Entity;
using HomeAutomation.ApplicationTier.Entity.Context;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAutomation.ApplicationTier.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime  
            services.AddDbContext<HomeAutomationDbContext>(options =>
            {
                options.UseSqlServer(AppSettings.ConnectionString,
                    sqlOptions => sqlOptions.CommandTimeout(120));
            }
              );

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
