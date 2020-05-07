using System;
using System.Globalization;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Property filter predicate.
    /// </summary>
    public class PropertyFilterPredicate
    {
        /// <summary>
        /// Gets or sets the match text.
        /// </summary>
        /// <value>The match text.</value>
        public string MatchText { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyFilterPredicate"/> class.
        /// </summary>
        /// <param name="matchText">The match text.</param>
        public PropertyFilterPredicate(string matchText)
        {
            if (matchText == null)
                throw new ArgumentNullException(nameof(matchText));
            MatchText = matchText.ToUpper(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Matches the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><c>true</c> if target matches predicate; otherwise, <c>false</c>.</returns>
        public virtual bool Match(string target)
        {
            return ((target != null) && target.ToUpper(CultureInfo.CurrentCulture).Contains(MatchText));
        }
    }
}
