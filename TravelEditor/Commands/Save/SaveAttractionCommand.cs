using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Save
{
    public class SaveAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public AttractionViewModel viewModel;
        public IDestinationService destinationService;

        public SaveAttractionCommand(AttractionViewModel viewModel,IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
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
                //opened separately, not from destination
                if (viewModel.DestinationViewModel == null)
                {
                    destinationService.UpdateDestinationAttractions((Destination)viewModel.SelectedDestination,attraction);
                }
                //adding attractions when adding a new destination
                else
                {
                    viewModel.DestinationViewModel.Destination.Attractions.Add(attraction);

                }

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
