// ReSharper disable UnusedMember.Global

using System;
using System.Windows;
using System.Windows.Threading;

namespace SchadLucas.EatSmart.Utilities
{
    /// <summary>
    ///     Provides services for managing the queue of work items on the UI thread.
    /// </summary>
    /// <remarks>
    ///     Wrapper for <see cref="Application.Dispatcher">Application.Current.Dispatcher</see> which
    ///     only forwards the command if the Application still exists. This prevents
    ///     <see cref="NullReferenceException" /> when the invoke happens during the Shutdown Sequence.
    /// </remarks>
    public sealed class UiDispatcher
    {
        /// <inheritdoc cref="Dispatcher.BeginInvoke(Delegate, object[])" />
        public static void BeginInvoke(Delegate method, params object[] args) => Application.Current?.Dispatcher?.BeginInvoke(method, args);

        /// <inheritdoc cref="Dispatcher.BeginInvoke(DispatcherPriority, Delegate)" />
        public static void BeginInvoke(DispatcherPriority priority, Delegate method) => Application.Current?.Dispatcher?.BeginInvoke(priority, method);

        /// <inheritdoc cref="Dispatcher.BeginInvoke(DispatcherPriority, Delegate, object)" />
        public static void BeginInvoke(DispatcherPriority priority, Delegate method, object arg) => Application.Current?.Dispatcher?.BeginInvoke(priority, method, arg);

        /// <inheritdoc cref="Dispatcher.Invoke(Action)" />
        public static void Invoke(Action action) => Application.Current?.Dispatcher?.Invoke(action);

        public static void Invoke(Action action, DispatcherPriority priority) => Application.Current?.Dispatcher?.Invoke(action, priority);

        /// <inheritdoc cref="Dispatcher.Invoke(Action)" />
        public static void InvokeAsync(Action callback) => Application.Current?.Dispatcher?.InvokeAsync(callback);

        /// <inheritdoc cref="Dispatcher.Invoke(Action, DispatcherPriority)" />
        public static void InvokeAsync(Action callback, DispatcherPriority priority) => Application.Current?.Dispatcher?.InvokeAsync(callback, priority);
    }
}