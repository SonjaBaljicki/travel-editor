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
    public class TravellerServiceTest
    {
        private readonly TravellerService _travellerService;
        private readonly Mock<ITravellerRepository> _mockTravellerRepository;
        private readonly Mock<ITripService> _mockTripService;
        private readonly Mock<IReviewService> _mockReviewService;

        public TravellerServiceTest()
        {
            _mockTravellerRepository = new Mock<ITravellerRepository>();
            _mockTripService = new Mock<ITripService>();
            _mockReviewService = new Mock<IReviewService>();
            _travellerService = new TravellerService(_mockTravellerRepository.Object, _mockTripService.Object, _mockReviewService.Object);
        }

        [Fact]
        public void LoadAll_ShouldReturnAllTravellers()
        {
            // Arrange
            var travellers = new List<Traveller> { new Traveller { TravellerId = 1 }, new Traveller { TravellerId = 2 } };
            _mockTravellerRepository.Setup(repo => repo.LoadAll()).Returns(travellers);

            // Act
            var result = _travellerService.LoadAll();

            // Assert
            Assert.Equal(travellers, result);
        }

        [Fact]
        public void Add_ShouldAddTraveller()
        {
            // Arrange
            var traveller = new Traveller { TravellerId = 1 };
            _mockTravellerRepository.Setup(repo => repo.Add(traveller)).Returns(true);

            // Act
            var result = _travellerService.Add(traveller);

            // Assert
            Assert.True(result);
            _mockTravellerRepository.Verify(repo => repo.Add(traveller), Times.Once);
        }

        [Fact]
        public void AddTravellerToTrip_ValidatesDatesAndAddsTraveller()
        {
            // Arrange
            var traveller = new Traveller { TravellerId = 1 };
            var trip = new Trip { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10), Travellers = new List<Traveller>() };

            _mockTripService.Setup(svc => svc.ValidateDates(trip.StartDate, trip.EndDate)).Returns(true);
            _mockTripService.Setup(svc => svc.Update(trip)).Returns(true);

            // Act
            var result = _travellerService.AddTravellerToTrip(traveller, trip);

            // Assert
            Assert.True(result);
            Assert.Contains(traveller, trip.Travellers);
            _mockTripService.Verify(svc => svc.Update(trip), Times.Once);
        }

        [Fact]
        public void Update_UpdatesTravellerIfEmailIsUnique()
        {
            // Arrange
            var traveller = new Traveller { TravellerId = 1, Email = "unique@example.com" };
            _mockTravellerRepository.Setup(repo => repo.FindTravellerByEmail(traveller.Email)).Returns((Traveller)null);
            _mockTravellerRepository.Setup(repo => repo.Update(traveller)).Returns(true);

            // Act
            var result = _travellerService.Update(traveller);

            // Assert
            Assert.True(result);
            _mockTravellerRepository.Verify(repo => repo.Update(traveller), Times.Once);
        }

        [Fact]
        public void DeleteTravellerFromTrip_RemovesTravellerIfDatesValid()
        {
            // Arrange
            var traveller = new Traveller { TravellerId = 1 };
            var trip = new Trip { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10), Travellers = new List<Traveller> { traveller } };

            _mockTripService.Setup(svc => svc.ValidateDates(trip.StartDate, trip.EndDate)).Returns(true);
            _mockTripService.Setup(svc => svc.Update(trip)).Returns(true);

            // Act
            var result = _travellerService.DeleteTravellerFromTrip(trip, traveller);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(traveller, trip.Travellers);
            _mockTripService.Verify(svc => svc.Update(trip), Times.Once);
        }

        [Fact]
        public void Delete_DeletesTravellerIfNoReviewsOrTrips()
        {
            // Arrange
            var traveller = new Traveller { TravellerId = 1 };

            _mockReviewService.Setup(svc => svc.TravellerHasReviews(traveller)).Returns(false);
            _mockTripService.Setup(svc => svc.TravellerHasTrips(traveller)).Returns(false);
            _mockTravellerRepository.Setup(repo => repo.Delete(traveller)).Returns(true);

            // Act
            var result = _travellerService.Delete(traveller);

            // Assert
            Assert.True(result);
            _mockTravellerRepository.Verify(repo => repo.Delete(traveller), Times.Once);
        }
    }
}
