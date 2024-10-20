using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.View
{
    public class ViewAttractionsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public IDestinationService destinationService;
        public IAttractionService attractionService;

        public ViewAttractionsCommand(MainViewModel viewModel, IDestinationService destinationService, IAttractionService attractionService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
            this.attractionService = attractionService;
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
            if (viewModel.SelectedDestination != null)
            {
                AttractionsGridView attractionsGridView = new AttractionsGridView(viewModel.SelectedDestination.Attractions, destinationService, attractionService);
                attractionsGridView.Show();
            }
        }
    }
}
