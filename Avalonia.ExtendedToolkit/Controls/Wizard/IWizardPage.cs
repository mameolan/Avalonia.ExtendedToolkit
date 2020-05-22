using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// interface for a wizard page
    /// </summary>
    public interface IWizardPage
    {
        /// <summary>
        /// description
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// ExteriorPanelBackground
        /// </summary>
        Brush ExteriorPanelBackground { get; set; }
        /// <summary>
        /// ExteriorPanelContent
        /// </summary>
        object ExteriorPanelContent { get; set; }
        /// <summary>
        /// HeaderBackground
        /// </summary>
        Brush HeaderBackground { get; set; }
        /// <summary>
        /// HeaderImage
        /// </summary>
        Image HeaderImage { get; set; }
        /// <summary>
        /// PageType
        /// </summary>
        WizardPageType PageType { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        string Title { get; set; }
    }
}
