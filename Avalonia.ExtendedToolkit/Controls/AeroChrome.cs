using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    public class AeroChrome: ContentControl
    {
        public Type StyleKey => typeof(AeroChrome);

        public bool RenderPressed
        {
            get { return (bool)GetValue(RenderPressedProperty); }
            set { SetValue(RenderPressedProperty, value); }
        }

        public static readonly StyledProperty<bool> RenderPressedProperty =
            AvaloniaProperty.Register<AeroChrome, bool>(nameof(RenderPressed));

        public bool RenderMouseOver
        {
            get { return (bool)GetValue(RenderMouseOverProperty); }
            set { SetValue(RenderMouseOverProperty, value); }
        }

        public static readonly StyledProperty<bool> RenderMouseOverProperty =
            AvaloniaProperty.Register<AeroChrome, bool>(nameof(RenderMouseOver));

        public IBrush MouseOverBackground
        {
            get { return (IBrush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MouseOverBackgroundProperty =
            AvaloniaProperty.Register<AeroChrome, IBrush>(nameof(MouseOverBackground));

        public IBrush MousePressedBackground
        {
            get { return (IBrush)GetValue(MousePressedBackgroundProperty); }
            set { SetValue(MousePressedBackgroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MousePressedBackgroundProperty =
            AvaloniaProperty.Register<AeroChrome, IBrush>(nameof(MousePressedBackground));
    }
}
