using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.ViewModels;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Commands.Delete
{
    public class DeleteDestinationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel viewModel;
        public IDestinationService destinationService;

        public DeleteDestinationCommand(MainViewModel viewModel, IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedDestination))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedDestination != null;
        }

        public void Execute(object? parameter)
        {
            Destination destination = viewModel.SelectedDestination;
            if (destination != null)
            {
                MessageBox.Show("Deleting destination");
                destinationService.Delete(destination);
            }
            else
            {
                MessageBox.Show("Please select the destination");
            }
        }
    }

}
