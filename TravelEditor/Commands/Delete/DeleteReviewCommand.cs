﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.ViewModels;
using TravelEditor.Services.Interfaces;
using TravelEditor.Services;
using TravelEditor.Views;

namespace TravelEditor.Commands.Delete
{
    public class DeleteReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel;
        public ReviewsGridViewModel reviewsViewModel;
        public IReviewService reviewService;

        public DeleteReviewCommand(MainViewModel viewModel, IReviewService reviewService)
        {
            mainViewModel = viewModel;
            this.reviewService = reviewService;
            mainViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedReview))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public DeleteReviewCommand(ReviewsGridViewModel viewModel, IReviewService reviewService)
        {
            reviewsViewModel = viewModel;
            this.reviewService = reviewService;
            reviewsViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ReviewsGridViewModel.SelectedReview))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public bool CanExecute(object? parameter)
        {
            return (mainViewModel != null && mainViewModel.SelectedReview != null)
               || (reviewsViewModel != null && reviewsViewModel.SelectedReview != null);
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel != null && mainViewModel.SelectedReview != null)
            {
               bool success = reviewService.Delete(mainViewModel.SelectedReview);
                if (success)
                {
                    MessageBox.Show("Delete successful");
                    Messenger.NotifyDataChanged();
                }
            }
            else if (reviewsViewModel != null && reviewsViewModel.SelectedReview != null)
            {
                bool success = reviewService.Delete(reviewsViewModel.SelectedReview);
                if (success)
                {
                    MessageBox.Show("Delete successful");
                    Messenger.NotifyDataChanged();
                }
            }
        }
    }

}
