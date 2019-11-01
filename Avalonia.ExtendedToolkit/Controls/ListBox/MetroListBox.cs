using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroListBox : ListBox
    {


        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }


        public static readonly AvaloniaProperty HorizontalContentAlignmentProperty =
            AvaloniaProperty.Register<MetroListBox, HorizontalAlignment>(nameof(HorizontalContentAlignment), defaultValue: HorizontalAlignment.Left);



        public VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }


        public static readonly AvaloniaProperty VerticalContentAlignmentProperty =
            AvaloniaProperty.Register<MetroListBox, VerticalAlignment>(nameof(VerticalContentAlignment), defaultValue: VerticalAlignment.Stretch);




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
