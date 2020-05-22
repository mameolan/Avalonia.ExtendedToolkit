namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// wizard page interface
    /// </summary>
    public interface IWizardPageVM
    {
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
        /// Description
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// PageType
        /// </summary>
        WizardPageType PageType { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// IsValid
        /// </summary>
        bool IsValid { get; }
        /// <summary>
        /// validates the model
        /// </summary>
        void Validate();
    }
}
