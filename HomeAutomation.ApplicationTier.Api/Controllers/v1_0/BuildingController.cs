using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.ApplicationTier.Entity.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.guiderfaces.Services.v1_0;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.ApplicationTier.Api.Controllers.v1_0
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        #region CRUD

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IList<Building>> GetAll()
        {
            return await _buildingService.GetAll();
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task Update(BuildingDto buildingDto)
        {
            var building = buildingDto.ToEntity();

            await _buildingService.Update(building);
        }

        [HttpGet("{id:guid}")]
        [MapToApiVersion("1.0")]
        public async Task<Building> GetOne([FromRoute] Guid id)
        {
            return await _buildingService.GetOne(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task Add(BuildingDto buildingDto)
        {
            var building = buildingDto.ToEntity();

            await _buildingService.Add(building);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _buildingService.Delete(id);
        }

        #endregion
    }
}