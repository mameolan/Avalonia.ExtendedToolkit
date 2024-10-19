using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from: https://github.com/punker76/MahApps.Metro.SimpleChildWindow/ 

    /// <summary>
    /// simple child window
    /// </summary>
    public partial class ChildWindow : ContentControl
    {
        /// <summary>
        /// initilaize autotimer and add some classhandlers
        /// </summary>
        public ChildWindow()
        {
            this.InitializeAutoCloseTimer();
            OffsetXProperty.Changed.AddClassHandler<ChildWindow>((x, y) => OnOffsetXChanged(x, y));
            OffsetYProperty.Changed.AddClassHandler<ChildWindow>((x, y) => OnOffsetYChanged(x, y));
            IsOpenProperty.Changed.AddClassHandler<ChildWindow>((x, y) => OnIsOpenChanged(x, y));
            IsAutoCloseEnabledProperty.Changed.AddClassHandler<ChildWindow>((o, e) => OnIsAutoCloseEnabledChanged(o, e));
            AutoCloseIntervalProperty.Changed.AddClassHandler<ChildWindow>((o, e) => OnAutoCloseIntervalChanged(o, e));

        }

        /// <summary>
        /// initialize the autoclose timer and
        /// starts the autoclose timeer if is open and if is autoclosetime is enabled
        /// </summary>
        /// <param name="childWindow"></param>
        /// <param name="e"></param>
        private void OnAutoCloseIntervalChanged(ChildWindow childWindow, AvaloniaPropertyChangedEventArgs e)
        {
            void AutoCloseIntervalChangedAction()
            {
                if (e.NewValue != e.OldValue)
                {
                    childWindow.InitializeAutoCloseTimer();
                    if (childWindow.IsAutoCloseEnabled && childWindow.IsOpen)
                    {
                        childWindow.StartAutoCloseTimer();
                    }
                }
            }

            Dispatcher.UIThread.InvokeAsync((Action)AutoCloseIntervalChangedAction, DispatcherPriority.Background);
        }

        /// <summary>
        /// starts or stopps the autoclose timer
        /// </summary>
        /// <param name="childWindow"></param>
        /// <param name="e"></param>
        private void OnIsAutoCloseEnabledChanged(ChildWindow childWindow, AvaloniaPropertyChangedEventArgs e)
        {
            void AutoCloseEnabledChangedAction()
            {
                if (e.NewValue != e.OldValue)
                {
                    if ((bool)e.NewValue)
                    {
                        if (childWindow.IsOpen)
                        {
                            childWindow.StartAutoCloseTimer();
                        }
                    }
                    else
                    {
                        childWindow.StopAutoCloseTimer();
                    }
                }
            }

            Dispatcher.UIThread.InvokeAsync((Action)AutoCloseEnabledChangedAction, DispatcherPriority.Background);
        }

        /// <summary>
        /// starts the open or close animation
        /// </summary>
        /// <param name="childWindow"></param>
        /// <param name="e"></param>
        private void OnIsOpenChanged(ChildWindow childWindow, AvaloniaPropertyChangedEventArgs e)
        {
            //skip other childs to display
            if (childWindow != this)
            {
                return;
            }


            if (Equals(e.OldValue, e.NewValue))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                if (_hideAnimationTask != null)
                {
                    _hideAnimationTokenSource.Cancel(false);
                    _hideAnimationTokenSource = null;
                    _hideAnimationTask = null;
                    // don't let the storyboard end it's completed event
                    // otherwise it could be hidden on start
                }
                //.edited
                _showAnimationTask = _showAnimation.RunAsync(childWindow).ContinueWith(
                 x =>
                 {
#warning little bit hacky should be done in the animation
                     _partOverlay.IsVisible = true;

                     _showAnimationTask = null;

                     var parent = childWindow.Parent as Panel;
                     childWindow.ZIndex = /*parent?.Children.Count + 1 ??*/ 99;

                     childWindow.TryToSetFocusedElement();

                     if (childWindow.IsAutoCloseEnabled)
                     {
                         childWindow.StartAutoCloseTimer();
                     }

                     childWindow.RaiseEvent(new RoutedEventArgs(IsOpenChangedEvent, childWindow));
                 }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                childWindow.StopAutoCloseTimer();

                _hideAnimationTokenSource = new CancellationTokenSource();
                //.edited, removed Clock
                _hideAnimationTask = _hideAnimation.RunAsync(childWindow).ContinueWith(
                     x =>
                     {
                         childWindow.OnClosingFinished();
                         _hideAnimationTask = null;

#warning little bit hacky should be done in the animation
                         _partOverlay.IsVisible = false;
                     }, _hideAnimationTokenSource.Token,
                        TaskContinuationOptions.None,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        
        /// <summary>
        /// tries to focus the element
        /// </summary>
        private void TryToSetFocusedElement()
        {
            if (this.AllowFocusElement && !Design.IsDesignMode/*( !DesignerProperties.GetIsInDesignMode(this)*/)
            {
                // first focus itself
                this.Focus();

                var elementToFocus = this.FocusedElement ?? /*TreeHelper.FindChildren<UIElement>(this)*/VisualTree.VisualExtensions.GetVisualChildren(this).OfType<Control>().FirstOrDefault(c => c.Focusable);
                if (this.ShowCloseButton && this._closeButton != null && elementToFocus == null)
                {
                    this._closeButton.SetValue(FocusableProperty, true);
                    elementToFocus = this._closeButton;
                }

                if (elementToFocus != null)
                {
                    IDisposable disposable = null;

                    disposable = elementToFocus.GetPropertyChangedObservable(IsVisibleProperty).Subscribe((x) =>
                     {
                         OnIsVisibleChanged(elementToFocus, x);
                     });

                    void OnIsVisibleChanged(object sender, AvaloniaPropertyChangedEventArgs args)
                    {
                        disposable?.Dispose();
                        if (elementToFocus.Focusable /*&& elementToFocus is HwndHost == false*/)
                        {
                            elementToFocus.Focus();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// raises the <see cref="ClosingFinishedEvent"/>
        /// </summary>
        private void OnClosingFinished()
        {
            this.RaiseEvent(new RoutedEventArgs(ClosingFinishedEvent, this));
        }

        /// <summary>
        /// moves in y orientation
        /// </summary>
        /// <param name="childWindow"></param>
        /// <param name="args"></param>
        private void OnOffsetYChanged(ChildWindow childWindow, AvaloniaPropertyChangedEventArgs args)
        {
            childWindow._moveTransform.SetValue(TranslateTransform.YProperty, args.NewValue);
        }

        /// <summary>
        /// moves in x orientation
        /// </summary>
        /// <param name="childWindow"></param>
        /// <param name="args"></param>
        private void OnOffsetXChanged(ChildWindow childWindow, AvaloniaPropertyChangedEventArgs args)
        {
            childWindow._moveTransform.SetValue(TranslateTransform.XProperty, args.NewValue);
        }

        /// <summary>
        /// sets the zindex
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.Handled == false)
            {
                var container = this.Parent as Panel;
                var elementOnTop = container?.Children.OfType<Control>().OrderBy(c => c.GetValue(Panel.ZIndexProperty)).LastOrDefault();

                if (elementOnTop != null && !Equals(elementOnTop, this))
                {
                    var zIndexOnTop = (int)elementOnTop.GetValue(Panel.ZIndexProperty);
                    elementOnTop.SetValue(Panel.ZIndexProperty, zIndexOnTop - 1);
                    this.SetValue(Panel.ZIndexProperty, zIndexOnTop);
                }
            }
        }

        /// <summary>
        /// initializes the autostop timer
        /// </summary>
        private void InitializeAutoCloseTimer()
        {
            this.StopAutoCloseTimer();

            this._autoCloseTimer = new DispatcherTimer();
            this._autoCloseTimer.Tick += this.AutoCloseTimerCallback;
            this._autoCloseTimer.Interval = TimeSpan.FromMilliseconds(this.AutoCloseInterval);
        }

        /// <summary>
        /// StopAutoCloseTimer and fires the close method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCloseTimerCallback(object sender, EventArgs e)
        {
            this.StopAutoCloseTimer();

            // if the ChildWindow is open and autoclose is still enabled then close the ChildWindow
            if (this.IsOpen && this.IsAutoCloseEnabled)
            {
                this.Close(CloseReason.AutoClose);
            }
        }

        /// <summary>
        /// starts the autoclose timer
        /// </summary>
        private void StartAutoCloseTimer()
        {
            // in case it is already running
            this.StopAutoCloseTimer();
            if (!Design.IsDesignMode)
            {
                _autoCloseTimer.Start();
            }
        }

        /// <summary>
        /// stops the autoclose timer
        /// </summary>
        private void StopAutoCloseTimer()
        {
            if ((_autoCloseTimer != null) && (_autoCloseTimer.IsEnabled))
            {
                _autoCloseTimer.Stop();
            }
        }

        /// <summary>
        /// resolves the controls items
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            // really necessary?
            //if (this.Template == null)
            //{
            //    return;
            //}

            var isActiveBindingAction = new Action(() =>
            {
                var window = this.TryFindParent<Window>();
                //var window = Window.GetWindow(this);
                if (window != null)
                {
                    var binding = new Binding(nameof(Window.IsActive))
                    {
                        Source = window,
                        Mode = BindingMode.OneWay,
                    };

                    this.Bind(IsWindowHostActiveProperty, binding);
                    //this.SetBinding(IsWindowHostActiveProperty, new Binding(nameof(Window.IsActive)) { Source = window, Mode = BindingMode.OneWay });
                }
            });

            // if (!this.IsInitialized)
            // {
            Dispatcher.UIThread.InvokeAsync(isActiveBindingAction, DispatcherPriority.Loaded);
            //this.BeginInvoke(isActiveBindingAction, DispatcherPriority.Loaded);
            // }
            // else
            // {
            //     isActiveBindingAction();
            // }

            //this.hideStoryboard = this.Template.FindName(HideStoryboard, this) as Storyboard;

            if (_partOverlay != null)
            {
                _partOverlay.PointerPressed -= this.PartOverlayOnClose;
                _partOverlay.PropertyChanged -= partOverlayPropertyChanged;
                //this.partOverlay.SizeChanged -= this.PartOverlay_SizeChanged;
            }

            _partOverlay = e.NameScope.Find<Grid>(PART_Overlay);
            if (_partOverlay != null)
            {
                _partOverlay.PointerPressed += this.PartOverlayOnClose;
                _partOverlay.PropertyChanged += partOverlayPropertyChanged;

                //this.partOverlay.SizeChanged += this.PartOverlay_SizeChanged;
            }
            _partWindow = e.NameScope.Find<Grid>(PART_Window);
            _partWindow?.SetValue(RenderTransformProperty, _moveTransform);

            _icon = e.NameScope.Find<ContentControl>(PART_Icon);

            if (_headerThumb != null)
            {
                _headerThumb.DragDelta -= this.HeaderThumbDragDelta;
            }

            _headerThumb = e.NameScope.Find<IMetroThumb>(PART_HeaderThumb);

            if (_headerThumb != null && _partWindow != null)
            {
                _headerThumb.DragDelta += this.HeaderThumbDragDelta;
            }

            if (_closeButton != null)
            {
                _closeButton.Click -= this.OnCloseButtonClick;
            }

            _closeButton = e.NameScope.Find<Button>(PART_CloseButton);
            if (_closeButton != null)
            {
                _closeButton.Click += this.OnCloseButtonClick;
            }

            _hideAnimation = CreateHideAnimation();
            _showAnimation = CreateShowAnimation();

            //UpdatePseudeoClasses();

        }

        /// <summary>
        /// creates the hide animation
        /// </summary>
        /// <returns></returns>
        private Animation.Animation CreateHideAnimation()
        {
            Animation.Animation animation = new Animation.Animation();
            animation.Duration = TimeSpan.FromMilliseconds(100);
            animation.FillMode = Animation.FillMode.None;
            KeyFrame keyFrame = new KeyFrame();
            keyFrame.KeyTime = TimeSpan.FromMilliseconds(0);
            keyFrame.Setters.Add(new Setter
            {
                Property = OpacityProperty,
                Value = 1d
            });
            animation.Children.Add(keyFrame);
            keyFrame = new KeyFrame();
            keyFrame.KeyTime = TimeSpan.FromMilliseconds(100);
            keyFrame.Setters.Add(new Setter
            {
                Property = OpacityProperty,
                Value = 0d
            });
            animation.Children.Add(keyFrame);
            keyFrame = new KeyFrame();
            keyFrame.KeyTime = TimeSpan.FromMilliseconds(100);
            keyFrame.Setters.Add(new Setter
            {
                Property = IsVisibleProperty,
                Value = false
            });
            animation.Children.Add(keyFrame);
            return animation;
        }

        /// <summary>
        /// creates the show animation
        /// </summary>
        /// <returns></returns>
        private Animation.Animation CreateShowAnimation()
        {
            Animation.Animation animation = new Animation.Animation();
            animation.Duration = TimeSpan.FromMilliseconds(200);
            animation.FillMode = Animation.FillMode.Forward;
            KeyFrame keyFrame = new KeyFrame();
            //keyFrame.KeyTime = TimeSpan.FromMilliseconds(0);
            keyFrame.Cue = new Cue(0d);
            keyFrame.Setters.Add(new Setter
            {
                Property = IsVisibleProperty,
                Value = true
            });
            animation.Children.Add(keyFrame);

            keyFrame = new KeyFrame();
            //keyFrame.KeyTime = TimeSpan.FromMilliseconds(0);
            keyFrame.Cue = new Cue(0d);
            keyFrame.Setters.Add(new Setter
            {
                Property = OpacityProperty,
                Value = 0d
            });
            animation.Children.Add(keyFrame);

            keyFrame = new KeyFrame();
            //keyFrame.KeyTime = TimeSpan.FromMilliseconds(200);
            keyFrame.Cue = new Cue(1d);
            keyFrame.Setters.Add(new Setter
            {
                Property = OpacityProperty,
                Value = 1d
            });

            animation.Children.Add(keyFrame);
            return animation;
        }

        /// <summary>
        /// executes the <see cref="PartOverlay_SizeChanged"/>
        /// if width or hight is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void partOverlayPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(_partOverlay.Width)
            || e.Property.Name == nameof(_partOverlay.Height))
            {
                PartOverlay_SizeChanged();
            }
        }

        /// <summary>
        /// execeutes the close method if the pointer left button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartOverlayOnClose(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(_partOverlay).Properties.IsLeftButtonPressed == false)
                return;

            if (Equals(e.Source, _partOverlay) && this.CloseOnOverlay)
            {
                this.Close(CloseReason.Overlay);
            }
        }

        /// <summary>
        /// executes the <see cref="ProcessMove(double, double)"/>
        /// </summary>
        private void PartOverlay_SizeChanged()
        {
            this.ProcessMove(0, 0);
        }

        /// <summary>
        /// moves this control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderThumbDragDelta(object sender, VectorEventArgs e)
        {
            var allowDragging = this.AllowMove && _partWindow.HorizontalAlignment != Layout.HorizontalAlignment.Stretch
                && _partWindow.VerticalAlignment != Layout.VerticalAlignment.Stretch;

            // drag only if IsWindowDraggable is set to true
            if (allowDragging && (Math.Abs(e.Vector.X) > 2 || Math.Abs(e.Vector.Y) > 2))
            {
                this.ProcessMove(e.Vector.X, e.Vector.Y);
            }
        }
        
        /// <summary>
        /// function for moving the control
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ProcessMove(double x, double y)
        {
            var width = _partOverlay./*RenderSize.*/Width;
            var height = _partOverlay./*RenderSize.*/Height;
            var offset = TreeExtensions.GetOffsetFrom(this, _partWindow);
            //var offset = VisualTreeHelper.GetOffset(this.partWindow);
            //todo correct me
            var widthOffset = offset.M11; // offset.X;
            var heightOffset = offset.M12; //offset.Y;

            var realX = _moveTransform.X + x + widthOffset;
            var realY = _moveTransform.Y + y + heightOffset;

            const int extraGap = 5;
            var widthGap = Math.Max(_icon?./*Actual*/Width + 5 ?? 30, 30);
            var heightGap = Math.Max(this.TitleBarHeight, 30);
            var changeX = _moveTransform.X;
            var changeY = _moveTransform.Y;

            if (realX < (0 + extraGap))
            {
                changeX = -widthOffset + extraGap;
            }
            else if (realX > (width - widthGap - extraGap))
            {
                changeX = width - widthOffset - widthGap - extraGap;
            }
            else
            {
                changeX += x;
            }

            if (realY < (0 + extraGap))
            {
                changeY = -heightOffset + extraGap;
            }
            else if (realY > (height - heightGap - extraGap))
            {
                changeY = height - heightOffset - heightGap - extraGap;
            }
            else
            {
                changeY += y;
            }

            if (!Equals(changeX, _moveTransform.X) || !Equals(changeY, _moveTransform.Y))
            {
                this.SetValue(OffsetXProperty, changeX);
                this.SetValue(OffsetYProperty, changeY);

                this.InvalidateArrange();
            }
        }

        /// <summary>
        /// executes the close method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close(CloseReason.Close);
        }

        /// <summary>
        /// This method fires the <see cref="Closing"/> event.
        /// </summary>
        /// <param name="e">The EventArgs for the closing step.</param>
        protected virtual void OnClosing(CancelEventArgs e)
        {
            this.Closing?.Invoke(this, e);
        }

        /// <summary>
        /// Closes this dialog.
        /// </summary>
        /// <param name="childWindowResult">A dialog result (optional).</param>
        public bool Close(object childWindowResult = null)
        {
            return this.Close(CloseReason.Close, childWindowResult);
        }

        /// <summary>
        /// Closes this dialog.
        /// </summary>
        /// <param name="closedBy">The dialog close reason.</param>
        /// <param name="childWindowResult">A dialog result (optional).</param>
        public bool Close(CloseReason closedBy, object childWindowResult = null)
        {
            this.ClosedBy = closedBy;

            // check if we really want close the dialog
            var e = new CancelEventArgs();
            this.OnClosing(e);

            if (!e.Cancel)
            {
                // now handle the command
                if (this.CloseButtonCommand != null)
                {
                    var parameter = this.CloseButtonCommandParameter ?? this;
                    if (!this.CloseButtonCommand.CanExecute(parameter))
                    {
                        return false;
                    }

                    this.CloseButtonCommand.Execute(parameter);
                }

                this.ChildWindowResult = childWindowResult;
                this.SetValue(IsOpenProperty, false);
                return true;
            }
            else
            {
                this.ClosedBy = CloseReason.None;
                return false;
            }
        }

        /// <summary>
        /// executes close if <see cref="CloseByEscape"/> is true and
        /// <see cref="Key.Escape"/> is pressed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.CloseByEscape && e.Key == Key.Escape)
            {
                e.Handled = this.Close(CloseReason.Escape);
            }

            //base.OnKeyDown(e);
            this.OnKeyUp(e);
        }
    }
}
