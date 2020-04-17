using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Contains state information and data related to FilterApplied event.
    /// </summary>
    public sealed class PropertyFilterAppliedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public PropertyFilter Filter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyFilterAppliedEventArgs"/> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public PropertyFilterAppliedEventArgs(PropertyFilter filter)
        {
            Filter = filter;
        }
    }
}
