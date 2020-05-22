using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies the order of category.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public sealed class CategoryOrderAttribute : Attribute
    {
        /// <summary>
        /// Gets the category name.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the category order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryOrderAttribute"/> class.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="order"></param>
        public CategoryOrderAttribute(string category, int order)
        {
            Category = category;
            Order = order;
        }
    }
}
