using System;
using System.Collections.Generic;
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
            Attraction attraction=null;

            if (mainViewModel != null)
            {
                attraction = mainViewModel.SelectedAttraction;
            }
            else if (attractionsGridViewModel != null)
            {
                attraction = attractionsGridViewModel.SelectedAttraction;
            }

            if (attraction != null)
            {
                attractionService.DeleteAttraction(attraction);
                MessageBox.Show("Deleted attraction");
            }
            else
            {
                MessageBox.Show("Please select the attraction");
            }
        }
    }
}
