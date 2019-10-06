namespace Avalonia.ExtendedToolkit.Controls
{
    public interface IWizardPageVM
    {
        bool IsBackButtonVisible { get; set; }
        bool IsCancelButtonVisibe { get; set; }
        bool IsFinishButtonVisible { get; set; }
        bool IsHelpButtonVisible { get; set; }
        bool IsNextButtonVisible { get; set; }
        string Description { get; set; }
        WizardPageType PageType { get; set; }
        string Title { get; set; }
        bool IsValid { get; }

        void Validate();
    }
}