using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Export.Iterfaces
{
    public interface IDataTableService
    {
        DataTable GetAsDataTable<T>() where T : class;
        List<Attraction> GetAttractions(DataTable? dataTable, Type type);
        List<Review> GetReviews(DataTable? dataTable, Type type);
        public void ImportTravellers(DataTable table, Type entityType);
        void ImportDestinations(DataTable? dataTable, Type type, List<Attraction> attractions);
        void ImportTrips(DataTable? dataTable, Type type, List<Review> reviews);
    }
}
