using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// extension for the Application class
    /// </summary>
    public static class ApplicationExtension
    {

        private static IClassicDesktopStyleApplicationLifetime GetApplicationLifetime()
        {
            return Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        }

        /// <summary>
        /// if the <see cref="Application.Current"/> is 
        /// <see cref="IClassicDesktopStyleApplicationLifetime"/>
        /// the Mainwindow is returned 
        /// otherwise null is returned
        /// </summary>
        /// <returns></returns>
        public static Window GetMainWindow()
        {
            var desktopLifetime = GetApplicationLifetime();

            if (desktopLifetime != null)
            {
                return desktopLifetime.MainWindow;
            }

            return null;
        }


        /// <summary>
        /// if the <see cref="Application.Current"/> is 
        /// <see cref="IClassicDesktopStyleApplicationLifetime"/>
        /// Windows are returned 
        /// otherwise null is returned
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<Window> GetWindows()
        {
            var desktopLifetime = GetApplicationLifetime();

            if (desktopLifetime != null)
            {
                return desktopLifetime.Windows;
            }
            return null;
        }




    }
}
