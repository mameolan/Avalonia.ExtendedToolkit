using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// aero chrome control
    /// </summary>
    public class AeroChrome: ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(AeroChrome);

        /// <summary>
        /// get /set RenderPressed
        /// </summary>
        public bool RenderPressed
        {
            get { return (bool)GetValue(RenderPressedProperty); }
            set { SetValue(RenderPressedProperty, value); }
        }

        /// <summary>
        /// <see cref="RenderPressed"/>
        /// </summary>
        public static readonly StyledProperty<bool> RenderPressedProperty =
            AvaloniaProperty.Register<AeroChrome, bool>(nameof(RenderPressed));

        /// <summary>
        /// get/sets RenderMouseOver
        /// </summary>
        public bool RenderMouseOver
        {
            get { return (bool)GetValue(RenderMouseOverProperty); }
            set { SetValue(RenderMouseOverProperty, value); }
        }

        /// <summary>
        /// <see cref="RenderMouseOver"/>
        /// </summary>
        public static readonly StyledProperty<bool> RenderMouseOverProperty =
            AvaloniaProperty.Register<AeroChrome, bool>(nameof(RenderMouseOver));

        /// <summary>
        /// get/sets MouseOverBackground
        /// </summary>
        public IBrush MouseOverBackground
        {
            get { return (IBrush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="MouseOverBackground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MouseOverBackgroundProperty =
            AvaloniaProperty.Register<AeroChrome, IBrush>(nameof(MouseOverBackground));

        /// <summary>
        /// get/ set MousePressedBackground
        /// </summary>
        public IBrush MousePressedBackground
        {
            get { return (IBrush)GetValue(MousePressedBackgroundProperty); }
            set { SetValue(MousePressedBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="MousePressedBackground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MousePressedBackgroundProperty =
            AvaloniaProperty.Register<AeroChrome, IBrush>(nameof(MousePressedBackground));
    }
}
