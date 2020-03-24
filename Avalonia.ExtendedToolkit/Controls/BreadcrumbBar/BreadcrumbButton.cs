using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.VisualTree;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// A breadcrumb button is part of a BreadcrumbItem and contains  a header and a dropdown button.
    /// </summary>
    public class BreadcrumbButton : HeaderedItemsControl
    {
        private ContextMenu contextMenu;
        private Control dropDownBtn;
        private Control dropPanel;
        private bool isPressed = false;

        private const string partMenu = "PART_Menu";
        private const string partToggle = "PART_Toggle";
        private const string partButton = "PART_button";
        private const string partDropDown = "PART_DropDown";

        public Type StyleKey => typeof(BreadcrumbButton);

        public ICommand OpenOverflowCommand { get; private set; }

        public ICommand SelectCommand { get; private set; }

        /// <summary>
        /// Gets or sets the Image of the BreadcrumbButton.
        /// </summary>
        public IImage Image
        {
            get { return (IImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly StyledProperty<IImage> ImageProperty =
            AvaloniaProperty.Register<BreadcrumbButton, IImage>(nameof(Image));

        public bool HasImage
        {
            get { return (bool)GetValue(HasImageProperty); }
            set { SetValue(HasImageProperty, value); }
        }

        public static readonly StyledProperty<bool> HasImageProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(HasImage));

        /// <summary>
        /// Gets or sets the selectedItem.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly StyledProperty<object> SelectedItemProperty =
            AvaloniaProperty.Register<BreadcrumbButton, object>(nameof(SelectedItem));

        /// <summary>
        /// Gets or sets the ButtonMode for the BreadcrumbButton.
        /// </summary>
        public ButtonMode Mode
        {
            get { return (ButtonMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly StyledProperty<ButtonMode> ModeProperty =
            AvaloniaProperty.Register<BreadcrumbButton, ButtonMode>(nameof(Mode), defaultValue: ButtonMode.Breadcrumb);

        /// <summary>
        /// Gets or sets whether the button is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsPressedProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsPressed));

        /// <summary>
        /// Gets or sets whether the drop down button is pressed.
        /// </summary>
        public bool IsDropDownPressed
        {
            get { return (bool)GetValue(IsDropDownPressedProperty); }
            set { SetValue(IsDropDownPressedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsDropDownPressedProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsDropDownPressed));

        /// <summary>
        /// Gets or sets whether the drop down button is visible.
        /// </summary>
        public bool IsDropDownVisible
        {
            get { return (bool)GetValue(IsDropDownVisibleProperty); }
            set { SetValue(IsDropDownVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsDropDownVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsDropDownVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets the DataTemplate for the drop down items.
        /// </summary>
        public DataTemplate DropDownContentTemplate
        {
            get { return (DataTemplate)GetValue(DropDownContentTemplateProperty); }
            set { SetValue(DropDownContentTemplateProperty, value); }
        }

        public static readonly StyledProperty<DataTemplate> DropDownContentTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbButton, DataTemplate>(nameof(DropDownContentTemplate));

        /// <summary>
        /// Gets or sets whether the button is visible.
        /// </summary>
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsButtonVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsButtonVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the Image is visible
        /// </summary>
        public bool IsImageVisible
        {
            get { return (bool)GetValue(IsImageVisibleProperty); }
            set { SetValue(IsImageVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsImageVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsImageVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether to use visual background style on MouseOver and/or MouseDown.
        /// </summary>
        public bool EnableVisualButtonStyle
        {
            get { return (bool)GetValue(EnableVisualButtonStyleProperty); }
            set { SetValue(EnableVisualButtonStyleProperty, value); }
        }

        public static readonly StyledProperty<bool> EnableVisualButtonStyleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(EnableVisualButtonStyle), defaultValue: true);

        /// <summary>
        /// returns true if items.count > 0
        /// </summary>
        public bool HasItems
        {
            get { return (bool)GetValue(HasItemsProperty); }
            private set { SetValue(HasItemsProperty, value); }
        }

        public static readonly StyledProperty<bool> HasItemsProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(HasItems));

        public static readonly RoutedEvent<RoutedEventArgs> SelectedItemChangedEvent =
                    RoutedEvent.Register<BreadcrumbButton, RoutedEventArgs>(nameof(OnSelectedItemChanged), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the SelectedItem is changed.
        /// </summary>
        public event EventHandler SelectedItemChanged
        {
            add
            {
                AddHandler(SelectedItemChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedItemChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
                    RoutedEvent.Register<BreadcrumbButton, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the Button is clicked.
        /// </summary>
        public event EventHandler Click
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        public BreadcrumbButton()
        {
            ImageProperty.Changed.AddClassHandler<BreadcrumbButton>((o, e) => OnImageChanged(o, e));
            ItemsProperty.Changed.AddClassHandler<BreadcrumbButton>((o, e) => OnItemsCollectionChanged(o, e));

            SelectedItemProperty.Changed.AddClassHandler<BreadcrumbButton>(((o, e) => OnSelectedItemChanged(o, e)));
            IsDropDownPressedProperty.Changed.AddClassHandler<BreadcrumbButton>(((o, e) => OverflowPressedChanged(o, e)));

            SelectCommand = ReactiveCommand.Create<object>((o) => SelectCommandExecuted(o), outputScheduler: RxApp.MainThreadScheduler);

            var openOverflowCommandCanExecute = this.WhenAny(x => x, (o) => ItemCount > 0);
            OpenOverflowCommand = ReactiveCommand.Create<object>((o) => OpenOverflowCommandExecuted(o)
                , canExecute: openOverflowCommandCanExecute
                , outputScheduler: RxApp.MainThreadScheduler);

            this.KeyBindings.Add(new KeyBinding() { Command = SelectCommand, Gesture = new KeyGesture(Key.Enter) });
            this.KeyBindings.Add(new KeyBinding() { Command = SelectCommand, Gesture = new KeyGesture(Key.Space) });

            this.KeyBindings.Add(new KeyBinding() { Command = OpenOverflowCommand, Gesture = new KeyGesture(Key.Down) });
            this.KeyBindings.Add(new KeyBinding() { Command = OpenOverflowCommand, Gesture = new KeyGesture(Key.Up) });
        }

        private void OnItemsCollectionChanged(BreadcrumbButton o, AvaloniaPropertyChangedEventArgs e)
        {
            HasItems = Items == null ? false : Items.OfType<object>().Count() > 0;
        }

        private void OnImageChanged(BreadcrumbButton o, AvaloniaPropertyChangedEventArgs e)
        {
            HasImage = e.NewValue is IImage;
        }

        protected override void ItemsChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.ItemsChanged(e);

            if (contextMenu != null && e.NewValue != null)
            {
                var currentItems = Items.OfType<object>().Where(x => x != null).ToList();

                //dropPanel.ContextMenu.IsVisible = true;

                if (currentItems.Count == 0)
                {
                    //    dropPanel.ContextMenu.IsVisible = false;
                    return;
                }

                contextMenu.ItemTemplate = ItemTemplate;

                //if (contextMenu.IsOpen)
                //    contextMenu.Close();

                var items = new AvaloniaList<object>();
                contextMenu.Items = new AvaloniaList<object>();

                foreach (object item in Items)
                {
                    if (item == null)
                        continue;

                    if (!(item is MenuItem) && !(item is Separator))
                    {
                        items.Add(CreateMenuItem(item));
                    }
                    else
                    {
                        if (item is Separator)
                        {
                            items.Add(new Separator());
                        }
                        else if (item is MenuItem)
                        {
                            MenuItem originalMenuItem = item as MenuItem;
                            originalMenuItem.Click -= item_Click;
                            Image image = originalMenuItem.Icon as Image;
                            MenuItem menuItem = new MenuItem();
                            menuItem.DataContext = originalMenuItem.DataContext;
                            menuItem.Header = originalMenuItem.Header;
                            menuItem.Icon = image;
                            menuItem.Click += item_Click;
                            if (menuItem.DataContext != null && menuItem.DataContext.Equals(SelectedItem))
                            {
                                menuItem.FontWeight = FontWeight.Bold;
                            }
                            menuItem.ItemTemplate = ItemTemplate;
                            items.Add(menuItem);
                        }
                        else
                        {
                            Debug.WriteLine("Error [contextMenu_Opened]: item is not a MenuItem or Seperator");
                        }
                    }
                }

                contextMenu.Items = items;

                //if(contextMenuWasOpen)
                //{
                //    contextMenu.Open(dropPanel);
                //}
            }
        }

        private void OverflowPressedChanged(BreadcrumbButton o, AvaloniaPropertyChangedEventArgs e)
        {
            //todo check
            if (contextMenu != null)
            {
                if (contextMenu.Items.OfType<object>().Count() == 0)
                {
                    IsDropDownPressed = false;
                    return;
                    //contextMenu.Items = new AvaloniaList<object>();
                }

                bool val = (bool)e.NewValue;

                //if (contextMenu.IsOpen)
                //{
                //    contextMenu.Close();
                //}

                if (val)
                {
                    if (contextMenu.ItemCount > 0)
                    {
                        try
                        {
                            //contextMenu.Focus();
                            contextMenu.Open(dropDownBtn);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    contextMenu.Close();
                }
            }

            o.OnOverflowPressedChanged();
        }

        protected virtual void OnOverflowPressedChanged()
        {
        }

        private void OpenOverflowCommandExecuted(object o)
        {
            IsDropDownPressed = true;
        }

        private void SelectCommandExecuted(object o)
        {
            SelectedItem = null;
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent);
            RaiseEvent(args);
        }

        protected override void OnPointerLeave(PointerEventArgs e)
        {
            IsPressed = false;
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            var prop = e.GetCurrentPoint(this).Properties;

            if (prop.IsLeftButtonPressed)
            {
                e.Handled = true;
                IsPressed = isPressed = true;
            }

            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            e.Handled = true;
            if (isPressed)
            {
                RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
                RaiseEvent(args);
                SelectCommand.Execute(this);
            }
            IsPressed = isPressed = false;

            base.OnPointerReleased(e);
        }

        private void OnSelectedItemChanged(BreadcrumbButton button, object e)
        {
            if (button.IsInitialized)
            {
                RoutedEventArgs args = new RoutedEventArgs(SelectedItemChangedEvent);
                button.RaiseEvent(args);
            }
        }

        protected override void OnPointerEnter(PointerEventArgs e)
        {
            var prop = e.GetCurrentPoint(this).Properties;

            isPressed = prop.IsLeftButtonPressed;
            IVisual parent = TemplatedParent as IVisual;
            while (parent != null && !(parent is BreadcrumbBar))
            {
                parent = VisualTree.VisualExtensions.GetVisualParent(parent);
            }

            BreadcrumbBar bar = parent as BreadcrumbBar;
            if (bar != null/* && bar.IsKeyboardFocusWithin*/)  //?
            {
                Focus();
            }

            IsPressed = isPressed;

            base.OnPointerEnter(e);
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            dropDownBtn = e.NameScope.Find<Control>(partDropDown);

            dropPanel = e.NameScope.Find<Control>("borderBtn");

            contextMenu = e.NameScope.Find<ContextMenu>(partMenu);
            if (contextMenu != null)
            {
                //contextMenu.Opened += new RoutedEventHandler(contextMenu_Opened);
                contextMenu.PropertyChanged += ContextMenu_PropertyChanged;
                //contextMenu.Items = Items;
            }
            if (dropDownBtn != null)
            {
                dropDownBtn.PointerPressed += dropDownBtn_MouseDown;// += new MouseButtonEventHandler(dropDownBtn_MouseDown);
            }

            base.OnTemplateApplied(e);

            RaisePropertyChanged(ItemsProperty, null, new Data.BindingValue<IEnumerable>(Items));
        }

        private void dropDownBtn_MouseDown(object sender, EventArgs e)
        {
            if (contextMenu.ItemCount == 0)
                return;

            //e.Handled = true;
            IsDropDownPressed ^= true;

            //if (contextMenu.IsOpen)
            //    contextMenu.Close();

            try
            {
                contextMenu.Open(dropPanel);
            }
            catch
            {
                //already parent exception
            }
        }

        private void ContextMenu_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(ContextMenu.IsOpen))
            {
                ContextMenu contextMenu = sender as ContextMenu;

                var parent = VisualTree.VisualExtensions.GetVisualParent<BreadcrumbBar>(this.Parent);

                if (contextMenu.IsOpen)
                {
                    //contextMenu.SelectedItem = contextMenu.Items.OfType<object>().FirstOrDefault();
                    //contextMenu_Opened(sender, new RoutedEventArgs());
                    //contextMenu.Focus();
                }
                else
                {
                    //contextMenu.Focusable = false;
                }
            }
        }

        //TODO: Menu needs too long to render if there are too many items (> 20000).
        private void contextMenu_Opened(object sender, RoutedEventArgs e)
        {
            contextMenu.Items.OfType<object>().ToList().Clear();
            contextMenu.ItemTemplate = ItemTemplate;
            //contextMenu.ItemTemplateSelector = ItemTemplateSelector;

            var items = new AvaloniaList<object>(); //contextMenu.Items.OfType<object>().ToList();

            var currentItems = Items.OfType<object>().ToList();

            //foreach (object item in Items)
            //{
            //    if (!(item is MenuItem) && !(item is Separator))
            //    {
            //        items.Add(CreateMenuItem(item));
            //    }
            //    else
            //    {
            //        if (item is Separator)
            //        {
            //            items.Add(new Separator());
            //        }
            //        else if (item is MenuItem)
            //        {
            //            MenuItem originalMenuItem = item as MenuItem;
            //            originalMenuItem.Click -= item_Click;
            //            Image image = originalMenuItem.Icon as Image;
            //            MenuItem menuItem = new MenuItem();
            //            menuItem.DataContext = originalMenuItem.DataContext;
            //            menuItem.Header = originalMenuItem.Header;
            //            menuItem.Icon = image;
            //            menuItem.Click += item_Click;
            //            if (menuItem.DataContext != null && menuItem.DataContext.Equals(SelectedItem))
            //            {
            //                menuItem.FontWeight = FontWeight.Bold;
            //            }
            //            menuItem.ItemTemplate = ItemTemplate;
            //            items.Add(menuItem);
            //        }
            //        else
            //        {
            //            Debug.WriteLine("Error [contextMenu_Opened]: item is not a MenuItem or Seperator");
            //        }

            //    }
            //}

            //contextMenu.Items = items;
            //contextMenu.Placement = PlacementMode.Relative;
            //contextMenu.PlacementTarget = dropDownBtn;
            //contextMenu.VerticalOffset = dropDownBtn.Height;
        }

        private MenuItem CreateMenuItem(object item)
        {
            //RibbonMenuItem menuItem = new RibbonMenuItem();
            MenuItem menuItem = new MenuItem();
            menuItem.DataContext = item;
            BreadcrumbItem bi = item as BreadcrumbItem;
            if (bi == null)
            {
                BreadcrumbItem parent = TemplatedParent as BreadcrumbItem;
                if (parent != null)
                    bi = parent.ContainerFromItem(item);
            }
            if (bi != null)
                menuItem.Header = bi.TraceValue;

            Image image = new Image();
            image.Source = bi != null ? bi.Image : null;
            //image.SnapsToDevicePixels = true;
            image.Stretch = Stretch.Fill;
            image.VerticalAlignment = Layout.VerticalAlignment.Center;
            image.HorizontalAlignment = Layout.HorizontalAlignment.Center;
            image.Width = 16;
            image.Height = 16;

            menuItem.Icon = image;

            menuItem.Click += item_Click;// += new RoutedEventHandler(item_Click);
            if (item != null && item.Equals(SelectedItem))
                menuItem.FontWeight = FontWeight.Bold;
            menuItem.ItemTemplate = ItemTemplate;
            //menuItem.ItemTemplateSelector = ItemTemplateSelector;
            //contextMenu.Items.OfType<object>().ToList().Add(menuItem);
            return menuItem;
        }

        //private void BreadcrumbItem_PointerPressed(object sender, PointerPressedEventArgs e)
        //{
        //    BreadcrumbItem item = e.Source as BreadcrumbItem;
        //    object dataItem = item.DataContext;
        //    RemoveSelectedItem(dataItem);
        //    SelectedItem = dataItem;

        //}

        private void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            object dataItem = item.DataContext;
            RemoveSelectedItem(dataItem);
            SelectedItem = dataItem;
            IsDropDownPressed = false;
        }

        /// <summary>
        /// When a BreadcrumbItem is selected from a dropdown menu, the SelectedItem of the new selected item must be set to null.
        /// Since no event is raised when a DependencyProperty is assigned to it's current value, this cannot be recognized at this place,
        /// therefore the SelectedItem DependencyProperty must previously set to null before setting it to it's new value to raise event
        /// when SelectedItem is changed:
        /// </summary>
        /// <param name="dataItem"></param>
        private void RemoveSelectedItem(object dataItem)
        {
            if (dataItem != null && dataItem.Equals(SelectedItem))
                SelectedItem = null;
        }
    }
}
