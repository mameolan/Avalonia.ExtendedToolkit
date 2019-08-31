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
            var test = ThemeManager.Instance.BaseColors;
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var vm = new MainWindowViewModel();

            MainWindowView mainWindowView = new MainWindowView();
            mainWindowView.DataContext = vm;
            MetroWindow metroWindow = new MetroWindow();
            metroWindow.Content = mainWindowView;

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.DataContext = vm;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                desktopLifetime.MainWindow = metroWindow;
            //else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
            //    singleViewLifetime.MainView = new MainView();

            ThemeManager.Instance.EnableTheme(metroWindow);


            base.OnFrameworkInitializationCompleted();
        }

    }
}
