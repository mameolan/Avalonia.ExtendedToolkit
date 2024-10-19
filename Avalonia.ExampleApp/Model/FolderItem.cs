using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalonia.ExampleApp.Model
{
    public class FolderItem
    {
        public string Folder { get; set; }
        public Bitmap Image { get; set; }

        public FolderItem()
        {
            Uri uri = new Uri("/Assets/openfolderHS.png", UriKind.Relative);
            Image= new Bitmap(AssetLoader.Open(uri));
        }
    }
}
