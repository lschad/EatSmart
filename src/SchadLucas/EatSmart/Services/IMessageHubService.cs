﻿// https://github.com/NimaAra/Easy.MessageHub/ ReSharper disable All

using System;
using SchadLucas.EatSmart.Data.EventArgs;
using SchadLucas.EatSmart.Data.Exceptions;

namespace SchadLucas.EatSmart.Services
{
    /// <summary>
    ///     An implementation of the <c>Event Aggregator</c> pattern.
    /// </summary>
    public interface IMessageHubService : IService, IDisposable
    {
        /// <summary>
        ///     Invoked if an error occurs when publishing the message to a subscriber.
        /// </summary>
        event EventHandler<MessageHubErrorEventArgs> OnError;

        /// <summary>
        ///     Clears all the subscriptions from the <see cref="IMessageHubService" />.
        /// </summary>
        /// <remarks>The global handler and the <see cref="OnError" /> are not affected</remarks>
        void ClearSubscriptions();

        /// <summary>
        ///     Checks if a specific subscription is active on the <see cref="IMessageHubService" />.
        /// </summary>
        /// <param name="token">The token representing the subscription</param>
        /// <returns><c>True</c> if the subscription is active otherwise <c>False</c></returns>
        bool IsSubscribed(Guid token);

        /// <summary>
        ///     Publishes the <paramref name="message" /> on the <see cref="IMessageHubService" />.
        /// </summary>
        /// <param name="message">The message to published</param>
        void Publish<T>(T message);

        /// <remarks>Added by me.</remarks>
        /// <typeparam name="TMessage"></typeparam>
        void Publish<TMessage>(object[] args = default);

        /// <summary>
        ///     Registers a callback which is invoked on every message published by the <see cref="IMessageHubService" />.
        /// </summary>
        /// <remarks>
        ///     Invoking this method with a new <paramref name="onMessage" /> overwrites the previous one.
        /// </remarks>
        /// <param name="onMessage">
        ///     The callback to invoke on every message <remarks>The callback receives the type of
        ///     the message and the message as arguments</remarks>
        /// </param>
        void RegisterGlobalHandler(Action<Type, object> onMessage);

        /// <summary>
        ///     Subscribes a callback against the <see cref="IMessageHubService" /> for a specific
        ///     type of message.
        /// </summary>
        /// <typeparam name="T">The type of message to subscribe to</typeparam>
        /// <param name="action">
        ///     The callback to be invoked once the message is published on the <see cref="IMessageHubService" />
        /// </param>
        /// <returns>The token representing the subscription</returns>
        Guid Subscribe<T>(Action<T> action);

        /// <summary>
        ///     Subscribes a callback against the <see cref="IMessageHubService" /> for a specific
        ///     type of message.
        /// </summary>
        /// <typeparam name="T">The type of message to subscribe to</typeparam>
        /// <param name="action">
        ///     The callback to be invoked once the message is published on the <see cref="IMessageHubService" />
        /// </param>
        /// <param name="throttleBy">
        ///     The <see cref="TimeSpan" /> specifying the rate at which subscription is throttled
        /// </param>
        /// <returns>The token representing the subscription</returns>
        Guid Subscribe<T>(Action<T> action, TimeSpan throttleBy);

        /// <summary>
        ///     Un-Subscribes a subscription from the <see cref="IMessageHubService" />.
        /// </summary>
        /// <param name="token">The token representing the subscription</param>
        void UnSubscribe(Guid token);
    }
}