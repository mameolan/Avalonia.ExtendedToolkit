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

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(ToggleSwitch);

        /// <summary>
        /// margin for header
        /// </summary>
        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderMargin"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="HeaderFontSize"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="HeaderFontWeight"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="HeaderFontFamily"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="OnLabel"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="OffLabel"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="OnSwitchBrush"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="OffSwitchBrush"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ThumbIndicatorBrush"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ThumbIndicatorDisabledBrush"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ThumbIndicatorWidth"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="IsChecked"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CheckChangedCommand"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CheckedCommand"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="UnCheckedCommand"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CheckChangedCommandParameter"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CheckedCommandParameter"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="UnCheckedCommandParameter"/>
        /// </summary>
        public static readonly StyledProperty<object> UnCheckedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(UnCheckedCommandParameter));

        /// <summary>
        /// Gets/sets the control's content flow direction.
        /// LeftToRight means content left and button right and RightToLeft vise versa
        /// </summary>
        public FlowDirection ContentDirection
        {
            get { return (FlowDirection)GetValue(ContentDirectionProperty); }
            set { SetValue(ContentDirectionProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentDirection"/>
        /// </summary>
        public static readonly StyledProperty<FlowDirection> ContentDirectionProperty =
            AvaloniaProperty.Register<ToggleSwitch, FlowDirection>(nameof(ContentDirection), defaultValue: Controls.FlowDirection.LeftToRight);

        /// <summary>
        /// Gets or sets the padding of the inner content.
        /// </summary>
        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentPadding"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ToggleSwitchButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> ToggleSwitchButtonStyleProperty =
            AvaloniaProperty.Register<ToggleSwitch, IStyle>(nameof(ToggleSwitchButtonStyle));

        /// <summary>
        /// An event that is raised when the value of IsChecked changes.
        /// </summary>
        public event EventHandler IsCheckedChanged;

        /// <summary>
        /// checked event
        /// </summary>
        public event EventHandler<RoutedEventArgs> Checked;

        /// <summary>
        /// unchecked event
        /// </summary>
        public event EventHandler<RoutedEventArgs> Unchecked;

        /// <summary>
        /// click event
        /// </summary>
        public event EventHandler<RoutedEventArgs> Click;
    }
}
