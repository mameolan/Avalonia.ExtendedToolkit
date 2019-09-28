using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroThumbContentControlDragCompletedEventArgs : VectorEventArgs
    {
        public MetroThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
        {
            this.Vector = new Vector(horizontalOffset, verticalOffset);
            this.Handled = canceled;
        }
    }
}