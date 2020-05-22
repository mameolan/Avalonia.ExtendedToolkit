using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)

    /// <summary>
    ///     This delegate must used by handlers of the RoutedPropertyChangedEvent event.
    /// </summary>
    /// <param name="sender">The current element along the event's route.</param>
    /// <param name="e">The event arguments containing additional information about the event.</param>
    /// <returns>Nothing.</returns>
    public delegate void RoutedPropertyChangedEventHandler<T>(object sender, RoutedPropertyChangedEventArgs<T> e);

    /// <summary>
    /// This RoutedPropertyChangedEventArgs class contains old and new value when
    /// RoutedPropertyChangedEvent is raised.
    /// </summary>
    /// <seealso cref="RoutedEventArgs" />
    /// <typeparam name="T"></typeparam>
    public class RoutedPropertyChangedEventArgs<T> : RoutedEventArgs
    {
        /// <summary>
        /// old value
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// new value
        /// </summary>
        public T NewValue { get; }

        /// <summary>
        /// constructs RoutedPropertyChangedEventArgs by parameter and routed event
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="routedEvent"></param>
        public RoutedPropertyChangedEventArgs(T oldValue, T newValue, RoutedEvent routedEvent)
        {
            OldValue = oldValue;
            NewValue = newValue;
            RoutedEvent = routedEvent;
        }

        /// <summary>
        /// constructs RoutedPropertyChangedEventArgs by parameter
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public RoutedPropertyChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
