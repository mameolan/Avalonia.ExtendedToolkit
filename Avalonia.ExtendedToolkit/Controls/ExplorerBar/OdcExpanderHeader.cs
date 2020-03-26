using System;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// A helper class to specify the header of an OdcExpander.
    /// </summary>
    public class OdcExpanderHeader: ToggleButton
    {
        public Type StyleKey => typeof(OdcExpanderHeader);

        /// <summary>
        /// Gets whether the expand geometry is not null.
        /// </summary>
        public bool HasExpandGeometry
        {
            get { return (bool)GetValue(HasExpandGeometryProperty); }
            set { SetValue(HasExpandGeometryProperty, value); }
        }

        public static readonly StyledProperty<bool> HasExpandGeometryProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, bool>(nameof(HasExpandGeometry));

        /// <summary>
        /// Gets or sets the geometry for the collapse symbol.
        /// </summary>
        public Geometry CollapseGeometry
        {
            get { return (Geometry)GetValue(CollapseGeometryProperty); }
            set { SetValue(CollapseGeometryProperty, value); }
        }

        public static readonly StyledProperty<Geometry> CollapseGeometryProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, Geometry>(nameof(CollapseGeometry));

        /// <summary>
        /// Gets or sets the geometry for the expand symbol.
        /// </summary>
        public Geometry ExpandGeometry
        {
            get { return (Geometry)GetValue(ExpandGeometryProperty); }
            set { SetValue(ExpandGeometryProperty, value); }
        }

        public static readonly StyledProperty<Geometry> ExpandGeometryProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, Geometry>(nameof(ExpandGeometry)
                );

        /// <summary>
        /// Gets or sets the corner radius for the header.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, CornerRadius>(nameof(CornerRadius));

        /// <summary>
        /// Gets or sets whether to display the ellipse arround the collapse/expand symbol.
        /// </summary>
        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowEllipseProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, bool>(nameof(ShowEllipse));

        /// <summary>
        /// for styling the cirecle button
        /// </summary>
        public IBrush CircleButtonStroke
        {
            get { return (IBrush)GetValue(CircleButtonStrokeProperty); }
            set { SetValue(CircleButtonStrokeProperty, value); }
        }

        public static readonly StyledProperty<IBrush> CircleButtonStrokeProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IBrush>(nameof(CircleButtonStroke));

        /// <summary>
        /// for styling the cirecle button
        /// </summary>
        public IBrush CircleButtonFill
        {
            get { return (IBrush)GetValue(CircleButtonFillProperty); }
            set { SetValue(CircleButtonFillProperty, value); }
        }

        public static readonly StyledProperty<IBrush> CircleButtonFillProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IBrush>(nameof(CircleButtonFill));

        /// <summary>
        /// for styling the cirecle button
        /// </summary>
        public IBrush CircleButtonForeground
        {
            get { return (IBrush)GetValue(CircleButtonForegroundProperty); }
            set { SetValue(CircleButtonForegroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> CircleButtonForegroundProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IBrush>(nameof(CircleButtonForeground));

        /// <summary>
        /// Gets or sets the Image to display on the header.
        /// </summary>
        public IImage Image
        {
            get { return (IImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly StyledProperty<IImage> ImageProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IImage>(nameof(Image));

        static OdcExpanderHeader()
        {
            ExpandGeometryProperty.Changed.AddClassHandler<OdcExpanderHeader>((o, e) => CollapseGeometryChangedCallback(o, e));
        }

        private static void CollapseGeometryChangedCallback(OdcExpanderHeader eh, AvaloniaPropertyChangedEventArgs e)
        {
            eh.HasExpandGeometry = e.NewValue != null;
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            RaisePropertyChanged(ExpandGeometryProperty, null, ExpandGeometry);
        }
    }
}
