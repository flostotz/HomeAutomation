using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;

namespace HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0
{
    public class DeviceTypeService : IDeviceTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<DeviceType>> GetAll()
        {
            return await _unitOfWork.Repository<DeviceType>().GetAllAsync();
        }

        public async Task<DeviceType> GetOne(int deviceTypeId)
        {
            return await _unitOfWork.Repository<DeviceType>().FindAsync(deviceTypeId);
        }

        public async Task Update(DeviceType deviceTypeInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceTypeRepos = _unitOfWork.Repository<DeviceType>();
                var deviceType = await deviceTypeRepos.FindAsync(deviceTypeInput.Id) ?? throw new KeyNotFoundException();
                deviceType.Id = deviceType.Id;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(DeviceType deviceTypeInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceTypeRepos = _unitOfWork.Repository<DeviceType>();
                await deviceTypeRepos.InsertAsync(deviceTypeInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int deviceTypeId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceTypeRepos = _unitOfWork.Repository<DeviceType>();
                var deviceType = await deviceTypeRepos.FindAsync(deviceTypeId) ?? throw new KeyNotFoundException();
                await deviceTypeRepos.DeleteAsync(deviceType);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
