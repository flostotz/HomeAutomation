using HomeAutomation.ApplicationTier.Entity;

namespace HomeAutomation.ApplicationTier.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add CORS policy to allow external accesses
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCORS(this IServiceCollection services)
        {
            return // CORS
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder.WithOrigins(AppSettings.CORS)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
                });
        }
    }
}