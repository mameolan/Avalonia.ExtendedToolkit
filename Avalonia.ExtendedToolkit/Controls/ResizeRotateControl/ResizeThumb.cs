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
    /// Thumb for the resize
    /// </summary>
    public class ResizeThumb : Thumb
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(Thumb);

        private RotateTransform _rotateTransform;
        private double _angle;
        private RelativePoint _transformOrigin;
        private Control _designerItem;
        private Canvas _canvas;

        /// <summary>
        /// Gets or sets StrokeBrush.
        /// </summary>
        public IBrush StrokeBrush
        {
            get { return (IBrush)GetValue(StrokeBrushProperty); }
            set { SetValue(StrokeBrushProperty, value); }
        }

        /// <summary>
        /// Defines the StrokeBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> StrokeBrushProperty =
        AvaloniaProperty.Register<ResizeThumb, IBrush>(nameof(StrokeBrush));

        /// <summary>
        /// Gets or sets FillBrush.
        /// </summary>
        public IBrush FillBrush
        {
            get { return (IBrush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }

        /// <summary>
        /// Defines the FillBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> FillBrushProperty =
        AvaloniaProperty.Register<ResizeThumb, IBrush>(nameof(FillBrush));

        public ResizeThumb()
        {
            DragStarted += ResizeThumb_DragStarted;
            DragDelta += ResizeThumb_DragDelta;
        }

        /// <summary>
        /// sets the Angle tot he rotate transform if not null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeThumb_DragStarted(object sender, VectorEventArgs e)
        {
            _designerItem = DataContext as Control;

            if (_designerItem != null)
            {
                _canvas = TreeExtensions.TryFindParent<Canvas>(_designerItem);
                if (_canvas != null)
                {
                    _transformOrigin = _designerItem.RenderTransformOrigin;

                    _rotateTransform = _designerItem.RenderTransform as RotateTransform;
                    if (_rotateTransform != null)
                    {
                        _angle = _rotateTransform.Angle * Math.PI / 180.0;
                    }
                    else
                    {
                        _angle = 0.0d;
                    }
                }
            }
        }

        /// <summary>
        /// sets the size to the content control if not null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeThumb_DragDelta(object sender, VectorEventArgs e)
        {
            if (_designerItem != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (VerticalAlignment)
                {
                    case Avalonia.Layout.VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.Vector.Y, _designerItem.DesiredSize.Height - _designerItem.MinHeight);
                        Canvas.SetTop(_designerItem, Canvas.GetTop(_designerItem) + (_transformOrigin.Point.Y * deltaVertical * (1 - Math.Cos(-_angle))));
                        Canvas.SetLeft(_designerItem, Canvas.GetLeft(_designerItem) - deltaVertical * _transformOrigin.Point.Y * Math.Sin(-_angle));
                        _designerItem.Height -= deltaVertical;
                        break;

                    case Avalonia.Layout.VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.Vector.Y, _designerItem.DesiredSize.Height - _designerItem.MinHeight);
                        Canvas.SetTop(_designerItem, Canvas.GetTop(_designerItem) + deltaVertical * Math.Cos(-_angle) + (_transformOrigin.Point.Y * deltaVertical * (1 - Math.Cos(-_angle))));
                        Canvas.SetLeft(_designerItem, Canvas.GetLeft(_designerItem) + deltaVertical * Math.Sin(-_angle) - (_transformOrigin.Point.Y * deltaVertical * Math.Sin(-_angle)));
                        _designerItem.Height -= deltaVertical;
                        break;

                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case Avalonia.Layout.HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.Vector.X, _designerItem.DesiredSize.Width - _designerItem.MinWidth);
                        Canvas.SetTop(_designerItem, Canvas.GetTop(_designerItem) + deltaHorizontal * Math.Sin(_angle) - _transformOrigin.Point.X * deltaHorizontal * Math.Sin(_angle));
                        Canvas.SetLeft(_designerItem, Canvas.GetLeft(_designerItem) + deltaHorizontal * Math.Cos(_angle) + (_transformOrigin.Point.X * deltaHorizontal * (1 - Math.Cos(_angle))));
                        _designerItem.Width -= deltaHorizontal;
                        break;

                    case Avalonia.Layout.HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.Vector.X, _designerItem.DesiredSize.Width - _designerItem.MinWidth);
                        Canvas.SetTop(_designerItem, Canvas.GetTop(_designerItem) - _transformOrigin.Point.X * deltaHorizontal * Math.Sin(_angle));
                        Canvas.SetLeft(_designerItem, Canvas.GetLeft(_designerItem) + (deltaHorizontal * _transformOrigin.Point.X * (1 - Math.Cos(_angle))));
                        _designerItem.Width -= deltaHorizontal;
                        break;

                    default:
                        break;
                }
                double left = Canvas.GetLeft(_designerItem);
                double top = Canvas.GetTop(_designerItem);
                if (double.IsNaN(left))
                {
                    left = 0;
                }
                if(double.IsNaN(top))
                {
                    top=0;
                }

                Canvas.SetRight(_designerItem, left+_designerItem.Width);
                Canvas.SetBottom(_designerItem, top + _designerItem.Height);
            }

            e.Handled = true;
        }
    }
}
