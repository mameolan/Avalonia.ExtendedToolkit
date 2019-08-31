using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class WizardPageViewModel : ReactiveObject, IWizardPageVM
    {
        private bool myIsBackButtonVisible = true;

        public bool IsBackButtonVisible
        {
            get { return myIsBackButtonVisible; }
            set
            {
                this.RaiseAndSetIfChanged(ref myIsBackButtonVisible, value);
            }
        }

        private bool myIsCancelButtonVisibe = true;

        public bool IsCancelButtonVisibe
        {
            get { return myIsCancelButtonVisibe; }
            set
            {
                this.RaiseAndSetIfChanged(ref myIsCancelButtonVisibe, value);
            }
        }

        private bool myIsFinishButtonVisible = true;

        public bool IsFinishButtonVisible
        {
            get { return myIsFinishButtonVisible; }
            set
            {
                this.RaiseAndSetIfChanged(ref myIsFinishButtonVisible, value);
            }
        }

        private bool myIsHelpButtonVisible = true;

        public bool IsHelpButtonVisible
        {
            get { return myIsHelpButtonVisible; }
            set
            {
                this.RaiseAndSetIfChanged(ref myIsHelpButtonVisible, value);
            }
        }

        private bool myIsNextButtonVisible = true;

        public bool IsNextButtonVisible
        {
            get { return myIsNextButtonVisible; }
            set
            {
                this.RaiseAndSetIfChanged(ref myIsNextButtonVisible, value);
            }
        }

        private string myDescription;

        public string Description
        {
            get { return myDescription; }
            set
            {
                this.RaiseAndSetIfChanged(ref myDescription, value);
            }
        }

        private WizardPageType myPageType;

        public WizardPageType PageType
        {
            get { return myPageType; }
            set
            {
                this.RaiseAndSetIfChanged(ref myPageType, value);
            }
        }

        private string myTitle;

        public string Title
        {
            get { return myTitle; }
            set
            {
                this.RaiseAndSetIfChanged(ref myTitle, value);
            }
        }

        private bool myIsValid;

        public bool IsValid
        {
            get { return myIsValid; }
            protected set
            {
                this.RaiseAndSetIfChanged(ref myIsValid, value);
            }
        }

        public void Validate()
        {
        }

        internal void OnEnterPage()
        {
            Validate();
        }

        internal void OnLeavePage()
        {
        }
    }
}