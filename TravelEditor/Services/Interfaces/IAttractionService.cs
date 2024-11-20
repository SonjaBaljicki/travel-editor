using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface IAttractionService
    {
        List<Attraction> LoadAll();
        bool Update(Attraction attraction, Destination destination);
        bool Delete(Attraction attraction);
        List<Attraction> FindAttractions(string searchAttractionsText);
    }
}
