using HomeAutomation.ApplicationTier.Api.Controllers.v1_0;
using HomeAutomation.ApplicationTier.Api.Dtos.v1_0;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using HomeAutomation.ApplicationTier.Entity.Interfaces.Services.v1_0;
using Moq;

namespace HomeAutomation.TestTier.BusinessLogic.Tests.Controller.v1_0
{
    public class BuildingsControllerTest
    {
        private readonly Mock<IBuildingService> _buildingServiceMock;
        private readonly BuildingController _buildingController;

        private List<Building> _buildingList = new List<Building>()
            { new Building { Id = Guid.NewGuid(), Housenumber = "7", State = "Gerolstein", Street = "Eichenweg", ZipCode = "54568" },
            { new Building { Id = Guid.NewGuid(), Housenumber = "37", State = "Walsdorf", Street = "Kölner Straße", ZipCode = "54567" } } };

        private BuildingDto _buildingDto = new BuildingDto { Id = Guid.NewGuid(), Housenumber = "7", State = "Gerolstein", Street = "Eichenweg", ZipCode = "54568" };

        public BuildingsControllerTest()
        {
            _buildingServiceMock = new Mock<IBuildingService>();
            _buildingController = new BuildingController(_buildingServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfBuildings()
        {
            // Arrange
            var expectedBuildings = _buildingList;

            _buildingServiceMock
                .Setup(service => service.GetAll())
                .ReturnsAsync(expectedBuildings);

            // Act
            var result = await _buildingController.GetAll();

            // Assert
            Assert.Equal(expectedBuildings, result);
        }

        [Fact]
        public async Task GetOne_WithValidBuildingId_ShouldReturnBuilding()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            var expectedBuilding = _buildingList.First();

            _buildingServiceMock
                .Setup(service => service.GetOne(buildingId))
                .ReturnsAsync(expectedBuilding);

            // Act
            var result = await _buildingController.GetOne(buildingId);

            // Assert
            Assert.Equal(expectedBuilding, result);
        }

        [Fact]
        public async Task Update_WithValidBuildingDto_ShouldCallServiceToUpdateBuilding()
        {
            // Arrange
            var buildingDto = _buildingDto;
            var building = buildingDto.ToEntity();

            // Act
            await _buildingController.Update(buildingDto);

            // Assert
            _buildingServiceMock.Verify(service => service.Update(building), Times.Once);
        }

        [Fact]
        public async Task Add_WithValidBuildingDto_ShouldCallServiceToAddBuilding()
        {
            // Arrange
            var buildingDto = _buildingDto;
            var building = buildingDto.ToEntity();

            // Act
            await _buildingController.Add(buildingDto);

            // Assert
            _buildingServiceMock.Verify(service => service.Add(building), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidBuildingId_ShouldCallServiceToDeleteBuilding()
        {
            // Arrange
            var buildingId = Guid.NewGuid();

            // Act
            await _buildingController.Delete(buildingId);

            // Assert
            _buildingServiceMock.Verify(service => service.Delete(buildingId), Times.Once);
        }
    }
}
