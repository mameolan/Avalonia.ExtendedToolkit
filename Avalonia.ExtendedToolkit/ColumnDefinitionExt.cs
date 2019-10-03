using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit
{
    public class ColumnDefinitionExt: ColumnDefinition
    {


        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }


        public static readonly AvaloniaProperty NameProperty =
            AvaloniaProperty.Register<ColumnDefinitionExt, string>(nameof(Name));


    }
}
