using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.IO;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from: 
    //https://www.codeproject.com/Articles/42849/Making-a-Drop-Down-Style-Custom-Color-Picker-in-WP

    /// <summary>
    /// color picker helper class
    /// </summary>
    internal static class ColorPickerHelper
    {
        /// <summary>
        /// 1*1 pixel copy is based on an article by Lee Brimelow
        /// http://thewpfblog.com/?p=62
        /// </summary>
        internal static Color GetColorFromImage(Image image,int i, int j)
        {
            //System.Drawing.Color color;
            //using (var mem = new MemoryStream())
            //{

            //    (image.Source as Bitmap).Save(mem);
            //    Bitmap bitmap = new Bitmap(mem);
            //    color = bitmap.GetPixel(i, j);
            //}
            //.edited
            CroppedBitmap cb = new CroppedBitmap(image.Source as Bitmap,
                new PixelRect(i,
                    j, 1, 1));
            byte[] color = new byte[4];
            //.edited, seems stupid fix
            color = cb.GetImageSourceAsByte();
            //cb.CopyPixels(color, 4, 0);
            Color Colorfromimagepoint = Color.FromArgb(color[0], color[1], color[2], color[3]);
            return Colorfromimagepoint;
        }

        /// <summary>
        /// checks if the color value is equal
        /// </summary>
        /// <param name="pointColor"></param>
        /// <param name="selectedColor"></param>
        /// <returns></returns>
        internal static bool SimmilarColor(Color pointColor, Color selectedColor)
        {
            int diff = Math.Abs(pointColor.R - selectedColor.R) +
                Math.Abs(pointColor.G - selectedColor.G) + Math.Abs(pointColor.B - selectedColor.B);
            if (diff < 20) return true;
            else
                return false;
        }

        /// <summary>
        /// tries to get the color from the textbox (<param name="sender"/>)
        /// if it is not textbox the <param name="customColor"/> is used
        /// to set the <param name="allText"/> value finally the custom color
        /// is returned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="customColor"></param>
        /// <param name="allText"></param>
        /// <returns></returns>
        internal static Color MakeColorFromHex(object sender, Color customColor, out string allText)
        {
            allText = string.Empty;
            try
            {
                string text = ((TextBox)sender).Text;

                return (Color.Parse(text));
            }
            catch
            {
                string alphaHex = customColor.A.ToString("X").PadLeft(2, '0');
                string redHex = customColor.R.ToString("X").PadLeft(2, '0');
                string greenHex = customColor.G.ToString("X").PadLeft(2, '0');
                string blueHex = customColor.B.ToString("X").PadLeft(2, '0');
                allText = String.Format("#{0}{1}{2}{3}",
                alphaHex, redHex,
                greenHex, blueHex);
            }
            return customColor;
        }

        /// <summary>
        /// returns a color object from the parameters
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        internal static Color MakeColorFromRGB(string alpha, string red, string green, string blue)
        {
            byte abyteValue = Convert.ToByte(alpha);
            byte rbyteValue = Convert.ToByte(red);
            byte gbyteValue = Convert.ToByte(green);
            byte bbyteValue = Convert.ToByte(blue);
            Color rgbColor =
                 Color.FromArgb(
                     abyteValue,
                     rbyteValue,
                     gbyteValue,
                     bbyteValue);
            return rgbColor;
        }

        internal static Color MakeColorFromRGB(uint alpha, uint red, uint green, uint blue)
        {
            byte abyteValue = Convert.ToByte(alpha);
            byte rbyteValue = Convert.ToByte(red);
            byte gbyteValue = Convert.ToByte(green);
            byte bbyteValue = Convert.ToByte(blue);
            Color rgbColor =
                 Color.FromArgb(
                     abyteValue,
                     rbyteValue,
                     gbyteValue,
                     bbyteValue);
            return rgbColor;
        }



    }
}
