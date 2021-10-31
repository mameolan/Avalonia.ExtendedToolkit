using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// main controls which supports rotate and resize of it's content
    /// </summary>
    public partial class ResizeRotateControl : TemplatedControl
    {
        /// <summary>
        /// registers CanResize class handler
        /// </summary>
        public ResizeRotateControl()
        {
            ShowAlwaysSizingProperty.Changed.AddClassHandler<ResizeRotateControl>((o, e) => OnShowAlwaysSizingChanged(o, e));
            WidthProperty.Changed.AddClassHandler<ResizeRotateControl>((o,e)=> OnSizeChanged(o,e));
            HeightProperty.Changed.AddClassHandler<ResizeRotateControl>((o,e)=> OnSizeChanged(o,e));
        }

        private void OnSizeChanged(ResizeRotateControl o, object e)
        {
            UpdateOuterRectPositions();
        }

        private void OnShowAlwaysSizingChanged(ResizeRotateControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (_sizeChrome != null && e.NewValue is bool showSizing)
            {
                _sizeChrome.IsVisible = showSizing;
            }
        }

        /// <summary>
        /// remove the dragcomplete event
        /// </summary>
        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            foreach (var item in _resizeThumbs)
            {
                item.DragCompleted -= OnDragCompleted;
            }

            if (_moveThumb != null)
            {
                _moveThumb.DragCompleted -= MoveThumb_DragCompleted;
            }

            if (_rotateThumb != null)
            {
                _rotateThumb.RotateFinsished -= RotateThumb_RotateFinished;
            }

            base.OnDetachedFromVisualTree(e);
        }

        /// <summary>
        ///  resolves the style of this control
        /// </summary>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _contentControl = e.NameScope.Find<ContentControl>(PART_Content);

            _sizeChrome = e.NameScope.Find<SizeChrome>(PART_SizeChrome);

            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbTopCenter));
            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbLeftCenter));
            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbRightCenter));
            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbBottomCenter));

            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbTopLeft));
            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbTopRight));
            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbBottomLeft));
            _resizeThumbs.Add(e.NameScope.Find<ResizeThumb>(PART_ResizeThumbBottomRight));

            _visualGrid = e.NameScope.Find<Grid>(PART_VisualGrid);
            _thumbGrid = e.NameScope.Find<Grid>(PART_ThumbGrid);

            _moveThumb = e.NameScope.Find<MoveThumb>(PART_MoveThumb);
            _moveThumb.DragCompleted += MoveThumb_DragCompleted;

            _rotateThumb = e.NameScope.Find<RotateThumb>(PART_RotateThumb);

            if (_rotateThumb != null)
            {
                _rotateThumb.RotateFinsished += RotateThumb_RotateFinished;
            }

            foreach (var item in _resizeThumbs)
            {
                item.DragCompleted += OnDragCompleted;
            }

            UpdateOuterRectPositions();
        }
        private void RotateThumb_RotateFinished(object sender, RotatedEventArgs e)
        {
            RotationAngle = e.Angle;

            var args = new RotatedEventArgs
            {
                Angle = e.Angle,
                Vector = e.Vector,
                Route = e.Route,
                Source = e.Source,
                RoutedEvent = RotatedEvent
            };
            RaiseEvent(args);
        }
        private void MoveThumb_DragCompleted(object sender, VectorEventArgs e)
        {
            var dragFinished = new VectorEventArgs();
            dragFinished.RoutedEvent = MovedEvent;
            dragFinished.Route = e.Route;
            dragFinished.Source = e.Source;
            dragFinished.Vector = e.Vector;
            RaiseEvent(dragFinished);

            RaisePositionEvent(e);

        }
        private void RaisePositionEvent(RoutedEventArgs e)
        {
            var positionEvent = new PositionChangedEventArgs();
            positionEvent.Route = e.Route;
            positionEvent.Source = e.Source;
            positionEvent.RoutedEvent = PositionChangedEvent;

            UpdateOuterRectPositions();

            positionEvent.Left = OuterRectLeft;
            positionEvent.Top = OuterRectTop;
            positionEvent.Right = OuterRectRight;
            positionEvent.Bottom = OuterRectBottom;
            RaiseEvent(positionEvent);
        }

        private void UpdateOuterRectPositions()
        {
            if (_contentControl == null)
            {
                return;
            }

            var relativePoint = VisualExtensions.TranslatePoint(_contentControl, new Point(), this);

            double left = Canvas.GetLeft(this);
            double top = Canvas.GetTop(this);
            double right = Canvas.GetRight(this);
            double bottom = Canvas.GetBottom(this);

            if (double.IsNaN(left) && !double.IsNaN(DesiredSize.Width) && DesiredSize.Width > 0 && !double.IsNaN(right))
            {
                left = right - DesiredSize.Width;
            }

            if (double.IsNaN(top) && !double.IsNaN(DesiredSize.Height) && DesiredSize.Height > 0 && !double.IsNaN(bottom))
            {
                top = bottom - DesiredSize.Height;
            }

            if (double.IsNaN(right) && !double.IsNaN(DesiredSize.Width) && DesiredSize.Width > 0 && !double.IsNaN(left))
            {
                right = left + DesiredSize.Width;
            }

            if (double.IsNaN(bottom) && !double.IsNaN(DesiredSize.Height) && DesiredSize.Height > 0 && !double.IsNaN(top))
            {
                bottom = top + DesiredSize.Height;
            }


            OuterRectLeft = left - relativePoint.Value.X;
            OuterRectTop = top - relativePoint.Value.Y;
            OuterRectRight = right - relativePoint.Value.X;
            OuterRectBottom = bottom - relativePoint.Value.Y;
        }
        private void OnDragCompleted(object sender, VectorEventArgs e)
        {
            var dragFinished = new VectorEventArgs();
            dragFinished.RoutedEvent = ResizedEvent;
            dragFinished.Route = e.Route;
            dragFinished.Source = e.Source;
            dragFinished.Vector = e.Vector;
            RaiseEvent(dragFinished);
            RaisePositionEvent(e);
        }

    }
}
