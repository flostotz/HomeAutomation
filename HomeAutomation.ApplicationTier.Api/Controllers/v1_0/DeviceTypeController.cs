using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.ApplicationTier.Entity.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.ApplicationTier.Api.Controllers.v1_0
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeviceTypeController : ControllerBase
    {
        private readonly IDeviceTypeService _deviceTypeService;
        public DeviceTypeController(IDeviceTypeService deviceTypeService)
        {
            _deviceTypeService = deviceTypeService;
        }

        #region CRUD

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IList<DeviceType>> GetAll()
        {
            return await _deviceTypeService.GetAll();
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task Update(DeviceTypeDto deviceTypeDto)
        {
            var deviceType = deviceTypeDto.ToEntity();

            await _deviceTypeService.Update(deviceType);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        public async Task<DeviceType> GetOne([FromRoute] Guid id)
        {
            return await _deviceTypeService.GetOne(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task Add(DeviceTypeDto deviceTypeDto)
        {
            var deviceType = deviceTypeDto.ToEntity();
            await _deviceTypeService.Add(deviceType);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _deviceTypeService.Delete(id);
        }

        #endregion
    }
}