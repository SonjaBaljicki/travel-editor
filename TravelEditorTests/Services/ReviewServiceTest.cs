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
    public class ReviewServiceTest
    {
        private readonly ReviewService _reviewService;
        private readonly Mock<IReviewRepository> _mockReviewRepository;
        private readonly Mock<ITripService> _mockTripService;

        public ReviewServiceTest()
        {
            _mockReviewRepository = new Mock<IReviewRepository>();
            _mockTripService = new Mock<ITripService>();
            _reviewService = new ReviewService(_mockReviewRepository.Object, _mockTripService.Object);
        }

        [Fact]
        public void LoadAll_ShouldReturnAllReviews()
        {
            // Arrange
            var reviews = new List<Review>
        {
            new Review { ReviewId = 1 },
            new Review { ReviewId = 2 }
        };
            _mockReviewRepository.Setup(repo => repo.LoadAll()).Returns(reviews);

            // Act
            var result = _reviewService.LoadAll();

            // Assert
            Assert.Equal(reviews, result);
        }

        [Fact]
        public void TravellerHasReviews_ShouldReturnTrueIfTravellerHasReviews()
        {
            // Arrange
            var traveller = new Traveller { TravellerId = 1 };
            _mockReviewRepository.Setup(repo => repo.TravellerHasReviews(traveller)).Returns(true);

            // Act
            var result = _reviewService.TravellerHasReviews(traveller);

            // Assert
            Assert.True(result);
            _mockReviewRepository.Verify(repo => repo.TravellerHasReviews(traveller), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateReviewIfTravellerWasOnTrip()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(-4), EndDate = DateTime.Now.AddDays(-2), Reviews = new List<Review>(), Travellers=new List<Traveller>() };
            var traveller = new Traveller { TravellerId = 1 };
            var review = new Review { ReviewId = 1, Traveller = traveller };

            trip.Travellers.Add(traveller);
            trip.Reviews.Add(review);
            _mockReviewRepository.Setup(repo => repo.Update(review)).Returns(true);

            // Act
            var result = _reviewService.Update(trip, review);

            // Assert
            Assert.True(result);
            _mockReviewRepository.Verify(repo => repo.Update(review), Times.Once);
        }

        [Fact]
        public void Update_ShouldDeleteAndAddTripReviewIfTripChanged()
        {
            // Arrange
            var trip = new Trip { StartDate = DateTime.Now.AddDays(-4), EndDate = DateTime.Now.AddDays(-2), Reviews = new List<Review>(), Travellers = new List<Traveller>() };
            var traveller = new Traveller { TravellerId = 1 };
            var review = new Review { ReviewId = 1, Traveller = traveller };

            trip.Travellers.Add(traveller);
            _mockReviewRepository.Setup(repo => repo.Update(review)).Returns(true);
            _mockTripService.Setup(svc => svc.HasTripHappened(trip.StartDate, trip.EndDate)).Returns(true);
            _mockReviewRepository.Setup(repo => repo.Delete(review)).Returns(true);
            _mockTripService.Setup(svc => svc.AddTripReview(trip, review)).Returns(true);

            // Act
            var result = _reviewService.Update(trip, review);

            // Assert
            Assert.True(result);
            _mockReviewRepository.Verify(repo => repo.Delete(review), Times.Once);
            _mockTripService.Verify(svc => svc.AddTripReview(trip, review), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallDeleteOnRepository()
        {
            // Arrange
            var review = new Review { ReviewId = 1 };
            _mockReviewRepository.Setup(repo => repo.Delete(review)).Returns(true);

            // Act
            var result = _reviewService.Delete(review);

            // Assert
            Assert.True(result);
            _mockReviewRepository.Verify(repo => repo.Delete(review), Times.Once);
        }
    }
}
