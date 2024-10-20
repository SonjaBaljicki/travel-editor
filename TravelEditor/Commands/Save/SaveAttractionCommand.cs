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
        public IAttractionService attractionService;

        public SaveAttractionCommand(AttractionViewModel viewModel,IDestinationService destinationService, IAttractionService attractionService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
            this.attractionService = attractionService;
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
                //adding attractions when adding a new destination
                if (viewModel.Destination != null && viewModel.Destination.DestinationId==0)
                {
                    viewModel.Destination.Attractions.Add(attraction);
                    //saves in destination window
                }
                //adding from destination attractions grid view
                else if (viewModel.Destination != null)
                {
                    destinationService.AddDestinationAttractions(viewModel.Destination, attraction);
                }
                //opened separately, not from destination
                else
                {
                    destinationService.AddDestinationAttractions((Destination)viewModel.SelectedDestination, attraction);
                }
                MessageBox.Show("Saving add");
            }
            else
            {
                attractionService.UpdateAttraction(viewModel.Attraction);
                MessageBox.Show("Saving edit");
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
