namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// Specifies what property to convert.
    /// belongs to <see cref="PathConversionEventArgs"/>
    /// </summary>
    public enum ConversionMode
    {
        /// <summary>
        /// Convert the display path to edit path.
        /// </summary>
        DisplayToEdit,

        /// <summary>
        /// convert the edit path to display path.
        /// </summary>
        EditToDisplay,
    }
}
