using Avalonia.Media;

namespace Avalonia.ExtendedToolkit
{
    public class TranslateTransformExt: TranslateTransform
    {
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly AvaloniaProperty<string> NameProperty =
            AvaloniaProperty.Register<TranslateTransformExt, string>(nameof(Name));
    }
}