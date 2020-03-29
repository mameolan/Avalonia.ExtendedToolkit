using System;
using System.Collections;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/xceedsoftware/wpftoolkit

    public partial class Wizard: ItemsControl
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

        public Type StyleKey => typeof(Wizard);

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
    }
}
