using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Interaction logic for <see cref="MetroWindow"/> xaml.
    /// </summary>
    public partial class MetroWindow : Window, IStyleable
    {
        public bool ShowIconOnTitleBar
        {
            get { return (bool)GetValue(ShowIconOnTitleBarProperty); }
            set { SetValue(ShowIconOnTitleBarProperty, value); }
        }

        public static readonly AvaloniaProperty ShowIconOnTitleBarProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowIconOnTitleBar), defaultValue: true);

        //edgemode is skipped

        public bool ShowTitleBar
        {
            get { return (bool)GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        public static readonly AvaloniaProperty ShowTitleBarProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowTitleBar), defaultValue: true);

        public bool ShowDialogsOverTitleBar
        {
            get { return (bool)GetValue(ShowDialogsOverTitleBarProperty); }
            set { SetValue(ShowDialogsOverTitleBarProperty, value); }
        }

        public static readonly AvaloniaProperty ShowDialogsOverTitleBarProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowDialogsOverTitleBar), defaultValue: true);

        public bool IsAnyDialogOpen
        {
            get { return (bool)GetValue(IsAnyDialogOpenProperty); }
            private set { SetValue(IsAnyDialogOpenProperty, value); }
        }

        public static readonly AvaloniaProperty IsAnyDialogOpenProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsAnyDialogOpen));

        public bool ShowMinButton
        {
            get { return (bool)GetValue(ShowMinButtonProperty); }
            set { SetValue(ShowMinButtonProperty, value); }
        }

        public static readonly AvaloniaProperty ShowMinButtonProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowMinButton), defaultValue: true);

        public bool ShowMaxRestoreButton
        {
            get { return (bool)GetValue(ShowMaxRestoreButtonProperty); }
            set { SetValue(ShowMaxRestoreButtonProperty, value); }
        }

        public static readonly AvaloniaProperty ShowMaxRestoreButtonProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowMaxRestoreButton), defaultValue: true);

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        public static readonly AvaloniaProperty ShowCloseButtonProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(ShowCloseButton), defaultValue: true);

        public bool IsMinButtonEnabled
        {
            get { return (bool)GetValue(IsMinButtonEnabledProperty); }
            private set { SetValue(IsMinButtonEnabledProperty, value); }
        }

        public static readonly AvaloniaProperty IsMinButtonEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsMinButtonEnabled), defaultValue: true);

        public bool IsMaxRestoreButtonEnabled
        {
            get { return (bool)GetValue(IsMaxRestoreButtonEnabledProperty); }
            private set { SetValue(IsMaxRestoreButtonEnabledProperty, value); }
        }

        public static readonly AvaloniaProperty IsMaxRestoreButtonEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsMaxRestoreButtonEnabled), defaultValue: true);

        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            private set { SetValue(IsCloseButtonEnabledProperty, value); }
        }

        public static readonly AvaloniaProperty IsCloseButtonEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsCloseButtonEnabled), defaultValue: true);

        //system menu skpped

        public int TitleBarHeight
        {
            get { return (int)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }

        public static readonly AvaloniaProperty TitleBarHeightProperty =
            AvaloniaProperty.Register<MetroWindow, int>(nameof(TitleBarHeight), defaultValue: 30);

        public CharacterCasing TitleCharacterCasing
        {
            get { return (CharacterCasing)GetValue(TitleCharacterCasingProperty); }
            set { SetValue(TitleCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty TitleCharacterCasingProperty =
            AvaloniaProperty.Register<MetroWindow, CharacterCasing>(nameof(TitleCharacterCasing),
                defaultValue: CharacterCasing.Upper);

        public HorizontalAlignment TitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(TitleAlignmentProperty); }
            set { SetValue(TitleAlignmentProperty, value); }
        }

        public static readonly AvaloniaProperty TitleAlignmentProperty =
            AvaloniaProperty.Register<MetroWindow, HorizontalAlignment>(nameof(TitleAlignment), defaultValue: HorizontalAlignment.Stretch);

        public bool SaveWindowPosition
        {
            get { return (bool)GetValue(SaveWindowPositionProperty); }
            set { SetValue(SaveWindowPositionProperty, value); }
        }

        public static readonly AvaloniaProperty SaveWindowPositionProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(SaveWindowPosition));

        

        public IBrush TitleForeground
        {
            get { return (IBrush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        public static readonly AvaloniaProperty TitleForegroundProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(TitleForeground));

        public bool WindowTransitionsEnabled
        {
            get { return (bool)GetValue(WindowTransitionsEnabledProperty); }
            set { SetValue(WindowTransitionsEnabledProperty, value); }
        }

        public static readonly AvaloniaProperty WindowTransitionsEnabledProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(WindowTransitionsEnabled), defaultValue: true);

        public IBrush WindowTitleBrush
        {
            get { return (IBrush)GetValue(WindowTitleBrushProperty); }
            set { SetValue(WindowTitleBrushProperty, value); }
        }

        public static readonly AvaloniaProperty WindowTitleBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(WindowTitleBrush), defaultValue: (IBrush)Brushes.Transparent);

        public IBrush NonActiveWindowTitleBrush
        {
            get { return (IBrush)GetValue(NonActiveWindowTitleBrushProperty); }
            set { SetValue(NonActiveWindowTitleBrushProperty, value); }
        }

        public static readonly AvaloniaProperty NonActiveWindowTitleBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(NonActiveWindowTitleBrush), defaultValue: (IBrush)Brushes.Gray);

        public IBrush NonActiveBorderBrush
        {
            get { return (IBrush)GetValue(NonActiveBorderBrushProperty); }
            set { SetValue(NonActiveBorderBrushProperty, value); }
        }

        public static readonly AvaloniaProperty NonActiveBorderBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(NonActiveBorderBrush), defaultValue: (IBrush)Brushes.Gray);

        public IBrush GlowBrush
        {
            get { return (IBrush)GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }

        public static readonly AvaloniaProperty GlowBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(GlowBrush));

        public IBrush NonActiveGlowBrush
        {
            get { return (IBrush)GetValue(NonActiveGlowBrushProperty); }
            set { SetValue(NonActiveGlowBrushProperty, value); }
        }

        public static readonly AvaloniaProperty NonActiveGlowBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(NonActiveGlowBrush));

        public IBrush OverlayBrush
        {
            get { return (IBrush)GetValue(OverlayBrushProperty); }
            set { SetValue(OverlayBrushProperty, value); }
        }

        public static readonly AvaloniaProperty OverlayBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(OverlayBrush));

        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }

        public static readonly AvaloniaProperty OverlayOpacityProperty =
            AvaloniaProperty.Register<MetroWindow, double>(nameof(OverlayOpacity), defaultValue: 0.7d);

        public bool OverlayFadeIn
        {
            get { return (bool)GetValue(OverlayFadeInProperty); }
            set { SetValue(OverlayFadeInProperty, value); }
        }

        public static readonly AvaloniaProperty OverlayFadeInProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(OverlayFadeIn));

        public bool OverlayFadeOut
        {
            get { return (bool)GetValue(OverlayFadeOutProperty); }
            set { SetValue(OverlayFadeOutProperty, value); }
        }

        public static readonly AvaloniaProperty OverlayFadeOutProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(OverlayFadeOut));

        public bool IgnoreTaskbarOnMaximize
        {
            get { return (bool)GetValue(IgnoreTaskbarOnMaximizeProperty); }
            set { SetValue(IgnoreTaskbarOnMaximizeProperty, value); }
        }

        public static readonly AvaloniaProperty IgnoreTaskbarOnMaximizeProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IgnoreTaskbarOnMaximize));

        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }

        public static readonly AvaloniaProperty ResizeBorderThicknessProperty =
            AvaloniaProperty.Register<MetroWindow, Thickness>(nameof(ResizeBorderThickness), defaultValue: new Thickness(6D));

        public bool KeepBorderOnMaximize
        {
            get { return (bool)GetValue(KeepBorderOnMaximizeProperty); }
            set { SetValue(KeepBorderOnMaximizeProperty, value); }
        }

        public static readonly AvaloniaProperty KeepBorderOnMaximizeProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(KeepBorderOnMaximize), defaultValue: true);

        public bool TryToBeFlickerFree
        {
            get { return (bool)GetValue(TryToBeFlickerFreeProperty); }
            set { SetValue(TryToBeFlickerFreeProperty, value); }
        }

        public static readonly AvaloniaProperty TryToBeFlickerFreeProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(TryToBeFlickerFree));

        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        public static readonly AvaloniaProperty IconTemplateProperty =
            AvaloniaProperty.Register<MetroWindow, DataTemplate>(nameof(IconTemplate));

        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        public static readonly AvaloniaProperty TitleTemplateProperty =
            AvaloniaProperty.Register<MetroWindow, DataTemplate>(nameof(TitleTemplate));

        public IBrush FlyoutOverlayBrush
        {
            get { return (IBrush)GetValue(FlyoutOverlayBrushProperty); }
            set { SetValue(FlyoutOverlayBrushProperty, value); }
        }

        public static readonly AvaloniaProperty FlyoutOverlayBrushProperty =
            AvaloniaProperty.Register<MetroWindow, IBrush>(nameof(FlyoutOverlayBrush));

        public FlyoutsControl Flyouts
        {
            get { return (FlyoutsControl)GetValue(FlyoutsProperty); }
            set { SetValue(FlyoutsProperty, value); }
        }

        public static readonly AvaloniaProperty FlyoutsProperty =
            AvaloniaProperty.Register<MetroWindow, FlyoutsControl>(nameof(Flyouts));

        /// <summary>
        /// Defines the <see cref="IsChromeVisible"/> property.
        /// </summary>
        public static readonly AvaloniaProperty<bool> IsChromeVisibleProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsChromeVisible), true);

        /// <summary>
        /// Defines the <see cref="TitleBarContent"/> property.
        /// </summary>
        public static readonly AvaloniaProperty<Control> TitleBarContentProperty =
            AvaloniaProperty.Register<MetroWindow, Control>(nameof(TitleBarContent));

        /// <summary>
        ///  Gets or sets the flag indicating whether chrome is visible.
        /// </summary>
        public bool IsChromeVisible
        {
            get => GetValue(IsChromeVisibleProperty);
            set => SetValue(IsChromeVisibleProperty, value);
        }

        /// <summary>
        ///  Gets or sets the title bar content control.
        /// </summary>
        public Control TitleBarContent
        {
            get => GetValue(TitleBarContentProperty);
            set => SetValue(TitleBarContentProperty, value);
        }

        public static RoutedEvent<RoutedEventArgs> FlyoutsStatusChangedEvent =
            RoutedEvent.Register<MetroWindow, RoutedEventArgs>(nameof(FlyoutsStatusChangedEvent), RoutingStrategies.Bubble);

        public event DragStartedEventHandler FlyoutsStatusChanged
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

        public static RoutedEvent<RoutedEventArgs> SizeChangedEvent =
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

        public static RoutedEvent<RoutedEventArgs> WindowTransitionCompletedEvent =
            RoutedEvent.Register<MetroWindow, RoutedEventArgs>(nameof(FlyoutsStatusChangedEvent), RoutingStrategies.Bubble);

        public event DragStartedEventHandler WindowTransitionCompleted
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

        public WindowCommands LeftWindowCommands
        {
            get { return (WindowCommands)GetValue(LeftWindowCommandsProperty); }
            set { SetValue(LeftWindowCommandsProperty, value); }
        }

        public static readonly AvaloniaProperty LeftWindowCommandsProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommands>(nameof(LeftWindowCommands));

        public WindowCommands RightWindowCommands
        {
            get { return (WindowCommands)GetValue(RightWindowCommandsProperty); }
            set { SetValue(RightWindowCommandsProperty, value); }
        }

        public static readonly AvaloniaProperty RightWindowCommandsProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommands>(nameof(RightWindowCommands));

        public WindowButtonCommands WindowButtonCommands
        {
            get { return (WindowButtonCommands)GetValue(WindowButtonCommandsProperty); }
            set { SetValue(WindowButtonCommandsProperty, value); }
        }

        public static readonly AvaloniaProperty WindowButtonCommandsProperty =
            AvaloniaProperty.Register<MetroWindow, WindowButtonCommands>(nameof(WindowButtonCommands));

        public WindowCommandsOverlayBehavior LeftWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(LeftWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(LeftWindowCommandsOverlayBehaviorProperty, value); }
        }

        public static readonly AvaloniaProperty LeftWindowCommandsOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommandsOverlayBehavior>(nameof(LeftWindowCommandsOverlayBehavior), defaultValue: WindowCommandsOverlayBehavior.Never);

        public WindowCommandsOverlayBehavior RightWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(RightWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(RightWindowCommandsOverlayBehaviorProperty, value); }
        }

        public static readonly AvaloniaProperty RightWindowCommandsOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, WindowCommandsOverlayBehavior>(nameof(RightWindowCommandsOverlayBehavior), defaultValue: WindowCommandsOverlayBehavior.Never);

        public OverlayBehavior WindowButtonCommandsOverlayBehavior
        {
            get { return (OverlayBehavior)GetValue(WindowButtonCommandsOverlayBehaviorProperty); }
            set { SetValue(WindowButtonCommandsOverlayBehaviorProperty, value); }
        }

        public static readonly AvaloniaProperty WindowButtonCommandsOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, OverlayBehavior>(nameof(WindowButtonCommandsOverlayBehavior), defaultValue: OverlayBehavior.Always);

        public OverlayBehavior IconOverlayBehavior
        {
            get { return (OverlayBehavior)GetValue(IconOverlayBehaviorProperty); }
            set { SetValue(IconOverlayBehaviorProperty, value); }
        }

        public static readonly AvaloniaProperty IconOverlayBehaviorProperty =
            AvaloniaProperty.Register<MetroWindow, OverlayBehavior>(nameof(IconOverlayBehavior), OverlayBehavior.Never);

        public bool UseNoneWindowStyle
        {
            get { return (bool)GetValue(UseNoneWindowStyleProperty); }
            set { SetValue(UseNoneWindowStyleProperty, value); }
        }

        public static readonly AvaloniaProperty UseNoneWindowStyleProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(UseNoneWindowStyle));

        public SolidColorBrush OverrideDefaultWindowCommandsBrush
        {
            get { return (SolidColorBrush)GetValue(OverrideDefaultWindowCommandsBrushProperty); }
            set { SetValue(OverrideDefaultWindowCommandsBrushProperty, value); }
        }

        public static readonly AvaloniaProperty OverrideDefaultWindowCommandsBrushProperty=
            AvaloniaProperty.Register<MetroWindow, SolidColorBrush>(nameof(OverrideDefaultWindowCommandsBrush));

        public bool IsWindowDraggable
        {
            get { return (bool)GetValue(IsWindowDraggableProperty); }
            set { SetValue(IsWindowDraggableProperty, value); }
        }

        public static readonly AvaloniaProperty IsWindowDraggableProperty =
            AvaloniaProperty.Register<MetroWindow, bool>(nameof(IsWindowDraggable), defaultValue: true);

        Type IStyleable.StyleKey => typeof(MetroWindow);

        private void ToggleWindowState()
        {
            var oldValue = WindowState;
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;

                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
            }
            RaisePropertyChanged(WindowStateProperty, oldValue, WindowState, Data.BindingPriority.TemplatedParent);
        }

        internal void HandleFlyoutStatusChange(Flyout flyout, List<Flyout> visibleFlyouts)
        {
            //checks a recently opened flyout's position.
            //if (flyout.Position == Position.Left || flyout.Position == Position.Right || flyout.Position == Position.Top)
            {
                //get it's zindex
                var zIndex = flyout.IsOpen ? flyout.ZIndex + 3 : visibleFlyouts.Count() + 2;

                //if the the corresponding behavior has the right flag, set the window commands' and icon zIndex to a number that is higher than the flyout's.
                this._icon?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : (this.IconOverlayBehavior.HasFlag(OverlayBehavior.Flyouts) ? zIndex : 1));
                this.LeftWindowCommandsPresenter?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : 1);
                this.RightWindowCommandsPresenter?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : 1);
                this.WindowButtonCommandsPresenter?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : (this.WindowButtonCommandsOverlayBehavior.HasFlag(OverlayBehavior.Flyouts) ? zIndex : 1));
                this.HandleWindowCommandsForFlyouts(visibleFlyouts);
            }

            if (this.flyoutModal != null)
            {
                this.flyoutModal.IsVisible = visibleFlyouts.Any(x => x.IsModal) ? true : false;
            }

            //flyout.IsVisible = true;

            RaiseEvent(new FlyoutStatusChangedRoutedEventArgs(FlyoutsStatusChangedEvent, this) { ChangedFlyout = flyout });
        }

        /// <inheritdoc/>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            if (_topHorizontalGrip != null && _topHorizontalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.North);
            }
            else if (_bottomHorizontalGrip != null && _bottomHorizontalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.South);
            }
            else if (_leftVerticalGrip != null && _leftVerticalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.West);
            }
            else if (_rightVerticalGrip != null && _rightVerticalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.East);
            }
            else if (_topLeftGrip != null && _topLeftGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.NorthWest);
            }
            else if (_bottomLeftGrip != null && _bottomLeftGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.SouthWest);
            }
            else if (_topRightGrip != null && _topRightGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.NorthEast);
            }
            else if (_bottomRightGrip != null && _bottomRightGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.SouthEast);
            }
            else if (_titleBar != null && _titleBar.IsPointerOver)
            {
                _mouseDown = true;
                _mouseDownPosition = e.GetPosition(this);
            }
            else
            {
                _mouseDown = false;
            }

            base.OnPointerPressed(e);
        }

        /// <inheritdoc/>
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            _mouseDown = false;
            base.OnPointerReleased(e);
        }

        /// <inheritdoc/>
        //protected override void OnPointerMoved(PointerEventArgs e)
        //{
        //    if (titleBar != null && titleBar.IsPointerOver && _mouseDown)
        //    {
        //        WindowState = WindowState.Normal;
        //        BeginMoveDrag();
        //        _mouseDown = false;
        //    }
        //    base.OnPointerMoved(e);
        //}

        public MetroWindow()
        {
            ShowIconOnTitleBarProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnShowIconOnTitleBarPropertyChangedCallback(o, e));
            ShowTitleBarProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnShowTitleBarPropertyChangedCallback(o, e));
            TitleBarHeightProperty.Changed.AddClassHandler<MetroWindow>((o, e) => TitleBarHeightPropertyChangedCallback(o, e));
            //TitleCharacterCasingProperty.Changed.AddClassHandler<MetroWindow>((o, e) => TitleCharacterCasingChangedCallback(o, e));
            TitleAlignmentProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnTitleAlignmentChanged(o, e));

            FlyoutsProperty.Changed.AddClassHandler<MetroWindow>((o, e) => UpdateLogicalChilds(o, e));
            LeftWindowCommandsProperty.Changed.AddClassHandler<MetroWindow>((o, e) => UpdateLogicalChilds(o, e));
            RightWindowCommandsProperty.Changed.AddClassHandler<MetroWindow>((o, e) => UpdateLogicalChilds(o, e));
            WindowButtonCommandsProperty.Changed.AddClassHandler<MetroWindow>((o, e) => UpdateLogicalChilds(o, e));
            LeftWindowCommandsOverlayBehaviorProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnShowTitleBarPropertyChangedCallback(o, e));
            RightWindowCommandsOverlayBehaviorProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnShowTitleBarPropertyChangedCallback(o, e));
            WindowButtonCommandsOverlayBehaviorProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnShowTitleBarPropertyChangedCallback(o, e));
            IconOverlayBehaviorProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnShowTitleBarPropertyChangedCallback(o, e));
            UseNoneWindowStyleProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnUseNoneWindowStylePropertyChangedCallback(o, e));

            WidthProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnWidthChanged(o, e));
            HeightProperty.Changed.AddClassHandler<MetroWindow>((o, e) => OnHeightChanged(o, e));

            if (Flyouts == null)
            {
                Flyouts = new FlyoutsControl();
            }

            ThemeManager.Instance.IsThemeChanged += ThemeManagerOnIsThemeChanged;

            SetVisibiltyForAllTitleElements();
        }

        private void OnWidthChanged(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;

            RaiseEvent(new RoutedEventArgs(SizeChangedEvent));
        }

        private void OnHeightChanged(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;
            RaiseEvent(new RoutedEventArgs(SizeChangedEvent));
        }

        private void OnTitleAlignmentChanged(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            o.SizeChanged -= o.MetroWindow_SizeChanged;
            if (e.NewValue is HorizontalAlignment && (HorizontalAlignment)e.NewValue == HorizontalAlignment.Center && o._titleBar != null)
            {
                o.SizeChanged += o.MetroWindow_SizeChanged;
            }
        }

        //private void TitleCharacterCasingChangedCallback(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        //{
        //    if(e.NewValue is CharacterCasing)
        //    {
        //    }

        //    //value => CharacterCasing.Normal <= (CharacterCasing)value && (CharacterCasing)value <= CharacterCasing.Upper

        //}

        private void TitleBarHeightPropertyChangedCallback(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                o.SetVisibiltyForAllTitleElements();
            }
        }

        private void OnShowIconOnTitleBarPropertyChangedCallback(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                SetVisibiltyForIcon();
            }
        }

        private void SetVisibiltyForIcon()
        {
            if (_icon != null)
            {
                var isVisible = (this.IconOverlayBehavior.HasFlag(OverlayBehavior.HiddenTitleBar) && !this.ShowTitleBar)
                                || (this.ShowIconOnTitleBar && this.ShowTitleBar);

                _icon.IsVisible = isVisible;
            }
        }

        private void OnUseNoneWindowStylePropertyChangedCallback(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                // if UseNoneWindowStyle = true no title bar should be shown
                var useNoneWindowStyle = (bool)e.NewValue;

                // UseNoneWindowStyle means no title bar, window commands or min, max, close buttons
                if (useNoneWindowStyle)
                {
                    o.SetValue(ShowTitleBarProperty, false);
                }
            }
        }

        private void OnShowTitleBarPropertyChangedCallback(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                o.SetVisibiltyForAllTitleElements();
            }
        }

        private void ThemeManagerOnIsThemeChanged(object sender, OnThemeChangedEventArgs e)
        {
            if (e.Theme != null)
            {
                var flyouts = this.Flyouts.GetFlyouts().ToList();
                // since we disabled the ThemeManager OnThemeChanged part, we must change all children flyouts too
                // e.g if the FlyoutsControl is hosted in a UserControl
                var allChildFlyouts = (this.Content as IVisual).GetSelfAndVisualDescendants().OfType<FlyoutsControl>().ToList();
                if (allChildFlyouts.Any())
                {
                    flyouts.AddRange(allChildFlyouts.SelectMany(flyoutsControl => flyoutsControl.GetFlyouts()));
                }

                if (!flyouts.Any())
                {
                    // we must update the window command brushes!!!
                    this.ResetAllWindowCommandsBrush();
                    return;
                }

                foreach (var flyout in flyouts)
                {
                    flyout.ChangeFlyoutTheme(e.Theme);
                }
                this.HandleWindowCommandsForFlyouts(flyouts);
            }
        }

        private void SetVisibiltyForAllTitleElements()
        {
            this.SetVisibiltyForIcon();
            var newVisibility = this.TitleBarHeight > 0 && this.ShowTitleBar && !this.UseNoneWindowStyle ? true : false;

            this._titleBar?.SetValue(IsVisibleProperty, newVisibility);
            this._titleBarBackground?.SetValue(IsVisibleProperty, newVisibility);

            var leftWindowCommandsVisibility = this.LeftWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) && !this.UseNoneWindowStyle ? true : newVisibility;
            this.LeftWindowCommandsPresenter?.SetValue(IsVisibleProperty, leftWindowCommandsVisibility);

            var rightWindowCommandsVisibility = this.RightWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) && !this.UseNoneWindowStyle ? true : newVisibility;
            this.RightWindowCommandsPresenter?.SetValue(IsVisibleProperty, rightWindowCommandsVisibility);

            var windowButtonCommandsVisibility = this.WindowButtonCommandsOverlayBehavior.HasFlag(OverlayBehavior.HiddenTitleBar) ? true : newVisibility;
            this.WindowButtonCommandsPresenter?.SetValue(IsVisibleProperty, windowButtonCommandsVisibility);

            this.SetWindowEvents();
        }

        private void SetWindowEvents()
        {
            // clear all event handlers first
            this.ClearWindowEvents();

            // set mouse down/up for icon
            if (_icon != null && _icon.IsVisible == true)
            {
                _icon.PointerPressed += IconMouseDown;
            }

            if (this._windowTitleThumb != null)
            {
                //this.windowTitleThumb.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                this._windowTitleThumb.DragDelta += this.WindowTitleThumbMoveOnDragDelta;
                this._windowTitleThumb.DoubleTapped += this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                this._windowTitleThumb.PointerReleased += this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            var thumbContentControl = this._titleBar as IMetroThumb;
            if (thumbContentControl != null)
            {
                //thumbContentControl.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                thumbContentControl.DragDelta += this.WindowTitleThumbMoveOnDragDelta;
                thumbContentControl.DoubleTapped += this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                thumbContentControl.PointerReleased += this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (this._flyoutModalDragMoveThumb != null)
            {
                //this.flyoutModalDragMoveThumb.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                this._flyoutModalDragMoveThumb.DragDelta += this.WindowTitleThumbMoveOnDragDelta;
                this._flyoutModalDragMoveThumb.DoubleTapped += this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                this._flyoutModalDragMoveThumb.PointerReleased += this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }

            // handle size if we have a Grid for the title (e.g. clean window have a centered title)
            //if (titleBar != null && titleBar.GetType() == typeof(Grid))
            if (_titleBar != null && TitleAlignment == HorizontalAlignment.Center)
            {
                this.SizeChanged += this.MetroWindow_SizeChanged;
            }
        }

        private void WindowTitleThumbChangeWindowStateOnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(this, e);
        }

        private void MetroWindow_SizeChanged(object sender, EventArgs e)
        {
            // this all works only for centered title
            if (TitleAlignment != HorizontalAlignment.Center)
            {
                return;
            }

            // Half of this MetroWindow
            var halfDistance = this.Width / 2;
            // Distance between center and left/right
            var margin = (Thickness)this._titleBar.GetValue(MarginProperty);
            var distanceToCenter = (this._titleBar.DesiredSize.Width - margin.Left - margin.Right) / 2;

            var iconWidth = this._icon?.Width ?? 0;
            var leftWindowCommandsWidth = this.LeftWindowCommands?.Width ?? 0;
            var rightWindowCommandsWidth = this.RightWindowCommands?.Width ?? 0;
            var windowButtonCommandsWith = this.WindowButtonCommands?.Width ?? 0;

            // Distance between right edge from LeftWindowCommands to left window side
            var distanceFromLeft = iconWidth + leftWindowCommandsWidth;
            // Distance between left edge from RightWindowCommands to right window side
            var distanceFromRight = rightWindowCommandsWidth + windowButtonCommandsWith;
            // Margin
            const double horizontalMargin = 5.0;

            var dLeft = distanceFromLeft + distanceToCenter + horizontalMargin;
            var dRight = distanceFromRight + distanceToCenter + horizontalMargin;
            if ((dLeft < halfDistance) && (dRight < halfDistance))
            {
                this._titleBar.SetValue(MarginProperty, default(Thickness));
                Grid.SetColumn(this._titleBar, 0);
                Grid.SetColumnSpan(this._titleBar, 5);
            }
            else
            {
                this._titleBar.SetValue(MarginProperty, new Thickness(leftWindowCommandsWidth, 0, rightWindowCommandsWidth, 0));
                Grid.SetColumn(this._titleBar, 2);
                Grid.SetColumnSpan(this._titleBar, 1);
            }
        }

        private void WindowTitleThumbSystemMenuOnMouseRightButtonUp(object sender, PointerReleasedEventArgs e)
        {
            if (e.MouseButton != MouseButton.Right)
                return;

            DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(this, e);
        }

        public void DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(MetroWindow metroWindow, PointerReleasedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        internal static void DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(MetroWindow metroWindow, RoutedEventArgs mouseButtonEventArgs)
        {
            // restore/maximize only with left button
            //if (mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                // we can maximize or restore the window if the title bar height is set (also if title bar is hidden)
                var canResize = metroWindow.CanResize;//.ResizeMode == ResizeMode.CanResizeWithGrip || metroWindow.ResizeMode == ResizeMode.CanResize;
                //var mousePos = Mouse.GetPosition(window);
                //var isMouseOnTitlebar = mousePos.Y <= window.TitleBarHeight && window.TitleBarHeight > 0;
                if (canResize /*&& isMouseOnTitlebar*/)
                {
                    metroWindow.ToggleWindowState();

                    mouseButtonEventArgs.Handled = true;
                }
            }
        }

        private void WindowTitleThumbMoveOnDragDelta(object sender, VectorEventArgs e)
        {
            DoWindowTitleThumbMoveOnDragDelta(sender as IMetroThumb, this, e);
        }

        private void IconMouseDown(object sender, PointerPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClearWindowEvents()
        {
            // clear all event handlers first:
            if (this._windowTitleThumb != null)
            {
                //this.windowTitleThumb.PreviewMouseLeftButtonUp -= this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                this._windowTitleThumb.DragDelta -= this.WindowTitleThumbMoveOnDragDelta;
                this._windowTitleThumb.PointerPressed -= this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                this._windowTitleThumb.PointerReleased -= this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            var thumbContentControl = this._titleBar as IMetroThumb;
            if (thumbContentControl != null)
            {
                //thumbContentControl.PreviewMouseLeftButtonUp -= this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                thumbContentControl.DragDelta -= this.WindowTitleThumbMoveOnDragDelta;
                thumbContentControl.PointerPressed -= this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                thumbContentControl.PointerReleased -= this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (this._flyoutModalDragMoveThumb != null)
            {
                //this.flyoutModalDragMoveThumb.PreviewMouseLeftButtonUp -= this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                this._flyoutModalDragMoveThumb.DragDelta -= this.WindowTitleThumbMoveOnDragDelta;
                this._flyoutModalDragMoveThumb.PointerPressed -= this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                this._flyoutModalDragMoveThumb.PointerReleased -= this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (_icon != null)
            {
                _icon.PointerPressed -= IconMouseDown;
            }
            this.SizeChanged -= this.MetroWindow_SizeChanged;
        }

        private void UpdateLogicalChilds(MetroWindow o, AvaloniaPropertyChangedEventArgs e)
        {
            var oldChild = e.OldValue as StyledElement;
            if (oldChild != null)
            {
                //this.RemoveLogicalChild(oldChild);
                this.LogicalChildren.Remove(oldChild);
            }

            var newChild = e.NewValue as StyledElement;
            if (newChild != null)
            {
                this.LogicalChildren.Add(newChild);
                // Yes, that's crazy. But we must do this to enable all possible scenarios for setting DataContext
                // in a Window. Without set the DataContext at this point it can happen that e.g. a Flyout
                // doesn't get the same DataContext.
                // So now we can type
                //
                // this.InitializeComponent();
                // this.DataContext = new MainViewModel();
                //
                // or
                //
                // this.DataContext = new MainViewModel();
                // this.InitializeComponent();
                //
                newChild.DataContext = this.DataContext;
            }
        }

        internal static void DoWindowTitleThumbMoveOnDragDelta(IMetroThumb thumb, MetroWindow window, VectorEventArgs dragDeltaEventArgs)
        {
            if (thumb == null)
            {
                throw new ArgumentNullException(nameof(thumb));
            }
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            // drag only if IsWindowDraggable is set to true
            if (!window.IsWindowDraggable ||
                (!(Math.Abs(dragDeltaEventArgs.Vector.Y) > 2) && !(Math.Abs(dragDeltaEventArgs.Vector.X) > 2)))
            {
                return;
            }

            // tage from DragMove internal code
            window.VerifyAccess();

            //var cursorPos = WinApiHelper.GetPhysicalCursorPos();

            // if the window is maximized dragging is only allowed on title bar (also if not visible)
            var windowIsMaximized = window.WindowState == WindowState.Maximized;

            if (window._titleBar != null && window._titleBar.IsPointerOver /*&& _mouseDown*/)
            {
                window.WindowState = WindowState.Normal;
                window.BeginMoveDrag();
                window._mouseDown = false;
            }

            //var isMouseOnTitlebar = Mouse.GetPosition(thumb).Y <= window.TitleBarHeight && window.TitleBarHeight > 0;
            //if (!isMouseOnTitlebar && windowIsMaximized)
            //{
            //    return;
            //}

#pragma warning disable 618
            // for the touch usage
            //UnsafeNativeMethods.ReleaseCapture();
#pragma warning restore 618

            if (windowIsMaximized)
            {
                //var cursorXPos = cursorPos.x;
                EventHandler windowOnStateChanged = null;
                windowOnStateChanged = (sender, args) =>
                {
                    //window.Top = 2;
                    //window.Left = Math.Max(cursorXPos - window.RestoreBounds.Width / 2, 0);

                    //window.StateChanged -= windowOnStateChanged;
                    //if (window.WindowState == WindowState.Normal)
                    //{
                    //    Mouse.Capture(thumb, CaptureMode.Element);
                    //}
                };
                //window.StateChanged -= windowOnStateChanged;
                //window.StateChanged += windowOnStateChanged;
            }

//            var criticalHandle = window.CriticalHandle;
//#pragma warning disable 618
//            // these lines are from DragMove
//            // NativeMethods.SendMessage(criticalHandle, WM.SYSCOMMAND, (IntPtr)SC.MOUSEMOVE, IntPtr.Zero);
//            // NativeMethods.SendMessage(criticalHandle, WM.LBUTTONUP, IntPtr.Zero, IntPtr.Zero);

//            var wpfPoint = window.PointToScreen(Mouse.GetPosition(window));
//            var x = (int)wpfPoint.X;
//            var y = (int)wpfPoint.Y;
//            NativeMethods.SendMessage(criticalHandle, WM.NCLBUTTONDOWN, (IntPtr)HT.CAPTION, new IntPtr(x | (y << 16)));
//#pragma warning restore 618
        }

        /// <inheritdoc/>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            LeftWindowCommandsPresenter = e.NameScope.Find<ContentPresenter>(PART_LeftWindowCommands);
            RightWindowCommandsPresenter = e.NameScope.Find<ContentPresenter>(PART_RightWindowCommands);
            WindowButtonCommandsPresenter = e.NameScope.Find<ContentPresenter>(PART_WindowButtonCommands);

            if (LeftWindowCommands == null)
                LeftWindowCommands = new WindowCommands();
            if (RightWindowCommands == null)
                RightWindowCommands = new WindowCommands();
            if (WindowButtonCommands == null)
                WindowButtonCommands = new WindowButtonCommands();

            LeftWindowCommands.ParentWindow = this;
            RightWindowCommands.ParentWindow = this;
            WindowButtonCommands.ParentWindow = this;
            overlayBox = e.NameScope.Find<Grid>(PART_OverlayBox);
            metroActiveDialogContainer = e.NameScope.Find<Grid>(PART_MetroActiveDialogContainer);
            metroInactiveDialogContainer = e.NameScope.Find<Grid>(PART_MetroInactiveDialogsContainer);
            flyoutModal = e.NameScope.Find<Rectangle>(PART_FlyoutModal);
            //flyoutModal.MouseDown += FlyoutsPreviewMouseDown;
            //this.PreviewMouseDown += FlyoutsPreviewMouseDown;

            _icon = e.NameScope.Find<ContentControl>(PART_Icon);
            _titleBar = e.NameScope.Find<ContentControl>(PART_TitleBar);
            _titleBarBackground = e.NameScope.Find<Rectangle>(PART_WindowTitleBackground);
            _windowTitleThumb = e.NameScope.Find<Thumb>(PART_WindowTitleThumb);
            _flyoutModalDragMoveThumb= e.NameScope.Find<Thumb>(PART_FlyoutModalDragMoveThumb);
            SetVisibiltyForAllTitleElements();

            var metroContentControl = e.NameScope.Find<MetroContentControl>(PART_Content);
            if (metroContentControl != null)
            {
                metroContentControl.TransitionCompleted += (sender, args) => this.RaiseEvent(new RoutedEventArgs(WindowTransitionCompletedEvent));
            }

            _topHorizontalGrip = e.NameScope.Find<Grid>(PART_TopHorizontalGrip);
            _bottomHorizontalGrip = e.NameScope.Find<Grid>(PART_BottomHorizontalGrip);
            _leftVerticalGrip = e.NameScope.Find<Grid>(PART_LeftVerticalGrip);
            _rightVerticalGrip = e.NameScope.Find<Grid>(PART_RightVerticalGrip);

            _topLeftGrip = e.NameScope.Find<Grid>(PART_TopLeftGrip);
            _bottomLeftGrip = e.NameScope.Find<Grid>(PART_BottomLeftGrip);
            _topRightGrip = e.NameScope.Find<Grid>(PART_TopRightGrip);
            _bottomRightGrip = e.NameScope.Find<Grid>(PART_BottomRightGrip);
        }
    }
}