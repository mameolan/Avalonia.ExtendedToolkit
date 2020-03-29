using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class Flyout : HeaderedContentControl
    {
        private Border flyoutRoot;
        private MetroThumbContentControl flyoutHeader;
        private ContentPresenter flyoutContent;
        //KeyFrameExt hideFrame;
        //KeyFrameExt hideFrameY;
        //KeyFrameExt showFrame;
        //KeyFrameExt showFrameY;
        //KeyFrameExt fadeOutFrame;

        private Point? dragStartedMousePos = null;

        private DispatcherTimer autoCloseTimer;

        public Type StyleKey => typeof(Flyout);

        public static readonly RoutedEvent<RoutedEventArgs> IsOpenChangedEvent =
            RoutedEvent.Register<Flyout, RoutedEventArgs>(nameof(IsOpenChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// An event that is raised when IsOpen changes.
        /// </summary>
        public event EventHandler IsOpenChanged
        {
            add
            {
                AddHandler(IsOpenChangedEvent, value);
            }
            remove
            {
                RemoveHandler(IsOpenChangedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> ClosingFinishedEvent =
            RoutedEvent.Register<Flyout, RoutedEventArgs>(nameof(ClosingFinishedEvent), RoutingStrategies.Bubble);

        public static readonly RoutedEvent<RoutedEventArgs> FlyoutThemeChangedEvent =
                    RoutedEvent.Register<Flyout, RoutedEventArgs>(nameof(FlyoutThemeChangedEvent), RoutingStrategies.Bubble);

        public event EventHandler FlyoutThemeChanged
        {
            add
            {
                AddHandler(FlyoutThemeChangedEvent, value);
            }
            remove
            {
                RemoveHandler(FlyoutThemeChangedEvent, value);
            }
        }

        /// <summary>
        /// An event that is raised when the closing animation has finished.
        /// </summary>
        public event EventHandler ClosingFinished
        {
            add
            {
                AddHandler(ClosingFinishedEvent, value);
            }
            remove
            {
                RemoveHandler(ClosingFinishedEvent, value);
            }
        }

        /// <summary>
        /// Gets/sets this flyout's position in the FlyoutsControl/MetroWindow.
        /// </summary>
        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly StyledProperty<Position> PositionProperty =
            AvaloniaProperty.Register<Flyout, Position>(nameof(Position), defaultValue: Position.Left);

        public FlyoutVisualState FlyoutVisualStates
        {
            get { return (FlyoutVisualState)GetValue(FlyoutVisualStatesProperty); }
            set { SetValue(FlyoutVisualStatesProperty, value); }
        }

        public static readonly StyledProperty<FlyoutVisualState> FlyoutVisualStatesProperty =
            AvaloniaProperty.Register<Flyout, FlyoutVisualState>(nameof(FlyoutVisualStates));

        /// <summary>
        /// Gets/sets whether this flyout stays open when the user clicks outside of it.
        /// </summary>
        public bool IsPinned
        {
            get { return (bool)GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsPinnedProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(IsPinned), defaultValue: true);

        /// <summary>
        /// Gets/sets whether this flyout is visible.
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(IsOpen), defaultBindingMode: Data.BindingMode.TwoWay);

        /// <summary>
        /// Gets/sets whether this flyout uses the open/close
        /// animation when changing the <see cref="Position"/> property. (default is true)
        /// </summary>
        public bool AnimateOnPositionChange
        {
            get { return (bool)GetValue(AnimateOnPositionChangeProperty); }
            set { SetValue(AnimateOnPositionChangeProperty, value); }
        }

        public static readonly StyledProperty<bool> AnimateOnPositionChangeProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(AnimateOnPositionChange), defaultValue: true);

        /// <summary>
        /// Gets/sets whether this flyout animates the opacity of the flyout when opening/closing.
        /// </summary>
        public bool AnimateOpacity
        {
            get { return (bool)GetValue(AnimateOpacityProperty); }
            set { SetValue(AnimateOpacityProperty, value); }
        }

        public static readonly StyledProperty<bool> AnimateOpacityProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(AnimateOpacity));

        /// <summary>
        /// Gets/sets whether this flyout is modal.
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        public static readonly StyledProperty<bool> IsModalProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(IsModal));

        /// <summary>
        /// Gets/sets a command which will be executed if the close button was clicked.
        /// Note that this won't execute when <see cref="IsOpen"/> is set to <c>false</c>.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CloseCommandProperty =
            AvaloniaProperty.Register<Flyout, ICommand>(nameof(CloseCommand));

        /// <summary>
        /// Gets/sets the command parameter which will be passed by the CloseCommand.
        /// </summary>
        public object CloseCommandParameter
        {
            get { return (object)GetValue(CloseCommandParameterProperty); }
            set { SetValue(CloseCommandParameterProperty, value); }
        }

        public static readonly StyledProperty<object> CloseCommandParameterProperty =
            AvaloniaProperty.Register<Flyout, object>(nameof(CloseCommandParameter));

        /// <summary>
        /// Gets or sets the theme of this flyout.
        /// </summary>
        public FlyoutTheme FlyoutTheme
        {
            get { return (FlyoutTheme)GetValue(FlyoutThemeProperty); }
            set { SetValue(FlyoutThemeProperty, value); }
        }

        public static readonly StyledProperty<FlyoutTheme> FlyoutThemeProperty =
            AvaloniaProperty.Register<Flyout, FlyoutTheme>(nameof(FlyoutTheme)
                , defaultValue: FlyoutTheme.Dark);

        /// <summary>
        /// Gets/sets the mouse button that closes the flyout on an external mouse click.
        /// </summary>
        public MouseButton ExternalCloseButton
        {
            get { return (MouseButton)GetValue(ExternalCloseButtonProperty); }
            set { SetValue(ExternalCloseButtonProperty, value); }
        }

        public static readonly StyledProperty<MouseButton> ExternalCloseButtonProperty =
            AvaloniaProperty.Register<Flyout, MouseButton>(nameof(ExternalCloseButton)
                , defaultValue: MouseButton.Left);

        /// <summary>
        /// Gets/sets if the close button is visible in this flyout.
        /// </summary>
        public bool CloseButtonIsVisible
        {
            get { return (bool)GetValue(CloseButtonIsVisibleProperty); }
            set { SetValue(CloseButtonIsVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> CloseButtonIsVisibleProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(CloseButtonIsVisible), defaultValue: true);

        /// <summary>
        /// Gets/sets if the close button is a cancel button in this flyout.
        /// </summary>
        public bool CloseButtonIsCancel
        {
            get { return (bool)GetValue(CloseButtonIsCancelProperty); }
            set { SetValue(CloseButtonIsCancelProperty, value); }
        }

        public static readonly StyledProperty<bool> CloseButtonIsCancelProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(CloseButtonIsCancel));

        /// <summary>
        /// Gets/sets if the title is visible in this flyout.
        /// </summary>
        public bool TitleIsVisible
        {
            get { return (bool)GetValue(TitleIsVisibleProperty); }
            set { SetValue(TitleIsVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> TitleIsVisibleProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(TitleIsVisible), defaultValue: true);

        public bool AreAnimationsEnabled
        {
            get { return (bool)GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> AreAnimationsEnabledProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(AreAnimationsEnabled), defaultValue: true);

        /// <summary>
        /// Gets or sets the focused element.
        /// </summary>
        public IControl FocusedElement
        {
            get { return (IControl)GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        public static readonly StyledProperty<IControl> FocusedElementProperty =
            AvaloniaProperty.Register<Flyout, IControl>(nameof(FocusedElement));

        /// <summary>
        /// Gets or sets a value indicating whether the flyout should try focus an element.
        /// </summary>
        public bool AllowFocusElement
        {
            get { return (bool)GetValue(AllowFocusElementProperty); }
            set { SetValue(AllowFocusElementProperty, value); }
        }

        public static readonly StyledProperty<bool> AllowFocusElementProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(AllowFocusElement), defaultValue: true);

        /// <summary>
        /// Gets or sets a value indicating whether the flyout should
        /// auto close after AutoCloseInterval has passed.
        /// </summary>
        public bool IsAutoCloseEnabled
        {
            get { return (bool)GetValue(IsAutoCloseEnabledProperty); }
            set { SetValue(IsAutoCloseEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> IsAutoCloseEnabledProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(IsAutoCloseEnabled));

        /// <summary>
        /// Gets or sets the time in milliseconds when the flyout should auto close.
        /// </summary>
        public long AutoCloseInterval
        {
            get { return (long)GetValue(AutoCloseIntervalProperty); }
            set { SetValue(AutoCloseIntervalProperty, value); }
        }

        public static readonly StyledProperty<long> AutoCloseIntervalProperty =
            AvaloniaProperty.Register<Flyout, long>(nameof(AutoCloseInterval), defaultValue: 5000L);

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly StyledProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(HeaderFontSize));

        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        public static readonly StyledProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.Register<Flyout, Thickness>(nameof(HeaderMargin));

        public double HideFrameTranslateTransformX
        {
            get { return (double)GetValue(HideFrameTranslateTransformXProperty); }
            set { SetValue(HideFrameTranslateTransformXProperty, value); }
        }

        public static readonly StyledProperty<double> HideFrameTranslateTransformXProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(HideFrameTranslateTransformX), defaultValue: 0);

        public double HideFrameTranslateTransformY
        {
            get { return (double)GetValue(HideFrameTranslateTransformYProperty); }
            set { SetValue(HideFrameTranslateTransformYProperty, value); }
        }

        public static readonly StyledProperty<double> HideFrameTranslateTransformYProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(HideFrameTranslateTransformY), defaultValue: 0);

        public double FadeOutFrameOpacity
        {
            get { return (double)GetValue(FadeOutFrameOpacityProperty); }
            set { SetValue(FadeOutFrameOpacityProperty, value); }
        }

        public static readonly StyledProperty<double> FadeOutFrameOpacityProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(FadeOutFrameOpacity), defaultValue: 0);

        public double ShowFrameTranslateTransformX
        {
            get { return (double)GetValue(ShowFrameTranslateTransformXProperty); }
            set { SetValue(ShowFrameTranslateTransformXProperty, value); }
        }

        public static readonly StyledProperty<double> ShowFrameTranslateTransformXProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(ShowFrameTranslateTransformX), defaultValue: 0);

        public double ShowFrameTranslateTransformY
        {
            get { return (double)GetValue(ShowFrameTranslateTransformYProperty); }
            set { SetValue(ShowFrameTranslateTransformYProperty, value); }
        }

        public static readonly StyledProperty<double> ShowFrameTranslateTransformYProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(ShowFrameTranslateTransformY), defaultValue: 0);
    }
}
