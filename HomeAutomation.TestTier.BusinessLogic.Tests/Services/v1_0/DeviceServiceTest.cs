using HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using Moq;

namespace HomeAutomation.TestTier.BusinessLogic.Tests.Services.v1_0
{
    public class DeviceServiceTests
    {
        [Fact]
        public async Task GetAll_ReturnsAllDevices()
        {
            // Arrange
            var devices = new List<Device>
            {
                new Device { Id = Guid.NewGuid(), Name = "Device 1" },
                new Device { Id = Guid.NewGuid(), Name = "Device 2" },
                new Device { Id = Guid.NewGuid(), Name = "Device 3" }
            };

            var repositoryMock = new Mock<IRepository<Device>>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(devices);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(repositoryMock.Object);

            var deviceService = new DeviceService(unitOfWorkMock.Object);

            // Act
            var result = await deviceService.GetAll();

            // Assert
            Assert.Equal(devices, result);
        }

        [Fact]
        public async Task GetOne_ReturnsDeviceById()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var device = new Device { Id = deviceId, Name = "Device 1" };

            var repositoryMock = new Mock<IRepository<Device>>();
            repositoryMock.Setup(repo => repo.FindAsync(deviceId)).ReturnsAsync(device);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(repositoryMock.Object);

            var deviceService = new DeviceService(unitOfWorkMock.Object);

            // Act
            var result = await deviceService.GetOne(deviceId);

            // Assert
            Assert.Equal(device, result);
        }

        [Fact]
        public async Task Update_DeviceExists_UpdatesDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceInput = new Device { Id = deviceId, Name = "Updated Device" };

            var repositoryMock = new Mock<IRepository<Device>>();
            repositoryMock.Setup(repo => repo.FindAsync(deviceId)).ReturnsAsync(new Device { Id = deviceId, Name = "Original Device" });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(repositoryMock.Object);

            var deviceService = new DeviceService(unitOfWorkMock.Object);

            // Act
            await deviceService.Update(deviceInput);

            // Assert
            unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
        }

        [Fact]
        public async Task Add_AddsDevice()
        {
            // Arrange
            var deviceInput = new Device { Id = Guid.NewGuid(), Name = "New Device" };

            var repositoryMock = new Mock<IRepository<Device>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(repositoryMock.Object);

            var deviceService = new DeviceService(unitOfWorkMock.Object);

            // Act
            await deviceService.Add(deviceInput);

            // Assert
            unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
        }

        [Fact]
        public async Task Delete_DeviceExists_DeletesDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();

            var repositoryMock = new Mock<IRepository<Device>>();
            repositoryMock.Setup(repo => repo.FindAsync(deviceId)).ReturnsAsync(new Device { Id = deviceId, Name = "Device 1" });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(repositoryMock.Object);

            var deviceService = new DeviceService(unitOfWorkMock.Object);

            // Act
            await deviceService.Delete(deviceId);

            // Assert
            unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
        }
    }
}
