using System;
using System.Collections.ObjectModel;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// OverflowMenuCreatedEventArgs
    /// for sending the menuitems
    /// </summary>
    public class OverflowMenuCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// sets the MenuItems
        /// </summary>
        /// <param name="menuItems"></param>
        public OverflowMenuCreatedEventArgs(Collection<object> menuItems)
            : base()
        {
            this.MenuItems = menuItems;
        }

        /// <summary>
        /// Menuitems to send
        /// </summary>
        public Collection<object> MenuItems { get; private set; }
    }
}
