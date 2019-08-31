using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Commanding
{
    public class TemplatedControlCommandBinding<T> : TemplatedControl where T : TemplatedControl
    {
        public CommandBindingCollection CommandBindings { get; set; } = new CommandBindingCollection();
    }
}
