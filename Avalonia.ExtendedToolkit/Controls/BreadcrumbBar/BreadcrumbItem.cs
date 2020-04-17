using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// A breadcrumb item that is part of a BreadcrumbBar and contains a BreadcrumbButton and nested child BreadcrumbItems.
    /// </summary>
    [DebuggerDisplay("Header: {Header}")]
    public class BreadcrumbItem : SelectingItemsControl
    {
        private const string partHeader = "PART_Header";
        private const string partSelected = "PART_Selected";

        private Layoutable headerControl;
        private Layoutable selectedControl;

        public Type StyleKey => typeof(BreadcrumbItem);

        /// <summary>
        /// Gets or sets wheter the dropdown button is pressed.
        /// </summary>
        public bool IsDropDownPressed
        {
            get { return (bool)GetValue(IsDropDownPressedProperty); }
            set { SetValue(IsDropDownPressedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsDropDownPressedProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsDropDownPressed));

        /// <summary>
        /// Gets whether the breadcrumb item is overflowed which means it is not
        /// visible in the breadcrumb bar but in the
        /// drop down menu of the breadcrumb bar.
        /// </summary>
        public bool IsOverflow
        {
            get { return (bool)GetValue(IsOverflowProperty); }
            set { SetValue(IsOverflowProperty, value); }
        }

        public static readonly StyledProperty<bool> IsOverflowProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsOverflow));

        /// <summary>
        /// Gets or sets whether the button is visible.
        /// </summary>
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsButtonVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsButtonVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the Image is visible.
        /// </summary>
        public bool IsImageVisible
        {
            get { return (bool)GetValue(IsImageVisibleProperty); }
            set { SetValue(IsImageVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsImageVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsImageVisible));

        /// <summary>
        /// Set to true, to collapse the item if SelectedItem is not null. otherwise false.
        /// </summary>
        public bool IsRoot
        {
            get { return (bool)GetValue(IsRootProperty); }
            set { SetValue(IsRootProperty, value); }
        }

        public static readonly StyledProperty<bool> IsRootProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsRoot));

        /// <summary>
        /// Gets the selected BreadcrumbItem.
        /// </summary>
        public BreadcrumbItem SelectedBreadcrumb
        {
            get { return (BreadcrumbItem)GetValue(SelectedBreadcrumbProperty); }
            set { SetValue(SelectedBreadcrumbProperty, value); }
        }

        public static readonly StyledProperty<BreadcrumbItem> SelectedBreadcrumbProperty =
            AvaloniaProperty.Register<BreadcrumbItem, BreadcrumbItem>(nameof(SelectedBreadcrumb));

        public IDataTemplate OverflowItemTemplate
        {
            get { return (IDataTemplate)GetValue(OverflowItemTemplateProperty); }
            set { SetValue(OverflowItemTemplateProperty, value); }
        }

        public static readonly StyledProperty<IDataTemplate> OverflowItemTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbItem, IDataTemplate>(nameof(OverflowItemTemplate));

        /// <summary>
        /// Gets or sets the image that is used to display this item.
        /// </summary>
        public IBitmap Image
        {
            get { return (IBitmap)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly StyledProperty<IBitmap> ImageProperty =
            AvaloniaProperty.Register<BreadcrumbItem, IBitmap>(nameof(Image));

        /// <summary>
        /// Gets or sets the Trace of the breadcrumb
        /// </summary>
        public object Trace
        {
            get { return (object)GetValue(TraceProperty); }
            set { SetValue(TraceProperty, value); }
        }

        public static readonly StyledProperty<object> TraceProperty =
            AvaloniaProperty.Register<BreadcrumbItem, object>(nameof(Trace));

        /// <summary>
        /// Gets or sets the header for the breadcrumb item.
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly StyledProperty<object> HeaderProperty =
            AvaloniaProperty.Register<BreadcrumbItem, object>(nameof(Header));

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static readonly StyledProperty<DataTemplate> HeaderTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbItem, DataTemplate>(nameof(HeaderTemplate));

        /// <summary>
        /// Gets the parent BreadcrumbBar container.
        /// </summary>
        public BreadcrumbBar BreadcrumbBar
        {
            get
            {
                ILogical current = this;

                while (current != null)
                {
                    current = LogicalExtensions.GetLogicalParent(current);
                    if (current is BreadcrumbBar) return current as BreadcrumbBar;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the parent BreadcrumbItem, otherwise null.
        /// </summary>
        public BreadcrumbItem ParentBreadcrumbItem
        {
            get
            {
                BreadcrumbItem parent = LogicalExtensions.GetLogicalParent(this) as BreadcrumbItem;
                return parent;
            }
        }

        /// <summary>
        /// returns the DataContext if not null
        /// else this is returned
        /// </summary>
        public object Data
        {
            get
            {
                return DataContext != null ? DataContext : this;
            }
        }

        /// <summary>
        /// DataTemplate of the Breadcrumb item
        /// </summary>
        public DataTemplate BreadcrumbItemTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Binding to the Trace property. This is not a dependency property.
        /// </summary>
        public Binding TraceBinding { get; set; }

        /// <summary>
        /// Gets or sets the Binding to the Image property.  This is not a dependency property.
        /// </summary>
        public Binding ImageBinding { get; set; }

        /// <summary>
        /// Gets the Trace string from the Trace property.
        /// </summary>
        public string TraceValue
        {
            get
            {
                XmlNode xml = Trace as XmlNode;
                if (xml != null)
                {
                    return xml.Value;
                }

                if (Trace != null) return Trace.ToString();
                if (Header != null) return Header.ToString();
                return string.Empty;
            }
        }

        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<object>> DropDownPressedChangedEvent =
                    RoutedEvent.Register<BreadcrumbItem, RoutedPropertyChangedEventArgs<object>>(nameof(DropDownPressedChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the IsDropDownPressed property is changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<object> DropDownPressedChanged
        {
            add
            {
                AddHandler(DropDownPressedChangedEvent, value);
            }
            remove
            {
                RemoveHandler(DropDownPressedChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<object>> TraceChangedEvent =
                    RoutedEvent.Register<BreadcrumbItem, RoutedPropertyChangedEventArgs<object>>(nameof(TraceChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the Trace property is changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<object> TraceChanged
        {
            add
            {
                AddHandler(TraceChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TraceChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> OverflowChangedEvent =
                    RoutedEvent.Register<BreadcrumbItem, RoutedEventArgs>(nameof(OverflowChangedEvent), RoutingStrategies.Bubble);

        public event EventHandler OverflowChanged
        {
            add
            {
                AddHandler(OverflowChangedEvent, value);
            }
            remove
            {
                RemoveHandler(OverflowChangedEvent, value);
            }
        }

        public BreadcrumbItem()
        {
            IsDropDownPressedProperty.Changed.AddClassHandler<BreadcrumbItem>((o, e) => DropDownPressedPropertyChanged(o, e));
            IsOverflowProperty.Changed.AddClassHandler<BreadcrumbItem>((o, e) => OverflowPropertyChanged(o, e));
            SelectedBreadcrumbProperty.Changed.AddClassHandler<BreadcrumbItem>((o, e) => SelectedBreadcrumbPropertyChanged(o, e));
            TraceProperty.Changed.AddClassHandler<BreadcrumbItem>((o, e) => TracePropertyChanged(o, e));
            HeaderProperty.Changed.AddClassHandler<BreadcrumbItem>((o, e) => HeaderPropertyChanged(o, e));

            this.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        /// Creates a new BreadcrumbItem out of the specified data.
        /// </summary>
        /// <param name="dataContext">The DataContext for the BreadcrumbItem</param>
        /// <returns>DataContext if dataContext is a Breadcrumbitem, otherwhise a new BreadcrumbItem.</returns>
        public static BreadcrumbItem CreateItem(object dataContext)
        {
            BreadcrumbItem item = dataContext as BreadcrumbItem;
            if (item == null && dataContext != null)
            {
                item = new BreadcrumbItem();
                item.DataContext = dataContext;
            }
            return item;
        }

        /// <summary>
        /// creates the initial Breadcrumb item
        /// </summary>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        internal static BreadcrumbItem CreateInitialItem(object dataContext)
        {
            BreadcrumbItem item = dataContext as BreadcrumbItem;
            if (item == null && dataContext != null)
            {
                item = new BreadcrumbItem();
                item.Items = dataContext as IEnumerable;
            }
            return item;
        }

        private void HeaderPropertyChanged(BreadcrumbItem sender, AvaloniaPropertyChangedEventArgs e)
        {
            BreadcrumbItem item = sender as BreadcrumbItem;
        }

        private void TracePropertyChanged(BreadcrumbItem sender, AvaloniaPropertyChangedEventArgs e)
        {
            BreadcrumbItem item = sender as BreadcrumbItem;

            RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>(e.OldValue, e.NewValue, TraceChangedEvent);
            item.RaiseEvent(args);
        }

        private void SelectedBreadcrumbPropertyChanged(BreadcrumbItem sender, AvaloniaPropertyChangedEventArgs e)
        {
            BreadcrumbItem item = sender as BreadcrumbItem;
            item.OnSelectedBreadcrumbChanged(e.OldValue, e.NewValue);
        }

        /// <summary>
        /// Occurs when the Overflow property is changed.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OverflowPropertyChanged(BreadcrumbItem o, AvaloniaPropertyChangedEventArgs e)
        {
            BreadcrumbItem item = o as BreadcrumbItem;
            item.OnOverflowChanged((bool)e.NewValue);
        }

        /// <summary>
        /// Occurs when the Overflow property is changed.
        /// </summary>
        protected virtual void OnOverflowChanged(bool newValue)
        {
            RoutedEventArgs args = new RoutedEventArgs(OverflowChangedEvent);
            RaiseEvent(args);
        }

        private bool lastValue = false;

        /// <summary>
        /// Occurs when the IsDropDownPressed property is changed.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void DropDownPressedPropertyChanged(BreadcrumbItem o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool)
            {
                bool value = (bool)e.NewValue;
                if (lastValue == value)
                    return;

                BreadcrumbItem item = o as BreadcrumbItem;
                item.OnDropDownPressedChanged();

                lastValue = value;
            }
        }

        /// <summary>
        /// Occurs when the DropDown button is pressed or released.
        /// </summary>
        protected virtual void OnDropDownPressedChanged()
        {
            RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>
                (IsDropDownPressed, IsDropDownPressed, DropDownPressedChangedEvent);
            RaiseEvent(args);
        }

        /// <summary>
        /// Occurs when the selected BreadcrumbItem is changed.
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="newItem"></param>
        protected virtual void OnSelectedBreadcrumbChanged(object oldItem, object newItem)
        {
            if (SelectedBreadcrumb != null)
            {
                SelectedBreadcrumb.SelectedItem = null;
            }
        }

        //protected override bool IsItemItsOwnContainerOverride(object item)
        //{
        //    return item is BreadcrumbItem;
        //}

        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    BreadcrumbItem item = new BreadcrumbItem();
        //    return item;
        //}

        /// <summary>
        /// Perform a special measurement that checks whether to collapse the header.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            if (SelectedItem != null)
            {
                headerControl.IsVisible = true;
                headerControl.Measure(constraint);
                Size size = new Size(constraint.Width - headerControl.DesiredSize.Width, constraint.Height);
                selectedControl.Measure(new Size(double.PositiveInfinity, constraint.Height));
                double width = headerControl.DesiredSize.Width + selectedControl.DesiredSize.Width;
                if (width > constraint.Width || (IsRoot && SelectedItem != null))
                {
                    headerControl.IsVisible = false;
                }
            }
            else if (headerControl != null)
            {
                headerControl.IsVisible = true;
            }
            IsOverflow = headerControl != null ? headerControl.IsVisible != true : false;

            Size result = base.MeasureOverride(constraint);
            return result;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem == null)
            {
                SelectedBreadcrumb = null;
            }
            else
            {
                SelectedBreadcrumb = ContainerFromItem(SelectedItem);
            }
        }

        /// <summary>
        /// Generates a new BreadcrumbItem out of the specified item.
        /// </summary>
        /// <param name="item">The item for which to create a new BreadcrumbItem.</param>
        /// <returns>Item, if item is a BreadcrumbItem, otherwhise a newly created BreadcrumbItem.</returns>
        public BreadcrumbItem ContainerFromItem(object item)
        {
            BreadcrumbItem result = item as BreadcrumbItem;
            if (result == null)
            {
                result = CreateItem(item);
                if (result != null)
                {
                    LogicalChildren.Add(result);
                    result.ApplyTemplate();
                }
            }
            return result;
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            headerControl = e.NameScope.Find(partHeader) as Layoutable;
            selectedControl = e.NameScope.Find(partSelected) as Layoutable;

            ApplyBinding();
        }

        /// <summary>
        ///  Appies the binding to the breadcrumb item.
        /// </summary>
        public void ApplyBinding()
        {
            object item = DataContext;
            if (item == null) return;

            BreadcrumbItem root = this;
            IDataTemplate template = HeaderTemplate;
            //DataTemplateSelector templateSelector = HeaderTemplateSelector;
            //if (templateSelector != null)
            //{
            //    template = templateSelector.SelectTemplate(item, root);
            //}
            if (template == null)
            {
                //DataTemplateKey key = GetResourceKey(item);
                //if (key != null)
                //{
                //    this.TryFindResource(key, template);
                //}
            }

            root.SelectedItem = null;

            TreeDataTemplate hdt = template as TreeDataTemplate;
            if (template != null)
            {
                //root.Header = template.LoadContent();
            }
            else
            {
                root.Header = item;
            }
            root.DataContext = item;

            if (hdt != null)
            {
                // bind the Items to the hierarchical data template:
                root.Bind(BreadcrumbItem.ItemsProperty, hdt.ItemsSource);
            }

            BreadcrumbBar bar = BreadcrumbBar;

            if (bar != null)
            {
                if (TraceBinding == null) TraceBinding = bar.TraceBinding;
                if (ImageBinding == null) ImageBinding = bar.ImageBinding;
            }

            if (TraceBinding != null)
            {
                root.Bind(BreadcrumbItem.TraceProperty, TraceBinding);
            }
            if (ImageBinding != null)
            {
                root.Bind(BreadcrumbItem.ImageProperty, ImageBinding);
            }

            ApplyProperties(item);
        }

        //private static DataTemplateKey GetResourceKey(object item)
        //{
        //    XmlDataProvider xml = item as XmlDataProvider;
        //    DataTemplateKey key;
        //    if (xml != null)
        //    {
        //        key = new DataTemplateKey(xml.XPath);
        //    }
        //    else
        //    {
        //        XmlNode node = item as XmlNode;
        //        if (node != null)
        //        {
        //            key = new DataTemplateKey(node.Name);
        //        }
        //        else
        //        {
        //            key = new DataTemplateKey(item.GetType());
        //        }
        //    }
        //    return key;
        //}

        private void ApplyProperties(object item)
        {
            ApplyPropertiesEventArgs e = new ApplyPropertiesEventArgs
                (item, this, BreadcrumbBar.ApplyPropertiesEvent);
            e.Image = Image;
            e.Trace = Trace;
            e.TraceValue = TraceValue;
            this.RaiseEvent(e);
            Image = e.Image;
            Trace = e.Trace;
        }

        /// <summary>
        /// Gets the string trace that is used to build the path.
        /// </summary>
        /// <returns></returns>
        public string GetTracePathValue()
        {
            ApplyPropertiesEventArgs e = new ApplyPropertiesEventArgs(DataContext, this, BreadcrumbBar.ApplyPropertiesEvent);
            e.Trace = Trace;
            e.TraceValue = TraceValue;
            this.RaiseEvent(e);
            return e.TraceValue;
        }

        /// <summary>
        /// Gets the item that represents the specified trace otherwise null.
        /// </summary>
        /// <param name="trace">The Trace property of the associated BreadcrumbItem.</param>
        /// <returns>An object included in Items, otherwise null.</returns>
        public object GetTraceItem(string trace)
        {
            this.ApplyTemplate();
            foreach (object item in Items)
            {
                BreadcrumbItem bcItem = ContainerFromItem(item);
                if (bcItem != null)
                {
                    ApplyProperties(item);
                    string value = bcItem.TraceValue;
                    if (value != null && value.Equals(trace, StringComparison.InvariantCultureIgnoreCase)) return item;
                }
                else return null;
            }
            return null;
        }

        protected new IAvaloniaList<ILogical> LogicalChildren
        {
            get
            {
                object content = this.SelectedBreadcrumb; ;
                if (content == null)
                {
                    return base.LogicalChildren;
                }

                if (base.TemplatedParent != null)
                {
                    ILogical current = content as ILogical;
                    if (current != null)
                    {
                        ILogical parent = LogicalTree.LogicalExtensions.GetLogicalParent(current);
                        if ((parent != null) && (parent != this))
                        {
                            return base.LogicalChildren;
                        }
                    }
                }

                return new AvaloniaList<ILogical>(SelectedBreadcrumb);
            }
        }
    }
}
