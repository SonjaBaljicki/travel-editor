using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Delete
{
    public class DeleteAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel;
        public AttractionsGridViewModel attractionsGridViewModel;
        public IAttractionService attractionService;

        public DeleteAttractionCommand(MainViewModel viewModel, IAttractionService attractionService)
        {
            this.mainViewModel = viewModel;
            this.attractionService = attractionService;
            this.mainViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedAttraction))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public DeleteAttractionCommand(AttractionsGridViewModel viewModel, IAttractionService attractionService)
        {
            this.attractionsGridViewModel = viewModel;
            this.attractionService = attractionService;
            this.attractionsGridViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedAttraction))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            if (mainViewModel != null && mainViewModel.SelectedAttraction != null)
            {
                return true;
            }
            else if (attractionsGridViewModel != null && attractionsGridViewModel.SelectedAttraction != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {

            if (mainViewModel != null && mainViewModel.SelectedAttraction!=null)
            {
                Attraction attraction = mainViewModel.SelectedAttraction;
                bool success = attractionService.Delete(attraction);
                if (success)
                {
                    Messenger.NotifyDataChanged();
                    //mainViewModel.Attractions.Remove(attraction);
                }
            }
            else if (attractionsGridViewModel != null && attractionsGridViewModel.SelectedAttraction!=null)
            {
                Attraction attraction = attractionsGridViewModel.SelectedAttraction;
                bool success=attractionService.Delete(attraction);
                if (success)
                {
                    Messenger.NotifyDataChanged();
                    //attractionsGridViewModel.Attractions.Remove(attraction);
                }
            }
        }
    }
}
