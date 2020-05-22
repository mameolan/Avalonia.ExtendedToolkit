using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Native Tab item for <see cref="TabbedLayout"/>
    /// </summary>
    public partial class TabbedLayoutItem : TabItem
    {
        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(TabbedLayoutItem);
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance can close.
        /// </summary>
        /// <value><c>true</c> if this instance can close; otherwise, <c>false</c>.</value>
        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }


        /// <summary>
        /// <see cref="CanClose"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanCloseProperty =
            AvaloniaProperty.Register<TabbedLayoutItem, bool>(nameof(CanClose));


        /// <summary>
        /// command which is set by the TabbedLayout Control
        /// </summary>
        public ICommand ClosePropertyTabCommand
        {
            get { return (ICommand)GetValue(ClosePropertyTabCommandProperty); }
            set { SetValue(ClosePropertyTabCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ClosePropertyTabCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ClosePropertyTabCommandProperty =
            AvaloniaProperty.Register<TabbedLayoutItem, ICommand>(nameof(ClosePropertyTabCommand));

    }
}
