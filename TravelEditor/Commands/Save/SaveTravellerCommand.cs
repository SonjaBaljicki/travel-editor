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
    public class SaveTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public TravellerViewModel viewModel;
        public ITravellerService travellerService;

        public SaveTravellerCommand(TravellerViewModel viewModel, ITravellerService travellerService)
        {
            this.viewModel = viewModel;
            this.travellerService = travellerService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.Traveller.TravellerId == 0)
            {
                bool success=travellerService.Add(viewModel.Traveller);
                if (success)
                {
                    Messenger.NotifyDataChanged();
                    MessageBox.Show("Saving add");
                }
            }
            else
            {
                bool success=travellerService.Update(viewModel.Traveller);
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
