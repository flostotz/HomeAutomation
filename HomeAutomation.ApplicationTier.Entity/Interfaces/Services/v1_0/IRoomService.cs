using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IRoomService
    {
        /// <summary>
        /// Get all items of Room table
        /// </summary>
        /// <returns></returns>
        Task<IList<Room>> GetAll();
        Task<Room> GetOne(int roomId);
        Task Update(Room room);
        Task Add(Room room);
        Task Delete(int roomId);
    }
}