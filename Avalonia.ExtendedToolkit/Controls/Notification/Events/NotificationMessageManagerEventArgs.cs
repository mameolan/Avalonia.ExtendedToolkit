using System;

//ported from https://github.com/Enterwell/Wpf.Notifications

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// The notification message manager event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="NotificationMessageManagerEventArgs"/> instance containing the event data.</param>
    public delegate void NotificationMessageManagerEventHandler(
        object sender,
        NotificationMessageManagerEventArgs args);

    /// <summary>
    /// The notification message manager event arguments.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class NotificationMessageManagerEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public INotificationMessage Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessageManagerEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NotificationMessageManagerEventArgs(INotificationMessage message)
        {
            Message = message;
        }
    }
}
