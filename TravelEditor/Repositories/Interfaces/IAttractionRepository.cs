using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Repositories.Interfaces
{
    public interface IAttractionRepository
    {
        List<Attraction> LoadAll();
        void UpdateAttraction(Attraction attraction);
        void DeleteAttraction(Attraction attraction);
        bool FindOne(Attraction attraction);
    }
}
