using System;
using Avalonia.Controls;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    public class CategoryEditorContentPresenter : ContentControl
    {
        public Type StyleKey => typeof(CategoryEditorContentPresenter);

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditorContentPresenter"/> class.
        /// </summary>
        public CategoryEditorContentPresenter()
        {
            //string parentContainerProperty = nameof(GridEntryContainer.ParentContainerProperty)
            //                                        .Replace("Property", string.Empty);
            //string entryProperty = nameof(GridEntryContainer.EntryProperty).Replace("Property", string.Empty);

            //var contentBinding = new Binding
            //{
            //    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
            //    Path=$"{parentContainerProperty}.{entryProperty}"
            //    //Path = new PropertyPath("(0).(1)", new[] { GridEntryContainer.ParentContainerProperty, GridEntryContainer.EntryProperty })
            //};

            //var contentTemplateBinding = new Binding
            //{
            //    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
            //    Path=$"{parentContainerProperty}.EditorTemplate"
            //    //Path = new PropertyPath("(0).EditorTemplate", new[] { GridEntryContainer.ParentContainerProperty })
            //};

            //this.Bind(ContentProperty, contentBinding);
            //this.Bind(ContentTemplateProperty, contentTemplateBinding);
        }
    }
}
