using System;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// event args for the IsSkinChanged event
    /// </summary>
    public class OnSkinChangedEventArgs : EventArgs
    {
        /// <summary>
        /// ctor with skin parameter
        /// </summary>
        /// <param name="skin"></param>
        public OnSkinChangedEventArgs(Skin skin)
        {
            Skin = skin;
        }

        /// <summary>
        /// Changed skin
        /// </summary>
        public Skin Skin { get; }
    }
}
