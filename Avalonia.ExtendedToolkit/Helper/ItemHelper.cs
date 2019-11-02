using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class ItemHelper
    {


        public static readonly AttachedProperty<IBrush> ActiveSelectionBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("ActiveSelectionBackgroundBrush", typeof(ItemHelper));

        public static IBrush GetActiveSelectionBackgroundBrush(IControl element)
        {
            return element.GetValue(ActiveSelectionBackgroundBrushProperty);
        }

        public static void SetActiveSelectionBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(ActiveSelectionBackgroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> ActiveSelectionForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("ActiveSelectionForegroundBrush", typeof(ItemHelper));

        public static IBrush GetActiveSelectionForegroundBrush(IControl element)
        {
            return element.GetValue(ActiveSelectionForegroundBrushProperty);
        }

        public static void SetActiveSelectionForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(ActiveSelectionForegroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> SelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("SelectedBackgroundBrush", typeof(ItemHelper));

        public static IBrush GetSelectedBackgroundBrush(IControl element)
        {
            return element.GetValue(SelectedBackgroundBrushProperty);
        }

        public static void SetSelectedBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(SelectedBackgroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> SelectedForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("SelectedForegroundBrush", typeof(ItemHelper));

        public static IBrush GetSelectedForegroundBrush(IControl element)
        {
            return element.GetValue(SelectedForegroundBrushProperty);
        }

        public static void SetSelectedForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(SelectedForegroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> HoverBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("HoverBackgroundBrush", typeof(ItemHelper));

        public static IBrush GetHoverBackgroundBrush(IControl element)
        {
            return element.GetValue(HoverBackgroundBrushProperty);
        }

        public static void SetHoverBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(HoverBackgroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> HoverSelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("HoverSelectedBackgroundBrush", typeof(ItemHelper));

        public static IBrush GetHoverSelectedBackgroundBrush(IControl element)
        {
            return element.GetValue(HoverSelectedBackgroundBrushProperty);
        }

        public static void SetHoverSelectedBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(HoverSelectedBackgroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> DisabledSelectedBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledSelectedBackgroundBrush", typeof(ItemHelper));

        public static IBrush GetDisabledSelectedBackgroundBrush(IControl element)
        {
            return element.GetValue(DisabledSelectedBackgroundBrushProperty);
        }

        public static void SetDisabledSelectedBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledSelectedBackgroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> DisabledSelectedForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledSelectedForegroundBrush", typeof(ItemHelper));

        public static IBrush GetDisabledSelectedForegroundBrush(IControl element)
        {
            return element.GetValue(DisabledSelectedForegroundBrushProperty);
        }

        public static void SetDisabledSelectedForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledSelectedForegroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> DisabledBackgroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledBackgroundBrush", typeof(ItemHelper));

        public static IBrush GetDisabledBackgroundBrush(IControl element)
        {
            return element.GetValue(DisabledBackgroundBrushProperty);
        }

        public static void SetDisabledBackgroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledBackgroundBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> DisabledForegroundBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("DisabledForegroundBrush", typeof(ItemHelper));

        public static IBrush GetDisabledForegroundBrush(IControl element)
        {
            return element.GetValue(DisabledForegroundBrushProperty);
        }

        public static void SetDisabledForegroundBrush(IControl element, IBrush value)
        {
            element.SetValue(DisabledForegroundBrushProperty, value);
        }

    }
}
