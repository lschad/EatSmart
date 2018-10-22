using SchadLucas.EatSmart.Data.Enums;
using SchadLucas.EatSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SchadLucas.EatSmart.Data.Types
{
    public class PopupNotification : DisposableObject, IPopupNotification
    {
        private IReadOnlyCollection<IPopupAction> _actions = new ReadOnlyCollection<IPopupAction>(new List<IPopupAction>());

        public IReadOnlyCollection<IPopupAction> Actions
        {
            get => _actions;
            set
            {
                _actions = value;

                foreach (var action in _actions)
                {
                    action.Executed += (s, e) => Dispose();
                }
            }
        }
        public int Duration { get; set; } = 8000;
        public Guid Id { get; } = Guid.NewGuid();
        public string Message { get; set; }
        public PopupNotificationLevel NotificationLevel { get; set; } = PopupNotificationLevel.Info;
        public bool Sticky { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                Actions = new IPopupAction[0];
                Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is IPopupNotification notification)
            {
                return Id == notification.Id;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"PopupNotification [{Id}] (Message: {Message}; Actions: {Actions})";
        }
    }
}