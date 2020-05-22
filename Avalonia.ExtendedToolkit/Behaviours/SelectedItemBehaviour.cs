using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.ExtendedToolkit.Behaviours
{
    /// <summary>
    /// tries to set the selected item
    /// on a <see cref="SelectingItemsControl"/>
    /// </summary>
    public class SelectedItemBehaviour:Trigger<SelectingItemsControl>
    {
        /// <summary>
        /// adds selection changed event to the associated object
        /// if items is not null try selected the first index
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += SelectingItemsControl_SelectionChanged;

            if(AssociatedObject.Items!=null)
            {
                AssociatedObject.SelectionChanged -= SelectingItemsControl_SelectionChanged;
                AssociatedObject.SelectedIndex = 0;

                AssociatedObject.SelectionChanged += SelectingItemsControl_SelectionChanged;
            }

        }

        /// <summary>
        /// removes the selectionchanged event
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= SelectingItemsControl_SelectionChanged;
        }

        /// <summary>
        /// tries to select the item by index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectingItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = e.AddedItems.OfType<object>().ToList();

            if (AssociatedObject.Items != null && selectedItems.Any())
            {
                var list = AssociatedObject.Items.OfType<object>().ToList();

                var newIndex=list.IndexOf(selectedItems.First());

                AssociatedObject.SelectionChanged -= SelectingItemsControl_SelectionChanged;
                AssociatedObject.SelectedIndex = newIndex;
                //AssociatedObject.SelectedItem = selectedItems.First();
                AssociatedObject.SelectionChanged += SelectingItemsControl_SelectionChanged;


            }

        }
    }
}
