using SchadLucas.EatSmart.Services;
using System;
using System.Windows.Input;
using SchadLucas.EatSmart.Data.Commands;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class NavigationViewModel : ViewModel, INavigationViewModel
    {
        private readonly INavigationService _navigationService;
        private bool _canNavigateBackward;
        private bool _canNavigateForward;
        private RelayCommand _navigateBackwardCommand;
        private ICommand _navigateForwardCommand;
        private ICommand _navigateToCommand;

        public bool CanNavigateBackward
        {
            get => _canNavigateBackward;
            set => SetField(ref _canNavigateBackward, value);
        }

        public bool CanNavigateForward
        {
            get => _canNavigateForward;
            set => SetField(ref _canNavigateForward, value);
        }

        public ICommand NavigateBackwardCommand
        {
            get
            {
                if (_navigateBackwardCommand == null)
                {
                    _navigateBackwardCommand = new RelayCommand(obj =>
                        _navigationService.NavigateBackward(), _ =>
                        _navigationService.HasPrevious);
                }

                return _navigateBackwardCommand;
            }
        }

        public ICommand NavigateForwardCommand
        {
            get
            {
                if (_navigateForwardCommand == null)
                {
                    _navigateForwardCommand = new RelayCommand(obj =>
                        _navigationService.NavigateForward(), _ =>
                        _navigationService.HasNext);
                }

                return _navigateForwardCommand;
            }
        }

        public ICommand NavigateToCommand
        {
            get
            {
                if (_navigateToCommand == null)
                {
                    _navigateToCommand = new RelayCommand(obj =>
                    {
                        if (obj is Type type)
                        {
                            _navigationService.NavigateTo(type);
                        }
                    });
                }

                return _navigateToCommand;
            }
        }

        public NavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}