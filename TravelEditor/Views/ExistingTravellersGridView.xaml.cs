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
    /// Interaction logic for ExistingTravellersGridView.xaml
    /// </summary>
    public partial class ExistingTravellersGridView : Window
    {
        public ExistingTravellersGridView(Trip trip,ITravellerService travellerService)
        {
            InitializeComponent();
            ExistingTravellersGridViewModel travellersGridViewModel = new ExistingTravellersGridViewModel(trip,travellerService);
            this.DataContext = travellersGridViewModel;
        }
    }
}
