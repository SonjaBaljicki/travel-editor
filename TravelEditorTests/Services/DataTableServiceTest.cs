using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TravelEditor.Database;
using TravelEditor.Export.Service;
using TravelEditor.Models;
using TravelEditor.Models.dtos;
using Xunit;

namespace TravelEditorTests.Services
{
    public class DataTableServiceTest
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly ExportService _dataTableService;

        public DataTableServiceTest()
        {
            _mockContext = new Mock<DatabaseContext>();
            _dataTableService = new ExportService(_mockContext.Object);
        }

        [Fact]
        public void GetAsDataTable_ReturnsDataTable_WithCorrectData()
        {
            // Arrange
            var travellerList = new List<Traveller>
            {
                new Traveller { TravellerId=1, Email = "mika@gmail.com", FirstName = "Mika" },
                new Traveller { TravellerId=2, Email = "zika@gmail.com", FirstName = "Zika" }
            };

            var mockSet = DbSetMock.CreateMockSet(travellerList);
            _mockContext.Setup(c => c.Travellers).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Set<Traveller>()).Returns(mockSet.Object);

            // Act
            var result = _dataTableService.GetAsDataTable<Traveller>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Rows.Count);
            Assert.Equal("Mika", result.Rows[0]["FirstName"]);
            Assert.Equal("Zika", result.Rows[1]["FirstName"]);
        }

        [Fact]
        public void AddColumns_ShouldAddColumnsToDataTable()
        {
            // Arrange
            var dataTable = new DataTable();
            var properties = typeof(TestEntity).GetProperties();

            // Act
            _dataTableService.AddColumns(dataTable, properties);

            // Assert
            Assert.Equal(properties.Length, dataTable.Columns.Count);
            foreach (var property in properties)
            {
                Assert.True(dataTable.Columns.Contains(property.Name));
            }
        }
        [Fact]
        public void PopulateRow_ShouldPopulateDataRowWithValues()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));

            var properties = typeof(TestEntity).GetProperties();
            var row = dataTable.NewRow();
            var entity = new TestEntity { Id = 1, Name = "Name" };

            // Act
            _dataTableService.PopulateRow(row, entity, properties);

            // Assert
            Assert.Equal(1, row["Id"]);
            Assert.Equal("Name", row["Name"]);
        }

        [Fact]
        public void SerializeTravellers_ShouldReturnSerializedString()
        {
            // Arrange
            var travellersList = new List<Traveller>
            {
                new Traveller { TravellerId = 1, FirstName = "Mika", Email = "mika@gmail.com" },
                new Traveller { TravellerId = 2, FirstName = "Zika", Email = "zika@gmail.com" }
            };

            // Act
            var result = _dataTableService.SerializeTravellers(travellersList);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Mika", result);
            Assert.Contains("Zika", result);
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
            var result = _dataTableService.GetEntities<TestEntity>(dataTable);

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
            var entity = _dataTableService.CreateEntity(dataTable.Rows[0], typeof(TestEntity), properties, null);

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
            var result = _dataTableService.SetTravellerProperty(review, json);

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
            var result = _dataTableService.SetTravellerProperty(review, json);

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
            _dataTableService.SetEntityProperty(entity, entity.GetType().GetProperty(propertyName), value, null);

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
            var result = _dataTableService.SetRelatedEntityList(entity, property, json, null);

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
            var result = _dataTableService.SetRelatedDestinationEntity(entity, property, json);

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
            var result = _dataTableService.IsMatchingEntity(entity, dto);

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
            var result = _dataTableService.IsMatchingEntity(entity, dto);

            // Assert
            Assert.False(result);
        }


    }
}
