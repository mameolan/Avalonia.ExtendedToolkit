using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// PathConversionEventHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PathConversionEventHandler(object sender, PathConversionEventArgs e);

    /// <summary>
    /// RoutedEventArgs to convert the display path to edit path and vice verca.
    /// </summary>
    public class PathConversionEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Gets or sets the display path.
        /// </summary>
        public string DisplayPath { get; set; }

        /// <summary>
        /// Gets or sets the edit path.
        /// </summary>
        public string EditPath { get; set; }

        /// <summary>
        /// Specifies what path property to convert.
        /// </summary>
        public ConversionMode Mode { get; private set; }

        /// <summary>
        /// Gets the root object of the breadcrumb bar.
        /// </summary>
        public object Root { get; private set; }

        /// <summary>
        /// Creates a new PathConversionEventArgs class.
        /// </summary>
        /// <param name="mode">The conversion mode.</param>
        /// <param name="path">The initial values for DisplayPath and EditPath.</param>
        /// <param name="root">The root object.</param>
        /// <param name="routedEvent"></param>
        public PathConversionEventArgs(ConversionMode mode, string path, object root, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Mode = mode;
            DisplayPath = EditPath = path;
            Root = root;
        }
    }
}
