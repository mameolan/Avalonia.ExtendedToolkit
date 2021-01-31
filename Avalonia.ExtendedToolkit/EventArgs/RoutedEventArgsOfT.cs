using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// a routed event with T args
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RoutedEventArgsOfT<T> : RoutedEventArgs
    {

        public RoutedEventArgsOfT(T info)
        {
            Info = info;
        }

        public RoutedEventArgsOfT(RoutedEvent routedEvent, IInteractive source) : base(routedEvent, source)
        {
        }


        public T Info { get; set; }

    }
}