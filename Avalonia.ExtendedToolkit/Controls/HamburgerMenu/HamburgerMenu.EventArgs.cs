using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ItemClickEventArgs : RoutedEventArgs
    {
        /// <inheritdoc />
        public ItemClickEventArgs()
        {
        }

        /// <inheritdoc />
        public ItemClickEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        {
        }

        /// <inheritdoc />
        public ItemClickEventArgs(RoutedEvent routedEvent, IInteractive source)
            : base(routedEvent, source)
        {
        }

        /// <summary>
        /// Gets the clicked item
        /// </summary>
        public object ClickedItem { get; internal set; }
    }

    /// <summary>
    /// RoutedEventHandler used for the <see cref="HamburgerMenu"/> ItemClick and OptionsItemClick event.
    /// </summary>
    public delegate void ItemClickRoutedEventHandler(object sender, ItemClickEventArgs args);

    public class HamburgerMenuItemInvokedEventArgs : RoutedEventArgs
    {
        /// <inheritdoc />
        public HamburgerMenuItemInvokedEventArgs()
        {
        }

        /// <inheritdoc />
        public HamburgerMenuItemInvokedEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        {
        }

        /// <inheritdoc />
        public HamburgerMenuItemInvokedEventArgs(RoutedEvent routedEvent, IInteractive source)
            : base(routedEvent, source)
        {
        }

        /// <summary>
        /// Gets the invoked item
        /// </summary>
        public object InvokedItem { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the invoked item is an options item
        /// </summary>
        public bool IsItemOptions { get; internal set; }
    }

    /// <summary>
    /// RoutedEventHandler used for the <see cref="HamburgerMenu"/> ItemInvoked event.
    /// </summary>
    public delegate void HamburgerMenuItemInvokedRoutedEventHandler(object sender, HamburgerMenuItemInvokedEventArgs args);

}
