namespace Avalonia.Controlz.Controls
{
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //
    // Licensed to The Avalonia Project under MIT License, courtesy of The .NET Foundation.

    /// <summary>
    /// Enum which describes how to position the TickBar.
    /// </summary>
    public enum TickBarPlacement
    {
        /// <summary>
        /// Position this tick at the left of target element.
        /// </summary>
        Left,

        /// <summary>
        /// Position this tick at the top of target element.
        /// </summary>
        Top,

        /// <summary>
        /// Position this tick at the right of target element.
        /// </summary>
        Right,

        /// <summary>
        /// Position this tick at the bottom of target element.
        /// </summary>
        Bottom,

        // NOTE: if you add or remove any values in this enum, be sure to update TickBar.IsValidTickBarPlacement()
    }
}