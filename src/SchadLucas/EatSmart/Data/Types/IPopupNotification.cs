using SchadLucas.EatSmart.Data.Enums;
using SchadLucas.EatSmart.ViewModels;
using System;
using System.Collections.Generic;

namespace SchadLucas.EatSmart.Data.Types
{
    public interface IPopupNotification : IDisposableObject
    {
        /// <summary>
        ///     Gets a collection of <see cref="IPopupAction">Actions</see> which will be added to
        ///     the Notification.
        /// </summary>
        /// <remarks>Default is an empty collection.</remarks>
        /// <returns>A collection of <see cref="IPopupAction">IPopupActions</see>.</returns>
        IReadOnlyCollection<IPopupAction> Actions { get; }

        /// <summary>
        ///     Sets the duration how long the Notification will be displayed (in ms).
        /// </summary>
        /// <remarks>
        ///     The value is ignored if <see cref="Sticky" /> is <see langword="true" />. The default
        ///     value is 2000.
        /// </remarks>
        int Duration { get; }

        /// <summary>
        ///     A unique Identifier for the notification.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        ///     Gets the content of the notification.
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Gets a value which determines the <see cref="PopupNotificationLevel" />.
        /// </summary>
        /// <remarks>
        ///     Users of <see cref="IPopupNotification" /> should use the level to theme their popups accordingly.
        /// </remarks>
        PopupNotificationLevel NotificationLevel { get; }

        /// <summary>
        ///     Gets a value which determines if the notification should be sticky.
        /// </summary>
        /// <remarks>
        ///     A sticky notification will be displayed until it's closed manually.
        ///     <see cref="Duration" /> will be ignored.
        /// </remarks>
        bool Sticky { get; }
    }
}