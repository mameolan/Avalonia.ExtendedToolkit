using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Provides a wrapper around property value to be used at presentation level.
    /// </summary>
    public partial class PropertyItemValue : AvaloniaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyItemValue"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public PropertyItemValue(PropertyItem property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
            ParentProperty = property;

            HasSubProperties = property.Converter.GetPropertiesSupported();

            if (HasSubProperties)
            {
                object value = property.GetValue();

                PropertyDescriptorCollection descriptors = property.Converter.GetProperties(value);
                foreach (PropertyDescriptor d in descriptors)
                {
                    SubProperties.Add(new PropertyItem(property.Owner, value, d));
                    // TODO: Move to PropertyData as a public property
                    NotifyParentPropertyAttribute notifyParent = d.Attributes[KnownTypes.Attributes.NotifyParentPropertyAttribute] as NotifyParentPropertyAttribute;
                    if (notifyParent != null && notifyParent.NotifyParent)
                    {
                        d.AddValueChanged(value, NotifySubPropertyChanged);
                    }
                }
            }

            this.ParentProperty.PropertyChanged += ParentPropertyChanged;
        }

        private void ParentPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(PropertyItem.PropertyValue))
                NotifyRootValueChanged();

            if (e.Property.Name == nameof(PropertyItem.IsReadOnly))
            {
                RaisePropertyChanged(PropertyItem.IsReadOnlyProperty,
                    !ParentProperty.IsReadOnly, ParentProperty.IsReadOnly);

                RaisePropertyChanged(IsEditableProperty, !IsEditable, IsEditable);
            }
        }

        /// <summary>
        /// Clears the value.
        /// </summary>
        public void ClearValue()
        {
            _parentProperty.ClearValue();
        }

        /// <summary>
        /// Converts the string to value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value instance</returns>
        protected object ConvertStringToValue(string value)
        {
            if (_parentProperty.PropertyType == typeof(string))
                return value;
            //if (value.Length == 0) return null;
            if (string.IsNullOrEmpty(value))
                return null;
            if (!_parentProperty.Converter.CanConvertFrom(typeof(string)))
                throw new InvalidOperationException("Value to String conversion is not supported!");
            return _parentProperty.Converter.ConvertFromString(null, GetSerializationCulture(), value);
        }

        /// <summary>
        /// Converts the value to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>String presentation of the value</returns>
        protected string ConvertValueToString(object value)
        {
            string collectionValue = string.Empty;
            if (value == null)
                return collectionValue;

            collectionValue = value as String;
            if (collectionValue != null)
                return collectionValue;

            var converter = this._parentProperty.Converter;
            if (converter.CanConvertTo(typeof(string)))
                collectionValue = converter.ConvertToString(null, GetSerializationCulture(), value);
            else
                collectionValue = value.ToString();

            // TODO: refer to resources or some constant
            if (string.IsNullOrEmpty(collectionValue) && (value is IEnumerable))
                collectionValue = "(Collection)";

            return collectionValue;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>Property value</returns>
        protected object GetValueCore()
        {
            return this._parentProperty.GetValue();
        }

        /// <summary>
        /// Gets the serialization culture.
        /// </summary>
        /// <returns>Culture to serialize value.</returns>
        protected virtual CultureInfo GetSerializationCulture()
        {
            return ObjectServices.GetSerializationCulture(_parentProperty.PropertyType);
        }

        /// <summary>
        /// Notifies the root value changed.
        /// </summary>
        protected virtual void NotifyRootValueChanged()
        {
            RaisePropertyChanged(IsDefaultValueProperty, !IsDefaultValue, IsDefaultValue);

            //does not exist
            //OnPropertyChanged("IsMixedValue");

            RaisePropertyChanged(IsCollectionProperty, !IsCollection, IsCollection);

            //does not exist
            //OnPropertyChanged("Collection");

            RaisePropertyChanged(HasSubPropertiesProperty, !HasSubProperties, HasSubProperties);

            RaisePropertyChanged(SubPropertiesProperty, null, SubProperties);

            //does not exist
            //OnPropertyChanged("Source");

            RaisePropertyChanged(CanConvertFromStringProperty, !CanConvertFromString, CanConvertFromString);

            NotifyValueChanged();
            OnRootValueChanged();
        }

        private void NotifyStringValueChanged()
        {
            RaisePropertyChanged(StringValueProperty, null, StringValue);
        }

        /// <summary>
        /// Notifies the sub property changed.
        /// </summary>
        protected void NotifySubPropertyChanged(object sender, EventArgs args)
        {
            NotifyValueChanged();
            OnSubPropertyChanged();
        }

        private void NotifyValueChanged()
        {
            RaisePropertyChanged(ValueProperty, null, Value);
            NotifyStringValueChanged();
        }

        private void OnRootValueChanged()
        {
            var handler = RootValueChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void OnSubPropertyChanged()
        {
            var handler = SubPropertyChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void SetValueCore(object value)
        {
            _parentProperty.SetValue(value);
        }

        // TODO: AvaloniaProperty validation should be placed here
        /// <summary>
        /// Validates the value.
        /// </summary>
        /// <param name="valueToValidate">The value to validate.</param>
        protected void ValidateValue(object valueToValidate)
        {
            //throw new NotImplementedException();
            // Do nothing
        }

        private void SetValueImpl(object value)
        {
            //this.ValidateValue(value);
            if (ParentProperty.Validate(value))
                SetValueCore(value);

            NotifyValueChanged();
            OnRootValueChanged();
        }

        /// <summary>
        /// Raises the <see cref="PropertyValueException"/> event.
        /// </summary>
        /// <param name="valueExceptionEventArgs">The <see cref="ValueExceptionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyValueException(ValueExceptionEventArgs valueExceptionEventArgs)
        {
            if (valueExceptionEventArgs == null)
                throw new ArgumentNullException(nameof(valueExceptionEventArgs));
            if (PropertyValueException != null)
                PropertyValueException(this, valueExceptionEventArgs);
        }

        /// <summary>
        /// Gets a value indicating whether exceptions should be cought.
        /// </summary>
        /// <value><c>true</c> if expceptions should be cought; otherwise, <c>false</c>.</value>
        protected virtual bool CatchExceptions
        {
            get { return (PropertyValueException != null); }
        }
    }
}
