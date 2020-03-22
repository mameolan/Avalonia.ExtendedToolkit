using Avalonia.Collections;

namespace Avalonia.Controlz.Controls
{
    /// <summary>
    /// avalonia collection of doubles
    /// </summary>
    public class DoubleCollection : AvaloniaList<double>
    {
        public static DoubleCollection Empty()
        {
            return new DoubleCollection();
        }
    }
}
