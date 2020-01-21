using Avalonia.Collections;

namespace Avalonia.Controlz.Controls
{
    public class DoubleCollection : AvaloniaList<double>
    {
        public static DoubleCollection Empty()
        {
            return new DoubleCollection();
        }
    }
}