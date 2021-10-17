using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// event if the DragFinished is execute on the <see cref="RotateThumb"/>
    /// </summary>
    public class RotateFinishedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// moved position
        /// </summary>
        /// <value></value>
        public Vector Vector { get; set; }

        /// <summary>
        /// Angle
        /// </summary>
        /// <value></value>
        public double Angle { get; set; }
    }
}
