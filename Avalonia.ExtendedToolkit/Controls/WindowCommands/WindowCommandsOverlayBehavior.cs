using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    [Flags]
    public enum WindowCommandsOverlayBehavior
    {
        /// <summary>
        /// Doesn't overlay a hidden TitleBar.
        /// </summary>
        Never = 0,

        /// <summary>
        /// Overlays a hidden TitleBar.
        /// </summary>
        HiddenTitleBar = 1 << 0
    }
}
