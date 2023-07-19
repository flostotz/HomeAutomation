using HomeAutomation.ApplicationTier.Api.Dtos;
using HomeAutomation.ApplicationTier.Entity.Dtos;
using Mapster;
using System.Reflection;

namespace HomeAutomation.ApplicationTier.Api.Extensions
{
    public static class MapsterConfiguration
    {
        public static void AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
            typeAdapterConfig.Scan(applicationAssembly);
        }
    }
}
