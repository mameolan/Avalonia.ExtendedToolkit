using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroThumb : Thumb, IMetroThumb
    {


        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }


        public static readonly AvaloniaProperty IsDraggingProperty =
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