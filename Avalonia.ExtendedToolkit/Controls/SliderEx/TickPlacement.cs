using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public enum TickPlacement
    {
        //
        // Zusammenfassung:
        //     Es werden keine Teilstriche angezeigt.
        None = 0,
        //
        // Zusammenfassung:
        //     Teilstriche werden bei einem horizontalen System.Windows.Controls.Slider über
        //     dem System.Windows.Controls.Primitives.Track und bei einem vertikalen System.Windows.Controls.Slider
        //     links vom System.Windows.Controls.Primitives.Track angezeigt.
        TopLeft = 1,
        //
        // Zusammenfassung:
        //     Teilstriche werden bei einem horizontalen System.Windows.Controls.Slider unter
        //     dem System.Windows.Controls.Primitives.Track und bei einem vertikalen System.Windows.Controls.Slider
        //     rechts vom System.Windows.Controls.Primitives.Track angezeigt.
        BottomRight = 2,
        //
        // Zusammenfassung:
        //     Teilstriche werden bei einem horizontalen System.Windows.Controls.Slider über
        //     und unter dem System.Windows.Controls.Primitives.Track und bei einem vertikalen
        //     System.Windows.Controls.Slider links und rechts vom System.Windows.Controls.Primitives.Track
        //     angezeigt.
        Both = 3
    }
}
