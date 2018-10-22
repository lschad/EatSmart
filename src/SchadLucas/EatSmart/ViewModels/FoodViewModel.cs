using SchadLucas.EatSmart.Data.Models;
using SchadLucas.EatSmart.Data.Types.Entities;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SchadLucas.EatSmart.Data.Commands;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class FoodViewModel : ViewModel, IFoodViewModel
    {
        private readonly INavigationService _navigationService;
        private ICommand _addFoodCommand;
        private ObservableCollection<Food> _foods;
        private bool _isLoading;
        private string _newFoodText = string.Empty;
        private ICommand _openFoodCommand;
        private ICommand _searchFoodCommand;
        private string _searchText;
        private Food _selectedFood;

        private IFoodModel Model { get; }
        public ICommand AddFoodCommand
        {
            get
            {
                if (_addFoodCommand == null)
                {
                    _addFoodCommand = new RelayCommand(AddFood, CanExecuteAddFoodCommand);
                }

                return _addFoodCommand;
            }
        }

        public ObservableCollection<Food> Foods
        {
            get => _foods;
            set
            {
                _foods = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        public string NewFoodText
        {
            get => _newFoodText ?? "";
            set => SetField(ref _newFoodText, value);
        }

        public ICommand OpenFoodCommand => _openFoodCommand ?? (_openFoodCommand = new RelayCommand(OpenFood, _ => SelectedFood != null));
        public ICommand SearchFoodCommand => _searchFoodCommand ?? (_searchFoodCommand = new RelayCommand(SearchFood));
        public string SearchText
        {
            get => _searchText;
            set => SetField(ref _searchText, value);
        }

        public Food SelectedFood
        {
            get => _selectedFood;
            set => SetField(ref _selectedFood, value);
        }

        public FoodViewModel(IFoodModel model, INavigationService navigationService)
        {
            _navigationService = navigationService;
            IsLoading = true;
            Model = model;
            Foods = new ObservableCollection<Food>();
            Enabling += async (s, e) => await UpdateFoodsAsync().ConfigureAwait(false);
            Disabled += (s, e) => Foods.Clear();
        }

        private async void AddFood(object value)
        {
            if (value is string text)
            {
                var food = new Food { Name = text };
                Model.Add(food);
                ResetSearch();
                await UpdateFoodsAsync().ConfigureAwait(false);
                SelectedFood = Foods.FirstOrDefault(f => f.Id == food.Id);
            }
        }

        private bool CanExecuteAddFoodCommand(object value)
        {
            if (value is string text)
            {
                return text.Length >= 3 && !Model.Exists(text);
            }

            return false;
        }

        private void OpenFood(object obj)
        {
            if (obj is Food food)
            {
                _navigationService.NavigateTo<IFoodDetailView, IFoodDetailViewModel>(new object[] { food });
            }
        }

        private void ResetInput()
        {
            NewFoodText = "";
        }

        private void ResetSearch()
        {
            SearchText = string.Empty;
            SearchFood(SearchText);
        }

        private async void SearchFood(object value)
        {
            if (value is string text)
            {
                await UpdateFoodsAsync(food => food.Name.Contains(text)).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Sets <see cref="Foods" /> to the current Database State.
        /// </summary>
        /// <param name="condition">Optional WHERE condition.</param>
        /// <example>
        ///     <code>
        /// var allFoods = UpdateFoodsAsync();
        /// var filteredByName = UpdateFoodsAsync(food =&gt; food.Name == "Creme");
        /// var filteredByFat = UpdateFoodsAsync(food =&gt; food.Fat &gt; 10);
        /// // ...
        ///     </code>
        /// </example>
        private async Task UpdateFoodsAsync(Func<Food, bool> condition = null)
        {
            IsLoading = true;
            Foods.Clear();
            IEnumerable<Food> foods;
            if (condition == null)
            {
                //foods = await Task.Run(() => Model.Get()).ConfigureAwait(false);
                foods = await Model.GetAsync();
            }
            else
            {
                //foods = await Task.Run(() => Model.GetWhere(condition)).ConfigureAwait(false);
                foods = await AwaitableTask.RunAsync(() => Model.GetWhere(condition));
            }

            if (foods != null)
            {
                Foods = new ObservableCollection<Food>(foods.OrderBy(f => f.Name));
            }

            ResetInput();
            SelectedFood = Foods.FirstOrDefault();
            IsLoading = false;
        }
    }
}