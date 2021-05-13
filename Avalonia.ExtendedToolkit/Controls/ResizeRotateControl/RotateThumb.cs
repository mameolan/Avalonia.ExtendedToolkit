using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1

    /// <summary>
    /// thumb for the rotation
    /// </summary>
    public class RotateThumb : Thumb
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(Thumb);

        private Point _currentPoint;

        private double _initialAngle;
        private RotateTransform _rotateTransform;
        private Vector _startVector;
        private Point _centerPoint;
        private ContentControl _designerItem;
        private Canvas _canvas;

        public RotateThumb()
        {
            DragDelta += RotateThumb_DragDelta;
            DragStarted += RotateThumb_DragStarted;
        }

        /// <summary>
        /// remembers the current point if left button down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);
            var currentPointerPoint = e.GetCurrentPoint(this);

            if (currentPointerPoint.Properties.IsLeftButtonPressed &&
                _designerItem != null && _canvas != null)
            {
                _currentPoint = e.GetPosition(_canvas);
            }
        }

        /// <summary>
        /// sets the angle to the rotate transform
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateThumb_DragDelta(object sender, VectorEventArgs e)
        {
            if (_designerItem != null && _canvas != null)
            {
                Vector deltaVector = _currentPoint - _centerPoint;
                double angle = VectorExtension.AngleBetween(_startVector, deltaVector);

                RotateTransform rotateTransform = _designerItem.RenderTransform as RotateTransform;
                rotateTransform.Angle = _initialAngle + Math.Round(angle, 0);
                _designerItem.InvalidateMeasure();
            }
        }

        /// <summary>
        /// sets the inital angle if datacontext is a ContentControl and if the Canvas was found.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateThumb_DragStarted(object sender, VectorEventArgs e)
        {
            _designerItem = DataContext as ContentControl;

            if (_designerItem != null)
            {
                _canvas = TreeExtensions.TryFindParent<Canvas>(_designerItem);
                if (_canvas != null)
                {
                    _centerPoint = (Point)_designerItem.TranslatePoint(
                        new Point(_designerItem.Width * _designerItem.RenderTransformOrigin.Point.X,
                                  _designerItem.Height * _designerItem.RenderTransformOrigin.Point.Y),
                                  _canvas);

                    Point startPoint = VisualExtensions.
                    PointToClient(_canvas, new PixelPoint((int)e.Vector.X, (int)e.Vector.Y));

                    _startVector = startPoint - _centerPoint;

                    _rotateTransform = _designerItem.RenderTransform as RotateTransform;
                    if (_rotateTransform == null)
                    {
                        _designerItem.RenderTransform = new RotateTransform(0);
                        _initialAngle = 0;
                    }
                    else
                    {
                        _initialAngle = _rotateTransform.Angle;
                    }
                }
            }
        }
    }
}
