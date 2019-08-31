using System;
using Avalonia.ExampleApp.ViewModels;
using Avalonia.ExampleApp.Views;
using Avalonia.ExtendedToolkit;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;

namespace Avalonia.ExampleApp
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        //public static void Main(string[] args) => BuildAvaloniaApp().Start(AppMain, args);

        //private static void AppMain(Application app, string[] args)
        //{
        //    var vm = new MainWindowViewModel();
        //    MainWindowView mainWindow = new MainWindowView();
        //    mainWindow.DataContext = vm;
        //    MetroWindow metroWindow = new MetroWindow();
        //    metroWindow.Content = mainWindow;
        //    ThemeManager.Instance.EnableTheme(metroWindow);
        //    app.Run(metroWindow);
        //}


        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseSkia()
                .UseReactiveUI()
                //.UseDataGrid()
                .LogToDebug();

    }
}
