using Avalonia.Collections;
using ReactiveUI;
using System.Linq;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class WizardViewModelBase : ReactiveObject, IWizardVM
    {
        private bool myCanCancel = true;

        public bool CanCancel
        {
            get { return myCanCancel; }
            set
            {
                this.RaiseAndSetIfChanged(ref myCanCancel, value);
            }
        }

        private bool myCanFinish;

        public bool CanFinish
        {
            get { return myCanFinish; }
            set
            {
                this.RaiseAndSetIfChanged(ref myCanFinish, value);
            }
        }

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

        private bool myHasPreviousPage;

        public bool HasPreviousPage
        {
            get { return myHasPreviousPage; }
            set
            {
                this.RaiseAndSetIfChanged(ref myHasPreviousPage, value);
            }
        }

        private bool myHasNextPage;

        public bool HasNextPage
        {
            get { return myHasNextPage; }
            set
            {
                this.RaiseAndSetIfChanged(ref myHasNextPage, value);
            }
        }

        private IWizardPageVM myCurrentPage;

        public IWizardPageVM CurrentPage
        {
            get { return myCurrentPage; }
            set
            {
                if (myCurrentPage != null)
                {
                    (myCurrentPage as WizardPageViewModel)?.OnLeavePage();
                }

                myCurrentPage = value;

                if (myCurrentPage != null)
                {
                    (myCurrentPage as WizardPageViewModel)?.OnEnterPage();
                }

                UpdatePageCommand();

                this.RaisePropertyChanged();
            }
        }

        private void UpdatePageCommand()
        {
            HasNextPage = NextPageExists;
            HasPreviousPage = PreviousPageExists;
            CanFinish = CurrentPage.IsValid == true && HasNextPage == false;
        }

        public ICommand CancelCommand { get; set; }
        public ICommand FinishCommand { get; set; }

        public ICommand HelpCommand { get; set; }

        public ICommand NextPageCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }

        public AvaloniaList<IWizardPageVM> WizardPages { get; } = new AvaloniaList<IWizardPageVM>();

        public WizardViewModelBase()
        {
            CancelCommand = ReactiveCommand.Create(ExecuteCancelCommand, outputScheduler: RxApp.MainThreadScheduler);

            var canExecuteFinishCommand = this.WhenAnyValue(x => x.CanFinish,
                 (canFinish) => (canFinish == true));
            FinishCommand = ReactiveCommand.Create(ExecuteFinishCommand, canExecuteFinishCommand, RxApp.MainThreadScheduler);

            var canExecuteHelpCommand = this.WhenAnyValue(x => x.IsHelpButtonVisible,
                (isHelpAvailable) => (isHelpAvailable == true));

            HelpCommand = ReactiveCommand.Create(ExecuteHelpCommand, canExecuteHelpCommand, RxApp.MainThreadScheduler);

            var canExecuteNextPageCommand = this.WhenAnyValue(x => x.HasNextPage,
                (nextPageExists) => (nextPageExists == true));
            NextPageCommand = ReactiveCommand.Create(ExecuteNextPageCommand, canExecuteNextPageCommand, RxApp.MainThreadScheduler);  //new DelegateCommand(x => ExecuteNextPageCommand(), x => CanExecuteNextPageCommand());//

            var canExecutePreviousPageCommand = this.WhenAnyValue(x => x.HasPreviousPage,
                (previousPageExists) => (previousPageExists == true));
            PreviousPageCommand = ReactiveCommand.Create(ExecutePreviousPageCommand, canExecutePreviousPageCommand, RxApp.MainThreadScheduler); //new DelegateCommand(x => ExecutePreviousPageCommand(), x => CanExecutePreviousPageCommand());//

            WizardPages.CollectionChanged += (o, e) =>
            {
                CurrentPage = WizardPages.FirstOrDefault();
                UpdatePageCommand();
            };
        }

        private void ExecutePreviousPageCommand()
        {
            IWizardPageVM previousPage = null;
            var currentIndex = WizardPages.IndexOf(CurrentPage);
            var previousPageIndex = currentIndex - 1;
            if (previousPageIndex >= 0 && previousPageIndex < WizardPages.Count)
                previousPage = WizardPages[previousPageIndex];
            CurrentPage = previousPage;
            UpdatePageCommand();
        }

        private void ExecuteNextPageCommand()
        {
            IWizardPageVM nextPage = null;

            var currentIndex = WizardPages.IndexOf(CurrentPage);
            var nextPageIndex = currentIndex + 1;
            if (nextPageIndex < WizardPages.Count)
                nextPage = WizardPages[nextPageIndex];

            CurrentPage = nextPage;

            UpdatePageCommand();
        }

        private void ExecuteHelpCommand()
        {
        }

        private void ExecuteFinishCommand()
        {
        }

        private void ExecuteCancelCommand()
        {
        }

        private bool NextPageExists
        {
            get
            {
                bool exists = false;

                //lets use an index to find the next page
                var currentIndex = WizardPages.IndexOf(CurrentPage);
                var nextPageIndex = currentIndex + 1;
                if (nextPageIndex < WizardPages.Count())
                    exists = true;

                return exists;
            }
        }

        private bool PreviousPageExists
        {
            get
            {
                bool exists = false;

                //lets use an index to find the next page
                var currentIndex = WizardPages.IndexOf(CurrentPage);
                var previousPageIndex = currentIndex - 1;
                if (previousPageIndex >= 0 && previousPageIndex < WizardPages.Count)
                    exists = true;

                return exists;
            }
        }
    }
}