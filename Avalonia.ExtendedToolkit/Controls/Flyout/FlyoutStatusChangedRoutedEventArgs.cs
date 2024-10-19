using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// FlyoutStatusChangedHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void FlyoutStatusChangedHandler(object sender, FlyoutStatusChangedRoutedEventArgs e);

    /// <summary>
    /// args which are send on flyout status changed from the MetrWwindow
    /// </summary>
    public class FlyoutStatusChangedRoutedEventArgs : RoutedEventArgs
    {
        internal FlyoutStatusChangedRoutedEventArgs(RoutedEvent rEvent, Interactive source)
            : base(rEvent, source)
        {
        }

        /// <summary>
        /// changed flyout
        /// </summary>
        public Flyout ChangedFlyout { get; internal set; }
    }
}
