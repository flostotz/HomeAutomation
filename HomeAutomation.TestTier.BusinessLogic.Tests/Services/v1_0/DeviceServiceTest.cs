using HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using Moq;

namespace HomeAutomation.TestTier.BusinessLogic.Tests.Services.v1_0
{
    public class DeviceServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeviceService _deviceService;

        private List<Device> _deviceList = new List<Device>()
            {
                new Device { Id = Guid.NewGuid(), Config = "Test", DeviceType = Guid.NewGuid(), Name = "Iphone" }
            };

        public DeviceServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _deviceService = new DeviceService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfDevices()
        {
            // Arrange
            var expectedDevices = _deviceList;
            _unitOfWorkMock
                .Setup(uow => uow.Repository<Device>().GetAllAsync())
                .ReturnsAsync(expectedDevices);

            // Act
            var result = await _deviceService.GetAll();

            // Assert
            Assert.Equal(expectedDevices, result);
        }

        [Fact]
        public async Task GetOne_WithValidDeviceId_ShoudReturnDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var expectedDevice = _deviceList.First();
            _unitOfWorkMock
                .Setup(uow => uow.Repository<Device>().FindAsync(deviceId))
                .ReturnsAsync(expectedDevice);

            // Act
            var result = await _deviceService.GetOne(deviceId);

            // Assert
            Assert.Equal(expectedDevice, result);
        }

        [Fact]
        public async Task Update_WithValidDevice_ShouldUpdateDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceInput = new Device { Id = deviceId };
            var existingDevice = new Device { Id = deviceId };

            var deviceRepositoryMock = new Mock<IRepository<Device>>();
            deviceRepositoryMock.Setup(repo => repo.FindAsync(deviceId)).ReturnsAsync(existingDevice);

            _unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(deviceRepositoryMock.Object);

            // Act
            await _deviceService.Update(deviceInput);

            // Assert
            deviceRepositoryMock.Verify(repo => repo.FindAsync(deviceId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Add_WithValidDevice_ShouldAddDevice()
        {
            // Arrange
            var deviceInput = new Device { Id = Guid.NewGuid(), Config = "Test", DeviceType = Guid.NewGuid(), Name = "Samsung" };

            var deviceRepositoryMock = new Mock<IRepository<Device>>();

            _unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(deviceRepositoryMock.Object);

            // Act
            await _deviceService.Add(deviceInput);

            // Assert            
            deviceRepositoryMock.Verify(repo => repo.InsertAsync(deviceInput, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Delete_WithValidDeviceId_ShouldDeleteDevice()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var existingDevice = new Device { Id = deviceId };

            var deviceRepositoryMock = new Mock<IRepository<Device>>();
            deviceRepositoryMock.Setup(repo => repo.FindAsync(deviceId)).ReturnsAsync(existingDevice);

            _unitOfWorkMock.Setup(uow => uow.Repository<Device>()).Returns(deviceRepositoryMock.Object);

            // Act
            await _deviceService.Delete(deviceId);

            // Assert
            deviceRepositoryMock.Verify(repo => repo.FindAsync(deviceId), Times.Once);
            deviceRepositoryMock.Verify(repo => repo.DeleteAsync(existingDevice, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }
    }
}