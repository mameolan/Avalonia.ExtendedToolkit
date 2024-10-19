using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// an arcodion like control
    /// </summary>
    public partial class OdcExpander : HeaderedContentControl
    {
        static OdcExpander()
        {
            MarginProperty.OverrideDefaultValue<OdcExpander>(new Thickness(10, 10, 10, 2));
            FocusableProperty.OverrideDefaultValue<OdcExpander>(false);

            IsMinimizedProperty.Changed.AddClassHandler<OdcExpander>((o, e) => IsMinimizedChanged(o, e));
            IsExpandedProperty.Changed.AddClassHandler<OdcExpander>((o, e) => IsExpandedChanged(o, e));
            PressedHeaderBackgroundProperty.Changed.AddClassHandler<OdcExpander>((o, e) => PressedHeaderBackgroundPropertyChangedCallback(o, e));

            HeaderClassesProperty.Changed.AddClassHandler<OdcExpander>((o, e) => HeaderClassesChanged(o, e));
        }

        private static void HeaderClassesChanged(OdcExpander o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is Classes && o._header != null)
            {
                Classes classes = e.NewValue as Classes;

                foreach(var item in classes)
                {
                    if(o._header.Classes.Contains(item)==false)
                    {
                        o._header.Classes.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// registered PointerPressed, PointerReleased
        /// for setting IsPressed state
        /// </summary>
        public OdcExpander()
        {
            PointerPressed += (o, e) =>
            {
                IsPressed = true;
            };

            PointerReleased += (o, e) =>
            {
                IsPressed = false;
            };
        }

        private static void PressedHeaderBackgroundPropertyChangedCallback(OdcExpander expander, AvaloniaPropertyChangedEventArgs e)
        {
            expander.HasPressedBackground = e.NewValue != null;
        }

        private static void IsExpandedChanged(OdcExpander expander, AvaloniaPropertyChangedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs((bool)e.NewValue ? ExpandedEvent : CollapsedEvent);
            expander.RaiseEvent(args);
        }

        private static void IsMinimizedChanged(OdcExpander expander, AvaloniaPropertyChangedEventArgs e)
        {
            bool minimized = (bool)e.NewValue;

            expander.IsEnabled = !minimized;
            RoutedEventArgs args = new RoutedEventArgs(minimized ? MinimizedEvent : MaximizedEvent);
            expander.RaiseEvent(args);
        }

        /// <summary>
        /// gets the header from the template
        /// raises header classes propertychanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            _header = e.NameScope.Find<OdcExpanderHeader>("PART_HEADER");
            //ExpanderHeaderHight = _header.Height;
            //ExpanderHeaderWidth = _header.Width;

            base.OnApplyTemplate(e);
            //.obsolete, HeaderClassesProperty is styled
            //RaisePropertyChanged(HeaderClassesProperty, null, HeaderClasses);
        }
    }
}
