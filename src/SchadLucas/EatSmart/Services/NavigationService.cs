using SchadLucas.EatSmart.Data.Exceptions;
using SchadLucas.EatSmart.Data.Messages;
using SchadLucas.EatSmart.Data.Types;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.Utilities;
using SchadLucas.EatSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchadLucas.EatSmart.Services
{
    public sealed class NavigationService : Service, INavigationService
    {
        private readonly IList<IHistoryItem> _items = new List<IHistoryItem>();
        private readonly IMessageHubService _messageHub;
        private readonly IViewModelHelper _viewModelHelper;

        private int CurrentIndex => _items?.IndexOf(Current) ?? 0;
        private int TopIndex => _items?.Count - 1 ?? 0;

        public IHistoryItem Current { get; private set; }
        public bool HasNext
        {
            get
            {
                var count = _items.Count;
                var topIndex = count - 1;
                var currentIndex = _items.IndexOf(Current);
                return currentIndex < topIndex;
            }
        }
        public bool HasPrevious
        {
            get
            {
                var currentIndex = _items.IndexOf(Current);
                return currentIndex > 0;
            }
        }
        public IHistoryItem Next => _items[_items.IndexOf(Current) + 1];
        public IHistoryItem Previous => _items[_items.IndexOf(Current) - 1];

        public NavigationService(IMessageHubService messageHub, IViewModelHelper viewModelHelper)
        {
            _viewModelHelper = viewModelHelper;
            _messageHub = messageHub;

            _messageHub.Subscribe<NavigatingHubMessage>(OnNavigatingMessage);
        }

        private IHistoryItem Find(IHistoryItem item)
        {
            return _items.FirstOrDefault(i =>
                                              i.ViewModel == item.ViewModel &&
                                              i.View == item.View &&
                                              i.Arguments == item.Arguments);
        }

        private void NavigateTo(Type view, Type viewModel, object[] arguments)
        {
            _messageHub.Publish(new NavigatingHubMessage(this, view, viewModel, arguments));
        }

        private void OnNavigatingMessage(NavigatingHubMessage msg)
        {
            var view = _viewModelHelper.GetViewFromType(msg.RequestedView);
            var viewModelType = msg.RequestedViewModel ?? _viewModelHelper.GetViewModelTypeFromViewType(msg.RequestedView);
            var viewModel = _viewModelHelper.GetViewModelFromType(viewModelType);

            if (view is null || viewModel is null)
            {
                if (view is null)
                {
                    Logger.Error($"View {msg.RequestedView} could not be found. Did you register it as IView?");
                }
                if (viewModel is null)
                {
                    Logger.Error($"ViewModel {msg.RequestedViewModel} could not be found. Did you register it as IView?");
                }

                return;
            }

            if (!viewModel.IsActive)
            {
                ViewModelBinder.BindArguments(viewModel, msg.Arguments);
                ViewModelBinder.BindView(viewModel, view);

                PublishItem(view, viewModel, msg.Arguments);
            }
        }

        private void PublishItem(IView view, IViewModel viewModel, object[] arguments)
        {
            var item = (IHistoryItem)new HistoryItem(view, viewModel, arguments);
            var foundItem = Find(item);

            if (foundItem == null)
            {
                while (TopIndex > CurrentIndex)
                {
                    _items.RemoveAt(TopIndex);
                }

                _items.Add(item);
            }
            else
            {
                if (foundItem == Current)
                {
                    return;
                }
                item = foundItem;
            }

            UpdateCurrentItem(item);
            PublishNavigatedMessage(item);
        }

        private void PublishNavigatedMessage(IHistoryItem item)
        {
            var message = new NavigatedHubMessage(this, item.View, item.ViewModel, item.Arguments);
            _messageHub.Publish(message);
        }

        private void UpdateCurrentItem(IHistoryItem item)
        {
            if (!_items.Contains(item))
            {
                throw new ArgumentException("Can not update items which are not available in the internal list.", nameof(item));
            }

            if (item == Current)
            {
                throw new ArgumentException("Can not update an item which is already set as Current", nameof(item));
            }

            Current?.ViewModel.Disable();
            Current = item;
            Current.ViewModel.Enable();
        }

        public void NavigateBackward()
        {
            if (!HasPrevious)
            {
                throw new NavigationException("Can not navigate backward.");
            }

            Current = Previous;
            PublishNavigatedMessage(Current);
        }

        public void NavigateForward()
        {
            if (!HasNext)
            {
                throw new NavigationException("Can not move forward.");
            }

            Current = Next;
            PublishNavigatedMessage(Current);
        }

        public void NavigateTo<TView>(object[] arguments = default)
        {
            NavigateTo(typeof(TView), arguments);
        }

        public void NavigateTo(Type view, object[] arguments = default)
        {
            var vmType = _viewModelHelper.GetViewModelTypeFromViewType(view);
            NavigateTo(view, vmType, arguments);
        }

        public void NavigateTo<TView, TViewModel>(object[] arguments = default) where TView : IView where TViewModel : IViewModel
        {
            NavigateTo(typeof(TView), typeof(TViewModel), arguments);
        }
    }
}