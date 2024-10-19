namespace Avalonia.ExtendedToolkit
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// enumeration for the different transition types
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// Use the VisualState DefaultTransition
        /// </summary>
        Default,

        /// <summary>
        /// Use the VisualState Normal
        /// </summary>
        Normal,

        /// <summary>
        /// Use the VisualState UpTransition
        /// </summary>
        Up,

        /// <summary>
        /// Use the VisualState DownTransition
        /// </summary>
        Down,

        /// <summary>
        /// Use the VisualState RightTransition
        /// </summary>
        Right,

        /// <summary>
        /// Use the VisualState RightReplaceTransition
        /// </summary>
        RightReplace,

        /// <summary>
        /// Use the VisualState LeftTransition
        /// </summary>
        Left,

        /// <summary>
        /// Use the VisualState LeftReplaceTransition
        /// </summary>
        LeftReplace,

        /// <summary>
        /// Use a custom VisualState, the name must be set using CustomVisualStatesName property
        /// </summary>
        Custom
    }
}
