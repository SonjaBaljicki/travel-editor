using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services;
using Xunit;

namespace TravelEditorTests.Services
{
    public class DestinationServiceTest
    {
        private readonly DestinationService _destinationService;
        private readonly Mock<IDestinationRepository> _mockDestinationRepository;

        public DestinationServiceTest()
        {
            _mockDestinationRepository = new Mock<IDestinationRepository>();
            _destinationService = new DestinationService(_mockDestinationRepository.Object);
        }

        [Fact]
        public void LoadAll_ShouldReturnAllDestinations()
        {
            // Arrange
            var destinations = new List<Destination>
        {
            new Destination { DestinationId = 1 },
            new Destination { DestinationId = 2 }
        };
            _mockDestinationRepository.Setup(repo => repo.LoadAll()).Returns(destinations);

            // Act
            var result = _destinationService.LoadAll();

            // Assert
            Assert.Equal(destinations, result);
        }

        [Fact]
        public void Add_ShouldReturnTrueWhenDestinationIsAdded()
        {
            // Arrange
            var destination = new Destination { DestinationId = 1 };
            _mockDestinationRepository.Setup(repo => repo.Add(destination)).Returns(true);

            // Act
            var result = _destinationService.Add(destination);

            // Assert
            Assert.True(result);
            _mockDestinationRepository.Verify(repo => repo.Add(destination), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnTrueWhenDestinationIsUpdated()
        {
            // Arrange
            var destination = new Destination { DestinationId = 1 };
            _mockDestinationRepository.Setup(repo => repo.Update(destination)).Returns(true);

            // Act
            var result = _destinationService.Update(destination);

            // Assert
            Assert.True(result);
            _mockDestinationRepository.Verify(repo => repo.Update(destination), Times.Once);
        }

        [Fact]
        public void AddDestinationAttraction_ShouldReturnTrueWhenAttractionIsAdded()
        {
            // Arrange
            var destination = new Destination { DestinationId = 1 };
            var attraction = new Attraction { AttractionId = 1 };
            _mockDestinationRepository.Setup(repo => repo.AddDestinationAttraction(destination, attraction)).Returns(true);

            // Act
            var result = _destinationService.AddDestinationAttraction(destination, attraction);

            // Assert
            Assert.True(result);
            _mockDestinationRepository.Verify(repo => repo.AddDestinationAttraction(destination, attraction), Times.Once);
        }

        [Fact]
        public void Delete_ShouldReturnTrueWhenDestinationIsDeleted()
        {
            // Arrange
            var destination = new Destination { DestinationId = 1 };
            _mockDestinationRepository.Setup(repo => repo.FindOne(destination)).Returns(true);
            _mockDestinationRepository.Setup(repo => repo.HasAssociatedTrips(destination)).Returns(false);
            _mockDestinationRepository.Setup(repo => repo.Delete(destination)).Returns(true);

            // Act
            var result = _destinationService.Delete(destination);

            // Assert
            Assert.True(result);
            _mockDestinationRepository.Verify(repo => repo.Delete(destination), Times.Once);
        }

        [Fact]
        public void Delete_ShouldReturnFalseWhenDestinationHasTrips()
        {
            // Arrange
            var destination = new Destination { DestinationId = 1 };
            _mockDestinationRepository.Setup(repo => repo.FindOne(destination)).Returns(true);
            _mockDestinationRepository.Setup(repo => repo.HasAssociatedTrips(destination)).Returns(true);

            // Act
            var result = _destinationService.Delete(destination);

            // Assert
            Assert.False(result);
            _mockDestinationRepository.Verify(repo => repo.Delete(destination), Times.Never);
        }

        [Fact]
        public void FindDestinationWithAttraction_ShouldReturnDestination()
        {
            // Arrange
            var attraction = new Attraction { AttractionId = 1 };
            var destination = new Destination { DestinationId = 1 };
            _mockDestinationRepository.Setup(repo => repo.FindDestinationWithAttraction(attraction)).Returns(destination);

            // Act
            var result = _destinationService.FindDestinationWithAttraction(attraction);

            // Assert
            Assert.Equal(destination, result);
        }
    }
}
