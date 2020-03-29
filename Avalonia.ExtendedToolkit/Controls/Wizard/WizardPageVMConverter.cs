using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class WizardPageVMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as IWizardPageVM) == null)
                return AvaloniaProperty.UnsetValue;

            var wizardPageVm = value as IWizardPageVM;

            WizardPage wizardPage = new WizardPage();
            wizardPage.DataContext = wizardPageVm;
            return wizardPage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
