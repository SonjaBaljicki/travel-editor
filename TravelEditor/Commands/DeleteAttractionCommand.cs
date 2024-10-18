using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands
{
    internal class DeleteAttractionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;

        public DeleteAttractionCommand(MainViewModel viewModel)
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
            return viewModel.SelectedAttraction!=null;
        }

        public void Execute(object? parameter)
        {
            Attraction attraction = (Attraction)viewModel.SelectedAttraction;
            if(attraction != null)
            {
                MessageBox.Show(attraction.Name);

            }
            else
            {
                MessageBox.Show("Please select the attraction");
            }
        }
    }
}
