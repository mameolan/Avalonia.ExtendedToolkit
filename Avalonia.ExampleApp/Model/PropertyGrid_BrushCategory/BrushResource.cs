using Avalonia.Media;
using ReactiveUI;

namespace Avalonia.ExampleApp.Model
{
    public class BrushResource : ReactiveObject
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set { this.RaiseAndSetIfChanged(ref _key, value); }
        }

        private SolidColorBrush _brush;

        public SolidColorBrush Brush
        {
            get { return _brush; }
            set { this.RaiseAndSetIfChanged(ref _brush, value); }
        }
    }
}