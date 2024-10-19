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
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// A breadcrumb button is part of a BreadcrumbItem and contains 
    /// a header and a dropdown button.
    /// </summary>
    public partial class BreadcrumbButton : HeaderedItemsControl
    {
        /// <summary>
        /// registeres listeners
        /// </summary>
        public BreadcrumbButton()
        {
            ImageProperty.Changed.AddClassHandler<BreadcrumbButton>((o, e) => OnImageChanged(o, e));
            //.changed
            ItemsSourceProperty.Changed.AddClassHandler<BreadcrumbButton>((o, e) => OnItemsCollectionChanged(o, e));

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
            HasItems = Items == null ? false : Items.OfType<object>().Any();
        }

        private void OnImageChanged(BreadcrumbButton o, AvaloniaPropertyChangedEventArgs e)
        {
            HasImage = e.NewValue is Bitmap;
        }

        /// <summary>
        /// sets the context menu
        /// </summary>
        /// <param name="e"></param>
        //.orig
        //protected override void ItemsChanged(AvaloniaPropertyChangedEventArgs e)
        //{
        //    base.ItemsChanged(e);

        //    if (contextMenu != null && e.NewValue != null)
        //    {
        //        var currentItems = Items.OfType<object>().Where(x => x != null).ToList();

        //        //dropPanel.ContextMenu.IsVisible = true;

        //        if (currentItems.Count == 0)
        //        {
        //            //    dropPanel.ContextMenu.IsVisible = false;
        //            return;
        //        }

        //        contextMenu.ItemTemplate = ItemTemplate;

        //        //if (contextMenu.IsOpen)
        //        //    contextMenu.Close();

        //        var items = new AvaloniaList<object>();
        //        contextMenu.ItemsSource = new AvaloniaList<object>();

        //        foreach (object item in Items)
        //        {
        //            if (item == null)
        //                continue;

        //            if (!(item is MenuItem) && !(item is Separator))
        //            {
        //                items.Add(CreateMenuItem(item));
        //            }
        //            else
        //            {
        //                if (item is Separator)
        //                {
        //                    items.Add(new Separator());
        //                }
        //                else if (item is MenuItem)
        //                {
        //                    MenuItem originalMenuItem = item as MenuItem;
        //                    originalMenuItem.Click -= item_Click;
        //                    Image image = originalMenuItem.Icon as Image;
        //                    MenuItem menuItem = new MenuItem();
        //                    menuItem.DataContext = originalMenuItem.DataContext;
        //                    menuItem.Header = originalMenuItem.Header;
        //                    menuItem.Icon = image;
        //                    menuItem.Click += item_Click;
        //                    if (menuItem.DataContext != null && menuItem.DataContext.Equals(SelectedItem))
        //                    {
        //                        menuItem.FontWeight = FontWeight.Bold;
        //                    }
        //                    menuItem.ItemTemplate = ItemTemplate;
        //                    items.Add(menuItem);
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("Error [contextMenu_Opened]: item is not a MenuItem or Seperator");
        //                }
        //            }
        //        }

        //        contextMenu.Items = items;

        //        //if(contextMenuWasOpen)
        //        //{
        //        //    contextMenu.Open(dropPanel);
        //        //}
        //    }
        //}

        private void OverflowPressedChanged(BreadcrumbButton o, AvaloniaPropertyChangedEventArgs e)
        {
            //todo check
            if (contextMenu != null)
            {
                if (contextMenu.Items.OfType<object>().Any() == false)
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

        /// <summary>
        /// occurs if the overflow is pressed
        /// </summary>
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

        /// <summary>
        /// sets <see cref="IsPressed"/> to false
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerExited(PointerEventArgs e)
        {
            IsPressed = false;
        }

        /// <summary>
        /// sets <see cref="IsPressed"/> to true
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// sets <see cref="IsPressed"/> to false
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// updates <see cref="IsPressed"/> 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerEntered(PointerEventArgs e)
        {
            var prop = e.GetCurrentPoint(this).Properties;

            isPressed = prop.IsLeftButtonPressed;
            Visual parent = TemplatedParent as Visual;
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

            base.OnPointerEntered(e);
        }

        /// <summary>
        /// gets some controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
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

            base.OnApplyTemplate(e);
            //.obsolete, ItemsSourceProperty is styled
            //RaisePropertyChanged(ItemsSourceProperty, new Data.Optional<IEnumerable>(), new Data.Optional<IEnumerable>(Items));
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
                //.edited
                var parent = VisualTree.VisualExtensions.GetVisualParent<BreadcrumbBar>((Visual)this.Parent);

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
        /// Since no event is raised when a AvaloniaProperty is assigned to it's current value, this cannot be recognized at this place,
        /// therefore the SelectedItem AvaloniaProperty must previously set to null before setting it to it's new value to raise event
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
