using HomeAutomation.ApplicationTier.Api.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IRoomService
    {
        /// <summary>
        /// Get all items of Room table
        /// </summary>
        /// <returns></returns>
        Task<IList<RoomDto>> GetAll();
        Task<RoomDto> GetOne(Guid roomDtoId);
        Task Update(Room room);
        Task Add(Room room);
        Task Delete(Guid roomId);
    }
}