using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class SliderHelper
    {
        public static readonly AttachedProperty<IBrush> ThumbFillBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillBrush", typeof(SliderHelper));

        public static IBrush GetThumbFillBrush(IControl element)
        {
            return element.GetValue(ThumbFillBrushProperty);
        }

        public static void SetThumbFillBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> ThumbFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillHoverBrush", typeof(SliderHelper));

        public static IBrush GetThumbFillHoverBrush(IControl element)
        {
            return element.GetValue(ThumbFillHoverBrushProperty);
        }

        public static void SetThumbFillHoverBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillHoverBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> ThumbFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillPressedBrush", typeof(SliderHelper));

        public static IBrush GetThumbFillPressedBrush(IControl element)
        {
            return element.GetValue(ThumbFillPressedBrushProperty);
        }

        public static void SetThumbFillPressedBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillPressedBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> ThumbFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillDisabledBrush", typeof(SliderHelper));

        public static IBrush GetThumbFillDisabledBrush(IControl element)
        {
            return element.GetValue(ThumbFillDisabledBrushProperty);
        }

        public static void SetThumbFillDisabledBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillDisabledBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackFillBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillBrush", typeof(SliderHelper));

        public static IBrush GetTrackFillBrush(IControl element)
        {
            return element.GetValue(TrackFillBrushProperty);
        }

        public static void SetTrackFillBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillHoverBrush", typeof(SliderHelper));

        public static IBrush GetTrackFillHoverBrush(IControl element)
        {
            return element.GetValue(TrackFillHoverBrushProperty);
        }

        public static void SetTrackFillHoverBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillHoverBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillPressedBrush", typeof(SliderHelper));

        public static IBrush GetTrackFillPressedBrush(IControl element)
        {
            return element.GetValue(TrackFillPressedBrushProperty);
        }

        public static void SetTrackFillPressedBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillPressedBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillDisabledBrush", typeof(SliderHelper));

        public static IBrush GetTrackFillDisabledBrush(IControl element)
        {
            return element.GetValue(TrackFillDisabledBrushProperty);
        }

        public static void SetTrackFillDisabledBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillDisabledBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackValueFillBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillBrush", typeof(SliderHelper));

        public static IBrush GetTrackValueFillBrush(IControl element)
        {
            return element.GetValue(TrackValueFillBrushProperty);
        }

        public static void SetTrackValueFillBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackValueFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillHoverBrush", typeof(SliderHelper));

        public static IBrush GetTrackValueFillHoverBrush(IControl element)
        {
            return element.GetValue(TrackValueFillHoverBrushProperty);
        }

        public static void SetTrackValueFillHoverBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillHoverBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackValueFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillPressedBrush", typeof(SliderHelper));

        public static IBrush GetTrackValueFillPressedBrush(IControl element)
        {
            return element.GetValue(TrackValueFillPressedBrushProperty);
        }

        public static void SetTrackValueFillPressedBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillPressedBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> TrackValueFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillDisabledBrush", typeof(SliderHelper));

        public static IBrush GetTrackValueFillDisabledBrush(IControl element)
        {
            return element.GetValue(TrackValueFillDisabledBrushProperty);
        }

        public static void SetTrackValueFillDisabledBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillDisabledBrushProperty, value);
        }

        public static readonly AttachedProperty<MouseWheelChange> ChangeValueByProperty =
           AvaloniaProperty.RegisterAttached<IControl, MouseWheelChange>("ChangeValueBy", typeof(SliderHelper), defaultValue: MouseWheelChange.SmallChange);

        public static MouseWheelChange GetChangeValueBy(IControl element)
        {
            return element.GetValue(ChangeValueByProperty);
        }

        public static void SetChangeValueBy(IControl element, MouseWheelChange value)
        {
            element.SetValue(ChangeValueByProperty, value);
        }

        //TODO correct me
        //MouseWheelState missing since there is no such type

    }
}
