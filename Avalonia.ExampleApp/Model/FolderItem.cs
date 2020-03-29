using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace Avalonia.ExampleApp.Model
{
    public class FolderItem
    {
        public string Folder { get; set; }
        public IBitmap Image { get; set; }

        public FolderItem()
        {
            Uri uri = new Uri("/Assets/openfolderHS.png", UriKind.Relative);

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            Image= new Bitmap(assets.Open(uri));
        }
    }
}
