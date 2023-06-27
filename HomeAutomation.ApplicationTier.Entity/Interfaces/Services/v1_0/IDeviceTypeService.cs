using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IDeviceTypeService
    {
        /// <summary>
        /// Get all items of Device table
        /// </summary>
        /// <returns></returns>
        Task<IList<DeviceType>> GetAll();
        Task<DeviceType> GetOne(int deviceTypeId);
        Task Update(DeviceType deviceType);
        Task Add(DeviceType deviceType);
        Task Delete(int deviceTypeId);
    }
}