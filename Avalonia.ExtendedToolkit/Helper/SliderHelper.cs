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
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillBrush(IControl element)
        {
            return element.GetValue(ThumbFillBrushProperty);
        }

        /// <summary>
        /// set ThumbFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillBrushProperty, value);
        }

        /// <summary>
        /// ThumbFillHoverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillHoverBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillHoverBrush(IControl element)
        {
            return element.GetValue(ThumbFillHoverBrushProperty);
        }

        /// <summary>
        /// set ThumbFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillHoverBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillHoverBrushProperty, value);
        }

        /// <summary>
        /// ThumbFillPressedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillPressedBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillPressedBrush(IControl element)
        {
            return element.GetValue(ThumbFillPressedBrushProperty);
        }

        /// <summary>
        /// set ThumbFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillPressedBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillPressedBrushProperty, value);
        }

        /// <summary>
        /// ThumbFillDisabledBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ThumbFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("ThumbFillDisabledBrush", typeof(SliderHelper));

        /// <summary>
        /// get ThumbFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetThumbFillDisabledBrush(IControl element)
        {
            return element.GetValue(ThumbFillDisabledBrushProperty);
        }

        /// <summary>
        /// set ThumbFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetThumbFillDisabledBrush(IControl element, IBrush value)
        {
            element.SetValue(ThumbFillDisabledBrushProperty, value);
        }

        /// <summary>
        /// TrackFillBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillBrush(IControl element)
        {
            return element.GetValue(TrackFillBrushProperty);
        }

        /// <summary>
        /// set TrackFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillBrushProperty, value);
        }

        /// <summary>
        /// TrackFillHoverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillHoverBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillHoverBrush(IControl element)
        {
            return element.GetValue(TrackFillHoverBrushProperty);
        }

        /// <summary>
        /// set TrackFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillHoverBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillHoverBrushProperty, value);
        }

        /// <summary>
        /// TrackFillPressedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillPressedBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillPressedBrush(IControl element)
        {
            return element.GetValue(TrackFillPressedBrushProperty);
        }

        /// <summary>
        /// set TrackFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillPressedBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillPressedBrushProperty, value);
        }

        /// <summary>
        /// TrackFillDisabledBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackFillDisabledBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackFillDisabledBrush(IControl element)
        {
            return element.GetValue(TrackFillDisabledBrushProperty);
        }

        /// <summary>
        /// set TrackFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackFillDisabledBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackFillDisabledBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillBrush(IControl element)
        {
            return element.GetValue(TrackValueFillBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillHoverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillHoverBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillHoverBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillHoverBrush(IControl element)
        {
            return element.GetValue(TrackValueFillHoverBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillHoverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillHoverBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillHoverBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillPressedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillPressedBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillPressedBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillPressedBrush(IControl element)
        {
            return element.GetValue(TrackValueFillPressedBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillPressedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillPressedBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillPressedBrushProperty, value);
        }

        /// <summary>
        /// TrackValueFillDisabledBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> TrackValueFillDisabledBrushProperty =
           AvaloniaProperty.RegisterAttached<IControl, IBrush>("TrackValueFillDisabledBrush", typeof(SliderHelper));

        /// <summary>
        /// get TrackValueFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetTrackValueFillDisabledBrush(IControl element)
        {
            return element.GetValue(TrackValueFillDisabledBrushProperty);
        }

        /// <summary>
        /// set TrackValueFillDisabledBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTrackValueFillDisabledBrush(IControl element, IBrush value)
        {
            element.SetValue(TrackValueFillDisabledBrushProperty, value);
        }

        /// <summary>
        /// ChangeValueBy AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<MouseWheelChange> ChangeValueByProperty =
           AvaloniaProperty.RegisterAttached<IControl, MouseWheelChange>("ChangeValueBy", typeof(SliderHelper), defaultValue: MouseWheelChange.SmallChange);

        /// <summary>
        /// get ChangeValueBy
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static MouseWheelChange GetChangeValueBy(IControl element)
        {
            return element.GetValue(ChangeValueByProperty);
        }

        /// <summary>
        /// set ChangeValueBy
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetChangeValueBy(IControl element, MouseWheelChange value)
        {
            element.SetValue(ChangeValueByProperty, value);
        }

        //public static readonly AttachedProperty<TickPlacement> TickPlacementProperty =
        //    AvaloniaProperty.RegisterAttached<IControl, TickPlacement>("TickPlacement", typeof(SliderHelper));

        //public static TickPlacement GetTickPlacement(IControl element)
        //{
        //    return element.GetValue(TickPlacementProperty);
        //}

        //public static void SetTickPlacement(IControl element, TickPlacement value)
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
