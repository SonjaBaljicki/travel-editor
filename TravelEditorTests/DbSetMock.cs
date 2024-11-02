using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditorTests
{
    public static class DbSetMock
    {
        public static Mock<DbSet<T>> CreateMockSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);

            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids =>
            {
                var keyProperty = typeof(T).GetProperties().FirstOrDefault();
                if (keyProperty == null) return null;

                var id = ids[0];
                return data.SingleOrDefault(d => keyProperty.GetValue(d).Equals(id));
            });

            return mockSet;
        }
    }

}
