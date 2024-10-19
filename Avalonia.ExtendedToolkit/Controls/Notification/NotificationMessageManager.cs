using Avalonia.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//ported from https://github.com/Enterwell/Wpf.Notifications

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// The notification message manager.
    /// </summary>
    /// <seealso cref="INotificationMessageManager" />
    public class NotificationMessageManager : INotificationMessageManager
    {
        /// <summary>
        /// if max items is reached the first item is removed from the collection
        /// </summary>
        public int MaxItems { get; set; } = int.MaxValue;

        private readonly List<INotificationMessage> queuedMessages = new List<INotificationMessage>();

        /// <summary>
        /// Occurs when new notification message is queued.
        /// </summary>
        public event NotificationMessageManagerEventHandler OnMessageQueued;

        /// <summary>
        /// Occurs when notification message is dismissed.
        /// </summary>
        public event NotificationMessageManagerEventHandler OnMessageDismissed;

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        public INotificationMessageFactory Factory { get; set; } = new NotificationMessageFactory();

        /// <summary>
        /// Queues the specified message.
        /// This will ignore the <c>null</c> message or already queued notification message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Queue(INotificationMessage message)
        {
            if (message == null || queuedMessages.Contains(message))
                return;

            if (queuedMessages.Count - 1 > MaxItems)
            {
                Dismiss(queuedMessages.FirstOrDefault());
            }


            queuedMessages.Add(message);

            TriggerMessageQueued(message);
        }

        /// <summary>
        /// Triggers the message queued event.
        /// </summary>
        /// <param name="message">The message.</param>
        private void TriggerMessageQueued(INotificationMessage message)
        {
            OnMessageQueued?.Invoke(this, new NotificationMessageManagerEventArgs(message));
        }

        /// <summary>
        /// Dismisses the specified message.
        /// This will ignore the <c>null</c> or not queued notification message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Dismiss(INotificationMessage message)
        {
            if (message == null || !queuedMessages.Contains(message))
                return;

            queuedMessages.Remove(message);

            if (message is INotificationAnimation animatableMessage
                )
            {
                if (animatableMessage.AnimatableElement != null)
                {
                    var animation = animatableMessage.AnimationIn;
                    animation.Delay = TimeSpan.FromSeconds(0);
                    animation.Duration = TimeSpan.FromSeconds(animatableMessage.AnimationOutDuration);

                    Animatable animatable = animatableMessage.AnimatableElement as Animatable;
                    //.edited, null arg removal
                    animation.RunAsync(animatable).ContinueWith(x =>
                    {
                        TriggerMessageDismissed(message);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    TriggerMessageDismissed(message);
                }
            }
            else
            {
                TriggerMessageDismissed(message);
            }
        }

        /// <summary>
        /// Triggers the message dismissed event.
        /// </summary>
        /// <param name="message">The message.</param>
        private void TriggerMessageDismissed(INotificationMessage message)
        {
            OnMessageDismissed?.Invoke(this, new NotificationMessageManagerEventArgs(message));
        }
    }
}
