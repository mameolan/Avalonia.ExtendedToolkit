using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils;
using Avalonia.Markup.Xaml.Templates;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Specifies a UI container for <see cref="GridEntry"/>
    /// </summary>
    public abstract class GridEntryContainer : ContentControl
    {
        public Type StyleKey => typeof(GridEntryContainer);

        private ResourceLocator _resourceLocator = new ResourceLocator();
        /// <summary>
        /// Gets or sets the resource locator.
        /// </summary>
        /// <value>The resource locator.</value>
        protected ResourceLocator ResourceLocator
        {
            get { return _resourceLocator; }
            set { _resourceLocator = value; }
        }



        public static readonly AttachedProperty<GridEntryContainer> ParentContainerProperty =
            AvaloniaProperty.RegisterAttached<GridEntryContainer, Control, GridEntryContainer>("ParentContainer");

        public static GridEntryContainer GetParentContainer(Control element)
        {
            return element.GetValue(ParentContainerProperty);
        }

        public static void SetParentContainer(Control element, GridEntryContainer value)
        {
            element.SetValue(ParentContainerProperty, value);
        }



        public GridEntry Entry
        {
            get { return (GridEntry)GetValue(EntryProperty); }
            set { SetValue(EntryProperty, value); }
        }


        public static readonly StyledProperty<GridEntry> EntryProperty =
            AvaloniaProperty.Register<GridEntryContainer, GridEntry>(nameof(Entry));

        /// <summary>
        /// Gets the editor template to present contained entry.
        /// </summary>
        /// <value>The editor template to present contained entry.</value>
        public DataTemplate EditorTemplate
        {
            get { return FindEditorTemplate(); }
        }

        /// <summary>
        /// Finds the editor template.
        /// </summary>
        /// <returns>DataTemplate the Editor should be applied.</returns>
        protected virtual DataTemplate FindEditorTemplate()
        {
            if (Entry == null)
                return null;

            var editor = Entry.Editor;

            if (editor == null)
                return null;

            var template = editor.InlineTemplate as DataTemplate;
            if (template != null)
                return template;

            return ResourceLocator.GetResource(editor.InlineTemplate) as DataTemplate;
        }




    }
}
