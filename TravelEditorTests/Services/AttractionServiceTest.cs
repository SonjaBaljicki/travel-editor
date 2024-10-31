using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;
using TravelEditor.Services;
using Xunit;

namespace TravelEditorTests.Services
{
    public class AttractionServiceTest
    {
        private readonly AttractionService _attractionService;
        private readonly Mock<IAttractionRepository> _mockAttractionRepository;
        private readonly Mock<IDestinationService> _mockDestinationService;

        public AttractionServiceTest()
        {
            _mockAttractionRepository = new Mock<IAttractionRepository>();
            _mockDestinationService = new Mock<IDestinationService>();
            _attractionService = new AttractionService(_mockAttractionRepository.Object, _mockDestinationService.Object);
        }

        [Fact]
        public void LoadAll_ShouldReturnAllAttractions()
        {
            // Arrange
            var attractions = new List<Attraction>
        {
            new Attraction { AttractionId = 1 },
            new Attraction { AttractionId = 2 }
        };
            _mockAttractionRepository.Setup(repo => repo.LoadAll()).Returns(attractions);

            // Act
            var result = _attractionService.LoadAll();

            // Assert
            Assert.Equal(attractions, result);
        }

        [Fact]
        public void Update_ShouldUpdateAttractionAndTransferToNewDestination()
        {
            // Arrange
            var attraction = new Attraction { AttractionId = 1 };
            var destination = new Destination { DestinationId = 1, Attractions = new List<Attraction>() };

            _mockAttractionRepository.Setup(repo => repo.Update(attraction)).Returns(true);
            _mockAttractionRepository.Setup(repo => repo.Delete(attraction)).Returns(true);
            _mockDestinationService.Setup(service => service.AddDestinationAttraction(destination, attraction)).Returns(true);

            // Act
            var result = _attractionService.Update(attraction, destination);

            // Assert
            Assert.True(result);
            _mockAttractionRepository.Verify(repo => repo.Update(attraction), Times.Once);
            _mockAttractionRepository.Verify(repo => repo.Delete(attraction), Times.Once);
            _mockDestinationService.Verify(service => service.AddDestinationAttraction(destination, attraction), Times.Once);
        }

        [Fact]
        public void Update_ShouldNotTransferAttractionIfAlreadyInDestination()
        {
            // Arrange
            var attraction = new Attraction { AttractionId = 1 };
            var destination = new Destination { DestinationId = 1, Attractions = new List<Attraction> { attraction } };

            _mockAttractionRepository.Setup(repo => repo.Update(attraction)).Returns(true);

            // Act
            var result = _attractionService.Update(attraction, destination);

            // Assert
            Assert.True(result);
            _mockAttractionRepository.Verify(repo => repo.Update(attraction), Times.Once);
            _mockAttractionRepository.Verify(repo => repo.Delete(attraction), Times.Never);
            _mockDestinationService.Verify(service => service.AddDestinationAttraction(destination, attraction), Times.Never);
        }

        [Fact]
        public void Delete_ShouldReturnTrueWhenAttractionIsDeleted()
        {
            // Arrange
            var attraction = new Attraction { AttractionId = 1 };
            _mockAttractionRepository.Setup(repo => repo.FindOne(attraction)).Returns(true);
            _mockAttractionRepository.Setup(repo => repo.Delete(attraction)).Returns(true);

            // Act
            var result = _attractionService.Delete(attraction);

            // Assert
            Assert.True(result);
            _mockAttractionRepository.Verify(repo => repo.Delete(attraction), Times.Once);
        }

        [Fact]
        public void Delete_ShouldReturnFalseWhenAttractionDoesNotExist()
        {
            // Arrange
            var attraction = new Attraction { AttractionId = 1 };
            _mockAttractionRepository.Setup(repo => repo.FindOne(attraction)).Returns(false);

            // Act
            var result = _attractionService.Delete(attraction);

            // Assert
            Assert.False(result);
            _mockAttractionRepository.Verify(repo => repo.Delete(attraction), Times.Never);
        }
    }
}
