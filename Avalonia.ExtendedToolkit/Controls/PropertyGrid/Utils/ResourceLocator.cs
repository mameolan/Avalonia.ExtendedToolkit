using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Provides a unified approach for resolving resources.
    /// </summary>
    public class ResourceLocator
    {
        private readonly Application _application;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLocator"/> class.
        /// </summary>
        public ResourceLocator() : this(Application.Current) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLocator"/> class.
        /// </summary>
        /// <param name="application">The application instance.</param>
        public ResourceLocator(Application application)
        {
            _application = application;
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Object from resources.</returns>
        public virtual object GetResource(object key)
        {
            object result = null;
             _application.TryFindResource(key, out result);
            return result;
        }
    }
}
