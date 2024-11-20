using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;
using TravelEditor.Database;
using TravelEditor.Export.Service;
using TravelEditor.Export_Import.Service;
using TravelEditor.Models;
using Xunit;

namespace TravelEditorTests.Services
{
    public class ImportServiceTest
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly ImportService _importService;


        public ImportServiceTest()
        {
            _mockContext = new Mock<DatabaseContext>();
            _importService = new ImportService(_mockContext.Object);

        }
        [Fact]
        public void GetEntities_ShouldReturnListOfEntities()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            dataTable.Rows.Add(1, "Value1");
            dataTable.Rows.Add(2, "Value2");

            // Act
            var result = _importService.GetEntities<TestEntity>(dataTable);

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Value1", result[0].Name);
            Assert.Equal("Value2", result[1].Name);
        }

        [Fact]
        public void CreateEntity_ShouldReturnValidEntity()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            dataTable.Rows.Add(1, "Value1");

            var properties = typeof(TestEntity).GetProperties()
           .ToDictionary(prop => prop.Name.ToLower(), prop => prop);

            // Act
            var entity = _importService.CreateEntity(dataTable.Rows[0], typeof(TestEntity), properties, null);

            // Assert
            Assert.NotNull(entity);
        }

        [Fact]
        public void SetTravellerProperty_ValidTraveller()
        {
            // Arrange
            var review = new Review();
            var travellerList = new List<Traveller>
            {
                new Traveller { TravellerId=1, Email = "mika@gmail.com", FirstName = "Mika" },
                new Traveller { TravellerId=2, Email = "zika@gmail.com", FirstName = "Zika" }
            };
            var json = "{\"TravellerId\": 1,\"Email\": \"mika@gmail.com\",\"FirstName\": \"Mika\"}";

            var mockSet = DbSetMock.CreateMockSet(travellerList);
            _mockContext.Setup(c => c.Travellers).Returns(mockSet.Object);

            // Act
            var result = _importService.SetTravellerProperty(review, json);

            // Assert
            Assert.True(result);
            Assert.NotNull(review.Traveller);
            Assert.Equal(travellerList[0].TravellerId, review.TravellerId);
            Assert.Equal(travellerList[0].Email, review.Traveller.Email);
        }

        [Fact]
        public void SetTravellerProperty_InvalidTraveller()
        {
            // Arrange
            var review = new Review();
            var travellerList = new List<Traveller>
            {
                new Traveller { TravellerId=1, Email = "mika@gmail.com", FirstName = "Mika" },
                new Traveller { TravellerId=2, Email = "zika@gmail.com", FirstName = "Zika" }
            };
            var json = "{\"TravellerId\":31,\"Email\": \"new@gmail.com\",\"FirstName\": \"New\"}";

            var mockSet = DbSetMock.CreateMockSet(travellerList);
            _mockContext.Setup(c => c.Travellers).Returns(mockSet.Object);

            // Act
            var result = _importService.SetTravellerProperty(review, json);

            // Assert
            Assert.False(result);
            Assert.Null(review.Traveller);
        }

        [Fact]
        public void SetEntityProperty_ShouldSetPropertyCorrectly()
        {
            // Arrange
            var entity = new Trip();
            var propertyName = "Name";
            var value = "Trip1";

            // Act
            _importService.SetEntityProperty(entity, entity.GetType().GetProperty(propertyName), value, null);

            // Assert
            Assert.Equal("Trip1", entity.Name);
        }

        [Fact]
        public void SetRelatedEntityList_ValidTravellerJson_SetsMatchingTravellers()
        {
            // Arrange
            var travellerList = new List<Traveller>
            {
                new Traveller { TravellerId=1, Email = "mika@gmail.com", FirstName = "Mika" }
            };

            var travellersMock = DbSetMock.CreateMockSet(travellerList);
            _mockContext.Setup(c => c.Travellers).Returns(travellersMock.Object);

            var entity = new Trip();
            var property = typeof(Trip).GetProperty("Travellers");
            var json = "[{\"TravellerId\": 1,\"Email\": \"mika@gmail.com\",\"FirstName\": \"Mika\"}]";

            // Act
            var result = _importService.SetRelatedEntityList(entity, property, json, null);

            // Assert
            Assert.True(result);
            var travellers = (List<Traveller>)property.GetValue(entity);
            Assert.Single(travellers);
            Assert.Equal("Mika", travellers[0].FirstName);
        }

        [Fact]
        public void SetRelatedDestinationEntity_ValidDestinationJson_SetsMatchingDestination()
        {
            // Arrange
            var destinationList = new List<Destination>
            {
                new Destination { DestinationId=1, City = "Paris", Country = "France" }
            };

            var destinationsMock = DbSetMock.CreateMockSet(destinationList);
            _mockContext.Setup(c => c.Destinations).Returns(destinationsMock.Object);

            var entity = new Trip();
            var property = typeof(Trip).GetProperty("Destination");
            var json = "{\"DestinationId\":1, \"City\":\"Paris\", \"Country\":\"France\"}";

            // Act
            var result = _importService.SetRelatedDestinationEntity(entity, property, json);

            // Assert
            Assert.True(result);
            var destination = (Destination)property.GetValue(entity);
            Assert.NotNull(destination);
            Assert.Equal("Paris", destination.City);
            Assert.Equal("France", destination.Country);
        }


        [Fact]
        public void IsMatchingEntity_ValidDto_ReturnsTrue()
        {
            // Arrange
            var entity = new Traveller { Email = "mika@gmail.com", FirstName = "Mika" };
            var dto = new Dictionary<string, object>
            {
                { "Email", "mika@gmail.com" },
                { "FirstName", "Mika" }
            };

            // Act
            var result = _importService.IsMatchingEntity(entity, dto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsMatchingEntity_InvalidDto_ReturnsFalse()
        {
            // Arrange
            var entity = new Traveller { Email = "mika@gmail.com", FirstName = "Mika" };
            var dto = new Dictionary<string, object>
            {
                { "Email", "mika@gmail.com" },
                { "FirstName", "Pera" }
            };

            // Act
            var result = _importService.IsMatchingEntity(entity, dto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateEntity_UpdatesStandardProperties()
        {
            // Arrange
            var existingEntity = new TestEntity { Id=1, Name="Old name"};
            var newEntity = new TestEntity { Id=1, Name = "New name"};

            // Act
            _importService.UpdateEntity(existingEntity, newEntity);

            // Assert
            Assert.Equal("New name", existingEntity.Name);
        }

        [Fact]
        public void UpdateEntity_UpdatesDestinationProperty()
        {
            var destinationList = new List<Destination>
            {
                new Destination { DestinationId=1, City = "Old City", Country = "Old Country" }
            };

            var mockSet = DbSetMock.CreateMockSet(destinationList);
            _mockContext.Setup(c => c.Destinations).Returns(mockSet.Object);

            // Arrange
            var existingEntity = new Trip {TripId=1, Destination = new Destination {DestinationId=1, City = "Old City", Country = "Old Country" } };
            var newEntity = new Trip { TripId=1, Destination = new Destination { DestinationId=1, City = "New City", Country = "New Country" } };

            // Act
            _importService.UpdateEntity(existingEntity, newEntity);

            // Assert
            Assert.Equal("New City", existingEntity.Destination.City);
            Assert.Equal("New Country", existingEntity.Destination.Country);
        }

        [Fact]
        public void UpdateEntity_NullExistingEntity_DoesNothing()
        {
            // Arrange
            TestEntity existingEntity = null;
            var newEntity = new TestEntity { Id=1, Name = "Entity1" };

            // Act
            _importService.UpdateEntity(existingEntity, newEntity);

            // Assert
            Assert.Null(existingEntity);
        }

        [Fact]
        public void UpdateRelatedEntity_ShouldUpdateProperties()
        {
            // Arrange
            var existingEntity = new Traveller { TravellerId = 1, FirstName = "Mika" };
            var newEntity = new Traveller { TravellerId = 1, FirstName = "Zika" };

            // Act
            _importService.UpdateRelatedEntity(existingEntity, newEntity);

            // Assert
            Assert.Equal("Zika", existingEntity.FirstName);
        }

        [Fact]
        public void UpdateStandardProperty_ShouldSetNewValue()
        {
            // Arrange
            var existingEntity = new Traveller { TravellerId = 1, FirstName = "Mika" };
            var newEntity = new Traveller { TravellerId = 1, FirstName = "Zika" };
            var propertyInfo = typeof(Traveller).GetProperty(nameof(Traveller.FirstName));

            // Act
            _importService.UpdateStandardProperty(existingEntity, newEntity, propertyInfo);

            // Assert
            Assert.Equal("Zika", existingEntity.FirstName);
        }

        [Fact]
        public void DeserializeValue_ShouldReturnListOfDictionaries()
        {
            // Arrange
            var json = "[{\"Email\":\"test1@example.com\"}, {\"Email\":\"test2@example.com\"}]";

            // Act
            var result = _importService.DeserializeValue(json);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("test1@example.com", result[0]["Email"]);
        }

        [Fact]
        public void SetTravellerList_ShouldSetMatchingTravellers()
        {
            // Arrange
            var entity = new Trip();
            var property = typeof(Trip).GetProperty("Travellers");
            var dtoList = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { { "Email", "mika@gmail.com" } },
                new Dictionary<string, object> { { "Email", "zika@gmail.com" } }
            };

            var travellers = new List<Traveller>
            {
                new Traveller { Email = "mika@gmail.com" },
                new Traveller { Email = "pera@gmail.com" }
            };

            _mockContext.Setup(m => m.Travellers).Returns(DbSetMock.CreateMockSet(travellers).Object);

            // Act
            var result = _importService.SetTravellerList(entity, property, dtoList);

            // Assert
            Assert.True(result);
            var matchedTravellers = (List<Traveller>)property.GetValue(entity);
            Assert.Equal(1, matchedTravellers.Count);
            Assert.Equal("mika@gmail.com", matchedTravellers.First().Email);
        }

        [Fact]
        public void RemoveNotPresentEntities_ShouldRemoveEntitiesNotInNewList()
        {
            // Arrange
            List<Attraction> existingAttractions = new List<Attraction>
            {
                new Attraction { AttractionId = 1, Name = "Attraction 1", Description="opis 1",Price=10, Location="lokacija 1" },
                new Attraction { AttractionId = 2, Name = "Attraction 2",  Description="opis 2",Price=10, Location="lokacija 2" }
            };

            List<Attraction> newAttractions = new List<Attraction>
            {
                new Attraction { AttractionId = 2, Name = "Attraction 2",  Description="opis 2",Price=10, Location="lokacija 2" }
            };

            var mockSet = DbSetMock.CreateMockSet(existingAttractions);

            _mockContext.Setup(m => m.Attractions).Returns(mockSet.Object);

            // Act
            _importService.RemoveNotPresentEntities(existingAttractions, newAttractions);

            // Assert
            Assert.Single(existingAttractions);
            Assert.Equal(2, existingAttractions[0].AttractionId);
        }

        [Fact]
        public void EmptyEntityList_ShouldRemoveAllEntitiesInAttractions()
        {
            // Arrange
            List<Attraction> existingAttractions = new List<Attraction>
            {
                new Attraction { AttractionId = 1, Name = "Attraction 1", Description="opis 1",Price=10, Location="lokacija 1" },
                new Attraction { AttractionId = 2, Name = "Attraction 2",  Description="opis 2",Price=10, Location="lokacija 2" }
            };
            var mockSet = DbSetMock.CreateMockSet(existingAttractions);

            _mockContext.Setup(m => m.Attractions).Returns(mockSet.Object); 

            // Act
            _importService.EmptyEntityList(existingAttractions);

            // Assert
            _mockContext.Verify(m => m.Attractions.RemoveRange(existingAttractions), Times.Once);
            Assert.Empty(existingAttractions);
        }

        [Fact]
        public void EmptyEntityList_ShouldClearTravellerListWithoutRemovingFromDb()
        {
            // Arrange
            var existingTravellers = new List<Traveller>
        {
            new Traveller { TravellerId = 1, FirstName = "Mika" },
            new Traveller { TravellerId = 2, FirstName = "Zika" }
        };

            // Act
            _importService.EmptyEntityList(existingTravellers);

            // Assert
            Assert.Empty(existingTravellers);
            _mockContext.Verify(m => m.Travellers.RemoveRange(It.IsAny<IEnumerable<Traveller>>()), Times.Never);
        }

        [Fact]
        public void EmptyEntityList_ShouldRemoveAllEntitiesInReviews()
        {
            // Arrange
            var existingReviews = new List<Review>
            {
                new Review { ReviewId = 1, Comment = "Great!" },
                new Review { ReviewId = 2, Comment = "Okay." }
            };

            var reviewSet = DbSetMock.CreateMockSet(existingReviews);

            _mockContext.Setup(m => m.Reviews).Returns(reviewSet.Object);

            // Act
            _importService.EmptyEntityList(existingReviews);

            // Assert
            _mockContext.Verify(m => m.Reviews.RemoveRange(existingReviews), Times.Once);
            Assert.Empty(existingReviews);
        }

    }
}
