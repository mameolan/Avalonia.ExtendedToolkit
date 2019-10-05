using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ContextMenuExt: ContextMenu
    {


        public new bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }


        public new static readonly AvaloniaProperty IsOpenProperty =
            AvaloniaProperty.Register<ContentControlEx, bool>(nameof(IsOpen));

        public ContextMenuExt()
        {
            IsOpenProperty.Changed.AddClassHandler<ContextMenuExt>((o, e) => IsOpenChanged(o, e));
        }

        private void IsOpenChanged(ContextMenuExt o, AvaloniaPropertyChangedEventArgs e)
        {
            base.SetValue(ContextMenu.IsOpenProperty, e.NewValue);
        }
    }
}
