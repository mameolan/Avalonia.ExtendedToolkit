using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// A sliding panel control that is hosted in a MetroWindow via a FlyoutsControl.
    /// <see cref="MetroWindow"/>
    /// <seealso cref="FlyoutsControl"/>
    /// </summary>
    public partial class Flyout : HeaderedContentControl
    {
        /// <summary>
        /// initilaze classhandlers
        /// autoclose timer
        /// </summary>
        public Flyout()
        {
            PositionProperty.Changed.AddClassHandler<Flyout>((o, e) => PositionChanged(o, e));
            AnimateOpacityProperty.Changed.AddClassHandler<Flyout>((o, e) => AnimateOpacityChanged(o, e));
            FlyoutThemeProperty.Changed.AddClassHandler<Flyout>((o, e) => ThemeChanged(o, e));
            IsAutoCloseEnabledProperty.Changed.AddClassHandler<Flyout>((o, e) => IsAutoCloseEnabledChanged(o, e));
            AutoCloseIntervalProperty.Changed.AddClassHandler<Flyout>((o, e) => AutoCloseIntervalChanged(o, e));
            ThemeManager.Instance.IsThemeChanged += (o, e) => { UpdateFlyoutTheme(); };
            InitializeAutoCloseTimer();
            IsOpenProperty.Changed.AddClassHandler<Flyout>((o, e) => IsOpenedChanged(o, e));

            WidthProperty.Changed.AddClassHandler<Flyout>((o,e)=> SizeChanged(o,e));
            HeightProperty.Changed.AddClassHandler<Flyout>((o, e) => SizeChanged(o, e));
        }

        private void SizeChanged(Flyout o, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.Property.Name== nameof(Width))
            {
                _currentSize=new Size((double)e.NewValue, _currentSize.Height);
            }
            else
            {
                _currentSize = new Size(_currentSize.Width, (double)e.NewValue);
            }
        }

        private MetroWindow parentWindow;
        private Size _currentSize;

        private MetroWindow ParentWindow => this.parentWindow ?? (this.parentWindow = this.TryFindParent<MetroWindow>());

        /// <summary>
        /// remembers available size
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            _currentSize = availableSize;
            return base.MeasureOverride(availableSize);
        }

        private void InitializeAutoCloseTimer()
        {
            this.StopAutoCloseTimer();

            this.autoCloseTimer = new DispatcherTimer();
            this.autoCloseTimer.Tick += this.AutoCloseTimerCallback;
            this.autoCloseTimer.Interval = TimeSpan.FromMilliseconds(this.AutoCloseInterval);
        }

        private void UpdateFlyoutTheme()
        {
            var flyoutsControl = this.TryFindParent<FlyoutsControl>();

            if (Design.IsDesignMode)
            {
                IsVisible = flyoutsControl != null ? false : true;
            }

            var window = this.ParentWindow;
            if (window != null)
            {
                Theme windowTheme = DetectTheme(this);

                if (windowTheme != null)
                {
                    this.ChangeFlyoutTheme(windowTheme);
                }

                // we must certain to get the right foreground for window commands and buttons
                if (flyoutsControl != null && this.IsOpen)
                {
                    flyoutsControl.HandleFlyoutStatusChange(this, window);
                }
            }
        }

        internal void ChangeFlyoutTheme(Theme windowTheme)
        {
            //ApplyTemplate();

            // Beware: Über-dumb code ahead!
            switch (this.FlyoutTheme)
            {
                case FlyoutTheme.Accent:
                    ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles, windowTheme);
                    this.OverrideFlyoutResources(this.Styles, true);
                    break;

                case FlyoutTheme.Adapt:
                    ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles, windowTheme);
                    this.OverrideFlyoutResources(this.Styles);
                    break;

                case FlyoutTheme.Inverse:
                    var inverseTheme = ThemeManager.Instance.GetInverseTheme(windowTheme);

                    if (inverseTheme == null)
                        throw new InvalidOperationException("The inverse flyout theme only works if the window" +
                                                             " theme abides the naming convention. " +
                                                            "See ThemeManager.GetInverseAppTheme for more infos");

                    ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles, inverseTheme);
                    this.OverrideFlyoutResources(this.Styles);
                    break;

                case FlyoutTheme.Dark:
                    ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles,
                        ThemeManager.Instance.Themes.First(x => x.BaseColorScheme == ThemeManager.BaseColorDark
                        && x.ColorScheme == windowTheme.ColorScheme));

                    this.OverrideFlyoutResources(this.Styles);
                    break;

                case FlyoutTheme.Light:
                    ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles,
                        ThemeManager.Instance.Themes.First(x => x.BaseColorScheme == ThemeManager.BaseColorLight
                        && x.ColorScheme == windowTheme.ColorScheme));
                    this.OverrideFlyoutResources(this.Styles);
                    break;
            }
        }

        private void OverrideFlyoutResources(Styles styles, bool accent = false)
        {
            var fromColorKey = accent ? "MahApps.Colors.Highlight" : "MahApps.Colors.Flyout";
            var item= styles.GetThemeStyle();
            styles.Remove(item);

            ResourceDictionary resources = (item.Loaded as Style).Resources as ResourceDictionary;
            //.edited
            item.TryGetResource(fromColorKey, ThemeVariant.Default, out object result);
            var fromColor = (Color)result;

            resources["MahApps.Colors.White"] = fromColor;
            resources["MahApps.Colors.Flyout"] = fromColor;

            var newBrush = new SolidColorBrush(fromColor);
            //newBrush.Freeze();
            resources["MahApps.Brushes.Flyout.Background"] = newBrush;
            resources["MahApps.Brushes.Control.Background"] = newBrush;
            resources["MahApps.Brushes.White"] = newBrush;
            resources["MahApps.Brushes.WhiteColor"] = newBrush;
            resources["MahApps.Brushes.DisabledWhite"] = newBrush;
            resources["MahApps.Brushes.Window.Background"] = newBrush;
            resources["ThemeBackgroundBrush"] = newBrush;

            if (accent)
            {
                fromColor = (Color)resources["MahApps.Colors.IdealForeground"];
                newBrush = new SolidColorBrush(fromColor);
            //    newBrush.Freeze();
                resources["MahApps.Brushes.Flyout.Foreground"] = newBrush;
                resources["MahApps.Brushes.Text"] = newBrush;
                resources["MahApps.Brushes.Label.Text"] = newBrush;

                if (resources.ContainsKey("MahApps.Colors.AccentBase"))
                {
                    fromColor = (Color)resources["MahApps.Colors.AccentBase"];
                }
                else
                {
                    var accentColor = (Color)resources["MahApps.Colors.Accent"];
                    fromColor = Color.FromArgb(255, accentColor.R, accentColor.G, accentColor.B);
                }
                newBrush = new SolidColorBrush(fromColor);
            //    newBrush.Freeze();
                resources["MahApps.Colors.Highlight"] = fromColor;
                resources["MahApps.Brushes.Highlight"] = newBrush;
                resources["MahApps.Brushes.Highlight"] = newBrush;
            }

            styles.Add(item);

            //resources.EndInit();
        }

        private static Theme DetectTheme(Flyout flyout)
        {
            if (flyout == null)
                return null;

            // first look for owner
            var window = flyout.ParentWindow;
            var theme = window != null ? ThemeManager.Instance.DetectTheme(window) : null;
            if (theme != null)
            {
                return theme;
            }

            // second try, look for main window
            if (Application.Current != null)
            {
                var mainWindow = (Application.Current as
                    IClassicDesktopStyleApplicationLifetime)?.MainWindow as MetroWindow;
                theme = mainWindow != null ? ThemeManager.Instance.DetectTheme(mainWindow) : null;
                if (theme != null)
                {
                    return theme;
                }

                // oh no, now look at application resource
                theme = ThemeManager.Instance.DetectTheme(Application.Current);
                if (theme != null)
                {
                    return theme;
                }
            }
            return null;
        }

        private void UpdateOpacityChange()
        {
            if (this.flyoutRoot == null  || Design.IsDesignMode)
            {
                return;
            }
            if (!this.AnimateOpacity)
            {
                FadeOutFrameOpacity = 1;
                this.flyoutRoot.Opacity = 1;
            }
            else
            {
                FadeOutFrameOpacity = 0;
                if (!this.IsOpen) this.flyoutRoot.Opacity = 0;
            }
        }

        /// <summary>
        /// gets the controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            this.flyoutRoot = e.NameScope.Find<Border>("PART_Root");
            if (this.flyoutRoot == null)
            {
                return;
            }

            this.flyoutHeader = e.NameScope.Find<MetroThumbContentControl>("PART_Header");
            this.flyoutHeader?.ApplyTemplate();
            this.flyoutContent = e.NameScope.Find<ContentPresenter>("PART_Content");

            var thumbContentControl = this.flyoutHeader as IMetroThumb;
            if (thumbContentControl != null)
            {
                thumbContentControl.DragStarted -= this.WindowTitleThumbOnDragStarted;
                thumbContentControl.DragCompleted -= this.WindowTitleThumbOnDragCompleted;
                //thumbContentControl.PreviewMouseLeftButtonUp -= this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                thumbContentControl.DragDelta -= this.WindowTitleThumbMoveOnDragDelta;
                thumbContentControl.DoubleTapped -= this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                thumbContentControl.PointerReleased -= this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;

                var flyoutsControl = this.TryFindParent<FlyoutsControl>();
                if (flyoutsControl != null)
                {
                    thumbContentControl.DragStarted += this.WindowTitleThumbOnDragStarted;
                    thumbContentControl.DragCompleted += this.WindowTitleThumbOnDragCompleted;
                    //thumbContentControl.PreviewMouseLeftButtonUp += this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                    thumbContentControl.DragDelta += this.WindowTitleThumbMoveOnDragDelta;
                    thumbContentControl.DoubleTapped += this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                    thumbContentControl.PointerReleased += this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
                }
            }

            //this.hideStoryboard = this.GetTemplateChild("HideStoryboard") as Storyboard;
            //this.hideFrame = e.NameScope.Find<KeyFrameExt>("hideFrame") as KeyFrameExt;
            //this.hideFrameY = this.GetTemplateChild("hideFrameY") as SplineDoubleKeyFrame;
            //this.showFrame = this.GetTemplateChild("showFrame") as SplineDoubleKeyFrame;
            //this.showFrameY = this.GetTemplateChild("showFrameY") as SplineDoubleKeyFrame;
            //this.fadeOutFrame = this.GetTemplateChild("fadeOutFrame") as SplineDoubleKeyFrame;

            //if (this.hideFrame == null || this.showFrame == null || this.hideFrameY == null || this.showFrameY == null || this.fadeOutFrame == null)
            //{
            //    return;
            //}

            base.OnApplyTemplate(e);
            UpdateOpacityChange();
            UpdateFlyoutTheme();
            this.ApplyAnimation(this.Position, this.AnimateOpacity);
        }

        private void WindowTitleThumbOnDragCompleted(object sender, VectorEventArgs e)
        {
            this.dragStartedMousePos = null;
        }

        private void WindowTitleThumbOnDragStarted(object sender, VectorEventArgs e)
        {
            var window = this.ParentWindow;
            if (window != null && this.Position != Position.Bottom)
            {
                //this.dragStartedMousePos = Mouse.GetPosition((IInputElement)sender);
            }
            else
            {
                this.dragStartedMousePos = null;
            }
        }

        /// <summary>
        /// cleanups the events
        /// </summary>
        /// <param name="flyoutsControl"></param>
        protected internal void CleanUp(FlyoutsControl flyoutsControl)
        {
            var thumbContentControl = this.flyoutHeader as IMetroThumb;
            if (thumbContentControl != null)
            {
                thumbContentControl.DragStarted -= this.WindowTitleThumbOnDragStarted;
                thumbContentControl.DragCompleted -= this.WindowTitleThumbOnDragCompleted;
                //thumbContentControl.PreviewMouseLeftButtonUp -= this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                thumbContentControl.DragDelta -= this.WindowTitleThumbMoveOnDragDelta;
                thumbContentControl.DoubleTapped -= this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                thumbContentControl.PointerReleased -= this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            this.parentWindow = null;
        }

        private void WindowTitleThumbMoveOnDragDelta(object sender, VectorEventArgs dragDeltaEventArgs)
        {
            var window = this.ParentWindow;
            //if (window != null && this.Position != Position.Bottom && (this.Position == Position.Top || (this.dragStartedMousePos.GetValueOrDefault().Y <= window.TitleBarHeight && window.TitleBarHeight > 0)))
            //if (window != null && this.Position != Position.Bottom && this.dragStartedMousePos.GetValueOrDefault().Y <= window.TitleBarHeight && window.TitleBarHeight > 0)
            if (window != null && this.Position != Position.Bottom)
            {
                MetroWindow.DoWindowTitleThumbMoveOnDragDelta(sender as IMetroThumb, window, dragDeltaEventArgs);
            }
        }

        private void WindowTitleThumbChangeWindowStateOnMouseDoubleClick(object sender, RoutedEventArgs mouseButtonEventArgs)
        {
            var window = this.ParentWindow;
            if (window != null && this.Position != Position.Bottom /*&& Mouse.GetPosition((IInputElement)sender).Y <= window.TitleBarHeight && window.TitleBarHeight > 0*/)
            {
                MetroWindow.DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(window, mouseButtonEventArgs);
            }
        }

        private void WindowTitleThumbSystemMenuOnMouseRightButtonUp(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton != MouseButton.Right)
                return;

            var window = this.ParentWindow;

            if (window != null && this.Position != Position.Bottom /*&& Mouse.GetPosition((IInputElement)sender).Y <= window.TitleBarHeight && window.TitleBarHeight > 0*/)
            {
                //MetroWindow.DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(window, e);
            }
        }

        internal void ApplyAnimation(Position position, bool animateOpacity, bool resetShowFrame = true)
        {
            if (this.flyoutRoot == null /*|| this.hideFrame == null || this.showFrame == null || this.hideFrameY == null || this.showFrameY == null || this.fadeOutFrame == null*/)
                return;

            if (this.Position == Position.Left || this.Position == Position.Right)
            {
                ShowFrameTranslateTransformX = 0;
            }
            if (this.Position == Position.Top || this.Position == Position.Bottom)
            {
                ShowFrameTranslateTransformY = 0;
            }

            // I mean, we don't need this anymore, because we use Width and Height of the flyoutRoot
            this.flyoutRoot.Measure(new Size(_currentSize.Width, _currentSize.Height));
            if (double.IsNaN(flyoutRoot.Height))
            {
                flyoutRoot.Height = _currentSize.Height;
            }

            if (double.IsNaN(flyoutRoot.Width))
            {
                flyoutRoot.Width = _currentSize.Width;
            }

            if (!animateOpacity)
            {
                FadeOutFrameOpacity = 1;
                this.flyoutRoot.Opacity = 1;
            }
            else
            {
                FadeOutFrameOpacity = 0;
                if (!this.IsOpen) this.flyoutRoot.Opacity = 0;
            }

            switch (position)
            {
                default:
                    this.HorizontalAlignment = this.Margin.Right <= 0 ?
                        (this.HorizontalContentAlignment != Avalonia.Layout.HorizontalAlignment.Stretch ?
                        Avalonia.Layout.HorizontalAlignment.Left : this.HorizontalContentAlignment) :
                        Avalonia.Layout.HorizontalAlignment.Stretch;//HorizontalAlignment.Left;
                    this.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
                    this.HideFrameTranslateTransformX = -this.flyoutRoot.Width - this.Margin.Left;
                    if (resetShowFrame)
                        this.flyoutRoot.RenderTransform = new TranslateTransform(-this.flyoutRoot.Width, 0);
                    break;

                case Position.Right:
                    this.HorizontalAlignment = this.Margin.Left <= 0 ?
                        (this.HorizontalContentAlignment != Avalonia.Layout.HorizontalAlignment.Stretch ?
                        Avalonia.Layout.HorizontalAlignment.Right : this.HorizontalContentAlignment) :
                        Avalonia.Layout.HorizontalAlignment.Stretch;//HorizontalAlignment.Right;
                    this.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
                    HideFrameTranslateTransformX = this.flyoutRoot.Width + this.Margin.Right;
                    if (resetShowFrame)
                        this.flyoutRoot.RenderTransform = new TranslateTransform(this.flyoutRoot.Width, 0);
                    break;

                case Position.Top:
                    this.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
                    this.VerticalAlignment = this.Margin.Bottom <= 0 ? (this.VerticalContentAlignment !=
                        Avalonia.Layout.VerticalAlignment.Stretch ? Avalonia.Layout.VerticalAlignment.Top :
                        this.VerticalContentAlignment) : Avalonia.Layout.VerticalAlignment.Stretch;//VerticalAlignment.Top;
                    HideFrameTranslateTransformY = -this.flyoutRoot.Height - 1 - this.Margin.Top;
                    if (resetShowFrame)
                        this.flyoutRoot.RenderTransform = new TranslateTransform(0, -this.flyoutRoot.Height - 1);
                    break;

                case Position.Bottom:
                    this.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
                    this.VerticalAlignment = this.Margin.Top <= 0 ? (this.VerticalContentAlignment !=
                        Avalonia.Layout.VerticalAlignment.Stretch ?
                        Avalonia.Layout.VerticalAlignment.Bottom : this.VerticalContentAlignment) :
                        Avalonia.Layout.VerticalAlignment.Stretch;//VerticalAlignment.Bottom;
                    HideFrameTranslateTransformY = this.flyoutRoot.Height + this.Margin.Bottom;
                    if (resetShowFrame)
                        this.flyoutRoot.RenderTransform = new TranslateTransform(0, this.flyoutRoot.Height);
                    break;
            }
        }

        /// <summary>
        /// arrange the control
        /// </summary>
        /// <param name="finalRect"></param>
        protected override void ArrangeCore(Rect finalRect)
        {
            base.ArrangeCore(finalRect);
            if (!this.IsOpen) return; // no changes for invisible flyouts, ApplyAnimation is called now in visible changed event
            //if (!sizeInfo.WidthChanged && !sizeInfo.HeightChanged) return;
            if (this.flyoutRoot == null /*|| this.hideFrame == null || this.showFrame == null || this.hideFrameY == null || this.showFrameY == null*/)
                return; // don't bother checking IsOpen and calling ApplyAnimation

            if (this.Position == Position.Left || this.Position == Position.Right)
                ShowFrameTranslateTransformX = 0;
            if (this.Position == Position.Top || this.Position == Position.Bottom)
                ShowFrameTranslateTransformY = 0;

            switch (this.Position)
            {
                default:
                    HideFrameTranslateTransformX = -this.flyoutRoot.Width - this.Margin.Left;
                    break;

                case Position.Right:
                    HideFrameTranslateTransformX = this.flyoutRoot.Width + this.Margin.Right;
                    break;

                case Position.Top:
                    HideFrameTranslateTransformY = -this.flyoutRoot.Height - 1 - this.Margin.Top;
                    break;

                case Position.Bottom:
                    HideFrameTranslateTransformY = this.flyoutRoot.Height + this.Margin.Bottom;
                    break;
            }
        }

        private void AutoCloseTimerCallback(object sender, EventArgs e)
        {
            this.StopAutoCloseTimer();

            //if the flyout is open and autoclose is still enabled then close the flyout
            if ((this.IsOpen) && (this.IsAutoCloseEnabled))
            {
                this.IsOpen = false;
            }
        }

        private void StopAutoCloseTimer()
        {
            if ((this.autoCloseTimer != null) && (this.autoCloseTimer.IsEnabled))
            {
                this.autoCloseTimer.Stop();
            }
        }

        private void AutoCloseIntervalChanged(Flyout flyout, AvaloniaPropertyChangedEventArgs e)
        {
            Action autoCloseIntervalChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    flyout.InitializeAutoCloseTimer();
                    if (flyout.IsAutoCloseEnabled && flyout.IsOpen)
                    {
                        flyout.StartAutoCloseTimer();
                    }
                }
            };

            Dispatcher.UIThread.InvokeAsync(autoCloseIntervalChangedAction, DispatcherPriority.Background);

            //flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, autoCloseIntervalChangedAction);
        }

        private void StartAutoCloseTimer()
        {
            //in case it is already running
            this.StopAutoCloseTimer();
            if (!Design.IsDesignMode)
            {
                this.autoCloseTimer.Start();
            }
        }

        private void IsAutoCloseEnabledChanged(Flyout flyout, AvaloniaPropertyChangedEventArgs e)
        {
            Action autoCloseEnabledChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    if ((bool)e.NewValue)
                    {
                        if (flyout.IsOpen)
                        {
                            flyout.StartAutoCloseTimer();
                        }
                    }
                    else
                    {
                        flyout.StopAutoCloseTimer();
                    }
                }
            };

            Dispatcher.UIThread.InvokeAsync(autoCloseEnabledChangedAction, DispatcherPriority.Background);
            //flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, autoCloseEnabledChangedAction);
        }

        private void ThemeChanged(Flyout flyout, AvaloniaPropertyChangedEventArgs e)
        {
            flyout.UpdateFlyoutTheme();
            flyout.RaiseEvent(new RoutedEventArgs(FlyoutThemeChangedEvent));
        }

        private void AnimateOpacityChanged(Flyout flyout, AvaloniaPropertyChangedEventArgs e)
        {
            flyout.UpdateOpacityChange();
        }

        private void IsOpenedChanged(Flyout flyout, AvaloniaPropertyChangedEventArgs e)
        {
            Action openedChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    if (flyout.AreAnimationsEnabled)
                    {
                        if ((bool)e.NewValue)
                        {
                            //if (flyout.hideStoryboard != null)
                            //{
                            //    // don't let the storyboard end it's completed event
                            //    // otherwise it could be hidden on start
                            //    flyout.hideStoryboard.Completed -= flyout.HideStoryboardCompleted;
                            //}
                            flyout.IsVisible = true;
                            flyout.ApplyAnimation(flyout.Position, flyout.AnimateOpacity);
                            flyout.TryFocusElement();
                            if (flyout.IsAutoCloseEnabled)
                            {
                                flyout.StartAutoCloseTimer();
                            }
                        }
                        else
                        {
                            flyout.StopAutoCloseTimer();
                            //if (flyout.hideStoryboard != null)
                            //{
                            //    flyout.hideStoryboard.Completed += flyout.HideStoryboardCompleted;
                            //}
                            //else
                            //{
                            //    flyout.Hide();
                            //}
                        }

                        FlyoutVisualStates = (bool)e.NewValue == false ? FlyoutVisualState.Hide : FlyoutVisualState.Show;

                        //VisualStateManager.GoToState(flyout, (bool)e.NewValue == false ? "Hide" : "Show", true);
                    }
                    else
                    {
                        if ((bool)e.NewValue)
                        {
                            flyout.IsVisible = true;
                            flyout.TryFocusElement();
                            if (flyout.IsAutoCloseEnabled)
                            {
                                flyout.StartAutoCloseTimer();
                            }
                        }
                        else
                        {
                            flyout.StopAutoCloseTimer();
                            flyout.Hide();
                        }

                        FlyoutVisualStates = (bool)e.NewValue == false ? FlyoutVisualState.HideDirect : FlyoutVisualState.ShowDirect;
                        //VisualStateManager.GoToState(flyout, (bool)e.NewValue == false ? "HideDirect" : "ShowDirect", true);
                    }
                }

                flyout.RaiseEvent(new RoutedEventArgs(IsOpenChangedEvent));
            };

            ////flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, openedChangedAction);
            Dispatcher.UIThread.InvokeAsync(openedChangedAction, DispatcherPriority.Render);
        }

        private void Hide()
        {
            // hide the flyout, we should get better performance and prevent showing the flyout on any resizing events
            this.IsVisible = false;
            this.RaiseEvent(new RoutedEventArgs(ClosingFinishedEvent));
        }

        private void TryFocusElement()
        {
            if (this.AllowFocusElement)
            {
                // first focus itself
                this.Focus();

                if (this.FocusedElement != null)
                {
                    this.FocusedElement.Focus();
                }
                else if (this.flyoutContent == null || !this.flyoutContent.IsFocused)//this.flyoutContent.MoveFocus(new TraversalRequest(FocusNavigationDirection.First)))
                {
                    //this.flyoutHeader?.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    this.flyoutHeader?.Focus();
                }
            }
        }

        private void PositionChanged(Flyout flyout, AvaloniaPropertyChangedEventArgs e)
        {
            var wasOpen = flyout.IsOpen;
            if (wasOpen && flyout.AnimateOnPositionChange)
            {
                flyout.ApplyAnimation((Position)e.NewValue, flyout.AnimateOpacity);
                //VisualStateManager.GoToState(flyout, "Hide", true);
                FlyoutVisualStates = FlyoutVisualState.Hide;
            }
            else
            {
                flyout.ApplyAnimation((Position)e.NewValue, flyout.AnimateOpacity, false);
            }

            if (wasOpen && flyout.AnimateOnPositionChange)
            {
                flyout.ApplyAnimation((Position)e.NewValue, flyout.AnimateOpacity);
                //VisualStateManager.GoToState(flyout, "Show", true);
                FlyoutVisualStates = FlyoutVisualState.Show;
            }
        }
    }
}
