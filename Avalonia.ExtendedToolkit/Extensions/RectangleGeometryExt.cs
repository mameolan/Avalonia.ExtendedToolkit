using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit
{
    public class RectangleGeometryExt: RectangleGeometry
    {


        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }


        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<RectangleGeometryExt, string>(nameof(Name));


    }
}
