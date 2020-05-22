using System;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Provides basic information for named colors.
    /// </summary>
    public sealed class NamedColor
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name { get; private set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets or sets the brush.
        /// </summary>
        /// <value>The brush.</value>
        public Brush Brush { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedColor"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        public NamedColor(string name, Color color)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            Color = color;
            Brush = (Brush)new SolidColorBrush(color);
        }

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current <see cref="Object"/>.
        /// </returns>
        public override String ToString()
        {
            return Name;
        }
    }
}
