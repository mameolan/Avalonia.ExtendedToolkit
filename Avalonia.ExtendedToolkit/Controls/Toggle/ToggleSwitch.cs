using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// A control that allows the user to toggle between two states: One represents true;
    /// The other represents false.
    /// </summary>
    public partial class ToggleSwitch : HeaderedContentControl
    {
        /// <summary>
        /// init some handlers
        /// </summary>
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

        /// <summary>
        /// gets some controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
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
        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private void ToggleButton_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(ToggleButton.IsChecked))
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
            //.orig
            //KeyboardDevice.Instance.SetFocusedElement(this, NavigationMethod.Unspecified, KeyModifiers.None);
        }

        /// <summary>
        /// returns a debug text with on/off
        /// </summary>
        /// <returns></returns>
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
