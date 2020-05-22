using System.Windows.Input;
using Avalonia.Collections;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// wizard vm interface
    /// </summary>
    public interface IWizardVM
    {
        /// <summary>
        /// CancelCommand
        /// </summary>
        ICommand CancelCommand { get; set; }
        /// <summary>
        /// FinishCommand
        /// </summary>
        ICommand FinishCommand { get; set; }
        /// <summary>
        /// HelpCommand
        /// </summary>
        ICommand HelpCommand { get; set; }
        /// <summary>
        /// NextPageCommand
        /// </summary>
        ICommand NextPageCommand { get; set; }
        /// <summary>
        /// PreviousPageCommand
        /// </summary>
        ICommand PreviousPageCommand { get; set; }
        /// <summary>
        /// IsBackButtonVisible
        /// </summary>
        bool IsBackButtonVisible { get; set; }
        /// <summary>
        /// IsCancelButtonVisibe
        /// </summary>
        bool IsCancelButtonVisibe { get; set; }
        /// <summary>
        /// IsFinishButtonVisible
        /// </summary>
        bool IsFinishButtonVisible { get; set; }
        /// <summary>
        /// IsHelpButtonVisible
        /// </summary>
        bool IsHelpButtonVisible { get; set; }
        /// <summary>
        /// IsNextButtonVisible
        /// </summary>
        bool IsNextButtonVisible { get; set; }
        /// <summary>
        /// collection of wizard pages
        /// </summary>
        AvaloniaList<IWizardPageVM> WizardPages { get; }
        
        //IWizardPageVM CurrentPage { get; }
        //WizardPage NextPage { get; set; }
        //WizardPage PreviousPage { get; set; }
    }
}
