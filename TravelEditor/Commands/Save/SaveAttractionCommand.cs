using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Save
{
    public class SaveAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public AttractionViewModel viewModel;

        public SaveAttractionCommand(AttractionViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.Attraction.AttractionId == 0)
            {
                string name = viewModel.Attraction.Name;
                string description = viewModel.Attraction.Description;
                double price = viewModel.Attraction.Price;
                string location = viewModel.Attraction.Location;
                Attraction attraction=new Attraction(name, description, price, location);
                viewModel.DestinationViewModel.Destination.Attractions.Add(attraction);

                MessageBox.Show("Saving add");
            }
            else
            {
                MessageBox.Show("Saving edit");
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
