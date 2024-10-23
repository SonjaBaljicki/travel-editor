using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Save
{
    public class SaveDestinationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public DestinationViewModel viewModel;
        public IDestinationService destinationService;

        public SaveDestinationCommand(DestinationViewModel viewModel, IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.destinationService = destinationService;
            if (viewModel.Destination.Attractions == null)
            {
                viewModel.Destination.Attractions = new List<Attraction>();
            }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }
 
        public void Execute(object? parameter)
        {
            if (viewModel.Destination.DestinationId==0)
            {
                string city = viewModel.Destination.City;
                string country = viewModel.Destination.Country;
                string description = viewModel.Destination.Description;
                string climate = viewModel.Destination.Climate;

                Destination destination = new Destination(city, country, description, climate, viewModel.Destination.Attractions);
                bool success=destinationService.Add(destination);
                if (success)
                {
                    Messenger.NotifyDataChanged();
                    MessageBox.Show("Saving add");
                }

            }
            else
            {
                bool success=destinationService.Update(viewModel.Destination);
                if (success)
                {
                    Messenger.NotifyDataChanged();
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
