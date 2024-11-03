using Spire.Xls;
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

namespace TravelEditor.Commands.Search
{
    public class SearchDestinationsCommand : ICommand
    {
        private MainViewModel viewModel;
        private IDestinationService destinationService;

        public SearchDestinationsCommand(MainViewModel viewModel, IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            List<Destination> destinations = destinationService.FindDestinations(viewModel.SearchDestinationsText);
          
            viewModel.Destinations.Clear();
            foreach (var destination in destinations)
            {
                viewModel.Destinations.Add(destination);
            }
        }
    }
}
