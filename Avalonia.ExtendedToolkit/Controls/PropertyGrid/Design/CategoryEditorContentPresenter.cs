using System;
using System.Collections.Generic;
using System.Text;
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
#warning todo
            var contentBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                //Path = new PropertyPath("(0).(1)", new[] { GridEntryContainer.ParentContainerProperty, GridEntryContainer.EntryProperty })
            };

            var contentTemplateBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                //Path = new PropertyPath("(0).EditorTemplate", new[] { GridEntryContainer.ParentContainerProperty })
            };

            //Bind(ContentProperty, contentBinding);
            //Bind(ContentTemplateProperty, contentTemplateBinding);
        }
    }
}
