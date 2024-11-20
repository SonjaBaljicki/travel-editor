using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Add
{
    public class AddTravellerToTripCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public ExistingTravellersGridViewModel viewModel;
        public ITravellerService travellerService;

        public AddTravellerToTripCommand(ExistingTravellersGridViewModel viewModel, ITravellerService travellerService)
        {
            this.viewModel = viewModel;
            this.travellerService = travellerService;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ExistingTravellersGridViewModel.SelectedTraveller))
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
            if (viewModel.SelectedTraveller != null)
            {
                MessageBox.Show("Adding traveller");
                travellerService.AddTravellerToTrip(viewModel.SelectedTraveller, viewModel.Trip);
            }
        }
    }
}
