using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Export.Iterfaces
{
    public interface IDataTableService
    {
        DataTable GetAsDataTable<T>() where T : class;
    }
}
