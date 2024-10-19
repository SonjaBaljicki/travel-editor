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
    public class AddReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel { get; }

        public AddReviewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ReviewView reviewView = new ReviewView(null);
            reviewView.Show();
        }
    }
}
