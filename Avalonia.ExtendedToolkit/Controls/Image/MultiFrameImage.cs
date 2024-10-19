using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// Mutiframe image
    /// </summary>
    public class MultiFrameImage : Image
    {
#warning complete implementation

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(MultiFrameImage);

        /// <summary>
        /// registered sourceproperty changed handler
        /// </summary>
        public MultiFrameImage()
        {
            SourceProperty.Changed.AddClassHandler<MultiFrameImage>((o, e) => OnSourceChanged(o, e));
        }

        /// <summary>
        /// get /sets MultiFrameImageMode
        /// </summary>
        public MultiFrameImageMode MultiFrameImageMode
        {
            get { return (MultiFrameImageMode)GetValue(MultiFrameImageModeProperty); }
            set { SetValue(MultiFrameImageModeProperty, value); }
        }

        /// <summary>
        /// <see cref="MultiFrameImageMode"/>
        /// </summary>
        public static readonly StyledProperty<MultiFrameImageMode> MultiFrameImageModeProperty =
            AvaloniaProperty.Register<MultiFrameImage, MultiFrameImageMode>(nameof(MultiFrameImageMode));

        private readonly List<Bitmap> _frames = new List<Bitmap>();

        private void OnSourceChanged(MultiFrameImage multiFrameImage, AvaloniaPropertyChangedEventArgs e)
        {
            multiFrameImage.UpdateFrameList();
        }

        private void UpdateFrameList()
        {
            _frames.Clear();

            //var bitmapFrame = Source as Bitmap;
            //if (bitmapFrame == null)
            //{
            //    return;
            //}

            //var decoder = bitmapFrame.Decoder;
            //if (decoder == null || decoder.Frames.Count == 0)
            //{
            //    return;
            //}

            //// order all frames by size, take the frame with the highest color depth per size
            //_frames.AddRange(
            //    decoder
            //        .Frames
            //        .GroupBy(f => f.PixelWidth * f.PixelHeight)
            //        .OrderBy(g => g.Key)
            //        .Select(g => g.OrderByDescending(f => f.Format.BitsPerPixel).First())
            //        );
        }

        /// <summary>
        /// draws the image
        /// </summary>
        /// <param name="dc"></param>
        //.orig
        //public override void Render(DrawingContext dc)
        //{
        //    if (_frames.Count == 0)
        //    {
        //        base.Render(dc);
        //        return;
        //    }

        //    switch (MultiFrameImageMode)
        //    {
        //        case MultiFrameImageMode.ScaleDownLargerFrame:
        //            //var minSize = Math.Max(RenderSize.Width, RenderSize.Height);
        //            //var minFrame = _frames.FirstOrDefault(f => f.Width >= minSize && f.Height >= minSize) ?? _frames.Last();
        //            //dc.DrawImage(minFrame, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
        //            break;

        //        case MultiFrameImageMode.NoScaleSmallerFrame:
        //            //var maxSize = Math.Min(RenderSize.Width, RenderSize.Height);
        //            //var maxFrame = _frames.LastOrDefault(f => f.Width <= maxSize && f.Height <= maxSize) ?? _frames.First();
        //            //dc.DrawImage(maxFrame, new Rect((RenderSize.Width - maxFrame.Width) / 2, (RenderSize.Height - maxFrame.Height) / 2, maxFrame.Width, maxFrame.Height));
        //            break;

        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}
    }
}
