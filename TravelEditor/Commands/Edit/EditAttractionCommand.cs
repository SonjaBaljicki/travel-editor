using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Edit
{
    public class EditAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel;
        public AttractionsGridViewModel attractionsGridViewModel;
        public IDestinationService destinationService;
        public IAttractionService attractionService;

        public EditAttractionCommand(MainViewModel viewModel, IDestinationService destinationService, IAttractionService attractionService)
        {
            this.mainViewModel = viewModel;
            this.destinationService = destinationService;
            this.attractionService = attractionService;
            this.mainViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedAttraction))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public EditAttractionCommand(AttractionsGridViewModel viewModel, IDestinationService destinationService, IAttractionService attractionService)
        {
            this.attractionsGridViewModel = viewModel;
            this.destinationService = destinationService;
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
            if(mainViewModel!=null && mainViewModel.SelectedAttraction != null)
            {
                return true;
            }
            else if(attractionsGridViewModel!=null && attractionsGridViewModel.SelectedAttraction != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel!=null && mainViewModel.SelectedAttraction != null)
            {
                AttractionView attractionView = new AttractionView(mainViewModel.SelectedAttraction,destinationService,attractionService);
                attractionView.Show();
            }else if(attractionsGridViewModel!=null && attractionsGridViewModel.SelectedAttraction != null)
            {
                AttractionView attractionView = new AttractionView(attractionsGridViewModel.SelectedAttraction, destinationService, attractionService);
                attractionView.Show();
            }
        }
    }
}
