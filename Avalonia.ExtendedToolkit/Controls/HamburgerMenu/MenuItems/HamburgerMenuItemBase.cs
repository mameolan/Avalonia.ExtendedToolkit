namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuItemBase : StyledElement
    {
        public object Tag
        {
            get { return (object)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        public static readonly AvaloniaProperty<object> TagProperty =
            AvaloniaProperty.Register<HamburgerMenuItemBase, object>(nameof(Tag));
    }
}