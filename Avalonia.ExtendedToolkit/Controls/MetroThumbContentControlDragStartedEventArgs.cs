using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    internal class MetroThumbContentControlDragStartedEventArgs : VectorEventArgs
    {
        public MetroThumbContentControlDragStartedEventArgs(double horizontalOffset, double verticalOffset)
        {
            this.Vector = new Vector(horizontalOffset, verticalOffset);
        }
    }
}