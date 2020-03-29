using System;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public partial class MetroWindow : Window
    {
        private const string PART_Icon = "PART_Icon";
        private const string PART_TitleBar = "PART_TitleBar";
        private const string PART_WindowTitleBackground = "PART_WindowTitleBackground";
        private const string PART_WindowTitleThumb = "PART_WindowTitleThumb";
        private const string PART_FlyoutModalDragMoveThumb = "PART_FlyoutModalDragMoveThumb";
        private const string PART_LeftWindowCommands = "PART_LeftWindowCommands";
        private const string PART_RightWindowCommands = "PART_RightWindowCommands";
        private const string PART_WindowButtonCommands = "PART_WindowButtonCommands";
        private const string PART_OverlayBox = "PART_OverlayBox";
        private const string PART_MetroActiveDialogContainer = "PART_MetroActiveDialogContainer";
        private const string PART_MetroInactiveDialogsContainer = "PART_MetroInactiveDialogsContainer";
        private const string PART_FlyoutModal = "PART_FlyoutModal";
        private const string PART_Content = "PART_Content";

        private const string PART_TopHorizontalGrip="PART_TopHorizontalGrip";
        private const string PART_BottomHorizontalGrip = "PART_BottomHorizontalGrip";
        private const string PART_LeftVerticalGrip = "PART_LeftVerticalGrip";
        private const string PART_RightVerticalGrip = "PART_RightVerticalGrip";
        private const string PART_TopLeftGrip = "PART_TopLeftGrip";
        private const string PART_BottomLeftGrip = "PART_BottomLeftGrip";
        private const string PART_TopRightGrip = "PART_TopRightGrip";
        private const string PART_BottomRightGrip = "PART_BottomRightGrip";

        private ContentControl _icon;
        private ContentControl _titleBar;
        private Rectangle _titleBarBackground;
        private Thumb _windowTitleThumb;
        private Thumb _flyoutModalDragMoveThumb;
        private IInputElement restoreFocus;
        internal ContentPresenter LeftWindowCommandsPresenter;
        internal ContentPresenter RightWindowCommandsPresenter;
        internal ContentPresenter WindowButtonCommandsPresenter;

        internal Grid overlayBox;
        internal Grid metroActiveDialogContainer;
        internal Grid metroInactiveDialogContainer;

        //private Storyboard overlayStoryboard;
        private Rectangle flyoutModal;

        //private Button _closeButton;
        //private Button _minimiseButton;
        //private Button _restoreButton;
        //private Image _icon;
        //private Grid _titleBar;

        private Grid _bottomHorizontalGrip;
        private Grid _bottomLeftGrip;
        private Grid _bottomRightGrip;
        private Grid _leftVerticalGrip;
        private Grid _rightVerticalGrip;
        private Grid _topHorizontalGrip;
        private Grid _topLeftGrip;
        private Grid _topRightGrip;

        private bool _mouseDown;
        private Point _mouseDownPosition;

        public Type StyleKey => typeof(MetroWindow);

        /// <summary>
        /// Get/sets whether the titlebar icon is visible or not.
        /// </summary>
        public bool ShowIconOnTitleBar
        {
            get { return (bool)GetValue(ShowIconOnTitleBarProperty); }
            set { SetValue(ShowIconOnTitleBarProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowIconOnTitleBarProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowIconOnTitleBar), defaultValue: true);

        //edgemode is skipped

        /// <summary>
        /// Gets/sets whether the TitleBar is visible or not.
        /// </summary>
        public bool ShowTitleBar
        {
            get { return (bool)GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowTitleBarProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowTitleBar), defaultValue: true);

        /// <summary>
        /// Get/sets whether dialogs show over the title bar.
        /// </summary>
        public bool ShowDialogsOverTitleBar
        {
            get { return (bool)GetValue(ShowDialogsOverTitleBarProperty); }
            set { SetValue(ShowDialogsOverTitleBarProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowDialogsOverTitleBarProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowDialogsOverTitleBar), defaultValue: true);

        /// <summary>
        /// Gets whether one or more dialogs are shown.
        /// </summary>
        public bool IsAnyDialogOpen
        {
            get { return (bool)GetValue(IsAnyDialogOpenProperty); }
            private set { SetValue(IsAnyDialogOpenProperty, value); }
        }

        public static readonly StyledProperty<bool> IsAnyDialogOpenProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsAnyDialogOpen));

        /// <summary>
        /// Gets or sets whether if the minimize button is visible and
        /// the minimize system menu is enabled.
        /// </summary>
        public bool ShowMinButton
        {
            get { return (bool)GetValue(ShowMinButtonProperty); }
            set { SetValue(ShowMinButtonProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowMinButtonProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowMinButton), defaultValue: true);

        /// <summary>
        /// Gets or sets whether if the maximize/restore button
        /// is visible and the maximize/restore system menu is enabled.
        /// </summary>
        public bool ShowMaxRestoreButton
        {
            get { return (bool)GetValue(ShowMaxRestoreButtonProperty); }
            set { SetValue(ShowMaxRestoreButtonProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowMaxRestoreButtonProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowMaxRestoreButton), defaultValue: true);

        /// <summary>
        /// Gets or sets whether if the close button is visible.
        /// </summary>
        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowCloseButtonProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowCloseButton), defaultValue: true);

        /// <summary>
        /// Gets/sets if the min button is enabled.
        /// </summary>
        public bool IsMinButtonEnabled
        {
            get { return (bool)GetValue(IsMinButtonEnabledProperty); }
            private set { SetValue(IsMinButtonEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> IsMinButtonEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsMinButtonEnabled), defaultValue: true);

        /// <summary>
        /// Gets/sets if the max/restore button is enabled.
        /// </summary>
        public bool IsMaxRestoreButtonEnabled
        {
            get { return (bool)GetValue(IsMaxRestoreButtonEnabledProperty); }
            private set { SetValue(IsMaxRestoreButtonEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> IsMaxRestoreButtonEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsMaxRestoreButtonEnabled), defaultValue: true);

        /// <summary>
        /// Gets/sets if the close button is enabled.
        /// </summary>
        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            private set { SetValue(IsCloseButtonEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> IsCloseButtonEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsCloseButtonEnabled), defaultValue: true);

        //system menu skpped

        /// <summary>
        /// Gets/sets the TitleBar's height.
        /// </summary>
        public int TitleBarHeight
        {
            get { return (int)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }

        public static readonly StyledProperty<int> TitleBarHeightProperty =
            AvaloniaProperty.Register<MetroWindow, int>(nameof(TitleBarHeight), defaultValue: 30);

        /// <summary>
        /// Character casing of the title
        /// </summary>
        public CharacterCasing TitleCharacterCasing
        {
            get { return (CharacterCasing)GetValue(TitleCharacterCasingProperty); }
            set { SetValue(TitleCharacterCasingProperty, value); }
        }

        public static readonly StyledProperty<CharacterCasing> TitleCharacterCasingProperty =
            AvaloniaProperty.Register<MetroWindow, CharacterCasing>(nameof(TitleCharacterCasing),
                defaultValue: CharacterCasing.Upper);

        /// <summary>
        /// Gets/sets the title horizontal alignment.
        /// </summary>
        public HorizontalAlignment TitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(TitleAlignmentProperty); }
            set { SetValue(TitleAlignmentProperty, value); }
        }

        public static readonly StyledProperty<HorizontalAlignment> TitleAlignmentProperty =
            AvaloniaProperty.Register<MetroWindow, HorizontalAlignment>(nameof(TitleAlignment), defaultValue: HorizontalAlignment.Stretch);

        /// <summary>
        /// Gets/sets whether the window will save it's position between loads.
        /// </summary>
        public bool SaveWindowPosition
        {
            get { return (bool)GetValue(SaveWindowPositionProperty); }
            set { SetValue(SaveWindowPositionProperty, value); }
        }

        public static readonly StyledProperty<bool> SaveWindowPositionProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(SaveWindowPosition));

        /// <summary>
        /// Gets/sets the brush used for the titlebar's foreground.
        /// </summary>
        public IBrush TitleForeground
        {
            get { return (IBrush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> TitleForegroundProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(TitleForeground));

        /// <summary>
        /// Gets/sets whether the window's entrance transition animation is enabled.
        /// </summary>
        public bool WindowTransitionsEnabled
        {
            get { return (bool)GetValue(WindowTransitionsEnabledProperty); }
            set { SetValue(WindowTransitionsEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> WindowTransitionsEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(WindowTransitionsEnabled), defaultValue: true);

        /// <summary>
        /// Gets/sets the brush used for the Window's title bar.
        /// </summary>
        public IBrush WindowTitleBrush
        {
            get { return (IBrush)GetValue(WindowTitleBrushProperty); }
            set { SetValue(WindowTitleBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> WindowTitleBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(WindowTitleBrush), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// Gets/sets the brush used for the Window's non-active title bar.
        /// </summary>
        public IBrush NonActiveWindowTitleBrush
        {
            get { return (IBrush)GetValue(NonActiveWindowTitleBrushProperty); }
            set { SetValue(NonActiveWindowTitleBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> NonActiveWindowTitleBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(NonActiveWindowTitleBrush), defaultValue: (IBrush)Brushes.Gray);

        /// <summary>
        /// Gets/sets the brush used for the Window's non-active border.
        /// </summary>
        public IBrush NonActiveBorderBrush
        {
            get { return (IBrush)GetValue(NonActiveBorderBrushProperty); }
            set { SetValue(NonActiveBorderBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> NonActiveBorderBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(NonActiveBorderBrush), defaultValue: (IBrush)Brushes.Gray);

        /// <summary>
        /// Gets/sets the brush used for the Window's glow.
        /// </summary>
        public IBrush GlowBrush
        {
            get { return (IBrush)GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> GlowBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(GlowBrush));

        /// <summary>
        /// Gets/sets the brush used for the Window's non-active glow.
        /// </summary>
        public IBrush NonActiveGlowBrush
        {
            get { return (IBrush)GetValue(NonActiveGlowBrushProperty); }
            set { SetValue(NonActiveGlowBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> NonActiveGlowBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(NonActiveGlowBrush));

        /// <summary>
        /// Gets/sets the brush used for the dialog overlay.
        /// </summary>
        public IBrush OverlayBrush
        {
            get { return (IBrush)GetValue(OverlayBrushProperty); }
            set { SetValue(OverlayBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> OverlayBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(OverlayBrush));

        /// <summary>
        /// Gets/sets the opacity used for the dialog overlay.
        /// </summary>
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }

        public static readonly StyledProperty<double> OverlayOpacityProperty =
            AvaloniaProperty.Register<MetroWindow, double>(nameof(OverlayOpacity), defaultValue: 0.7d);

        public bool OverlayFadeIn
        {
            get { return (bool)GetValue(OverlayFadeInProperty); }
            set { SetValue(OverlayFadeInProperty, value); }
        }

        public static readonly StyledProperty<bool> OverlayFadeInProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(OverlayFadeIn));

        public bool OverlayFadeOut
        {
            get { return (bool)GetValue(OverlayFadeOutProperty); }
            set { SetValue(OverlayFadeOutProperty, value); }
        }

        public static readonly StyledProperty<bool> OverlayFadeOutProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(OverlayFadeOut));

        /// <summary>
        /// Defines if the Taskbar should be ignored when maximizing a Window.
        /// This only works with WindowStyle = None.
        /// </summary>
        public bool IgnoreTaskbarOnMaximize
        {
            get { return (bool)GetValue(IgnoreTaskbarOnMaximizeProperty); }
            set { SetValue(IgnoreTaskbarOnMaximizeProperty, value); }
        }

        public static readonly StyledProperty<bool> IgnoreTaskbarOnMaximizeProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IgnoreTaskbarOnMaximize));

        /// <summary>
        /// Gets or sets resize border thickness. This enables animation, styling, binding, etc...
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }

        public static readonly StyledProperty<Thickness> ResizeBorderThicknessProperty =
            AvaloniaProperty.Register<MetroWindow, Thickness>(nameof(ResizeBorderThickness), defaultValue: new Thickness(6D));

        /// <summary>
        /// Gets/sets if the border thickness value should be kept on maximize
        /// if the MaxHeight/MaxWidth of the window is less than the monitor resolution.
        /// </summary>
        public bool KeepBorderOnMaximize
        {
            get { return (bool)GetValue(KeepBorderOnMaximizeProperty); }
            set { SetValue(KeepBorderOnMaximizeProperty, value); }
        }

        public static readonly StyledProperty<bool> KeepBorderOnMaximizeProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(KeepBorderOnMaximize), defaultValue: true);

        /// <summary>
        /// Gets or sets wether the resizing of the window should be tried in a way
        /// that does not cause flicker/jitter, especially when resizing from the left side.
        /// </summary>
        /// <remarks>
        /// Please note that setting this to <c>true</c> may cause resize lag and
        /// black areas appearing on some systems.
        /// </remarks>
        public bool TryToBeFlickerFree
        {
            get { return (bool)GetValue(TryToBeFlickerFreeProperty); }
            set { SetValue(TryToBeFlickerFreeProperty, value); }
        }

        public static readonly StyledProperty<bool> TryToBeFlickerFreeProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(TryToBeFlickerFree));

        /// <summary>
        /// Gets/sets the icon content template to show a custom icon.
        /// </summary>
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        public static readonly StyledProperty<DataTemplate> IconTemplateProperty =
            AvaloniaProperty.Register<MetroWindow, DataTemplate>(nameof(IconTemplate));

        /// <summary>
        /// Gets/sets the title content template to show a custom title.
        /// </summary>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        public static readonly StyledProperty<DataTemplate> TitleTemplateProperty =
            AvaloniaProperty.Register<MetroWindow, DataTemplate>(nameof(TitleTemplate));

        /// <summary>
        /// Gets or sets the brush used for the Flyouts overlay.
        /// </summary>
        public IBrush FlyoutOverlayBrush
        {
            get { return (IBrush)GetValue(FlyoutOverlayBrushProperty); }
            set { SetValue(FlyoutOverlayBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> FlyoutOverlayBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(FlyoutOverlayBrush));

        /// <summary>
        /// Gets/sets the FlyoutsControl that hosts the window's flyouts.
        /// </summary>
        public FlyoutsControl Flyouts
        {
            get { return (FlyoutsControl)GetValue(FlyoutsProperty); }
            set { SetValue(FlyoutsProperty, value); }
        }

        public static readonly StyledProperty<FlyoutsControl> FlyoutsProperty =
            AvaloniaProperty.Register<MetroWindow, FlyoutsControl>(nameof(Flyouts));

        /// <summary>
        /// Defines the <see cref="IsChromeVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsChromeVisibleProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsChromeVisible), true);

        /// <summary>
        /// Defines the <see cref="TitleBarContent"/> property.
        /// </summary>
        public static readonly StyledProperty<Control> TitleBarContentProperty =
            AvaloniaProperty.Register<MetroWindow, Control>(nameof(TitleBarContent));

        /// <summary>
        ///  Gets or sets the flag indicating whether chrome is visible.
        /// </summary>
        public bool IsChromeVisible
        {
            get => (bool)GetValue(IsChromeVisibleProperty);
            set => SetValue(IsChromeVisibleProperty, value);
        }

        /// <summary>
        ///  Gets or sets the title bar content control.
        /// </summary>
        public Control TitleBarContent
        {
            get => (Control)GetValue(TitleBarContentProperty);
            set => SetValue(TitleBarContentProperty, value);
        }

        public static readonly RoutedEvent<FlyoutStatusChangedRoutedEventArgs> FlyoutsStatusChangedEvent =
            RoutedEvent.Register<MetroWindow, FlyoutStatusChangedRoutedEventArgs>(nameof(FlyoutsStatusChangedEvent), RoutingStrategies.Bubble);

        public event FlyoutStatusChangedHandler FlyoutsStatusChanged
        {
            add
            {
                AddHandler(FlyoutsStatusChangedEvent, value);
            }
            remove
            {
                RemoveHandler(FlyoutsStatusChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> SizeChangedEvent =
            RoutedEvent.Register<MetroWindow, RoutedEventArgs>(nameof(SizeChangedEvent), RoutingStrategies.Bubble);

        public event EventHandler SizeChanged
        {
            add
            {
                AddHandler(SizeChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SizeChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> WindowTransitionCompletedEvent =
            RoutedEvent.Register<MetroWindow, RoutedEventArgs>(nameof(FlyoutsStatusChangedEvent), RoutingStrategies.Bubble);

        public event EventHandler<VectorEventArgs> WindowTransitionCompleted
        {
            add
            {
                AddHandler(WindowTransitionCompletedEvent, value);
            }
            remove
            {
                RemoveHandler(WindowTransitionCompletedEvent, value);
            }
        }

        /// <summary>
        /// Gets/sets the left window commands that hosts the user commands.
        /// </summary>
        public WindowCommands LeftWindowCommands
        {
            get { return (WindowCommands)GetValue(LeftWindowCommandsProperty); }
            set { SetValue(LeftWindowCommandsProperty, value); }
        }

        public static readonly StyledProperty<WindowCommands> LeftWindowCommandsProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommands>(nameof(LeftWindowCommands));

        /// <summary>
        /// Gets/sets the right window commands that hosts the user commands.
        /// </summary>
        public WindowCommands RightWindowCommands
        {
            get { return (WindowCommands)GetValue(RightWindowCommandsProperty); }
            set { SetValue(RightWindowCommandsProperty, value); }
        }

        public static readonly StyledProperty<WindowCommands> RightWindowCommandsProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommands>(nameof(RightWindowCommands));

        /// <summary>
        /// Gets/sets the window button commands that hosts the min/max/close commands.
        /// </summary>
        public WindowButtonCommands WindowButtonCommands
        {
            get { return (WindowButtonCommands)GetValue(WindowButtonCommandsProperty); }
            set { SetValue(WindowButtonCommandsProperty, value); }
        }

        public static readonly StyledProperty<WindowButtonCommands> WindowButtonCommandsProperty =
            AvaloniaProperty.Register<MetroWindow, WindowButtonCommands>(nameof(WindowButtonCommands));

        public WindowCommandsOverlayBehavior LeftWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(LeftWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(LeftWindowCommandsOverlayBehaviorProperty, value); }
        }

        public static readonly StyledProperty<WindowCommandsOverlayBehavior> LeftWindowCommandsOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommandsOverlayBehavior>(nameof(LeftWindowCommandsOverlayBehavior), defaultValue: WindowCommandsOverlayBehavior.Never);

        public WindowCommandsOverlayBehavior RightWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(RightWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(RightWindowCommandsOverlayBehaviorProperty, value); }
        }

        public static readonly StyledProperty<WindowCommandsOverlayBehavior> RightWindowCommandsOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommandsOverlayBehavior>(nameof(RightWindowCommandsOverlayBehavior), defaultValue: WindowCommandsOverlayBehavior.Never);

        public OverlayBehavior WindowButtonCommandsOverlayBehavior
        {
            get { return (OverlayBehavior)GetValue(WindowButtonCommandsOverlayBehaviorProperty); }
            set { SetValue(WindowButtonCommandsOverlayBehaviorProperty, value); }
        }

        public static readonly StyledProperty<OverlayBehavior> WindowButtonCommandsOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, OverlayBehavior>(nameof(WindowButtonCommandsOverlayBehavior), defaultValue: OverlayBehavior.Always);

        public OverlayBehavior IconOverlayBehavior
        {
            get { return (OverlayBehavior)GetValue(IconOverlayBehaviorProperty); }
            set { SetValue(IconOverlayBehaviorProperty, value); }
        }

        public static readonly StyledProperty<OverlayBehavior> IconOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, OverlayBehavior>(nameof(IconOverlayBehavior), OverlayBehavior.Never);

        /// <summary>
        /// Gets/sets whether the WindowStyle is None or not.
        /// </summary>
        public bool UseNoneWindowStyle
        {
            get { return (bool)GetValue(UseNoneWindowStyleProperty); }
            set { SetValue(UseNoneWindowStyleProperty, value); }
        }

        public static readonly StyledProperty<bool> UseNoneWindowStyleProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(UseNoneWindowStyle));

        /// <summary>
        /// Allows easy handling of window commands brush. Theme is also applied based on this brush.
        /// </summary>
        public IBrush OverrideDefaultWindowCommandsBrush
        {
            get { return (IBrush)GetValue(OverrideDefaultWindowCommandsBrushProperty); }
            set { SetValue(OverrideDefaultWindowCommandsBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> OverrideDefaultWindowCommandsBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(OverrideDefaultWindowCommandsBrush));

        public bool IsWindowDraggable
        {
            get { return (bool)GetValue(IsWindowDraggableProperty); }
            set { SetValue(IsWindowDraggableProperty, value); }
        }

        public static readonly StyledProperty<bool> IsWindowDraggableProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsWindowDraggable), defaultValue: true);
    }
}
