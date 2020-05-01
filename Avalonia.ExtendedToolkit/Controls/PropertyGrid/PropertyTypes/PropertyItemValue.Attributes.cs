using System;
using System.Diagnostics;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes
{
    public partial class PropertyItemValue
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

        public static readonly DirectProperty<PropertyItemValue, PropertyItem> ParentPropertyProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, PropertyItem>(
                    nameof(ParentProperty),
                    o => o.ParentProperty);

        private PropertyItem _parentProperty;

        /// <summary>
        /// Gets the parent property.
        /// </summary>
        /// <value>The parent property.</value>
        public PropertyItem ParentProperty
        {
            get { return _parentProperty; }
            private set { SetAndRaise(ParentPropertyProperty, ref _parentProperty, value); }
        }

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

        public static readonly DirectProperty<PropertyItemValue, bool> HasSubPropertiesProperty =
                        AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                            nameof(HasSubProperties),
                            o => o.HasSubProperties);

        private bool _hasSubProperties;

        /// <summary>
        /// Gets a value indicating whether encapsulated value has sub-properties.
        /// </summary>
        /// <remarks>This property is reserved for future implementations.</remarks>
        /// <value>
        /// 	<c>true</c> if this instance has sub properties; otherwise, <c>false</c>.
        /// </value>
        public bool HasSubProperties
        {
            get { return _hasSubProperties; }
            private set { SetAndRaise(HasSubPropertiesProperty, ref _hasSubProperties, value); }
        }

        public static readonly DirectProperty<PropertyItemValue, bool> CanConvertFromStringProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(CanConvertFromString),
                    o => o.CanConvertFromString);

        /// <summary>
        /// Gets a value indicating whether this instance can convert from string.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can convert from string; otherwise, <c>false</c>.
        /// </value>
        public bool CanConvertFromString
        {
            get
            {
                return (((_parentProperty.Converter != null)
                  && _parentProperty.Converter.CanConvertFrom(typeof(string)))
                  && !_parentProperty.IsReadOnly);
            }
        }

        public static readonly DirectProperty<PropertyItemValue, bool> IsCollectionProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsCollection),
                    o => o.IsCollection);

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is collection.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if encapsulated property value is collection; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollection
        {
            get { return _parentProperty.IsCollection; }
        }

        public static readonly DirectProperty<PropertyItemValue, bool> IsDefaultValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsDefaultValue),
                    o => o.IsDefaultValue);

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is default value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if encapsulated property value is default value; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultValue
        {
            get { return _parentProperty.IsDefaultValue; }
        }

        public static readonly DirectProperty<PropertyItemValue, string> StringValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, string>(
                    nameof(StringValue),
                    o => o.StringValue, (o, v) => o.StringValue = v, defaultBindingMode: Data.BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the string representation of the value.
        /// </summary>
        /// <value>The string value.</value>
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

        public static readonly DirectProperty<PropertyItemValue, object> ValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, object>(
                    nameof(Value),
                    o => o.Value, (o, v) => o.Value = v, defaultBindingMode: Data.BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
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

        public static readonly DirectProperty<PropertyItemValue, bool> IsReadOnlyProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsReadOnly),
                    o => o.IsReadOnly);

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is read only.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return _parentProperty.IsReadOnly; }
        }

        public static readonly DirectProperty<PropertyItemValue, bool> IsEditableProperty =
                AvaloniaProperty.RegisterDirect<PropertyItemValue, bool>(
                    nameof(IsEditable),
                    o => o.IsEditable);

        /// <summary>
        /// Gets a value indicating whether encapsulated property value is editable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable
        {
            get { return !_parentProperty.IsReadOnly; }
        }
    }
}
