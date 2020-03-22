using Avalonia.Interactivity;

namespace Avalonia.Controlz.EventArgs
{
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //


    /// <summary>
    /// This RoutedPropertyChangedEventArgs class contains old and new value when 
    /// RoutedPropertyChangedEvent is raised.
    /// </summary>
    /// <seealso cref="RoutedEventArgs" />
    /// <typeparam name="T"></typeparam>
    public class RoutedPropertyChangedEventArgs<T> : RoutedEventArgs
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
