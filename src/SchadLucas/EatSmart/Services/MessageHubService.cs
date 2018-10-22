using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SchadLucas.EatSmart.Data.Exceptions;
using JetBrains.Annotations;
using SchadLucas.EatSmart.Data.EventArgs;

// source: https://github.com/NimaAra/Easy.MessageHub/ modified to fit my project

namespace SchadLucas.EatSmart.Services
{
    internal static class Subscriptions
    {
        private static readonly List<Subscription> AllSubscriptions = new List<Subscription>();

        [ThreadStatic]
        private static int _localSubscriptionRevision;

        [ThreadStatic]
        private static Subscription[] _localSubscriptions;

        private static int _subscriptionsChangeCounter;

        private static T[] RemoveAt<T>(T[] source, int index)
        {
            var dest = new T[source.Length - 1];
            if (index > 0) { Array.Copy(source, 0, dest, 0, index); }

            if (index < source.Length - 1)
            {
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            }

            return dest;
        }

        internal static void Clear()
        {
            lock (AllSubscriptions)
            {
                AllSubscriptions.Clear();
                if (_localSubscriptions != null)
                {
                    Array.Clear(_localSubscriptions, 0, _localSubscriptions.Length);
                }
                _subscriptionsChangeCounter++;
            }
        }

        internal static Subscription[] GetTheLatestSubscriptions()
        {
            if (_localSubscriptions == null) { _localSubscriptions = new Subscription[0]; }

            var changeCounterLatestCopy = Interlocked.CompareExchange(ref _subscriptionsChangeCounter, 0, 0);
            if (_localSubscriptionRevision == changeCounterLatestCopy) { return _localSubscriptions; }

            Subscription[] latestSubscriptions;
            lock (AllSubscriptions)
            {
                latestSubscriptions = AllSubscriptions.ToArray();
            }

            _localSubscriptionRevision = changeCounterLatestCopy;
            _localSubscriptions = latestSubscriptions;
            return _localSubscriptions;
        }

        internal static bool IsRegistered(Guid token)
        {
            lock (AllSubscriptions) { return AllSubscriptions.Any(s => s.Token == token); }
        }

        internal static Guid Register<T>(TimeSpan throttleBy, Action<T> action)
        {
            var type = typeof(T);
            var key = Guid.NewGuid();
            var subscription = new Subscription(type, key, throttleBy, action);

            lock (AllSubscriptions)
            {
                AllSubscriptions.Add(subscription);
                _subscriptionsChangeCounter++;
            }

            return key;
        }

        internal static void UnRegister(Guid token)
        {
            lock (AllSubscriptions)
            {
                var subscription = AllSubscriptions.Find(s => s.Token == token);
                if (subscription == null) { return; }

                var removed = AllSubscriptions.Remove(subscription);
                if (!removed) { return; }

                if (_localSubscriptions != null)
                {
                    var localIdx = Array.IndexOf(_localSubscriptions, subscription);
                    if (localIdx >= 0) { _localSubscriptions = RemoveAt(_localSubscriptions, localIdx); }
                }

                _subscriptionsChangeCounter++;
            }
        }
    }

    internal sealed class Subscription
    {
        private const long TicksMultiplier = 1000 * TimeSpan.TicksPerMillisecond;
        private readonly long _throttleByTicks;
        private double? _lastHandleTimestamp;

        private object Handler { get; }
        internal Guid Token { get; }

        internal Type Type { get; }

        internal Subscription(Type type, Guid token, TimeSpan throttleBy, object handler)
        {
            Type = type;
            Token = token;
            Handler = handler;
            _throttleByTicks = throttleBy.Ticks;
        }

        internal bool CanHandle()
        {
            if (_throttleByTicks == 0) { return true; }

            if (_lastHandleTimestamp == null)
            {
                _lastHandleTimestamp = Stopwatch.GetTimestamp();
                return true;
            }

            var now = Stopwatch.GetTimestamp();
            var durationInTicks = (now - _lastHandleTimestamp) / Stopwatch.Frequency * TicksMultiplier;

            if (durationInTicks >= _throttleByTicks)
            {
                _lastHandleTimestamp = now;
                return true;
            }

            return false;
        }

        internal void Handle<T>(T message)
        {
            if (!CanHandle()) { return; }

            var handler = Handler as Action<T>;
            // ReSharper disable once PossibleNullReferenceException
            handler(message);
        }
    }

    /// <summary>
    ///     An implementation of the <i>Event Aggregator</i> pattern.
    /// </summary>
    [UsedImplicitly]
    public sealed class MessageHubService : Service, IMessageHubService
    {
        private Action<Type, object> _globalHandler;

        public event EventHandler<MessageHubErrorEventArgs> OnError;

        [DebuggerStepThrough]
        private void EnsureNotNull(object obj)
        {
            if (obj == null) { throw new NullReferenceException(nameof(obj)); }
        }

        public void ClearSubscriptions() => Subscriptions.Clear();

        public void Dispose()
        {
            _globalHandler = null;
            ClearSubscriptions();
        }

        public bool IsSubscribed(Guid token) => Subscriptions.IsRegistered(token);

        public void Publish<T>(T message)
        {
            var localSubscriptions = Subscriptions.GetTheLatestSubscriptions();

            var msgType = typeof(T);

            _globalHandler?.Invoke(msgType, message);

            // ReSharper disable once ForCanBeConvertedToForeach | Performance Critical
            for (var idx = 0; idx < localSubscriptions.Length; idx++)
            {
                var subscription = localSubscriptions[idx];

                if (!subscription.Type.IsAssignableFrom(msgType)) { continue; }
                try
                {
                    subscription.Handle(message);
                }
                catch (Exception e)
                {
                    var copy = OnError;
                    copy?.Invoke(this, new MessageHubErrorEventArgs(e, subscription.Token));
                }
            }
        }

        public void Publish<TMessage>(object[] args = default)
        {
            var message = (TMessage)Activator.CreateInstance(typeof(TMessage), args);
            Publish(message);
        }

        public void RegisterGlobalHandler(Action<Type, object> onMessage)
        {
            EnsureNotNull(onMessage);
            _globalHandler = onMessage;
        }

        public Guid Subscribe<T>(Action<T> action) => Subscribe(action, TimeSpan.Zero);

        public Guid Subscribe<T>(Action<T> action, TimeSpan throttleBy)
        {
            EnsureNotNull(action);
            return Subscriptions.Register(throttleBy, action);
        }

        public void UnSubscribe(Guid token) => Subscriptions.UnRegister(token);
    }
}