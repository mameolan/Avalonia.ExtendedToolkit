namespace Avalonia.ExtendedToolkit.Controls
{
    internal class WindowApplicationSettings : IWindowPlacementSettings
    {
        private MetroWindow metroWindow;

        public WindowApplicationSettings(MetroWindow metroWindow)
        {
            this.metroWindow = metroWindow;
        }
    }
}