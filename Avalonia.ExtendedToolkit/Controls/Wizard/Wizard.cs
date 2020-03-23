using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class Wizard : ItemsControl
    {
        private string ButtonName_Help = "HelpButton";
        private string ButtonName_Back = "BackButton";
        private string ButtonName_Next = "NextButton";
        private string ButtonName_Finish = "FinishButton";
        private string ButtonName_Cancel = "CancelButton";

        private Button btnHelp;
        private Button btnBack;
        private Button btnNext;
        private Button btnFinish;
        private Button btnCancel;

        /// <summary>
        /// used for viewmodel binding
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
            AvaloniaProperty.Register<Wizard, IEnumerable>(nameof(ItemsSource));

        public object BackButtonContent
        {
            get { return (object)GetValue(BackButtonContentProperty); }
            set { SetValue(BackButtonContentProperty, value); }
        }

        public static readonly StyledProperty<object> BackButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(BackButtonContent), defaultValue: "< Back");

        public bool IsBackButtonVisible
        {
            get { return (bool)GetValue(IsBackButtonVisibleProperty); }
            set { SetValue(IsBackButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsBackButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsBackButtonVisible), defaultValue: true);

        public bool CanCancel
        {
            get { return (bool)GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        public static readonly StyledProperty<bool> CanCancelProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanCancel), defaultValue: true);

        public bool CancelButtonClosesWindow
        {
            get { return (bool)GetValue(CancelButtonClosesWindowProperty); }
            set { SetValue(CancelButtonClosesWindowProperty, value); }
        }

        public static readonly StyledProperty<bool> CancelButtonClosesWindowProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CancelButtonClosesWindow), defaultValue: true);

        public object CancelButtonContent
        {
            get { return (object)GetValue(CancelButtonContentProperty); }
            set { SetValue(CancelButtonContentProperty, value); }
        }

        public static readonly StyledProperty<object> CancelButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(CancelButtonContent), defaultValue: "Cancel");

        public bool IsCancelButtonVisible
        {
            get { return (bool)GetValue(IsCancelButtonVisibleProperty); }
            set { SetValue(IsCancelButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsCancelButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsCancelButtonVisible), defaultValue: true);

        public bool CanFinish
        {
            get { return (bool)GetValue(CanFinishProperty); }
            set { SetValue(CanFinishProperty, value); }
        }

        public static readonly StyledProperty<bool> CanFinishProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanFinish), defaultValue: false);

        public bool CanHelp
        {
            get { return (bool)GetValue(CanHelpProperty); }
            set { SetValue(CanHelpProperty, value); }
        }

        public static readonly StyledProperty<bool> CanHelpProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanHelp), defaultValue: true);

        public bool CanSelectNextPage
        {
            get { return (bool)GetValue(CanSelectNextPageProperty); }
            set { SetValue(CanSelectNextPageProperty, value); }
        }

        public static readonly StyledProperty<bool> CanSelectNextPageProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanSelectNextPage), defaultValue: true);

        public bool CanSelectPreviousPage
        {
            get { return (bool)GetValue(CanSelectPreviousPageProperty); }
            set { SetValue(CanSelectPreviousPageProperty, value); }
        }

        public static readonly StyledProperty<bool> CanSelectPreviousPageProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanSelectPreviousPage), defaultValue: true);

        public WizardPage CurrentPage
        {
            get { return (WizardPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly StyledProperty<WizardPage> CurrentPageProperty =
            AvaloniaProperty.Register<Wizard, WizardPage>(nameof(CurrentPage));

        public IWizardPageVM CurrentWizardPageVM
        {
            get { return (IWizardPageVM)GetValue(CurrentWizardPageVMProperty); }
            set { SetValue(CurrentWizardPageVMProperty, value); }
        }

        public static readonly StyledProperty<IWizardPageVM> CurrentWizardPageVMProperty =
            AvaloniaProperty.Register<Wizard, IWizardPageVM>(nameof(CurrentWizardPageVM));

        private void OnCurrentPageChanged(Wizard o, AvaloniaPropertyChangedEventArgs e)
        {
            OnCurrentPageChanged(e.OldValue as WizardPage, e.NewValue as WizardPage);
        }

        private void OnCurrentPageChanged(WizardPage oldValue, WizardPage newValue)
        {
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = Wizard.PageChangedEvent,
                Source = this
            });
        }

        public double ExteriorPanelMinWidth
        {
            get { return (double)GetValue(ExteriorPanelMinWidthProperty); }
            set { SetValue(ExteriorPanelMinWidthProperty, value); }
        }

        public static readonly StyledProperty<double> ExteriorPanelMinWidthProperty =
            AvaloniaProperty.Register<Wizard, double>(nameof(ExteriorPanelMinWidth), defaultValue: 165.0);

        public bool FinishButtonClosesWindow
        {
            get { return (bool)GetValue(FinishButtonClosesWindowProperty); }
            set { SetValue(FinishButtonClosesWindowProperty, value); }
        }

        public static readonly StyledProperty<bool> FinishButtonClosesWindowProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(FinishButtonClosesWindow), defaultValue: true);

        public object FinishButtonContent
        {
            get { return (object)GetValue(FinishButtonContentProperty); }
            set { SetValue(FinishButtonContentProperty, value); }
        }

        public static readonly StyledProperty<object> FinishButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(FinishButtonContent), defaultValue: "Finish");

        public bool IsFinishButtonVisible
        {
            get { return (bool)GetValue(IsFinishButtonVisibleProperty); }
            set { SetValue(IsFinishButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsFinishButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsFinishButtonVisible), defaultValue: false);

        public object HelpButtonContent
        {
            get { return (object)GetValue(HelpButtonContentProperty); }
            set { SetValue(HelpButtonContentProperty, value); }
        }

        public static readonly StyledProperty<object> HelpButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(HelpButtonContent), defaultValue: "Help");

        public bool IsHelpButtonVisible
        {
            get { return (bool)GetValue(IsHelpButtonVisibleProperty); }
            set { SetValue(IsHelpButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsHelpButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsHelpButtonVisible), defaultValue: true);

        public object NextButtonContent
        {
            get { return (object)GetValue(NextButtonContentProperty); }
            set { SetValue(NextButtonContentProperty, value); }
        }

        public static readonly StyledProperty<object> NextButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(NextButtonContent), defaultValue: "Next >");

        public bool IsNextButtonVisible
        {
            get { return (bool)GetValue(IsNextButtonVisibleProperty); }
            set { SetValue(IsNextButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsNextButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsNextButtonVisible), defaultValue: true);

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CancelCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(CancelCommand));

        public ICommand FinishCommand
        {
            get { return (ICommand)GetValue(FinishCommandProperty); }
            set { SetValue(FinishCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> FinishCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(FinishCommand));

        public ICommand HelpCommand
        {
            get { return (ICommand)GetValue(HelpCommandProperty); }
            set { SetValue(HelpCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> HelpCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(HelpCommand));

        public ICommand NextPageCommand
        {
            get { return (ICommand)GetValue(NextPageCommandProperty); }
            set { SetValue(NextPageCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> NextPageCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(NextPageCommand));

        public ICommand PreviousPageCommand
        {
            get { return (ICommand)GetValue(PreviousPageCommandProperty); }
            set { SetValue(PreviousPageCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> PreviousPageCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(PreviousPageCommand));

        //public static RoutedCommand CancelCommand { get; } =
        //    new RoutedCommand(nameof(CancelCommand), RoutingStrategies.Bubble, typeof(WizardCommands));

        //public static RoutedCommand FinishCommand { get; } =
        //    new RoutedCommand(nameof(FinishCommand), RoutingStrategies.Bubble, typeof(WizardCommands));
        //public static RoutedCommand HelpCommand { get; } =
        //    new RoutedCommand(nameof(HelpCommand), RoutingStrategies.Bubble, typeof(WizardCommands));

        //public static RoutedCommand NextPageCommand { get; } =
        //    new RoutedCommand(nameof(NextPageCommand), RoutingStrategies.Bubble, typeof(WizardCommands));

        //public static RoutedCommand PreviousPageCommand { get; } =
        //    new RoutedCommand(nameof(PreviousPageCommand), RoutingStrategies.Bubble, typeof(WizardCommands));

        public Wizard()
        {
            Items = new AvaloniaList<WizardPage>();

            CurrentPageProperty.Changed.AddClassHandler((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o, e) => OnCurrentPageChanged(o, e)));
            ItemsPanelProperty.Changed.AddClassHandler((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o, e) => OnItemChanged(o, e)));
            ItemsSourceProperty.Changed.AddClassHandler((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o, e) => OnItemSourceChanged(o, e)));
            CurrentWizardPageVMProperty.Changed.AddClassHandler((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o, e) => OnCurrentWizardPageVM(o, e)));

            this.Initialized += (o, e) =>
            {
                if (Items.OfType<object>().Any() && CurrentPage == null)
                    CurrentPage = Items.OfType<WizardPage>().FirstOrDefault();
            };

            //CommandBindings.Add(new CommandBinding(CancelCommand, ExecuteCancelWizard, CanExecuteCancelWizard));
            //CommandBindings.Add(new CommandBinding(FinishCommand, ExecuteFinishWizard, CanExecuteFinishWizard));
            //CommandBindings.Add(new CommandBinding(HelpCommand, ExecuteRequestHelp, CanExecuteRequestHelp));
            //CommandBindings.Add(new CommandBinding(NextPageCommand, ExecuteSelectNextPage, CanExecuteSelectNextPage));
            //CommandBindings.Add(new CommandBinding(PreviousPageCommand, ExecuteSelectPreviousPage, CanExecuteSelectPreviousPage));
        }

        private void OnCurrentWizardPageVM(Wizard o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;

            IWizardPageVM wizardPageVM = e.NewValue as IWizardPageVM;

            CurrentPage = this.Items.OfType<WizardPage>().FirstOrDefault(x => x.DataContext == wizardPageVM);
        }

        private void OnItemSourceChanged(Wizard wizard, AvaloniaPropertyChangedEventArgs e)
        {
            if (Items == null)
                Items = new AvaloniaList<WizardPage>();

            if (e.NewValue == null)
                return;

            var list = e.NewValue as IEnumerable<IWizardPageVM>;

            if (list != null)
            {
                foreach (IWizardPageVM wizardPageVM in list)
                {
                    IWizardPageVM vm = wizardPageVM as IWizardPageVM;
                    WizardPage wizardPage = new WizardPage();
                    wizardPage.DataContext = vm;
                    wizardPage.CanCancel = CanCancel;
                    (this.Items as AvaloniaList<WizardPage>).Add(wizardPage);
                }
            }
        }

        private void OnItemChanged(Wizard o, AvaloniaPropertyChangedEventArgs e)
        {
            if (Items == null)
            {
                Items = new AvaloniaList<WizardPage>();
            }

            if (e.NewValue == null)
                return;

            if (Items.OfType<WizardPage>().Count() > 0 && CurrentPage == null)
                CurrentPage = Items.OfType<WizardPage>().FirstOrDefault();
        }

        protected override void OnPropertyChanged<T>(AvaloniaProperty<T> property, Optional<T> oldValue, BindingValue<T> newValue, BindingPriority priority)
        {
            base.OnPropertyChanged(property, oldValue, newValue, priority);
            if ((property.Name == nameof(CanSelectNextPage)) || (property.Name == nameof(CanHelp))
            || (property.Name == nameof(CanFinish)) || (property.Name == nameof(CanCancel))
            || (property.Name == nameof(CanSelectPreviousPage))
            || (property.Name == nameof(CurrentPage))
            )
            {
                //CommandManager.InvalidateRequerySuggested();
                CancelCommand?.CanExecute(CanCancel);
                FinishCommand?.CanExecute(CanFinish);
                PreviousPageCommand?.CanExecute(CanSelectPreviousPage);
                NextPageCommand?.CanExecute(CanSelectNextPage);
                HelpCommand?.CanExecute(CanHelp);
                UpdateButtonState();
            }
        }

        private void ExecuteCancelWizard()
        {
            RaiseRoutedEvent(Wizard.CancelEvent);
            if (CancelCommand?.CanExecute(CanCancel) == true)
            {
                CancelCommand?.Execute(CanCancel);
            }
            if (CancelButtonClosesWindow)
                CloseParentWindow(false);
        }

        private bool CanExecuteCancelWizard()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanCancel.HasValue)
                    result = CurrentPage.CanCancel.Value;
                else
                    result = CanCancel;
            }
            return result;
        }

        private void ExecuteFinishWizard()
        {
            if (FinishCommand?.CanExecute(CanFinish) == true)
            {
                FinishCommand?.Execute(CanFinish);
            }

            var eventArgs = new RoutedEventArgs(Wizard.FinishEvent);
            this.RaiseEvent(eventArgs);
            //if (eventArgs.Cancel)
            //    return;

            if (FinishButtonClosesWindow)
                CloseParentWindow(true);
        }

        private bool CanExecuteFinishWizard()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanFinish.HasValue)
                    result = CurrentPage.CanFinish.Value;
                else
                    result = CanFinish;
            }

            return result;
        }

        private void ExecuteRequestHelp()
        {
            RaiseRoutedEvent(Wizard.HelpEvent);
            if (HelpCommand?.CanExecute(CanHelp) == true)
            {
                HelpCommand?.Execute(CanHelp);
            }
        }

        private bool CanExecuteRequestHelp()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanHelp.HasValue)
                    result = CurrentPage.CanHelp.Value;
                else
                    result = CanHelp;
            }
            return result;
        }

        private void ExecuteSelectNextPage()
        {
            WizardPage nextPage = null;

            if (NextPageCommand?.CanExecute(CanSelectNextPage) == true)
            {
                NextPageCommand?.Execute(CanSelectNextPage);
            }

            if (CurrentPage != null)
            {
                var eventArgs = new RoutedEventArgs(NextEvent);
                this.RaiseEvent(eventArgs);
                //if (eventArgs.Cancel)
                //    return;

                //check next page
                if (CurrentPage.NextPage != null)
                    nextPage = CurrentPage.NextPage;
                else
                {
                    //no next page defined use index
                    var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                    var nextPageIndex = currentIndex + 1;
                    if (nextPageIndex < Items.OfType<WizardPage>().Count())
                        nextPage = Items.OfType<WizardPage>().ToList()[nextPageIndex];
                }
            }

            CurrentPage = nextPage;
        }

        private bool CanExecuteSelectNextPage()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanSelectNextPage.HasValue) //check to see if page has overriden default behavior
                {
                    if (CurrentPage.CanSelectNextPage.Value)
                        result = NextPageExists();
                }
                else if (CanSelectNextPage)
                    result = NextPageExists();
            }
            return result;
        }

        private void ExecuteSelectPreviousPage()
        {
            WizardPage previousPage = null;

            if (PreviousPageCommand?.CanExecute(CanSelectPreviousPage) == true)
            {
                PreviousPageCommand?.Execute(CanSelectPreviousPage);
            }

            if (CurrentPage != null)
            {
                var eventArgs = new RoutedEventArgs(PreviousEvent);
                this.RaiseEvent(eventArgs);
                //if (eventArgs.Cancel)
                //    return;

                //check previous page
                if (CurrentPage.PreviousPage != null)
                    previousPage = CurrentPage.PreviousPage;
                else
                {
                    //no previous page defined so use index
                    var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                    var previousPageIndex = currentIndex - 1;
                    if (previousPageIndex >= 0 && previousPageIndex < Items.OfType<WizardPage>().Count())
                        previousPage = Items.OfType<WizardPage>().ToList()[previousPageIndex];
                }
            }

            CurrentPage = previousPage;
        }

        private bool CanExecuteSelectPreviousPage()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanSelectPreviousPage.HasValue) //check to see if page has overriden default behavior
                {
                    if (CurrentPage.CanSelectPreviousPage.Value)
                        result = PreviousPageExists();
                }
                else if (CanSelectPreviousPage)
                    result = PreviousPageExists();
            }
            return result;
        }

        public static readonly RoutedEvent<RoutedEventArgs> CancelEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(CancelEvent), RoutingStrategies.Bubble);

        public event EventHandler Cancel
        {
            add
            {
                AddHandler(CancelEvent, value);
            }
            remove
            {
                RemoveHandler(CancelEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> PageChangedEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(PageChangedEvent), RoutingStrategies.Bubble);

        public event EventHandler PageChanged
        {
            add
            {
                AddHandler(PageChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PageChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> FinishEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(FinishEvent), RoutingStrategies.Bubble);

        public event EventHandler Finish
        {
            add
            {
                AddHandler(FinishEvent, value);
            }
            remove
            {
                RemoveHandler(FinishEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> HelpEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(HelpEvent), RoutingStrategies.Bubble);

        public event EventHandler Help
        {
            add
            {
                AddHandler(HelpEvent, value);
            }
            remove
            {
                RemoveHandler(HelpEvent, value);
            }
        }

        public delegate void NextRoutedEventHandler(object sender, RoutedEventArgs e);

        public static readonly RoutedEvent<RoutedEventArgs> NextEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(NextEvent), RoutingStrategies.Bubble);

        public event EventHandler Next
        {
            add
            {
                AddHandler(NextEvent, value);
            }
            remove
            {
                RemoveHandler(NextEvent, value);
            }
        }

        public delegate void PreviousRoutedEventHandler(object sender, RoutedEventArgs e);

        public static readonly RoutedEvent<RoutedEventArgs> PreviousEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(PreviousEvent), RoutingStrategies.Bubble);

        public event EventHandler Previous
        {
            add
            {
                AddHandler(PreviousEvent, value);
            }
            remove
            {
                RemoveHandler(PreviousEvent, value);
            }
        }

        private void CloseParentWindow(bool dialogResult)
        {
            Window window = this.TryFindParent<Window>();
            if (window != null)
            {
           
                window.Close();
            }
        }

        private bool NextPageExists()
        {
            bool exists = false;

            if (CurrentPage.NextPage != null) //check to see if a next page has been specified
                exists = true;
            else
            {
                //lets use an index to find the next page
                var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                var nextPageIndex = currentIndex + 1;
                if (nextPageIndex < Items.OfType<WizardPage>().Count())
                    exists = true;
            }

            return exists;
        }

        private bool PreviousPageExists()
        {
            bool exists = false;

            if (CurrentPage.PreviousPage != null) //check to see if a previous page has been specified
                exists = true;
            else
            {
                //lets use an index to find the next page
                var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                var previousPageIndex = currentIndex - 1;
                if (previousPageIndex >= 0 && previousPageIndex < Items.OfType<WizardPage>().Count())
                    exists = true;
            }

            return exists;
        }

        private void RaiseRoutedEvent(RoutedEvent routedEvent)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(routedEvent, this);
            base.RaiseEvent(newEventArgs);
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            btnHelp = e.NameScope.Find<Button>(ButtonName_Help);
            btnBack = e.NameScope.Find<Button>(ButtonName_Back);
            btnNext = e.NameScope.Find<Button>(ButtonName_Next);
            btnFinish = e.NameScope.Find<Button>(ButtonName_Finish);
            btnCancel = e.NameScope.Find<Button>(ButtonName_Cancel);

            btnHelp.Click += BtnHelp_Click;
            btnBack.Click += BtnBack_Click;
            btnNext.Click += BtnNext_Click;
            btnFinish.Click += BtnFinish_Click;
            btnCancel.Click += BtnCancel_Click;
            UpdateButtonState();
            base.OnTemplateApplied(e);
        }

        private void UpdateButtonState()
        {
            if (btnHelp != null)
                btnHelp.IsEnabled = CanExecuteRequestHelp();
            if (btnBack != null)
                btnBack.IsEnabled = CanExecuteSelectPreviousPage();
            if (btnNext != null)
                btnNext.IsEnabled = CanExecuteSelectNextPage();
            if (btnFinish != null)
                btnFinish.IsEnabled = CanExecuteFinishWizard();
            if (btnCancel != null)
                btnCancel.IsEnabled = CanExecuteCancelWizard();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteCancelWizard())
            {
                ExecuteCancelWizard();
            }
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteFinishWizard())
            {
                ExecuteFinishWizard();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteSelectNextPage())
            {
                ExecuteSelectNextPage();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteSelectPreviousPage())
            {
                ExecuteSelectPreviousPage();
            }
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteRequestHelp())
            {
                ExecuteRequestHelp();
            }
        }
    }
}
