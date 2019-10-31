using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class HamburgerMenu
    {


        public static RoutedEvent<ItemClickEventArgs> ItemClickEvent =
                    RoutedEvent.Register<HamburgerMenu, ItemClickEventArgs>(nameof(ItemClickEvent), RoutingStrategies.Direct);

        public event ItemClickRoutedEventHandler ItemClick
        {
            add
            {
                AddHandler(ItemClickEvent, value);
            }
            remove
            {
                RemoveHandler(ItemClickEvent, value);
            }
        }



        public static RoutedEvent<ItemClickEventArgs> OptionsItemClickEvent =
                    RoutedEvent.Register<HamburgerMenu, ItemClickEventArgs>(nameof(OptionsItemClickEvent), RoutingStrategies.Direct);

        public event ItemClickRoutedEventHandler OptionsItemClick
        {
            add
            {
                AddHandler(OptionsItemClickEvent, value);
            }
            remove
            {
                RemoveHandler(OptionsItemClickEvent, value);
            }
        }



        public static RoutedEvent<HamburgerMenuItemInvokedEventArgs> ItemInvokedEvent =
                    RoutedEvent.Register<HamburgerMenu, HamburgerMenuItemInvokedEventArgs>(nameof(ItemInvokedEvent), RoutingStrategies.Direct);

        public event HamburgerMenuItemInvokedRoutedEventHandler ItemInvoked
        {
            add
            {
                AddHandler(ItemInvokedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemInvokedEvent, value);
            }
        }



        public static RoutedEvent<RoutedEventArgs> HamburgerButtonClickEvent =
                    RoutedEvent.Register<HamburgerMenu, RoutedEventArgs>(nameof(HamburgerButtonClickEvent), RoutingStrategies.Direct);

        public event EventHandler HamburgerButtonClick
        {
            add
            {
                AddHandler(HamburgerButtonClickEvent, value);
            }
            remove
            {
                RemoveHandler(HamburgerButtonClickEvent, value);
            }
        }

        private void OnHamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(HamburgerMenu.HamburgerButtonClickEvent, (IInteractive)sender);
            this.RaiseEvent(args);

            if (!args.Handled)
            {
                IsPaneOpen = !IsPaneOpen;
            }
        }

        private bool OnItemClick()
        {
            var selectedItem = _buttonsListView.SelectedItem;

            (selectedItem as HamburgerMenuItem)?.RaiseCommand();
            RaiseItemCommand();

            var raiseItemEvents = this.RaiseItemEvents(selectedItem);
            if (raiseItemEvents && _optionsListView != null)
            {
                _optionsListView.SelectedIndex = -1;
            }

            return raiseItemEvents;
        }

        private bool RaiseItemEvents(object selectedItem)
        {
            if (selectedItem is null)
            {
                return false;
            }

            var itemClickEventArgs = new ItemClickEventArgs(ItemClickEvent, this) { ClickedItem = selectedItem };
            this.RaiseEvent(itemClickEventArgs);

            var hamburgerMenuItemInvokedEventArgs = new HamburgerMenuItemInvokedEventArgs(ItemInvokedEvent, this) { InvokedItem = selectedItem, IsItemOptions = false };
            this.RaiseEvent(hamburgerMenuItemInvokedEventArgs);

            return !itemClickEventArgs.Handled && !hamburgerMenuItemInvokedEventArgs.Handled;
        }

        private bool OnOptionsItemClick()
        {
            var selectedItem = _optionsListView.SelectedItem;

            (selectedItem as HamburgerMenuItem)?.RaiseCommand();
            RaiseOptionsItemCommand();

            var raiseOptionsItemEvents = this.RaiseOptionsItemEvents(selectedItem);
            if (raiseOptionsItemEvents && _buttonsListView != null)
            {
                _buttonsListView.SelectedIndex = -1;
            }

            return raiseOptionsItemEvents;
        }

        private bool RaiseOptionsItemEvents(object selectedItem)
        {
            if (selectedItem is null)
            {
                return false;
            }

            var itemClickEventArgs = new ItemClickEventArgs(OptionsItemClickEvent, this) { ClickedItem = selectedItem };
            this.RaiseEvent(itemClickEventArgs);

            var hamburgerMenuItemInvokedEventArgs = new HamburgerMenuItemInvokedEventArgs(ItemInvokedEvent, this) { InvokedItem = selectedItem, IsItemOptions = true };
            this.RaiseEvent(hamburgerMenuItemInvokedEventArgs);

            return !itemClickEventArgs.Handled && !hamburgerMenuItemInvokedEventArgs.Handled;
        }

        private void ButtonsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null)
            {
                return;
            }

            listBox.SelectionChanged -= this.ButtonsListView_SelectionChanged;

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var canItemClick = OnItemClick();

                if (!canItemClick)
                {
                    // The following lines will fire another SelectionChanged event.
                    if (e.RemovedItems.Count > 0)
                    {
                        listBox.SelectedItem = e.RemovedItems[0];
                    }
                    else
                    {
                        listBox.SelectedIndex = -1;
                    }
                }
            }

            listBox.SelectionChanged += this.ButtonsListView_SelectionChanged;
        }

        private void OptionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null)
            {
                return;
            }

            listBox.SelectionChanged -= this.OptionsListView_SelectionChanged;

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var canItemClick = OnOptionsItemClick();

                if (!canItemClick)
                {
                    // The following lines will fire another SelectionChanged event.
                    if (e.RemovedItems.Count > 0)
                    {
                        listBox.SelectedItem = e.RemovedItems[0];
                    }
                    else
                    {
                        listBox.SelectedIndex = -1;
                    }
                }
            }

            listBox.SelectionChanged += this.OptionsListView_SelectionChanged;
        }


    }
}
