using Avalonia.Collections;
using System;
using System.Collections.Generic;
using System.Text;

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
