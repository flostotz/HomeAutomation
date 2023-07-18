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
    public class DeviceControllerTest
    {
        private readonly Mock<IDeviceService> _deviceServiceMock;
        private readonly DeviceController _deviceController;

        private List<Device> _deviceList = new List<Device>()
        {
            new Device { Id = Guid.NewGuid(), Config = "", DeviceType = Guid.NewGuid(), Name = "Test", DeviceValueHistory = null }
        };

        private DeviceDto _deviceDto = new DeviceDto { Id = Guid.NewGuid(), Config = "", DeviceType = Guid.NewGuid(), Name = "Test", DeviceValueHistory = null };
    
        public DeviceControllerTest()
        {
            _deviceServiceMock = new Mock<IDeviceService>();
            _deviceController = new DeviceController(_deviceServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDevices()
        {
            // Arrange
            var expectedDevices = _deviceList;

            _deviceServiceMock
                .Setup(service => service.GetAll())
                .ReturnsAsync(expectedDevices);

            // Act
            var result = await _deviceController.GetAll();

            // Assert
            Assert.Equal(expectedDevices, result);
        }

        [Fact]
        public async Task GetOne_WithValidDeviceId_ShouldReturnDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var expectedDevice = _deviceList.First();

            _deviceServiceMock
                .Setup(service => service.GetOne(deviceId))
                .ReturnsAsync(expectedDevice);

            // Act
            var result = await _deviceController.GetOne(deviceId);

            // Assert
            Assert.Equal(expectedDevice, result);
        }

        [Fact]
        public async Task Update_WithValidDeviceDto_ShouldCallServiceToUpdateDevice()
        {
            // Arrange
            var deviceDto = _deviceDto;
            var device = deviceDto.ToEntity();

            // Act
            await _deviceController.Update(deviceDto);

            // Assert
            _deviceServiceMock.Verify(service => service.Update(device), Times.Once);
        }

        [Fact]
        public async Task Add_WithValidDeviceDto_ShouldCallServiceToAddDevice()
        {
            // Arrange
            var deviceDto = _deviceDto;
            var device = deviceDto.ToEntity();

            // Act
            await _deviceController.Add(deviceDto);

            // Assert
            _deviceServiceMock.Verify(service => service.Add(device), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidDeviceId_ShouldCallServiceToDeleteDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();

            // Act
            await _deviceController.Delete(deviceId);

            // Assert
            _deviceServiceMock.Verify(service => service.Delete(deviceId), Times.Once);
        }
    }
}
