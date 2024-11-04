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
    public class SearchAttractionsCommand : ICommand
    {
        private MainViewModel viewModel;
        private IAttractionService attractionService;

        public SearchAttractionsCommand(MainViewModel viewModel, IAttractionService attractionService)
        {
            this.viewModel = viewModel;
            this.attractionService = attractionService;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            List<Attraction> attractions = attractionService.FindAttractions(viewModel.SearchAttractionsText);
       
            viewModel.Attractions.Clear();
            foreach (var attraction in attractions)
            {
                viewModel.Attractions.Add(attraction);
            }

        }
    }
}
