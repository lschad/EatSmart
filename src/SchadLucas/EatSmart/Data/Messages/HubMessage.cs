namespace SchadLucas.EatSmart.Data.Messages
{
    public abstract class HubMessage : HubMessageBase
    {
        public object Sender { get; protected set; }

        protected HubMessage(object sender)
        {
            Sender = sender;
        }
    }
}