using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public interface IMetroThumb : IInputElement
    {
        event EventHandler<VectorEventArgs> DragStarted;

        event EventHandler<VectorEventArgs> DragDelta;

        event EventHandler<VectorEventArgs> DragCompleted;

        event EventHandler<RoutedEventArgs> DoubleTapped;
    }
}
