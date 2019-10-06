using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public delegate void DragStartedEventHandler(object sender, VectorEventArgs e);

    public delegate void DragDeltaEventHandler(object sender, VectorEventArgs e);

    public delegate void DragCompletedEventHandler(object sender, VectorEventArgs e);
}