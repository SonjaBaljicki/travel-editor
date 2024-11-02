using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Export.Iterfaces
{
    public interface IExportService
    {
        DataTable GetAsDataTable<T>() where T : class;

    }
}
