using HomeAutomation.ApplicationTier.Api.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0
{
    public interface IBuildingService
    {
        /// <summary>
        /// Get all items of Building table
        /// </summary>
        /// <returns></returns>
        Task<IList<BuildingDto>> GetAll();
        Task<BuildingDto> GetOne(Guid buildingDtoId);
        Task Update(Building building);
        Task Add(Building building);
        Task Delete(Guid buildingId);
    }
}