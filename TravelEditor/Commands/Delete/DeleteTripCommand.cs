using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Delete
{
    internal class DeleteTripCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel viewModel;

        public DeleteTripCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedTrip))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedTrip != null;
        }

        public void Execute(object? parameter)
        {
            Trip trip = viewModel.SelectedTrip;
            if (trip != null)
            {
                MessageBox.Show(trip.Name);
            }
            else
            {
                MessageBox.Show("Please select the trip");
            }
        }
    }
}
