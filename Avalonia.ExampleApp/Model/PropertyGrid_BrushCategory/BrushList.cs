using System.Collections.ObjectModel;

namespace Avalonia.ExampleApp.Model
{
    /// <summary>
    /// Sample dataset for brushes.
    /// You should really use a custom brush editor (e.g. from Telerik) but it all depends on what your aims are, of course.
    /// </summary>
    public class BrushList : ObservableCollection<string>
    {
        public BrushList()
        {
            Add("#FFFFFFFF");
            Add("#FF000000");
            Add("#FF142233");
            Add("#FFABEF14");
        }
    }
}
