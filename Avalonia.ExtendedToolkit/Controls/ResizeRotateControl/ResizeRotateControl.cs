using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// main controls which supports rotate and resize of it's content
    /// </summary>
    public class ResizeRotateControl : ContentControl
    {
        private static readonly string PART_SizeChrome = "PART_SizeChrome";
        private static readonly string PART_ResizeThumbTopCenter = "PART_ResizeThumbTopCenter";
        private static readonly string PART_ResizeThumbLeftCenter = "PART_ResizeThumbLeftCenter";
        private static readonly string PART_ResizeThumbRightCenter = "PART_ResizeThumbRightCenter";
        private static readonly string PART_ResizeThumbBottomCenter = "PART_ResizeThumbBottomCenter";
        private static readonly string PART_ResizeThumbTopLeft = "PART_ResizeThumbTopLeft";
        private static readonly string PART_ResizeThumbTopRight = "PART_ResizeThumbTopRight";
        private static readonly string PART_ResizeThumbBottomLeft = "PART_ResizeThumbBottomLeft";
        private static readonly string PART_ResizeThumbBottomRight = "PART_ResizeThumbBottomRight";
        private static readonly string PART_VisualGrid = "PART_VisualGrid";
        private static readonly string PART_ThumbGrid = "PART_ThumbGrid";
        private static readonly string PART_MoveThumb = "PART_MoveThumb";
        private static readonly string PART_RotateThumb = "PART_RotateThumb";

        private List<ResizeThumb> _resizeThumbs = new List<ResizeThumb>();

        private SizeChrome _sizeChrome;
        private Grid _visualGrid;
        private Grid _thumbGrid;

        /// <summary>
        /// Gets or sets IsRotationEnabled.
        /// </summary>
        public bool IsRotationEnabled
        {
            get { return (bool)GetValue(IsRotationEnabledProperty); }
            set { SetValue(IsRotationEnabledProperty, value); }
        }

        /// <summary>
        /// Defines the IsRotationEnabled property.
        /// </summary>
        public static readonly StyledProperty<bool> IsRotationEnabledProperty =
        AvaloniaProperty.Register<ResizeRotateControl, bool>(nameof(IsRotationEnabled), defaultValue: true);

        /// <summary>
        /// Gets or sets ShowAlwaysSizing.
        /// </summary>
        public bool ShowAlwaysSizing
        {
            get { return (bool)GetValue(ShowAlwaysSizingProperty); }
            set { SetValue(ShowAlwaysSizingProperty, value); }
        }

        /// <summary>
        /// Defines the ShowAlwaysSizing property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowAlwaysSizingProperty =
        AvaloniaProperty.Register<ResizeRotateControl, bool>(nameof(ShowAlwaysSizing));

        /// <summary>
        /// Gets or sets ShowOuterRect.
        /// </summary>
        public bool ShowOuterRect
        {
            get { return (bool)GetValue(ShowOuterRectProperty); }
            set { SetValue(ShowOuterRectProperty, value); }
        }

        /// <summary>
        /// Defines the ShowOuterRect property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowOuterRectProperty =
        AvaloniaProperty.Register<ResizeRotateControl, bool>(nameof(ShowOuterRect), defaultValue: true);

        /// <summary>
        /// Gets or sets CanDrag.
        /// </summary>
        public bool CanDrag
        {
            get { return (bool)GetValue(CanDragProperty); }
            set { SetValue(CanDragProperty, value); }
        }

        /// <summary>
        /// Defines the DragFinished routed event.
        /// </summary>
        public static readonly RoutedEvent<VectorEventArgs> MoveFinishedEvent =
        RoutedEvent.Register<ResizeRotateControl, VectorEventArgs>(nameof(MoveFinishedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets MoveFinished eventhandler.
        /// </summary>
        public event EventHandler MoveFinished
        {
            add
            {
                AddHandler(MoveFinishedEvent, value);
            }
            remove
            {
                RemoveHandler(MoveFinishedEvent, value);
            }
        }

        /// <summary>
        /// Defines the ResizeFinished routed event.
        /// </summary>
        public static readonly RoutedEvent<VectorEventArgs> ResizeFinishedEvent =
        RoutedEvent.Register<ResizeRotateControl, VectorEventArgs>(nameof(ResizeFinishedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets ResizeFinished eventhandler.
        /// </summary>
        public event EventHandler ResizeFinished
        {
            add
            {
                AddHandler(ResizeFinishedEvent, value);
            }
            remove
            {
                RemoveHandler(ResizeFinishedEvent, value);
            }
        }

        /// <summary>
        /// Defines the RotateFinsished routed event.
        /// </summary>
        public static readonly RoutedEvent<RotateFinishedEventArgs> RotateFinsishedEvent =
        RoutedEvent.Register<ResizeRotateControl, RotateFinishedEventArgs>
                    (nameof(RotateFinsishedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets RotateFinsished eventhandler.
        /// </summary>
        public event EventHandler<RotateFinishedEventArgs> RotateFinsished
        {
            add
            {
                AddHandler(RotateFinsishedEvent, value);
            }
            remove
            {
                RemoveHandler(RotateFinsishedEvent, value);
            }
        }

        /// <summary>
        /// Defines the CanDrag property.
        /// </summary>
        public static readonly StyledProperty<bool> CanDragProperty =
        AvaloniaProperty.Register<ResizeRotateControl, bool>(nameof(CanDrag), defaultValue: true);

        /// <summary>
        /// Gets or sets CanResize.
        /// </summary>
        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        /// <summary>
        /// Defines the CanResize property.
        /// </summary>
        public static readonly StyledProperty<bool> CanResizeProperty =
        AvaloniaProperty.Register<ResizeRotateControl, bool>(nameof(CanResize), defaultValue: true);

        /// <summary>
        /// Gets or sets ThumbFillBrush.
        /// </summary>
        public IBrush ThumbFillBrush
        {
            get { return (IBrush)GetValue(ThumbFillBrushProperty); }
            set { SetValue(ThumbFillBrushProperty, value); }
        }

        /// <summary>
        /// Defines the ThumbFillBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ThumbFillBrushProperty =
        AvaloniaProperty.Register<ResizeRotateControl, IBrush>(nameof(ThumbFillBrush));

        /// <summary>
        /// Gets or sets ThumbStrokeBrush.
        /// </summary>
        public IBrush ThumbStrokeBrush
        {
            get { return (IBrush)GetValue(ThumbStrokeBrushProperty); }
            set { SetValue(ThumbStrokeBrushProperty, value); }
        }

        /// <summary>
        /// Defines the ThumbStrokeBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ThumbStrokeBrushProperty =
        AvaloniaProperty.Register<ResizeRotateControl, IBrush>(nameof(ThumbStrokeBrush));

        /// <summary>
        /// Gets or sets OuterRectStrokeBrush.
        /// </summary>
        public IBrush OuterRectStrokeBrush
        {
            get { return (IBrush)GetValue(OuterRectStrokeBrushProperty); }
            set { SetValue(OuterRectStrokeBrushProperty, value); }
        }

        /// <summary>
        /// Defines the OuterRectStrokeBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> OuterRectStrokeBrushProperty =
        AvaloniaProperty.Register<ResizeRotateControl, IBrush>(nameof(OuterRectStrokeBrush));

        /// <summary>
        /// Gets or sets OuterRectStrokeThickness.
        /// </summary>
        public double OuterRectStrokeThickness
        {
            get { return (double)GetValue(OuterRectStrokeThicknessProperty); }
            set { SetValue(OuterRectStrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Defines the OuterRectStrokeThickness property.
        /// </summary>
        public static readonly StyledProperty<double> OuterRectStrokeThicknessProperty =
        AvaloniaProperty.Register<ResizeRotateControl, double>(nameof(OuterRectStrokeThickness));

        /// <summary>
        /// Gets or sets SizeChromeTextBrush.
        /// </summary>
        public IBrush SizeChromeTextBrush
        {
            get { return (IBrush)GetValue(SizeChromeTextBrushProperty); }
            set { SetValue(SizeChromeTextBrushProperty, value); }
        }

        /// <summary>
        /// Defines the SizeChromeTextBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> SizeChromeTextBrushProperty =
        AvaloniaProperty.Register<ResizeRotateControl, IBrush>(nameof(SizeChromeTextBrush));

        /// <summary>
        /// registers CanResize class handler
        /// </summary>
        public ResizeRotateControl()
        {
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            foreach (var item in _resizeThumbs)
            {
                item.DragStarted -= OnDragStarted;
                item.DragCompleted -= OnDragCompleted;
            }
            base.OnDetachedFromVisualTree(e);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
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

            var moveThumb = e.NameScope.Find<MoveThumb>(PART_MoveThumb);
            moveThumb.DragCompleted += (o, e) =>
            {
                var dragFinished = new VectorEventArgs();
                dragFinished.RoutedEvent = MoveFinishedEvent;
                dragFinished.Route = e.Route;
                dragFinished.Source = e.Source;
                dragFinished.Vector = e.Vector;
                RaiseEvent(dragFinished);
            };

            var rotateThumb = e.NameScope.Find<RotateThumb>(PART_RotateThumb);

            if (rotateThumb != null)
            {
                rotateThumb.RotateFinsished += (o, e) =>
            {
                var args = new RotateFinishedEventArgs
                {
                    Angle = e.Angle,
                    Vector = e.Vector,
                    Route = e.Route,
                    Source = e.Source,
                    RoutedEvent = RotateFinsishedEvent
                };
                RaiseEvent(args);
            };
            }

            //_visualGrid.IsVisible = _thumbGrid.IsVisible = CanResize;

            //_resizeThumbs.ForEach(x=> x.IsVisible=CanResize);

            foreach (var item in _resizeThumbs)
            {
                item.DragStarted += OnDragStarted;
                item.DragCompleted += OnDragCompleted;
            }
        }

        private void OnDragCompleted(object sender, VectorEventArgs e)
        {
            if (_sizeChrome != null && ShowAlwaysSizing == false)
            {
                _sizeChrome.IsVisible = false;
            }

            var dragFinished = new VectorEventArgs();
            dragFinished.RoutedEvent = ResizeFinishedEvent;
            dragFinished.Route = e.Route;
            dragFinished.Source = e.Source;
            dragFinished.Vector = e.Vector;
            RaiseEvent(dragFinished);
        }

        private void OnDragStarted(object sender, VectorEventArgs e)
        {
            if (_sizeChrome != null && ShowAlwaysSizing == false)
            {
                _sizeChrome.IsVisible = true;
            }
        }
    }
}
