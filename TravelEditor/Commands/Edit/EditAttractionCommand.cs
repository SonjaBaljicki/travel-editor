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
    public class EditAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;

        public EditAttractionCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedAttraction))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedAttraction != null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedAttraction != null)
            {
                AttractionView attractionView = new AttractionView(viewModel.SelectedAttraction,null);
                attractionView.Show();
            }
        }
    }
}
