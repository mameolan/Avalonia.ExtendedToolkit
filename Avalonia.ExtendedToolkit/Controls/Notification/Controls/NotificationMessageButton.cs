using Avalonia.Controls;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{

    //ported from https://github.com/Enterwell/Wpf.Notifications

    /// <summary>
    /// Button control which implements the
    /// <see cref="INotificationMessageButton"/> interface
    /// </summary>
    public class NotificationMessageButton : Button, INotificationMessageButton
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(Button);

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessageButton"/> class.
        /// </summary>
        public NotificationMessageButton()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessageButton"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public NotificationMessageButton(object content)
        {
            Content = content;
        }

        /// <summary>
        /// Called when a <see cref="T:Avlonia.Controls.Button" /> is clicked.
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();
            Callback?.Invoke(this);
        }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>
        /// The callback.
        /// </value>
        public Action<INotificationMessageButton> Callback { get; set; }
    }
}
