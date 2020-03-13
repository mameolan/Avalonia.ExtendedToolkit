using System.Linq;
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
        public static int Main(string[] args)
        {

            AppBuilder appBuilder = null;

            if(args.Contains("testing"))
            {
                appBuilder = AppBuilder.Configure<AppTesting>();
            }
            else
            {
                appBuilder = AppBuilder.Configure<App>();
            }

            appBuilder.UsePlatformDetect()
                .UseSkia()
                .UseReactiveUI()
                //.UseDataGrid()
                .LogToDebug();


            return appBuilder.StartWithClassicDesktopLifetime(args);

        }
        
    }
}
