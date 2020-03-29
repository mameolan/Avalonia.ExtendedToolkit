using System;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public interface IMetroThumb : IInputElement
    {
        event EventHandler<VectorEventArgs> DragStarted;

        event EventHandler<VectorEventArgs> DragDelta;

        event EventHandler<VectorEventArgs> DragCompleted;

        event EventHandler<RoutedEventArgs> DoubleTapped;
    }
}
