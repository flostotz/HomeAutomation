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
    public class DeviceTypeControllerTest
    {
        private readonly Mock<IDeviceTypeService> _deviceTypeServiceMock;
        private readonly DeviceTypeController _deviceTypeController;

        private List<DeviceType> _deviceTypeList = new List<DeviceType>()
        {
            new DeviceType { Id = Guid.NewGuid(), Type = "Lampe"}
        };

        private DeviceTypeDto _deviceTypeDto = new DeviceTypeDto { Id = Guid.NewGuid(), Type = "Lampe" };

        public DeviceTypeControllerTest()
        {
            _deviceTypeServiceMock = new Mock<IDeviceTypeService>();
            _deviceTypeController = new DeviceTypeController(_deviceTypeServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDeviceTypes()
        {
            // Arrange
            var expectedDeviceTypes = _deviceTypeList;

            _deviceTypeServiceMock
                .Setup(service => service.GetAll())
                .ReturnsAsync(expectedDeviceTypes);

            // Act
            var result = await _deviceTypeController.GetAll();

            // Assert
            Assert.Equal(expectedDeviceTypes, result);
        }

        [Fact]
        public async Task GetOne_WithValidDeviceTypeId_ShouldReturnDeviceType()
        {
            // Arrange
            var deviceTypeId = Guid.NewGuid();
            var expectedDeviceType = _deviceTypeList.First();

            _deviceTypeServiceMock
                .Setup(service => service.GetOne(deviceTypeId))
                .ReturnsAsync(expectedDeviceType);

            // Act
            var result = await _deviceTypeController.GetOne(deviceTypeId);

            // Assert
            Assert.Equal(expectedDeviceType, result);
        }

        [Fact]
        public async Task Update_WithValidDeviceTypeDto_ShouldCallServiceToUpdateDeviceType()
        {
            // Arrange
            var deviceTypeDto = _deviceTypeDto;
            var deviceType = deviceTypeDto.ToEntity();

            // Act
            await _deviceTypeController.Update(deviceTypeDto);

            // Assert
            _deviceTypeServiceMock.Verify(service => service.Update(deviceType), Times.Once);
        }

        [Fact]
        public async Task Add_WithValidDeviceTypeDto_ShouldCallServiceToAddDeviceType()
        {
            // Arrange
            var deviceTypeDto = _deviceTypeDto;
            var deviceType = deviceTypeDto.ToEntity();

            // Act
            await _deviceTypeController.Add(deviceTypeDto);

            // Assert
            _deviceTypeServiceMock.Verify(service => service.Add(deviceType), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidDeviceTypeId_ShouldCallServiceToDeleteDeviceType()
        {
            // Arrange
            var deviceTypeId = Guid.NewGuid();

            // Act
            await _deviceTypeController.Delete(deviceTypeId);

            // Assert
            _deviceTypeServiceMock.Verify(service => service.Delete(deviceTypeId), Times.Once);
        }
    }
}
