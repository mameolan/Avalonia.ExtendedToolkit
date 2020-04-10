using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    internal class GridEntryLayoutContainer<T> : ItemContainerGenerator<T> where T : GridEntryContainer, new()
    {
        public GridEntryLayoutContainer(GridEntryLayout<T> owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
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
                return base.CreateContainer(item);
            }
            else if(element is GridEntry)
            {
                var item = element as GridEntry;
                item.DataContext = Owner.DataContext;
                return base.CreateContainer(item);
            }
            else
            {

            }
            //else if(element is CategoryItem)
            //{
            //    var item = element as CategoryItem;
            //    item.DataContext = Owner.DataContext;
            //    item.Bind(GridEntryContainer.EntryProperty, new Binding());
            //    return base.CreateContainer(item);
            //}


            return base.CreateContainer(element);
        }


    }
}
