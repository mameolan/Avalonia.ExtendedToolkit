using System;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies a layout for categories.
    /// </summary>
    public class CategoryItemsLayout : ItemsControl//GridEntryLayout<CategoryContainer>
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(CategoryItemsLayout);
    }
}
