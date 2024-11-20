using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Views
{
    /// <summary>
    /// Interaction logic for ReviewsGridView.xaml
    /// </summary>
    public partial class ReviewsGridView : Window
    {
        public ReviewsGridView(Trip trip, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            InitializeComponent();
            ReviewsGridViewModel reviewsGridViewModel = new ReviewsGridViewModel(trip, reviewService, travellerService, tripService);
            this.DataContext = reviewsGridViewModel;
        }
    }
}
