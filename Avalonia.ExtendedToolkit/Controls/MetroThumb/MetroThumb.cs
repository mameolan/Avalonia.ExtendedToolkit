using System;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public class MetroThumb : Thumb, IMetroThumb
    {
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

        public static readonly StyledProperty<bool> IsDraggingProperty =
            AvaloniaProperty.Register <MetroThumb, bool>(nameof(IsDragging));

        protected override void OnDragStarted(VectorEventArgs e)
        {
            IsDragging = true;
            base.OnDragStarted(e);
        }

        protected override void OnDragCompleted(VectorEventArgs e)
        {
            IsDragging = false;
            base.OnDragCompleted(e);
        }
    }
}
