using HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAutomation.ApplicationTier.BusinessLogic
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceTypeService, DeviceTypeService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IDeviceValueHistoryService, DeviceValueHistoryService>();
            return services;
        }
    }
}
