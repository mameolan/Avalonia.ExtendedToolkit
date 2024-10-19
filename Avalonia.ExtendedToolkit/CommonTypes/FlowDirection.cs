namespace Avalonia.ExtendedToolkit.Controls
{
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)

    /// <summary>
    /// The 'flow-direction' property specifies whether the primary text advance
    /// direction shall be left-to-right or right-to-left.
    /// </summary>
    public enum FlowDirection
    {
        /// <internalonly>
        /// Sets the primary text advance direction to left-to-right, and the line
        /// progression direction to top-to-bottom as is common in most Roman-based
        /// documents. For most characters, the current text position is advanced
        /// from left to right after each glyph is rendered. The 'direction' property
        /// is set to 'ltr'.
        /// </internalonly>
        LeftToRight,

        /// <internalonly>
        /// Sets the primary text advance direction to right-to-left, and the line
        /// progression direction to top-to-bottom as is common in Arabic or Hebrew
        /// scripts. The direction property is set to 'rtl'.
        /// </internalonly>
        RightToLeft
    }
}
