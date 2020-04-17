using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.ExtendedToolkit.Behaviours
{
    public class SelectedItemBehaviour:Trigger<SelectingItemsControl>
    {
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += SelectingItemsControl_SelectionChanged;

            if(AssociatedObject.Items!=null)
            {
                AssociatedObject.SelectionChanged -= SelectingItemsControl_SelectionChanged;
                AssociatedObject.SelectedItem = 0;

                AssociatedObject.SelectionChanged += SelectingItemsControl_SelectionChanged;
            }

        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= SelectingItemsControl_SelectionChanged;
            

        }

        private void SelectingItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = e.AddedItems.OfType<Object>().ToList();

            if (AssociatedObject.Items != null && selectedItems.Any())
            {
                var list = AssociatedObject.Items.OfType<object>().ToList();

                var newIndex=list.IndexOf(selectedItems.First());

                AssociatedObject.SelectionChanged -= SelectingItemsControl_SelectionChanged;
                AssociatedObject.SelectedIndex = newIndex;
                AssociatedObject.SelectedItem = selectedItems.First();
                AssociatedObject.SelectionChanged += SelectingItemsControl_SelectionChanged;


            }

        }
    }
}
