using System.ComponentModel;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Encapsulates a method that contains an object and PropertyEditingEventArgs event arguments.
    /// </summary>
    public delegate void PropertyEditingEventHandler(object sender, PropertyEditingEventArgs e);

    /// <summary>
    /// PropertyEditingEventArgs
    /// </summary>
    public class PropertyEditingEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Gets property descriptor.
        /// </summary>
        // TODO: Replace with my wrapper?
        public PropertyDescriptor PropertyDescriptor { get; private set; }

        /// <summary>
        /// sets PropertyDescriptor
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptor"></param>
        public PropertyEditingEventArgs(RoutedEvent routedEvent, Interactive source, PropertyDescriptor propertyDescriptor)
          : base(routedEvent, source)
        {
            PropertyDescriptor = propertyDescriptor;
        }
    }
}
