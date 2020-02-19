using Avalonia.Animation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class StateTransiston : Transition<string>
    {
        public override IObservable<string> DoTransition(IObservable<double> progress, string oldValue, string newValue)
        {
            throw new NotImplementedException();
        }
    }
}
