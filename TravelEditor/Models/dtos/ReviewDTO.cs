using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models.dtos
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}
