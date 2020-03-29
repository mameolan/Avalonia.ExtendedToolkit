using System.Diagnostics;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// Helper class for displaying color schemes.
    /// </summary>
    [DebuggerDisplay("Name={" + nameof(Name) + "}")]
    public class ColorScheme
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ColorScheme(Theme theme)
            : this(theme.ColorScheme, theme.ShowcaseBrush)
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ColorScheme(string name, IBrush showcaseBrush)
        {
            this.Name = name;
            this.ShowcaseBrush = showcaseBrush;
        }

        /// <summary>
        /// Gets the name for this color scheme.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the showcase brush for this color scheme.
        /// </summary>
        public IBrush ShowcaseBrush { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
