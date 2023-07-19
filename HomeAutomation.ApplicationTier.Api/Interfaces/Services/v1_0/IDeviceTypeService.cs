using HomeAutomation.ApplicationTier.Api.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IDeviceTypeService
    {
        /// <summary>
        /// Get all items of Device table
        /// </summary>
        /// <returns></returns>
        Task<IList<DeviceTypeDto>> GetAll();
        Task<DeviceTypeDto> GetOne(Guid deviceTypeId);
        Task Update(DeviceType deviceType);
        Task Add(DeviceType deviceType);
        Task Delete(Guid deviceTypeId);
    }
}