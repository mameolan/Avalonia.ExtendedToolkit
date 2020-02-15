using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuImageItem: HamburgerMenuItem
    {
        public IBitmap Thumbnail
        {
            get { return (IBitmap)GetValue(ThumbnailProperty); }
            set { SetValue(ThumbnailProperty, value); }
        }

        public static readonly AvaloniaProperty<IBitmap> ThumbnailProperty =
            AvaloniaProperty.Register<HamburgerMenuImageItem, IBitmap>(nameof(Thumbnail));
    }
}