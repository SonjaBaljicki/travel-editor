using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    internal class Destination
    {
        public int DestinationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Climate { get; set; }
        public List<Attraction> Attractions { get; set; } = new List<Attraction>();

        public Destination() { }

        public Destination(int destinationId, string city, string country, string description, string climate, List<Attraction> attractions)
        {
            DestinationId = destinationId;
            City = city;
            Country = country;
            Description = description;
            Climate = climate;
            Attractions = attractions;
        }
    }
}
