using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// A FlyoutsControl is for displaying flyouts in a MetroWindow.
    /// <see cref="MetroWindow"/>
    /// </summary>
    public class FlyoutsControl : ItemsControl
    {
        /// <summary>
        /// Gets/sets whether
        /// <see cref="Avalonia.ExtendedToolkit.Controls.Flyout.ExternalCloseButton"/>
        /// is ignored and all flyouts behave as if it was set to the value of this property.
        /// </summary>
        public MouseButton? OverrideExternalCloseButton
        {
            get { return (MouseButton?)GetValue(OverrideExternalCloseButtonProperty); }
            set { SetValue(OverrideExternalCloseButtonProperty, value); }
        }

        /// <summary>
        /// <see cref="OverrideExternalCloseButton"/>
        /// </summary>
        public static readonly StyledProperty<MouseButton?> OverrideExternalCloseButtonProperty =
            AvaloniaProperty.Register<FlyoutsControl, MouseButton?>(nameof(OverrideExternalCloseButton));

        /// <summary>
        /// Gets/sets whether
        /// <see cref="Avalonia.ExtendedToolkit.Controls.Flyout.IsPinned"/>
        /// is ignored and all flyouts behave as if it was set false.
        /// </summary>
        public bool OverrideIsPinned
        {
            get { return (bool)GetValue(OverrideIsPinnedProperty); }
            set { SetValue(OverrideIsPinnedProperty, value); }
        }

        /// <summary>
        /// <see cref="OverrideIsPinned"/>
        /// </summary>
        public static readonly StyledProperty<bool> OverrideIsPinnedProperty =
            AvaloniaProperty.Register<FlyoutsControl, bool>(nameof(OverrideIsPinned));

        /// <summary>
        /// registers changed events
        /// </summary>
        public FlyoutsControl()
        {
            //ItemsPanelProperty.OverrideDefaultValue<FlyoutsControl>(DefaultPanel);
            ItemsSourceProperty.Changed.AddClassHandler<FlyoutsControl>((o, e) => OnItemsChaned(o, e));
        }

        private void OnItemsChaned(FlyoutsControl o, AvaloniaPropertyChangedEventArgs e)
        {
            this.IsVisible = true;
        }

        /// <summary>
        /// use FlyoutContainerGenerator as container generator
        /// </summary>
        /// <returns></returns>
        //.orig
        //protected override ItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    return new FlyoutContainerGenerator(this);
        //}

        /// <summary>
        /// called from the <see cref="FlyoutContainerGenerator.CreateContainer(object)"/> only.
        /// </summary>
        /// <param name="flyout"></param>
        internal void AttachHandlers(Flyout flyout)
        {
            flyout.IsOpenChanged -= FlyoutStatusChanged;
            flyout.FlyoutThemeChanged -= FlyoutStatusChanged;

            flyout.IsOpenChanged += FlyoutStatusChanged;
            flyout.FlyoutThemeChanged += FlyoutStatusChanged;

            //var isOpenNotifier = new PropertyChangeNotifier(flyout, Flyout.IsOpenProperty);
            //isOpenNotifier.ValueChanged += FlyoutStatusChanged;
            //flyout.IsOpenPropertyChangeNotifier = isOpenNotifier;

            //var themeNotifier = new PropertyChangeNotifier(flyout, Flyout.FlyoutThemeProperty);
            //themeNotifier.ValueChanged += FlyoutStatusChanged;
            //flyout.ThemePropertyChangeNotifier = themeNotifier;
        }

        private void FlyoutStatusChanged(object sender, EventArgs e)
        {
            var flyout = this.GetFlyout(sender); //Get the flyout that raised the handler.

            this.HandleFlyoutStatusChange(flyout, this.TryFindParent<MetroWindow>());
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
                lastChanged.IsVisible = true;

                lastChanged.ZIndex = index;
                //Panel.SetZIndex(lastChanged, index);
            }
        }
    }
}
