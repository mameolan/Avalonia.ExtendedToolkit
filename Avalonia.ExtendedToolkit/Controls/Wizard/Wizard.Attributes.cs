using System;
using System.Collections;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/xceedsoftware/wpftoolkit

    /// <summary>
    /// wizard main control
    /// </summary>
    public partial class Wizard : ItemsControl
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
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(Wizard);

        /// <summary>
        /// get/sets BackButtonContent
        /// </summary>
        public object BackButtonContent
        {
            get { return (object)GetValue(BackButtonContentProperty); }
            set { SetValue(BackButtonContentProperty, value); }
        }

        /// <summary>
        /// <see cref="BackButtonContent"/>
        /// </summary>
        public static readonly StyledProperty<object> BackButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(BackButtonContent), defaultValue: "< Back");

        /// <summary>
        /// get/sets IsBackButtonVisible
        /// </summary>
        public bool IsBackButtonVisible
        {
            get { return (bool)GetValue(IsBackButtonVisibleProperty); }
            set { SetValue(IsBackButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsBackButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsBackButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsBackButtonVisible), defaultValue: true);

        /// <summary>
        /// get/sets CanCancel
        /// </summary>
        public bool CanCancel
        {
            get { return (bool)GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        /// <summary>
        /// <see cref="CanCancel"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanCancelProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanCancel), defaultValue: true);

        /// <summary>
        /// get/sets CancelButtonClosesWindow
        /// </summary>
        public bool CancelButtonClosesWindow
        {
            get { return (bool)GetValue(CancelButtonClosesWindowProperty); }
            set { SetValue(CancelButtonClosesWindowProperty, value); }
        }

        /// <summary>
        /// <see cref="CancelButtonClosesWindow"/>
        /// </summary>
        public static readonly StyledProperty<bool> CancelButtonClosesWindowProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CancelButtonClosesWindow), defaultValue: true);

        /// <summary>
        /// get/sets CancelButtonContent
        /// </summary>
        public object CancelButtonContent
        {
            get { return (object)GetValue(CancelButtonContentProperty); }
            set { SetValue(CancelButtonContentProperty, value); }
        }

        /// <summary>
        /// <see cref="CancelButtonContent"/>
        /// </summary>
        public static readonly StyledProperty<object> CancelButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(CancelButtonContent), defaultValue: "Cancel");

        /// <summary>
        /// get/sets IsCancelButtonVisible
        /// </summary>
        public bool IsCancelButtonVisible
        {
            get { return (bool)GetValue(IsCancelButtonVisibleProperty); }
            set { SetValue(IsCancelButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsCancelButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsCancelButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsCancelButtonVisible), defaultValue: true);

        /// <summary>
        /// get/sets CanFinish
        /// </summary>
        public bool CanFinish
        {
            get { return (bool)GetValue(CanFinishProperty); }
            set { SetValue(CanFinishProperty, value); }
        }

        /// <summary>
        /// <see cref="CanFinish"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanFinishProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanFinish), defaultValue: false);

        /// <summary>
        /// get/sets CanHelp
        /// </summary>
        public bool CanHelp
        {
            get { return (bool)GetValue(CanHelpProperty); }
            set { SetValue(CanHelpProperty, value); }
        }

        /// <summary>
        /// <see cref="CanHelp"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanHelpProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanHelp), defaultValue: true);

        /// <summary>
        /// get/set CanSelectNextPage
        /// </summary>
        public bool CanSelectNextPage
        {
            get { return (bool)GetValue(CanSelectNextPageProperty); }
            set { SetValue(CanSelectNextPageProperty, value); }
        }

        /// <summary>
        /// <see cref="CanSelectNextPage"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanSelectNextPageProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanSelectNextPage), defaultValue: true);

        /// <summary>
        /// get/sets CanSelectPreviousPage
        /// </summary>
        public bool CanSelectPreviousPage
        {
            get { return (bool)GetValue(CanSelectPreviousPageProperty); }
            set { SetValue(CanSelectPreviousPageProperty, value); }
        }

        /// <summary>
        /// <see cref="CanSelectPreviousPage"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanSelectPreviousPageProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(CanSelectPreviousPage), defaultValue: true);

        /// <summary>
        /// get/sets CurrentPage
        /// </summary>
        public WizardPage CurrentPage
        {
            get { return (WizardPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        /// <summary>
        /// <see cref="CurrentPage"/>
        /// </summary>
        public static readonly StyledProperty<WizardPage> CurrentPageProperty =
            AvaloniaProperty.Register<Wizard, WizardPage>(nameof(CurrentPage));

        ///// <summary>
        ///// get/sets CurrentWizardPage
        ///// </summary>
        //public IWizardPage CurrentWizardPage
        //{
        //    get { return (IWizardPage)GetValue(CurrentWizardPageProperty); }
        //    set { SetValue(CurrentWizardPageProperty, value); }
        //}

        ///// <summary>
        ///// <see cref="CurrentWizardPage"/>
        ///// </summary>
        //public static readonly StyledProperty<IWizardPage> CurrentWizardPageProperty =
        //    AvaloniaProperty.Register<Wizard, IWizardPage>(nameof(CurrentWizardPage));

        /// <summary>
        /// get/sets ExteriorPanelMinWidth
        /// </summary>
        public double ExteriorPanelMinWidth
        {
            get { return (double)GetValue(ExteriorPanelMinWidthProperty); }
            set { SetValue(ExteriorPanelMinWidthProperty, value); }
        }

        /// <summary>
        /// <see cref="ExteriorPanelMinWidth"/>
        /// </summary>
        public static readonly StyledProperty<double> ExteriorPanelMinWidthProperty =
            AvaloniaProperty.Register<Wizard, double>(nameof(ExteriorPanelMinWidth), defaultValue: 165.0);

        /// <summary>
        /// get/sets FinishButtonClosesWindow
        /// </summary>
        public bool FinishButtonClosesWindow
        {
            get { return (bool)GetValue(FinishButtonClosesWindowProperty); }
            set { SetValue(FinishButtonClosesWindowProperty, value); }
        }

        /// <summary>
        /// <see cref="FinishButtonClosesWindow"/>
        /// </summary>
        public static readonly StyledProperty<bool> FinishButtonClosesWindowProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(FinishButtonClosesWindow), defaultValue: true);

        /// <summary>
        /// get/sets FinishButtonContent
        /// </summary>
        public object FinishButtonContent
        {
            get { return (object)GetValue(FinishButtonContentProperty); }
            set { SetValue(FinishButtonContentProperty, value); }
        }

        /// <summary>
        /// <see cref="FinishButtonContent"/>
        /// </summary>
        public static readonly StyledProperty<object> FinishButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(FinishButtonContent), defaultValue: "Finish");

        /// <summary>
        /// get/sets IsFinishButtonVisible
        /// </summary>
        public bool IsFinishButtonVisible
        {
            get { return (bool)GetValue(IsFinishButtonVisibleProperty); }
            set { SetValue(IsFinishButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsFinishButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsFinishButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsFinishButtonVisible), defaultValue: false);

        /// <summary>
        /// get/sets HelpButtonContent
        /// </summary>
        public object HelpButtonContent
        {
            get { return (object)GetValue(HelpButtonContentProperty); }
            set { SetValue(HelpButtonContentProperty, value); }
        }

        /// <summary>
        /// <see cref="HelpButtonContent"/>
        /// </summary>
        public static readonly StyledProperty<object> HelpButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(HelpButtonContent), defaultValue: "Help");

        /// <summary>
        /// get/sets IsHelpButtonVisible
        /// </summary>
        public bool IsHelpButtonVisible
        {
            get { return (bool)GetValue(IsHelpButtonVisibleProperty); }
            set { SetValue(IsHelpButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsHelpButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsHelpButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsHelpButtonVisible), defaultValue: true);

        /// <summary>
        /// get/sets NextButtonContent
        /// </summary>
        public object NextButtonContent
        {
            get { return (object)GetValue(NextButtonContentProperty); }
            set { SetValue(NextButtonContentProperty, value); }
        }

        /// <summary>
        /// <see cref="NextButtonContent"/>
        /// </summary>
        public static readonly StyledProperty<object> NextButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(NextButtonContent), defaultValue: "Next >");

        /// <summary>
        /// get/sets IsNextButtonVisible
        /// </summary>
        public bool IsNextButtonVisible
        {
            get { return (bool)GetValue(IsNextButtonVisibleProperty); }
            set { SetValue(IsNextButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsNextButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsNextButtonVisibleProperty =
            AvaloniaProperty.Register<Wizard, bool>(nameof(IsNextButtonVisible), defaultValue: true);

        /// <summary>
        /// get/sets CancelCommand
        /// </summary>
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="CancelCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> CancelCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(CancelCommand));

        /// <summary>
        /// get/sets FinishCommand
        /// </summary>
        public ICommand FinishCommand
        {
            get { return (ICommand)GetValue(FinishCommandProperty); }
            set { SetValue(FinishCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="FinishCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> FinishCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(FinishCommand));

        /// <summary>
        /// get/ sets HelpCommand
        /// </summary>
        public ICommand HelpCommand
        {
            get { return (ICommand)GetValue(HelpCommandProperty); }
            set { SetValue(HelpCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="HelpCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> HelpCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(HelpCommand));

        /// <summary>
        /// get/sets NextPageCommand
        /// </summary>
        public ICommand NextPageCommand
        {
            get { return (ICommand)GetValue(NextPageCommandProperty); }
            set { SetValue(NextPageCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="NextPageCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> NextPageCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(NextPageCommand));

        /// <summary>
        /// get/sets PreviousPageCommand
        /// </summary>
        public ICommand PreviousPageCommand
        {
            get { return (ICommand)GetValue(PreviousPageCommandProperty); }
            set { SetValue(PreviousPageCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="PreviousPageCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> PreviousPageCommandProperty =
            AvaloniaProperty.Register<Wizard, ICommand>(nameof(PreviousPageCommand));

        /// <summary>
        /// <see cref="Cancel"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> CancelEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(CancelEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Cancel event
        /// </summary>
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

        /// <summary>
        /// <see cref="PageChanged"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> PageChangedEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(PageChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// PageChanged event
        /// </summary>
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

        /// <summary>
        /// <see cref="Finish"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> FinishEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(FinishEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Finish event
        /// </summary>
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

        /// <summary>
        /// <see cref="Help"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> HelpEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(HelpEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Help event
        /// </summary>
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

        /// <summary>
        /// <see cref="Next"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> NextEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(NextEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Next event
        /// </summary>
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

        /// <summary>
        /// <see cref="Previous"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> PreviousEvent =
            RoutedEvent.Register<Wizard, RoutedEventArgs>(nameof(PreviousEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Previous event
        /// </summary>
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
