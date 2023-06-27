using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IDeviceValueHistoryService
    {
        /// <summary>
        /// Get all items of DeviceValueHistory table
        /// </summary>
        /// <returns></returns>
        Task<IList<DeviceValueHistory>> GetAll();
        Task<DeviceValueHistory> GetOne(int deviceValueHistoryId);
        Task Update(DeviceValueHistory deviceValueHistory);
        Task Add(DeviceValueHistory deviceValueHistory);
        Task Delete(int deviceValueHistoryId);
    }
}