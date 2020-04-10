using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Defines a basement for GridEntry UI layouts (panels, lists, etc)
    /// </summary>
    /// <typeparam name="T">The type of elements in the control.</typeparam>
    public abstract class GridEntryLayout<T> : ItemsControl where T : GridEntryContainer, new()
    {
        /// <summary>
        /// The default value for the <see cref="ItemsControl.ItemsPanel"/> property.
        /// </summary>
        private static readonly FuncTemplate<IPanel> DefaultPanel =
            new FuncTemplate<IPanel>(() => new VirtualizingStackPanel());

        static GridEntryLayout()
        {
            ItemsPanelProperty.OverrideDefaultValue<GridEntryLayout<T>>(DefaultPanel);
        }

        public GridEntryLayout()
        {
       
        }

        //protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        //{
        //    foreach(var item in e.Containers)
        //    {
        //        var container = item.ContainerControl;
        //        if (container is T)
        //        {
        //            container.DataContext = item;
        //            container.Bind(GridEntryContainer.EntryProperty, new Binding());
        //        }
        //    }



        //    base.OnContainersMaterialized(e);
        //}


        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            var result = new GridEntryLayoutContainer<T>(this);
            return result;
        }



#warning todo
        ///// <summary>
        ///// Creates or identifies the element that is used to display the given item.
        ///// </summary>
        ///// <returns>
        ///// The element that is used to display the given item.
        ///// </returns>
        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    return new T();
        //}

        ///// <summary>
        ///// Prepares the specified element to display the specified item.
        ///// </summary>
        ///// <param name="element">Element used to display the specified item.</param>
        ///// <param name="item">Specified item.</param>
        //protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        //{
        //    var container = element as T;
        //    if (container != null)
        //    {
        //        container.DataContext = item;
        //        container.SetBinding(GridEntryContainer.EntryProperty, new Binding());
        //    }
        //    base.PrepareContainerForItemOverride(element, item);
        //}
    }
}
