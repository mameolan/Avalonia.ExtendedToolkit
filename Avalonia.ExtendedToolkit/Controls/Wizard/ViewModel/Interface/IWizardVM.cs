using System.Windows.Input;
using Avalonia.Collections;

namespace Avalonia.ExtendedToolkit.Controls
{
    public interface IWizardVM
    {
        ICommand CancelCommand { get; set; }
        ICommand FinishCommand { get; set; }
        ICommand HelpCommand { get; set; }
        ICommand NextPageCommand { get; set; }
        ICommand PreviousPageCommand { get; set; }
        bool IsBackButtonVisible { get; set; }
        bool IsCancelButtonVisibe { get; set; }
        bool IsFinishButtonVisible { get; set; }
        bool IsHelpButtonVisible { get; set; }
        bool IsNextButtonVisible { get; set; }
        AvaloniaList<IWizardPageVM> WizardPages { get; }
        //IWizardPageVM CurrentPage { get; }

        //WizardPage NextPage { get; set; }
        //WizardPage PreviousPage { get; set; }
    }
}
