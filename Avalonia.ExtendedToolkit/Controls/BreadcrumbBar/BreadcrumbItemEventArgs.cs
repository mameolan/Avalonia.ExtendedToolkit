using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

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
