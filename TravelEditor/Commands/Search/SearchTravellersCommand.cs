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
    public class SearchTravellersCommand : ICommand
    {
        private MainViewModel viewModel;
        private ITravellerService travellerService;

        public SearchTravellersCommand(MainViewModel viewModel, ITravellerService travellerService)
        {
            this.viewModel = viewModel;
            this.travellerService = travellerService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            List<Traveller> travellers = travellerService.FindTravellers(viewModel.SearchTravellersText);
        
            viewModel.Travellers.Clear();
            foreach (var traveller in travellers)
            {
                viewModel.Travellers.Add(traveller);
            }
        }
    }
}
