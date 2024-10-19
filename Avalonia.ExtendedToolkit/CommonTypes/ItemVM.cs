using Avalonia.Media;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit
{
    public class ItemVM : ReactiveObject
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                this.RaiseAndSetIfChanged(ref _text, value);
            }
        }

        private IImage _image;

        public IImage Image
        {
            get { return _image; }
            set
            {
                this.RaiseAndSetIfChanged(ref _image, value);
            }
        }

        private object _data;

        public object Data
        {
            get { return _data; }
            set
            {
                this.RaiseAndSetIfChanged(ref _data, value);
            }
        }
    }
}
