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
    /// A breadcrumb item that is part of a BreadcrumbBar and
    /// contains a BreadcrumbButton and nested child BreadcrumbItems.
    /// </summary>
    [DebuggerDisplay("Header: {Header}")]
    public partial class  BreadcrumbItem : SelectingItemsControl
    {
        /// <summary>
        /// registered some listeners
        /// </summary>
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

        /// <summary>
        /// gets some controls from the style
        /// calls <see cref="ApplyBinding"/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

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

        /// <summary>
        /// if the SelectedBreadcrumb is null returns base.LogicalChildren
        /// if TemplatedParent is not null parent logical children
        /// else SelectedBreadcrumb as logicals is returned
        /// </summary>
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
