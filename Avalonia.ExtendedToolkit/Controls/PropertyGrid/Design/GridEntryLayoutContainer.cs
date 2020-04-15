using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    internal class GridEntryLayoutContainer<T> : ItemContainerGenerator<T> where T : GridEntryContainer, new()
    {
        public GridEntryLayoutContainer(GridEntryLayout<T> owner)
            : base(owner, GridEntryContainer.ContentProperty, GridEntryLayout<T>.ItemTemplateProperty)
        {
            Owner = owner;
        }

        public new GridEntryLayout<T> Owner { get; }

        protected override IControl CreateContainer(object element)
        {
            if (element is GridEntryContainer)
            {
                var item = element as GridEntryContainer;

                item.DataContext = Owner.DataContext;
                item.Bind(GridEntryContainer.EntryProperty, new Binding());
                return item;
            }
            return base.CreateContainer(element);
        }
    }
}
