using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors
{
    /// <summary>
    /// Provides value editing service for property value that is of some specific type.
    /// </summary>
    public class TypeEditor : Editor
    {
        public new Type StyleKey => typeof(TypeEditor);

        /// <summary>
        /// Gets or sets the type of the object editor is bound to.
        /// </summary>
        /// <value>The type of the object editor is bound to.</value>
        public Type EditedType
        {
            get { return (Type)GetValue(EditedTypeProperty); }
            set { SetValue(EditedTypeProperty, value); }
        }

        public static readonly StyledProperty<Type> EditedTypeProperty =
            AvaloniaProperty.Register<TypeEditor, Type>(nameof(EditedType));

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeEditor"/> class.
        /// </summary>
        public TypeEditor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeEditor"/> class.
        /// </summary>
        /// <param name="editedType">The type of the object editor is bound to.</param>
        public TypeEditor(Type editedType) : this(editedType, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeEditor"/> class.
        /// </summary>
        /// <param name="editedType">The type of the object editor is bound to.</param>
        /// <param name="inlineTemplate">The inline template for UI presentation. Can be either a DataTemplate or ComponentResourceKey object.</param>
        public TypeEditor(Type editedType, object inlineTemplate) : this(editedType, inlineTemplate, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeEditor"/> class.
        /// </summary>
        /// <param name="editedType">Type of the edited.</param>
        /// <param name="inlineTemplate">The inline template for UI presentation. Can be either a DataTemplate or ComponentResourceKey object.</param>
        /// <param name="extendedTemplate">The extended template for UI presentation. Can be either a DataTemplate or ComponentResourceKey object.</param>
        public TypeEditor(Type editedType, object inlineTemplate, object extendedTemplate)
        {
            if (editedType == null)
                throw new ArgumentNullException("editedType");

            EditedType = editedType;

            InlineTemplate = GetEditorTemplate(inlineTemplate);
            
            ExtendedTemplate = GetEditorTemplate(extendedTemplate);
            
        }
    }
}
