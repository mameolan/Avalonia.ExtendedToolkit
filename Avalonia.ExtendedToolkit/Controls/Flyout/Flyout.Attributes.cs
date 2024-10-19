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

        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(Flyout);

        /// <summary>
        /// <see cref="IsOpenChanged"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ClosingFinished"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClosingFinishedEvent =
            RoutedEvent.Register<Flyout, RoutedEventArgs>(nameof(ClosingFinishedEvent), RoutingStrategies.Bubble);

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
        /// <see cref="FlyoutThemeChanged"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> FlyoutThemeChangedEvent =
                    RoutedEvent.Register<Flyout, RoutedEventArgs>(nameof(FlyoutThemeChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// occured when flaout theme changed
        /// </summary>
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
        /// Gets/sets this flyout's position in the FlyoutsControl/MetroWindow.
        /// </summary>
        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// <see cref="Position"/>
        /// </summary>
        public static readonly StyledProperty<Position> PositionProperty =
            AvaloniaProperty.Register<Flyout, Position>(nameof(Position), defaultValue: Position.Left);

        /// <summary>
        /// get/sets FlyoutVisualStates
        /// </summary>
        public FlyoutVisualState FlyoutVisualStates
        {
            get { return (FlyoutVisualState)GetValue(FlyoutVisualStatesProperty); }
            set { SetValue(FlyoutVisualStatesProperty, value); }
        }

        /// <summary>
        /// <see cref="FlyoutVisualStates"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="IsPinned"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="IsOpen"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="AnimateOnPositionChange"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="AnimateOpacity"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="IsModal"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CloseCommand"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CloseCommandParameter"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="FlyoutTheme"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ExternalCloseButton"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CloseButtonIsVisible"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="CloseButtonIsCancel"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="TitleIsVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> TitleIsVisibleProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(TitleIsVisible), defaultValue: true);

        /// <summary>
        /// get/sets AreAnimationsEnabled
        /// </summary>
        public bool AreAnimationsEnabled
        {
            get { return (bool)GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        /// <summary>
        /// <see cref="AreAnimationsEnabled"/>
        /// </summary>
        public static readonly StyledProperty<bool> AreAnimationsEnabledProperty =
            AvaloniaProperty.Register<Flyout, bool>(nameof(AreAnimationsEnabled), defaultValue: true);

        /// <summary>
        /// Gets or sets the focused element.
        /// </summary>
        public Control FocusedElement
        {
            get { return (Control)GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        /// <summary>
        /// <see cref="FocusedElement"/>
        /// </summary>
        public static readonly StyledProperty<Control> FocusedElementProperty =
            AvaloniaProperty.Register<Flyout, Control>(nameof(FocusedElement));

        /// <summary>
        /// Gets or sets a value indicating whether the flyout should try focus an element.
        /// </summary>
        public bool AllowFocusElement
        {
            get { return (bool)GetValue(AllowFocusElementProperty); }
            set { SetValue(AllowFocusElementProperty, value); }
        }

        /// <summary>
        /// <see cref="AllowFocusElement"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="IsAutoCloseEnabled"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="AutoCloseInterval"/>
        /// </summary>
        public static readonly StyledProperty<long> AutoCloseIntervalProperty =
            AvaloniaProperty.Register<Flyout, long>(nameof(AutoCloseInterval), defaultValue: 5000L);

        /// <summary>
        /// get/sets HeaderFontSize
        /// </summary>
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderFontSize"/>
        /// </summary>
        public static readonly StyledProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(HeaderFontSize));

        /// <summary>
        /// get/sets HeaderMargin
        /// </summary>
        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderMargin"/>
        /// </summary>
        public static readonly StyledProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.Register<Flyout, Thickness>(nameof(HeaderMargin));

        /// <summary>
        /// get/sets HideFrameTranslateTransformX
        /// </summary>
        internal double HideFrameTranslateTransformX
        {
            get { return (double)GetValue(HideFrameTranslateTransformXProperty); }
            set { SetValue(HideFrameTranslateTransformXProperty, value); }
        }

        /// <summary>
        /// <see cref="HideFrameTranslateTransformX"/>
        /// </summary>
        internal static readonly StyledProperty<double> HideFrameTranslateTransformXProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(HideFrameTranslateTransformX), defaultValue: 0);

        /// <summary>
        /// get/set HideFrameTranslateTransformY
        /// </summary>
        internal double HideFrameTranslateTransformY
        {
            get { return (double)GetValue(HideFrameTranslateTransformYProperty); }
            set { SetValue(HideFrameTranslateTransformYProperty, value); }
        }

        /// <summary>
        /// <see cref="HideFrameTranslateTransformY"/>
        /// </summary>
        internal static readonly StyledProperty<double> HideFrameTranslateTransformYProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(HideFrameTranslateTransformY), defaultValue: 0);

        /// <summary>
        /// get/sets FadeOutFrameOpacity
        /// </summary>
        public double FadeOutFrameOpacity
        {
            get { return (double)GetValue(FadeOutFrameOpacityProperty); }
            set { SetValue(FadeOutFrameOpacityProperty, value); }
        }

        /// <summary>
        /// <see cref="FadeOutFrameOpacity"/>
        /// </summary>
        public static readonly StyledProperty<double> FadeOutFrameOpacityProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(FadeOutFrameOpacity), defaultValue: 0);

        /// <summary>
        /// get/sets ShowFrameTranslateTransformX
        /// </summary>
        internal double ShowFrameTranslateTransformX
        {
            get { return (double)GetValue(ShowFrameTranslateTransformXProperty); }
            set { SetValue(ShowFrameTranslateTransformXProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowFrameTranslateTransformX"/>
        /// </summary>
        internal static readonly StyledProperty<double> ShowFrameTranslateTransformXProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(ShowFrameTranslateTransformX), defaultValue: 0);

        /// <summary>
        /// get/sets ShowFrameTranslateTransformY
        /// </summary>
        internal double ShowFrameTranslateTransformY
        {
            get { return (double)GetValue(ShowFrameTranslateTransformYProperty); }
            set { SetValue(ShowFrameTranslateTransformYProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowFrameTranslateTransformY"/>
        /// </summary>
        internal static readonly StyledProperty<double> ShowFrameTranslateTransformYProperty =
            AvaloniaProperty.Register<Flyout, double>(nameof(ShowFrameTranslateTransformY), defaultValue: 0);
    }
}
