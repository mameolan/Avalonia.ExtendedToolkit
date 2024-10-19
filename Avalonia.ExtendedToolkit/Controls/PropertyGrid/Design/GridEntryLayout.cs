using Avalonia.Controls;
using Avalonia.Controls.Generators;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Defines a basement for GridEntry UI layouts (panels, lists, etc)
    /// </summary>
    /// <typeparam name="T">The type of elements in the control.</typeparam>
    public abstract class GridEntryLayout<T> : ItemsControl where T : GridEntryContainer, new()
    {
        /// <summary>
        /// uses the GridEntryLayoutContainer as Generator
        /// </summary>
        /// <returns></returns>
        //.orig
        //protected override ItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    var result = new GridEntryLayoutContainer<T>(this);
        //    return result;
        //}
    }
}
