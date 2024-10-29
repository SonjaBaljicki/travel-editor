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
        void ClearDatabase();
        DataTable GetAsDataTable<T>() where T : class;
        List<T> GetEntities<T>(DataTable? table) where T : class, new();
        void ImportEntities(DataTable table, Type entityType, Dictionary<string, List<object>> relatedEntities = null);
    }
}
