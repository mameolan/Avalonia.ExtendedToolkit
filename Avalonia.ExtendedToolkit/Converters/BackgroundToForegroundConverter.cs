using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// sets the foreground color
    /// from the background color
    /// </summary>
    public class BackgroundToForegroundConverter : IValueConverter, IMultiValueConverter
    {
        private static BackgroundToForegroundConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BackgroundToForegroundConverter()
        {
        }

        private BackgroundToForegroundConverter()
        {
        }

        /// <summary>
        /// instance
        /// </summary>
        public static BackgroundToForegroundConverter Instance
        {
            get { return _instance ?? (_instance = new BackgroundToForegroundConverter()); }
        }

        /// <summary>
        /// Determining Ideal Text Color Based on Specified Background Color
        /// http://www.codeproject.com/KB/GDI-plus/IdealTextColor.aspx
        /// </summary>
        /// <param name = "bg">The bg.</param>
        /// <returns></returns>
        private Color IdealTextColor(Color bg)
        {
            const int nThreshold = 86;//105;
            var bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            var foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        /// <summary>
        /// values first: background color
        /// values second titlebroush
        /// if title brush is not null return title brush
        /// else call convert
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            var bgBrush = values.Count > 0 ? values[0] as Brush : null;
            var titleBrush = values.Count > 1 ? values[1] as Brush : null;
            if (titleBrush != null)
            {
                return titleBrush;
            }
            return Convert(bgBrush, targetType, parameter, culture);
        }

        /// <summary>
        /// if value is <see cref="SolidColorBrush"/>
        /// find ideal color
        /// else
        /// return white
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                var idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                var foreGroundBrush = new SolidColorBrush(idealForegroundColor);
                //foreGroundBrush.Freeze();
                return foreGroundBrush;
            }
            return Brushes.White;
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
