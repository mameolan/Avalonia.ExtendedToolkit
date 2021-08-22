using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

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
        /// Gets or sets CanDrag.
        /// </summary>
        public bool CanDrag
        {
            get { return (bool)GetValue(CanDragProperty); }
            set { SetValue(CanDragProperty, value); }
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
