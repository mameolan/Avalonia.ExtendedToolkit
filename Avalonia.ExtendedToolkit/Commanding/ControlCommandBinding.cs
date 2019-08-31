using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Commanding
{
    public class ControlCommandBinding<T> : Control where T : Control
    {
        public CommandBindingCollection CommandBindings { get; set; } = new CommandBindingCollection();


    }
}
