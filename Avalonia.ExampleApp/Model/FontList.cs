using System.Collections.ObjectModel;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Model
{
    public class FontList : ObservableCollection<FontFamily>
    {
        public FontList()
        {
            FontFamilyExtensions.InstalledFontFamilies.ForEach(x=> this.Add(x));
        }
    }
}
