using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class Badged : ContentControl
    {
        public const string BadgeContainerPartName = "PART_BadgeContainer";

        public object Badge
        {
            get { return (object)GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        public static readonly AvaloniaProperty BadgeProperty =
            AvaloniaProperty.Register<Badged, object>(nameof(Badge));

        public SolidColorBrush BadgeBackground
        {
            get { return (SolidColorBrush)GetValue(BadgeBackgroundProperty); }
            set { SetValue(BadgeBackgroundProperty, value); }
        }

        public static readonly AvaloniaProperty BadgeBackgroundProperty =
            AvaloniaProperty.Register<Badged, SolidColorBrush>(nameof(BadgeBackground));

        public SolidColorBrush BadgeForeground
        {
            get { return (SolidColorBrush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        public static readonly AvaloniaProperty BadgeForegroundProperty =
            AvaloniaProperty.Register<Badged, SolidColorBrush>(nameof(BadgeForeground));

        public BadgePlacementMode BadgePlacementMode
        {
            get { return (BadgePlacementMode)GetValue(BadgePlacementModeProperty); }
            set { SetValue(BadgePlacementModeProperty, value); }
        }

        public static readonly AvaloniaProperty BadgePlacementModeProperty =
            AvaloniaProperty.Register<Badged, BadgePlacementMode>(nameof(BadgePlacementMode));

        public static readonly RoutedEvent BadgeChangedEvent =
            RoutedEvent.Register<Badged, RoutedPropertyChangedEventArgs<object>>(nameof(BadgeChanged), RoutingStrategies.Bubble);

        public event EventHandler<RoutedPropertyChangedEventArgs<object>> BadgeChanged
        {
            add { AddHandler(BadgeChangedEvent, value); }
            remove { RemoveHandler(BadgeChangedEvent, value); }
        }

        public bool IsBadgeSet
        {
            get { return (bool)GetValue(IsBadgeSetProperty); }
            private set { SetValue(IsBadgeSetProperty, value); }
        }

        public static readonly AvaloniaProperty IsBadgeSetProperty =
            AvaloniaProperty.Register<Badged, bool>(nameof(IsBadgeSet));

        private Control _badgeContainer;

        public Badged()
        {
            BadgeProperty.Changed.AddClassHandler<Badged>((o, e) => OnBadgeChanged(o, e));
        }

        private void OnBadgeChanged(Badged o, AvaloniaPropertyChangedEventArgs e)
        {
            var instance = o;

            instance.IsBadgeSet = !string.IsNullOrWhiteSpace(e.NewValue as string) || (e.NewValue != null && !(e.NewValue is string));

            //todo
            var args = new RoutedPropertyChangedEventArgs<object>(
                e.OldValue,
                e.NewValue)
            { RoutedEvent = BadgeChangedEvent };
            instance.RaiseEvent(args);
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