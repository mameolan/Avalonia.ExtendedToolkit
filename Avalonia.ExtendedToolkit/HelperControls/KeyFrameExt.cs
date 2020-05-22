using Avalonia.Animation;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// keyframe with name
    /// </summary>
    public class KeyFrameExt: KeyFrame
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
            AvaloniaProperty.Register<KeyFrameExt, string>(nameof(Name));
    }
}
