using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class OutlookBar
    {
        private Popup popup;
        private ToggleButton btnMenu;
        private const string partMinimizedButtonContainer = "PART_MinimizedContainer";
        private const string partPopup = "PART_Popup";
        private AvaloniaList<ILogical> _logicalChildren = new AvaloniaList<ILogical>();
        private Control _minimizedButtonContainer;

        //Workaround remember size from ArrangeOverride
        private Size _finalSize = new Size(0, 0);

        private ObservableCollection<OutlookSection> _maximizedSections;
        private ObservableCollection<OutlookSection> _minimizedSections;
        private ObservableCollection<object> _overflowMenu;
        private ObservableCollection<Button> _optionButtons;

        private static readonly FuncTemplate<Panel> DefaultPanel =
            new FuncTemplate<Panel>(() => new VirtualizingStackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch });

        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(OutlookBar);

        /// <summary>
        /// Gets the buttons on the side when IsExpanded is set to
        /// false and ShowSideButtons is set to true.
        /// </summary>
        public ObservableCollection<OutlookSection> SideButtons
        {
            get { return (ObservableCollection<OutlookSection>)GetValue(SideButtonsProperty); }
            private set { SetValue(SideButtonsProperty, value); }
        }

        /// <summary>
        /// <see cref="SideButtons"/>
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<OutlookSection>> SideButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<OutlookSection>>(nameof(SideButtons));

        /// <summary>
        /// get MaximizedSections
        /// </summary>
        public ObservableCollection<OutlookSection> MaximizedSections
        {
            get { return (ObservableCollection<OutlookSection>)GetValue(MaximizedSectionsProperty); }
            private set { SetValue(MaximizedSectionsProperty, value); }
        }

        /// <summary>
        /// <see cref="MaximizedSections"/>
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<OutlookSection>> MaximizedSectionsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<OutlookSection>>(nameof(MaximizedSections));

        /// <summary>
        /// get MinimizedSections
        /// </summary>
        public ObservableCollection<OutlookSection> MinimizedSections
        {
            get { return (ObservableCollection<OutlookSection>)GetValue(MinimizedSectionsProperty); }
            private set { SetValue(MinimizedSectionsProperty, value); }
        }

        /// <summary>
        /// <see cref="MinimizedSections"/>
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<OutlookSection>> MinimizedSectionsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<OutlookSection>>(nameof(MinimizedSections));

        /// <summary>
        /// Gets or sets the default items for the overflow menu.
        /// </summary>
        public ObservableCollection<object> OverflowMenuItems
        {
            get { return (ObservableCollection<object>)GetValue(OverflowMenuItemsProperty); }
            set { SetValue(OverflowMenuItemsProperty, value); }
        }

        /// <summary>
        /// <see cref="OverflowMenuItems"/>
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<object>> OverflowMenuItemsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<object>>(nameof(OverflowMenuItems));

        /// <summary>
        /// get/set OptionButtons
        /// </summary>
        public ObservableCollection<Button> OptionButtons
        {
            get { return (ObservableCollection<Button>)GetValue(OptionButtonsProperty); }
            set { SetValue(OptionButtonsProperty, value); }
        }

        /// <summary>
        /// <see cref="OptionButtons"/>
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<Button>> OptionButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, ObservableCollection<Button>>(nameof(OptionButtons));

        /// <summary>
        /// Gets or sets whether to show the SideButtons when IsExpanded is set to false.
        /// </summary>
        public bool ShowSideButtons
        {
            get { return (bool)GetValue(ShowSideButtonsProperty); }
            set { SetValue(ShowSideButtonsProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowSideButtons"/>
        /// </summary>
        public static readonly StyledProperty<bool> ShowSideButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(ShowSideButtons), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the Outlookbar is Maximized or Minimized.
        /// </summary>
        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            set { SetValue(IsMaximizedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsMaximized"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsMaximizedProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsMaximized), defaultValue: true);

        /// <summary>
        /// Gets or sets the width when IsExpanded is set to false.
        /// </summary>
        public double MinimizedWidth
        {
            get { return (double)GetValue(MinimizedWidthProperty); }
            set { SetValue(MinimizedWidthProperty, value); }
        }

        /// <summary>
        /// <see cref="MinimizedWidth"/>
        /// </summary>
        public static readonly StyledProperty<double> MinimizedWidthProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(MinimizedWidth), defaultValue: 32d);

        /// <summary>
        /// get/sets MaximizedWidth
        /// </summary>
        public double MaximizedWidth
        {
            get { return (double)GetValue(MaximizedWidthProperty); }
            set { SetValue(MaximizedWidthProperty, value); }
        }

        /// <summary>
        /// <see cref="MaximizedWidth"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="DockPosition"/>
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> DockPositionProperty =
            AvaloniaProperty.Register<OutlookBar, HorizontalAlignment>(nameof(DockPosition), defaultValue: HorizontalAlignment.Left);

        /// <summary>
        /// Gets or sets how many buttons are completely visible.
        /// </summary>
        public int MaxNumberOfButtons
        {
            get { return (int)GetValue(MaxNumberOfButtonsProperty); }
            set { SetValue(MaxNumberOfButtonsProperty, value); }
        }

        /// <summary>
        /// <see cref="MaxNumberOfButtons"/>
        /// </summary>
        public static readonly StyledProperty<int> MaxNumberOfButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, int>(nameof(MaxNumberOfButtons), defaultValue: int.MaxValue);

        /// <summary>
        /// Gets or sets whether the popup panel is visible.
        /// </summary>
        public bool IsPopupVisible
        {
            get { return (bool)GetValue(IsPopupVisibleProperty); }
            set { SetValue(IsPopupVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsPopupVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsPopupVisibleProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsPopupVisible), defaultValue: false);

        /// <summary>
        /// Gets or sets the index of the selected section.
        /// </summary>
        public int SelectedSectionIndex
        {
            get { return (int)GetValue(SelectedSectionIndexProperty); }
            set { SetValue(SelectedSectionIndexProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedSectionIndex"/>
        /// </summary>
        public static readonly StyledProperty<int> SelectedSectionIndexProperty =
            AvaloniaProperty.Register<OutlookBar, int>(nameof(SelectedSectionIndex), defaultValue: 0);

        /// <summary>
        /// Gets or sets the selected section.
        /// </summary>
        public OutlookSection SelectedSection
        {
            get { return (OutlookSection)GetValue(SelectedSectionProperty); }
            set { SetValue(SelectedSectionProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedSection"/>
        /// </summary>
        public static readonly StyledProperty<OutlookSection> SelectedSectionProperty =
            AvaloniaProperty.Register<OutlookBar, OutlookSection>(nameof(SelectedSection));

        /// <summary>
        /// Gets the content of the selected section.
        /// </summary>
        internal object SectionContent
        {
            get { return (object)GetValue(SectionContentProperty); }
            set { SetValue(SectionContentProperty, value); }
        }

        /// <summary>
        /// <see cref="SectionContent"/>
        /// </summary>
        public static readonly StyledProperty<object> SectionContentProperty =
            AvaloniaProperty.Register<OutlookBar, object>(nameof(SectionContent));

        /// <summary>
        /// Gets or sets the content for the popup.
        /// </summary>
        internal object CollapsedSectionContent
        {
            get { return (object)GetValue(CollapsedSectionContentProperty); }
            set { SetValue(CollapsedSectionContentProperty, value); }
        }

        /// <summary>
        /// <see cref="CollapsedSectionContent"/>
        /// </summary>
        public static readonly StyledProperty<object> CollapsedSectionContentProperty =
            AvaloniaProperty.Register<OutlookBar, object>(nameof(CollapsedSectionContent));

        /// <summary>
        /// Gets or sets whether the overflow menu of the available sections is visible.
        /// </summary>
        public bool IsOverflowVisible
        {
            get { return (bool)GetValue(IsOverflowVisibleProperty); }
            set { SetValue(IsOverflowVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsOverflowVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsOverflowVisibleProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsOverflowVisible), defaultValue: false);

        /// <summary>
        /// Toggles the IsExpanded property.
        /// </summary>
        public ICommand CollapseCommand
        {
            get; private set;
        }

        /// <summary>
        /// Starts dragging the splitter for the visible section buttons (used for the xaml template).
        /// </summary>
        public ICommand StartDraggingCommand
        {
            get; private set;
        }

        /// <summary>
        /// Shows the popup window.
        /// </summary>
        public ICommand ShowPopupCommand
        {
            get; private set;
        }

        /// <summary>
        /// Start to resize the Width of the OutlookBar
        /// (used for the xaml template to initiate resizing).
        /// </summary>
        public ICommand ResizeCommand
        {
            get; private set;
        }

        /// <summary>
        /// Close the OutlookBar
        /// </summary>
        public ICommand CloseCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the height of the section buttons.
        /// </summary>
        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        /// <summary>
        /// <see cref="ButtonHeight"/>
        /// </summary>
        public static readonly StyledProperty<double> ButtonHeightProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(ButtonHeight), defaultValue: 28.0d);

        /// <summary>
        /// Gets or sets the with of the popup window.
        /// </summary>
        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }

        /// <summary>
        /// <see cref="PopupWidth"/>
        /// </summary>
        public static readonly StyledProperty<double> PopupWidthProperty =
            AvaloniaProperty.Register<OutlookBar, double>(nameof(PopupWidth), defaultValue: (double)double.NaN);

        /// <summary>
        /// Gets or sets whether the splitter for the section buttons is visible
        /// </summary>
        public bool IsButtonSplitterVisible
        {
            get { return (bool)GetValue(IsButtonSplitterVisibleProperty); }
            set { SetValue(IsButtonSplitterVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsButtonSplitterVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsButtonSplitterVisibleProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsButtonSplitterVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the section buttons are visible.
        /// </summary>
        public bool ShowButtons
        {
            get { return (bool)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowButtons"/>
        /// </summary>
        public static readonly StyledProperty<bool> ShowButtonsProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(ShowButtons), defaultValue: true);

        /// <summary>
        /// Gets or sets wether the width of the OutlookBar can be manually resized
        /// by a gripper at the right (or left).
        /// </summary>
        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        /// <summary>
        /// <see cref="CanResize"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanResizeProperty =
            AvaloniaProperty.Register<OutlookBar, bool>(nameof(CanResize), defaultValue: true);

        /// <summary>
        /// Gets or sets wether the close button is visible.
        /// </summary>
        public bool IsCloseButtonVisible
        {
            get { return (bool)GetValue(IsCloseButtonVisibleProperty); }
            set { SetValue(IsCloseButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsCloseButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsCloseButtonVisibleProperty =
           AvaloniaProperty.Register<OutlookBar, bool>(nameof(IsCloseButtonVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets the text or content that is displayed
        /// on the minimized OutlookBar at the Button to open up the Navigation Pane.
        /// </summary>
        public object NavigationPaneText
        {
            get { return (object)GetValue(NavigationPaneTextProperty); }
            set { SetValue(NavigationPaneTextProperty, value); }
        }

        /// <summary>
        /// <see cref="NavigationPaneText"/>
        /// </summary>
        public static readonly StyledProperty<object> NavigationPaneTextProperty =
            AvaloniaProperty.Register<OutlookBar, object>(nameof(NavigationPaneText), defaultValue: "Navigation Pane");

        /// <summary>
        /// for setting the OdcExpander classes
        /// </summary>
        public Classes OdcExpanderClasses
        {
            get { return (Classes)GetValue(OdcExpanderClassesProperty); }
            set { SetValue(OdcExpanderClassesProperty, value); }
        }

        /// <summary>
        /// <see cref="OdcExpanderClasses"/>
        /// </summary>
        public static readonly StyledProperty<Classes> OdcExpanderClassesProperty =
            AvaloniaProperty.Register<OutlookBar, Classes>(nameof(OdcExpanderClasses));

        /// <summary>
        /// for setting the OptionButton classes
        /// </summary>
        public Classes OptionButtonClasses
        {
            get { return (Classes)GetValue(OptionButtonClassesProperty); }
            set { SetValue(OptionButtonClassesProperty, value); }
        }

        /// <summary>
        /// <see cref="OptionButtonClasses"/>
        /// </summary>
        public static readonly StyledProperty<Classes> OptionButtonClassesProperty =
            AvaloniaProperty.Register<OutlookBar, Classes>(nameof(OptionButtonClasses));

        /// <summary>
        /// for setting the OptionToggleButton classes
        /// </summary>
        public Classes OptionToggleButtonClasses
        {
            get { return (Classes)GetValue(OptionToggleButtonClassesProperty); }
            set { SetValue(OptionToggleButtonClassesProperty, value); }
        }

        /// <summary>
        /// <see cref="OptionToggleButtonClasses"/>
        /// </summary>
        public static readonly StyledProperty<Classes> OptionToggleButtonClassesProperty =
            AvaloniaProperty.Register<OutlookBar, Classes>(nameof(OptionToggleButtonClasses));

        /// <summary>
        /// <see cref="Collapsed"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> CollapsedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(CollapsedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs after the OutlookBar has collapsed.
        /// </summary>
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

        /// <summary>
        /// <see cref="Expanded"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ExpandedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(ExpandedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs after the OutlookBar has expanded.
        /// </summary>
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

        /// <summary>
        /// <see cref="PopupOpened"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> PopupOpenedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(PopupOpenedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs after the Popup has opened.
        /// </summary>
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

        /// <summary>
        /// <see cref="PopupClosed"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> PopupClosedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedEventArgs>(nameof(PopupClosedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs after the Popup has closed.
        /// </summary>
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

        /// <summary>
        /// <see cref="SelectedSectionChanged"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<OutlookSection>>
            SelectedSectionChangedEvent =
                    RoutedEvent.Register<OutlookBar, RoutedPropertyChangedEventArgs<OutlookSection>>
            (nameof(SelectedSectionChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the SelectedSection has changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<OutlookSection> SelectedSectionChanged
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
    }
}
