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
    public class EditTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel;
        public TravellersGridViewModel travellersViewModel;
        public ITravellerService travellerService;

        public EditTravellerCommand(MainViewModel viewModel, ITravellerService travellerService)
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
        public EditTravellerCommand(TravellersGridViewModel viewModel,ITravellerService travellerService)
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
            return (mainViewModel!=null && mainViewModel.SelectedTraveller != null)
                || (travellersViewModel != null && travellersViewModel.SelectedTraveller != null);
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel != null && mainViewModel.SelectedTraveller != null)
            {
                TravellerView travellerView = new TravellerView(mainViewModel.SelectedTraveller, travellerService);
                travellerView.Show();
            }
            else if (travellersViewModel != null && travellersViewModel.SelectedTraveller != null)
            {
                TravellerView travellerView = new TravellerView(travellersViewModel.SelectedTraveller, travellerService);
                travellerView.Show();
            }
        }
    }
}
