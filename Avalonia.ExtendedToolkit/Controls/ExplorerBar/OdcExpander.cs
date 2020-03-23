using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class OdcExpander : HeaderedContentControl
    {
        private OdcExpanderHeader _header;

        public Type StyleKey => typeof(OdcExpander);

        public IBrush HeaderBorderBrush
        {
            get { return (IBrush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> HeaderBorderBrushProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(HeaderBorderBrush), defaultValue: Brushes.Gray);

        public Classes HeaderClasses
        {
            get { return (Classes)GetValue(HeaderClassesProperty); }
            set { SetValue(HeaderClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> HeaderClassesProperty =
            AvaloniaProperty.Register<OdcExpander, Classes>(nameof(HeaderClasses));

        public IBrush HeaderBackground
        {
            get { return (IBrush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(HeaderBackground), defaultValue: Brushes.Silver);

        public bool IsMinimized
        {
            get { return (bool)GetValue(IsMinimizedProperty); }
            set { SetValue(IsMinimizedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsMinimizedProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsMinimized), defaultValue: false);

        public IImage Image
        {
            get { return (IImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly StyledProperty<IImage> ImageProperty =
            AvaloniaProperty.Register<OdcExpander, IImage>(nameof(Image));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsExpanded), defaultValue: true);

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<OdcExpander, CornerRadius>(nameof(CornerRadius));

        public IBrush MouseOverHeaderBackground
        {
            get { return (IBrush)GetValue(MouseOverHeaderBackgroundProperty); }
            set { SetValue(MouseOverHeaderBackgroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MouseOverHeaderBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(MouseOverHeaderBackground));

        public bool HasPressedBackground
        {
            get { return (bool)GetValue(HasPressedBackgroundProperty); }
            set { SetValue(HasPressedBackgroundProperty, value); }
        }

        public static readonly StyledProperty<bool> HasPressedBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(HasPressedBackground));

        public IBrush PressedHeaderBackground
        {
            get { return (IBrush)GetValue(PressedHeaderBackgroundProperty); }
            set { SetValue(PressedHeaderBackgroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> PressedHeaderBackgroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(PressedHeaderBackground));

        public Thickness HeaderBorderThickness
        {
            get { return (Thickness)GetValue(HeaderBorderThicknessProperty); }
            set { SetValue(HeaderBorderThicknessProperty, value); }
        }

        public static readonly StyledProperty<Thickness> HeaderBorderThicknessProperty =
            AvaloniaProperty.Register<OdcExpander, Thickness>(nameof(HeaderBorderThickness));

        public IBrush MouseOverHeaderForeground
        {
            get { return (IBrush)GetValue(MouseOverHeaderForegroundProperty); }
            set { SetValue(MouseOverHeaderForegroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MouseOverHeaderForegroundProperty =
            AvaloniaProperty.Register<OdcExpander, IBrush>(nameof(MouseOverHeaderForeground));

        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowEllipseProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(ShowEllipse));

        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        public static readonly StyledProperty<bool> CanAnimateProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(CanAnimate), defaultValue: true);

        public bool IsHeaderVisible
        {
            get { return (bool)GetValue(IsHeaderVisibleProperty); }
            set { SetValue(IsHeaderVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsHeaderVisibleProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsHeaderVisible), defaultValue: true);

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsPressedProperty =
            AvaloniaProperty.Register<OdcExpander, bool>(nameof(IsPressed));
               
        public static readonly RoutedEvent<RoutedEventArgs> ExpandedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(ExpandedEvent), RoutingStrategies.Bubble);

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

        public static readonly RoutedEvent<RoutedEventArgs> CollapsedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(CollapsedEvent), RoutingStrategies.Bubble);

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

        public static readonly RoutedEvent<RoutedEventArgs> MinimizedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(MinimizedEvent), RoutingStrategies.Bubble);

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

        public static readonly RoutedEvent<RoutedEventArgs> MaximizedEvent =
                    RoutedEvent.Register<OdcExpander, RoutedEventArgs>(nameof(MaximizedEvent), RoutingStrategies.Bubble);

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

        static OdcExpander()
        {
            MarginProperty.OverrideDefaultValue<OdcExpander>(new Thickness(10, 10, 10, 2));
            FocusableProperty.OverrideDefaultValue<OdcExpander>(false);

            IsMinimizedProperty.Changed.AddClassHandler<OdcExpander>((o, e) => IsMinimizedChanged(o, e));
            IsExpandedProperty.Changed.AddClassHandler<OdcExpander>((o, e) => IsExpandedChanged(o, e));
            PressedHeaderBackgroundProperty.Changed.AddClassHandler<OdcExpander>((o, e) => PressedHeaderBackgroundPropertyChangedCallback(o, e));

            HeaderClassesProperty.Changed.AddClassHandler<OdcExpander>((o, e) => HeaderClassesChanged(o, e));
        }

        private static void HeaderClassesChanged(OdcExpander o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is Classes && o._header != null)
            {
                Classes classes = e.NewValue as Classes;

                foreach(var item in classes)
                {
                    if(o._header.Classes.Contains(item)==false)
                    {
                        o._header.Classes.Add(item);
                    }
                }
            }
        }

        public OdcExpander()
        {
            PointerPressed += (o, e) =>
            {
                IsPressed = true;
            };

            PointerReleased += (o, e) =>
            {
                IsPressed = false;
            };
        }

        private static void PressedHeaderBackgroundPropertyChangedCallback(OdcExpander expander, AvaloniaPropertyChangedEventArgs e)
        {
            expander.HasPressedBackground = e.NewValue != null;
        }

        private static void IsExpandedChanged(OdcExpander expander, AvaloniaPropertyChangedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs((bool)e.NewValue ? ExpandedEvent : CollapsedEvent);
            expander.RaiseEvent(args);
        }

        private static void IsMinimizedChanged(OdcExpander expander, AvaloniaPropertyChangedEventArgs e)
        {
            bool minimized = (bool)e.NewValue;

            expander.IsEnabled = !minimized;
            RoutedEventArgs args = new RoutedEventArgs(minimized ? MinimizedEvent : MaximizedEvent);
            expander.RaiseEvent(args);
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            _header = e.NameScope.Find<OdcExpanderHeader>("PART_HEADER");
            //ExpanderHeaderHight = _header.Height;
            //ExpanderHeaderWidth = _header.Width;


            base.OnTemplateApplied(e);

            RaisePropertyChanged(HeaderClassesProperty, null, HeaderClasses);
        }
    }
}
