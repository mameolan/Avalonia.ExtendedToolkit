using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// selectable contencontrol
    /// </summary>
    public class IndexListItem : ContentControl, ISelectable
    {
        private Control _header;

        /// <summary>
        /// Defines the <see cref="IsSelected"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
            ListBoxItem.IsSelectedProperty.AddOwner<IndexListItem>();

        /// <summary>
        /// Gets or sets the selection state of the item.
        /// </summary>
        public bool IsSelected
        {
            get { return GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// overrides some default values
        /// </summary>
        static IndexListItem()
        {
            SelectableMixin.Attach<IndexListItem>(IsSelectedProperty);
            FocusableProperty.OverrideDefaultValue<IndexListItem>(true);
            RequestBringIntoViewEvent.AddClassHandler<IndexListItem>((x, e) => x.OnRequestBringIntoView(e));
            DataContextProperty.Changed.AddClassHandler<IndexListItem>((x, e) => OnDataContextChanged(x, e));
        }

        private static void OnDataContextChanged(IndexListItem indexListItem, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is IndexItemModel model)
            {
                Binding binding = new Binding();
                binding.Source = model;
                binding.Path = nameof(model.IsVisible);
                binding.Mode = BindingMode.TwoWay;

                indexListItem.Bind(IsVisibleProperty, binding);
            }
        }

        /// <summary>
        /// tries to bring the content to the view
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRequestBringIntoView(RequestBringIntoViewEventArgs e)
        {
            if (e.TargetObject == this && _header != null)
            {
                var m = _header.TransformToVisual(this);

                if (m.HasValue)
                {
                    var bounds = new Rect(_header.Bounds.Size);
                    var rect = bounds.TransformToAABB(m.Value);
                    e.TargetRect = rect;
                }
            }
        }

        /// <summary>
        /// gets the requirement controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _header = e.NameScope.Find<Control>("PART_Header");
        }
    }
}
