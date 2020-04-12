using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Encapsulates a method that contains an object and PropertyValueChanged event arguments.
    /// </summary>
    public delegate void PropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs e);

    /// <summary>
    /// Contains state information and data associated with property value change events.
    /// </summary>
    public sealed class PropertyValueChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Gets the old value.
        /// </summary>
        /// <value>The old value.</value>
        public object OldValue { get; private set; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        /// <value>The new value.</value>
        public object NewValue { get; private set; }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyItem Property { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueChangedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="property">The property.</param>
        /// <param name="oldValue">The old value.</param>
        public PropertyValueChangedEventArgs(RoutedEvent routedEvent, PropertyItem property, object oldValue)
          : base(routedEvent)
        {
            Property = property;
            NewValue = property.PropertyValue;
            OldValue = oldValue;
        }
    }
}
