namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Contains a set of editor keys.
    /// </summary>
    public static class EditorKeys
    {
        private static readonly string _NamedColorEditorKey = "NamedColorEditor";
        private static readonly string _PasswordEditorKey = "PasswordEditor";
        private static readonly string _DefaultEditorKey = "DefaultEditor";
        private static readonly string _BooleanEditorKey = "BooleanEditor";
        private static readonly string _DoubleEditorKey = "DoubleEditor";
        private static readonly string _EnumEditorKey = "EnumEditor";
        private static readonly string _SliderEditorKey = "SliderEditor";
        private static readonly string _FontFamilyEditorKey = "FontFamilyEditor";
        private static readonly string _BrushEditorKey = "BrushEditor";
        private static readonly string _DefaultCategoryEditorKey = "DefaultCategoryEditor";
        private static readonly string _ComplexPropertyEditorKey = "ComplexPropertyEditor";

        /// <summary>
        /// Gets the NamedColor editor key.
        /// </summary>
        /// <value>The named color editor key.</value>
        public static string NamedColorEditorKey { get { return _NamedColorEditorKey; } }

        /// <summary>
        /// Gets the password editor key.
        /// </summary>
        /// <value>The password editor key.</value>
        public static string PasswordEditorKey { get { return _PasswordEditorKey; } }

        /// <summary>
        /// Gets the default editor key.
        /// </summary>
        /// <value>The default editor key.</value>
        public static string DefaultEditorKey { get { return _DefaultEditorKey; } }

        /// <summary>
        /// Gets the boolean editor key.
        /// </summary>
        /// <value>The boolean editor key.</value>
        public static string BooleanEditorKey { get { return _BooleanEditorKey; } }

        /// <summary>
        /// Gets the double editor key.
        /// </summary>
        /// <value>The double editor key.</value>
        public static string DoubleEditorKey { get { return _DoubleEditorKey; } }

        /// <summary>
        /// Gets the enum editor key.
        /// </summary>
        /// <value>The enum editor key.</value>
        public static string EnumEditorKey { get { return _EnumEditorKey; } }

        /// <summary>
        /// Gets the slider editor key.
        /// </summary>
        /// <value>The slider editor key.</value>
        public static string SliderEditorKey { get { return _SliderEditorKey; } }

        /// <summary>
        /// Gets the font family editor key.
        /// </summary>
        /// <value>The font family editor key.</value>
        public static string FontFamilyEditorKey { get { return _FontFamilyEditorKey; } }

        /// <summary>
        /// Gets the brush editor key.
        /// </summary>
        /// <value>The brush editor key.</value>
        public static string BrushEditorKey { get { return _BrushEditorKey; } }

        /// <summary>
        /// Gets the default category editor key.
        /// </summary>
        /// <value>The default category editor key.</value>
        public static string DefaultCategoryEditorKey { get { return _DefaultCategoryEditorKey; } }

        /// <summary>
        /// Gets the default complex property editor key.
        /// </summary>
        public static string ComplexPropertyEditorKey { get { return _ComplexPropertyEditorKey; } }
    }
}
