using Avalonia.Collections;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class WizardItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return AvaloniaProperty.UnsetValue;

            var list = value as IEnumerable<IWizardPageVM>;

            var result = new AvaloniaList<WizardPage>();



            foreach (IWizardPageVM wizardPageVM in list)
            {
                IWizardPageVM vm = wizardPageVM as IWizardPageVM;
                WizardPage wizardPage = new WizardPage();
                wizardPage.DataContext = vm;
                result.Add(wizardPage);
            }





            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
