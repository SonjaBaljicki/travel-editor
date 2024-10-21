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
            //add
            if (viewModel.Attraction.AttractionId == 0)
            {
                string name = viewModel.Attraction.Name;
                string description = viewModel.Attraction.Description;
                double price = viewModel.Attraction.Price;
                string location = viewModel.Attraction.Location;
                Attraction attraction=new Attraction(name, description, price, location);
              
                if (viewModel.SelectedDestination != null)
                {
                    destinationService.AddDestinationAttractions(viewModel.SelectedDestination, attraction);
                    MessageBox.Show("Saving add");
                }
                else
                {
                    MessageBox.Show("Please select a destination");
                }
            }
            //edit
            else
            {
                if (viewModel.SelectedDestination != null)
                {
                    attractionService.UpdateAttraction(viewModel.Attraction, viewModel.SelectedDestination);
                    MessageBox.Show("Saving edit");
                }
          
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
