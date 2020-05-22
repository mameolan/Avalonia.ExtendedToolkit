using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specialized UI container for a category entry.
    /// </summary>
    public class CategoryContainer : GridEntryContainer
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public new Type StyleKey => typeof(CategoryContainer);

        /// <summary>
        /// Initializes the <see cref="CategoryContainer"/> class.
        /// </summary>
        static CategoryContainer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryContainer"/> class.
        /// </summary>
        public CategoryContainer()
        {
            SetParentContainer(this, this);
        }
    }
}
