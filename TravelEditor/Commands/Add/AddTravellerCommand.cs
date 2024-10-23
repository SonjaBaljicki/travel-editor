using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Add
{
    public class AddTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public ITravellerService travellerService;

        public AddTravellerCommand(MainViewModel viewModel, ITravellerService travellerService)
        {
            this.viewModel = viewModel;
            this.travellerService = travellerService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            TravellerView travellerView = new TravellerView(new Traveller(),travellerService);
            travellerView.Show();
        }
    }
}
