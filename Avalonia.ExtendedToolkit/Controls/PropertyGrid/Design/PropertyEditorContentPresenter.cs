using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls.Presenters;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Defines a content presenter control for a Property editor.
    /// </summary>
    public sealed class PropertyEditorContentPresenter : ContentPresenter
    {
        public Type StyleKey => typeof(PropertyEditorContentPresenter);

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyEditorContentPresenter"/> class.
        /// </summary>
        public PropertyEditorContentPresenter()
        {
            var contentBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
#warning todo path
                //Path = new PropertyPath("(0).(1).PropertyValue", new[] { GridEntryContainer.ParentContainerProperty, GridEntryContainer.EntryProperty })
            };

            var contentTemplateBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
#warning todo path
                //Path = new PropertyPath("(0).EditorTemplate", new[] { GridEntryContainer.ParentContainerProperty })
            };

            //Bind(ContentProperty,contentBinding);
            //Bind(ContentTemplateProperty, contentTemplateBinding);
        }
    }
}
