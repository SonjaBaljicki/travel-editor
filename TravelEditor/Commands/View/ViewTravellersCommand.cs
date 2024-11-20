using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.View
{
    public class ViewTravellersCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel mainViewModel;
        public TravellersGridViewModel travellersGridViewModel;
        public ITravellerService travellerService;
        public ViewTravellersCommand(MainViewModel viewModel, ITravellerService travellerService)
        {
            mainViewModel = viewModel;
            this.travellerService = travellerService;
            mainViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedTrip))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public ViewTravellersCommand(TravellersGridViewModel viewModel, ITravellerService travellerService)
        {
            travellersGridViewModel = viewModel;
            this.travellerService = travellerService;
        }
        public bool CanExecute(object? parameter)
        {
            return (mainViewModel!=null && mainViewModel.SelectedTrip != null) || travellersGridViewModel!=null;
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel!=null && mainViewModel.SelectedTrip != null)
            {
                TravellersGridView travellersGridView = new TravellersGridView(mainViewModel.SelectedTrip, travellerService);
                travellersGridView.Show();
            }else if (travellersGridViewModel != null)
            {
                ExistingTravellersGridView existingTravellersGrid = new ExistingTravellersGridView(travellersGridViewModel.Trip, travellerService);
                existingTravellersGrid.Show();
            }
        }
    }
}
