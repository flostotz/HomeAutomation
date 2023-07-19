using HomeAutomation.ApplicationTier.Api.Controllers.v1_0;
using HomeAutomation.ApplicationTier.Entity.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.TestTier.BusinessLogic.Tests.Controller.v1_0
{
    public class RoomControllerTest
    {
        private readonly Mock<IRoomService> _roomServiceMock;
        private readonly RoomController _roomController;

        private List<Room> _roomList = new List<Room>()
        {
            new Room { Id = Guid.NewGuid(), Name = "Konferenzraum", Building = Guid.NewGuid() }
        };

        private RoomDto _roomDto = new RoomDto { Id = Guid.NewGuid(), Name = "Konferenzraum", Building = Guid.NewGuid() };


        public RoomControllerTest()
        {
            _roomServiceMock = new Mock<IRoomService>();
            _roomController = new RoomController(_roomServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfRooms()
        {
            // Arrange
            var expectedRooms = _roomList;

            _roomServiceMock
                .Setup(service => service.GetAll())
                .ReturnsAsync(expectedRooms);

            // Act
            var result = await _roomController.GetAll();

            // Assert
            Assert.Equal(expectedRooms, result);
        }

        [Fact]
        public async Task GetOne_WithValidRoomId_ShouldReturnRoom()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var expectedRoom = _roomList.First();

            _roomServiceMock
                .Setup(service => service.GetOne(roomId))
                .ReturnsAsync(expectedRoom);

            // Act
            var result = await _roomController.GetOne(roomId);

            // Assert
            Assert.Equal(expectedRoom, result);
        }

        [Fact]
        public async Task Update_WithValidRoomDto_ShouldCallServiceToUpdateRoom()
        {
            // Arrange
            var roomDto = _roomDto;
            var room = roomDto.ToEntity();

            // Act
            await _roomController.Update(roomDto);

            // Assert
            _roomServiceMock.Verify(service => service.Update(room), Times.Once);
        }

        [Fact]
        public async Task Add_WithValidRoomDto_ShouldCallServiceToAddRoom()
        {
            // Arrange
            var roomDto = _roomDto;
            var room = roomDto.ToEntity();

            // Act
            await _roomController.Add(roomDto);

            // Assert
            _roomServiceMock.Verify(service => service.Add(room), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidRoomId_ShouldCallServiceToDeleteRoom()
        {
            // Arrange
            var roomId = Guid.NewGuid();

            // Act
            await _roomController.Delete(roomId);

            // Assert
            _roomServiceMock.Verify(service => service.Delete(roomId), Times.Once);
        }
    }
}
