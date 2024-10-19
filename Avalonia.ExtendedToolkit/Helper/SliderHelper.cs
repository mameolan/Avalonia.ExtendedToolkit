using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Slider attached properties
    /// </summary>
    public class SliderHelper
    {
        /// <summary>
        /// ThumbFillBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("ThumbFillBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillBrush(Control element)
        {
            return element.GetValue<IBrush>(ThumbFillBrushProperty);
        }

        /// <summary>
        /// set ThumbFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillBrush(Control element, IBrush value)
        {
            element.SetValue(ThumbFillBrushProperty, value);
        }

        /// <summary>
        /// ThumbFillHoverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("ThumbFillHoverBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillHoverBrush(Control element)
        {
            return element.GetValue<IBrush>(ThumbFillHoverBrushProperty);
        }

        /// <summary>
        /// set ThumbFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillHoverBrush(Control element, IBrush value)
        {
            element.SetValue(ThumbFillHoverBrushProperty, value);
        }

        /// <summary>
        /// ThumbFillPressedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("ThumbFillPressedBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillPressedBrush(Control element)
        {
            return element.GetValue<IBrush>(ThumbFillPressedBrushProperty);
        }

        /// <summary>
        /// set ThumbFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillPressedBrush(Control element, IBrush value)
        {
            element.SetValue(ThumbFillPressedBrushProperty, value);
        }

        /// <summary>
        /// ThumbFillDisabledBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("ThumbFillDisabledBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillDisabledBrush(Control element)
        {
            return element.GetValue<IBrush>(ThumbFillDisabledBrushProperty);
        }

        /// <summary>
        /// set ThumbFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillDisabledBrush(Control element, IBrush value)
        {
            element.SetValue(ThumbFillDisabledBrushProperty, value);
        }

        /// <summary>
        /// TrackFillBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackFillBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackFillBrushProperty);
        }

        /// <summary>
        /// set TrackFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillBrush(Control element, IBrush value)
        {
            element.SetValue(TrackFillBrushProperty, value);
        }

        /// <summary>
        /// TrackFillHoverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackFillHoverBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillHoverBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackFillHoverBrushProperty);
        }

        /// <summary>
        /// set TrackFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillHoverBrush(Control element, IBrush value)
        {
            element.SetValue(TrackFillHoverBrushProperty, value);
        }

        /// <summary>
        /// TrackFillPressedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackFillPressedBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillPressedBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackFillPressedBrushProperty);
        }

        /// <summary>
        /// set TrackFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillPressedBrush(Control element, IBrush value)
        {
            element.SetValue(TrackFillPressedBrushProperty, value);
        }

        /// <summary>
        /// TrackFillDisabledBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackFillDisabledBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillDisabledBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackFillDisabledBrushProperty);
        }

        /// <summary>
        /// set TrackFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillDisabledBrush(Control element, IBrush value)
        {
            element.SetValue(TrackFillDisabledBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackValueFillBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackValueFillBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillBrush(Control element, IBrush value)
        {
            element.SetValue(TrackValueFillBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillHoverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackValueFillHoverBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillHoverBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackValueFillHoverBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillHoverBrush(Control element, IBrush value)
        {
            element.SetValue(TrackValueFillHoverBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillPressedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackValueFillPressedBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillPressedBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackValueFillPressedBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillPressedBrush(Control element, IBrush value)
        {
            element.SetValue(TrackValueFillPressedBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillDisabledBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<Control, IBrush>("TrackValueFillDisabledBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillDisabledBrush(Control element)
        {
            return element.GetValue<IBrush>(TrackValueFillDisabledBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillDisabledBrush(Control element, IBrush value)
        {
            element.SetValue(TrackValueFillDisabledBrushProperty, value);
        }

        /// <summary>
        /// ChangeValueBy AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<MouseWheelChange> ChangeValueByProperty =
           AvaloniaProperty.RegisterAttached<Control, MouseWheelChange>("ChangeValueBy", typeof(SliderHelper), defaultValue: MouseWheelChange.SmallChange);

        /// <summary>
        /// get ChangeValueBy
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static MouseWheelChange GetChangeValueBy(Control element)
        {
            return element.GetValue<MouseWheelChange>(ChangeValueByProperty);
        }

        /// <summary>
        /// set ChangeValueBy
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetChangeValueBy(Control element, MouseWheelChange value)
        {
            element.SetValue(ChangeValueByProperty, value);
        }

        //public static readonly AttachedProperty<TickPlacement> TickPlacementProperty =
        //    AvaloniaProperty.RegisterAttached<Control, TickPlacement>("TickPlacement", typeof(SliderHelper));

        //public static TickPlacement GetTickPlacement(Control element)
        //{
        //    return element.GetValue(TickPlacementProperty);
        //}

        //public static void SetTickPlacement(Control element, TickPlacement value)
        //{
        //    element.SetValue(TickPlacementProperty, value);
        //    UpdatePseudoClass((StyledElement)element, value);
        //}

        //private static void UpdatePseudoClass(StyledElement element, TickPlacement value)
        //{
        //    var property = element.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).FirstOrDefault(x => x.PropertyType == typeof(IPseudoClasses));

        //    Classes propertyValue = property.GetValue(element) as Classes;
        //    //Classes classes = new Classes(propertyValue);

        //    //foreach (string enumValue in Enum.GetNames(typeof(TickBarPlacement)))
        //    //{
        //    //    var itemToRemove = propertyValue.FirstOrDefault(x => x.Contains(enumValue));
        //    //    if (itemToRemove != null)
        //    //    {
        //    //        classes.Remove(itemToRemove);
        //    //    }
        //    //}

        //    propertyValue.Set($":TickPlacement_None", value == TickPlacement.None);
        //    propertyValue.Set($":TickPlacement_Both",value== TickPlacement.Both);
        //    propertyValue.Set($":TickPlacement_BottomRight", value == TickPlacement.BottomRight);
        //    propertyValue.Set($":TickPlacement_TopLeft", value == TickPlacement.TopLeft);
        //    //property.SetValue(element, propertyValue);

        //}

        //TODO correct me
        //MouseWheelState missing since there is no such type
    }
}
