using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// routed eventargs for colors
    /// </summary>
    public class ColorRoutedEventArgs : RoutedEventArgs
    {
        public Color Color { get; private set; }

        public ColorRoutedEventArgs(Color color, RoutedEvent routedEvent):base(routedEvent)
        {
            Color = color;
        }
    }
}
