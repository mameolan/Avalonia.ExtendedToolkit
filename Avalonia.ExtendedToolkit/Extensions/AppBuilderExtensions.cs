
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.ExtendedToolkit;
using Splat;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// class for application extensions
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// registers some extra service
        /// </summary>
        //.dangerous change?
        public static AppBuilder UseAvaloniaExtended<TAppBuilder>(this AppBuilder builder)
        {
            var result = builder.AfterPlatformServicesSetup(x =>
            {
                if (Locator.CurrentMutable is null)
                {
                    return;
                }
                Locator.CurrentMutable.Register<IFileDialogService>(() => new FileDialogService());
            });

            return result;
        }
    }
}
