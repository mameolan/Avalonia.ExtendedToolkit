using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Commanding
{
    public delegate void CancelRoutedEventHandler(object sender, CancelRoutedEventArgs e);
    public class CancelRoutedEventArgs : RoutedEventArgs
    {
        public CancelRoutedEventArgs()
      : base()
        {
        }

        public CancelRoutedEventArgs(RoutedEvent routedEvent)
      : base(routedEvent)
        {
        }

        public CancelRoutedEventArgs(RoutedEvent routedEvent, IInteractive source)
          : base(routedEvent, source)
        {
        }

        public bool Cancel { get; set; }

    }
}
