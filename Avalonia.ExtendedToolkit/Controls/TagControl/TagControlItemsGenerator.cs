using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// item containergeneratir for tagitems
    /// </summary>
    public class TagControlItemsGenerator : ItemContainerGenerator<TagItem>
    {
        private TagControl _owner;

        public TagControlItemsGenerator(TagControl owner,
                                        AvaloniaProperty contentProperty,
                                        AvaloniaProperty contentTemplateProperty)
                                        : base(owner, contentProperty, contentTemplateProperty)
        {
            _owner = owner;
        }

        protected override IControl CreateContainer(object item)
        {
            var container = item as TagItem;
            if (container != null)
            {
                return container;
            }

            container = new TagItem();
            container.Margin = _owner.TagMargin;
            container.DataContext = item;
            Binding binding = new Binding();
            binding.Source = item;
            container.Bind(TagItem.TextProperty, binding, BindingPriority.LocalValue);
            container.Closed += _owner.OnTagControlClosed;
            container.Selected += _owner.OnTagControlSelected;
            return container;
        }
    }
}