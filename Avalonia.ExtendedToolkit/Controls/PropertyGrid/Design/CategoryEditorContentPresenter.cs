using System;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// content presenter for categories
    /// </summary>
    public class CategoryEditorContentPresenter : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(CategoryEditorContentPresenter);

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditorContentPresenter"/> class.
        /// </summary>
        public CategoryEditorContentPresenter()
        {
        }
    }
}
