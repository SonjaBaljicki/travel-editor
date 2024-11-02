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
    public class ExportServiceTest
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly ExportService _exportService;

        public ExportServiceTest()
        {
            _mockContext = new Mock<DatabaseContext>();
            _exportService = new ExportService(_mockContext.Object);
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
            var result = _exportService.GetAsDataTable<Traveller>();

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
            _exportService.AddColumns(dataTable, properties);

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
            _exportService.PopulateRow(row, entity, properties);

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
            var result = _exportService.SerializeTravellers(travellersList);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Mika", result);
            Assert.Contains("Zika", result);
        }

    }
}
