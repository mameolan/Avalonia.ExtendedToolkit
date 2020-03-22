using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Styling;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    public partial class OutlookBar : HeaderedItemsControl
    {
        public Type StyleKey => typeof(OutlookBar);

        private Popup popup;
        private ToggleButton btnMenu;
        private const string partMinimizedButtonContainer = "PART_MinimizedContainer";
        private const string partPopup = "PART_Popup";
        private AvaloniaList<ILogical> _logicalChildren = new AvaloniaList<ILogical>();
        private Control _minimizedButtonContainer;

        //Workaround remember size from ArrangeOverride
        private Size _finalSize = new Size();

        private ObservableCollection<OutlookSection> _maximizedSections;
        private ObservableCollection<OutlookSection> _minimizedSections;
        private ObservableCollection<object> _overflowMenu;
        private ObservableCollection<Button> _optionButtons;

        private static readonly FuncTemplate<IPanel> DefaultPanel =
            new FuncTemplate<IPanel>(() => new VirtualizingStackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch });

        public ObservableCollection<OutlookSection> SideButtons
        {
            get { return (ObservableCollection<OutlookSection>)GetValue(SideButtonsProperty); }
            private set { SetValue(SideButtonsProperty, value); }
        }

        public static readonly StyledProperty<ObservableCollection<OutlookSection>> SideButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<OutlookSection>>(nameof(SideButtons));

        public ObservableCollection<OutlookSection> MaximizedSections
        {
            get { return (ObservableCollection<OutlookSection>)GetValue(MaximizedSectionsProperty); }
            private set { SetValue(MaximizedSectionsProperty, value); }
        }

        public static readonly StyledProperty<ObservableCollection<OutlookSection>> MaximizedSectionsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<OutlookSection>>(nameof(MaximizedSections));

        public ObservableCollection<OutlookSection> MinimizedSections
        {
            get { return (ObservableCollection<OutlookSection>)GetValue(MinimizedSectionsProperty); }
            private set { SetValue(MinimizedSectionsProperty, value); }
        }

        public static readonly StyledProperty<ObservableCollection<OutlookSection>> MinimizedSectionsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<OutlookSection>>(nameof(MinimizedSections));

        public ObservableCollection<object> OverflowMenuItems
        {
            get { return (ObservableCollection<object>)GetValue(OverflowMenuItemsProperty); }
            set { SetValue(OverflowMenuItemsProperty, value); }
        }

        public static readonly StyledProperty<ObservableCollection<object>> OverflowMenuItemsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<object>>(nameof(OverflowMenuItems));

        public ObservableCollection<Button> OptionButtons
        {
            get { return (ObservableCollection<Button>)GetValue(OptionButtonsProperty); }
            set { SetValue(OptionButtonsProperty, value); }
        }

        public static readonly StyledProperty<ObservableCollection<Button>> OptionButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<Button>>(nameof(OptionButtons));

        public bool ShowSideButtons
        {
            get { return (bool)GetValue(ShowSideButtonsProperty); }
            set { SetValue(ShowSideButtonsProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowSideButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(ShowSideButtons), defaultValue: true);

        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            set { SetValue(IsMaximizedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsMaximizedProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsMaximized), defaultValue: true);

        public double MinimizedWidth
        {
            get { return (double)GetValue(MinimizedWidthProperty); }
            set { SetValue(MinimizedWidthProperty, value); }
        }

        public static readonly StyledProperty<double> MinimizedWidthProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(MinimizedWidth), defaultValue: 32d);

        public double MaximizedWidth
        {
            get { return (double)GetValue(MaximizedWidthProperty); }
            set { SetValue(MaximizedWidthProperty, value); }
        }

        public static readonly StyledProperty<double> MaximizedWidthProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(MaximizedWidth), defaultValue: 300d);

        /// <summary>
        /// Gets or sets how to align template of the OutlookBar.
        /// Currently, only Left or Right is supported!
        /// </summary>
        public HorizontalAlignment DockPosition
        {
            get { return (HorizontalAlignment)GetValue(DockPositionProperty); }
            set { SetValue(DockPositionProperty, value); }
        }

        public static readonly StyledProperty<HorizontalAlignment> DockPositionProperty =
            AvaloniaProperty.Register<OutlookBar, HorizontalAlignment>(nameof(DockPosition), defaultValue: HorizontalAlignment.Left);

        public int MaxNumberOfButtons
        {
            get { return (int)GetValue(MaxNumberOfButtonsProperty); }
            set { SetValue(MaxNumberOfButtonsProperty, value); }
        }

        public static readonly StyledProperty<int> MaxNumberOfButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, int>(nameof(MaxNumberOfButtons), defaultValue: int.MaxValue);

        public bool IsPopupVisible
        {
            get { return (bool)GetValue(IsPopupVisibleProperty); }
            set { SetValue(IsPopupVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsPopupVisibleProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsPopupVisible), defaultValue: false);

        public int SelectedSectionIndex
        {
            get { return (int)GetValue(SelectedSectionIndexProperty); }
            set { SetValue(SelectedSectionIndexProperty, value); }
        }

        public static readonly StyledProperty<int> SelectedSectionIndexProperty =
            AvaloniaProperty.Register<OutlookBar, int>(nameof(SelectedSectionIndex), defaultValue: 0);

        public OutlookSection SelectedSection
        {
            get { return (OutlookSection)GetValue(SelectedSectionProperty); }
            set { SetValue(SelectedSectionProperty, value); }
        }

        public static readonly StyledProperty<OutlookSection> SelectedSectionProperty =
            AvaloniaProperty.Register<OutlookBar, OutlookSection>(nameof(SelectedSection));

        internal object SectionContent
        {
            get { return (object)GetValue(SectionContentProperty); }
            set { SetValue(SectionContentProperty, value); }
        }

        public static readonly StyledProperty<object> SectionContentProperty =
            AvaloniaProperty.Register<OutlookBar, object>(nameof(SectionContent));

        internal object CollapsedSectionContent
        {
            get { return (object)GetValue(CollapsedSectionContentProperty); }
            set { SetValue(CollapsedSectionContentProperty, value); }
        }

        public static readonly StyledProperty<object> CollapsedSectionContentProperty =
            AvaloniaProperty.Register<OutlookBar, object>(nameof(CollapsedSectionContent));

        public bool IsOverflowVisible
        {
            get { return (bool)GetValue(IsOverflowVisibleProperty); }
            set { SetValue(IsOverflowVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsOverflowVisibleProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsOverflowVisible), defaultValue: false);

        public ICommand CollapseCommand
        {
            get; private set;
        }

        public ICommand StartDraggingCommand
        {
            get; private set;
        }

        public ICommand ShowPopupCommand
        {
            get; private set;
        }

        public ICommand ResizeCommand
        {
            get; private set;
        }

        public ICommand CloseCommand
        {
            get; private set;
        }

        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        public static readonly StyledProperty<double> ButtonHeightProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(ButtonHeight), defaultValue: 28.0d);

        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }

        public static readonly StyledProperty<double> PopupWidthProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(PopupWidth), defaultValue: (double)double.NaN);

        public bool IsButtonSplitterVisible
        {
            get { return (bool)GetValue(IsButtonSplitterVisibleProperty); }
            set { SetValue(IsButtonSplitterVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsButtonSplitterVisibleProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsButtonSplitterVisible), defaultValue: true);

        public bool ShowButtons
        {
            get { return (bool)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(ShowButtons), defaultValue: true);

        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        public static readonly StyledProperty<bool> CanResizeProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(CanResize), defaultValue: true);

        public bool IsCloseButtonVisible
        {
            get { return (bool)GetValue(IsCloseButtonVisibleProperty); }
            set { SetValue(IsCloseButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsCloseButtonVisibleProperty =
           AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsCloseButtonVisible), defaultValue: true);

        public object NavigationPaneText
        {
            get { return (object)GetValue(NavigationPaneTextProperty); }
            set { SetValue(NavigationPaneTextProperty, value); }
        }

        public static readonly StyledProperty<object> NavigationPaneTextProperty =
            AvaloniaProperty.Register<OutlookBar, object>(nameof(NavigationPaneText), defaultValue: "Navigation Pane");

        public Classes OdcExpanderClasses
        {
            get { return (Classes)GetValue(OdcExpanderClassesProperty); }
            set { SetValue(OdcExpanderClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> OdcExpanderClassesProperty =
            AvaloniaProperty.Register<OutlookBar, Classes>(nameof(OdcExpanderClasses));

        public Classes OptionButtonClasses
        {
            get { return (Classes)GetValue(OptionButtonClassesProperty); }
            set { SetValue(OptionButtonClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> OptionButtonClassesProperty =
            AvaloniaProperty.Register<OutlookBar, Classes>(nameof(OptionButtonClasses));

        public Classes OptionToggleButtonClasses
        {
            get { return (Classes)GetValue(OptionToggleButtonClassesProperty); }
            set { SetValue(OptionToggleButtonClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> OptionToggleButtonClassesProperty =
            AvaloniaProperty.Register<OutlookBar, Classes>(nameof(OptionToggleButtonClasses));

        public static RoutedEvent<RoutedEventArgs> CollapsedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(CollapsedEvent), RoutingStrategies.Bubble);

        public event EventHandler Collapsed
        {
            add
            {
                AddHandler(CollapsedEvent, value);
            }
            remove
            {
                RemoveHandler(CollapsedEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> ExpandedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(ExpandedEvent), RoutingStrategies.Bubble);

        public event EventHandler Expanded
        {
            add
            {
                AddHandler(ExpandedEvent, value);
            }
            remove
            {
                RemoveHandler(ExpandedEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> PopupOpenedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(PopupOpenedEvent), RoutingStrategies.Bubble);

        public event EventHandler PopupOpened
        {
            add
            {
                AddHandler(PopupOpenedEvent, value);
            }
            remove
            {
                RemoveHandler(PopupOpenedEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> PopupClosedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(PopupClosedEvent), RoutingStrategies.Bubble);

        public event EventHandler PopupClosed
        {
            add
            {
                AddHandler(PopupClosedEvent, value);
            }
            remove
            {
                RemoveHandler(PopupClosedEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> SelectedSectionChangedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(SelectedSectionChangedEvent), RoutingStrategies.Bubble);

        public event EventHandler SelectedSectionChanged
        {
            add
            {
                AddHandler(SelectedSectionChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedSectionChangedEvent, value);
            }
        }

        protected new IAvaloniaList<ILogical> LogicalChildren
        {
            get
            {
                if (_logicalChildren.Count > 0)
                    _logicalChildren.Clear();

                _logicalChildren.AddRange(GetLogicalChildren().OfType<ILogical>().ToList());

                return _logicalChildren;
            }
        }

        private IEnumerable<ILogical> GetLogicalChildren()
        {
            foreach (var section in Items) yield return section as ILogical;
            if (SelectedSection != null) yield return SelectedSection.Content as ILogical;
        }

        private void ApplyOverflowMenu()
        {
            ObservableCollection<object> overflowItems = new ObservableCollection<object>();
            if (OverflowMenuItems.Count > 0)
            {
                foreach (object item in OverflowMenuItems)
                {
                    overflowItems.Add(item);
                }
            }

            bool separatorAdded = false;
            int visibleButtons = _maximizedSections.Count + (IsMaximized ? _minimizedSections.Count : 0);

            var items = Items.OfType<OutlookSection>().ToList();
            if (items.Count > 0)
            {
                for (int i = visibleButtons; i < items.Count; i++)
                {
                    OutlookSection section = items[i];

                    if (overflowItems.OfType<MenuItem>().Count(x => x.Tag == section) > 0)
                    {
                        continue;
                    }

                    if (!separatorAdded)
                    {
                        overflowItems.Add(new Separator());
                        separatorAdded = true;
                    }

                    MenuItem item = new MenuItem();
                    item.Width = section.Width;
                    item.Header = section.Header;
                    item.Foreground = new SolidColorBrush(Colors.Red);
                    Image image = new Image();
                    image.Source = section.Image;
                    item.Icon = image;
                    item.Tag = section;
                    item.Click += Item_Click;
                    overflowItems.Add(item);
                }
            }
            //SetValue(OutlookBar.OverflowMenuItemsProperty, overflowItems);

            OverflowMenuItems = overflowItems;
        }

        private int GetNumberOfMinimizedButtons()
        {
            if (_minimizedButtonContainer != null)
            {
                const double width = 32;
                const double overflowWidth = 18;

                var result = _minimizedButtonContainer.DesiredSize;

                double fraction = ((result.Width + width) - overflowWidth) / width;

                int minimizedButtons = (int)Math.Truncate(fraction);
                int visibleButtons = MaxNumberOfButtons + minimizedButtons;
                return visibleButtons;
            }
            return 0;
        }

        public event EventHandler<OverflowMenuCreatedEventArgs> OverflowMenuCreated;

        protected virtual void OnOverflowMenuCreated(Collection<object> menuItems)
        {
            if (OverflowMenuCreated != null)
            {
                OverflowMenuCreatedEventArgs e = new OverflowMenuCreatedEventArgs(menuItems);
                OverflowMenuCreated(this, e);
            }
        }

        private void Item_Click(object sender, Interactivity.RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            OutlookSection section = item.Tag as OutlookSection;
            this.SelectedSection = section;
        }

        private void SectionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ApplySections();
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            if (popup != null)
            {
                popup.Closed -= OnPopupClosed;
                popup.Opened -= OnPopupOpened;
            }
            _minimizedButtonContainer = e.NameScope.Find<Control>(partMinimizedButtonContainer);

            popup = e.NameScope.Find<Popup>(partPopup);
            if (popup != null)
            {
                popup.Closed += new EventHandler(OnPopupClosed);
                popup.Opened += new EventHandler(OnPopupOpened);
            }

            ToggleButton btn = e.NameScope.Find<ToggleButton>("PART_ToggleButton");
            if (btn != null)
            {
                btn.PointerReleased += Btn_PointerReleased;
            }

            btnMenu = e.NameScope.Find<ToggleButton>("toggleMenu");

            btnMenu.PointerLeave += (o, e) =>
            {
                e.Pointer.Capture(null);
            };

            btnMenu.Click += (o, e) =>
            {
                ApplyOverflowMenu();

                if (OverflowMenuItems.Count == 0)
                    return;

                //IsOverflowVisible = !IsOverflowVisible;
                if (btnMenu.ContextMenu.IsOpen == false)
                {
                    btnMenu.ContextMenu.Items = OverflowMenuItems;
                    btnMenu.ContextMenu.Open(this);
                }
                else
                {
                    btnMenu.ContextMenu.Close();
                }
            };

            //Border headerBorder = e.NameScope.Find<Border>("headerBorder");
            //headerBorder.DoubleTapped += (o, e) =>
            //{
            //    IsMaximized = !IsMaximized;
            //};

            e.NameScope.Find<Button>("resizeButton").Click += (o, e) =>
            {
                ResizeCommand.Execute(o);
            };

            e.NameScope.Find<Button>("closeButton").Click += (o, e) =>
            {
                CloseCommand.Execute(o);
            };

            e.NameScope.Find<Button>("toggleMinimizeButton").Click += (o, e) =>
            {
                CollapseCommand.Execute(o);
            };

            e.NameScope.Find<Button>("splitter").Click += (o, e) =>
            {
                StartDraggingCommand.Execute(o);
            };

            base.OnTemplateApplied(e);

            RaisePropertyChanged(OdcExpanderClassesProperty, null, new Data.BindingValue<Classes>(OdcExpanderClasses));
            ApplySections();

            //RaisePropertyChanged(IsOverflowVisibleProperty, !IsOverflowVisible, IsOverflowVisible);
        }

        private void Btn_PointerReleased(object sender, Input.PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton != Input.MouseButton.Left)
                return;

            //          e.Pointer.Capture(null);

            popup.StaysOpen = false;
        }

        protected virtual void OnPopupOpened(object sender, EventArgs e)
        {
            IsPopupVisible = true;
            //Mouse.Capture(this, CaptureMode.SubTree);
        }

        protected virtual void OnPopupClosed(object sender, EventArgs e)
        {
            IsPopupVisible = false;
            //Mouse.Capture(null);
        }

        /// <summary>
        /// Occurs when the IsMaximized property has changed.
        /// </summary>
        /// <param name="isExpanded"></param>
        protected virtual void OnMaximizedChanged(bool isExpanded)
        {
            if (isExpanded) IsPopupVisible = false;
            EnsureSectionContentIsVisible();
            if (isExpanded)
            {
                MaxWidth = MaximizedWidth;
                RaiseEvent(new RoutedEventArgs(ExpandedEvent));
            }
            else
            {
                MaxWidth = MinimizedWidth + (CanResize ? 4 : 0);
                RaiseEvent(new RoutedEventArgs(CollapsedEvent));
            }

            RaisePropertyChanged(IsOverflowVisibleProperty, !IsOverflowVisible, IsOverflowVisible);
        }

        /// <summary>
        /// This code ensures that the section content is visible when the IsExpanded has changed,
        /// since the parent of the content could have changed either.
        /// </summary>
        private void EnsureSectionContentIsVisible()
        {
            object content = SelectedSection != null ? SelectedSection.Content : null;
            SectionContent = null;  // set temporarily to null, so resetting to the current content will have an effect.
            CollapsedSectionContent = IsMaximized ? null : content;
            SectionContent = IsMaximized ? content : null;
        }

        public OutlookBar()
        {
            OverflowMenuItems = _overflowMenu = new ObservableCollection<object>();
            OptionButtons = _optionButtons = new ObservableCollection<Button>();
            _optionButtons.CollectionChanged += OnOptionButtons_CollectionChanged;
            //command bindings missing

            _maximizedSections = new ObservableCollection<OutlookSection>();
            _minimizedSections = new ObservableCollection<OutlookSection>();
            Items = new ObservableCollection<OutlookSection>();
            (Items as ObservableCollection<OutlookSection>).CollectionChanged += new NotifyCollectionChangedEventHandler(SectionsCollectionChanged);

            WidthProperty.Changed.AddClassHandler<OutlookBar>((o, e) => SizeChanged(o, e));
            HeightProperty.Changed.AddClassHandler<OutlookBar>((o, e) => SizeChanged(o, e));

            //Collapsed += (o, e) =>
            //{
            //    IsMaximized = false;
            //};
            //Expanded += (o, e) =>
            //{
            //    IsMaximized = true;
            //};

            IsMaximizedProperty.Changed.AddClassHandler<OutlookBar>((o, e) => MaximizedPropertyChanged(o, e));
            MaxNumberOfButtonsProperty.Changed.AddClassHandler<OutlookBar>((o, e) => MaxNumberOfButtonsChanged(o, e));
            IsPopupVisibleProperty.Changed.AddClassHandler<OutlookBar>((o, e) => IsPopupVisibleChanged(o, e));
            SelectedSectionIndexProperty.Changed.AddClassHandler<OutlookBar>((o, e) => SelectedIndexPropertyChanged(o, e));
            SelectedSectionProperty.Changed.AddClassHandler<OutlookBar>((o, e) => SelectedSectionPropertyChanged(o, e));
            IsOverflowVisibleProperty.Changed.AddClassHandler<OutlookBar>((o, e) => OverflowVisiblePropertyChanged(o, e));

            CollapseCommand = ReactiveCommand.Create<object>(x => CollapseCommandExecuted(x), outputScheduler: RxApp.MainThreadScheduler);
            ShowPopupCommand = ReactiveCommand.Create<object>(x => ShowPopupCommandExecuted(x), outputScheduler: RxApp.MainThreadScheduler);
            ResizeCommand = ReactiveCommand.Create<object>(x => ResizeCommandExecuted(x), outputScheduler: RxApp.MainThreadScheduler);
            CloseCommand = ReactiveCommand.Create<object>(x => CloseCommandExecuted(x), outputScheduler: RxApp.MainThreadScheduler);
            StartDraggingCommand = ReactiveCommand.Create<object>(x => StartDraggingCommandExecuted(x), outputScheduler: RxApp.MainThreadScheduler);

            OdcExpanderClassesProperty.Changed.AddClassHandler<OutlookBar>((o, e) => OnOdcExpanderClassesChanged(o, e));
            OptionButtonClassesProperty.Changed.AddClassHandler<OutlookBar>((o, e) => OnOptionButtonClassesChanged(o, e));
            OptionToggleButtonClassesProperty.Changed.AddClassHandler<OutlookBar>((o, e) => OnOptionToggleButtonClassesChanged(o, e));
        }

        private void OnOptionToggleButtonClassesChanged(OutlookBar o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is Classes)
            {
                foreach (var item in OptionButtons)
                {
                    if ((item is ToggleButton) == false)
                        continue;

                    foreach (var itemClass in (e.NewValue as Classes))
                    {
                        if (item.Classes.Contains(itemClass) == false)
                        {
                            item.Classes.Add(itemClass);
                        }
                    }
                }
            }
        }

        private void OnOptionButtonClassesChanged(OutlookBar o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is Classes)
            {
                foreach (var item in OptionButtons)
                {
                    if (item is ToggleButton)
                        continue;

                    foreach (var itemClass in (e.NewValue as Classes))
                    {
                        if (item.Classes.Contains(itemClass) == false)
                        {
                            item.Classes.Add(itemClass);
                        }
                    }
                }
            }
        }

        private void OnOptionButtons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (OptionButtonClasses != null)
            {
                foreach (Button button in e.NewItems.OfType<Button>())
                {
                    foreach (var itemClass in OptionButtonClasses)
                    {
                        if (button.Classes.Contains(itemClass) == false)
                        {
                            button.Classes.Add(itemClass);
                        }
                    }
                }
            }
        }

        static OutlookBar()
        {
            ItemsPanelProperty.OverrideDefaultValue<OutlookBar>(DefaultPanel);
        }

        private void OnOdcExpanderClassesChanged(OutlookBar o, AvaloniaPropertyChangedEventArgs e)
        {
            if (OdcExpanderClasses != null)
            {
                foreach (var item in Items.OfType<IControl>().SelectMany(x => x.GetLogicalChildren().OfType<OdcExpander>()))
                {
                    foreach (string cls in OdcExpanderClasses.ToList())
                        item.Classes.Add(cls);
                }
            }
        }

        private void OverflowVisiblePropertyChanged(OutlookBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bool newValue = (bool)e.NewValue;
            bar.OnOverflowVisibleChanged(newValue);
        }

        /// <summary>
        /// Occurs when the IsOverflowVisible has changed.
        /// </summary>
        /// <param name="newValue"></param>
        protected virtual void OnOverflowVisibleChanged(bool newValue)
        {
            if (newValue == true)
            {
                ApplyOverflowMenu();
            }
        }

        private void SelectedSectionPropertyChanged(OutlookBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.OnSelectedSectionChanged((OutlookSection)e.NewValue);
        }

        private void SelectedIndexPropertyChanged(OutlookBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.ApplySections();
        }

        private void IsPopupVisibleChanged(OutlookBar o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnPopupVisibleChanged((bool)e.NewValue);
        }

        private void MaxNumberOfButtonsChanged(OutlookBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.ApplySections();
        }

        private void MaximizedPropertyChanged(OutlookBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.OnMaximizedChanged((bool)e.NewValue);
        }

        private void SizeChanged(OutlookBar o, AvaloniaPropertyChangedEventArgs e)
        {
            ApplySections();
        }

        private void CollapseCommandExecuted(object x)
        {
            var temp = IsMaximized;
            IsMaximized ^= true;
        }

        private void ShowPopupCommandExecuted(object x)
        {
            if (!IsMaximized)
            {
                IsPopupVisible = true;
            }
        }

        private void ResizeCommandExecuted(object x)
        {
            Control c = x as Control;
            if (c != null)
            {
                c.PointerReleased += DragMouseLeftButtonUp;
            }

            PointerMoved += PreviewMouseMoveResize;
        }

        private void CloseCommandExecuted(object x)
        {
            this.IsVisible = false;
        }

        private void PreviewMouseMoveResize(object sender, PointerEventArgs e)
        {
            PointerPointProperties properties = e.GetCurrentPoint(this).Properties;

            if (properties.IsLeftButtonPressed == true)
            {
                if (DockPosition == HorizontalAlignment.Left)
                {
                    ResizeFromRight(e);
                }
                else
                {
                    ResizeFromLeft(e);
                }
            }
        }

        private void ResizeFromLeft(Input.PointerEventArgs e)
        {
            Point pos = e.GetPosition(this);
            double w = this.Width - pos.X;

            if (w < 80)
            {
                w = double.NaN;
                IsMaximized = false;
            }
            else
            {
                IsMaximized = true;
            }

            if (MaxWidth != double.NaN && w > MaxWidth)
            {
                w = MaxWidth;
            }

            Width = w;
        }

        private void ResizeFromRight(Input.PointerEventArgs e)
        {
            Point pos = e.GetPosition(this);
            double w = pos.X;

            if (w < 80)
            {
                w = double.NaN;
                IsMaximized = false;
            }
            else
            {
                IsMaximized = true;
            }

            if (MaxWidth != double.NaN && w > MaxWidth)
            {
                w = MaxWidth;
            }

            Width = w;
        }

        private void StartDraggingCommandExecuted(object x)
        {
            Control c = x as Control;
            if (c != null)
            {
                c.PointerReleased += DragMouseLeftButtonUp;
            }

            this.PointerMoved += PreviewMouseMoveButtons;
        }

        /// <summary>
        /// Remove all PreviewMouseMove events from the outlookbar that have been possible set at the beginning of a drag command.
        /// </summary>
        private void DragMouseLeftButtonUp(object sender, Input.PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton != Input.MouseButton.Left)
                return;

            Control c = e.Source as Control;
            if (c != null)
            {
                c.PointerReleased -= DragMouseLeftButtonUp;
            }
            this.PointerMoved -= PreviewMouseMoveButtons;
            this.PointerMoved -= PreviewMouseMoveResize;
        }

        private void PreviewMouseMoveButtons(object sender, PointerEventArgs e)
        {
            var properties = e.GetCurrentPoint(this).Properties;

            if (properties.IsLeftButtonPressed)
            {
                Point pos = e.GetPosition(this);
                double h = _finalSize.Height - 1 - ButtonHeight - pos.Y;
                MaxNumberOfButtons = (int)((double)(h / ButtonHeight));
            }
            else
            {
                this.PointerMoved -= PreviewMouseMoveButtons;
            }
        }

        //protected override Size ArrangeOverride(Size finalSize)
        //{
        //    _finalSize = finalSize;
        //    _minimizedButtonContainer.Arrange(new Rect(finalSize));
        //    return base.ArrangeOverride(finalSize);
        //}

        protected override Size MeasureOverride(Size availableSize)
        {
            _finalSize = availableSize;
            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// Occurs when the SelectedSection has changed.
        /// </summary>
        protected virtual void OnSelectedSectionChanged(OutlookSection newSection)
        {
            var items = Items.OfType<OutlookSection>().ToList();
            for (int index = 0; index < items.Count; index++)
            {
                OutlookSection section = items[index];
                bool selected = newSection == section;
                section.IsSelected = newSection == section;
                if (selected)
                {
                    SelectedSectionIndex = index;
                    SectionContent = IsMaximized ? section.Content : null;
                    CollapsedSectionContent = IsMaximized ? null : section.Content;
                }
            }
            //RaiseEvent(new RoutedPropertyChangedEventArgs<OutlookSection>(oldSection, newSection, SelectedSectionChangedEvent));
            RaiseEvent(new RoutedEventArgs(SelectedSectionChangedEvent));
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if(double.IsNaN(Width)==false)
            {
                MaximizedWidth = Width;
            }

            ApplySections();
        }

        protected virtual void ApplySections()
        {
            if (this.IsInitialized)
            {
                _maximizedSections = new ObservableCollection<OutlookSection>();
                _minimizedSections = new ObservableCollection<OutlookSection>();
                int max = MaxNumberOfButtons;
                int index = 0;
                int selectedIndex = SelectedSectionIndex;
                OutlookSection selectedContent = null;

                int n = GetNumberOfMinimizedButtons();

                foreach (OutlookSection e in Items.OfType<OutlookSection>())
                {
                    ((ISetLogicalParent)e).SetParent(null);
                    e.OutlookBar = this;
                    e.Height = ButtonHeight;
                    if (max-- > 0)
                    {
                        e.IsMaximized = true;
                        _maximizedSections.Add(e);
                    }
                    else
                    {
                        e.IsMaximized = false;
                        if (_minimizedSections.Count < n)
                        {
                            _minimizedSections.Add(e);
                        }
                    }

                    bool selected = index++ == selectedIndex;
                    e.IsSelected = selected;
                    if (selected) selectedContent = e;
                }

                try
                {
                    SetValue(MaximizedSectionsProperty, _maximizedSections);
                }
                catch
                {
                    //already has parent exception
                    InvalidateVisual();
                    InvalidateMeasure();
                    InvalidateArrange();
                }

                try
                {
                    SetValue(MinimizedSectionsProperty, _minimizedSections);
                }
                catch
                {
                    //already has parent exception
                }

                SetValue(SelectedSectionProperty, selectedContent);
            }
        }

        /// <summary>
        /// Occurs when the IsPopupVisible has changed.
        /// </summary>
        /// <param name="isPopupVisible"></param>
        protected virtual void OnPopupVisibleChanged(bool isPopupVisible)
        {
            if (popup != null)
            {
                popup.StaysOpen = true;
                popup.IsOpen = isPopupVisible;
            }
            if (isPopupVisible)
            {
                RaiseEvent(new RoutedEventArgs(PopupOpenedEvent));
            }
            else
            {
                RaiseEvent(new RoutedEventArgs(PopupClosedEvent));
            }
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            var result = new ItemContainerGenerator<OutlookSection>
                (
                    this,
                    OutlookSection.ContentProperty,
                    OutlookSection.ContentTemplateProperty
                );

            return result;
        }
    }
}
