using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// ported from https://github.com/alicanerdogan/HamburgerMenu
    /// </summary>
    public class HamburgerMenuItem : ListBoxItem
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(HamburgerMenuItem);

        private const string ImageCtrlName = "menuImage";
        private Image _imageCtl;
        private string _lastSelectedBaseColorScheme;

        /// <summary>
        /// get/set Text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// <see cref="Text"/>
        /// </summary>
        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, string>(nameof(Text));

        /// <summary>
        /// get/set IconLight
        /// </summary>
        public Bitmap IconLight
        {
            get { return (Bitmap)GetValue(IconLightProperty); }
            set { SetValue(IconLightProperty, value); }
        }

        /// <summary>
        /// <see cref="IconLight"/>
        /// </summary>
        public static readonly StyledProperty<Bitmap> IconLightProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, Bitmap>(nameof(IconLight));

        /// <summary>
        /// get/set IconBlack
        /// </summary>
        public Bitmap IconBlack
        {
            get { return (Bitmap)GetValue(IconBlackProperty); }
            set { SetValue(IconBlackProperty, value); }
        }

        /// <summary>
        /// <see cref="IconBlack"/>
        /// </summary>
        public static readonly StyledProperty<Bitmap> IconBlackProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, Bitmap>(nameof(IconBlack));

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
            AvaloniaProperty.Register<HamburgerMenuItem, IBrush>(nameof(SelectionIndicatorColor));

        /// <summary>
        /// get/sets SelectionCommand
        /// </summary>
        public ICommand SelectionCommand
        {
            get { return (ICommand)GetValue(SelectionCommandProperty); }
            set { SetValue(SelectionCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectionCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> SelectionCommandProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, ICommand>(nameof(SelectionCommand));

        /// <summary>
        /// registered on ThemeManager.Instance.IsThemeChanged
        /// </summary>
        public HamburgerMenuItem()
        {
            ThemeManager.Instance.IsThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(object sender, OnThemeChangedEventArgs e)
        {
            UpdateMenuIcon();
        }

        /// <summary>
        /// gets some controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _imageCtl = e.NameScope.Find<Image>(ImageCtrlName);
            Button _button = e.NameScope.Find<Button>("ListBoxItemButton");
            _button.Tapped += (o, f) =>
            {
                this.IsSelected = true;
            };

            UpdateMenuIcon();
        }

        /// <summary>
        /// updates the icon for black/white theme
        /// </summary>
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
