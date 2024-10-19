using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.ObjectModel;

namespace Avalonia.ExtendedToolkit.Controls
{

    //ported from https://github.com/Enterwell/Wpf.Notifications


    /// <summary>
    /// class which implements <see cref="INotificationMessage"/>
    /// and <see cref="INotificationAnimation"/>
    /// </summary>
    public partial class NotificationMessage : TemplatedControl, INotificationMessage, INotificationAnimation
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(NotificationMessage);

        /// <summary>
        /// Gets or sets OverlayContent.
        /// </summary>
        public object OverlayContent
        {
            get { return (object)GetValue(OverlayContentProperty); }
            set { SetValue(OverlayContentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="OverlayContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> OverlayContentProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(OverlayContent));

        /// <summary>
        /// Gets or sets AdditionalContentTop.
        /// </summary>
        public object AdditionalContentTop
        {
            get { return (object)GetValue(AdditionalContentTopProperty); }
            set { SetValue(AdditionalContentTopProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AdditionalContentTop"/> property.
        /// </summary>
        public static readonly StyledProperty<object> AdditionalContentTopProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(AdditionalContentTop));

        /// <summary>
        /// Gets or sets AdditionalContentBottom.
        /// </summary>
        public object AdditionalContentBottom
        {
            get { return (object)GetValue(AdditionalContentBottomProperty); }
            set { SetValue(AdditionalContentBottomProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AdditionalContentBottom"/> property.
        /// </summary>
        public static readonly StyledProperty<object> AdditionalContentBottomProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(AdditionalContentBottom));

        /// <summary>
        /// Gets or sets AdditionalContentLeft.
        /// </summary>
        public object AdditionalContentLeft
        {
            get { return (object)GetValue(AdditionalContentLeftProperty); }
            set { SetValue(AdditionalContentLeftProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AdditionalContentLeft"/> property.
        /// </summary>
        public static readonly StyledProperty<object> AdditionalContentLeftProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(AdditionalContentLeft));

        /// <summary>
        /// Gets or sets AdditionalContentRight.
        /// </summary>
        public object AdditionalContentRight
        {
            get { return (object)GetValue(AdditionalContentRightProperty); }
            set { SetValue(AdditionalContentRightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AdditionalContentRight"/> property.
        /// </summary>
        public static readonly StyledProperty<object> AdditionalContentRightProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(AdditionalContentRight));

        /// <summary>
        /// Gets or sets AdditionalContentMain.
        /// </summary>
        public object AdditionalContentMain
        {
            get { return (object)GetValue(AdditionalContentMainProperty); }
            set { SetValue(AdditionalContentMainProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AdditionalContentMain"/> property.
        /// </summary>
        public static readonly StyledProperty<object> AdditionalContentMainProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(AdditionalContentMain));

        /// <summary>
        /// Gets or sets AdditionalContentOverBadge.
        /// </summary>
        public object AdditionalContentOverBadge
        {
            get { return (object)GetValue(AdditionalContentOverBadgeProperty); }
            set { SetValue(AdditionalContentOverBadgeProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AdditionalContentOverBadge"/> property.
        /// </summary>
        public static readonly StyledProperty<object> AdditionalContentOverBadgeProperty =
            AvaloniaProperty.Register<NotificationMessage, object>(nameof(AdditionalContentOverBadge));

        /// <summary>
        /// Gets or sets AccentBrush.
        /// </summary>
        public IBrush AccentBrush
        {
            get { return (IBrush)GetValue(AccentBrushProperty); }
            set { SetValue(AccentBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AccentBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> AccentBrushProperty =
            AvaloniaProperty.Register<NotificationMessage, IBrush>(nameof(AccentBrush));

        /// <summary>
        /// Gets or sets ButtonAccentBrush.
        /// </summary>
        public IBrush ButtonAccentBrush
        {
            get { return (IBrush)GetValue(ButtonAccentBrushProperty); }
            set { SetValue(ButtonAccentBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ButtonAccentBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ButtonAccentBrushProperty =
            AvaloniaProperty.Register<NotificationMessage, IBrush>(nameof(ButtonAccentBrush));

        /// <summary>
        /// Gets or sets IsBadgeVisible.
        /// </summary>
        public bool IsBadgeVisible
        {
            get { return (bool)GetValue(IsBadgeVisibleProperty); }
            set { SetValue(IsBadgeVisibleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsBadgeVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsBadgeVisibleProperty =
            AvaloniaProperty.Register<NotificationMessage, bool>(nameof(IsBadgeVisible));

        /// <summary>
        /// Gets or sets BadgeAccentBrush.
        /// </summary>
        public IBrush BadgeAccentBrush
        {
            get { return (IBrush)GetValue(BadgeAccentBrushProperty); }
            set { SetValue(BadgeAccentBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="BadgeAccentBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> BadgeAccentBrushProperty =
            AvaloniaProperty.Register<NotificationMessage, IBrush>(nameof(BadgeAccentBrush));

        /// <summary>
        /// Gets or sets BadgeText.
        /// </summary>
        public string BadgeText
        {
            get { return (string)GetValue(BadgeTextProperty); }
            set { SetValue(BadgeTextProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="BadgeText"/> property.
        /// </summary>
        public static readonly StyledProperty<string> BadgeTextProperty =
            AvaloniaProperty.Register<NotificationMessage, string>(nameof(BadgeText));

        /// <summary>
        /// Gets or sets IsHeaderVisible.
        /// </summary>
        public bool IsHeaderVisible
        {
            get { return (bool)GetValue(IsHeaderVisibleProperty); }
            set { SetValue(IsHeaderVisibleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsHeaderVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsHeaderVisibleProperty =
            AvaloniaProperty.Register<NotificationMessage, bool>(nameof(IsHeaderVisible));

        /// <summary>
        /// Gets or sets Header.
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Header"/> property.
        /// </summary>
        public static readonly StyledProperty<string> HeaderProperty =
            AvaloniaProperty.Register<NotificationMessage, string>(nameof(Header));

        /// <summary>
        /// Gets or sets IsMessageVisible.
        /// </summary>
        public bool IsMessageVisible
        {
            get { return (bool)GetValue(IsMessageVisibleProperty); }
            set { SetValue(IsMessageVisibleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsMessageVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsMessageVisibleProperty =
            AvaloniaProperty.Register<NotificationMessage, bool>(nameof(IsMessageVisible));

        /// <summary>
        /// Gets or sets Message.
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Message"/> property.
        /// </summary>
        public static readonly StyledProperty<string> MessageProperty =
            AvaloniaProperty.Register<NotificationMessage, string>(nameof(Message));

        /// <summary>
        /// Gets or sets Buttons.
        /// </summary>
        public ObservableCollection<object> Buttons
        {
            get { return (ObservableCollection<object>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Buttons"/> property.
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<object>> ButtonsProperty =
            AvaloniaProperty.Register<NotificationMessage, ObservableCollection<object>>(nameof(Buttons));

        /// <summary>
        /// Gets or sets Animates.
        /// </summary>
        public bool Animates
        {
            get { return (bool)GetValue(AnimatesProperty); }
            set { SetValue(AnimatesProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Animates"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> AnimatesProperty =
            AvaloniaProperty.Register<NotificationMessage, bool>(nameof(Animates));

        /// <summary>
        /// Gets or sets AnimationInDuration.
        /// </summary>
        public double AnimationInDuration
        {
            get { return (double)GetValue(AnimationInDurationProperty); }
            set { SetValue(AnimationInDurationProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AnimationInDuration"/> property.
        /// </summary>
        public static readonly StyledProperty<double> AnimationInDurationProperty =
            AvaloniaProperty.Register<NotificationMessage, double>(nameof(AnimationInDuration), defaultValue: 0.25d);

        /// <summary>
        /// Defines the <see cref="AnimationIn"/> direct property.
        /// </summary>
        public static readonly DirectProperty<NotificationMessage, Animation.Animation> AnimationInProperty =
                AvaloniaProperty.RegisterDirect<NotificationMessage, Animation.Animation>(
                    nameof(AnimationIn),
                    o => o.AnimationIn);

        private Animation.Animation _animationIn;

        /// <summary>
        /// Gets or sets AnimationIn.
        /// </summary>
        public Animation.Animation AnimationIn
        {
            get
            {
                if (_animationIn == null)
                {
                    _animationIn = CreateDefaultAnimation(true);
                }
                return _animationIn;
            }
            set
            {
                SetAndRaise(AnimationInProperty, ref _animationIn, value);
            }
        }

        /// <summary>
        /// Gets or sets AnimationOutDuration.
        /// </summary>
        public double AnimationOutDuration
        {
            get { return (double)GetValue(AnimationOutDurationProperty); }
            set { SetValue(AnimationOutDurationProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AnimationOutDuration"/> property.
        /// </summary>
        public static readonly StyledProperty<double> AnimationOutDurationProperty =
            AvaloniaProperty.Register<NotificationMessage, double>(nameof(AnimationOutDuration), defaultValue: 0.25d);

        /// <summary>
        /// Defines the <see cref="AnimationOut"/> direct property.
        /// </summary>
        public static readonly DirectProperty<NotificationMessage, Animation.Animation> AnimationOutProperty =
                AvaloniaProperty.RegisterDirect<NotificationMessage, Animation.Animation>(
                    nameof(AnimationOut),
                    o => o.AnimationOut);

        private Animation.Animation _animationOut;

        /// <summary>
        /// Gets or sets AnimationOut.
        /// </summary>
        public Animation.Animation AnimationOut
        {
            get
            {
                if (_animationOut == null)
                {
                    _animationOut = CreateDefaultAnimation(false);
                }

                return _animationOut;
            }
            set
            {
                SetAndRaise(AnimationOutProperty, ref _animationOut, value);
            }
        }

        /// <summary>
        /// The animatable element used for show/hide animations.
        /// </summary>
        public Control AnimatableElement => this;

        public NotificationMessage()
        {
            Buttons = new ObservableCollection<object>();
            Foreground = new BrushConverter().ConvertFromString("#DDDDDD") as IBrush;

            AccentBrushProperty.Changed.AddClassHandler<NotificationMessage>((o, e)
                => AccentBrushPropertyChangedCallback(o, e));
            BadgeTextProperty.Changed.AddClassHandler<NotificationMessage>((o, e)
                => BadgeTextPropertyChangedCallback(o, e));
            HeaderProperty.Changed.AddClassHandler<NotificationMessage>((o, e)
                => HeaderPropertyChangesCallback(o, e));
            MessageProperty.Changed.AddClassHandler<NotificationMessage>((o, e)
                => MessagePropertyChangesCallback(o, e));
        }

        /// <summary>
        /// if <see cref="Message"/> is not null <see cref="IsMessageVisible"/>
        /// is set to true
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void MessagePropertyChangesCallback(NotificationMessage o, AvaloniaPropertyChangedEventArgs e)
        {
            o.IsMessageVisible = e.NewValue == null
                ? false
                : true;
        }

        /// <summary>
        /// if <see cref="Header"/> is not null <see cref="IsHeaderVisible"/>
        /// is set to true
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void HeaderPropertyChangesCallback(NotificationMessage o, AvaloniaPropertyChangedEventArgs e)
        {
            o.IsHeaderVisible = e.NewValue == null
                ? false
                : true;
        }

        /// <summary>
        /// if <see cref="BadgeText"/> is not null <see cref="IsBadgeVisible"/>
        /// is set to true
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void BadgeTextPropertyChangedCallback(NotificationMessage o, AvaloniaPropertyChangedEventArgs e)
        {
            o.IsBadgeVisible = e.NewValue == null
                ? false
                : true;
        }

        /// <summary>
        /// if <see cref="BadgeAccentBrush"/> or <see cref="ButtonAccentBrush"/>
        /// is null NewValue as <see cref="IBrush"/> is set
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void AccentBrushPropertyChangedCallback(NotificationMessage o, AvaloniaPropertyChangedEventArgs e)
        {
            if (o.BadgeAccentBrush == null)
            {
                o.BadgeAccentBrush = e.NewValue as IBrush;
            }

            if (o.ButtonAccentBrush == null)
            {
                o.ButtonAccentBrush = e.NewValue as IBrush;
            }
        }

        /// <summary>
        /// creates default in or out animation
        /// </summary>
        /// <param name="inDirection"></param>
        /// <returns></returns>
        private Animation.Animation CreateDefaultAnimation(bool inDirection)
        {
            Animation.Animation animation = new Animation.Animation();
            if (inDirection)
            {
                animation.IterationCount = new IterationCount(1);
                KeyFrame keyFrame = new KeyFrame();
                keyFrame.Setters.Add(new Setter
                {
                    Property = OpacityProperty,
                    Value = 1d
                });
                keyFrame.Cue = new Cue(0d);
                animation.Children.Add(keyFrame);

                keyFrame = new KeyFrame();
                keyFrame.Setters.Add(new Setter
                {
                    Property = OpacityProperty,
                    Value = 0d
                });
                keyFrame.Cue = new Cue(1d);
                animation.Children.Add(keyFrame);
            }
            else
            {
                animation.FillMode = FillMode.None;
                animation.IterationCount = new IterationCount(1);
                KeyFrame keyFrame = new KeyFrame();
                keyFrame.Setters.Add(new Setter
                {
                    Property = OpacityProperty,
                    Value = 0d
                });
                keyFrame.Cue = new Cue(0d);
                animation.Children.Add(keyFrame);

                keyFrame = new KeyFrame();
                keyFrame.Setters.Add(new Setter
                {
                    Property = OpacityProperty,
                    Value = 1d
                });
                keyFrame.Cue = new Cue(1d);
                animation.Children.Add(keyFrame);
            }

            return animation;
        }
    }
}
