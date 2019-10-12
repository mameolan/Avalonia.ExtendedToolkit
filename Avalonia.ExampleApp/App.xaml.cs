using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ExampleApp.ViewModels;
using Avalonia.ExampleApp.Views;
using Avalonia.ExtendedToolkit;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp
{
    public class App : Application
    {
        public App()
        {
            
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var vm = new MainWindowViewModel();

            //MainWindowView mainWindowView = new MainWindowView();
            //mainWindowView.DataContext = vm;
            //MetroWindow mainWindow = new MetroWindow();
            //mainWindow.Content = mainWindowView;

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = vm;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                desktopLifetime.MainWindow = mainWindow;
            //else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
            //    singleViewLifetime.MainView = new MainView();

            ThemeManager.Instance.EnableTheme(mainWindow);


            base.OnFrameworkInitializationCompleted();
        }

    }
}
