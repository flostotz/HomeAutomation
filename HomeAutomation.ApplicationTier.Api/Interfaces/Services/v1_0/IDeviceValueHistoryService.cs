using HomeAutomation.ApplicationTier.Api.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IDeviceValueHistoryService
    {
        /// <summary>
        /// Get all items of DeviceValueHistory table
        /// </summary>
        /// <returns></returns>
        Task<IList<DeviceValueHistoryDto>> GetAll();
        Task<DeviceValueHistoryDto> GetOne(Guid deviceValueHistoryId);
        Task Update(DeviceValueHistory deviceValueHistory);
        Task Add(DeviceValueHistory deviceValueHistory);
        Task Delete(Guid deviceValueHistoryId);
    }
}