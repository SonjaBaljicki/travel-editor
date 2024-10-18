using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Add
{
    internal class AddAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel { get; }

        public AddAttractionCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            AttractionView attractionView = new AttractionView(null);
            attractionView.Show();
        }
    }
}
