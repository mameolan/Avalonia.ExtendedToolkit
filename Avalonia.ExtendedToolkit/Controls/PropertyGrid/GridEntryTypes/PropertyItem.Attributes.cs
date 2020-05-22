using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //
    [DebuggerDisplay("DisplayName= {DisplayName} Component= {Component}")]
    public partial class PropertyItem
    {
        private readonly PropertyItemValue _parentValue;

        private readonly object _component;
        private object _unwrappedComponent;
        private readonly PropertyDescriptor _descriptor;
        private readonly AttributesContainer _metadata;

        // TODO: Reserved for future implementations.
        /// <summary>
        /// Gets the parent value.
        /// <remarks>This property is reserved for future implementations</remarks>
        /// </summary>
        /// <value>The parent value.</value>
        public PropertyItemValue ParentValue
        {
            get { return _parentValue; }
        }

        /// <summary>
        /// <see cref="PropertyValue"/>
        /// </summary>
        public static readonly DirectProperty<PropertyItem, PropertyItemValue> PropertyValueProperty =
                AvaloniaProperty.RegisterDirect<PropertyItem, PropertyItemValue>(
                    nameof(PropertyValue),
                    o => o.PropertyValue/*, defaultBindingMode: Data.BindingMode.TwoWay*/);

        private PropertyItemValue _propertyValue;

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <value>The property value.</value>
        public PropertyItemValue PropertyValue
        {
            get
            {
                if (_propertyValue == null)
                    PropertyValue = CreatePropertyValueInstance();
                return _propertyValue;
            }
            private set { SetAndRaise(PropertyValueProperty, ref _propertyValue, value); }
        }

        /// <summary>
        /// Gets PropertyDescriptor instance for the underlying property.
        /// </summary>
        public PropertyDescriptor PropertyDescriptor
        {
            get { return _descriptor; }
        }

        /// <summary>
        /// <see cref="DisplayName"/>
        /// </summary>
        public static readonly DirectProperty<PropertyItem, string> DisplayNameProperty =
                AvaloniaProperty.RegisterDirect<PropertyItem, string>(
                    nameof(DisplayName),
                    o => o.DisplayName, (o, v) => o.DisplayName=v,defaultBindingMode: Data.BindingMode.TwoWay);

        private string _displayName;

        /// <summary>
        /// Gets the display name for the property.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The display name for the property.
        /// </returns>
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayName))
                    DisplayName = GetDisplayName();
                return _displayName;
            }

            set
            {
                if (_displayName == value)
                    return;
                SetAndRaise(DisplayNameProperty, ref _displayName, value);
            }
        }

        private readonly string _categoryName;

        /// <summary>
        /// Gets the name of the category that this property resides in.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The name of the category that this property resides in.
        /// </returns>
        public string CategoryName
        {
            get { return _categoryName; }
        }

        /// <summary>
        /// <see cref="Description"/>
        /// </summary>
        public static readonly DirectProperty<PropertyItem, string> DescriptionProperty =
                AvaloniaProperty.RegisterDirect<PropertyItem, string>(
                    nameof(Description),
                    o => o.Description, (o, v) => o.Description = v, defaultBindingMode: Data.BindingMode.TwoWay);

        private string _description;

        /// <summary>
        /// Gets the description of the encapsulated property.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The description of the encapsulated property.
        /// </returns>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                SetAndRaise(DescriptionProperty, ref _description, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the encapsulated property is an advanced property.
        /// </summary>
        /// <returns>true if the encapsulated property is an advanced property; otherwise, false.</returns>
        // TODO: move intilialization to ctor
        public bool IsAdvanced
        {
            get
            {
                var browsable = (EditorBrowsableAttribute)Attributes[typeof(EditorBrowsableAttribute)];
                return browsable != null && browsable.State == EditorBrowsableState.Advanced;
            }
        }

        private readonly bool _isLocalizable;

        /// <summary>
        /// Gets a value indicating whether the encapsulated property is localizable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is localizable; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocalizable
        {
            get { return _isLocalizable; }
        }

        /// <summary>
        /// <see cref="IsReadOnly"/>
        /// </summary>
        public static readonly DirectProperty<PropertyItem, bool> IsReadOnlyProperty =
                AvaloniaProperty.RegisterDirect<PropertyItem, bool>(
                    nameof(IsReadOnly),
                    o => o.IsReadOnly, (o, v) => o.IsReadOnly=v, unsetValue: false, defaultBindingMode: Data.BindingMode.TwoWay);

        private bool _isReadOnly;

        /// <summary>
        /// Gets a value indicating whether the encapsulated property is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the encapsulated property is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                if (_isReadOnly == value)
                    return;

                SetAndRaise(IsReadOnlyProperty, ref _isReadOnly, value);
            }
        }

        /// <summary>
        /// Gets the type of the encapsulated property.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The type of the encapsulated property.
        /// </returns>
        public virtual Type PropertyType
        {
            get
            {
                if (_descriptor == null)
                    return null;
                return _descriptor.PropertyType;
            }
        }

        /// <summary>
        /// Gets the standard values that the encapsulated property supports.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="ICollection"/> of standard values that the encapsulated property supports.
        /// </returns>
        public ICollection StandardValues
        {
            get
            {
                if (Converter.GetStandardValuesSupported())
                    return Converter.GetStandardValues();

                return new ArrayList(0);
            }
        }

        /// <summary>
        /// Gets the component the property belongs to.
        /// </summary>
        /// <value>The component.</value>
        public object Component
        {
            get { return _component; }
        }

        /// <summary>
        /// Gets the component the property belongs to.
        /// </summary>
        /// <remarks>
        /// This property returns a real unwrapped component even if a custom type descriptor is used.
        /// </remarks>
        public object UnwrappedComponent
        {
            get { return _unwrappedComponent ?? (_unwrappedComponent = ObjectServices.GetUnwrappedObject(_component)); }
        }

        /// <summary>
        /// Gets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        public object ToolTip
        {
            get
            {
                var attribute = GetAttribute<DescriptionAttribute>();
                return (attribute != null && !string.IsNullOrEmpty(attribute.Description))
                  ? attribute.Description
                  : DisplayName;
            }
        }

        /// <summary>
        /// Gets the custom attributes bound to property.
        /// </summary>
        /// <value>The attributes.</value>
        public virtual AttributeCollection Attributes
        {
            get
            {
                if (_descriptor == null)
                    return null;
                return _descriptor.Attributes;
            }
        }

        /// <summary>
        /// Gets the custom attributes container.
        /// </summary>
        /// <value>The custom attributes container.</value>
        public AttributesContainer Metadata
        {
            get { return _metadata; }
        }

        /// <summary>
        /// Gets the converter.
        /// </summary>
        /// <value>The converter.</value>
        public TypeConverter Converter
        {
            get { return ObjectServices.GetPropertyConverter(_descriptor); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can clear value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can clear value; otherwise, <c>false</c>.
        /// </value>
        public bool CanClearValue
        {
            get { return _descriptor.CanResetValue(_component); }
        }

        // TODO: support this (UI should also react on it)
        /// <summary>
        /// Gets a value indicating whether this instance is default value.
        /// <remarks>This property is reserved for future implementations.</remarks>
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default value; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultValue
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is collection.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is collection; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollection
        {
            get { return typeof(IList).IsAssignableFrom(PropertyType); }
        }

        /// <summary>
        /// Occurs when property value is changed.
        /// </summary>
        public event Action<PropertyItem, object, object> ValueChanged;
    }
}
