using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Controls the Browsable state of the category with corresponding properties.
    /// Supports the "*" (All) wildcard determining whether all the categories within the given class should be Browsable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class BrowsableCategoryAttribute : Attribute
    {
        /// <summary>
        /// Determines a wildcard for all categories to be affected.
        /// </summary>
        public const string All = "*";

        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        /// <value>The name of the category.</value>
        public string CategoryName { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether category is browsable.
        /// </summary>
        /// <value><c>true</c> if category should be displayed at run time; otherwise, <c>false</c>.</value>
        public bool Browsable { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsableCategoryAttribute"/> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="browsable">if set to <c>true</c> the category is browsable.</param>
        public BrowsableCategoryAttribute(string categoryName, bool browsable)
        {
            CategoryName = string.IsNullOrEmpty(categoryName) ? All : categoryName;
            Browsable = browsable;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsableCategoryAttribute"/> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        public BrowsableCategoryAttribute(string categoryName) : this(categoryName, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsableCategoryAttribute"/> class.
        /// </summary>
        /// <param name="browsable">if set to <c>true</c> all categories are browsable; otherwise hidden</param>
        public BrowsableCategoryAttribute(bool browsable) : this(All, browsable) { }
    }
}
