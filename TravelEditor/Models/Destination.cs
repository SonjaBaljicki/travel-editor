using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    internal class Destination
    {
        [Key]
        public int DestinationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Climate { get; set; }
        public virtual List<Attraction> Attractions { get; set; }

        public Destination() { }

        public Destination(string city, string country, string description, string climate, List<Attraction> attractions)
        {
            City = city;
            Country = country;
            Description = description;
            Climate = climate;
            Attractions = attractions;
        }
    }
}
