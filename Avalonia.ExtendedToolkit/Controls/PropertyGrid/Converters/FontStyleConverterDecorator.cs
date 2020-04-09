using System.ComponentModel;
using Avalonia.Controlz.Font;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    /// <summary>
    /// Extended <see cref="FontStyleConverter"/> that provides standard values collection.
    /// </summary>
    public sealed class FontStyleConverterDecorator : FontConverterDecorator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontStyleConverterDecorator"/> class.
        /// </summary>
        public FontStyleConverterDecorator() : base(new FontStyleConverter()) { }

        /// <summary>
        /// Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
        /// </returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
              new[]
              {
          FontStyle.Italic,
          FontStyle.Normal,
          FontStyle.Oblique
              });
        }
    }
}
