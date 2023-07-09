using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;

namespace HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0
{
    public class DeviceValueHistoryService : IDeviceValueHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceValueHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<DeviceValueHistory>> GetAll()
        {
            return await _unitOfWork.Repository<DeviceValueHistory>().GetAllAsync();
        }

        public async Task<DeviceValueHistory> GetOne(Guid deviceValueHistoryId)
        {
            return await _unitOfWork.Repository<DeviceValueHistory>().FindAsync(deviceValueHistoryId);
        }

        public async Task Update(DeviceValueHistory deviceValueHistoryInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceValueHistoryRepos = _unitOfWork.Repository<DeviceValueHistory>();
                var deviceValueHistory = await deviceValueHistoryRepos.FindAsync(deviceValueHistoryInput.Id) ?? throw new KeyNotFoundException();
                deviceValueHistory.Id = deviceValueHistory.Id;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(DeviceValueHistory deviceValueHistoryInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceValueHistoryRepos = _unitOfWork.Repository<DeviceValueHistory>();
                await deviceValueHistoryRepos.InsertAsync(deviceValueHistoryInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(Guid deviceValueHistoryId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var deviceValueHistoryRepos = _unitOfWork.Repository<DeviceValueHistory>();
                var deviceValueHistory = await deviceValueHistoryRepos.FindAsync(deviceValueHistoryId) ?? throw new KeyNotFoundException();
                await deviceValueHistoryRepos.DeleteAsync(deviceValueHistory);

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
