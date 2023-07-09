using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;

namespace HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BuildingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Building>> GetAll()
        {
            return await _unitOfWork.Repository<Building>().GetAllAsync();
        }

        public async Task<Building> GetOne(Guid buildingId)
        {
            return await _unitOfWork.Repository<Building>().FindAsync(buildingId);
        }

        public async Task Update(Building buildingInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var buildingRepos = _unitOfWork.Repository<Building>();
                var building = await buildingRepos.FindAsync(buildingInput.Id) ?? throw new KeyNotFoundException();
                building.Id = building.Id;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(Building buildingInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var buildingRepos = _unitOfWork.Repository<Building>();
                await buildingRepos.InsertAsync(buildingInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(Guid buildingId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var buildingRepos = _unitOfWork.Repository<Building>();
                var building = await buildingRepos.FindAsync(buildingId) ?? throw new KeyNotFoundException();
                await buildingRepos.DeleteAsync(building);

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
