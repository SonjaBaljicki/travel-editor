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
        List<T> GetEntities<T>(DataTable? table) where T : class, new();
        void ImportEntities<T>(DataTable table, Dictionary<string, List<object>> relatedEntities = null) where T : class;
        void ValidateReviews();

    }
}
