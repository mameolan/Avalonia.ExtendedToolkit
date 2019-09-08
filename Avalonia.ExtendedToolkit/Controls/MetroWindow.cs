using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Interaction logic for <see cref="MetroWindow"/> xaml.
    /// </summary>
    public class MetroWindow : Window, IStyleable
    {
        private Grid _titleBar;

        private Grid _bottomHorizontalGrip;
        private Grid _bottomLeftGrip;
        private Grid _bottomRightGrip;
        private Grid _leftVerticalGrip;
        private Grid _rightVerticalGrip;
        private Grid _topHorizontalGrip;
        private Grid _topLeftGrip;
        private Grid _topRightGrip;

        private Button _closeButton;
        private Button _minimiseButton;
        private Button _restoreButton;

        Rectangle flyoutModal;
        Thumb flyoutModalDragMoveThumb;

        private Image _icon;

        private bool _mouseDown;
        private Point _mouseDownPosition;



        public Brush FlyoutOverlayBrush
        {
            get { return (Brush)GetValue(FlyoutOverlayBrushProperty); }
            set { SetValue(FlyoutOverlayBrushProperty, value); }
        }


        public static readonly AvaloniaProperty FlyoutOverlayBrushProperty =
            AvaloniaProperty.Register<MetroWindow, Brush>(nameof(FlyoutOverlayBrush));



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
            //RaisePropertyChanged(WindowStateProperty, oldValue, WindowState, Data.BindingPriority.TemplatedParent);
        }

        internal void HandleFlyoutStatusChange(Flyout flyout, List<Flyout> visibleFlyouts)
        {
            //checks a recently opened flyout's position.
            //if (flyout.Position == Position.Left || flyout.Position == Position.Right || flyout.Position == Position.Top)
            {
                //get it's zindex
                var zIndex = flyout.IsOpen ? flyout.ZIndex + 3 : visibleFlyouts.Count() + 2;

                //if the the corresponding behavior has the right flag, set the window commands' and icon zIndex to a number that is higher than the flyout's.
                //this.icon?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : (this.IconOverlayBehavior.HasFlag(OverlayBehavior.Flyouts) ? zIndex : 1));
                //this.LeftWindowCommandsPresenter?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : 1);
                //this.RightWindowCommandsPresenter?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : 1);
                //this.WindowButtonCommandsPresenter?.SetValue(Panel.ZIndexProperty, flyout.IsModal && flyout.IsOpen ? 0 : (this.WindowButtonCommandsOverlayBehavior.HasFlag(OverlayBehavior.Flyouts) ? zIndex : 1));
                this.HandleWindowCommandsForFlyouts(visibleFlyouts);
            }

            if (this.flyoutModal != null)
            {
                this.flyoutModal.IsVisible = visibleFlyouts.Any(x => x.IsModal) ? true : false;
            }

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
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (_titleBar != null && _titleBar.IsPointerOver && _mouseDown)
            {
                WindowState = WindowState.Normal;
                BeginMoveDrag();
                _mouseDown = false;
            }
            base.OnPointerMoved(e);
        }

        public MetroWindow()
        {
            FlyoutsProperty.Changed.AddClassHandler<MetroWindow>((o, e) => UpdateLogicalChilds(o, e));

            ThemeManager.Instance.IsThemeChanged += ThemeManagerOnIsThemeChanged;
            
            //if(Flyouts==null)
            //{
            //    Flyouts = new FlyoutsControl();
            //}

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
               //     this.ResetAllWindowCommandsBrush();
                    return;
                }

                foreach (var flyout in flyouts)
                {
                    flyout.ChangeFlyoutTheme(e.Theme);
                }
                this.HandleWindowCommandsForFlyouts(flyouts);
            }
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


        /// <inheritdoc/>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            _titleBar = e.NameScope.Find<Grid>("PART_TitleBar");
            _minimiseButton = e.NameScope.Find<Button>("PART_MinimiseButton");
            _restoreButton = e.NameScope.Find<Button>("PART_RestoreButton");
            _closeButton = e.NameScope.Find<Button>("PART_CloseButton");
            _icon = e.NameScope.Find<Image>("PART_Icon");

            _topHorizontalGrip = e.NameScope.Find<Grid>("PART_TopHorizontalGrip");
            _bottomHorizontalGrip = e.NameScope.Find<Grid>("PART_BottomHorizontalGrip");
            _leftVerticalGrip = e.NameScope.Find<Grid>("PART_LeftVerticalGrip");
            _rightVerticalGrip = e.NameScope.Find<Grid>("PART_RightVerticalGrip");

            _topLeftGrip = e.NameScope.Find<Grid>("PART_TopLeftGrip");
            _bottomLeftGrip = e.NameScope.Find<Grid>("PART_BottomLeftGrip");
            _topRightGrip = e.NameScope.Find<Grid>("PART_TopRightGrip");
            _bottomRightGrip = e.NameScope.Find<Grid>("PART_BottomRightGrip");

            flyoutModal = e.NameScope.Find<Rectangle>("PART_FlyoutModal");
            flyoutModalDragMoveThumb= e.NameScope.Find<Thumb>("PART_FlyoutModalDragMoveThumb");

            if (_minimiseButton != null)
            {
                _minimiseButton.Click += (sender, ee) => { WindowState = WindowState.Minimized; };

            }

            if (_restoreButton != null)
            {
                _restoreButton.Click += (sender, ee) => { ToggleWindowState(); };
            }

            if (_titleBar != null)
            {
                _titleBar.DoubleTapped += (sender, ee) =>
                {
                    ToggleWindowState();
                };
            }

            if (_closeButton != null)
            {
                _closeButton.Click += (sender, ee) => { Close(); };
            }

            //if (_icon != null)
            //{
            //    _icon.DoubleTapped += (sender, ee) => { Close(); };
            //}
        }
    }
}