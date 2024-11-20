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
    public class TripServiceTest
    {
        private readonly Mock<ITripRepository> _tripRepositoryMock;
        private readonly TripService _tripService;

        public TripServiceTest()
        {
            _tripRepositoryMock = new Mock<ITripRepository>();
            _tripService = new TripService(_tripRepositoryMock.Object);
        }

        [Fact]
        public void LoadAll_ShouldReturnTrips()
        {
            // Arrange
            var expectedTrips = new List<Trip> { new Trip(), new Trip() };
            _tripRepositoryMock.Setup(repo => repo.LoadAll()).Returns(expectedTrips);

            // Act
            var result = _tripService.LoadAll();

            // Assert
            Assert.Equal(expectedTrips, result);
        }

        [Fact]
        public void Add_ValidTrip_ShouldReturnTrue()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2) };
            _tripRepositoryMock.Setup(repo => repo.Add(trip)).Returns(true);

            // Act
            var result = _tripService.Add(trip);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_InvalidTrip_ShouldReturnFalse()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(-1) };

            // Act
            var result = _tripService.Add(trip);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Update_ValidTrip_ShouldReturnTrue()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2) };
            _tripRepositoryMock.Setup(repo => repo.Update(trip)).Returns(true);

            // Act
            var result = _tripService.Update(trip);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_TripNotInProgress_ShouldReturnTrue()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(-4), EndDate = DateTime.Now.AddDays(-2) };
            _tripRepositoryMock.Setup(repo => repo.Delete(trip)).Returns(true);

            // Act
            var result = _tripService.Delete(trip);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_TripInProgress_ShouldReturnFalse()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) };

            // Act
            var result = _tripService.Delete(trip);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TravellerHasTrips_WithOngoingTrip_ShouldReturnTrue()
        {
            // Arrange
            var traveller = new Traveller();
            var ongoingTrip = new Trip { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1), Travellers = new List<Traveller> { traveller } };
            _tripRepositoryMock.Setup(repo => repo.FindTravellersTrips(traveller)).Returns(new List<Trip> { ongoingTrip });

            // Act
            var result = _tripService.TravellerHasTrips(traveller);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddTripReview_TripHasHappenedAndTravellerIsOnTrip_ShouldReturnTrue()
        {
            // Arrange
            var trip = new Trip
            {
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(-1),
                Travellers = new List<Traveller> { new Traveller { TravellerId = 1 } },
                Reviews = new List<Review>()
            };
            var review = new Review { Traveller = new Traveller { TravellerId = 1 } };
            _tripRepositoryMock.Setup(repo => repo.Update(trip)).Returns(true);

            // Act
            var result = _tripService.AddTripReview(trip, review);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddTripReview_TripHasNotHappened_ShouldReturnFalse()
        {
            // Arrange
            var trip = new Trip
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                Travellers = new List<Traveller> { new Traveller { TravellerId = 1 } }

            };
            var review = new Review { Traveller = new Traveller { TravellerId = 1 } };

            // Act
            var result = _tripService.AddTripReview(trip, review);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddTripReview_TravellerWasntOn_ShouldReturnFalse()
        {
            // Arrange
            var trip = new Trip
            {
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(-1),
                Travellers = new List<Traveller> { new Traveller { TravellerId = 2 } }

            };
            var review = new Review { Traveller = new Traveller { TravellerId = 1 } };

            // Act
            var result = _tripService.AddTripReview(trip, review);

            // Assert
            Assert.False(result);
        }
    }
}
