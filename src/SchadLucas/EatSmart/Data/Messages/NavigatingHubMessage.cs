using System;

namespace SchadLucas.EatSmart.Data.Messages
{
    public sealed class NavigatingHubMessage : HubMessage
    {
        public object[] Arguments { get; private set; }
        public Type RequestedView { get; private set; }
        public Type RequestedViewModel { get; private set; }

        public NavigatingHubMessage(object sender, Type requestedView, Type requestedViewModel, object[] args = null) : base(sender)
        {
            Initialize(sender, requestedView, requestedViewModel, args);
        }

        public NavigatingHubMessage(object sender, Type requestedView, object[] args = null) : base(sender)
        {
            Initialize(sender, requestedView, null, args);
        }

        private void Initialize(object sender, Type requestedView, Type requestedViewModel, object[] args)
        {
            Sender = sender;
            RequestedView = requestedView;
            RequestedViewModel = requestedViewModel;
            Arguments = args;
        }
    }
}