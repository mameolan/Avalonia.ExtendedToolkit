using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit
{
    public partial class ThemeManager
    {
        // Note: add more checks if these keys aren't sufficient
        private static readonly List<string> styleKeys = new List<string>(new[]
                                                                          {
                                                                              "MahApps.Colors.Highlight",
                                                                              "MahApps.Colors.AccentBase",
                                                                              "MahApps.Brushes.Highlight",
                                                                              "MahApps.Brushes.AccentBase"
                                                                          });
    }
}
