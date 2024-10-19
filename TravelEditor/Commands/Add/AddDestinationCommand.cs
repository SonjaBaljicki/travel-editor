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
    public class AddDestinationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public IDestinationService destinationService;



        public AddDestinationCommand(MainViewModel viewModel, IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            DestinationView destinationView = new DestinationView(new Destination(),destinationService);
            destinationView.Show();
        }
    }
}
