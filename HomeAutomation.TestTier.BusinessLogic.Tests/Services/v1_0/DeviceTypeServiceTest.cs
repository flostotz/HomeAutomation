using HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0;
using HomeAutomation.ApplicationTier.DataAccess;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.TestTier.BusinessLogic.Tests.Services.v1_0
{
    public class DeviceTypeServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeviceTypeService _deviceTypeService;

        private List<DeviceType> _deviceTypeList = new List<DeviceType>()
            { 
                new DeviceType { Id = Guid.NewGuid(), Type = "Lampe" }
            };
        
        public DeviceTypeServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _deviceTypeService = new DeviceTypeService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDeviceTypes()
        {
            // Arrange
            var expectedDeviceTypes = _deviceTypeList;
            _unitOfWorkMock
                .Setup(uow => uow.Repository<DeviceType>().GetAllAsync())
                .ReturnsAsync(expectedDeviceTypes);

            // Act
            var result = await _deviceTypeService.GetAll();

            // Assert
            Assert.Equal(expectedDeviceTypes, result);
        }

        [Fact]
        public async Task GetOne_WithValidDeviceTypeId_ShoudReturnDeviceType()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var expectedDeviceTypes = _deviceTypeList.First();
            _unitOfWorkMock
                .Setup(uow => uow.Repository<DeviceType>().FindAsync(deviceId))
                .ReturnsAsync(expectedDeviceTypes);

            // Act
            var result = await _deviceTypeService.GetOne(deviceId);

            // Assert
            Assert.Equal(expectedDeviceTypes, result);
        }

        [Fact]
        public async Task Update_WithValidDeviceTypes_ShouldUpdateDeviceTypes()
        {
            // Arrange
            var deviceTypeId = Guid.NewGuid();
            var deviceTypeInput = new DeviceType { Id = deviceTypeId };
            var existingDeviceType = new DeviceType { Id = deviceTypeId };

            var deviceTypeRepositoryMock = new Mock<IRepository<DeviceType>>();
            deviceTypeRepositoryMock.Setup(repo => repo.FindAsync(deviceTypeId)).ReturnsAsync(existingDeviceType);

            _unitOfWorkMock.Setup(uow => uow.Repository<DeviceType>()).Returns(deviceTypeRepositoryMock.Object);

            // Act
            await _deviceTypeService.Update(deviceTypeInput);

            // Assert
            deviceTypeRepositoryMock.Verify(repo => repo.FindAsync(deviceTypeId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Add_WithValidDeviceType_ShouldAddDeviceType()
        {
            // Arrange
            var deviceTypeInput = new DeviceType { Id = Guid.NewGuid(), Type = "Licht" };

            var deviceTypeRepositoryMock = new Mock<IRepository<DeviceType>>();

            _unitOfWorkMock.Setup(uow => uow.Repository<DeviceType>()).Returns(deviceTypeRepositoryMock.Object);

            // Act
            await _deviceTypeService.Add(deviceTypeInput);

            // Assert            
            deviceTypeRepositoryMock.Verify(repo => repo.InsertAsync(deviceTypeInput, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Delete_WithValidDeviceTypeId_ShouldDeleteDeviceType()
        {
            // Arrange
            var deviceTypeId = Guid.NewGuid();
            var existingDeviceType = new DeviceType { Id = deviceTypeId };

            var deviceTypeRepositoryMock = new Mock<IRepository<DeviceType>>();
            deviceTypeRepositoryMock.Setup(repo => repo.FindAsync(deviceTypeId)).ReturnsAsync(existingDeviceType);

            _unitOfWorkMock.Setup(uow => uow.Repository<DeviceType>()).Returns(deviceTypeRepositoryMock.Object);

            // Act
            await _deviceTypeService.Delete(deviceTypeId);

            // Assert
            deviceTypeRepositoryMock.Verify(repo => repo.FindAsync(deviceTypeId), Times.Once);
            deviceTypeRepositoryMock.Verify(repo => repo.DeleteAsync(existingDeviceType, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }
    }
}