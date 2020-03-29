using System;
using System.Windows.Input;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public partial class ToggleSwitch : HeaderedContentControl
    {
        private const string SwitchPart = "Switch";

        private ToggleButton _toggleButton;

        public Type StyleKey => typeof(ToggleSwitch);

        /// <summary>
        /// margin for header
        /// </summary>
        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        public static readonly StyledProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.Register<ToggleSwitch, Thickness>(nameof(HeaderMargin));

        /// <summary>
        /// font size for the header
        /// </summary>
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly StyledProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.Register<ToggleSwitch, double>(nameof(HeaderFontSize));

        /// <summary>
        /// font weight for header
        /// </summary>
        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }

        public static readonly StyledProperty<FontWeight> HeaderFontWeightProperty =
            AvaloniaProperty.Register<ToggleSwitch, FontWeight>(nameof(HeaderFontWeight), defaultValue: FontWeight.Normal);

        /// <summary>
        /// font family for the header
        /// </summary>
        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }

        public static readonly StyledProperty<FontFamily> HeaderFontFamilyProperty =
            AvaloniaProperty.Register<ToggleSwitch, FontFamily>(nameof(HeaderFontFamily));

        /// <summary>
        /// Gets/sets the text to display when the control is in it's On state.
        /// </summary>
        public string OnLabel
        {
            get { return (string)GetValue(OnLabelProperty); }
            set { SetValue(OnLabelProperty, value); }
        }

        public static readonly StyledProperty<string> OnLabelProperty =
            AvaloniaProperty.Register<ToggleSwitch, string>(nameof(OnLabel), defaultValue: "On");

        /// <summary>
        /// Gets/sets the text to display when the control is in it's Off state.
        /// </summary>
        public string OffLabel
        {
            get { return (string)GetValue(OffLabelProperty); }
            set { SetValue(OffLabelProperty, value); }
        }

        public static readonly StyledProperty<string> OffLabelProperty =
            AvaloniaProperty.Register<ToggleSwitch, string>(nameof(OffLabel), defaultValue: "Off");

        /// <summary>
        /// Gets/sets the brush used for the on-switch's foreground.
        /// </summary>
        public IBrush OnSwitchBrush
        {
            get { return (IBrush)GetValue(OnSwitchBrushProperty); }
            set { SetValue(OnSwitchBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> OnSwitchBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(OnSwitchBrush));

        /// <summary>
        /// Gets/sets the brush used for the off-switch's foreground.
        /// </summary>
        public IBrush OffSwitchBrush
        {
            get { return (IBrush)GetValue(OffSwitchBrushProperty); }
            set { SetValue(OffSwitchBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> OffSwitchBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(OffSwitchBrush));

        /// <summary>
        /// Gets/sets the brush used for the thumb indicator.
        /// </summary>
        public IBrush ThumbIndicatorBrush
        {
            get { return (IBrush)GetValue(ThumbIndicatorBrushProperty); }
            set { SetValue(ThumbIndicatorBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> ThumbIndicatorBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(ThumbIndicatorBrush));

        /// <summary>
        /// Gets/sets the brush used for the thumb indicator.
        /// </summary>
        public IBrush ThumbIndicatorDisabledBrush
        {
            get { return (IBrush)GetValue(ThumbIndicatorDisabledBrushProperty); }
            set { SetValue(ThumbIndicatorDisabledBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> ThumbIndicatorDisabledBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(ThumbIndicatorDisabledBrush));

        /// <summary>
        /// Gets/sets the width of the thumb indicator.
        /// </summary>
        public double ThumbIndicatorWidth
        {
            get { return (double)GetValue(ThumbIndicatorWidthProperty); }
            set { SetValue(ThumbIndicatorWidthProperty, value); }
        }

        public static readonly StyledProperty<double> ThumbIndicatorWidthProperty =
            AvaloniaProperty.Register<ToggleSwitch, double>(nameof(ThumbIndicatorWidth));

        /// <summary>
        /// Gets/sets whether the control is Checked (On) or not (Off).
        /// </summary>
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly StyledProperty<bool?> IsCheckedProperty =
            AvaloniaProperty.Register<ToggleSwitch, bool?>(nameof(IsChecked));

        /// <summary>
        /// Gets/sets the command which will be executed if the IsChecked property was changed.
        /// </summary>
        public ICommand CheckChangedCommand
        {
            get { return (ICommand)GetValue(CheckChangedCommandProperty); }
            set { SetValue(CheckChangedCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CheckChangedCommandProperty =
            AvaloniaProperty.Register<ToggleSwitch, ICommand>(nameof(CheckChangedCommand));

        /// <summary>
        /// Gets/sets the command which will be executed if the checked event of the control is fired.
        /// </summary>
        public ICommand CheckedCommand
        {
            get { return (ICommand)GetValue(CheckedCommandProperty); }
            set { SetValue(CheckedCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CheckedCommandProperty =
            AvaloniaProperty.Register<ToggleSwitch, ICommand>(nameof(CheckedCommand));

        /// <summary>
        /// Gets/sets the command which will be executed if the checked event of the control is fired.
        /// </summary>
        public ICommand UnCheckedCommand
        {
            get { return (ICommand)GetValue(UnCheckedCommandProperty); }
            set { SetValue(UnCheckedCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> UnCheckedCommandProperty =
            AvaloniaProperty.Register<ToggleSwitch, ICommand>(nameof(UnCheckedCommand));

        /// <summary>
        /// Gets/sets the command parameter which will be passed by the CheckChangedCommand.
        /// </summary>
        public object CheckChangedCommandParameter
        {
            get { return (object)GetValue(CheckChangedCommandParameterProperty); }
            set { SetValue(CheckChangedCommandParameterProperty, value); }
        }

        public static readonly StyledProperty<object> CheckChangedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(CheckChangedCommandParameter));

        /// <summary>
        /// Gets/sets the command parameter which will be passed by the CheckedCommand.
        /// </summary>
        public object CheckedCommandParameter
        {
            get { return (object)GetValue(CheckedCommandParameterProperty); }
            set { SetValue(CheckedCommandParameterProperty, value); }
        }

        public static readonly StyledProperty<object> CheckedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(CheckedCommandParameter));

        /// <summary>
        /// Gets/sets the command parameter which will be passed by the UnCheckedCommand.
        /// </summary>
        public object UnCheckedCommandParameter
        {
            get { return (object)GetValue(UnCheckedCommandParameterProperty); }
            set { SetValue(UnCheckedCommandParameterProperty, value); }
        }

        public static readonly StyledProperty<object> UnCheckedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(UnCheckedCommandParameter));

        /// <summary>
        /// Gets/sets the control's content flow direction.
        /// </summary>
        public FlowDirection ContentDirection
        {
            get { return (FlowDirection)GetValue(ContentDirectionProperty); }
            set { SetValue(ContentDirectionProperty, value); }
        }

        // LeftToRight means content left and button right and RightToLeft vise versa
        public static readonly StyledProperty<FlowDirection> ContentDirectionProperty =
            AvaloniaProperty.Register<ToggleSwitch, FlowDirection>(nameof(ContentDirection), defaultValue: FlowDirection.LeftToRight);

        /// <summary>
        /// Gets or sets the padding of the inner content.
        /// </summary>
        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }

        public static readonly StyledProperty<Thickness> ContentPaddingProperty =
            AvaloniaProperty.Register<ToggleSwitch, Thickness>(nameof(ContentPadding), defaultValue: new Thickness());

        /// <summary>
        /// Gets/sets the control's toggle switch button style.
        /// </summary>
        public IStyle ToggleSwitchButtonStyle
        {
            get { return (IStyle)GetValue(ToggleSwitchButtonStyleProperty); }
            set { SetValue(ToggleSwitchButtonStyleProperty, value); }
        }

        public static readonly StyledProperty<IStyle> ToggleSwitchButtonStyleProperty =
            AvaloniaProperty.Register<ToggleSwitch, IStyle>(nameof(ToggleSwitchButtonStyle));

        /// <summary>
        /// An event that is raised when the value of IsChecked changes.
        /// </summary>
        public event EventHandler IsCheckedChanged;

        public event EventHandler<RoutedEventArgs> Checked;

        public event EventHandler<RoutedEventArgs> Unchecked;

        public event EventHandler<RoutedEventArgs> Click;
    }
}
