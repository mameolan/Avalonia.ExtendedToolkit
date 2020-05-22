using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// BreadcrumbItemEventHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void BreadcrumbItemEventHandler(object sender, BreadcrumbItemEventArgs e);

    /// <summary>
    /// BreadcrumbItem event
    /// </summary>
    public class BreadcrumbItemEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// sets the item to send
        /// </summary>
        /// <param name="item"></param>
        /// <param name="routedEvent"></param>
        public BreadcrumbItemEventArgs(BreadcrumbItem item, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Item = item;
        }

        /// <summary>
        /// current item
        /// </summary>
        public BreadcrumbItem Item { get; private set; }
    }
}
