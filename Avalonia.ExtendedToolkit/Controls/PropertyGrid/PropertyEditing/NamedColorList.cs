using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Provides a list of colors that is possible to bind to UI
    /// </summary>
    // For more details see http://shevaspace.spaces.live.com/blog/cns!FD9A0F1F8DD06954!435.entry
    public sealed class NamedColorList : ObservableCollection<NamedColor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedColorList"/> class.
        /// </summary>
        public NamedColorList()
        {
            NamedColor nc;
            const MethodAttributes inclusiveAttributes = MethodAttributes.Static | MethodAttributes.Public;

            foreach (var pi in typeof(Colors).GetProperties())
            {
                if (pi.PropertyType != typeof(Color))
                    continue;

                var mi = pi.GetGetMethod();
                if ((mi == null) || ((mi.Attributes & inclusiveAttributes) != inclusiveAttributes))
                    continue;

                nc = new NamedColor(pi.Name, (Color)pi.GetValue(null, null));
                Add(nc);
            }

        }
    }
}
