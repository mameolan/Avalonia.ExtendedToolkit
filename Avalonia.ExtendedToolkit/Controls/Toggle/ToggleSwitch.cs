using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using Avalonia.Controls;
using System.Windows.Input;
using Avalonia.Input;
using System.Globalization;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ToggleSwitch : HeaderedContentControl
    {
        private const string SwitchPart = "Switch";

        private ToggleButton _toggleButton;



        



        public ToggleSwitchState ToggleSwitchState
        {
            get { return (ToggleSwitchState)GetValue(ToggleSwitchStateProperty); }
            set { SetValue(ToggleSwitchStateProperty, value); }
        }


        public static readonly AvaloniaProperty ToggleSwitchStateProperty =
            AvaloniaProperty.Register<ToggleSwitch, ToggleSwitchState>(nameof(ToggleSwitchState));



        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderMarginProperty =
            AvaloniaProperty.Register<ToggleSwitch, Thickness>(nameof(HeaderMargin));





        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderFontSizeProperty =
            AvaloniaProperty.Register<ToggleSwitch, double>(nameof(HeaderFontSize));



        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderFontWeightProperty =
            AvaloniaProperty.Register<ToggleSwitch, FontWeight>(nameof(HeaderFontWeight), defaultValue: FontWeight.Normal);




        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderFontFamilyProperty =
            AvaloniaProperty.Register<ToggleSwitch, FontFamily>(nameof(HeaderFontFamily));



        public string OnLabel
        {
            get { return (string)GetValue(OnLabelProperty); }
            set { SetValue(OnLabelProperty, value); }
        }


        public static readonly AvaloniaProperty OnLabelProperty =
            AvaloniaProperty.Register<ToggleSwitch, string>(nameof(OnLabel), defaultValue: "On");



        public string OffLabel
        {
            get { return (string)GetValue(OffLabelProperty); }
            set { SetValue(OffLabelProperty, value); }
        }


        public static readonly AvaloniaProperty OffLabelProperty =
            AvaloniaProperty.Register<ToggleSwitch, string>(nameof(OffLabel), defaultValue: "Off");



        public IBrush OnSwitchBrush
        {
            get { return (IBrush)GetValue(OnSwitchBrushProperty); }
            set { SetValue(OnSwitchBrushProperty, value); }
        }


        public static readonly AvaloniaProperty OnSwitchBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(OnSwitchBrush));



        public IBrush OffSwitchBrush
        {
            get { return (IBrush)GetValue(OffSwitchBrushProperty); }
            set { SetValue(OffSwitchBrushProperty, value); }
        }


        public static readonly AvaloniaProperty OffSwitchBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(OffSwitchBrush));



        public IBrush ThumbIndicatorBrush
        {
            get { return (IBrush)GetValue(ThumbIndicatorBrushProperty); }
            set { SetValue(ThumbIndicatorBrushProperty, value); }
        }


        public static readonly AvaloniaProperty ThumbIndicatorBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(ThumbIndicatorBrush));



        public IBrush ThumbIndicatorDisabledBrush
        {
            get { return (IBrush)GetValue(ThumbIndicatorDisabledBrushProperty); }
            set { SetValue(ThumbIndicatorDisabledBrushProperty, value); }
        }


        public static readonly AvaloniaProperty ThumbIndicatorDisabledBrushProperty =
            AvaloniaProperty.Register<ToggleSwitch, IBrush>(nameof(ThumbIndicatorDisabledBrush));



        public double ThumbIndicatorWidth
        {
            get { return (double)GetValue(ThumbIndicatorWidthProperty); }
            set { SetValue(ThumbIndicatorWidthProperty, value); }
        }


        public static readonly AvaloniaProperty ThumbIndicatorWidthProperty =
            AvaloniaProperty.Register<ToggleSwitch, double>(nameof(ThumbIndicatorWidth));



        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }


        public static readonly AvaloniaProperty IsCheckedProperty =
            AvaloniaProperty.Register<ToggleSwitch, bool?>(nameof(IsChecked));



        public ICommand CheckChangedCommand
        {
            get { return (ICommand)GetValue(CheckChangedCommandProperty); }
            set { SetValue(CheckChangedCommandProperty, value); }
        }


        public static readonly AvaloniaProperty CheckChangedCommandProperty =
            AvaloniaProperty.Register<ToggleSwitch, ICommand>(nameof(CheckChangedCommand));



        public ICommand CheckedCommand
        {
            get { return (ICommand)GetValue(CheckedCommandProperty); }
            set { SetValue(CheckedCommandProperty, value); }
        }


        public static readonly AvaloniaProperty CheckedCommandProperty =
            AvaloniaProperty.Register<ToggleSwitch, ICommand>(nameof(CheckedCommand));



        public ICommand UnCheckedCommand
        {
            get { return (ICommand)GetValue(UnCheckedCommandProperty); }
            set { SetValue(UnCheckedCommandProperty, value); }
        }


        public static readonly AvaloniaProperty UnCheckedCommandProperty =
            AvaloniaProperty.Register<ToggleSwitch, ICommand>(nameof(UnCheckedCommand));




        public object CheckChangedCommandParameter
        {
            get { return (object)GetValue(CheckChangedCommandParameterProperty); }
            set { SetValue(CheckChangedCommandParameterProperty, value); }
        }


        public static readonly AvaloniaProperty CheckChangedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(CheckChangedCommandParameter));



        public object CheckedCommandParameter
        {
            get { return (object)GetValue(CheckedCommandParameterProperty); }
            set { SetValue(CheckedCommandParameterProperty, value); }
        }


        public static readonly AvaloniaProperty CheckedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(CheckedCommandParameter));



        public object UnCheckedCommandParameter
        {
            get { return (object)GetValue(UnCheckedCommandParameterProperty); }
            set { SetValue(UnCheckedCommandParameterProperty, value); }
        }


        public static readonly AvaloniaProperty UnCheckedCommandParameterProperty =
            AvaloniaProperty.Register<ToggleSwitch, object>(nameof(UnCheckedCommandParameter));



        public FlowDirection ContentDirection
        {
            get { return (FlowDirection)GetValue(ContentDirectionProperty); }
            set { SetValue(ContentDirectionProperty, value); }
        }

        // LeftToRight means content left and button right and RightToLeft vise versa
        public static readonly AvaloniaProperty ContentDirectionProperty =
            AvaloniaProperty.Register<ToggleSwitch, FlowDirection>(nameof(ContentDirection), defaultValue: FlowDirection.LeftToRight);



        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }


        public static readonly AvaloniaProperty ContentPaddingProperty =
            AvaloniaProperty.Register<ToggleSwitch, Thickness>(nameof(ContentPadding), defaultValue: new Thickness());



        public IStyle ToggleSwitchButtonStyle
        {
            get { return (IStyle)GetValue(ToggleSwitchButtonStyleProperty); }
            set { SetValue(ToggleSwitchButtonStyleProperty, value); }
        }
        

        public static readonly AvaloniaProperty ToggleSwitchButtonStyleProperty =
            AvaloniaProperty.Register<ToggleSwitch, IStyle>(nameof(ToggleSwitchButtonStyle));

        public event EventHandler<RoutedEventArgs> Checked;
        public event EventHandler<RoutedEventArgs> Unchecked;
        public event EventHandler<RoutedEventArgs> Click;









        /// <summary>
        /// An event that is raised when the value of IsChecked changes.
        /// </summary>
        public event EventHandler IsCheckedChanged;







        public ToggleSwitch()
        {
            IsCheckedProperty.Changed.AddClassHandler<ToggleSwitch>((o, e) => OnIsCheckedChanged(o, e));
            KeyUp += ToggleSwitch_KeyUp;
            PointerReleased += (o, e) => Focus();
            
        }

        private void ToggleSwitch_KeyUp(object sender, Input.KeyEventArgs e)
        {
            if (e.Key == Input.Key.Space)
            {
                this.SetValue(ToggleSwitch.IsCheckedProperty, !IsChecked);
            }
        }

        private void OnIsCheckedChanged(ToggleSwitch toggleSwitch, AvaloniaPropertyChangedEventArgs e)
        {
            if (toggleSwitch._toggleButton != null)
            {
                var oldValue = (bool?)e.OldValue;
                var newValue = (bool?)e.NewValue;

                if (oldValue != newValue)
                {
                    var command = toggleSwitch.CheckChangedCommand;
                    var commandParameter = toggleSwitch.CheckChangedCommandParameter ?? toggleSwitch;
                    if (command != null && command.CanExecute(commandParameter))
                    {
                        command.Execute(commandParameter);
                    }

                    toggleSwitch.IsCheckedChanged?.Invoke(toggleSwitch, EventArgs.Empty);
                }

            }

        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            if (_toggleButton != null)
            {
                //_toggleButton.Checked -= CheckedHandler;
                //_toggleButton.Unchecked -= UncheckedHandler;
                //_toggleButton.Indeterminate -= IndeterminateHandler;
                _toggleButton.Click -= ClickHandler;
                //BindingOperations.ClearBinding(_toggleButton, ToggleButton.IsCheckedProperty);
                _toggleButton.ClearValue(ToggleButton.IsCheckedProperty);
                //_toggleButton.IsEnabledChanged -= IsEnabledHandler;
                _toggleButton.PropertyChanged -= ToggleButton_PropertyChanged;
                _toggleButton.PointerReleased -= this.ToggleButtonPreviewMouseUp;
            }

            _toggleButton = e.NameScope.Find<ToggleButton>(SwitchPart);
            if (_toggleButton != null)
            {
                //_toggleButton.Checked += CheckedHandler;
                //_toggleButton.Unchecked += UncheckedHandler;
                //_toggleButton.Indeterminate += IndeterminateHandler;
                _toggleButton.Click += ClickHandler;
                var binding = new Binding("IsChecked") { Source = this };
                _toggleButton.Bind(ToggleButton.IsCheckedProperty, binding);
                _toggleButton.PropertyChanged += ToggleButton_PropertyChanged;

                //_toggleButton.IsEnabledChanged += IsEnabledHandler;

                _toggleButton.PointerReleased += this.ToggleButtonPreviewMouseUp;
            }
            ChangeVisualState();

        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private void ChangeVisualState()
        {
            ToggleSwitchState = IsEnabled ? ToggleSwitchState.NormalState : ToggleSwitchState.DisabledState;
        }

        private void ToggleButton_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(ToggleButton.IsEnabled))
            {
                ChangeVisualState();
            }
            else if (e.Property.Name == nameof(ToggleButton.IsChecked))
            {
                if ((bool)e.NewValue)
                {
                    var command = this.CheckedCommand;
                    var commandParameter = this.CheckedCommandParameter ?? this;
                    if (command != null && command.CanExecute(commandParameter))
                    {
                        command.Execute(commandParameter);
                    }

                    Checked?.Invoke(this, new RoutedEventArgs());
                }
                else
                {
                    var command = this.UnCheckedCommand;
                    var commandParameter = this.UnCheckedCommandParameter ?? this;
                    if (command != null && command.CanExecute(commandParameter))
                    {
                        command.Execute(commandParameter);
                    }

                    Unchecked?.Invoke(this, new RoutedEventArgs());
                }
            }
        }

        private void ToggleButtonPreviewMouseUp(object sender, PointerReleasedEventArgs e)
        {
            KeyboardDevice.Instance.SetFocusedElement(this, NavigationMethod.Unspecified, InputModifiers.None);
        }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{{ToggleSwitch IsChecked={0}, Content={1}}}",
                IsChecked,
                Content
            );
        }
    }
}
