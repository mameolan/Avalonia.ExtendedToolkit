using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Describes an object that supports filtering.
    /// </summary>
    public interface IPropertyFilterTarget
    {
        /// <summary>
        /// Occurs when filter is applied for the entry.
        /// </summary>
        event EventHandler<PropertyFilterAppliedEventArgs> FilterApplied;

        /// <summary>
        /// Applies the filter for the entry.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void ApplyFilter(PropertyFilter filter);

        /// <summary>
        /// Checks whether the entry matches the filtering predicate.
        /// </summary>
        /// <param name="predicate">The filtering predicate.</param>
        /// <returns><c>true</c> if entry matches filter; otherwise, <c>false</c>.</returns>
        bool MatchesPredicate(PropertyFilterPredicate predicate);

        /// <summary>
        /// Gets or sets a value indicating whether the entry matches filter.
        /// </summary>
        /// <value><c>true</c> if entry matches filter; otherwise, <c>false</c>.</value>
        bool MatchesFilter { get; }
    }
}
