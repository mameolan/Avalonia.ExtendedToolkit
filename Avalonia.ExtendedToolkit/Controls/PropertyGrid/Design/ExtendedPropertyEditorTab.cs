using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Special Tab used to contain Extended Editors. Used in Tabbed Layout.
    /// </summary>
    public class ExtendedPropertyEditorTab : TabbedLayoutItem
    {
        public Type StyleKey => typeof(ExtendedPropertyEditorTab);

        //private readonly ResourceLocator _resourceLocator = new ResourceLocator();

        /// <summary>
        /// Gets or sets the property an extended editor is bound to.
        /// </summary>
        /// <value>The property.</value>
        public PropertyItem Property { get; private set; }

        /// <summary>
        /// Initializes the <see cref="ExtendedPropertyEditorTab"/> class.
        /// </summary>
        static ExtendedPropertyEditorTab()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPropertyEditorTab"/> class.
        /// </summary>
        public ExtendedPropertyEditorTab()
        {
            CanClose = true;
            VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPropertyEditorTab"/> class.
        /// </summary>
        /// <param name="property">The property to display extended editor for.</param>
        public ExtendedPropertyEditorTab(PropertyItem property)
          : this()
        {
            if (property == null)
                throw new ArgumentNullException("property");

            Property = property;
            Header = property.Name;
            Content = CreateContent(property);
        }

        /// <summary>
        /// Creates the content with extended editor.
        /// </summary>
        /// <param name="propertyItem">The property item.</param>
        /// <returns>ContentControl to place into Tabbed Layout tab.</returns>
        protected virtual object CreateContent(PropertyItem propertyItem)
        {
            Editor editor = propertyItem.Editor;

            if (editor == null || editor.ExtendedTemplate == null)
                throw new ArgumentException("Property Editor does not support Extended templates!");

            if (editor.ExtendedTemplate == null)
                return null;

            var content = new ContentControl
            {
                VerticalContentAlignment = VerticalAlignment.Stretch,
                ContentTemplate = GetDataTemplate(editor.ExtendedTemplate),
                Content = propertyItem.PropertyValue
            };

            return content;
        }

        private DataTemplate GetDataTemplate(object template)
        {
            if (template == null)
                return null;

            var dataTemplate = template as DataTemplate;
            if (dataTemplate != null)
                return dataTemplate;

            return null;
#warning todo 
            //var resourceKey = template as ComponentResourceKey;
            //if (resourceKey == null)
            //    return null;

            //return _resourceLocator.GetResource(resourceKey) as DataTemplate;
        }
    }
}
