using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1

    /// <summary>
    /// move thumb control
    /// </summary>
    public class MoveThumb : Thumb
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(Thumb);

        private RotateTransform _rotateTransform;

        private Control _designerItem;

        /// <summary>
        /// registers DragStarted, DragDelta
        /// </summary>
        public MoveThumb()
        {
            DragStarted += MoveThumb_DragStarted;
            DragDelta += MoveThumb_DragDelta;
        }

        /// <summary>
        /// sets the rotatateTransform from the contentcontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveThumb_DragStarted(object sender, VectorEventArgs e)
        {
            _designerItem = DataContext as Control;

            if (_designerItem != null)
            {
                _rotateTransform = _designerItem.RenderTransform as RotateTransform;
            }
        }

        /// <summary>
        /// sets the canvas through the dragDelta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveThumb_DragDelta(object sender, VectorEventArgs e)
        {
            if (_designerItem != null)
            {
                Point dragDelta = new Point(e.Vector.X, e.Vector.Y);

                if (_rotateTransform != null)
                {
                    //TODO Fix me?
                    //dragDelta = this.rotateTransform.Transform(dragDelta);
                }

                Canvas.SetLeft(_designerItem, Canvas.GetLeft(_designerItem) + dragDelta.X);
                Canvas.SetTop(_designerItem, Canvas.GetTop(_designerItem) + dragDelta.Y);
                
                Canvas.SetRight(_designerItem, Canvas.GetLeft(_designerItem) + _designerItem.Width);
                Canvas.SetBottom(_designerItem, Canvas.GetTop(_designerItem) + _designerItem.Height);



            }
        }
    }
}
