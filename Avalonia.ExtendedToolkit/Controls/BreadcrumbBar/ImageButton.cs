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
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(ImageButton);

        /// <summary>
        /// get/sets the image width
        /// </summary>
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        /// <summary>
        /// <see cref="ImageWidth"/>
        /// </summary>
        public static readonly StyledProperty<double> ImageWidthProperty =
            AvaloniaProperty.Register<ImageButton, double>(nameof(ImageWidth));

        /// <summary>
        /// get/sets image height
        /// </summary>
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        /// <summary>
        /// <see cref="ImageHeight"/>
        /// </summary>
        public static readonly StyledProperty<double> ImageHeightProperty =
            AvaloniaProperty.Register<ImageButton, double>(nameof(ImageHeight));

        /// <summary>
        /// get/sets image to display
        /// </summary>
        public Bitmap Image
        {
            get { return (Bitmap)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// <see cref="Image"/>
        /// </summary>
        public static readonly StyledProperty<Bitmap> ImageProperty =
            AvaloniaProperty.Register<ImageButton, Bitmap>(nameof(Image));
    }
}
