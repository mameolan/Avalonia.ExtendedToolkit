using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/ControlzEx/ControlzEx
    //and 
    //ported from https://github.com/MahApps/MahApps.Metro


    public class Badged : ContentControl
    {
        public const string BadgeContainerPartName = "PART_BadgeContainer";

        public Type StyleKey => typeof(Badged);

        /// <summary>
        /// Gets or sets the Badge content to display.
        /// </summary>
        public object Badge
        {
            get { return (object)GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        public static readonly StyledProperty<object> BadgeProperty =
            AvaloniaProperty.Register<Badged, object>(nameof(Badge));

        /// <summary>
        /// Gets or sets the background brush for the Badge.
        /// </summary>
        public SolidColorBrush BadgeBackground
        {
            get { return (SolidColorBrush)GetValue(BadgeBackgroundProperty); }
            set { SetValue(BadgeBackgroundProperty, value); }
        }

        public static readonly StyledProperty<SolidColorBrush> BadgeBackgroundProperty =
            AvaloniaProperty.Register<Badged, SolidColorBrush>(nameof(BadgeBackground));

        /// <summary>
        /// Gets or sets the foreground brush for the Badge.
        /// </summary>
        public SolidColorBrush BadgeForeground
        {
            get { return (SolidColorBrush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        public static readonly StyledProperty<SolidColorBrush> BadgeForegroundProperty =
            AvaloniaProperty.Register<Badged, SolidColorBrush>(nameof(BadgeForeground));

        /// <summary>
        /// Gets or sets the placement of the Badge relative to its content.
        /// </summary>
        public BadgePlacementMode BadgePlacementMode
        {
            get { return (BadgePlacementMode)GetValue(BadgePlacementModeProperty); }
            set { SetValue(BadgePlacementModeProperty, value); }
        }

        public static readonly StyledProperty<BadgePlacementMode> BadgePlacementModeProperty =
            AvaloniaProperty.Register<Badged, BadgePlacementMode>(nameof(BadgePlacementMode));

        /// <summary>
        /// Gets or sets a margin which can be used to make minor adjustments to the placement of the Badge.
        /// </summary>
        public Thickness BadgeMargin
        {
            get { return (Thickness)GetValue(BadgeMarginProperty); }
            set { SetValue(BadgeMarginProperty, value); }
        }


        public static readonly StyledProperty<Thickness> BadgeMarginProperty =
            AvaloniaProperty.Register<Badged, Thickness>(nameof(BadgeMargin));

        /// <summary>
        /// Indicates whether the Badge has content to display.
        /// </summary>
        public bool IsBadgeSet
        {
            get { return (bool)GetValue(IsBadgeSetProperty); }
            private set { SetValue(IsBadgeSetProperty, value); }
        }

        public static readonly StyledProperty<bool> IsBadgeSetProperty =
            AvaloniaProperty.Register<Badged, bool>(nameof(IsBadgeSet));




        public static readonly RoutedEvent BadgeChangedEvent =
            RoutedEvent.Register<Badged, RoutedPropertyChangedEventArgs<object>>(nameof(BadgeChanged), RoutingStrategies.Bubble);

        public event EventHandler<RoutedPropertyChangedEventArgs<object>> BadgeChanged
        {
            add { AddHandler(BadgeChangedEvent, value); }
            remove { RemoveHandler(BadgeChangedEvent, value); }
        }




        

        private Control _badgeContainer;

        public Badged()
        {
            BadgeProperty.Changed.AddClassHandler<Badged>((o, e) => OnBadgeChanged(o, e));
        }

        private void OnBadgeChanged(Badged badged, AvaloniaPropertyChangedEventArgs e)
        {
            badged.IsBadgeSet = !string.IsNullOrWhiteSpace(e.NewValue as string) || (e.NewValue != null && !(e.NewValue is string));

            var args = new RoutedPropertyChangedEventArgs<object>(
                e.OldValue,
                e.NewValue)
            { RoutedEvent = BadgeChangedEvent };
            badged.RaiseEvent(args);
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            _badgeContainer = e.NameScope.Get<Control>(BadgeContainerPartName);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        //is not working in avalonia
        //protected override Size ArrangeOverride(Size arrangeBounds)
        //{
        //    var result = base.ArrangeOverride(arrangeBounds);

        //    if (_badgeContainer == null) return result;

        //    var containerDesiredSize = _badgeContainer.DesiredSize;
        //    var location = _badgeContainer.PointToScreen(new Point(0,0));

        //    //    if ((containerDesiredSize.Width <= 0.0 || containerDesiredSize.Height <= 0.0)
        //    //        && !double.IsNaN(_badgeContainer.Width) && !double.IsInfinity(_badgeContainer.Width)
        //    //        && !double.IsNaN(_badgeContainer.Height) && !double.IsInfinity(_badgeContainer.Height))
        //    //    {
        //    //        containerDesiredSize = new Size(_badgeContainer.Width, _badgeContainer.Height);
        //    //    }

        //    //    var h = 0 - Math.Floor(containerDesiredSize.Width / 2);
        //    //    var v = 0 - Math.Floor(containerDesiredSize.Height / 2);
        //    //    //if (containerDesiredSize.Width > 0 && containerDesiredSize.Height > 0)
        //    //    {
        //    //        _badgeContainer.Margin = new Thickness(-3);
        //    //        //_badgeContainer.Margin = new Thickness(h, v, h, v);
        //    //    }
        //    return result;
        //}
    }
}
