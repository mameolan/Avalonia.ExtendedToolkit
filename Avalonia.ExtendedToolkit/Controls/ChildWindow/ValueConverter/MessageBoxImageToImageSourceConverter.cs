using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Avalonia.ExtendedToolkit.Controls.ChildWindowConverter
{
    //ported from: https://github.com/punker76/MahApps.Metro.SimpleChildWindow/�

    /// <summary>
    /// tries to set the correct icon
    /// </summary>
    public class MessageBoxImageToImageSourceConverter : IValueConverter
    {
        private readonly Visual hand_stop_error;

        private const string HandStopErrorResource = "avares://Avalonia.ExtendedToolkit/Styles/ExtendedControls/ChildWindow/Icons.axaml";

        /// <summary>
        /// gets the correct visual from the xaml
        /// </summary>
        public MessageBoxImageToImageSourceConverter()
        {
            var icons = new StyleInclude(new Uri($"resm:Styles?assembly={this.GetType().Namespace}"))
            {
                Source = new Uri(HandStopErrorResource)
            };

            object icon = null;
            if (icons.TryGetResource("appbar_noentry", ThemeVariant.Default, out icon))
            {
                hand_stop_error = icon as Visual;
            }
        }

        /// <summary>
        /// returns the correct visual from resources
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageBoxImage)
            {
                switch ((MessageBoxImage)value)
                {
                    case MessageBoxImage.None:
                        break;

                    case MessageBoxImage.Hand:
                        return this.hand_stop_error;

                    case MessageBoxImage.Question:
                        break;

                    case MessageBoxImage.Exclamation:
                        break;

                    case MessageBoxImage.Asterisk:
                        break;
                }
            }
            return AvaloniaProperty.UnsetValue;
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
