using System;
using System.Diagnostics;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies the default category editor.
    /// </summary>
    [DebuggerDisplay("[CategoryEditor] CategoryName: {CategoryName} DeclaringType: {DeclaringType}")]
    public class CategoryEditor : Editor
    {
        public new Type StyleKey => typeof(CategoryEditor);

        public Type DeclaringType
        {
            get { return (Type)GetValue(DeclaringTypeProperty); }
            set { SetValue(DeclaringTypeProperty, value); }
        }

        public static readonly StyledProperty<Type> DeclaringTypeProperty =
            AvaloniaProperty.Register<CategoryEditor, Type>(nameof(DeclaringType));

        public string CategoryName
        {
            get { return (string)GetValue(CategoryNameProperty); }
            set { SetValue(CategoryNameProperty, value); }
        }

        public static readonly StyledProperty<string> CategoryNameProperty =
            AvaloniaProperty.Register<CategoryEditor, string>(nameof(CategoryName));

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditor"/> class.
        /// </summary>
        public CategoryEditor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditor"/> class.
        /// </summary>
        /// <param name="declaringType">Declaring type.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="inlineTemplate">The inline template.</param>
        public CategoryEditor(Type declaringType, string categoryName, object inlineTemplate)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentNullException("categoryName");

            DeclaringType = declaringType;
            CategoryName = categoryName;

            InlineTemplate= GetEditorTemplate(inlineTemplate);
        }
    }
}
