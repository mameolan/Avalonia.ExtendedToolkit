using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroThumbContentControlDragStartedEventArgs : VectorEventArgs
    {
        public MetroThumbContentControlDragStartedEventArgs(double horizontalOffset, double verticalOffset)
        {
            this.Vector = new Vector(horizontalOffset, verticalOffset);
        }
    }
}