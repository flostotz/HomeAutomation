using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;

namespace HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Device>> GetAll()
        {
            return await _unitOfWork.Repository<Device>().GetAllAsync();
        }

        public async Task<Device> GetOne(int deviceId)
        {
            return await _unitOfWork.Repository<Device>().FindAsync(deviceId);
        }

        public async Task Update(Device deviceInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceRepos = _unitOfWork.Repository<Device>();
                var device = await deviceRepos.FindAsync(deviceInput.Id) ?? throw new KeyNotFoundException();
                device.Id = device.Id;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(Device deviceInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceRepos = _unitOfWork.Repository<Device>();
                await deviceRepos.InsertAsync(deviceInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int deviceId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceRepos = _unitOfWork.Repository<Device>();
                var device = await deviceRepos.FindAsync(deviceId) ?? throw new KeyNotFoundException();
                await deviceRepos.DeleteAsync(device);

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
