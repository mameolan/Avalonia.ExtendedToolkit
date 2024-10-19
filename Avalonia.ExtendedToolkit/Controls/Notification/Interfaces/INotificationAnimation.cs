using Avalonia.Animation;
using Avalonia.Controls;

//ported from https://github.com/Enterwell/Wpf.Notifications

namespace Avalonia.ExtendedToolkit.Controls
{
    public interface INotificationAnimation
    {
        /// <summary>
        /// Gets or sets whether the item animates in and out.
        /// </summary>
        bool Animates { get; set; }

        /// <summary>
        /// Gets or sets the animation in duration (in seconds).
        /// </summary>
        double AnimationInDuration { get; set; }

        /// <summary>
        /// Animtion which is used if an item is removed
        /// </summary>
        Animation.Animation AnimationIn { get; set; }

        /// <summary>
        /// Gets or sets the animation out duration (in seconds).
        /// </summary>
        double AnimationOutDuration { get; set; }

        /// <summary>
        /// Animtion which is used if an item is added
        /// </summary>
        Animation.Animation AnimationOut { get; set; }

        /// <summary>
        /// Gets the animatable IControl.
        /// Typically this is the whole Control object so that the entire
        /// item can be animated.
        /// </summary>
        Control AnimatableElement { get; }
    }
}
