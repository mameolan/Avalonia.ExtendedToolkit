using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// item attached properties
    /// </summary>
    public static class ItemHelper
    {
        /// <summary>
        /// ActiveSelectionBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ActiveSelectionBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("ActiveSelectionBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get ActiveSelectionBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetActiveSelectionBackgroundBrush(Control element)
        {
            return element.GetValue<IBrush>(ActiveSelectionBackgroundBrushProperty);
        }

        /// <summary>
        /// set ActiveSelectionBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetActiveSelectionBackgroundBrush(Control element, IBrush value)
        {
            element.SetValue(ActiveSelectionBackgroundBrushProperty, value);
        }

        /// <summary>
        /// ActiveSelectionForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ActiveSelectionForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("ActiveSelectionForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get ActiveSelectionForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetActiveSelectionForegroundBrush(Control element)
        {
            return element.GetValue<IBrush>(ActiveSelectionForegroundBrushProperty);
        }

        /// <summary>
        /// set ActiveSelectionForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetActiveSelectionForegroundBrush(Control element, IBrush value)
        {
            element.SetValue(ActiveSelectionForegroundBrushProperty, value);
        }

        /// <summary>
        /// SelectedBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> SelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("SelectedBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get SelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetSelectedBackgroundBrush(Control element)
        {
            return element.GetValue<IBrush>(SelectedBackgroundBrushProperty);
        }

        /// <summary>
        /// set SelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSelectedBackgroundBrush(Control element, IBrush value)
        {
            element.SetValue(SelectedBackgroundBrushProperty, value);
        }

        /// <summary>
        /// SelectedForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> SelectedForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("SelectedForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get SelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetSelectedForegroundBrush(Control element)
        {
            return element.GetValue<IBrush>(SelectedForegroundBrushProperty);
        }

        /// <summary>
        /// set SelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSelectedForegroundBrush(Control element, IBrush value)
        {
            element.SetValue(SelectedForegroundBrushProperty, value);
        }

        /// <summary>
        /// HoverBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> HoverBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("HoverBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get HoverBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetHoverBackgroundBrush(Control element)
        {
            return element.GetValue<IBrush>(HoverBackgroundBrushProperty);
        }

        /// <summary>
        /// set HoverBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHoverBackgroundBrush(Control element, IBrush value)
        {
            element.SetValue(HoverBackgroundBrushProperty, value);
        }

        /// <summary>
        /// HoverSelectedBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> HoverSelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("HoverSelectedBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get HoverSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetHoverSelectedBackgroundBrush(Control element)
        {
            return element.GetValue<IBrush>(HoverSelectedBackgroundBrushProperty);
        }

        /// <summary>
        /// set HoverSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHoverSelectedBackgroundBrush(Control element, IBrush value)
        {
            element.SetValue(HoverSelectedBackgroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledSelectedBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledSelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("DisabledSelectedBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledSelectedBackgroundBrush(Control element)
        {
            return element.GetValue<IBrush>(DisabledSelectedBackgroundBrushProperty);
        }

        /// <summary>
        /// set DisabledSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledSelectedBackgroundBrush(Control element, IBrush value)
        {
            element.SetValue(DisabledSelectedBackgroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledSelectedForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledSelectedForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("DisabledSelectedForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledSelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledSelectedForegroundBrush(Control element)
        {
            return element.GetValue<IBrush>(DisabledSelectedForegroundBrushProperty);
        }

        /// <summary>
        /// set DisabledSelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledSelectedForegroundBrush(Control element, IBrush value)
        {
            element.SetValue(DisabledSelectedForegroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("DisabledBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledBackgroundBrush(Control element)
        {
            return element.GetValue<IBrush>(DisabledBackgroundBrushProperty);
        }

        /// <summary>
        /// set DisabledBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledBackgroundBrush(Control element, IBrush value)
        {
            element.SetValue(DisabledBackgroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("DisabledForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledForegroundBrush(Control element)
        {
            return element.GetValue<IBrush>(DisabledForegroundBrushProperty);
        }

        /// <summary>
        /// set DisabledForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledForegroundBrush(Control element, IBrush value)
        {
            element.SetValue(DisabledForegroundBrushProperty, value);
        }
    }
}
