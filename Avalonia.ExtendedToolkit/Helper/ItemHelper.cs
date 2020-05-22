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
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("ActiveSelectionBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get ActiveSelectionBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetActiveSelectionBackgroundBrush(IControl element)
        {
            return element.GetValue(ActiveSelectionBackgroundBrushProperty);
        }

        /// <summary>
        /// set ActiveSelectionBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetActiveSelectionBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(ActiveSelectionBackgroundBrushProperty, value);
        }

        /// <summary>
        /// ActiveSelectionForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> ActiveSelectionForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("ActiveSelectionForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get ActiveSelectionForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetActiveSelectionForegroundBrush(IControl element)
        {
            return element.GetValue(ActiveSelectionForegroundBrushProperty);
        }

        /// <summary>
        /// set ActiveSelectionForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetActiveSelectionForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(ActiveSelectionForegroundBrushProperty, value);
        }

        /// <summary>
        /// SelectedBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> SelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("SelectedBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get SelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetSelectedBackgroundBrush(IControl element)
        {
            return element.GetValue(SelectedBackgroundBrushProperty);
        }

        /// <summary>
        /// set SelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSelectedBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(SelectedBackgroundBrushProperty, value);
        }

        /// <summary>
        /// SelectedForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> SelectedForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("SelectedForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get SelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetSelectedForegroundBrush(IControl element)
        {
            return element.GetValue(SelectedForegroundBrushProperty);
        }

        /// <summary>
        /// set SelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSelectedForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(SelectedForegroundBrushProperty, value);
        }

        /// <summary>
        /// HoverBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> HoverBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("HoverBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get HoverBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetHoverBackgroundBrush(IControl element)
        {
            return element.GetValue(HoverBackgroundBrushProperty);
        }

        /// <summary>
        /// set HoverBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHoverBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(HoverBackgroundBrushProperty, value);
        }

        /// <summary>
        /// HoverSelectedBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> HoverSelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("HoverSelectedBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get HoverSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetHoverSelectedBackgroundBrush(IControl element)
        {
            return element.GetValue(HoverSelectedBackgroundBrushProperty);
        }

        /// <summary>
        /// set HoverSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHoverSelectedBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(HoverSelectedBackgroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledSelectedBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledSelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledSelectedBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledSelectedBackgroundBrush(IControl element)
        {
            return element.GetValue(DisabledSelectedBackgroundBrushProperty);
        }

        /// <summary>
        /// set DisabledSelectedBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledSelectedBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledSelectedBackgroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledSelectedForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledSelectedForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledSelectedForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledSelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledSelectedForegroundBrush(IControl element)
        {
            return element.GetValue(DisabledSelectedForegroundBrushProperty);
        }

        /// <summary>
        /// set DisabledSelectedForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledSelectedForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledSelectedForegroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledBackgroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledBackgroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledBackgroundBrush(IControl element)
        {
            return element.GetValue(DisabledBackgroundBrushProperty);
        }

        /// <summary>
        /// set DisabledBackgroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledBackgroundBrushProperty, value);
        }

        /// <summary>
        /// DisabledForegroundBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> DisabledForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledForegroundBrush", typeof(ItemHelper));

        /// <summary>
        /// get DisabledForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetDisabledForegroundBrush(IControl element)
        {
            return element.GetValue(DisabledForegroundBrushProperty);
        }

        /// <summary>
        /// set DisabledForegroundBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDisabledForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledForegroundBrushProperty, value);
        }
    }
}
