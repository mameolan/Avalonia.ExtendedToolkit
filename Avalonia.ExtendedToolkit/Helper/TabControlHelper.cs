using Avalonia.Controls;
using Avalonia.Media;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Specifies the underline position of a TabControl.
    /// </summary>
    public enum UnderlinedType
    {
        None,
        TabItems,
        SelectedTabItem,
        TabPanel
    }

    public static class TabControlHelper
    {
        public static readonly AttachedProperty<bool> CloseButtonEnabledProperty =
            AvaloniaProperty.RegisterAttached<IControl, bool>("CloseButtonEnabled", typeof(TabControlHelper));

        public static bool GetCloseButtonEnabled(IControl element)
        {
            return element.GetValue(CloseButtonEnabledProperty);
        }

        public static void SetCloseButtonEnabled(IControl element, bool value)
        {
            element.SetValue(CloseButtonEnabledProperty, value);
        }

        public static readonly AttachedProperty<ICommand> CloseTabCommandProperty =
            AvaloniaProperty.RegisterAttached<IControl, ICommand>("CloseTabCommand", typeof(TabControlHelper));

        public static ICommand GetCloseTabCommand(IControl element)
        {
            return element.GetValue(CloseTabCommandProperty);
        }

        public static void SetCloseTabCommand(IControl element, ICommand value)
        {
            element.SetValue(CloseTabCommandProperty, value);
        }

        public static readonly AttachedProperty<object> CloseTabCommandParameterProperty =
            AvaloniaProperty.RegisterAttached<IControl, object>("CloseTabCommandParameter", typeof(TabControlHelper));

        public static object GetCloseTabCommandParameter(IControl element)
        {
            return element.GetValue(CloseTabCommandParameterProperty);
        }

        public static void SetCloseTabCommandParameter(IControl element, object value)
        {
            element.SetValue(CloseTabCommandParameterProperty, value);
        }

        public static readonly AttachedProperty<UnderlinedType> UnderlinedProperty =
            AvaloniaProperty.RegisterAttached<IControl, UnderlinedType>("Underlined",
                typeof(TabControlHelper), defaultValue: UnderlinedType.None);

        public static UnderlinedType GetUnderlined(IControl element)
        {
            return element.GetValue(UnderlinedProperty);
        }

        public static void SetUnderlined(IControl element, UnderlinedType value)
        {
            element.SetValue(UnderlinedProperty, value);

            if (value == UnderlinedType.TabPanel)
                element.SetValue(IsTabPanelSelectedProperty, true);


        }



        public static readonly AttachedProperty<bool> IsTabPanelSelectedProperty =
            AvaloniaProperty.RegisterAttached<IControl, bool>("IsTabPanelSelected", typeof(TabControlHelper));

        public static bool GetIsTabPanelSelected(IControl element)
        {
            return element.GetValue(IsTabPanelSelectedProperty);
        }

        public static void SetIsTabPanelSelected(IControl element, bool value)
        {
            element.SetValue(IsTabPanelSelectedProperty, value);
        }





        public static readonly AttachedProperty<IBrush> UnderlineBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineBrush", typeof(TabControlHelper));

        public static IBrush GetUnderlineBrush(IControl element)
        {
            return element.GetValue(UnderlineBrushProperty);
        }

        public static void SetUnderlineBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> UnderlineSelectedBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineSelectedBrush", typeof(TabControlHelper));

        public static IBrush GetUnderlineSelectedBrush(IControl element)
        {
            return element.GetValue(UnderlineSelectedBrushProperty);
        }

        public static void SetUnderlineSelectedBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineSelectedBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> UnderlineMouseOverBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineMouseOverBrush", typeof(TabControlHelper));

        public static IBrush GetUnderlineMouseOverBrush(IControl element)
        {
            return element.GetValue(UnderlineMouseOverBrushProperty);
        }

        public static void SetUnderlineMouseOverBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineMouseOverBrushProperty, value);
        }

        public static readonly AttachedProperty<IBrush> UnderlineMouseOverSelectedBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineMouseOverSelectedBrush", typeof(TabControlHelper));

        public static IBrush GetUnderlineMouseOverSelectedBrush(IControl element)
        {
            return element.GetValue(UnderlineMouseOverSelectedBrushProperty);
        }

        public static void SetUnderlineMouseOverSelectedBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineMouseOverSelectedBrushProperty, value);
        }

        public static readonly AttachedProperty<TransitionType> TransitionProperty =
            AvaloniaProperty.RegisterAttached<IControl, TransitionType>("Transition", typeof(TabControlHelper));

        public static TransitionType GetTransition(IControl element)
        {
            return element.GetValue(TransitionProperty);
        }

        public static void SetTransition(IControl element, TransitionType value)
        {
            element.SetValue(TransitionProperty, value);
        }

        public static readonly AttachedProperty<Dock?> UnderlinePlacementProperty =
            AvaloniaProperty.RegisterAttached<IControl, Dock?>("UnderlinePlacement", typeof(TabControlHelper));

        public static Dock? GetUnderlinePlacement(IControl element)
        {
            return element.GetValue(UnderlinePlacementProperty);
        }

        public static void SetUnderlinePlacement(IControl element, Dock? value)
        {
            element.SetValue(UnderlinePlacementProperty, value);
        }
    }
}