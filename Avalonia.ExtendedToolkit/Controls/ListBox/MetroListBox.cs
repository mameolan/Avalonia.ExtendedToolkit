using Avalonia.Controls;
using Avalonia.Controls.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroListBox : ListBox
    {
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            var itemContainer = new ItemContainerGenerator<MetroListBoxItem>(
               this,
               MetroListBoxItem.ContentProperty,
               MetroListBoxItem.ContentTemplateProperty);

            return itemContainer;
        }

    }
}
