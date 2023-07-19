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
    public class DeviceValueHistoryServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeviceValueHistoryService _deviceValueHistoryService;

        private List<DeviceValueHistory> _deviceValueHistoryList = new List<DeviceValueHistory>()
            { 
                new DeviceValueHistory { Id = Guid.NewGuid(), Device = Guid.NewGuid(), Timestamp = DateTime.Now, Value = "Test" }
            };
        
        public DeviceValueHistoryServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _deviceValueHistoryService = new DeviceValueHistoryService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDeviceValueHistorys()
        {
            // Arrange
            var expectedDeviceValueHistorys = _deviceValueHistoryList;
            _unitOfWorkMock
                .Setup(uow => uow.Repository<DeviceValueHistory>().GetAllAsync())
                .ReturnsAsync(expectedDeviceValueHistorys);

            // Act
            var result = await _deviceValueHistoryService.GetAll();

            // Assert
            Assert.Equal(expectedDeviceValueHistorys, result);
        }

        [Fact]
        public async Task GetOne_WithValidDeviceValueHistoryId_ShoudReturnDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryId = Guid.NewGuid();
            var expectedDeviceValueHistory = _deviceValueHistoryList.First();
            _unitOfWorkMock
                .Setup(uow => uow.Repository<DeviceValueHistory>().FindAsync(deviceValueHistoryId))
                .ReturnsAsync(expectedDeviceValueHistory);

            // Act
            var result = await _deviceValueHistoryService.GetOne(deviceValueHistoryId);

            // Assert
            Assert.Equal(expectedDeviceValueHistory, result);
        }

        [Fact]
        public async Task Update_WithValidDeviceValueHistory_ShouldUpdateDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryId = Guid.NewGuid();
            var deviceValueHistoryInput = new DeviceValueHistory { Id = deviceValueHistoryId };
            var existingDeviceValueHistory = new DeviceValueHistory { Id = deviceValueHistoryId };

            var deviceValueHistoryRepositoryMock = new Mock<IRepository<DeviceValueHistory>>();
            deviceValueHistoryRepositoryMock.Setup(repo => repo.FindAsync(deviceValueHistoryId)).ReturnsAsync(existingDeviceValueHistory);

            _unitOfWorkMock.Setup(uow => uow.Repository<DeviceValueHistory>()).Returns(deviceValueHistoryRepositoryMock.Object);

            // Act
            await _deviceValueHistoryService.Update(deviceValueHistoryInput);

            // Assert
            deviceValueHistoryRepositoryMock.Verify(repo => repo.FindAsync(deviceValueHistoryId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Add_WithValidDeviceValueHistory_ShouldAddDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryInput = new DeviceValueHistory { Id = Guid.NewGuid(), Device = Guid.NewGuid(), Timestamp = DateTime.Now, Value = "Test2" };

            var deviceValueHistoryRepositoryMock = new Mock<IRepository<DeviceValueHistory>>();

            _unitOfWorkMock.Setup(uow => uow.Repository<DeviceValueHistory>()).Returns(deviceValueHistoryRepositoryMock.Object);

            // Act
            await _deviceValueHistoryService.Add(deviceValueHistoryInput);

            // Assert            
            deviceValueHistoryRepositoryMock.Verify(repo => repo.InsertAsync(deviceValueHistoryInput, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Delete_WithValidDeviceValueHistoryId_ShouldDeleteDeviceValueHistory()
        {
            // Arrange
            var deviceValueHistoryId = Guid.NewGuid();
            var existingDeviceValueHistory = new DeviceValueHistory { Id = deviceValueHistoryId };

            var deviceValueHistoryRepositoryMock = new Mock<IRepository<DeviceValueHistory>>();
            deviceValueHistoryRepositoryMock.Setup(repo => repo.FindAsync(deviceValueHistoryId)).ReturnsAsync(existingDeviceValueHistory);

            _unitOfWorkMock.Setup(uow => uow.Repository<DeviceValueHistory>()).Returns(deviceValueHistoryRepositoryMock.Object);

            // Act
            await _deviceValueHistoryService.Delete(deviceValueHistoryId);

            // Assert
            deviceValueHistoryRepositoryMock.Verify(repo => repo.FindAsync(deviceValueHistoryId), Times.Once);
            deviceValueHistoryRepositoryMock.Verify(repo => repo.DeleteAsync(existingDeviceValueHistory, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }
    }
}