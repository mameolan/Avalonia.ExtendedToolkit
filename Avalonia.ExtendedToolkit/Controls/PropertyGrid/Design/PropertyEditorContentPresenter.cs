using System;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Defines a content presenter control for a Property editor.
    /// </summary>
    public sealed class PropertyEditorContentPresenter : ContentControl
    {
        public Type StyleKey => typeof(PropertyEditorContentPresenter);

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyEditorContentPresenter"/> class.
        /// </summary>
        public PropertyEditorContentPresenter()
        {
            string parentContainerProperty = nameof(GridEntryContainer.ParentContainerProperty)
                                                    .Replace("Property", string.Empty);
            string entryProperty = nameof(GridEntryContainer.EntryProperty).Replace("Property", string.Empty);

            var contentBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                Path = $"{parentContainerProperty}.{entryProperty}"
                //Path = new PropertyPath("(0).(1).PropertyValue", new[] { GridEntryContainer.ParentContainerProperty, GridEntryContainer.EntryProperty })
            };

            var contentTemplateBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                Path = $"{parentContainerProperty}..EditorTemplate"
                //Path = new PropertyPath("(0).EditorTemplate", new[] { GridEntryContainer.ParentContainerProperty })
            };

            this.Bind(ContentProperty,contentBinding);
            this.Bind(ContentTemplateProperty, contentTemplateBinding);
        }
    }
}
