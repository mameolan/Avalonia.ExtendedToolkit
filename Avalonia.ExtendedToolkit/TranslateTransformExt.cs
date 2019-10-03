using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit
{
    public class TranslateTransformExt: TranslateTransform
    {


        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }


        public static readonly AvaloniaProperty NameProperty =
            AvaloniaProperty.Register<TranslateTransformExt, string>(nameof(Name));


    }
}
