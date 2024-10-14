using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    internal class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        public int TravellerId { get; set; }
        public virtual Traveller Traveller { get; set; }

        public Review() { }

        public Review(int reviewId, string comment, int rating, DateTime date, int travellerId, Traveller traveller)
        {
            ReviewId = reviewId;
            Comment = comment;
            Rating = rating;
            Date = date;
            TravellerId = travellerId;
            Traveller = traveller;
        }
    }
}
