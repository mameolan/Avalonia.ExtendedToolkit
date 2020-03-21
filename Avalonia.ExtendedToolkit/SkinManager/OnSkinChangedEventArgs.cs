using System;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// event args for the IsSkinChanged event
    /// </summary>
    public class OnSkinChangedEventArgs : EventArgs
    {
        public OnSkinChangedEventArgs(Skin skin)
        {
            Skin = skin;
        }

        public Skin Skin { get; }
    }
}