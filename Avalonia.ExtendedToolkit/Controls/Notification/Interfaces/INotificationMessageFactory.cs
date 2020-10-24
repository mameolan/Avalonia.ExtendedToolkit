
//ported from https://github.com/Enterwell/Wpf.Notifications

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// The notification message factory.
    /// </summary>
    public interface INotificationMessageFactory
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns>Returns new instance of notification message.</returns>
        INotificationMessage GetMessage();

        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <returns>Returns new instance of notification message button.</returns>
        INotificationMessageButton GetButton();
    }
}
