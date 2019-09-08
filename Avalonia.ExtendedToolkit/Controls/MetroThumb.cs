using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroThumb : Thumb, IMetroThumb
    {
        public event MouseButtonEventHandler MouseDoubleClick;
        public event MouseButtonEventHandler MouseRightButtonUp;

    }
}
