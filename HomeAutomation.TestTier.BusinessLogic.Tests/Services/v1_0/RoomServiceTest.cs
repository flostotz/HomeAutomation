using HomeAutomation.ApplicationTier.BusinessLogic.Services.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces;
using Moq;

namespace HomeAutomation.TestTier.BusinessLogic.Tests.Services.v1_0
{
    public class RoomServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RoomService _roomService;

        private List<Room> _roomList = new List<Room>()
            {
                new Room { Id = Guid.NewGuid(), Name = "Raum 1", Building = Guid.NewGuid() }
            };

        public RoomServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _roomService = new RoomService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfRooms()
        {
            // Arrange
            var expectedRooms = _roomList;
            _unitOfWorkMock
                .Setup(uow => uow.Repository<Room>().GetAllAsync())
                .ReturnsAsync(expectedRooms);

            // Act
            var result = await _roomService.GetAll();

            // Assert
            Assert.Equal(expectedRooms, result);
        }

        [Fact]
        public async Task GetOne_WithValidRoomId_ShoudReturnRoom()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var expectedRoom = _roomList.First();
            _unitOfWorkMock
                .Setup(uow => uow.Repository<Room>().FindAsync(roomId))
                .ReturnsAsync(expectedRoom);

            // Act
            var result = await _roomService.GetOne(roomId);

            // Assert
            Assert.Equal(expectedRoom, result);
        }

        [Fact]
        public async Task Update_WithValidRoom_ShouldUpdateRoom()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var roomInput = new Room { Id = roomId };
            var existingRoom = new Room { Id = roomId };

            var roomRepositoryMock = new Mock<IRepository<Room>>();
            roomRepositoryMock.Setup(repo => repo.FindAsync(roomId)).ReturnsAsync(existingRoom);

            _unitOfWorkMock.Setup(uow => uow.Repository<Room>()).Returns(roomRepositoryMock.Object);

            // Act
            await _roomService.Update(roomInput);

            // Assert
            roomRepositoryMock.Verify(repo => repo.FindAsync(roomId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Add_WithValidRoom_ShouldAddRoom()
        {
            // Arrange
            var roomInput = new Room { Id = Guid.NewGuid(), Name = "Raum 2", Building = Guid.NewGuid() };

            var roomRepositoryMock = new Mock<IRepository<Room>>();

            _unitOfWorkMock.Setup(uow => uow.Repository<Room>()).Returns(roomRepositoryMock.Object);

            // Act
            await _roomService.Add(roomInput);

            // Assert            
            roomRepositoryMock.Verify(repo => repo.InsertAsync(roomInput, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Delete_WithValidRoomId_ShouldDeleteRoom()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var existingRoom = new Room { Id = roomId };

            var roomRepositoryMock = new Mock<IRepository<Room>>();
            roomRepositoryMock.Setup(repo => repo.FindAsync(roomId)).ReturnsAsync(existingRoom);

            _unitOfWorkMock.Setup(uow => uow.Repository<Room>()).Returns(roomRepositoryMock.Object);

            // Act
            await _roomService.Delete(roomId);

            // Assert
            roomRepositoryMock.Verify(repo => repo.FindAsync(roomId), Times.Once);
            roomRepositoryMock.Verify(repo => repo.DeleteAsync(existingRoom, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }
    }
}