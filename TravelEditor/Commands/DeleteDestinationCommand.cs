using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands
{
    internal class DeleteDestinationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel viewModel;

        public DeleteDestinationCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
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
            Destination destination = (Destination)viewModel.SelectedDestination;
            if (destination != null)
            {
                MessageBox.Show(destination.City);
            }
            else
            {
                MessageBox.Show("Please select the destination");
            }
        }
    }

}
