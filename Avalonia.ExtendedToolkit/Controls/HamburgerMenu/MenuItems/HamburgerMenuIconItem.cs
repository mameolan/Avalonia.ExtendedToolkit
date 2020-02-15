namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuIconItem : HamburgerMenuItem
    {
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly StyledProperty<object> IconProperty =
            AvaloniaProperty.Register<HamburgerMenuIconItem, object>(nameof(Icon));
    }
}