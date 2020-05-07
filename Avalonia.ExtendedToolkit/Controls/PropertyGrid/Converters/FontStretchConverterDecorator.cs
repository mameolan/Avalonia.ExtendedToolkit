using System;
using System.ComponentModel;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// FontStretchConverter is missing
    /// Extended  that provides standard values collection.
    /// Todo update if avalonia has a front stretch type
    /// </summary>
    public sealed class FontStretchConverterDecorator : FontConverterDecorator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontStretchConverterDecorator"/> class.
        /// </summary>
        public FontStretchConverterDecorator() : base(null/*new FontStretchConverter()*/) { }

        /// <summary>
        /// Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
        /// </returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            throw new NotImplementedException();
          //  return new StandardValuesCollection(
          //    new[]
          //    {
          //FontStretches.Condensed,
          //FontStretches.Expanded,
          //FontStretches.ExtraCondensed,
          //FontStretches.ExtraExpanded,
          //FontStretches.Normal,
          //FontStretches.SemiCondensed,
          //FontStretches.SemiExpanded,
          //FontStretches.UltraCondensed,
          //FontStretches.UltraExpanded
          //    });
        }
    }
}
