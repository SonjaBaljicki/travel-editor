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
    /// Interaction logic for AttractionsGridView.xaml
    /// </summary>
    public partial class AttractionsGridView : Window
    {
        public AttractionsGridView(Destination destination,IDestinationService destinationService
            , IAttractionService attractionService)
        {
            InitializeComponent();
            AttractionsGridViewModel attractionsGridViewModel = new AttractionsGridViewModel(destination,destinationService,attractionService);
            this.DataContext = attractionsGridViewModel;
        }
    }
}
