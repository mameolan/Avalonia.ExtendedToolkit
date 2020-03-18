using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class OdcExpanderHeader: ToggleButton
    {
        public Type StyleKey => typeof(OdcExpanderHeader);

        public bool HasExpandGeometry
        {
            get { return (bool)GetValue(HasExpandGeometryProperty); }
            set { SetValue(HasExpandGeometryProperty, value); }
        }

        public static readonly StyledProperty<bool> HasExpandGeometryProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, bool>(nameof(HasExpandGeometry));

        public Geometry CollapseGeometry
        {
            get { return (Geometry)GetValue(CollapseGeometryProperty); }
            set { SetValue(CollapseGeometryProperty, value); }
        }

        public static readonly StyledProperty<Geometry> CollapseGeometryProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, Geometry>(nameof(CollapseGeometry));

        public Geometry ExpandGeometry
        {
            get { return (Geometry)GetValue(ExpandGeometryProperty); }
            set { SetValue(ExpandGeometryProperty, value); }
        }

        public static readonly StyledProperty<Geometry> ExpandGeometryProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, Geometry>(nameof(ExpandGeometry)
                );

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, CornerRadius>(nameof(CornerRadius));

        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowEllipseProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, bool>(nameof(ShowEllipse));




        public IBrush CircleButtonStroke
        {
            get { return (IBrush)GetValue(CircleButtonStrokeProperty); }
            set { SetValue(CircleButtonStrokeProperty, value); }
        }


        public static readonly StyledProperty<IBrush> CircleButtonStrokeProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IBrush>(nameof(CircleButtonStroke));



        public IBrush CircleButtonFill
        {
            get { return (IBrush)GetValue(CircleButtonFillProperty); }
            set { SetValue(CircleButtonFillProperty, value); }
        }


        public static readonly StyledProperty<IBrush> CircleButtonFillProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IBrush>(nameof(CircleButtonFill));



        public IBrush CircleButtonForeground
        {
            get { return (IBrush)GetValue(CircleButtonForegroundProperty); }
            set { SetValue(CircleButtonForegroundProperty, value); }
        }


        public static readonly StyledProperty<IBrush> CircleButtonForegroundProperty =
            AvaloniaProperty.Register<OdcExpanderHeader, IBrush>(nameof(CircleButtonForeground));







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