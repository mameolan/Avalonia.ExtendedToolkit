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
        public Type StyleKey => typeof(PropertyItemsLayout);

        public PropertyItemsLayout()
        {
            
        }

    }
}
