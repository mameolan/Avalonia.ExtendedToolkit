using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Specifies a range.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class NumberRangeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public double Minimum { get; private set; }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public double Maximum { get; private set; }

        /// <summary>
        /// Gets or sets the tick.
        /// </summary>
        /// <value>The tick.</value>
        public double Tick { get; private set; }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        public double Precision { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberRangeAttribute"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="tick">The tick.</param>
        public NumberRangeAttribute(double minimum, double maximum, double tick)
          : this(minimum, maximum, tick, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberRangeAttribute"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="tick">The tick.</param>
        /// <param name="precision">The precision.</param>
        public NumberRangeAttribute(double minimum, double maximum, double tick, double precision)
        {
            Minimum = minimum;
            Maximum = maximum;
            Tick = tick;
            Precision = precision;
        }
    }
}
