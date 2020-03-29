using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public delegate void FlyoutStatusChangedHandler(object sender, FlyoutStatusChangedRoutedEventArgs e);

    /// <summary>
    /// args which are send on flyout status changed from the MetrWwindow
    /// </summary>
    public class FlyoutStatusChangedRoutedEventArgs : RoutedEventArgs
    {
        internal FlyoutStatusChangedRoutedEventArgs(RoutedEvent rEvent, IInteractive source)
            : base(rEvent, source)
        {
        }

        public Flyout ChangedFlyout { get; internal set; }
    }
}
