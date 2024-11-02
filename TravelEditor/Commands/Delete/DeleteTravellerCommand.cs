using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.ViewModels;
using TravelEditor.Services.Interfaces;
using TravelEditor.Views;

namespace TravelEditor.Commands.Delete
{
    public class DeleteTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel;
        public TravellersGridViewModel travellersViewModel;
        public ITravellerService travellerService;

        public DeleteTravellerCommand(MainViewModel viewModel, ITravellerService travellerService)
        {
            mainViewModel = viewModel;
            this.travellerService = travellerService;
            mainViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedTraveller))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public DeleteTravellerCommand(TravellersGridViewModel viewModel, ITravellerService travellerService)
        {
            travellersViewModel = viewModel;
            this.travellerService = travellerService;
            travellersViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TravellersGridViewModel.SelectedTraveller))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return (mainViewModel != null && mainViewModel.SelectedTraveller != null)
                || (travellersViewModel != null && travellersViewModel.SelectedTraveller != null);
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel != null && mainViewModel.SelectedTraveller != null)
            {
                bool success = travellerService.Delete(mainViewModel.SelectedTraveller);
                if (success)
                {
                    mainViewModel.Travellers.Remove(mainViewModel.SelectedTraveller);
                }
                MessageBox.Show("Delete successful");

            }
            else if (travellersViewModel != null && travellersViewModel.SelectedTraveller != null)
            {
                bool success = travellerService.DeleteTravellerFromTrip(travellersViewModel.Trip, travellersViewModel.SelectedTraveller);
                if (success)
                {
                    travellersViewModel.Travellers.Remove(travellersViewModel.SelectedTraveller);
                }
                MessageBox.Show("Delete successful");
            }
        }
    }

}
