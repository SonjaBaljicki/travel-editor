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
        bool Update(Attraction attraction);
        bool Delete(Attraction attraction);
        bool FindOne(Attraction attraction);
        List<Attraction> FindAttractions(string searchAttractionsText);
    }
}
