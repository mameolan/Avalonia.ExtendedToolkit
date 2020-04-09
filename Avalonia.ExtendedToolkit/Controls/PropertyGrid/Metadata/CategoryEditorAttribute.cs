using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Specifies the editor to use to change the category.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public sealed class CategoryEditorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the editor.
        /// </summary>
        /// <value>The type of the editor.</value>
        public string EditorType { get; private set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>The name of the category.</value>
        public string CategoryName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditorAttribute"/> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="editorType">Type of the editor.</param>
        public CategoryEditorAttribute(string categoryName, string editorType)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentNullException("categoryName");
            if (string.IsNullOrEmpty(editorType))
                throw new ArgumentNullException("editorType");

            EditorType = editorType;
            CategoryName = categoryName.ToUpperInvariant();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditorAttribute"/> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="editorType">Type of the editor.</param>
        public CategoryEditorAttribute(string categoryName, Type editorType)
          : this(categoryName, editorType.AssemblyQualifiedName)
        {
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return EditorType.GetHashCode() ^ CategoryName.GetHashCode();
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An <see cref="T:System.Object"/> to compare with this instance or null.</param>
        /// <returns>
        /// true if <paramref name="obj"/> equals the type and value of this instance; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            var attribute = obj as CategoryEditorAttribute;
            return (attribute != null && attribute.EditorType == EditorType && attribute.CategoryName == CategoryName);
        }
    }
}
