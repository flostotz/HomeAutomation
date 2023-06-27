using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.ApplicationTier.Api.Controllers.v1_0
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        #region CRUD

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IList<Room>> GetAll()
        {
            return await _roomService.GetAll();
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task Update(Room room)
        {
            await _roomService.Update(room);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        public async Task<Room> GetOne([FromRoute] int id)
        {
            return await _roomService.GetOne(id);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task Add(Room room)
        {
            await _roomService.Add(room);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task Delete([FromRoute] int id)
        {
            await _roomService.Delete(id);
        }

        #endregion
    }
}