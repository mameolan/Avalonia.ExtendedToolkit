using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Specifies a layout for categories.
    /// </summary>
    public class CategoryItemsLayout : ItemsControl//GridEntryLayout<CategoryContainer>
    {
        public Type StyleKey => typeof(CategoryItemsLayout);
    }
}
