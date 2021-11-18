using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// extensions for the <see cref="IImage"/>
    /// </summary>
    public static class IImageExtensions
    {
        /// <summary>
        /// saves the image to the path
        /// </summary>
        public static void Save(this IImage image, string path)
        {
            if (image is Bitmap bitmap)
            {
                bitmap.Save(path);
            }
            else if (image is CroppedBitmap croppedBitmap)
            {
                var width = croppedBitmap.SourceRect.Width;
                var height = croppedBitmap.SourceRect.Height;

                var pixelSize = new PixelSize(width, height);
                var size = new Size(width, height);
                var dpiVector = new Vector(96, 96);
                using (var renderBitmap = new RenderTargetBitmap(pixelSize, dpiVector))
                {
                    using (var context = new DrawingContext(renderBitmap.CreateDrawingContext(null)))
                    {
                        var source = new Rect(0, 0, croppedBitmap.Size.Width, croppedBitmap.Size.Height);
                        var rect = new Rect(croppedBitmap.SourceRect.X, croppedBitmap.SourceRect.Y, croppedBitmap.SourceRect.Width, croppedBitmap.SourceRect.Height);
                        croppedBitmap.Draw(context, source, rect, Avalonia.Visuals.Media.Imaging.BitmapInterpolationMode.HighQuality);
                    }
                    renderBitmap.Save(path);
                }
            }
        }

        /// <summary>
        /// saves the image to the stream
        /// </summary>
        public static void Save(this IImage image, Stream stream)
        {
            if (image is Bitmap bitmap)
            {
                bitmap.Save(stream);
            }
            else if (image is CroppedBitmap croppedBitmap)
            {
                var width = croppedBitmap.SourceRect.Width;
                var height = croppedBitmap.SourceRect.Height;

                var pixelSize = new PixelSize(width, height);
                var size = new Size(width, height);
                var dpiVector = new Vector(96, 96);
                using (var renderBitmap = new RenderTargetBitmap(pixelSize, dpiVector))
                {
                    using (var context = new DrawingContext(renderBitmap.CreateDrawingContext(null)))
                    {
                        var source = new Rect(0, 0, croppedBitmap.Size.Width, croppedBitmap.Size.Height);
                        var rect = new Rect(croppedBitmap.SourceRect.X, croppedBitmap.SourceRect.Y, croppedBitmap.SourceRect.Width, croppedBitmap.SourceRect.Height);
                        croppedBitmap.Draw(context, source, rect, Avalonia.Visuals.Media.Imaging.BitmapInterpolationMode.HighQuality);
                    }
                    renderBitmap.Save(stream);
                }
            }
        }

        /// <summary>
        /// converts the <see cref="IImage"/> to a <see cref="System.Drawing.Bitmap"/>
        /// </summary>
        public static System.Drawing.Bitmap GetDrawingBitmap(this IImage image)
        {
            return new System.Drawing.Bitmap(new MemoryStream(image.GetImageSourceAsByte()));
        }

        /// <summary>
        /// returns the <see cref="IImage"/> to a byte array
        /// </summary>
        public static byte[] GetImageSourceAsByte(this IImage image)
        {
            try
            {
                byte[] imageContent = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream);
                    imageContent = memoryStream.ToArray();
                }
                return imageContent;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// creates a <see cref="Bitmap"/> from <see cref="System.Drawing.Bitmap"/>
        /// </summary>
        public static IImage FromDrawingBitmap(this IImage image, System.Drawing.Bitmap source)
        {
            IImage result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                source.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;
                if (memoryStream.Length > 0 && source.Width > 0 && source.Height > 0)
                {
                    result = new Bitmap(memoryStream);
                }
            }
            return result;
        }

        /// <summary>
        /// flips the <see cref="IImage"/>
        /// </summary>
        public static IImage Flip(this IImage image, FipType flipType)
        {
            using (var bitmap = image.GetDrawingBitmap())
            {
                switch (flipType)
                {
                    case FipType.Horizontal:
                        bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
                        break;

                    case FipType.Vertical:
                        bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX);
                        break;
                }

                return image.FromDrawingBitmap(bitmap);
            }
        }

        /// <summary>
        /// rotates the <see cref="IImage"/>
        /// </summary>
        public static IImage Rotate(this IImage image, RotateType rotateType)
        {
            using (var bitmap = image.GetDrawingBitmap())
            {
                switch (rotateType)
                {
                    case RotateType.LeftHandedRotation:
                        bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                        break;

                    case RotateType.RightHandedRotation:
                        bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                        break;
                }
                return image.FromDrawingBitmap(bitmap);
            }
        }

        /// <summary>
        /// zooms the <see cref="IImage"/>
        /// </summary>
        public static IImage Zoom(this string fileName, double zoomFactor)
        {
            using (var image = new Bitmap(fileName))
            {
                return image.Zoom(zoomFactor);
            }
        }

        /// <summary>
        /// zooms the <see cref="IImage"/>
        /// </summary>
        public static IImage Zoom(this IImage image, double zoomFactor)
        {
            if (zoomFactor == 0)
            {
                return image;
            }

            using (System.Drawing.Bitmap sourceBitmap = image.GetDrawingBitmap())
            {
                var width = (float)(sourceBitmap.Width);
                var height = (float)(sourceBitmap.Width);

                var scaleWidth = (int)Math.Max(Math.Round(sourceBitmap.Width * zoomFactor, MidpointRounding.ToEven), 1);
                var scaleHeight = (int)Math.Max(Math.Round(sourceBitmap.Height * zoomFactor, MidpointRounding.ToEven), 1);
                using (var scaledBitmap = new System.Drawing.Bitmap(scaleWidth, scaleHeight))
                {
                    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(scaledBitmap))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.FillRectangle(System.Drawing.Brushes.Transparent, new System.Drawing.RectangleF(0, 0, width, height));
                        graphics.DrawImage(sourceBitmap, new System.Drawing.Rectangle(0, 0, scaleWidth, scaleHeight));
                    }

                    return image.FromDrawingBitmap(scaledBitmap);
                }
            }
        }

        /// <summary>
        /// creates a cropped <see cref="IImage"/>
        /// </summary>
        public static IImage CreateCroppedBitmap(this IImage image, float x, float y, float width, float height, CroppingType croppingType)
        {
            return image.FromDrawingBitmap(CreateCroppedDrawingBitmap(image, x, y, width, height, croppingType));
        }



        /// <summary>
        /// creates a cropped <see cref="System.Drawing.Bitmap"/> by <see cref="CroppingType"/>
        /// </summary>
        private static System.Drawing.Bitmap CreateCroppedDrawingBitmap(IImage image, float x, float y, float width, float height, CroppingType croppingType)
        {


            System.Drawing.Bitmap sourceBitmap = image.GetDrawingBitmap();

            if (x < 0 || float.IsNaN(x))
            {
                x = 0;
            }

            if (y < 0 || float.IsNaN(y))
            {
                y = 0;
            }

            if (x + width >= sourceBitmap.Width)
            {
                width = sourceBitmap.Width;
            }

            if (y + height >= sourceBitmap.Height)
            {
                height = sourceBitmap.Height;
            }


            if ((int)width <= 0 || float.IsNaN(width))
            {
                width = 1;
            }

            if ((int)height <= 0 || float.IsNaN(height))
            {
                height = 1;
            }




            System.Drawing.RectangleF cropRect = new System.Drawing.RectangleF(x, y, width, height);
            var target = new System.Drawing.Bitmap((int)cropRect.Width, (int)cropRect.Height);

            if (croppingType == CroppingType.Rectangle)
            {
                using (var graphics = System.Drawing.Graphics.FromImage(target))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphics.Clear(System.Drawing.Color.Transparent);

                    graphics.DrawImage(sourceBitmap, new System.Drawing.Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        System.Drawing.GraphicsUnit.Pixel);
                    graphics.Flush();
                }
            }
            else
            {
                

                try
                {
                    using (var graphics = System.Drawing.Graphics.FromImage(target))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.Clear(System.Drawing.Color.Transparent);
                        using (System.Drawing.TextureBrush brush = new System.Drawing.TextureBrush(sourceBitmap, cropRect))
                        {
                            cropRect.X = 0;
                            cropRect.Y = 0;
                            graphics.FillEllipse(brush, cropRect);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return target;


        }
    }
}
