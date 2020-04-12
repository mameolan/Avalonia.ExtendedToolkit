using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Extensions;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
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
                return item;// base.CreateContainer(item);
            }
            else if(element is GridEntry)
            {
                //var item = element as GridEntry;

                //GridEntryContainer gridEntryContainer = null;

                //var pg= item.FindVisualParent<PropertyGrid>();

                //var itemsPresenter = pg.FindVisualChild<ItemsPresenter>();



                //var test = LogicalTree.LogicalExtensions.GetSelfAndLogicalAncestors(itemsPresenter);
                //    //.
                //    //FirstOrDefault(x=> x.GetType()==typeof(CategoryContainer));//itemsPresenter.FindChildren<CategoryContainer>(true);

                //switch (item.GetType().Name)
                //{
                //    case nameof(CategoryItem):
                //        //gridEntryContainer= 
                //    break;
                //    case nameof(PropertyItem):
                //        gridEntryContainer = item.TryFindParent<PropertyContainer>();
                //        break;

                //    default:
                //        break;
                //}

                

                //if (gridEntryContainer != null)
                //{
                //    //parent.DataContext = item;
                //}
                //return this.CreateContainer(parent);
                
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
