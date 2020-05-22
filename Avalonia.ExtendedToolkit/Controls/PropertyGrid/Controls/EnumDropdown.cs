using System;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Combobox control to present enumeration classes.
    /// </summary>
    public class EnumDropdown : ComboBox
    {
        private bool _wrappedEvents;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(EnumDropdown);

        /// <summary>
        /// get/sets PropertyValue
        /// </summary>
        public PropertyItemValue PropertyValue
        {
            get { return (PropertyItemValue)GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="PropertyValue"/>
        /// </summary>
        public static readonly StyledProperty<PropertyItemValue> PropertyValueProperty =
            AvaloniaProperty.Register<EnumDropdown, PropertyItemValue>(nameof(PropertyValue));

        /// <summary>
        /// registers some handler
        /// </summary>
        public EnumDropdown()
        {
            PropertyValueProperty.Changed.AddClassHandler<EnumDropdown>((o, e) => OnPropertyValueChanged(o, e));
            
            this.AttachedToVisualTree += EnumDropdownLoaded;
            this.DetachedFromVisualTree += EnumDropdownUnloaded;
            this.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyValue.Value = e.AddedItems[0];
        }

        private void EnumDropdownUnloaded(object sender, VisualTreeAttachmentEventArgs e)
        {
            var value = PropertyValue;

            if (value != null)
                UnwrapEventHandlers(value);
        }

        private void EnumDropdownLoaded(object sender, VisualTreeAttachmentEventArgs e)
        {
            var value = PropertyValue;

            if (value != null)
                WrapEventHandlers(value);
        }

        private void OnPropertyValueChanged(EnumDropdown dropdown, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
                dropdown.UnwrapEventHandlers((PropertyItemValue)e.OldValue);

            var newValue = e.NewValue as PropertyItemValue;
            if (newValue == null)
                return;

            dropdown.SelectedItem = newValue.Value;
            dropdown.Items = newValue.ParentProperty.StandardValues;
            dropdown.WrapEventHandlers(newValue);
        }

        private void WrapEventHandlers(PropertyItemValue target)
        {
            if (target == null)
                return;
            if (_wrappedEvents)
                return;

            target.PropertyChanged += ValuePropertyChanged;
            _wrappedEvents = true;
        }

        // TODO: Provide a dedicated ValueChanged event not to listen to everything (performance increase)
        private void ValuePropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == PropertyItemValue.ValueProperty)
            {
                if (SelectedItem != PropertyValue.Value)
                    SelectedItem = PropertyValue.Value;
            }
        }

        private void UnwrapEventHandlers(PropertyItemValue target)
        {
            if (target == null)
                return;
            if (!_wrappedEvents)
                return;

            target.PropertyChanged -= ValuePropertyChanged;
            _wrappedEvents = false;
        }
    }
}
