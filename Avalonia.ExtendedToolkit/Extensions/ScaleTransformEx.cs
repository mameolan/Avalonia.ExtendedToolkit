using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ScaleTransformEx: ScaleTransform
    {
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<ScaleTransformEx, string>(nameof(Name));
    }
}