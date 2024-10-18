using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class ReviewViewModel
    {
        public Review Review { get; set; }
        public SaveReviewCommand SaveReviewCommand { get; }


        public ReviewViewModel(Review review)
        {
            Review = review;
            SaveReviewCommand = new SaveReviewCommand(this);
        }
    }
}
