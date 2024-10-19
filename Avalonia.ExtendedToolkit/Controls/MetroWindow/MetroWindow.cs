using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// Interaction logic for <see cref="MetroWindow"/> xaml.
    /// </summary>
    public partial class MetroWindow : Window, IStyleable
    {

        private static Pointer dummyMovePointer = null;
        private static PointerPressedEventArgs dummyPointerPressedEventArgs = null;

#warning check commented code

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
            //.obsolete, WindowStateProperty is styled
            //RaisePropertyChanged(WindowStateProperty, oldValue, Data.BindingPriority.TemplatedParent);
        }

        internal void HandleFlyoutStatusChange(Flyout flyout, List<Flyout> visibleFlyouts)
        {
            //checks a recently opened flyout's position.
            //if (flyout.Position == Position.Left || flyout.Position == Position.Right || flyout.Position == Position.Top)
            {
                //get it's zindex
                var zIndex = flyout.IsOpen ? flyout.ZIndex + 3 : visibleFlyouts.Count + 2;

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
            if (Design.IsDesignMode)
                return;

            if (_topHorizontalGrip != null && _topHorizontalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.North, e);
            }
            else if (_bottomHorizontalGrip != null && _bottomHorizontalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.South, e);
            }
            else if (_leftVerticalGrip != null && _leftVerticalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.West, e);
            }
            else if (_rightVerticalGrip != null && _rightVerticalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.East, e);
            }
            else if (_topLeftGrip != null && _topLeftGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.NorthWest, e);
            }
            else if (_bottomLeftGrip != null && _bottomLeftGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.SouthWest, e);
            }
            else if (_topRightGrip != null && _topRightGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.NorthEast, e);
            }
            else if (_bottomRightGrip != null && _bottomRightGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.SouthEast, e);
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
            if (Design.IsDesignMode)
                return;

            _mouseDown = false;
            base.OnPointerReleased(e);
        }

        /// <inheritdoc/>
        //protected override void OnPointerMoved(PointerEventArgs e)
        //{
        //    if (_titleBar != null && _titleBar.IsPointerOver && _mouseDown)
        //    {
        //        WindowState = WindowState.Normal;
        //        BeginMoveDrag(e);
        //        _mouseDown = false;
        //    }
        //    base.OnPointerMoved(e);
        //}

        public MetroWindow()
        {
            if (Design.IsDesignMode == false)
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
                ThemeManager.Instance.IsThemeChanged += ThemeManagerOnIsThemeChanged;

                SetVisibiltyForAllTitleElements();

                if (Flyouts == null)
                {
                    Flyouts = new FlyoutsControl();
                }



            }


        }

        /// <summary>
        /// !!!hack so the controls are drawn correctly!!!
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

#warning remove this hack if drawing is fixed
            //workaround for so the window is correctly displayed

            var tempState = this.WindowState;
            this.WindowState = WindowState.Maximized;
            this.WindowState = tempState;

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
                var allChildFlyouts = (this.Content as Visual)?.GetSelfAndVisualDescendants().OfType<FlyoutsControl>().ToList();
                if (allChildFlyouts?.Any()==true)
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
            if (Design.IsDesignMode)
                return;

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
            if (e.InitialPressMouseButton != MouseButton.Right)
                return;

            DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(this, e);
        }

        internal void DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(MetroWindow metroWindow, PointerReleasedEventArgs e)
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

            if (window._titleBar != null && window._titleBar.IsPointerOver /*&& window._mouseDown*/)
            {
                window.WindowState = WindowState.Normal;

                try
                {
                    if (dummyMovePointer == null)
                    {
                        dummyMovePointer = 
                            new Pointer(0, PointerType.Mouse, true);
                    }

                    if (dummyPointerPressedEventArgs == null)
                    {
                        //just create a dummy PointerPressedEventArgs
                        dummyPointerPressedEventArgs = new PointerPressedEventArgs
                        (
                            window,
                            dummyMovePointer,
                            window,
                            new Point(1, 1),
                            0,
                            new PointerPointProperties(RawInputModifiers.None,
                                    PointerUpdateKind.LeftButtonPressed),
                            KeyModifiers.None
                        );
                    }
                    //.later, seems like we need to implement it for differents OS`s, brrrr
                    //window.PlatformImpl.BeginMoveDrag(dummyPointerPressedEventArgs);
                }
                catch
                {

                }
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
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

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
            flyoutModal.PointerPressed += FlyoutsPreviewMouseDown;
            //flyoutModal.MouseDown += FlyoutsPreviewMouseDown;
            this.PointerPressed += FlyoutsPreviewMouseDown;

            _icon = e.NameScope.Find<ContentControl>(PART_Icon);
            _titleBar = e.NameScope.Find<ContentControl>(PART_TitleBar);
            _titleBarBackground = e.NameScope.Find<Rectangle>(PART_WindowTitleBackground);
            _windowTitleThumb = e.NameScope.Find<Thumb>(PART_WindowTitleThumb);
            _flyoutModalDragMoveThumb = e.NameScope.Find<Thumb>(PART_FlyoutModalDragMoveThumb);
            SetVisibiltyForAllTitleElements();

            var metroContentControl = e.NameScope.Find<MetroContentControl>(PART_Content);
            if (metroContentControl != null)
            {
                if (Design.IsDesignMode == false)
                {
                    metroContentControl.TransitionCompleted += (sender, args) => this.RaiseEvent(new RoutedEventArgs(WindowTransitionCompletedEvent));
                }
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

        private void FlyoutsPreviewMouseDown(object sender, PointerPressedEventArgs e)
        {
            var element = (e.Source as Control);
            if (element != null)
            {
                // no preview if we just clicked these elements
                if (element.TryFindParent<Flyout>() != null
                    || Equals(element, this.overlayBox)
                    //|| element.TryFindParent<BaseMetroDialog>() != null
                    || Equals(element.TryFindParent<ContentControl>(), this.Icon)
                    || element.TryFindParent<WindowCommands>() != null
                    || element.TryFindParent<WindowButtonCommands>() != null)
                {
                    return;
                }
            }

            //don't know how to do this in avalonia e.ChangedButton
            //miaaing in the event.

            //if (Flyouts.OverrideExternalCloseButton == null)
            //{
            //    foreach (var flyout in Flyouts.GetFlyouts().
            //        Where(x => x.IsOpen && x.ExternalCloseButton == e.ChangedButton
            //        && (!x.IsPinned || Flyouts.OverrideIsPinned)))
            //    {
            //        flyout.IsOpen = false;
            //    }
            //}
            //else if (Flyouts.OverrideExternalCloseButton == e.ChangedButton)
            //{
            //    foreach (var flyout in Flyouts.GetFlyouts().Where(x => x.IsOpen &&
            //    (!x.IsPinned || Flyouts.OverrideIsPinned)))
            //    {
            //        flyout.IsOpen = false;
            //    }
            //}
        }
    }
}
