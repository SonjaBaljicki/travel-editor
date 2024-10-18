using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands
{
    internal class SaveTravellerCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public TravellerViewModel viewModel;

        public SaveTravellerCommand(TravellerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.Traveller != null)
            {
                MessageBox.Show("Saving edit");
            }
            else
            {
                MessageBox.Show("Saving add");
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
