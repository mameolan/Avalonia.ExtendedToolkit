using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    internal class MetroThumbContentControlDragCompletedEventArgs : VectorEventArgs
    {
        public MetroThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
        {
            this.Vector = new Vector(horizontalOffset, verticalOffset);
            this.Handled = canceled;
        }
    }
}