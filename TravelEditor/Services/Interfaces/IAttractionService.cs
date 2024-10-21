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
        void UpdateAttraction(Attraction attraction, Destination destination);
        void DeleteAttraction(Attraction attraction);


    }
}
