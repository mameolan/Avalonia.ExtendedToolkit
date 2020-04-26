using System;
using Avalonia.Controls;

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

        public static readonly StyledProperty<bool> CanCloseProperty =
            AvaloniaProperty.Register<TabbedLayoutItem, bool>(nameof(CanClose));





    }
}
