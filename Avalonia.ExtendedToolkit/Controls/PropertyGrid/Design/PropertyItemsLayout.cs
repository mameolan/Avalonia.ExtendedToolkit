using System;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies a layout for properties.
    /// </summary>
    public class PropertyItemsLayout : ItemsControl//GridEntryLayout<PropertyContainer>
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(PropertyItemsLayout);

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyItemsLayout"/> class.
        /// </summary>
        public PropertyItemsLayout()
        {
            
        }

    }
}
