using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IDeviceService
    {
        /// <summary>
        /// Get all items of Device table
        /// </summary>
        /// <returns></returns>
        Task<IList<Device>> GetAll();
        Task<Device> GetOne(Guid deviceId);
        Task Update(Device device);
        Task Add(Device device);
        Task Delete(Guid deviceId);
    }
}