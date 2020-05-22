using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// <see cref="ResizeMode"/> to bool converter
    /// </summary>
    public sealed class ResizeModeMinMaxButtonVisibilityConverter : IMultiValueConverter
    {
        private static ResizeModeMinMaxButtonVisibilityConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ResizeModeMinMaxButtonVisibilityConverter()
        {
        }

        private ResizeModeMinMaxButtonVisibilityConverter()
        {
        }

        /// <summary>
        /// returns the singelton
        /// </summary>
        public static ResizeModeMinMaxButtonVisibilityConverter Instance
        {
            get { return _instance ?? (_instance = new ResizeModeMinMaxButtonVisibilityConverter()); }
        }

        /// <summary>
        /// values first: bool showButton
        /// values second: bool useNoneWindowStyle
        /// values third: ResizeMode windowResizeMode
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            var whichButton = parameter as string;
            if (values != null && !string.IsNullOrEmpty(whichButton))
            {
                List<bool> localValues = values.OfType<bool>().ToList();

                var showButton = localValues.Count > 0 && (bool)localValues[0];
                var useNoneWindowStyle = localValues.Count > 1 && (bool)localValues[1];
                var windowResizeMode = localValues.Count > 2 ? (ResizeMode)values[2] : ResizeMode.CanResize;

                if (whichButton == "CLOSE")
                {
                    return useNoneWindowStyle || !showButton ? false : true;
                }

                switch (windowResizeMode)
                {
                    case ResizeMode.NoResize:
                        return false;

                    case ResizeMode.CanMinimize:
                        if (whichButton == "MIN")
                        {
                            return useNoneWindowStyle || !showButton ? false : true;
                        }
                        return false;

                    case ResizeMode.CanResize:
                    case ResizeMode.CanResizeWithGrip:
                    default:
                        return useNoneWindowStyle || !showButton ? false : true;
                }
            }
            return true;
        }
    }
}
