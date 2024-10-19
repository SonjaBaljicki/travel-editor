using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.View
{
    public class ViewTravellersCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel viewModel;
        public ViewTravellersCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
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
            return viewModel.SelectedTrip != null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedTrip != null)
            {
                TravellersGridView travellersGridView = new TravellersGridView(viewModel.SelectedTrip.Travellers);
                travellersGridView.Show();
            }
        }
    }
}
