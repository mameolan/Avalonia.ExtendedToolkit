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

        protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        {
            base.OnContainersMaterialized(e);
        }

        protected override void OnContainersRecycled(ItemContainerEventArgs e)
        {
            base.OnContainersRecycled(e);
        }

        protected override void OnContainersDematerialized(ItemContainerEventArgs e)
        {
            base.OnContainersDematerialized(e);
        }




        //protected override IItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    return new ItemContainerGenerator<Flyout>(
        //       this,
        //       Flyout.ContentProperty,
        //       Flyout.ContentTemplateProperty);

        //    //return new ItemContainerGenerator(this);
        //}

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
            //return (Flyout)this.ItemContainerGenerator.ContainerFromItem(item);
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
