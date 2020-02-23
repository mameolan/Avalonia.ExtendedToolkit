using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuItem : ListBoxItem
    {
        private const string ImageCtrlName = "menuImage";
        private Image _imageCtl;
        private string _lastSelectedBaseColorScheme;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, string>(nameof(Text));

        public IImage IconLight
        {
            get { return (IImage)GetValue(IconLightProperty); }
            set { SetValue(IconLightProperty, value); }
        }

        public static readonly StyledProperty<IImage> IconLightProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, IImage>(nameof(IconLight));

        public IImage IconBlack
        {
            get { return (IImage)GetValue(IconBlackProperty); }
            set { SetValue(IconBlackProperty, value); }
        }

        public static readonly StyledProperty<IImage> IconBlackProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, IImage>(nameof(IconBlack));

        public IBrush SelectionIndicatorColor
        {
            get { return (IBrush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        public static readonly StyledProperty<IBrush> SelectionIndicatorColorProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, IBrush>(nameof(SelectionIndicatorColor));

        public ICommand SelectionCommand
        {
            get { return (ICommand)GetValue(SelectionCommandProperty); }
            set { SetValue(SelectionCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> SelectionCommandProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, ICommand>(nameof(SelectionCommand));

        public HamburgerMenuItem()
        {
            ThemeManager.Instance.IsThemeChanged += OnThemeChanged;

            IsSelectedProperty.Changed.AddClassHandler<HamburgerMenuItem>((o, e) => IsSelectedChanged(o, e));
        }

        private void IsSelectedChanged(HamburgerMenuItem o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool)
            {
                if ((bool)e.NewValue)
                {
                    Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    Background = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void OnThemeChanged(object sender, OnThemeChangedEventArgs e)
        {
            UpdateMenuIcon();
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            _imageCtl = e.NameScope.Find<Image>(ImageCtrlName);
            Button _button = e.NameScope.Find<Button>("ListBoxItemButton");
            _button.Tapped += (o, f) =>
            {
                this.IsSelected = true;
            };

            UpdateMenuIcon();
        }

        private void UpdateMenuIcon()
        {
            if (_imageCtl == null)
                return;

            if (_lastSelectedBaseColorScheme == ThemeManager.Instance?.SelectedTheme.BaseColorScheme)
            {
                return;
            }

            if (ThemeManager.Instance?.SelectedTheme.BaseColorScheme == ThemeManager.BaseColorDark)
            {
                _imageCtl.Source = IconBlack;
            }
            else
            {
                _imageCtl.Source = IconLight;
            }

            _lastSelectedBaseColorScheme = ThemeManager.Instance?.SelectedTheme.BaseColorScheme;
        }
    }
}