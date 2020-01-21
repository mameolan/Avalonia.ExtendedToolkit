namespace Avalonia.Controlz.Controls
{
    /// <summary>
    /// // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //
    // Licensed to The Avalonia Project under MIT License, courtesy of The .NET Foundation.
    /// </summary>
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