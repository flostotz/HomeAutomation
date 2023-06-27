using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;

namespace HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Room>> GetAll()
        {
            return await _unitOfWork.Repository<Room>().GetAllAsync();
        }

        public async Task<Room> GetOne(int roomId)
        {
            return await _unitOfWork.Repository<Room>().FindAsync(roomId);
        }

        public async Task Update(Room roomInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var roomRepos = _unitOfWork.Repository<Room>();
                var room = await roomRepos.FindAsync(roomInput.Id) ?? throw new KeyNotFoundException();
                room.Id = room.Id;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(Room roomInput)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var roomRepos = _unitOfWork.Repository<Room>();
                await roomRepos.InsertAsync(roomInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int roomId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var roomRepos = _unitOfWork.Repository<Room>();
                var room = await roomRepos.FindAsync(roomId) ?? throw new KeyNotFoundException();
                await roomRepos.DeleteAsync(room);

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
