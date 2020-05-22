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
            AvaloniaProperty.RegisterAttached<IControl, bool>("CloseButtonEnabled",
                typeof(TabControlHelper), defaultValue:false);

        /// <summary>
        /// get CloseButtonEnabled
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetCloseButtonEnabled(IControl element)
        {
            return element.GetValue(CloseButtonEnabledProperty);
        }

        /// <summary>
        /// set CloseButtonEnabled
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCloseButtonEnabled(IControl element, bool value)
        {
            element.SetValue(CloseButtonEnabledProperty, value);
        }

        /// <summary>
        /// CloseTabCommand AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<ICommand> CloseTabCommandProperty =
            AvaloniaProperty.RegisterAttached<IControl, ICommand>("CloseTabCommand", typeof(TabControlHelper));

        /// <summary>
        /// get CloseTabCommand
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ICommand GetCloseTabCommand(IControl element)
        {
            return element.GetValue(CloseTabCommandProperty);
        }

        /// <summary>
        /// set CloseTabCommand
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCloseTabCommand(IControl element, ICommand value)
        {
            element.SetValue(CloseTabCommandProperty, value);
        }

        /// <summary>
        /// CloseTabCommandParameter AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<object> CloseTabCommandParameterProperty =
            AvaloniaProperty.RegisterAttached<IControl, object>("CloseTabCommandParameter", typeof(TabControlHelper));

        /// <summary>
        /// get CloseTabCommandParameter
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static object GetCloseTabCommandParameter(IControl element)
        {
            return element.GetValue(CloseTabCommandParameterProperty);
        }

        /// <summary>
        /// set CloseTabCommandParameter
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCloseTabCommandParameter(IControl element, object value)
        {
            element.SetValue(CloseTabCommandParameterProperty, value);
        }

        /// <summary>
        /// Underlined AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<UnderlinedType> UnderlinedProperty =
            AvaloniaProperty.RegisterAttached<IControl, UnderlinedType>("Underlined",
                typeof(TabControlHelper), defaultValue: UnderlinedType.None);

        /// <summary>
        /// get Underlined
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static UnderlinedType GetUnderlined(IControl element)
        {
            return element.GetValue(UnderlinedProperty);
        }

        /// <summary>
        /// set Underlined
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlined(IControl element, UnderlinedType value)
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
            AvaloniaProperty.RegisterAttached<IControl, bool>("IsTabPanelSelected", typeof(TabControlHelper), defaultValue:false);

        /// <summary>
        /// get IsTabPanelSelected
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetIsTabPanelSelected(IControl element)
        {
            return element.GetValue(IsTabPanelSelectedProperty);
        }

        /// <summary>
        /// set IsTabPanelSelected
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIsTabPanelSelected(IControl element, bool value)
        {
            element.SetValue(IsTabPanelSelectedProperty, value);
        }

        /// <summary>
        /// UnderlineBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineBrush",
                typeof(TabControlHelper),defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineBrush(IControl element)
        {
            return element.GetValue(UnderlineBrushProperty);
        }

        /// <summary>
        /// set UnderlineBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineBrushProperty, value);
        }

        /// <summary>
        /// UnderlineSelectedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineSelectedBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineSelectedBrush",
                typeof(TabControlHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineSelectedBrush(IControl element)
        {
            return element.GetValue(UnderlineSelectedBrushProperty);
        }

        /// <summary>
        /// set UnderlineSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineSelectedBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineSelectedBrushProperty, value);
        }

        /// <summary>
        /// UnderlineMouseOverBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineMouseOverBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineMouseOverBrush",
                typeof(TabControlHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineMouseOverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineMouseOverBrush(IControl element)
        {
            return element.GetValue(UnderlineMouseOverBrushProperty);
        }

        /// <summary>
        /// set UnderlineMouseOverBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineMouseOverBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineMouseOverBrushProperty, value);
        }

        /// <summary>
        /// UnderlineMouseOverSelectedBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> UnderlineMouseOverSelectedBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("UnderlineMouseOverSelectedBrush",
                typeof(TabControlHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get UnderlineMouseOverSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetUnderlineMouseOverSelectedBrush(IControl element)
        {
            return element.GetValue(UnderlineMouseOverSelectedBrushProperty);
        }

        /// <summary>
        /// set UnderlineMouseOverSelectedBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlineMouseOverSelectedBrush(IControl element, IBrush value)
        {
            element.SetValue(UnderlineMouseOverSelectedBrushProperty, value);
        }

        /// <summary>
        /// Transition AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<TransitionType> TransitionProperty =
            AvaloniaProperty.RegisterAttached<IControl, TransitionType>("Transition",
                typeof(TabControlHelper), defaultValue:TransitionType.Default);

        /// <summary>
        /// get Transition
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static TransitionType GetTransition(IControl element)
        {
            return element.GetValue(TransitionProperty);
        }

        /// <summary>
        /// set Transition
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTransition(IControl element, TransitionType value)
        {
            element.SetValue(TransitionProperty, value);
        }

        /// <summary>
        /// UnderlinePlacement AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Dock?> UnderlinePlacementProperty =
            AvaloniaProperty.RegisterAttached<IControl, Dock?>("UnderlinePlacement", typeof(TabControlHelper));

        /// <summary>
        /// get UnderlinePlacement
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Dock? GetUnderlinePlacement(IControl element)
        {
            return element.GetValue(UnderlinePlacementProperty);
        }

        /// <summary>
        /// set UnderlinePlacement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetUnderlinePlacement(IControl element, Dock? value)
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
