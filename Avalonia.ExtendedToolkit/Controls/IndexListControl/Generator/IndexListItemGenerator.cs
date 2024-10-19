//using Avalonia.Controls;
//using Avalonia.Controls.Generators;
//using Avalonia.Controls.Templates;
//using Avalonia.Data;
//using System.Collections.Generic;
//////.orig
//namespace Avalonia.ExtendedToolkit.Controls
//{
//    /// <summary>
//    /// generator for indexlist items
//    /// partial taken from <see cref="TreeItemContainerGenerator{T}"/>
//    /// </summary>
//    public class IndexListItemGenerator : ItemContainerGenerator<IndexListHeaderItem>

//    {
//        /// <summary>
//        /// Gets the item container's Items property.
//        /// </summary>
//        protected AvaloniaProperty ItemsProperty { get; }

//        /// <summary>
//        /// sets the itemsproperty parameter
//        /// </summary>
//        /// <param name="owner"></param>
//        /// <param name="contentProperty"></param>
//        /// <param name="contentTemplateProperty"></param>
//        /// <param name="itemsProperty"></param>
//        public IndexListItemGenerator(Control owner,
//            AvaloniaProperty contentProperty,
//            AvaloniaProperty contentTemplateProperty,
//            AvaloniaProperty itemsProperty)
//            : base(owner, contentProperty, contentTemplateProperty)
//        {
//            ItemsProperty = itemsProperty;
//        }

//        /// <summary>
//        /// tries to create a container from this item
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        protected override Control CreateContainer(object item)
//        {
//            var container = item as IndexListHeaderItem;
//            if (item == null)
//            {
//                return null;
//            }
//            else if (container != null)
//            {
//                return container;
//            }
//            else
//            {
//                IndexList owner = Owner as IndexList;

//                var template = GetTreeDataTemplate(item, ItemTemplate);
//                var result = new IndexListHeaderItem();

//                result.SetValue(ContentProperty, template.Build(item), BindingPriority.Style);
//                Binding binding = new Binding();
//                binding.Source = owner;
//                binding.Path = nameof(owner.ShowEmptyItems);
//                result.Bind(IndexListHeaderItem.ShowEmptyItemsProperty, binding, BindingPriority.Style);

//                var itemsSelector = template.ItemsSelector(item);

//                if (itemsSelector != null)
//                {
//                    BindingOperations.Apply(result, ItemsProperty, itemsSelector, null);
//                }

//                if ((item is Control) == false)
//                {
//                    result.DataContext = item;
//                }

//                return result;
//            }
//        }

//        private class WrapperTreeDataTemplate : ITreeDataTemplate
//        {
//            private readonly IDataTemplate _inner;

//            public WrapperTreeDataTemplate(IDataTemplate inner) => _inner = inner;

//            public Control Build(object param) => _inner.Build(param);

//            //public bool SupportsRecycling => _inner.SupportsRecycling;

//            public bool Match(object data) => _inner.Match(data);

//            public InstancedBinding ItemsSelector(object item) => null;
//        }

//        private ITreeDataTemplate GetTreeDataTemplate(object item, IDataTemplate primary)
//        {
//            var template = Owner.FindDataTemplate(item, primary) ?? FuncDataTemplate.Default;
//            var treeTemplate = template as ITreeDataTemplate ?? new WrapperTreeDataTemplate(template);
//            return treeTemplate;
//        }
//    }
//}
