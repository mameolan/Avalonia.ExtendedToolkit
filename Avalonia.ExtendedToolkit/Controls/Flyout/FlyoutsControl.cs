using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Avalonia.LogicalTree;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class FlyoutsControl : ItemsControl
    {
        private static readonly FuncTemplate<IPanel> DefaultPanel =
            new FuncTemplate<IPanel>(() => new VirtualizingStackPanel());

        public MouseButton? OverrideExternalCloseButton
        {
            get { return (MouseButton?)GetValue(OverrideExternalCloseButtonProperty); }
            set { SetValue(OverrideExternalCloseButtonProperty, value); }
        }


        public static readonly AvaloniaProperty OverrideExternalCloseButtonProperty =
            AvaloniaProperty.Register<FlyoutsControl, MouseButton?>(nameof(OverrideExternalCloseButton));



        public bool OverrideIsPinned
        {
            get { return (bool)GetValue(OverrideIsPinnedProperty); }
            set { SetValue(OverrideIsPinnedProperty, value); }
        }


        public static readonly AvaloniaProperty OverrideIsPinnedProperty =
            AvaloniaProperty.Register<FlyoutsControl, bool>(nameof(OverrideIsPinned));


        public FlyoutsControl()
        {
            //ItemsPanelProperty.OverrideDefaultValue<FlyoutsControl>(DefaultPanel);
            //ItemsProperty.AddOwner<FlyoutsControl>((o,e)=> ONi)
        }

        
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            var itemContainer= new ItemContainerGenerator<Flyout>(
               this,
               Flyout.ContentProperty,
               Flyout.ContentTemplateProperty);

            itemContainer.Materialized += ItemContainer_Materialized;
            itemContainer.Dematerialized += ItemContainer_Dematerialized;
            itemContainer.Recycled += ItemContainer_Recycled;

            return itemContainer;
            //return new ItemContainerGenerator(this);
        }

        private void ItemContainer_Recycled(object sender, ItemContainerEventArgs e)
        {
            
        }

        private void ItemContainer_Dematerialized(object sender, ItemContainerEventArgs e)
        {
            
        }

        private void ItemContainer_Materialized(object sender, ItemContainerEventArgs e)
        {
            
        }

        protected override void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.ItemsCollectionChanged(sender, e);
            this.IsVisible = true;
        }



        internal void HandleFlyoutStatusChange(Flyout flyout, MetroWindow parentWindow)
        {
            if (flyout == null || parentWindow == null)
            {
                return;
            }

            this.ReorderZIndices(flyout);

            var visibleFlyouts = this.GetFlyouts(this.Items).Where(i => i.IsOpen).OrderBy(x => x.ZIndex).ToList();
            parentWindow.HandleFlyoutStatusChange(flyout, visibleFlyouts);
        }

        private Flyout GetFlyout(object item)
        {
            var flyout = item as Flyout;
            if (flyout != null)
            {
                return flyout;
            }

            return (Flyout)item;




            //int index = this.ItemContainerGenerator.IndexFromContainer(DefaultPanel);



            //return (Flyout)this.ItemContainerGenerator.ContainerFromIndex(index);
        }

        internal IEnumerable<Flyout> GetFlyouts()
        {
            return GetFlyouts(this.Items);
        }

        private IEnumerable<Flyout> GetFlyouts(IEnumerable items)
        {
            return from object item in items select this.GetFlyout(item);
        }

        private void ReorderZIndices(Flyout lastChanged)
        {
            var openFlyouts = this.GetFlyouts(this.Items).Where(i => i.IsOpen && i != lastChanged).OrderBy(x => x.ZIndex);
            var index = 0;
            foreach (var openFlyout in openFlyouts)
            {
                openFlyout.ZIndex = index;
                //Panel.SetZIndex(openFlyout, index);
                index++;
            }

            if (lastChanged.IsOpen)
            {
                //lastChanged.IsVisible = true;

                lastChanged.ZIndex = index;
                //Panel.SetZIndex(lastChanged, index);
            }
        }

    }
}
