﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.ApplicationTier.Api.Controllers.v1_0
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        #region CRUD

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IList<Device>> GetAll()
        {
            return await _deviceService.GetAll();
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task Update(Device device)
        {
            await _deviceService.Update(device);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        public async Task<Device> GetOne([FromRoute] int id)
        {
            return await _deviceService.GetOne(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task Add(Device device)
        {
            await _deviceService.Add(device);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task Delete([FromRoute] int id)
        {
            await _deviceService.Delete(id);
        }

        #endregion
    }
}