using System;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using DynamicData.Kernel;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/xceedsoftware/wpftoolkit

    /// <summary>
    /// WizardPage
    /// </summary>
    public class WizardPage : ContentControl//, IWizardPage
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(WizardPage);

        /// <summary>
        /// get/set IsBackButtonVisible
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
            AvaloniaProperty.Register<WizardPage, bool>(nameof(IsBackButtonVisible),inherits: true);

        /// <summary>
        /// get/set CanCancel
        /// </summary>
        public bool? CanCancel
        {
            get { return (bool?)GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        /// <summary>
        /// <see cref="CanCancel"/>
        /// </summary>
        public static readonly StyledProperty<bool?> CanCancelProperty =
            AvaloniaProperty.Register<WizardPage, bool?>(nameof(CanCancel), inherits: true);

        /// <summary>
        /// get/set IsCancelButtonVisible
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
            AvaloniaProperty.Register<WizardPage, bool>(nameof(IsCancelButtonVisible));

        /// <summary>
        /// get/set CanFinish
        /// </summary>
        public bool? CanFinish
        {
            get { return (bool?)GetValue(CanFinishProperty); }
            set { SetValue(CanFinishProperty, value); }
        }

        /// <summary>
        /// <see cref="CanFinish"/>
        /// </summary>
        public static readonly StyledProperty<bool?> CanFinishProperty =
            AvaloniaProperty.Register<WizardPage, bool?>(nameof(CanFinish), inherits: true);

        /// <summary>
        /// get/set CanHelp
        /// </summary>
        public bool? CanHelp
        {
            get { return (bool?)GetValue(CanHelpProperty); }
            set { SetValue(CanHelpProperty, value); }
        }

        /// <summary>
        /// <see cref="CanHelp"/>
        /// </summary>
        public static readonly StyledProperty<bool?> CanHelpProperty =
            AvaloniaProperty.Register<WizardPage, bool?>(nameof(CanHelp));

        /// <summary>
        /// get/set CanSelectNextPage
        /// </summary>
        public bool? CanSelectNextPage
        {
            get { return (bool?)GetValue(CanSelectNextPageProperty); }
            set { SetValue(CanSelectNextPageProperty, value); }
        }

        /// <summary>
        /// <see cref="CanSelectNextPage"/>
        /// </summary>
        public static readonly StyledProperty<bool?> CanSelectNextPageProperty =
            AvaloniaProperty.Register<WizardPage, bool?>(nameof(CanSelectNextPage));

        /// <summary>
        /// get/sets CanSelectPreviousPage
        /// </summary>
        public bool? CanSelectPreviousPage
        {
            get { return (bool?)GetValue(CanSelectPreviousPageProperty); }
            set { SetValue(CanSelectPreviousPageProperty, value); }
        }

        /// <summary>
        /// <see cref="CanSelectPreviousPage"/>
        /// </summary>
        public static readonly StyledProperty<bool?> CanSelectPreviousPageProperty =
            AvaloniaProperty.Register<WizardPage, bool?>(nameof(CanSelectPreviousPage));

        /// <summary>
        /// get/set Description
        /// </summary>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        /// <summary>
        /// <see cref="Description"/>
        /// </summary>
        public static readonly StyledProperty<string> DescriptionProperty =
            AvaloniaProperty.Register<WizardPage, string>(nameof(Description));

        /// <summary>
        /// get/set ExteriorPanelBackground
        /// </summary>
        public Brush ExteriorPanelBackground
        {
            get { return (Brush)GetValue(ExteriorPanelBackgroundProperty); }
            set { SetValue(ExteriorPanelBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="ExteriorPanelBackground"/>
        /// </summary>
        public static readonly StyledProperty<Brush> ExteriorPanelBackgroundProperty =
            AvaloniaProperty.Register<WizardPage, Brush>(nameof(ExteriorPanelBackground));

        /// <summary>
        /// get/set ExteriorPanelContent
        /// </summary>
        public object ExteriorPanelContent
        {
            get { return (object)GetValue(ExteriorPanelContentProperty); }
            set { SetValue(ExteriorPanelContentProperty, value); }
        }

        /// <summary>
        /// <see cref="ExteriorPanelContent"/>
        /// </summary>
        public static readonly StyledProperty<object> ExteriorPanelContentProperty =
            AvaloniaProperty.Register<WizardPage, object>(nameof(ExteriorPanelContent));

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
            AvaloniaProperty.Register<WizardPage, bool>(nameof(IsFinishButtonVisible));

        /// <summary>
        /// get/set HeaderBackground
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderBackground"/>
        /// </summary>
        public static readonly StyledProperty<Brush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<WizardPage, Brush>(nameof(HeaderBackground));

        /// <summary>
        /// get/sets HeaderImage
        /// </summary>
        public Image HeaderImage
        {
            get { return (Image)GetValue(HeaderImageProperty); }
            set { SetValue(HeaderImageProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderImage"/>
        /// </summary>
        public static readonly StyledProperty<Image> HeaderImageProperty =
            AvaloniaProperty.Register<WizardPage, Image>(nameof(HeaderImage));

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
            AvaloniaProperty.Register<WizardPage, bool>(nameof(IsHelpButtonVisible));

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
            AvaloniaProperty.Register<WizardPage, bool>(nameof(IsNextButtonVisible));

        /// <summary>
        /// get/sets NextPage
        /// </summary>
        public WizardPage NextPage
        {
            get { return (WizardPage)GetValue(NextPageProperty); }
            set { SetValue(NextPageProperty, value); }
        }

        /// <summary>
        /// <see cref="NextPage"/>
        /// </summary>
        public static readonly StyledProperty<WizardPage> NextPageProperty =
            AvaloniaProperty.Register<WizardPage, WizardPage>(nameof(NextPage));

        /// <summary>
        /// get/sets PageType
        /// </summary>
        public WizardPageType PageType
        {
            get { return (WizardPageType)GetValue(PageTypeProperty); }
            set { SetValue(PageTypeProperty, value); }
        }

        /// <summary>
        /// <see cref="PageType"/>
        /// </summary>
        public static readonly StyledProperty<WizardPageType> PageTypeProperty =
            AvaloniaProperty.Register<WizardPage, WizardPageType>(nameof(PageType));

        /// <summary>
        /// get/sets PreviousPage
        /// </summary>
        public WizardPage PreviousPage
        {
            get { return (WizardPage)GetValue(PreviousPageProperty); }
            set { SetValue(PreviousPageProperty, value); }
        }

        /// <summary>
        /// <see cref="PreviousPage"/>
        /// </summary>
        public static readonly StyledProperty<WizardPage> PreviousPageProperty =
            AvaloniaProperty.Register<WizardPage, WizardPage>(nameof(PreviousPage));

        /// <summary>
        /// get/sets Title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// <see cref="Title"/>
        /// </summary>
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<WizardPage, string>(nameof(Title));

        /// <summary>
        /// registers some events
        /// </summary>
        public WizardPage()
        {
            this.Initialized += WizardPage_Initialized;
            this.DataContextChanged += WizardPage_DataContextChanged;
        }

        private void WizardPage_DataContextChanged(object sender, EventArgs e)
        {
            if (this.DataContext is IWizardPage)
            {
                IWizardPage wizardPageVM = this.DataContext as IWizardPage;
                Title = wizardPageVM.Title;
                Description = wizardPageVM.Description;
                PageType = wizardPageVM.PageType;
            }
        }

        private void WizardPage_Initialized(object sender, EventArgs e)
        {
            if (this.IsVisible)
            {
                base.RaiseEvent(new RoutedEventArgs(EnterEvent, this));
            }
        }

        //protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        //{
        //    base.OnPropertyChanged(e);
        //    if ((e.Property.Name == nameof(CanSelectNextPage)) 
        //        || (e.Property.Name == nameof(CanHelp)) 
        //        || (e.Property.Name == nameof(CanFinish))
        //        || (e.Property.Name == nameof(CanCancel)) 
        //        || (e.Property.Name == nameof(CanSelectPreviousPage)))
        //    {
        //        //CommandManager.InvalidateRequerySuggested();
        //    }
        //}

        /// <summary>
        /// <see cref="Enter"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> EnterEvent =
            RoutedEvent.Register<WizardPage, RoutedEventArgs>(nameof(EnterEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Enter event
        /// </summary>
        public event EventHandler Enter
        {
            add
            {
                AddHandler(EnterEvent, value);
            }
            remove
            {
                RemoveHandler(EnterEvent, value);
            }
        }

        /// <summary>
        /// <see cref="Leave"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> LeaveEvent =
            RoutedEvent.Register<WizardPage, RoutedEventArgs>(nameof(LeaveEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Leave event
        /// </summary>
        public event EventHandler Leave
        {
            add
            {
                AddHandler(LeaveEvent, value);
            }
            remove
            {
                RemoveHandler(LeaveEvent, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerEntered(PointerEventArgs e)
        {
            base.OnPointerEntered(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerExited(PointerEventArgs e)
        {
            base.OnPointerExited(e);
        }
    }
}
