using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Edit
{
    public class EditTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;

        public EditTravellerCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedTraveller))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedTraveller != null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedTraveller != null)
            {
                TravellerView travellerView = new TravellerView(viewModel.SelectedTraveller);
                travellerView.Show();
            }
        }
    }
}
