using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace Avalonia.ExampleApp.Model
{
    /// <summary>
    /// orginal XmlLanguage instead of string
    /// </summary>
    public class XmlLanguageList : ObservableCollection<string>
    {
        public XmlLanguageList()
        {
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                if (string.IsNullOrEmpty(ci.IetfLanguageTag))
                    continue;
                Add(ci.IetfLanguageTag);
            }
        }
    }
}
