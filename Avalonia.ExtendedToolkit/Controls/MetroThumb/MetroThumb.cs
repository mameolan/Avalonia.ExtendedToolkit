using System;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// metro thumb
    /// </summary>
    public class MetroThumb : Thumb, IMetroThumb
    {
        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(MetroThumb);

        /// <summary>
        /// Indicates that the left mouse button is
        /// pressed and is over the MetroThumbContentControl.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        /// <summary>
        /// <see cref="IsDragging"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsDraggingProperty =
            AvaloniaProperty.Register <MetroThumb, bool>(nameof(IsDragging));

        event EventHandler<RoutedEventArgs> IMetroThumb.DoubleTapped
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// set is dragging to true
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragStarted(VectorEventArgs e)
        {
            IsDragging = true;
            base.OnDragStarted(e);
        }

        /// <summary>
        /// set is dragging to false
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragCompleted(VectorEventArgs e)
        {
            IsDragging = false;
            base.OnDragCompleted(e);
        }
    }
}
