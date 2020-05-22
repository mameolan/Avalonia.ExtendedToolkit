using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// overlay behaviour
    /// </summary>
    [Flags]
    public enum OverlayBehavior
    {
        /// <summary>
        /// Doesn't overlay Flyouts nor a hidden TitleBar.
        /// </summary>
        Never = 0,

        /// <summary>
        /// Overlays opened <see cref="Flyout"/> controls.
        /// </summary>
        Flyouts = 1 << 0,

        /// <summary>
        /// Overlays a hidden TitleBar.
        /// </summary>
        HiddenTitleBar = 1 << 1,
        /// <summary>
        /// always overlay
        /// </summary>
        Always = ~(-1 << 2)
    }
}
