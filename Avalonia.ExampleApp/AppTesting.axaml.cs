using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ExampleApp.Model;
using Avalonia.ExampleApp.ViewModels;
using Avalonia.ExampleApp.Views;
using Avalonia.ExtendedToolkit;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace Avalonia.ExampleApp
{
    public class AppTesting : Application
    {
        public AppTesting()
        {
            
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var vm = new MainWindowViewModel();

            TestMainWindow mainWindow = new TestMainWindow();
            mainWindow.DataContext = vm;
            //mainWindow.WindowState = Controls.WindowState.Maximized;
            mainWindow.WindowState = Controls.WindowState.Normal;
            // mainWindow.HorizontalContentAlignment = Layout.HorizontalAlignment.Stretch;
            // mainWindow.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                desktopLifetime.MainWindow = mainWindow;
            //else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
            //    singleViewLifetime.MainView = new MainView();


            ThemeManager.Instance.EnableTheme(mainWindow);
            SkinManager.Instance.EnableSkin(mainWindow);

            //Splat.Locator.CurrentMutable.Register(
            //            () => new PropertyEditorCommandBinder(),
            //            typeof(ICreatesCommandBinding));



            base.OnFrameworkInitializationCompleted();
        }

    }
}
