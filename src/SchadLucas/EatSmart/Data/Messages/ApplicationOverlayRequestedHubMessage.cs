namespace SchadLucas.EatSmart.Data.Messages
{
    public class ApplicationOverlayRequestedHubMessage : HubMessage
    {
        public object Content { get; }

        public ApplicationOverlayRequestedHubMessage(object sender, object content) : base(sender)
        {
            Content = content;
        }
    }
}