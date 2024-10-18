using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Delete
{
    internal class DeleteReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public MainViewModel viewModel;

        public DeleteReviewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedReview))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedReview != null;
        }

        public void Execute(object? parameter)
        {
            Review review = viewModel.SelectedReview;
            if (review != null)
            {
                MessageBox.Show(review.Comment);
            }
            else
            {
                MessageBox.Show("Please select the review");
            }
        }
    }

}
