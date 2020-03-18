using System;

namespace Avalonia.ExtendedToolkit
{
    public class OnSkinChangedEventArgs : EventArgs
    {
        public OnSkinChangedEventArgs(Skin skin)
        {
            Skin = skin;
        }

        public Skin Skin { get; }
    }
}