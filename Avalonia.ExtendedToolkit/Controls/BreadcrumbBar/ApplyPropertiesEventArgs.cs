using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// apply properties event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ApplyPropertiesEventHandler(object sender, ApplyPropertiesEventArgs e);

    /// <summary>
    /// event args for apply the items binding
    /// </summary>
    public class ApplyPropertiesEventArgs: RoutedEventArgs
    {
        /// <summary>
        /// item which belongs to the breadcrumpitem
        /// </summary>
        /// <param name="item"></param>
        /// <param name="breadcrumb"></param>
        /// <param name="routedEvent"></param>
        public ApplyPropertiesEventArgs(object item, BreadcrumbItem breadcrumb, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Item = item;
            Breadcrumb = breadcrumb;
        }

        /// <summary>
        /// The breadcrumb for which to apply the properites.
        /// </summary>
        public BreadcrumbItem Breadcrumb { get; private set; }

        /// <summary>
        /// The data item of the breadcrumb.
        /// </summary>
        public object Item { get; private set; }

        /// <summary>
        /// image to show
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// The trace that is used to show the title of a breadcrumb.
        /// </summary>
        public object Trace { get; set; }

        /// <summary>
        /// The trace that is used to build the path.
        /// This can be used to remove the trace of the root item in the path, if necassary.
        /// </summary>
        public string TraceValue { get; set; }
    }
}
