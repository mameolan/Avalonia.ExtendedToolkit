using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Skia;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Rendering.Composition;
using Avalonia.Platform;

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
                var dpiVector = new Vector(96, 96);
                using (RenderTargetBitmap renderBitmap = new RenderTargetBitmap(pixelSize, dpiVector))
                {
                    using (var context = renderBitmap.CreateDrawingContext())
                    {
                        var source = new Rect(0, 0, croppedBitmap.Size.Width, croppedBitmap.Size.Height);
                        var rect = new Rect(croppedBitmap.SourceRect.X, croppedBitmap.SourceRect.Y, croppedBitmap.SourceRect.Width, croppedBitmap.SourceRect.Height);
                        croppedBitmap.Draw(context, source, rect);
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
                    using (var context = renderBitmap.CreateDrawingContext())
                    {
                        var source = new Rect(0, 0, croppedBitmap.Size.Width, croppedBitmap.Size.Height);
                        var rect = new Rect(croppedBitmap.SourceRect.X, croppedBitmap.SourceRect.Y, croppedBitmap.SourceRect.Width, croppedBitmap.SourceRect.Height);
                        croppedBitmap.Draw(context, source, rect);
                    }
                    renderBitmap.Save(stream);
                }
            }
        }

        /// <summary>
        /// converts the <see cref="IImage"/> to a <see cref="Avalonia.Media.Imaging.RenderTargetBitmap"/>
        /// </summary>
        public static RenderTargetBitmap GetDrawingBitmap(this IImage image)
        {
            return new RenderTargetBitmap(new PixelSize((int)image.Size.Width, (int)image.Size.Width));
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
        /// creates a <see cref="Bitmap"/> from <see cref="Avalonia.Media.Imaging.RenderTargetBitmap"/>
        /// </summary>
        public static IImage FromDrawingBitmap(this IImage image, RenderTargetBitmap source)
        {
            IImage result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                source.Save(memoryStream);
                memoryStream.Position = 0;
                if (memoryStream.Length > 0 && source.Size.Width > 0 && source.Size.Height > 0)
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
                        bitmap.Flip(FipType.Horizontal);
                        break;

                    case FipType.Vertical:
                        bitmap.Flip(FipType.Vertical);
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
                        bitmap.Rotate(RotateType.LeftHandedRotation);
                        break;

                    case RotateType.RightHandedRotation:
                        bitmap.Rotate(RotateType.RightHandedRotation);
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

            using (RenderTargetBitmap sourceBitmap = image.GetDrawingBitmap())
            {
                var width = (float)(sourceBitmap.Size.Width);
                var height = (float)(sourceBitmap.Size.Height);

                var scaleWidth = (int)Math.Max(Math.Round(sourceBitmap.Size.Width * zoomFactor, MidpointRounding.ToEven), 1);
                var scaleHeight = (int)Math.Max(Math.Round(sourceBitmap.Size.Height * zoomFactor, MidpointRounding.ToEven), 1);
                using (var scaledBitmap = new RenderTargetBitmap(new PixelSize(scaleWidth, scaleHeight)))
                {
                    //var graphics = new DrawingImage { };
                    //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    //graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //graphics.FillRectangle(System.Drawing.Brushes.Transparent, new System.Drawing.RectangleF(0, 0, width, height));
                    //graphics.Drawing { }
                    //(sourceBitmap, new System.Drawing.Rectangle(0, 0, scaleWidth, scaleHeight));
                    //}
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
        /// creates a cropped <see cref="RenderTargetBitmap"/> by <see cref="CroppingType"/>
        /// </summary>
        private static RenderTargetBitmap CreateCroppedDrawingBitmap(IImage image, float x, float y, float width, float height, CroppingType croppingType)
        {

            RenderTargetBitmap sourceBitmap = image.GetDrawingBitmap();

            if (x < 0 || float.IsNaN(x))
            {
                x = 0;
            }

            if (y < 0 || float.IsNaN(y))
            {
                y = 0;
            }

            if (x + width >= sourceBitmap.Size.Width)
            {
                width = (float)sourceBitmap.Size.Width;
            }

            if (y + height >= sourceBitmap.Size.Height)
            {
                height = (float)sourceBitmap.Size.Height;
            }


            if ((int)width <= 0 || float.IsNaN(width))
            {
                width = 1;
            }

            if ((int)height <= 0 || float.IsNaN(height))
            {
                height = 1;
            }

            var cropRect = new Rect(x, y, width, height);
            var target = new RenderTargetBitmap(new PixelSize((int)cropRect.Width, (int)cropRect.Height));

            if (croppingType == CroppingType.Rectangle)
            {
                //using (var graphics = System.Drawing.Graphics.FromImage(target))
                //{
                //    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //    graphics.Clear(System.Drawing.Color.Transparent);

                //    graphics.DrawImage(sourceBitmap, new System.Drawing.Rectangle(0, 0, target.Width, target.Height),
                //        cropRect,
                //        System.Drawing.GraphicsUnit.Pixel);
                //    graphics.Flush();
                //}
            }
            else
            {
                

                try
                {
                    //using (var graphics = System.Drawing.Graphics.FromImage(target))
                    //{
                    //    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    //    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //    graphics.Clear(System.Drawing.Color.Transparent);
                    //    using (System.Drawing.TextureBrush brush = new System.Drawing.TextureBrush(sourceBitmap, cropRect))
                    //    {
                    //        cropRect.X = 0;
                    //        cropRect.Y = 0;
                    //        graphics.FillEllipse(brush, cropRect);
                    //    }
                    //}
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
