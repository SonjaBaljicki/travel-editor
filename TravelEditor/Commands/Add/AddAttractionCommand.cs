using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Add
{
    public class AddAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel { get; }
        public DestinationViewModel destinationViewModel { get; }

        //when adding an attraction separately --TODO
        public AddAttractionCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        //adding attraction when creating a destination
        public AddAttractionCommand(DestinationViewModel destinationViewModel)
        {
            this.destinationViewModel = destinationViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            AttractionView attractionView = new AttractionView(new Attraction(),destinationViewModel);
            attractionView.Show();
        }
    }
}
