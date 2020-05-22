using System;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils;
using Avalonia.Markup.Xaml.Templates;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies a UI container for <see cref="GridEntry"/>
    /// </summary>
    public abstract class GridEntryContainer : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
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

        /// <summary>
        /// ParentContainer AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<GridEntryContainer> ParentContainerProperty =
            AvaloniaProperty.RegisterAttached<GridEntryContainer, Control, GridEntryContainer>("ParentContainer");

        /// <summary>
        /// get ParentContainer
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static GridEntryContainer GetParentContainer(Control element)
        {
            return element.GetValue(ParentContainerProperty);
        }

        /// <summary>
        /// set ParentContainer
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetParentContainer(Control element, GridEntryContainer value)
        {
            element.SetValue(ParentContainerProperty, value);
        }

        /// <summary>
        /// get/set gridentry
        /// </summary>
        public GridEntry Entry
        {
            get { return (GridEntry)GetValue(EntryProperty); }
            set { SetValue(EntryProperty, value); }
        }

        /// <summary>
        /// <see cref="Entry"/>
        /// </summary>
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

        /// <summary>
        /// assigns the DataContext as GridEntry to the Entry property
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if(this.DataContext is GridEntry)
            {
                Entry = DataContext as GridEntry;
            }
        }
    }
}
