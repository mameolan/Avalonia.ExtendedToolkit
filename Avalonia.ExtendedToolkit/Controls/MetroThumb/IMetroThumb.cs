using System;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// IMetroThumb interface
    /// </summary>
    public interface IMetroThumb : IInputElement
    {
        /// <summary>
        /// DragStarted event
        /// </summary>
        event EventHandler<VectorEventArgs> DragStarted;

        /// <summary>
        /// DragDelta event
        /// </summary>
        event EventHandler<VectorEventArgs> DragDelta;

        /// <summary>
        /// DragCompleted event
        /// </summary>
        event EventHandler<VectorEventArgs> DragCompleted;

        /// <summary>
        /// DoubleTapped event
        /// </summary>
        event EventHandler<RoutedEventArgs> DoubleTapped;
    }
}
