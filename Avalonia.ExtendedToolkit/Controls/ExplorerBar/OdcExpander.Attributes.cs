using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// an arcodion like control
    /// </summary>
    public partial class OdcExpander : HeaderedContentControl
    {
        /// <summary>
        /// inner header
        /// </summary>
        private OdcExpanderHeader _header;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(OdcExpander);

        /// <summary>
        /// get/sets HeaderBorderBrush
        /// </summary>
        public IBrush HeaderBorderBrush
        {
            get { return (IBrush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderBorderBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBorderBrushProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(HeaderBorderBrush), defaultValue: Brushes.Gray);

        /// <summary>
        /// sets the classes to the inner header
        /// </summary>
        public Classes HeaderClasses
        {
            get { return (Classes)GetValue(HeaderClassesProperty); }
            set { SetValue(HeaderClassesProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderClasses"/>
        /// </summary>
        public static readonly StyledProperty<Classes> HeaderClassesProperty =
            AvaloniaProperty.Register<OdcExpander, Classes>(nameof(HeaderClasses));

        /// <summary>
        /// get/sets HeaderBackground
        /// </summary>
        public IBrush HeaderBackground
        {
            get { return (IBrush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderBackground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(HeaderBackground), defaultValue: Brushes.Silver);

        /// <summary>
        /// get/sets IsMinimized
        /// </summary>
        public bool IsMinimized
        {
            get { return (bool)GetValue(IsMinimizedProperty); }
            set { SetValue(IsMinimizedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsMinimized"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsMinimizedProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsMinimized), defaultValue: false);

        /// <summary>
        /// Gets or sets the ImageSource for the image in the header.
        /// </summary>
        public Bitmap Image
        {
            get { return (Bitmap)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// <see cref="Image"/>
        /// </summary>
        public static readonly StyledProperty<Bitmap> ImageProperty =
            AvaloniaProperty.Register<OdcExpander, Bitmap>(nameof(Image));

        /// <summary>
        /// get/sets IsExpanded
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsExpanded"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsExpanded), defaultValue: true);

        /// <summary>
        /// Gets or sets the corner radius for the header.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// <see cref="CornerRadius"/>
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<OdcExpander, CornerRadius>(nameof(CornerRadius));

        /// <summary>
        /// Gets or sets the background color of the header on mouse over.
        /// </summary>
        public IBrush MouseOverHeaderBackground
        {
            get { return (IBrush)GetValue(MouseOverHeaderBackgroundProperty); }
            set { SetValue(MouseOverHeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="MouseOverHeaderBackground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MouseOverHeaderBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(MouseOverHeaderBackground));

        /// <summary>
        /// Gets whether the PressedBackground is not null.
        /// </summary>
        public bool HasPressedBackground
        {
            get { return (bool)GetValue(HasPressedBackgroundProperty); }
            set { SetValue(HasPressedBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="HasPressedBackground"/>
        /// </summary>
        public static readonly StyledProperty<bool> HasPressedBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(HasPressedBackground));

        /// <summary>
        /// Gets or sets the background color of the header in pressed mode.
        /// </summary>
        public IBrush PressedHeaderBackground
        {
            get { return (IBrush)GetValue(PressedHeaderBackgroundProperty); }
            set { SetValue(PressedHeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="PressedHeaderBackground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> PressedHeaderBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(PressedHeaderBackground));

        /// <summary>
        /// get/sets HeaderBorderThickness
        /// </summary>
        public Thickness HeaderBorderThickness
        {
            get { return (Thickness)GetValue(HeaderBorderThicknessProperty); }
            set { SetValue(HeaderBorderThicknessProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderBorderThickness"/>
        /// </summary>
        public static readonly StyledProperty<Thickness> HeaderBorderThicknessProperty =
            AvaloniaProperty.Register<OdcExpander, Thickness>(nameof(HeaderBorderThickness));

        /// <summary>
        /// Gets or sets the foreground color of the header on mouse over.
        /// </summary>
        public IBrush MouseOverHeaderForeground
        {
            get { return (IBrush)GetValue(MouseOverHeaderForegroundProperty); }
            set { SetValue(MouseOverHeaderForegroundProperty, value); }
        }

        /// <summary>
        /// <see cref="MouseOverHeaderForeground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MouseOverHeaderForegroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(MouseOverHeaderForeground));

        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowEllipse"/>
        /// </summary>
        public static readonly StyledProperty<bool> ShowEllipseProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(ShowEllipse));

        /// <summary>
        /// Gets or sets whether animation is possible
        /// </summary>
        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        /// <summary>
        /// <see cref="CanAnimate"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanAnimateProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(CanAnimate), defaultValue: true);

        /// <summary>
        /// get/set IsHeaderVisible
        /// </summary>
        public bool IsHeaderVisible
        {
            get { return (bool)GetValue(IsHeaderVisibleProperty); }
            set { SetValue(IsHeaderVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsHeaderVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsHeaderVisibleProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsHeaderVisible), defaultValue: true);

        /// <summary>
        /// get/sets IsPressed
        /// </summary>
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsPressed"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsPressedProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsPressed));

        /// <summary>
        /// <see cref="Expanded"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ExpandedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(ExpandedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Expanded eventhandler
        /// </summary>
        public event EventHandler Expanded
        {
            add
            {
                AddHandler(ExpandedEvent, value);
            }
            remove
            {
                RemoveHandler(ExpandedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="Collapsed"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> CollapsedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(CollapsedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Collapsed Eventhandler
        /// </summary>
        public event EventHandler Collapsed
        {
            add
            {
                AddHandler(CollapsedEvent, value);
            }
            remove
            {
                RemoveHandler(CollapsedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="Minimized"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> MinimizedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(MinimizedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Minimized Event handler
        /// </summary>
        public event EventHandler Minimized
        {
            add
            {
                AddHandler(MinimizedEvent, value);
            }
            remove
            {
                RemoveHandler(MinimizedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="Maximized"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> MaximizedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(MaximizedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Maximized event handler
        /// </summary>
        public event EventHandler Maximized
        {
            add
            {
                AddHandler(MaximizedEvent, value);
            }
            remove
            {
                RemoveHandler(MaximizedEvent, value);
            }
        }
    }
}
