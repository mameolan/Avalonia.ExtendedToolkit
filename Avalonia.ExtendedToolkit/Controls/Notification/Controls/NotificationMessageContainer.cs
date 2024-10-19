using Avalonia;
using Avalonia.Animation;
using Avalonia.Collections;
using Avalonia.Controls;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/Enterwell/Wpf.Notifications

    /// <summary>
    /// items controls which holds messages
    /// </summary>
    public class NotificationMessageContainer : ItemsControl
    {
        /// <summary>
        /// Gets or sets Manager.
        /// </summary>
        public INotificationMessageManager Manager
        {
            get { return (INotificationMessageManager)GetValue(ManagerProperty); }
            set { SetValue(ManagerProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Manager"/> property.
        /// </summary>
        public static readonly StyledProperty<INotificationMessageManager> ManagerProperty =
            AvaloniaProperty.Register<NotificationMessageContainer, INotificationMessageManager>(nameof(Manager));

        public NotificationMessageContainer()
        {
            ManagerProperty.Changed.AddClassHandler<NotificationMessageContainer>((o, e) => ManagerPropertyChangedCallback(o, e));
        }

        /// <summary>
        /// if manager changed old events are unregistered
        /// and new events are registered
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void ManagerPropertyChangedCallback(NotificationMessageContainer o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotificationMessageManager oldManager)
                o.DetachManagerEvents(oldManager);

            if (e.NewValue is INotificationMessageManager newManager)
                o.AttachManagerEvents(newManager);
        }

        /// <summary>
        /// Attaches the manager events.
        /// </summary>
        /// <param name="newManager">The new manager.</param>
        private void AttachManagerEvents(INotificationMessageManager newManager)
        {
            newManager.OnMessageQueued += ManagerOnOnMessageQueued;
            newManager.OnMessageDismissed += ManagerOnOnMessageDismissed;
        }

        /// <summary>
        /// Detaches the manager events.
        /// </summary>
        /// <param name="oldManager">The old manager.</param>
        private void DetachManagerEvents(INotificationMessageManager oldManager)
        {
            oldManager.OnMessageQueued -= ManagerOnOnMessageQueued;
            oldManager.OnMessageDismissed -= ManagerOnOnMessageDismissed;
        }

        /// <summary>
        /// Manager on message dismissed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NotificationMessageManagerEventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException">Can't use both ItemsSource and Items collection at the same time.</exception>
        private void ManagerOnOnMessageDismissed(object sender, NotificationMessageManagerEventArgs args)
        {
            RemoveMessage(args.Message);
        }

        /// <summary>
        /// removes the message from the items collection
        /// </summary>
        /// <param name="message"></param>
        private void RemoveMessage(INotificationMessage message)
        {
            //.edited
            Items.Remove(message);
        }

        /// <summary>
        /// Manager on message queued.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NotificationMessageManagerEventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException">Can't use both ItemsSource and Items collection at the same time.</exception>
        private void ManagerOnOnMessageQueued(object sender, NotificationMessageManagerEventArgs args)
        {
            //.edited
            Items.Add(args.Message);

            if (args.Message is INotificationAnimation animatableMessage
                && animatableMessage.AnimatableElement != null)
            {
                var animation = animatableMessage.AnimationOut;
                animation.Delay = TimeSpan.FromSeconds(0);
                animation.Duration = TimeSpan.FromSeconds(animatableMessage.AnimationOutDuration);

                Animatable animatable = animatableMessage.AnimatableElement as Animatable;
                //.edited, null arg removal
                animation.RunAsync(animatable);
            }
        }
    }
}
