using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Add
{
    public class AddAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel { get; }
        public AttractionsGridViewModel attractionsGridViewModel { get; }
        public DestinationViewModel destinationViewModel { get; }

        public IDestinationService destinationService;
        public IAttractionService attractionService;

        //when adding an attraction separately
        public AddAttractionCommand(MainViewModel viewModel, IDestinationService destinationService, IAttractionService attractionService)
        {
            this.mainViewModel = viewModel;
            this.destinationService = destinationService;
            this.attractionService = attractionService;
        }
        //adding attraction when creating a destination, doesnt need services
        public AddAttractionCommand(DestinationViewModel destinationViewModel)
        {
            this.destinationViewModel = destinationViewModel;
        }
        //adding from attractions grid for a certain destination
        public AddAttractionCommand(AttractionsGridViewModel viewModel, IDestinationService destinationService, IAttractionService attractionService)
        {
            this.attractionsGridViewModel = viewModel;
            this.destinationService = destinationService;
            this.attractionService = attractionService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //adding when creating destination
            if (destinationViewModel != null)
            {
                AttractionView attractionView = new AttractionView(new Attraction(), destinationViewModel.Destination, destinationService, attractionService);
                attractionView.Show();
            }
            //adding separately in main view
            if (mainViewModel != null)
            {
                AttractionView attractionView = new AttractionView(new Attraction(), destinationService, attractionService);
                attractionView.Show();
            }
            //adding in destinations attraction grid view
            else if(attractionsGridViewModel != null)
            {
                AttractionView attractionView = new AttractionView(new Attraction(), attractionsGridViewModel.Destination, destinationService, attractionService);
                attractionView.Show();
            }
  
        }
    }
}
