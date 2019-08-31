using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Commanding
{
    public class ItemsControlCommandBinding<T> : ItemsControl where T : ItemsControl
    {
        public CommandBindingCollection CommandBindings { get; set; } = new CommandBindingCollection();
    }
}
