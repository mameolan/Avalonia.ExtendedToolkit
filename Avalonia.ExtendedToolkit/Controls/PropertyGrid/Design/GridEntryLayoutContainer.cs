using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Data;

//namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
//{
//    //
//    // ported from https://github.com/DenisVuyka/WPG
//    //

//    /// <summary>
//    /// GridEntryLayoutContainer T is GridEntryContainer
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    internal class GridEntryLayoutContainer<T> : ItemContainerGenerator<T> where T : GridEntryContainer, new()
//    {
//        public GridEntryLayoutContainer(GridEntryLayout<T> owner)
//            : base(owner, GridEntryContainer.ContentProperty, GridEntryLayout<T>.ItemTemplateProperty)
//        {
//            Owner = owner;
//        }

//        public new GridEntryLayout<T> Owner { get; }

//        /// <summary>
//        /// assigns the datacontext from the owner
//        /// </summary>
//        /// <param name="element"></param>
//        /// <returns></returns>
//        protected override Control CreateContainer(object element)
//        {
//            if (element is GridEntryContainer)
//            {
//                var item = element as GridEntryContainer;

//                item.DataContext = Owner.DataContext;
//                item.Bind(GridEntryContainer.EntryProperty, new Binding());
//                return item;
//            }
//            return base.CreateContainer(element);
//        }
//    }
//}
