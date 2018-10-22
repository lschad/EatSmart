using System;

namespace SchadLucas.EatSmart.Data.EventArgs
{
    /// <summary>
    ///     A class representing an error event raised by the <see cref="SchadLucas.EatSmart.Services.IMessageHubService" />
    /// </summary>
    public sealed class MessageHubErrorEventArgs : System.EventArgs
    {
        /// <summary>
        ///     Gets the exception thrown by the <see cref="SchadLucas.EatSmart.Services.IMessageHubService" />
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        ///     Gets the subscription token of the subscriber to which message was published by the <see cref="SchadLucas.EatSmart.Services.IMessageHubService" />
        /// </summary>
        public Guid Token { get; }

        /// <summary>
        ///     Creates an instance of the <see cref="MessageHubErrorEventArgs" />
        /// </summary>
        /// <param name="e">The exception thrown by the <see cref="SchadLucas.EatSmart.Services.IMessageHubService" /></param>
        /// <param name="token">
        ///     The subscription token of the subscriber to which message was published by the <see cref="SchadLucas.EatSmart.Services.IMessageHubService" />
        /// </param>
        public MessageHubErrorEventArgs(Exception e, Guid token)
        {
            Exception = e;
            Token = token;
        }
    }
}