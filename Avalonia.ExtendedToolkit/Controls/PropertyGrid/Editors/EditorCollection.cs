using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Collections;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Defines a collection of value Editors (Type, Category and Property editors).
    /// </summary>
    public class EditorCollection : AvaloniaList<Editor>
    {
        private static readonly Dictionary<Type, Editor> Cache = new Dictionary<Type, Editor>
    {
      { typeof(Boolean), new TypeEditor(typeof(Boolean), EditorKeys.BooleanEditorKey) },
      //{ KnownTypes.Wpf.FontStretch, new TypeEditor(KnownTypes.Wpf.FontStretch, EditorKeys.EnumEditorKey) },
      { KnownTypes.Wpf.FontStyle, new TypeEditor(KnownTypes.Wpf.FontStyle, EditorKeys.EnumEditorKey) },
      { KnownTypes.Wpf.FontWeight, new TypeEditor(KnownTypes.Wpf.FontWeight, EditorKeys.EnumEditorKey) },
      { KnownTypes.Wpf.Cursor, new TypeEditor(KnownTypes.Wpf.Cursor, EditorKeys.EnumEditorKey) },
      { KnownTypes.Wpf.FontFamily, new TypeEditor(KnownTypes.Wpf.FontFamily, EditorKeys.FontFamilyEditorKey) },
      { KnownTypes.Wpf.Brush, new TypeEditor(KnownTypes.Wpf.Brush, EditorKeys.BrushEditorKey) },
      { typeof(Enum), new TypeEditor(typeof(Enum), EditorKeys.EnumEditorKey) }
    };

        /// <summary>
        /// Finds the type editor.
        /// </summary>
        /// <param name="editedType">Edited type.</param>
        /// <returns>Editor for Type</returns>
        public TypeEditor FindTypeEditor(Type editedType)
        {
            if (editedType == null)
                throw new ArgumentNullException("editedType");

            return this
              .OfType<TypeEditor>()
              .FirstOrDefault(item => item.EditedType.IsAssignableFrom(editedType));
        }

        /// <summary>
        /// Finds the property editor.
        /// </summary>
        /// <param name="declaringType">Declaring type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Editor for Property</returns>
        public PropertyEditor FindPropertyEditor(Type declaringType, string propertyName)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            return this
              .OfType<PropertyEditor>()
              .Where(item => item.DeclaringType.IsAssignableFrom(declaringType))
              .FirstOrDefault(item => item.PropertyName == propertyName);
        }

        /// <summary>
        /// Finds the category editor.
        /// </summary>
        /// <param name="declaringType">Declaring type.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>Editor for Category</returns>
        public CategoryEditor FindCategoryEditor(Type declaringType, string categoryName)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentNullException("categoryName");

            return this
              .OfType<CategoryEditor>()
              .Where(item => item.DeclaringType.IsAssignableFrom(declaringType))
              .FirstOrDefault(item => item.CategoryName == categoryName);
        }

        /// <summary>
        /// Gets the property editor by attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns>Editor for Property</returns>
        public static Editor GetPropertyEditorByAttributes(AttributeCollection attributes)
        {
            if (attributes == null)
                return null;

            var attribute = attributes[KnownTypes.Attributes.PropertyEditorAttribute] as PropertyEditorAttribute;
            if (attribute == null)
                return null;

            try
            {
                var editorType = Type.GetType(attribute.EditorType);
                if (editorType == null || !KnownTypes.Wpg.Editor.IsAssignableFrom(editorType))
                    return null;
                return (Editor)Activator.CreateInstance(editorType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the category editor by attributes.
        /// </summary>
        /// <param name="declaringType">Type of the declaring.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>Editor for Category</returns>
        public static Editor GetCategoryEditorByAttributes(Type declaringType, string categoryName)
        {
            if (declaringType == null || string.IsNullOrEmpty(categoryName))
                return null;

            string name = categoryName.ToUpperInvariant();

            CategoryEditorAttribute attribute = declaringType
              .GetCustomAttributes(KnownTypes.Attributes.CategoryEditorAttribute, true)
              .OfType<CategoryEditorAttribute>()
              .FirstOrDefault(attr => attr.CategoryName == name);

            if (attribute == null)
                return null;

            try
            {
                Type editorType = Type.GetType(attribute.EditorType);
                if (editorType == null || !KnownTypes.Wpg.Editor.IsAssignableFrom(editorType))
                    return null;
                return (Editor)Activator.CreateInstance(editorType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <param name="categoryItem">The category item.</param>
        /// <returns>Editor for Category</returns>
        public Editor GetEditor(CategoryItem categoryItem)
        {
            if (categoryItem == null)
                throw new ArgumentNullException("categoryItem");

            if (categoryItem.Owner == null)
                return null;

            object declaringObject = ObjectServices.GetUnwrappedObject(categoryItem.Owner.SelectedObject);
            if (declaringObject == null)
                return null;

            Type declaringType = declaringObject.GetType();

            Editor editor = FindCategoryEditor(declaringType, categoryItem.Name);
            if (editor != null)
                return editor;

            editor = GetCategoryEditorByAttributes(declaringType, categoryItem.Name);
            if (editor != null)
                return editor;

            return new CategoryEditor(declaringType, categoryItem.Name, EditorKeys.DefaultCategoryEditorKey);
        }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <param name="propertyItem">The property item.</param>
        /// <returns>Editor for Property</returns>
        public Editor GetEditor(PropertyItem propertyItem)
        {
            if (propertyItem == null)
                throw new ArgumentNullException("propertyItem");

            Editor editor;

            if (propertyItem.Attributes != null)
            {
                editor = GetPropertyEditorByAttributes(propertyItem.Attributes);
                if (editor != null)
                    return editor;
            }

            if (propertyItem.Component != null && !string.IsNullOrEmpty(propertyItem.Name))
            {
                object declaringObject = ObjectServices.GetUnwrappedObject(propertyItem.Owner.SelectedObject);
                editor = FindPropertyEditor(declaringObject.GetType(), propertyItem.Name);
                if (editor != null)
                    return editor;
            }

            if (propertyItem.PropertyValue.HasSubProperties)
                return new TypeEditor(propertyItem.PropertyType, EditorKeys.ComplexPropertyEditorKey);

            bool hasType = propertyItem.PropertyType != null;

            if (hasType)
            {
                editor = FindTypeEditor(propertyItem.PropertyType);
                if (editor != null)
                    return editor;
            }

            if (hasType)
            {
                foreach (var cachedEditor in Cache)
                {
                    if (cachedEditor.Key.IsAssignableFrom(propertyItem.PropertyType))
                        return cachedEditor.Value;
                }

                return new TypeEditor(propertyItem.PropertyType, EditorKeys.DefaultEditorKey);
            }

            return null;
        }
    }
}
