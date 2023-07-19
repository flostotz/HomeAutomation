using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.ApplicationTier.Api.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.ApplicationTier.Api.Controllers.v1_0
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeviceValueHistoryController : ControllerBase
    {
        private readonly IDeviceValueHistoryService _deviceValueHistoryService;
        public DeviceValueHistoryController(IDeviceValueHistoryService deviceValueHistoryService)
        {
            _deviceValueHistoryService = deviceValueHistoryService;
        }

        #region CRUD

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IList<DeviceValueHistory>> GetAll()
        {
            return await _deviceValueHistoryService.GetAll();
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task Update(DeviceValueHistoryDto deviceValueHistoryDto)
        {
            var deviceValueHistory = deviceValueHistoryDto.ToEntity();

            await _deviceValueHistoryService.Update(deviceValueHistory);
        }

        [HttpGet("{id:guid}")]
        [MapToApiVersion("1.0")]
        public async Task<DeviceValueHistory> GetOne([FromRoute] Guid id)
        {
            return await _deviceValueHistoryService.GetOne(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task Add(DeviceValueHistoryDto deviceValueHistoryDto)
        {
            var deviceValueHistory = deviceValueHistoryDto.ToEntity();
            await _deviceValueHistoryService.Add(deviceValueHistory);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _deviceValueHistoryService.Delete(id);
        }

        #endregion
    }
}