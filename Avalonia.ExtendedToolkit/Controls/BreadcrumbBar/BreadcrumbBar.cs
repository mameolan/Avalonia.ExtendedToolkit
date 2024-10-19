using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// A breadcrumb bar the contains breadcrumb items, a dropdown control, additional buttons and a progress bar.
    /// </summary>
    public partial class BreadcrumbBar : ContentControl, IAddChild
    {
        /// <summary>
        /// initialize events for the breadcrump bar
        /// </summary>
        public BreadcrumbBar()
        {
            RootProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => OnRootPropertyChanged(o, e)));
            SelectedItemProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => OnSelectedItemPropertyChanged(o, e)));
            SelectedBreadcrumbProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => OnSelectedBreadcrumbPropertyChanged(o, e)));
            IsOverflowPressedProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => OverflowPressedChanged(o, e)));
            RootItemProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => OnRootItemPropertyChanged(o, e)));
            IsDropDownOpenProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => IsDropDownOpenChanged(o, e)));
            PathProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => PathPropertyChanged(o, e)));
            PathBindingProperty.Changed.AddClassHandler<BreadcrumbBar>((o, e) => OnPathBindingPropertyChanged(o, e));
            ProgressValueProperty.Changed.AddClassHandler<BreadcrumbBar>(((o, e) => ProgressValuePropertyChanged(o, e)));

            ShowDropDownCommand = ReactiveCommand.Create<object>(o => ShowDropDownExecuted(o), outputScheduler: RxApp.MainThreadScheduler);
            SelectTraceCommand = ReactiveCommand.Create<object>(o => SelectTraceCommandExecuted(o), outputScheduler: RxApp.MainThreadScheduler);
            SelectRootCommand = ReactiveCommand.Create<object>(o => SelectRootCommandExecuted(o), outputScheduler: RxApp.MainThreadScheduler);

            comboBoxControlItems = new ItemsControl();
            Binding b = new Binding("HasItems");
            b.Source = comboBoxControlItems;
            this.Bind(BreadcrumbBar.HasDropDownItemsProperty, b);

            traces = new ObservableCollection<object>();
            CollapsedTraces = traces;

            AddHandler(BreadcrumbItem.SelectionChangedEvent, breadcrumbItemSelectedItemChanged);
            AddHandler(BreadcrumbItem.TraceChangedEvent, breadcrumbItemTraceValueChanged);
            AddHandler(BreadcrumbItem.SelectionChangedEvent, breadcrumbItemSelectionChangedEvent);
            AddHandler(BreadcrumbItem.DropDownPressedChangedEvent, breadcrumbItemDropDownChangedEvent);
            AddHandler(Button.ClickEvent, buttonClickedEvent);
            traces.Add(null);

            this.PointerPressed += OnMouseDown;
        }

        private void breadcrumbItemSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            BreadcrumbItem breadcrumb = e.Source as BreadcrumbItem;
            if (breadcrumb != null && breadcrumb.SelectedBreadcrumb != null) breadcrumb = breadcrumb.SelectedBreadcrumb;
            SelectedBreadcrumb = breadcrumb;

            if (SelectedBreadcrumb != null)
            {
                SelectedItem = SelectedBreadcrumb.Data;
            }
            Path = GetEditPath();
        }

        private void breadcrumbItemTraceValueChanged(object sender, RoutedEventArgs e)
        {
            if (e.Source == RootItem)
            {
                //TODO: This causes Path binding not to work (see PasswordSafe):
                //Path = GetEditPath();
            }
        }

        private void breadcrumbItemSelectionChangedEvent(object sender, RoutedEventArgs e)
        {
            BreadcrumbItem parent = e.Source as BreadcrumbItem;
            if (parent != null && parent.SelectedBreadcrumb != null)
            {
                OnPopulateItems(parent.SelectedBreadcrumb);
            }
        }

        private void breadcrumbItemDropDownChangedEvent(object sender, RoutedEventArgs e)
        {
            BreadcrumbItem breadcrumb = e.Source as BreadcrumbItem;
            if (breadcrumb.IsDropDownPressed)
            {
                OnBreadcrumbItemDropDownOpened(e);
            }
            else
            {
                OnBreadcrumbItemDropDownClosed(e);
            }
        }

        /// <summary>
        /// Remove the focus from a button when it was clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonClickedEvent(object sender, RoutedEventArgs e)
        {
            if (!this.IsDropDownOpen)
            {
                // focus only if it has aleady focus and the button
                // that was pressed is not the drop down button of the combobox:
                if (/*this.IsKeyboardFocusWithin &&*/ (e.Source is BreadcrumbButton))
                {
                    //todo test if enabled
                    //this.Focus();
                }
            }
        }

        /// <summary>
        /// Occurs before acessing the Items property of a BreadcrumbItem. This event can be used to populate the Items on demand.
        /// </summary>
        protected virtual void OnPopulateItems(BreadcrumbItem item)
        {
            BreadcrumbItemEventArgs args = new BreadcrumbItemEventArgs(item, BreadcrumbBar.PopulateItemsEvent);
            RaiseEvent(args);
        }

        /// <summary>
        /// Occurs when the dropdown of a BreadcrumbItem is opened.
        /// </summary>
        protected virtual void OnBreadcrumbItemDropDownOpened(RoutedEventArgs e)
        {
            BreadcrumbItemEventArgs args = new BreadcrumbItemEventArgs(e.Source as BreadcrumbItem, BreadcrumbItemDropDownOpenedEvent);
            RaiseEvent(args);
        }

        /// <summary>
        /// Occurs when the dropdown of a BreadcrumbItem is closed.
        /// </summary>
        protected virtual void OnBreadcrumbItemDropDownClosed(RoutedEventArgs e)
        {
            BreadcrumbItemEventArgs args = new BreadcrumbItemEventArgs(e.Source as BreadcrumbItem, BreadcrumbItemDropDownClosedEvent);
            RaiseEvent(args);
        }

        private void SelectRootCommandExecuted(object sender)
        {
            BreadcrumbItem item = sender as BreadcrumbItem;
            if (item != null)
            {
                item.SelectedItem = null;
            }
        }

        private void SelectTraceCommandExecuted(object sender)
        {
            BreadcrumbItem item = sender as BreadcrumbItem;
            if (item != null)
            {
                item.SelectedItem = null;
            }
        }

        private void ShowDropDownExecuted(object sender)
        {
            BreadcrumbBar bar = sender as BreadcrumbBar;
            if (bar.IsEditable && bar.DropDownItems.OfType<object>().Any())
            {
                bar.IsDropDownOpen = true; 
            }
        }

        private static double CoerceProgressMaximum(AvaloniaObject d, double baseValue)
        {
            BreadcrumbBar bar = d as BreadcrumbBar;
            double value = (double)baseValue;
            if (value < bar.ProgressMinimum) value = bar.ProgressMinimum;
            if (value < bar.ProgressValue) bar.ProgressValue = value;
            if (value < 0) value = 0;

            return value;
        }

        private static double CoerceProgressMinimum(AvaloniaObject d, double baseValue)
        {
            BreadcrumbBar bar = d as BreadcrumbBar;
            double value = (double)baseValue;
            if (value > bar.ProgressMaximum) value = bar.ProgressMaximum;
            if (value > bar.ProgressValue) bar.ProgressValue = value;

            return value;
        }

        private static double CoerceProgressValue(AvaloniaObject o, double baseValue)
        {
            BreadcrumbBar bar = o as BreadcrumbBar;
            double value = (double)baseValue;
            if (value > bar.ProgressMaximum) value = bar.ProgressMaximum;
            if (value < bar.ProgressMinimum) value = bar.ProgressMinimum;

            return value;
        }

        /// <summary>
        /// calls CheckOverflowImage
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = base.ArrangeOverride(finalSize);
            CheckOverflowImage();
            return size;
        }

        /// <summary>
        /// Check what image to display in the drop down button of the overflow button:
        /// </summary>
        private void CheckOverflowImage()
        {
            bool isOverflow = (RootItem != null && RootItem.SelectedBreadcrumb != null && RootItem.SelectedBreadcrumb.IsOverflow);
            OverflowMode = isOverflow ? ButtonMode.Overflow : ButtonMode.Breadcrumb;
        }

        /// <summary>
        /// Build the list of traces for the overflow button.
        /// </summary>
        private void BuildTraces()
        {
            BreadcrumbItem item = RootItem;

            traces.Clear();
            if (item != null && item.IsOverflow)
            {
                foreach (object trace in item.Items)
                {
                    MenuItem menuItem = new MenuItem();//new RibbonMenuItem();
                    menuItem.Tag = trace;
                    BreadcrumbItem bcItem = item.ContainerFromItem(trace);

                    menuItem.Click += menuItem_Click;
                    menuItem.Icon = GetImage(bcItem != null ? bcItem.Image : null);
                    if (trace == RootItem.SelectedItem) menuItem.FontWeight = FontWeight.Bold;
                    traces.Add(menuItem);
                    if (bcItem != null) menuItem.Header = bcItem.TraceValue;
                }
                traces.Insert(0, new Separator());
                MenuItem rootMenuItem = new MenuItem();//new RibbonMenuItem();
                rootMenuItem.Header = item.TraceValue;
                rootMenuItem.Command = SelectRootCommand;
                rootMenuItem.CommandParameter = item;
                rootMenuItem.Icon = GetImage(item.Image);
                traces.Insert(0, rootMenuItem);
            }

            item = item != null ? item.SelectedBreadcrumb : null;

            while (item != null)
            {
                if (!item.IsOverflow) break;
                MenuItem traceMenuItem = new MenuItem();
                traceMenuItem.Header = item.TraceValue;
                traceMenuItem.Command = SelectRootCommand;
                traceMenuItem.CommandParameter = item;
                traceMenuItem.Icon = GetImage(item.Image);
                traces.Insert(0, traceMenuItem);
                item = item.SelectedBreadcrumb;
            }
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            if (RootItem != null && item != null)
            {
                object dataItem = item.Tag;
                if (dataItem != null && dataItem.Equals(RootItem.SelectedItem)) RootItem.SelectedItem = null;
                RootItem.SelectedItem = dataItem;
            }
        }

        private object GetImage(Bitmap imageSource)
        {
            if (imageSource == null) return null;
            Image image = new Image();
            image.Source = imageSource;
            image.Stretch = Stretch.Fill;
            //image.SnapsToDevicePixels = true;
            image.Width = image.Height = 16;

            return image;
        }

        private void OnPathBindingPropertyChanged(BreadcrumbBar o, AvaloniaPropertyChangedEventArgs e)
        {
            o.SetValue(PathProperty, e.NewValue);
        }

        private void ProgressValuePropertyChanged(BreadcrumbBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(BreadcrumbBar.ProgressValueChangedEvent);
            bar.RaiseEvent(args);
        }

        private void PathPropertyChanged(BreadcrumbBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            string newPath = e.NewValue as string;
            //   bar.PathBinding = newPath;

            if (!bar.IsInitialized)
            {
                bar.Path = bar.initPath = newPath;
            }
            else
            {
                bar.BuildBreadcrumbsFromPath(newPath);
                bar.OnPathChanged(e.OldValue as string, newPath);
            }
        }

        /// <summary>
        /// Occurs when the Path property is changed.
        /// </summary>
        protected virtual void OnPathChanged(string oldValue, string newValue)
        {
            BuildBreadcrumbsFromPath(newValue);
            if (this.IsInitialized)//Loaded
            {
                RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(oldValue, newValue, PathChangedEvent);
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// Traces the specified path and builds the associated BreadcrumbItems.
        /// </summary>
        /// <param name="newPath">The traces separated by the SepearatorString property.</param>
        private bool BuildBreadcrumbsFromPath(string newPath)
        {
            PathConversionEventArgs e = new PathConversionEventArgs(ConversionMode.EditToDisplay, newPath, Root, PathConversionEvent);
            RaiseEvent(e);
            newPath = e.DisplayPath;

            BreadcrumbItem item = RootItem;
            if (item == null)
            {
                this.Path = null;
                return false;
            }

            newPath = RemoveLastEmptySeparator(newPath);
            string[] traces = newPath.Split(new string[] { SeparatorString }, StringSplitOptions.RemoveEmptyEntries);
            if (traces.Length == 0) RootItem.SelectedItem = null;
            int index = 0;

            List<int> itemIndex = new List<int>();

            // if the root is specified as first trace, then skip:
            int length = traces.Length;
            int max = BreadcrumbsToHide;
            if (traces.Length > index && max > 0 && traces[index] == (RootItem.TraceValue))
            {
                length--;
                index++;
                max--;
            }

            for (int i = index; i < traces.Length; i++)
            {
                if (item == null) break;

                string trace = traces[i];
                OnPopulateItems(item);
                object next = item.GetTraceItem(trace);
                if (next == null) break;
                itemIndex.Add(item.Items.OfType<object>().ToList().IndexOf(next));
                BreadcrumbItem container = item.ContainerFromItem(next);

                item = container;
            }
            if (length != itemIndex.Count)
            {
                //recover the last path:
                Path = GetDisplayPath();
                return false;
            }

            // temporarily remove the SelectionChangedEvent handler to minimize processing of events while building the breadcrumb items:
            RemoveHandler(BreadcrumbItem.SelectionChangedEvent, breadcrumbItemSelectedItemChanged);
            try
            {
                item = RootItem;
                for (int i = 0; i < itemIndex.Count; i++)
                {
                    if (item == null) break;
                    item.SelectedIndex = itemIndex[i];
                    item = item.SelectedBreadcrumb;
                }
                if (item != null) item.SelectedItem = null;
                SelectedBreadcrumb = item;
                SelectedItem = item != null ? item.Data : null;
            }
            finally
            {
                AddHandler(BreadcrumbItem.SelectionChangedEvent, breadcrumbItemSelectedItemChanged);
            }

            return true;
        }

        /// <summary>
        /// Remove the last separator string from the path if there is no additional trace.
        /// </summary>
        /// <param name="path">The path from which to remove the last separator.</param>
        /// <returns>The path without an unecassary last separator.</returns>
        private string RemoveLastEmptySeparator(string path)
        {
            path = path.Trim();
            int sepLength = SeparatorString.Length;
            if (path.EndsWith(SeparatorString))
            {
                path = path.Remove(path.Length - sepLength, sepLength);
            }
            return path;
        }

        private void IsDropDownOpenChanged(BreadcrumbBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.OnDropDownOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        /// <summary>
        /// Occurs when the IsDropDownOpen property is changed.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnDropDownOpenChanged(bool oldValue, bool newValue)
        {
            if (comboBox != null && newValue)
            {
                SetInputState();
                if (IsEditable)
                {
                    comboBox.IsVisible = true;

                    comboBox.IsDropDownOpen = true;
                }
            }
        }

        private void OnRootItemPropertyChanged(BreadcrumbBar o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnRootItemChanged((BreadcrumbItem)e.OldValue, (BreadcrumbItem)e.NewValue);
        }

        /// <summary>
        /// Occurs when the selected item of an embedded BreadcrumbItem is changed.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnRootItemChanged(BreadcrumbItem oldValue, BreadcrumbItem newValue)
        {
        }

        private void OverflowPressedChanged(BreadcrumbBar bar, object e)
        {
            bar.OnOverflowPressedChanged();
        }

        /// <summary>
        /// Occurs when the IsOverflowPressed property is changed.
        /// </summary>
        protected virtual void OnOverflowPressedChanged()
        {
            // rebuild the list of tracess to show in the popup of the overflow button:
            if (IsOverflowPressed)
            {
                BuildTraces();
            }
        }

        private void OnSelectedBreadcrumbPropertyChanged(BreadcrumbBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            BreadcrumbItem selected = e.NewValue as BreadcrumbItem;
            bar.IsRootSelected = selected == bar.RootItem;
            if (bar.IsInitialized)
            {
                RoutedPropertyChangedEventArgs<BreadcrumbItem> args = new RoutedPropertyChangedEventArgs<BreadcrumbItem>(e.OldValue as BreadcrumbItem, e.NewValue as BreadcrumbItem, BreadcrumbBar.SelectedBreadcrumbChangedEvent);
                bar.RaiseEvent(args);
            }
        }

        private void OnSelectedItemPropertyChanged(BreadcrumbBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.OnSelectedItemChanged(bar, e.OldValue, e.NewValue);
        }

        /// <summary>
        /// Occurs when the selected item of an embedded BreadcrumbItem is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnSelectedItemChanged(BreadcrumbBar sender, object oldvalue, object newValue)
        {
        }

        private void OnRootPropertyChanged(BreadcrumbBar bar, AvaloniaPropertyChangedEventArgs e)
        {
            bar.OnRootChanged(bar, e.OldValue, e.NewValue);
        }

        /// <summary>
        /// Occurs when the Root property is changed.
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnRootChanged(BreadcrumbBar bar, object oldValue, object newValue)
        {
            if (_isInitial == false)
            {
                newValue = GetFirstItem(newValue);
            }
            BreadcrumbItem oldRoot = oldValue as BreadcrumbItem;
            if (oldRoot != null)
            {
                oldRoot.IsRoot = false;
            }

            if (newValue == null)
            {
                RootItem = null;
                Path = null;
            }
            else
            {
                BreadcrumbItem root = newValue as BreadcrumbItem;
                if (root == null)
                {
                    if(_isInitial==false)
                    {
                        root = BreadcrumbItem.CreateItem(newValue);
                    }
                    else
                    {
                        root = BreadcrumbItem.CreateInitialItem(newValue);
                    }
                }
                if (root != null)
                {
                    root.IsRoot = true;
                }
                if (oldValue != null)
                {
                    this.LogicalChildren.Remove((ILogical)oldValue);
                }
                RootItem = root;
                if (root != null)
                {
                    //bool result = LogicalTree.LogicalExtensions.GetLogicalParent(root as ILogical) == null;
                    ////if (LogicalTree.LogicalExtensions.GetLogicalParent(root as ILogical) == null)
                    //{
                    //    this.LogicalChildren.Remove(root);

                    //    this.LogicalChildren.Add(root);
                    //}
                }
                SelectedItem = root != null ? root.DataContext : null;
                if (IsInitialized)
                {
                    SelectedBreadcrumb = root;
                }
                else
                {
                    SelectedBreadcrumb = root;
                }
            }
        }

        /// <summary>
        /// Gets the first item of the specified value if it is a collection, otherwise it returns the value itself.
        /// </summary>
        /// <param name="entity">A collection, otherwise an object.</param>
        /// <returns>The first item of the collection, otherwise the entity.</returns>
        private object GetFirstItem(object entity)
        {
            ICollection c = entity as ICollection;
            if (c != null)
            {
                foreach (object item in c)
                {
                    return item;
                }
            }
            return entity;
        }

        /// <summary>
        /// gets the controls from the styles
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            comboBox = e.NameScope.Find<ComboBox>(partComboBox);
            rootButton = e.NameScope.Find<BreadcrumbButton>(partRoot);

            ToggleButton toggleButton = e.NameScope.Find<ToggleButton>("dropDown");
            //toggleButton.Checked += (o, e) =>
            //{
            //    if (toggleButton.IsChecked == true)
            //    {
            //        IsDropDownOpen = true;
            //    }
            //    else
            //    {
            //        IsDropDownOpen = false;
            //    }

            //};

            if (comboBox != null)
            {
                //comboBox.DropDownClosed += new EventHandler(comboBox_DropDownClosed);
                comboBox.PropertyChanged += ComboBox_PropertyChanged;

                //comboBox.IsKeyboardFocusWithinChanged += new DependencyPropertyChangedEventHandler(comboBox_IsKeyboardFocusWithinChanged);
                comboBox.KeyDown += comboBox_KeyDown;

                PseudoClasses.Set(":comboBoxIsVisible", comboBox.IsVisible);
            }
            if (rootButton != null)
            {
                rootButton.PointerPressed += rootButton_Click;
            }

            BreadcrumbItem breadcrumbItem = Content as BreadcrumbItem;

            _isInitial = true;
            //.orig, think many, f*ing Styled Properties
            //var options = new Optional<object>();
            //SetAndRaise(RootProperty, ref options, new Optional<object>(breadcrumbItem.Items));
            _isInitial = false;

            //RaisePropertyChanged(PathProperty, string.Empty, Path);
        }

        private void rootButton_Click(object sender, PointerPressedEventArgs e)
        {
            SetInputState();
        }

        private bool _oldValue = true;
        private static bool _isInitial;

        private void ComboBox_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (e.Property.Name == nameof(ComboBox.IsVisible))
            {
                PseudoClasses.Set(":comboBoxIsVisible", comboBox.IsVisible);
            }

            if (e.Property.Name == nameof(ComboBox.IsDropDownOpen))
            {
                if (_oldValue == comboBox.IsDropDownOpen)
                    return;

                if (comboBox.IsDropDownOpen == false)
                {
                    comboBox_DropDownClosed(comboBox, EventArgs.Empty);
                }
                else
                {
                }

                _oldValue = comboBox.IsDropDownOpen;
            }
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            IsDropDownOpen = false;
            Path = comboBox.SelectedItem.ToString();//todo check
        }

        private void comboBox_KeyDown(object sender, Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Input.Key.Escape: Exit(false); break;
                case Input.Key.Enter: Exit(true); break;
                default: return;
            }
            e.Handled = true;
        }

        private void OnMouseDown(object sender, PointerPressedEventArgs e)
        {
            if (e.Handled) return;

            var prop = e.GetCurrentPoint(this).Properties;

            if (prop.IsLeftButtonPressed)
            {
                e.Handled = true;
                SetInputState();
            }
        }

        private void SetInputState()
        {
            if (comboBox != null && IsEditable)
            {
                //todo check
                //comboBox.Text = Path;

                comboBox.IsVisible = true;
                comboBox.Focus();
            }
        }

        /// <summary>
        /// Gets the edit path from the tracess of the BreacrumbItems.
        /// </summary>
        /// <returns></returns>
        public string GetEditPath()
        {
            string displayPath = GetDisplayPath();
            PathConversionEventArgs e = new PathConversionEventArgs(ConversionMode.DisplayToEdit, displayPath, Root, PathConversionEvent);
            RaiseEvent(e);
            return e.EditPath;
        }

        /// <summary>
        /// Gets the path of the specified BreadcrumbItem.
        /// </summary>
        /// <param name="item">The BreadrumbItem for which to determine the path.</param>
        /// <returns>The path of the BreadcrumbItem which is the concenation of all Traces from all selected breadcrumbs.</returns>
        public string PathFromBreadcrumbItem(BreadcrumbItem item)
        {
            StringBuilder sb = new StringBuilder();
            while (item != null)
            {
                if (item == RootItem && sb.Length > 0) break;
                if (sb.Length > 0) sb.Insert(0, SeparatorString);
                sb.Insert(0, item.TraceValue);
                item = item.ParentBreadcrumbItem;
            }
            PathConversionEventArgs e = new PathConversionEventArgs(ConversionMode.DisplayToEdit, sb.ToString(), Root, PathConversionEvent);
            RaiseEvent(e);
            return e.EditPath;
        }

        /// <summary>
        /// Gets the display path from the traces of the BreacrumbItems.
        /// </summary>
        /// <returns></returns>
        public string GetDisplayPath()
        {
            string separator = SeparatorString;
            StringBuilder sb = new StringBuilder();
            BreadcrumbItem item = RootItem;
            int index = 0;
            while (item != null)
            {
                if (sb.Length > 0) sb.Append(separator);
                if (index >= BreadcrumbsToHide || item.SelectedItem == null)
                {
                    sb.Append(item.GetTracePathValue());
                }
                index++;
                item = item.SelectedBreadcrumb;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Do what's necassary to do when the BreadcrumbBar has lost focus.
        /// </summary>
        private void Exit(bool updatePath)
        {
            if (comboBox != null)
            {
                if (updatePath && comboBox.IsVisible)
                {
                    Path = comboBox.SelectedItem.ToString();
                }
                comboBox.IsVisible = false;
            }
        }

        /// <summary>
        /// sets the root by value
        /// </summary>
        /// <param name="value"></param>
        public void AddChild(object value)
        {
            this.Root = value;
        }

        /// <summary>
        /// sets the root text
        /// </summary>
        /// <param name="text"></param>
        public void AddText(string text)
        {
            AddChild(text);
        }
    }
}
