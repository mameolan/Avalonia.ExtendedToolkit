using System;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Special Tab used to contain Extended Editors. Used in Tabbed Layout.
    /// </summary>
    public class ExtendedPropertyEditorTab : TabbedLayoutItem
    {
        public new Type StyleKey => typeof(ExtendedPropertyEditorTab);

        private readonly ResourceLocator _resourceLocator = new ResourceLocator();

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
                throw new ArgumentNullException(nameof(property));

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
                Template = GetControlTemplate(editor.ExtendedTemplate),
            };

            content.SetValue(ContentControl.DataContextProperty, propertyItem);

            //somehow binding destroys all editors (?)

            //var binding = new Binding()
            //{
            //    Source = propertyItem,
            //    Mode = BindingMode.OneWay
            //};
            //content.Bind(DataContextProperty, binding);

            return content;
        }

        /// <summary>
        /// returns a controlltemplate or null
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private ControlTemplate GetControlTemplate(object template)
        {
            if (template == null)
                return null;

            var controlTemplate = template as ControlTemplate;
            if (controlTemplate != null)
                return controlTemplate;

            //return null;

            var resourceKey = template as string;
            if (resourceKey == null)
                return null;

            //try find the resources from the application
            return _resourceLocator.GetResource(resourceKey) as ControlTemplate;
        }
    }
}
