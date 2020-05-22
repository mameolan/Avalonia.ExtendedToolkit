using System;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// ported from https://github.com/alicanerdogan/HamburgerMenu
    /// </summary>
    public class HamburgerMenu: ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(HamburgerMenu);

        /// <summary>
        /// get/sets HamburgerMenuItems as content
        /// </summary>
        public new AvaloniaList<HamburgerMenuItem> Content
        {
            get { return (AvaloniaList<HamburgerMenuItem>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// <see cref="Content"/>
        /// </summary>
        public new static readonly StyledProperty<AvaloniaList<HamburgerMenuItem>> ContentProperty =
            AvaloniaProperty.Register<HamburgerMenu, AvaloniaList<HamburgerMenuItem>>(nameof(Content));

        /// <summary>
        /// get/set IsOpen
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// <see cref="IsOpen"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(IsOpen), defaultValue:false);

        /// <summary>
        /// get/set MenuIconColor
        /// </summary>
        public IBrush MenuIconColor
        {
            get { return (IBrush)GetValue(MenuIconColorProperty); }
            set { SetValue(MenuIconColorProperty, value); }
        }

        /// <summary>
        /// <see cref="MenuIconColor"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MenuIconColorProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(MenuIconColor));

        /// <summary>
        /// get/set SelectionIndicatorColor
        /// </summary>
        public IBrush SelectionIndicatorColor
        {
            get { return (IBrush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectionIndicatorColor"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> SelectionIndicatorColorProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(SelectionIndicatorColor));

        /// <summary>
        /// get/set MenuItemForeground
        /// </summary>
        public IBrush MenuItemForeground
        {
            get { return (IBrush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        /// <summary>
        /// <see cref="MenuItemForeground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MenuItemForegroundProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(MenuItemForeground));

        /// <summary>
        /// get/set SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedIndex"/>
        /// </summary>
        public static readonly StyledProperty<int> SelectedIndexProperty =
            AvaloniaProperty.Register<HamburgerMenu, int>(nameof(SelectedIndex), defaultValue:0);

        //Setting over a property does not work right now
        //public double MaximizedSize
        //{
        //    get { return (double)GetValue(MaximizedSizeProperty); }
        //    set { SetValue(MaximizedSizeProperty, value); }
        //}

        //public static readonly StyledProperty<double> MaximizedSizeProperty =
        //    AvaloniaProperty.Register<HamburgerMenu, double>(nameof(MaximizedSize), defaultValue: 300d);

        //Setting over a property does not work right now
        //public double MinimizedSize
        //{
        //    get { return (double)GetValue(MinimizedSizeProperty); }
        //    set { SetValue(MinimizedSizeProperty, value); }
        //}

        //public static readonly StyledProperty<double> MinimizedSizeProperty =
        //    AvaloniaProperty.Register<HamburgerMenu, double>(nameof(MinimizedSize), defaultValue: 50d);

        /// <summary>
        /// initilaize content and collection changed
        /// </summary>
        public HamburgerMenu()
        {
            Content = new AvaloniaList<HamburgerMenuItem>();
            Content.CollectionChanged += Content_CollectionChanged;
        }

        private void Content_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<HamburgerMenuItem>())
                {
                    item.PropertyChanged += MenuItem_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
            }
        }

        private void MenuItem_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "IsSelected")
            {
                HamburgerMenuItem menuItem = sender as HamburgerMenuItem;
                SelectedIndex = Content.IndexOf(menuItem);
            }
        }
    }
}
