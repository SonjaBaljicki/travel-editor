using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Export_Import.Iterfaces
{
    public interface IImportService
    {
        List<T> GetEntities<T>(DataTable? table) where T : class, new();
        void ImportEntities<T>(DataTable table, Dictionary<string, List<object>> relatedEntities = null) where T : class;
        void ValidateReviews();
    }
}
