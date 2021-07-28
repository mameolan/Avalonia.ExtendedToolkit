using System.Collections.ObjectModel;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Model
{
    public class FontList : ObservableCollection<FontFamily>
    {
        public FontList()
        {
            foreach (var item in FontFamily.Default.FamilyNames)
            {
                this.Add(item);
            }
        }
    }
}
