using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public interface IWizardPage
    {
        //bool? CanCancel { get; set; }
        //bool? CanFinish { get; set; }
        //bool? CanHelp { get; set; }
        //bool? CanSelectNextPage { get; set; }
        //bool? CanSelectPreviousPage { get; set; }
        string Description { get; set; }
        Brush ExteriorPanelBackground { get; set; }
        object ExteriorPanelContent { get; set; }
        Brush HeaderBackground { get; set; }
        Image HeaderImage { get; set; }
        //bool IsBackButtonVisible { get; set; }
        //bool IsCancelButtonVisibe { get; set; }
        //bool IsFinishButtonVisible { get; set; }
        //bool IsHelpButtonVisible { get; set; }
        //bool IsNextButtonVisible { get; set; }
        //WizardPage NextPage { get; set; }
        //WizardPage PreviousPage { get; set; }
        WizardPageType PageType { get; set; }
        string Title { get; set; }
    }
}