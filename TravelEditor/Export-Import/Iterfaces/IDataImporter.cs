using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Export_Import.Iterfaces
{
    public interface IDataImporter
    {
        void Import(string filePath);
    }
}
