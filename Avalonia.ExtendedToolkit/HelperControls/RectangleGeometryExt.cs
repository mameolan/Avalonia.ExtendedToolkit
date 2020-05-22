using Avalonia.Media;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// just a <see cref="RectangleGeometry"/> with name
    /// should be removed if avalonia supports name
    /// </summary>
    public class RectangleGeometryExt: RectangleGeometry
    {
        /// <summary>
        /// get/sets Name
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// <see cref="Name"/>
        /// </summary>
        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<RectangleGeometryExt, string>(nameof(Name));
    }
}
