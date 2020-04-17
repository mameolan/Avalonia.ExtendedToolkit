using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies a layout for properties.
    /// </summary>
    public class PropertyItemsLayout : GridEntryLayout<PropertyContainer>
    {
        public Type StyleKey => typeof(PropertyItemsLayout);
    }
}
