using System.ComponentModel;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Encapsulates a method that contains an object and PropertyEditingEventArgs event arguments.
    /// </summary>
    public delegate void PropertyEditingEventHandler(object sender, PropertyEditingEventArgs e);

    public class PropertyEditingEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Gets property descriptor.
        /// </summary>
        // TODO: Replace with my wrapper?
        public PropertyDescriptor PropertyDescriptor { get; private set; }

        public PropertyEditingEventArgs(RoutedEvent routedEvent, IInteractive source, PropertyDescriptor propertyDescriptor)
          : base(routedEvent, source)
        {
            PropertyDescriptor = propertyDescriptor;
        }
    }
}
