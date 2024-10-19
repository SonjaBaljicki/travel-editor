using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    public class ReviewViewModel
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
