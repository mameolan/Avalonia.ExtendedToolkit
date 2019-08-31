using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class RoutedPropertyChangedEventArgs<T>: RoutedEventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }

        public RoutedPropertyChangedEventArgs(T oldValue, T newValue, RoutedEvent routedEvent)
        {
            OldValue = oldValue;
            NewValue = newValue;
            RoutedEvent = routedEvent;
        }

        public RoutedPropertyChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}