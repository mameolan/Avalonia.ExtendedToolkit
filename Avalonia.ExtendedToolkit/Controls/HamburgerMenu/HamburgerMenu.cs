using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Specialized;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenu: ContentControl
    {
        public new AvaloniaList<HamburgerMenuItem> Content
        {
            get { return (AvaloniaList<HamburgerMenuItem>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public new static readonly StyledProperty<AvaloniaList<HamburgerMenuItem>> ContentProperty =
            AvaloniaProperty.Register<HamburgerMenu, AvaloniaList<HamburgerMenuItem>>(nameof(Content));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(IsOpen), defaultValue:false);

        public IBrush MenuIconColor
        {
            get { return (IBrush)GetValue(MenuIconColorProperty); }
            set { SetValue(MenuIconColorProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MenuIconColorProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(MenuIconColor));

        public IBrush SelectionIndicatorColor
        {
            get { return (IBrush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        public static readonly StyledProperty<IBrush> SelectionIndicatorColorProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(SelectionIndicatorColor));

        public IBrush MenuItemForeground
        {
            get { return (IBrush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MenuItemForegroundProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(MenuItemForeground));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly StyledProperty<int> SelectedIndexProperty =
            AvaloniaProperty.Register<HamburgerMenu, int>(nameof(SelectedIndex), defaultValue:1);

        public HamburgerMenu()
        {
            Content = new AvaloniaList<HamburgerMenuItem>();
            //ContentProperty.Changed.AddClassHandler<HamburgerMenu>((o, e) => OnHamburgerManuItemsAdded(o, e));
            Content.CollectionChanged += Content_CollectionChanged;

            IsOpenProperty.Changed.AddClassHandler<HamburgerMenu>((o, e) => IsOpenChanged(o, e));
        }

        private void IsOpenChanged(HamburgerMenu o, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.NewValue is bool)
            {
                //bool val = (bool)e.NewValue;

                //Animation animation = new Animation();

                //animation.Duration = TimeSpan.FromSeconds(2);
                //KeyFrame keyFrame = new KeyFrame();
                //keyFrame.SetValue(WidthProperty, 300);
                //animation.Children.Add(keyFrame);
                ////animation.Apply(o, null, null, () => { int i= 0; });

                //animation.RunAsync(this);
            }
        }

        private void Content_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
    }
}