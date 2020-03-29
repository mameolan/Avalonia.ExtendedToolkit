using System;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// extended button with image properties
    /// </summary>
    public class ImageButton : Button
    {
        public Type StyleKey => typeof(ImageButton);

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly StyledProperty<double> ImageWidthProperty =
            AvaloniaProperty.Register<ImageButton, double>(nameof(ImageWidth));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly StyledProperty<double> ImageHeightProperty =
            AvaloniaProperty.Register<ImageButton, double>(nameof(ImageHeight));

        public IBitmap Image
        {
            get { return (IBitmap)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly StyledProperty<IBitmap> ImageProperty =
            AvaloniaProperty.Register<ImageButton, IBitmap>(nameof(Image));
    }
}
