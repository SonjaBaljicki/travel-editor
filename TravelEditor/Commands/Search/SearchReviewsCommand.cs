using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Search
{
    public class SearchReviewsCommand : ICommand
    {
        private MainViewModel viewModel;
        private IReviewService reviewService;

        public SearchReviewsCommand(MainViewModel viewModel, IReviewService reviewService)
        {
            this.viewModel = viewModel;
            this.reviewService = reviewService;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            List<Review> reviews = reviewService.FindReviews(viewModel.SearchReviewsText);

            viewModel.Reviews.Clear();
            foreach (var review in reviews)
            {
                viewModel.Reviews.Add(review);
            }

        }

    }
}
