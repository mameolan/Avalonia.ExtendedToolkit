using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.Controlz.Controls
{
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //
    // Licensed to The Avalonia Project under MIT License, courtesy of The .NET Foundation.

    /// <summary>
    /// Placement options for Slider's AutoToolTip
    /// </summary>
    public enum AutoToolTipPlacement
    {
        /// <summary>
        /// No AutoToolTip
        /// </summary>
        None,

        /// <summary>
        /// Show AutoToolTip at top edge of Thumb (for HorizontalSlider), or at left edge of Thumb (for VerticalSlider)
        /// </summary>
        TopLeft,

        /// <summary>
        /// Show AutoToolTip at bottom edge of Thumb (for HorizontalSlider), or at right edge of Thumb (for VerticalSlider)
        /// </summary>
        BottomRight,

        // NOTE: if you add or remove any values in this enum, be sure to update Slider.IsValidAutoToolTipPlacement()
    };
}
