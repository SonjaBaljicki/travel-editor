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
    public class ViewTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public ITravellerService travellerService;

        public ViewTravellerCommand(MainViewModel viewModel, ITravellerService travellerService)
        {
            this.viewModel = viewModel;
            this.travellerService = travellerService;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedReview))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedReview != null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedReview != null)
            {
                TravellerView travellerView = new TravellerView(viewModel.SelectedReview.Traveller, travellerService);
                travellerView.Show();
            }
        }
    }
}
