using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    internal class FlyoutStatusChangedRoutedEventArgs : RoutedEventArgs
    {
        internal FlyoutStatusChangedRoutedEventArgs(RoutedEvent rEvent, IInteractive source) 
            : base(rEvent, source)
        {

        }

        public Flyout ChangedFlyout { get; internal set; }
    }
}