using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    public delegate void BreadcrumbItemEventHandler(object sender, BreadcrumbItemEventArgs e);

    public class BreadcrumbItemEventArgs : RoutedEventArgs
    {
        public BreadcrumbItemEventArgs(BreadcrumbItem item, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Item = item;
        }

        public BreadcrumbItem Item { get; private set; }
    }
}