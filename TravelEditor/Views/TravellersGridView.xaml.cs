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
    /// Interaction logic for TravellersGridView.xaml
    /// </summary>
    public partial class TravellersGridView : Window
    {
        public TravellersGridView(Trip trip, ITravellerService travellerService)
        {
            InitializeComponent();
            TravellersGridViewModel travellersGridViewModel = new TravellersGridViewModel(trip, travellerService);
            this.DataContext = travellersGridViewModel;
        }
    }
}
