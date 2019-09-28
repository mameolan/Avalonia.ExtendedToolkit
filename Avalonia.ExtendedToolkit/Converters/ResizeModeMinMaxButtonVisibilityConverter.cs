using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Avalonia.ExtendedToolkit.Converters
{
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

        public static ResizeModeMinMaxButtonVisibilityConverter Instance
        {
            get { return _instance ?? (_instance = new ResizeModeMinMaxButtonVisibilityConverter()); }
        }


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
