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
    public class ViewDestinationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public IDestinationService destinationService;

        public ViewDestinationCommand(MainViewModel viewModel,IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
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
           return viewModel.SelectedTrip!=null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedTrip != null)
            {
                DestinationView destinationView = new DestinationView(viewModel.SelectedTrip.Destination,destinationService);
                destinationView.Show();
            }
        }
    }
}
