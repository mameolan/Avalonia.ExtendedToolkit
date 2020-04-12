using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Provides a wrapper around property value to be used at presentation level.
    /// </summary>
    public class PropertyItemValue : AvaloniaObject //: INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when exception is raised at Property Value.
        /// <remarks>This event is reserved for future implementations.</remarks>
        /// </summary>
        public event EventHandler<ValueExceptionEventArgs> PropertyValueException;

        /// <summary>
        /// Occurs when root value is changed.
        /// <remarks>This event is reserved for future implementations.</remarks>
        /// </summary>
        public event EventHandler RootValueChanged;

        /// <summary>
        /// Occurs when sub property changed.
        /// </summary>
        public event EventHandler SubPropertyChanged;

        //private readonly PropertyItem _property;
        ///// <summary>
        ///// Gets the parent property.
        ///// </summary>
        ///// <value>The parent property.</value>
        //public PropertyItem ParentProperty
        //{
        //    get { return _property; }
        //}

        public static readonly DirectProperty<PropertyItemValue, PropertyItem> ParentPropertyProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, PropertyItem>(
                    nameof(ParentProperty),
                    o => o.ParentProperty);

        private PropertyItem _parentProperty;

        public PropertyItem ParentProperty
        {
            get { return _parentProperty; }
            private set { SetAndRaise(ParentPropertyProperty, ref _parentProperty, value); }
        }

        //private readonly GridEntryCollection<PropertyItem> _subProperties = new GridEntryCollection<PropertyItem>();
        //public GridEntryCollection<PropertyItem> SubProperties
        //{
        //    get { return _subProperties; }
        //}

        public static readonly DirectProperty<PropertyItemValue, GridEntryCollection<PropertyItem>> SubPropertiesProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, GridEntryCollection<PropertyItem>>(
                    nameof(SubProperties),
                    o => o.SubProperties);

        private GridEntryCollection<PropertyItem> _subProperties = new GridEntryCollection<PropertyItem>();

        public GridEntryCollection<PropertyItem> SubProperties
        {
            get { return _subProperties; }
            private set { SetAndRaise(SubPropertiesProperty, ref _subProperties, value); }
        }

        //private readonly bool _hasSubProperties;
        ///// <summary>
        ///// Gets a value indicating whether encapsulated value has sub-properties.
        ///// </summary>
        ///// <remarks>This property is reserved for future implementations.</remarks>
        ///// <value>
        ///// 	<c>true</c> if this instance has sub properties; otherwise, <c>false</c>.
        ///// </value>
        //public bool HasSubProperties
        //{
        //    get { return _hasSubProperties; }
        //}

        public static readonly DirectProperty<PropertyItemValue, bool> HasSubPropertiesProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(HasSubProperties),
                    o => o.HasSubProperties);

        private bool _hasSubProperties;

        public bool HasSubProperties
        {
            get { return _hasSubProperties; }
            private set { SetAndRaise(HasSubPropertiesProperty, ref _hasSubProperties, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyItemValue"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public PropertyItemValue(PropertyItem property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
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
                //OnPropertyChanged(nameof(PropertyItem.IsReadOnly));
                RaisePropertyChanged(PropertyItem.IsReadOnlyProperty, !ParentProperty.IsReadOnly, ParentProperty.IsReadOnly);
                //OnPropertyChanged(nameof(IsEditable));
                RaisePropertyChanged(IsEditableProperty, !IsEditable, IsEditable);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can convert from string.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can convert from string; otherwise, <c>false</c>.
        /// </value>
        //public bool CanConvertFromString
        //{
        //    get
        //    {
        //        return (((_property.Converter != null)
        //          && _property.Converter.CanConvertFrom(typeof(string))) && !_property.IsReadOnly);
        //    }
        //}

        public static readonly DirectProperty<PropertyItemValue, bool> CanConvertFromStringProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(CanConvertFromString),
                    o => o.CanConvertFromString);

        public bool CanConvertFromString
        {
            get
            {
                return (((_parentProperty.Converter != null)
                  && _parentProperty.Converter.CanConvertFrom(typeof(string)))
                  && !_parentProperty.IsReadOnly);
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
        /// Gets a value indicating whether encapsulated property value is collection.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if encapsulated property value is collection; otherwise, <c>false</c>.
        /// </value>
        //public bool IsCollection
        //{
        //    get { return _property.IsCollection; }
        //}

        public static readonly DirectProperty<PropertyItemValue, bool> IsCollectionProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsCollection),
                    o => o.IsCollection);

        public bool IsCollection
        {
            get { return _parentProperty.IsCollection; }
        }

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is default value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if encapsulated property value is default value; otherwise, <c>false</c>.
        /// </value>
        //public bool IsDefaultValue
        //{
        //    get { return _property.IsDefaultValue; }
        //}

        public static readonly DirectProperty<PropertyItemValue, bool> IsDefaultValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsDefaultValue),
                    o => o.IsDefaultValue);

        public bool IsDefaultValue
        {
            get { return _parentProperty.IsDefaultValue; }
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
        /// <param name="e">The <see cref="WpfPropertyGrid.ValueExceptionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyValueException(ValueExceptionEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            if (PropertyValueException != null)
                PropertyValueException(this, e);
        }

        /// <summary>
        /// Gets a value indicating whether exceptions should be cought.
        /// </summary>
        /// <value><c>true</c> if expceptions should be cought; otherwise, <c>false</c>.</value>
        protected virtual bool CatchExceptions
        {
            get { return (PropertyValueException != null); }
        }

        /// <summary>
        /// Gets or sets the string representation of the value.
        /// </summary>
        /// <value>The string value.</value>
        //public string StringValue
        //{
        //    get
        //    {
        //        string str = string.Empty;
        //        if (CatchExceptions)
        //        {
        //            try
        //            {
        //                str = ConvertValueToString(Value);
        //            }
        //            catch (Exception exception)
        //            {
        //                OnPropertyValueException(new ValueExceptionEventArgs("Cannot convert value to string", this, ValueExceptionSource.Get, exception));
        //            }
        //            return str;
        //        }
        //        return ConvertValueToString(Value);
        //    }
        //    set
        //    {
        //        if (CatchExceptions)
        //        {
        //            try
        //            {
        //                Value = ConvertStringToValue(value);
        //            }
        //            catch (Exception exception)
        //            {
        //                OnPropertyValueException(new ValueExceptionEventArgs("Cannot create value from string", this, ValueExceptionSource.Set, exception));
        //            }
        //        }
        //        else
        //        {
        //            Value = ConvertStringToValue(value);
        //        }
        //    }
        //}

        public static readonly DirectProperty<PropertyItemValue, string> StringValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, string>(
                    nameof(StringValue),
                    o => o.StringValue);

        public string StringValue
        {
            get
            {
                string str = string.Empty;
                if (CatchExceptions)
                {
                    try
                    {
                        str = ConvertValueToString(Value);
                    }
                    catch (Exception exception)
                    {
                        OnPropertyValueException(new ValueExceptionEventArgs("Cannot convert value to string", this, ValueExceptionSource.Get, exception));
                    }
                    return str;
                }
                return ConvertValueToString(Value);
            }
            set
            {
                if (CatchExceptions)
                {
                    try
                    {
                        Value = ConvertStringToValue(value);
                    }
                    catch (Exception exception)
                    {
                        OnPropertyValueException(new ValueExceptionEventArgs("Cannot create value from string", this, ValueExceptionSource.Set, exception));
                    }
                }
                else
                {
                    Value = ConvertStringToValue(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        //public object Value
        //{
        //    get
        //    {
        //        object valueCore = null;
        //        if (CatchExceptions)
        //        {
        //            try
        //            {
        //                valueCore = GetValueCore();
        //            }
        //            catch (Exception exception)
        //            {
        //                OnPropertyValueException(new ValueExceptionEventArgs("Value Get Failed", this, ValueExceptionSource.Get, exception));
        //            }
        //            return valueCore;
        //        }
        //        return GetValueCore();
        //    }
        //    set
        //    {
        //        if (CatchExceptions)
        //        {
        //            try
        //            {
        //                SetValueImpl(value);
        //            }
        //            catch (Exception exception)
        //            {
        //                OnPropertyValueException(new ValueExceptionEventArgs("Value Set Failed", this, ValueExceptionSource.Set, exception));
        //            }
        //        }
        //        else
        //        {
        //            SetValueImpl(value);
        //        }
        //    }
        //}

        public static readonly DirectProperty<PropertyItemValue, object> ValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, object>(
                    nameof(Value),
                    o => o.Value);

        public object Value
        {
            get
            {
                object valueCore = null;
                if (CatchExceptions)
                {
                    try
                    {
                        valueCore = GetValueCore();
                    }
                    catch (Exception exception)
                    {
                        OnPropertyValueException(new ValueExceptionEventArgs("Value Get Failed", this, ValueExceptionSource.Get, exception));
                    }
                    return valueCore;
                }
                return GetValueCore();
            }
            set
            {
                if (CatchExceptions)
                {
                    try
                    {
                        SetValueImpl(value);
                    }
                    catch (Exception exception)
                    {
                        OnPropertyValueException(new ValueExceptionEventArgs("Value Set Failed", this, ValueExceptionSource.Set, exception));
                    }
                }
                else
                {
                    SetValueImpl(value);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is read only.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        //public bool IsReadOnly
        //{
        //    get { return _property.IsReadOnly; }
        //}

        public static readonly DirectProperty<PropertyItemValue, bool> IsReadOnlyProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsReadOnly),
                    o => o.IsReadOnly);

        public bool IsReadOnly
        {
            get { return _parentProperty.IsReadOnly; }
        }

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is editable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        //public bool IsEditable
        //{
        //    get { return !_property.IsReadOnly; }
        //}

        public static readonly DirectProperty<PropertyItemValue, bool> IsEditableProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsEditable),
                    o => o.IsEditable);

        public bool IsEditable
        {
            get { return !_parentProperty.IsReadOnly; }
        }

        /// <summary>
        /// Gets the serialization culture.
        /// </summary>
        /// <returns>Culture to serialize value.</returns>
        protected virtual CultureInfo GetSerializationCulture()
        {
            return ObjectServices.GetSerializationCulture(_parentProperty.PropertyType);
        }

        ///// <summary>
        ///// Occurs when a property value changes.
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        ///// <summary>
        ///// Called when property value is changed.
        ///// </summary>
        ///// <param name="propertyName">Name of the property.</param>
        //protected void OnPropertyChanged([CallerMemberName]string propertyName=null)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        /// <summary>
        /// Notifies the root value changed.
        /// </summary>
        protected virtual void NotifyRootValueChanged()
        {
            //OnPropertyChanged("IsDefaultValue");
            RaisePropertyChanged(IsDefaultValueProperty, !IsDefaultValue, IsDefaultValue);

            //does not exist
            //OnPropertyChanged("IsMixedValue");

            //OnPropertyChanged("IsCollection");
            RaisePropertyChanged(IsCollectionProperty, !IsCollection, IsCollection);

            //does not exist
            //OnPropertyChanged("Collection");

            //OnPropertyChanged("HasSubProperties");
            RaisePropertyChanged(HasSubPropertiesProperty, !HasSubProperties, HasSubProperties);

            //OnPropertyChanged("SubProperties");
            RaisePropertyChanged(SubPropertiesProperty, null, SubProperties);

            //does not exist
            //OnPropertyChanged("Source");

            //OnPropertyChanged("CanConvertFromString");
            RaisePropertyChanged(CanConvertFromStringProperty, !CanConvertFromString, CanConvertFromString);

            NotifyValueChanged();
            OnRootValueChanged();
        }

        private void NotifyStringValueChanged()
        {
            //OnPropertyChanged("StringValue");
            RaisePropertyChanged(StringValueProperty, null, CanConvertFromString);
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
            //OnPropertyChanged("Value");
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
    }
}
