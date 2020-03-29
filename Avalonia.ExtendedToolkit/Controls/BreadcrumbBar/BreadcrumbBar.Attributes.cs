using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// holds the properties and fields for the Breadcrumb Control
    /// </summary>
    public partial class BreadcrumbBar: ContentControl
    {
        private const string partComboBox = "PART_ComboBox";
        private const string partRoot = "PART_Root";

        private ComboBox comboBox;
        private BreadcrumbButton rootButton;

        /// <summary>
        /// On initializing, it is possible that the Path property is set before
        /// the RootItem property, thus the declarative xaml Path would be overwritten by settings the
        /// RootItem property later. To avoid this affect, setting the Path also sets
        /// initPath on initializing and after initializing, the Path is restored by this value:
        /// </summary>
        private string initPath;

        public Type StyleKey => typeof(BreadcrumbBar);

        private ObservableCollection<Button> buttons = new ObservableCollection<Button>();

        /// <summary>
        /// Gets the collection of buttons to appear on the right of the breadcrumb bar.
        /// </summary>
        public ObservableCollection<Button> Buttons
        {
            get { return buttons; }
        }

        /// <summary>
        /// Gets or sets the DropDownItems for the combobox.
        /// </summary>
        public IEnumerable DropDownItems
        {
            get { return comboBoxControlItems.Items; }
        }

        // A helper class to store the DropDownItems since ItemCollection has no public creator:
        private ItemsControl comboBoxControlItems;

        private ObservableCollection<object> traces;

        /// <summary>
        /// Gets or sets wether the root node is removed from the breadcrumb bar if any child node is selected.
        /// This is a dependency property.
        /// </summary>
        public bool HideRootNode
        {
            get { return (bool)GetValue(HideRootNodeProperty); }
            set { SetValue(HideRootNodeProperty, value); }
        }

        public static readonly StyledProperty<bool> HideRootNodeProperty =
            AvaloniaProperty.Register<BreadcrumbBar, bool>(nameof(HideRootNode), defaultValue: true);

        /// <summary>
        /// Gets whether the dropdown has items.
        /// </summary>
        public bool HasDropDownItems
        {
            get { return (bool)GetValue(HasDropDownItemsProperty); }
            private set { SetValue(HasDropDownItemsProperty, value); }
        }

        public static readonly StyledProperty<bool> HasDropDownItemsProperty =
            AvaloniaProperty.Register<BreadcrumbBar, bool>(nameof(HasDropDownItems));

        /// <summary>
        /// Gets or sets the ItemsPanelTemplate for the DropDownItems of the combobox.
        /// </summary>
        public ItemsPanelTemplate DropDownItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(DropDownItemsPanelProperty); }
            set { SetValue(DropDownItemsPanelProperty, value); }
        }

        public static readonly StyledProperty<ItemsPanelTemplate> DropDownItemsPanelProperty =
            AvaloniaProperty.Register<BreadcrumbBar, ItemsPanelTemplate>(nameof(DropDownItemsPanel));

        /// <summary>
        /// Gets whether the selected breadcrumb is the RootItem.
        /// </summary>
        public bool IsRootSelected
        {
            get { return (bool)GetValue(IsRootSelectedProperty); }
            set { SetValue(IsRootSelectedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsRootSelectedProperty =
            AvaloniaProperty.Register<BreadcrumbBar, bool>(nameof(IsRootSelected), defaultValue: true);

        /// <summary>
        /// Gets or sets the DataTemplate for the DropDownItems of the combobox.
        /// </summary>
        public IDataTemplate DropDownItemTemplate
        {
            get { return (IDataTemplate)GetValue(DropDownItemTemplateProperty); }
            set { SetValue(DropDownItemTemplateProperty, value); }
        }

        public static readonly StyledProperty<IDataTemplate> DropDownItemTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbBar, IDataTemplate>(nameof(DropDownItemTemplate));

        /// <summary>
        /// Gets or sets whether the breadcrumb bar can change to edit mode where the path can be edited.
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public static readonly StyledProperty<bool> IsEditableProperty =
            AvaloniaProperty.Register<BreadcrumbBar, bool>(nameof(IsEditable), defaultValue: true);

        /// <summary>
        /// Gets or set the DataTemplate for the OverflowItem.
        /// </summary>
        public IDataTemplate OverflowItemTemplate
        {
            get { return (IDataTemplate)GetValue(OverflowItemTemplateProperty); }
            set { SetValue(OverflowItemTemplateProperty, value); }
        }

        public static readonly StyledProperty<IDataTemplate> OverflowItemTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbBar, IDataTemplate>(nameof(OverflowItemTemplate));

        /// <summary>
        /// Gets the collapsed traces.
        /// </summary>
        public IEnumerable CollapsedTraces
        {
            get { return (IEnumerable)GetValue(CollapsedTracesProperty); }
            set { SetValue(CollapsedTracesProperty, value); }
        }

        public static readonly StyledProperty<IEnumerable> CollapsedTracesProperty =
            AvaloniaProperty.Register<BreadcrumbBar, IEnumerable>(nameof(CollapsedTraces));

        /// <summary>
        /// Gets or sets the root of the breadcrumb which can be a hierarchical data source or a BreadcrumbItem.
        /// </summary>
        public object Root
        {
            get { return (object)GetValue(RootProperty); }
            set { SetValue(RootProperty, value); }
        }

        public static readonly StyledProperty<object> RootProperty =
            AvaloniaProperty.Register<BreadcrumbBar, object>(nameof(Root));

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly StyledProperty<object> SelectedItemProperty =
            AvaloniaProperty.Register<BreadcrumbBar, object>(nameof(SelectedItem));

        /// <summary>
        /// Gets the selected BreadcrumbItem
        /// </summary>
        public BreadcrumbItem SelectedBreadcrumb
        {
            get { return (BreadcrumbItem)GetValue(SelectedBreadcrumbProperty); }
            set { SetValue(SelectedBreadcrumbProperty, value); }
        }

        public static readonly StyledProperty<BreadcrumbItem> SelectedBreadcrumbProperty =
            AvaloniaProperty.Register<BreadcrumbBar, BreadcrumbItem>(nameof(SelectedBreadcrumb));

        /// <summary>
        /// Gets whether the Overflow button is pressed.
        /// </summary>
        public bool IsOverflowPressed
        {
            get { return (bool)GetValue(IsOverflowPressedProperty); }
            set { SetValue(IsOverflowPressedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsOverflowPressedProperty =
            AvaloniaProperty.Register<BreadcrumbBar, bool>(nameof(IsOverflowPressed));

        /// <summary>
        /// Gets the Root BreadcrumbItem.
        /// </summary>
        public BreadcrumbItem RootItem
        {
            get { return (BreadcrumbItem)GetValue(RootItemProperty); }
            set { SetValue(RootItemProperty, value); }
        }

        public static readonly StyledProperty<BreadcrumbItem> RootItemProperty =
            AvaloniaProperty.Register<BreadcrumbBar, BreadcrumbItem>(nameof(RootItem));

        /// <summary>
        /// Gets or sets the Template for an embedded BreadcrumbItem.
        /// </summary>
        public IDataTemplate BreadcrumbItemTemplate
        {
            get { return (IDataTemplate)GetValue(BreadcrumbItemTemplateProperty); }
            set { SetValue(BreadcrumbItemTemplateProperty, value); }
        }

        public static readonly StyledProperty<IDataTemplate> BreadcrumbItemTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbBar, IDataTemplate>(nameof(BreadcrumbItemTemplate));

        /// <summary>
        /// Gets the overflow mode for the Overflow BreadcrumbButton (PART_Root).
        /// </summary>
        public ButtonMode OverflowMode
        {
            get { return (ButtonMode)GetValue(OverflowModeProperty); }
            private set { SetValue(OverflowModeProperty, value); }
        }

        public static readonly StyledProperty<ButtonMode> OverflowModeProperty =
            AvaloniaProperty.Register<BreadcrumbBar, ButtonMode>(nameof(OverflowMode), defaultValue: ButtonMode.Overflow);

        /// <summary>
        /// Gets or sets whether the combobox dropdown is opened.
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public static readonly StyledProperty<bool> IsDropDownOpenProperty =
            AvaloniaProperty.Register<BreadcrumbBar, bool>(nameof(IsDropDownOpen));

        /// <summary>
        /// Gets or sets the string that is used to separate between traces.
        /// </summary>
        public string SeparatorString
        {
            get { return (string)GetValue(SeparatorStringProperty); }
            set { SetValue(SeparatorStringProperty, value); }
        }

        public static readonly StyledProperty<string> SeparatorStringProperty =
            AvaloniaProperty.Register<BreadcrumbBar, string>(nameof(SeparatorString), defaultValue: System.IO.Path.DirectorySeparatorChar.ToString());

        public string PathBinding
        {
            get { return (string)GetValue(PathBindingProperty); }
            set { SetValue(PathBindingProperty, value); }
        }

        public static readonly StyledProperty<string> PathBindingProperty =
            AvaloniaProperty.Register<BreadcrumbBar, string>(nameof(PathBinding));

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly StyledProperty<string> PathProperty =
            AvaloniaProperty.Register<BreadcrumbBar, string>(nameof(Path), defaultValue: string.Empty);

        /// <summary>
        /// Gets or sets the SelectedIndex of the combobox.
        /// </summary>
        public int SelectedDropDownIndex
        {
            get { return (int)GetValue(SelectedDropDownIndexProperty); }
            set { SetValue(SelectedDropDownIndexProperty, value); }
        }

        public static readonly StyledProperty<int> SelectedDropDownIndexProperty =
            AvaloniaProperty.Register<BreadcrumbBar, int>(nameof(SelectedDropDownIndex), defaultValue: -1);

        /// <summary>
        /// Gets or sets the current progress indicator value.
        /// </summary>
        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        public static readonly StyledProperty<double> ProgressValueProperty =
            AvaloniaProperty.Register<BreadcrumbBar, double>(nameof(ProgressValue), defaultValue: 0.0d,
                coerce: (o, e) => { return CoerceProgressValue(o, e); });

        /// <summary>
        /// Gets or sets the maximum progress value.
        /// </summary>
        public double ProgressMaximum
        {
            get { return (double)GetValue(ProgressMaximumProperty); }
            set { SetValue(ProgressMaximumProperty, value); }
        }

        public static readonly StyledProperty<double> ProgressMaximumProperty =
            AvaloniaProperty.Register<BreadcrumbBar, double>(nameof(ProgressMaximum), defaultValue: 100d
                , coerce: (o, e) => { return CoerceProgressMaximum(o, e); });

        /// <summary>
        /// Gets or sets the minimum progess value.
        /// </summary>
        public double ProgressMinimum
        {
            get { return (double)GetValue(ProgressMinimumProperty); }
            set { SetValue(ProgressMinimumProperty, value); }
        }

        public static readonly StyledProperty<double> ProgressMinimumProperty =
            AvaloniaProperty.Register<BreadcrumbBar, double>(nameof(ProgressMinimum), defaultValue: 0.0d
                , coerce: (o, e) => { return CoerceProgressMinimum(o, e); });

        /// <summary>
        /// This command shows the drop down part of the combobox.
        /// </summary>
        public ICommand ShowDropDownCommand { get; private set; }

        /// <summary>
        /// This command selects the BreadcrumbItem that is specified as Parameter.
        /// </summary>
        public ICommand SelectTraceCommand { get; private set; }

        /// <summary>
        /// This command selects the root.
        /// </summary>
        public ICommand SelectRootCommand { get; private set; }

        public static readonly RoutedEvent<RoutedEventArgs> BreadcrumbItemDropDownOpenedEvent =
                    RoutedEvent.Register<BreadcrumbBar, RoutedEventArgs>(nameof(BreadcrumbItemDropDownOpenedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the dropdown of a BreadcrumbItem is opened.
        /// </summary>
        public event EventHandler BreadcrumbItemDropDownOpened
        {
            add
            {
                AddHandler(BreadcrumbItemDropDownOpenedEvent, value);
            }
            remove
            {
                RemoveHandler(BreadcrumbItemDropDownOpenedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> BreadcrumbItemDropDownClosedEvent =
                    RoutedEvent.Register<BreadcrumbBar, RoutedEventArgs>(nameof(BreadcrumbItemDropDownClosedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the dropdown of a BreadcrumbItem is closed.
        /// </summary>
        public event EventHandler BreadcrumbItemDropDownClosed
        {
            add
            {
                AddHandler(BreadcrumbItemDropDownClosedEvent, value);
            }
            remove
            {
                RemoveHandler(BreadcrumbItemDropDownClosedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> ProgressValueChangedEvent =
                    RoutedEvent.Register<BreadcrumbBar, RoutedEventArgs>(nameof(ProgressValueChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the ProgressValue is changed.
        /// </summary>
        public event EventHandler ProgressValueChanged
        {
            add
            {
                AddHandler(ProgressValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ProgressValueChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<ApplyPropertiesEventArgs> ApplyPropertiesEvent =
                    RoutedEvent.Register<BreadcrumbBar, ApplyPropertiesEventArgs>(nameof(ApplyPropertiesEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs after a BreadcrumbItem is created for which to apply additional properties.
        /// </summary>
        public event ApplyPropertiesEventHandler ApplyProperties
        {
            add
            {
                AddHandler(ApplyPropertiesEvent, value);
            }
            remove
            {
                RemoveHandler(ApplyPropertiesEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> SelectedBreadcrumbChangedEvent =
                    RoutedEvent.Register<BreadcrumbBar, RoutedEventArgs>(nameof(SelectedBreadcrumbChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the selected BreadcrumbItem is changed.
        /// </summary>
        public event EventHandler SelectedBreadcrumbChanged
        {
            add
            {
                AddHandler(SelectedBreadcrumbChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedBreadcrumbChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<string>> PathChangedEvent =
                    RoutedEvent.Register<BreadcrumbBar, RoutedPropertyChangedEventArgs<string>>
                    (nameof(PathChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the Path property is changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> PathChanged
        {
            add
            {
                AddHandler(PathChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PathChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<BreadcrumbItemEventArgs> PopulateItemsEvent =
                    RoutedEvent.Register<BreadcrumbBar, BreadcrumbItemEventArgs>(nameof(PopulateItemsEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs before acessing the Items property of a BreadcrumbItem. This event can be used to populate the Items on demand.
        /// </summary>
        public event BreadcrumbItemEventHandler PopulateItems
        {
            add
            {
                AddHandler(PopulateItemsEvent, value);
            }
            remove
            {
                RemoveHandler(PopulateItemsEvent, value);
            }
        }

        public static readonly RoutedEvent<PathConversionEventArgs> PathConversionEvent =
                    RoutedEvent.Register<BreadcrumbBar, PathConversionEventArgs>
            (nameof(PathConversionEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when a path needs to be converted between display path and edit path.
        /// </summary>
        public event PathConversionEventHandler PathConversion
        {
            add
            {
                AddHandler(PathConversionEvent, value);
            }
            remove
            {
                RemoveHandler(PathConversionEvent, value);
            }
        }

        /// <summary>
        /// Gets the number of the first breadcrumb to hide
        /// in the path if descending breadcrumbs are selected.
        /// </summary>
        private int BreadcrumbsToHide
        {
            get { return HideRootNode ? 1 : 0; }
        }

        /// <summary>
        /// Gets or sets the TraceBinding property that will be set to every child BreadcrumbItem.
        /// This is not a dependency property!
        /// </summary>
        public Binding TraceBinding { get; set; }

        /// <summary>
        /// Gets or sets the ImageBinding property that will be set to every child BreadcrumbItem.
        /// This is not a dependency property!
        /// </summary>
        public Binding ImageBinding { get; set; }
    }
}
