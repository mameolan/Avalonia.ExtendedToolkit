using Avalonia.Collections;

namespace Avalonia.Controlz.Controls
{
    /// <summary>
    /// avalonia collection of doubles
    /// </summary>
    public class DoubleCollection : AvaloniaList<double>
    {
        /// <summary>
        /// returns an empty list
        /// </summary>
        /// <returns></returns>
        public static DoubleCollection Empty()
        {
            return new DoubleCollection();
        }
    }
}
