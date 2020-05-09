namespace Avalonia.Controlz.Controls
{
    
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //
    // Licensed to The Avalonia Project under MIT License, courtesy of The .NET Foundation.
    
    /// <summary>
    /// Type to control the placement of the <see cref="TickBar"/> 
    /// in the <see cref="SliderEx"/> Control
    /// </summary>
    public enum TickPlacement
    {
        /// <summary>
        /// No TickMark
        /// </summary>
        None = 0,

        /// <summary>
        /// Show TickMark above the Track (for HorizontalSlider), or left of the Track (for VerticalSlider)
        /// </summary>
        TopLeft = 1,

        /// <summary>
        /// Show TickMark below the Track (for HorizontalSlider), or right of the Track (for VerticalSlider)
        /// </summary>
        BottomRight = 2,

        /// <summary>
        /// Show TickMark on both side of the Track
        /// </summary>
        Both = 3
    }
}
