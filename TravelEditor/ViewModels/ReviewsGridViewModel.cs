using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class ReviewsGridViewModel
    {
        public ObservableCollection<Review> Reviews { get; set; }

        public ReviewsGridViewModel(List<Review> reviews)
        {
            Reviews = new ObservableCollection<Review>(reviews);
        }
    }
}
