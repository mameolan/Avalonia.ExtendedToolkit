using System;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Defines a content presenter control for a Property editor.
    /// </summary>
    public sealed class PropertyEditorContentPresenter : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(PropertyEditorContentPresenter);

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyEditorContentPresenter"/> class.
        /// </summary>
        public PropertyEditorContentPresenter()
        {
        }
    }
}
