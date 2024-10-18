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
    internal class DeleteTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel viewModel;

        public DeleteTravellerCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedTraveller))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedTraveller != null;
        }

        public void Execute(object? parameter)
        {
            Traveller traveler = (Traveller)viewModel.SelectedTraveller;
            if (traveler != null)
            {
                MessageBox.Show(traveler.FirstName);
            }
            else
            {
                MessageBox.Show("Please select the traveler");
            }
        }
    }

}
