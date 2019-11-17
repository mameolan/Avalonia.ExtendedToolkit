using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.Controlz.EventArgs
{
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
