using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// a headered itemscontrol with <see cref="IndexListItem"/>
    /// </summary>
    public class IndexListHeaderItem : HeaderedItemsControl
    {
        /// <summary>
        /// Gets or sets ShowEmptyItems.
        /// </summary>
        public bool ShowEmptyItems
        {
            get { return (bool)GetValue(ShowEmptyItemsProperty); }
            set { SetValue(ShowEmptyItemsProperty, value); }
        }

        /// <summary>
        /// Defines the ShowEmptyItems property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowEmptyItemsProperty =
            //AvaloniaProperty.Register<IndexList, bool>(nameof(ShowEmptyItems));
            IndexList.ShowEmptyItemsProperty.AddOwner<IndexListHeaderItem>();

        private static readonly ITemplate<Panel> DefaultPanel =
            new FuncTemplate<Panel>(() => new StackPanel());

        /// <summary>
        /// overrides some default values
        /// </summary>
        static IndexListHeaderItem()
        {
            FocusableProperty.OverrideDefaultValue<IndexListHeaderItem>(false);
            ItemsPanelProperty.OverrideDefaultValue<IndexListHeaderItem>(DefaultPanel);
        }

        /// <summary>
        /// generates <see cref="IndexListItem"/> subitems
        /// </summary>
        /// <returns></returns>
        //.orig
        //protected override IItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    return new ItemContainerGenerator<IndexListItem>(
        //        this,
        //        IndexListItem.ContentProperty,
        //        IndexListItem.ContentTemplateProperty);
        //}
    }
}
