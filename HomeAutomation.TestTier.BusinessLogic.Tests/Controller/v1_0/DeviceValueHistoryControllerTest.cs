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
    public class DeviceValueHistoryControllerTest
    {
        private readonly Mock<IDeviceValueHistoryService> _deviceValueHistoryServiceMock;
        private readonly DeviceValueHistoryController _deviceValueHistoryController;

        private List<DeviceValueHistory> _deviceValueHistoryList = new List<DeviceValueHistory>()
        {
            new DeviceValueHistory { Id = Guid.NewGuid(), Timestamp = DateTime.Now, Value = "Test" }
        };

        private DeviceValueHistoryDto _deviceValueHistoryDto = new DeviceValueHistoryDto { Id = Guid.NewGuid(), Timestamp = DateTime.Now, Value = "Test" };

        public DeviceValueHistoryControllerTest()
        {
            _deviceValueHistoryServiceMock = new Mock<IDeviceValueHistoryService>();
            _deviceValueHistoryController = new DeviceValueHistoryController(_deviceValueHistoryServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDeviceValueHistory()
        {
            // Arrange
            var expectedDeviceValueHistory = _deviceValueHistoryList;

            _deviceValueHistoryServiceMock
                .Setup(service => service.GetAll())
                .ReturnsAsync(expectedDeviceValueHistory);

            // Act
            var result = await _deviceValueHistoryController.GetAll();

            // Assert
            Assert.Equal(expectedDeviceValueHistory, result);
        }

        [Fact]
        public async Task GetOne_WithValidDeviceValueHistoryId_ShouldReturnDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryId = Guid.NewGuid();
            var expectedDeviceValueHistory = _deviceValueHistoryList.First();

            _deviceValueHistoryServiceMock
                .Setup(service => service.GetOne(deviceValueHistoryId))
                .ReturnsAsync(expectedDeviceValueHistory);

            // Act
            var result = await _deviceValueHistoryController.GetOne(deviceValueHistoryId);

            // Assert
            Assert.Equal(expectedDeviceValueHistory, result);
        }

        [Fact]
        public async Task Update_WithValidDeviceValueHistoryDto_ShouldCallServiceToUpdateDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryDto = _deviceValueHistoryDto;
            var deviceValueHistory = deviceValueHistoryDto.ToEntity();

            // Act
            await _deviceValueHistoryController.Update(deviceValueHistoryDto);

            // Assert
            _deviceValueHistoryServiceMock.Verify(service => service.Update(deviceValueHistory), Times.Once);
        }

        [Fact]
        public async Task Add_WithValidDeviceValueHistoryDto_ShouldCallServiceToAddDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryDto = _deviceValueHistoryDto;
            var deviceValueHistory = deviceValueHistoryDto.ToEntity();

            // Act
            await _deviceValueHistoryController.Add(deviceValueHistoryDto);

            // Assert
            _deviceValueHistoryServiceMock.Verify(service => service.Add(deviceValueHistory), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidDeviceValueHistoryId_ShouldCallServiceToDeleteDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryId = Guid.NewGuid();

            // Act
            await _deviceValueHistoryController.Delete(deviceValueHistoryId);

            // Assert
            _deviceValueHistoryServiceMock.Verify(service => service.Delete(deviceValueHistoryId), Times.Once);
        }
    }
}
