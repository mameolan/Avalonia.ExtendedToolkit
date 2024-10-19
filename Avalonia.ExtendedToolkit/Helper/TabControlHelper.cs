using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// TabControl attached properties
    /// </summary>
    public static class TabControlHelper
    {
        /// <summary>
        /// CloseButtonEnabled AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<bool> CloseButtonEnabledProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("CloseButtonEnabled",
                typeof(TabControlHelper), defaultValue:false);

        /// <summary>
        /// get CloseButtonEnabled
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetCloseButtonEnabled(Control element)
        {
            return element.GetValue<bool>(CloseButtonEnabledProperty);
        }

        /// <summary>
        /// set CloseButtonEnabled
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCloseButtonEnabled(Control element, bool value)
        {
            element.SetValue(CloseButtonEnabledProperty, value);
        }

        /// <summary>
        /// CloseTabCommand AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<ICommand> CloseTabCommandProperty =
            AvaloniaProperty.RegisterAttached<Control, ICommand>("CloseTabCommand", typeof(TabControlHelper));

        /// <summary>
        /// get CloseTabCommand
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ICommand GetCloseTabCommand(Control element)
        {
            return element.GetValue<ICommand>(CloseTabCommandProperty);
        }

        /// <summary>
        /// set CloseTabCommand
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCloseTabCommand(Control element, ICommand value)
        {
            element.SetValue(CloseTabCommandProperty, value);
        }

        /// <summary>
        /// CloseTabCommandParameter AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<object> CloseTabCommandParameterProperty =
            AvaloniaProperty.RegisterAttached<Control, object>("CloseTabCommandParameter", typeof(TabControlHelper));

        /// <summary>
        /// get CloseTabCommandParameter
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static object GetCloseTabCommandParameter(Control element)
        {
            return element.GetValue(CloseTabCommandParameterProperty);
        }

        /// <summary>
        /// set CloseTabCommandParameter
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCloseTabCommandParameter(Control element, object value)
        {
            element.SetValue(CloseTabCommandParameterProperty, value);
        }

        /// <summary>
        /// Underlined AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<UnderlinedType> UnderlinedProperty =
            AvaloniaProperty.RegisterAttached<Control, UnderlinedType>("Underlined",
                typeof(TabControlHelper), defaultValue: UnderlinedType.None);

        /// <summary>
        /// get Underlined
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static UnderlinedType GetUnderlined(Control element)
        {
            return element.GetValue<UnderlinedType>(UnderlinedProperty);
        }

        /// <summary>
        /// set Underlined
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlined(Control element, UnderlinedType value)
        {
            element.SetValue(UnderlinedProperty, value);

            element.Classes.Add($":underlined_{value.ToString().ToLower()}");

            if (value == UnderlinedType.TabPanel)
                element.SetValue(IsTabPanelSelectedProperty, true);
        }

        /// <summary>
        /// IsTabPanelSelected AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<bool> IsTabPanelSelectedProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("IsTabPanelSelected", typeof(TabControlHelper), defaultValue:false);

        /// <summary>
        /// get IsTabPanelSelected
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetIsTabPanelSelected(Control element)
        {
            return element.GetValue<bool>(IsTabPanelSelectedProperty);
        }

        /// <summary>
        /// set IsTabPanelSelected
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIsTabPanelSelected(Control element, bool value)
        {
            element.SetValue(IsTabPanelSelectedProperty, value);
        }

        /// <summary>
        /// UnderlineBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("UnderlineBrush",
                typeof(TabControlHelper),defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineBrush(Control element)
        {
            return element.GetValue<IBrush>(UnderlineBrushProperty);
        }

        /// <summary>
        /// set UnderlineBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineBrush(Control element, IBrush value)
        {
            element.SetValue(UnderlineBrushProperty, value);
        }

        /// <summary>
        /// UnderlineSelectedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineSelectedBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("UnderlineSelectedBrush",
                typeof(TabControlHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineSelectedBrush(Control element)
        {
            return element.GetValue<IBrush>(UnderlineSelectedBrushProperty);
        }

        /// <summary>
        /// set UnderlineSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineSelectedBrush(Control element, IBrush value)
        {
            element.SetValue(UnderlineSelectedBrushProperty, value);
        }

        /// <summary>
        /// UnderlineMouseOverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineMouseOverBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("UnderlineMouseOverBrush",
                typeof(TabControlHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineMouseOverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineMouseOverBrush(Control element)
        {
            return element.GetValue<IBrush>(UnderlineMouseOverBrushProperty);
        }

        /// <summary>
        /// set UnderlineMouseOverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineMouseOverBrush(Control element, IBrush value)
        {
            element.SetValue(UnderlineMouseOverBrushProperty, value);
        }

        /// <summary>
        /// UnderlineMouseOverSelectedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineMouseOverSelectedBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("UnderlineMouseOverSelectedBrush",
                typeof(TabControlHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineMouseOverSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineMouseOverSelectedBrush(Control element)
        {
            return element.GetValue<IBrush>(UnderlineMouseOverSelectedBrushProperty);
        }

        /// <summary>
        /// set UnderlineMouseOverSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineMouseOverSelectedBrush(Control element, IBrush value)
        {
            element.SetValue(UnderlineMouseOverSelectedBrushProperty, value);
        }

        /// <summary>
        /// Transition AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<TransitionType> TransitionProperty =
            AvaloniaProperty.RegisterAttached<Control, TransitionType>("Transition",
                typeof(TabControlHelper), defaultValue:TransitionType.Default);

        /// <summary>
        /// get Transition
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static TransitionType GetTransition(Control element)
        {
            return element.GetValue<TransitionType>(TransitionProperty);
        }

        /// <summary>
        /// set Transition
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTransition(Control element, TransitionType value)
        {
            element.SetValue(TransitionProperty, value);
        }

        /// <summary>
        /// UnderlinePlacement AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Dock?> UnderlinePlacementProperty =
            AvaloniaProperty.RegisterAttached<Control, Dock?>("UnderlinePlacement", typeof(TabControlHelper));

        /// <summary>
        /// get UnderlinePlacement
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Dock? GetUnderlinePlacement(Control element)
        {
            return element.GetValue<Dock?>(UnderlinePlacementProperty);
        }

        /// <summary>
        /// set UnderlinePlacement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlinePlacement(Control element, Dock? value)
        {
            string classNameSet = ":underlineplacementset";
            string classNameNotSet = ":underlineplacementnotset";
            if (value.HasValue && element.Classes.Contains(classNameSet) == false)
            {
                element.Classes.Add(classNameSet);
            }else if(value.HasValue && element.Classes.Contains(classNameNotSet) == false)
            {
                element.Classes.Add(classNameNotSet);
            }

            element.SetValue(UnderlinePlacementProperty, value);
        }
    }
}
