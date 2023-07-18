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
    public class BuildingServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly BuildingService _buildingService;
        private List<Building> _buildingList = new List<Building>()
            { new Building { Id = Guid.NewGuid(), Housenumber = "7", State = "Gerolstein", Street = "Eichenweg", ZipCode = "54568" },
            { new Building { Id = Guid.NewGuid(), Housenumber = "37", State = "Walsdorf", Street = "Kölner Straße", ZipCode = "54567" } } };
        
        public BuildingServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _buildingService = new BuildingService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfBuildings()
        {
            // Arrange
            var expectedBuildings = _buildingList;
            _unitOfWorkMock
                .Setup(uow => uow.Repository<Building>().GetAllAsync())
                .ReturnsAsync(expectedBuildings);

            // Act
            var result = await _buildingService.GetAll();

            // Assert
            Assert.Equal(expectedBuildings, result);
        }

        [Fact]
        public async Task GetOne_WithValidBuildingId_ShoudReturnBuilding()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            var expectedBuilding = _buildingList.First();
            _unitOfWorkMock
                .Setup(uow => uow.Repository<Building>().FindAsync(buildingId))
                .ReturnsAsync(expectedBuilding);

            // Act
            var result = await _buildingService.GetOne(buildingId);

            // Assert
            Assert.Equal(expectedBuilding, result);
        }

        [Fact]
        public async Task Update_WithValidBuilding_ShouldUpdateBuilding()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            var buildingInput = new Building { Id = buildingId };
            var existingBuilding = new Building { Id = buildingId };

            var buildingRepositoryMock = new Mock<IRepository<Building>>();
            buildingRepositoryMock.Setup(repo => repo.FindAsync(buildingId)).ReturnsAsync(existingBuilding);

            _unitOfWorkMock.Setup(uow => uow.Repository<Building>()).Returns(buildingRepositoryMock.Object);

            // Act
            await _buildingService.Update(buildingInput);

            // Assert
            buildingRepositoryMock.Verify(repo => repo.FindAsync(buildingId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Add_WithValidBuilding_ShouldAddBuilding()
        {
            // Arrange
            var buildingInput = new Building { Id = Guid.NewGuid(), Street = "Steinsbergstraße", Housenumber = "16", ZipCode = "56477", State = "Rennerod", Room = null };

            var buildingRepositoryMock = new Mock<IRepository<Building>>();

            _unitOfWorkMock.Setup(uow => uow.Repository<Building>()).Returns(buildingRepositoryMock.Object);

            // Act
            await _buildingService.Add(buildingInput);

            // Assert            
            buildingRepositoryMock.Verify(repo => repo.InsertAsync(buildingInput, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }

        [Fact]
        public async Task Delete_WithValidBuildingId_ShouldDeleteBuilding()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            var existingBuilding = new Building { Id = buildingId };

            var buildingRepositoryMock = new Mock<IRepository<Building>>();
            buildingRepositoryMock.Setup(repo => repo.FindAsync(buildingId)).ReturnsAsync(existingBuilding);

            _unitOfWorkMock.Setup(uow => uow.Repository<Building>()).Returns(buildingRepositoryMock.Object);

            // Act
            await _buildingService.Delete(buildingId);

            // Assert
            buildingRepositoryMock.Verify(repo => repo.FindAsync(buildingId), Times.Once);
            buildingRepositoryMock.Verify(repo => repo.DeleteAsync(existingBuilding, true), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransaction(), Times.Never);
        }
    }
}