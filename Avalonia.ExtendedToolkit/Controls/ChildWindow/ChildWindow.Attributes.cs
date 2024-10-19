using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from: https://github.com/punker76/MahApps.Metro.SimpleChildWindow/ 

    public partial class ChildWindow : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(ChildWindow);


        private const string PART_Overlay = "PART_Overlay";
        private const string PART_Window = "PART_Window";
        private const string PART_Header = "PART_Header";
        private const string PART_HeaderThumb = "PART_HeaderThumb";
        private const string PART_Icon = "PART_Icon";
        private const string PART_CloseButton = "PART_CloseButton";
        private const string PART_Border = "PART_Border";
        private const string PART_Content = "PART_Content";

        private Animation.Animation _showAnimation = null;
        private Task _showAnimationTask;
        private Animation.Animation _hideAnimation = null;
        private Task _hideAnimationTask;
        private CancellationTokenSource _hideAnimationTokenSource;


        private IMetroThumb _headerThumb;
        private Button _closeButton;
        private readonly TranslateTransform _moveTransform = new TranslateTransform();
        private Grid _partWindow;
        private Grid _partOverlay;
        private ContentControl _icon;

        DispatcherTimer _autoCloseTimer;


        /// <summary>
        /// Gets or sets AllowMove.
        /// </summary>
        public bool AllowMove
        {
            get { return (bool)GetValue(AllowMoveProperty); }
            set { SetValue(AllowMoveProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AllowMove"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> AllowMoveProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(AllowMove));



        /// <summary>
        /// Gets or sets OffsetX.
        /// </summary>
        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="OffsetX"/> property.
        /// </summary>
        public static readonly StyledProperty<double> OffsetXProperty =
            AvaloniaProperty.Register<ChildWindow, double>(nameof(OffsetX), defaultValue: 0d);



        /// <summary>
        /// Gets or sets OffsetY.
        /// </summary>
        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="OffsetY"/> property.
        /// </summary>
        public static readonly StyledProperty<double> OffsetYProperty =
            AvaloniaProperty.Register<ChildWindow, double>(nameof(OffsetY), defaultValue: 0d);



        /// <summary>
        /// Gets or sets IsModal.
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsModal"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsModalProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(IsModal), defaultValue: true);



        /// <summary>
        /// Gets or sets OverlayBrush.
        /// </summary>
        public IBrush OverlayBrush
        {
            get { return (IBrush)GetValue(OverlayBrushProperty); }
            set { SetValue(OverlayBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="OverlayBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> OverlayBrushProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(OverlayBrush), defaultValue: (IBrush)Brushes.Transparent);



        /// <summary>
        /// Gets or sets CloseOnOverlay.
        /// </summary>
        public bool CloseOnOverlay
        {
            get { return (bool)GetValue(CloseOnOverlayProperty); }
            set { SetValue(CloseOnOverlayProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="CloseOnOverlay"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> CloseOnOverlayProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(CloseOnOverlay));



        /// <summary>
        /// Gets or sets CloseByEscape.
        /// </summary>
        public bool CloseByEscape
        {
            get { return (bool)GetValue(CloseByEscapeProperty); }
            set { SetValue(CloseByEscapeProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="CloseByEscape"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> CloseByEscapeProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(CloseByEscape), defaultValue: true);



        /// <summary>
        /// Gets or sets ShowTitleBar.
        /// </summary>
        public bool ShowTitleBar
        {
            get { return (bool)GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ShowTitleBar"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowTitleBarProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(ShowTitleBar), defaultValue: true);



        /// <summary>
        /// Gets or sets TitleBarHeight.
        /// </summary>
        public int TitleBarHeight
        {
            get { return (int)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleBarHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<int> TitleBarHeightProperty =
            AvaloniaProperty.Register<ChildWindow, int>(nameof(TitleBarHeight), defaultValue: 30);



        /// <summary>
        /// Gets or sets TitleBarBackground.
        /// </summary>
        public IBrush TitleBarBackground
        {
            get { return (IBrush)GetValue(TitleBarBackgroundProperty); }
            set { SetValue(TitleBarBackgroundProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleBarBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> TitleBarBackgroundProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(TitleBarBackground), defaultValue: (IBrush)Brushes.Transparent);



        /// <summary>
        /// Gets or sets TitleBarNonActiveBackground.
        /// </summary>
        public IBrush TitleBarNonActiveBackground
        {
            get { return (IBrush)GetValue(TitleBarNonActiveBackgroundProperty); }
            set { SetValue(TitleBarNonActiveBackgroundProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleBarNonActiveBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> TitleBarNonActiveBackgroundProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(TitleBarNonActiveBackground), (IBrush)Brushes.Gray);



        /// <summary>
        /// Gets or sets NonActiveBorderBrush.
        /// </summary>
        public IBrush NonActiveBorderBrush
        {
            get { return (IBrush)GetValue(NonActiveBorderBrushProperty); }
            set { SetValue(NonActiveBorderBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="NonActiveBorderBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> NonActiveBorderBrushProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(NonActiveBorderBrush), defaultValue: (IBrush)Brushes.Gray);



        /// <summary>
        /// Gets or sets TitleForeground.
        /// </summary>
        public IBrush TitleForeground
        {
            get { return (IBrush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleForeground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> TitleForegroundProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(TitleForeground), (IBrush)Brushes.Black);



        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Title"/> property.
        /// </summary>
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<ChildWindow, string>(nameof(Title));



        /// <summary>
        /// Gets or sets TitleCharacterCasing.
        /// </summary>
        public CharacterCasing TitleCharacterCasing
        {
            get { return (CharacterCasing)GetValue(TitleCharacterCasingProperty); }
            set { SetValue(TitleCharacterCasingProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleCharacterCasing"/> property.
        /// </summary>
        public static readonly StyledProperty<CharacterCasing> TitleCharacterCasingProperty =
            AvaloniaProperty.Register<ChildWindow, CharacterCasing>(nameof(TitleCharacterCasing), defaultValue: CharacterCasing.Normal);



        /// <summary>
        /// Gets or sets TitleHorizontalAlignment.
        /// </summary>
        public HorizontalAlignment TitleHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TitleHorizontalAlignmentProperty); }
            set { SetValue(TitleHorizontalAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleHorizontalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> TitleHorizontalAlignmentProperty =
            AvaloniaProperty.Register<ChildWindow, HorizontalAlignment>(nameof(TitleHorizontalAlignment), defaultValue: HorizontalAlignment.Stretch);



        /// <summary>
        /// Gets or sets TitleVerticalAlignment.
        /// </summary>
        public VerticalAlignment TitleVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TitleVerticalAlignmentProperty); }
            set { SetValue(TitleVerticalAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleVerticalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> TitleVerticalAlignmentProperty =
            AvaloniaProperty.Register<ChildWindow, VerticalAlignment>(nameof(TitleVerticalAlignment), defaultValue: VerticalAlignment.Center);



        /// <summary>
        /// Gets or sets TitleTemplate.
        /// </summary>
        public IDataTemplate TitleTemplate
        {
            get { return (IDataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> TitleTemplateProperty =
            AvaloniaProperty.Register<ChildWindow, IDataTemplate>(nameof(TitleTemplate));



        /// <summary>
        /// Gets or sets TitleFontSize.
        /// </summary>
        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleFontSize"/> property.
        /// </summary>
        public static readonly StyledProperty<double> TitleFontSizeProperty =
            AvaloniaProperty.Register<ChildWindow, double>(nameof(TitleFontSize));



        /// <summary>
        /// Gets or sets TitleFontFamily.
        /// </summary>
        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TitleFontFamily"/> property.
        /// </summary>
        public static readonly StyledProperty<FontFamily> TitleFontFamilyProperty =
            AvaloniaProperty.Register<ChildWindow, FontFamily>(nameof(TitleFontFamily));



        /// <summary>
        /// Gets or sets Icon.
        /// </summary>
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Icon"/> property.
        /// </summary>
        public static readonly StyledProperty<object> IconProperty =
            AvaloniaProperty.Register<ChildWindow, object>(nameof(Icon));



        /// <summary>
        /// Gets or sets IconTemplate.
        /// </summary>
        public IDataTemplate IconTemplate
        {
            get { return (IDataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IconTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> IconTemplateProperty =
            AvaloniaProperty.Register<ChildWindow, IDataTemplate>(nameof(IconTemplate));



        /// <summary>
        /// Gets or sets ShowCloseButton.
        /// </summary>
        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ShowCloseButton"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowCloseButtonProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(ShowCloseButton), defaultValue: true);


        /// <summary>
        /// Gets or sets CloseButtonCommand.
        /// </summary>
        public ICommand CloseButtonCommand
        {
            get { return (ICommand)GetValue(CloseButtonCommandProperty); }
            set { SetValue(CloseButtonCommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="CloseButtonCommand"/> property.
        /// </summary>
        public static readonly StyledProperty<ICommand> CloseButtonCommandProperty =
            AvaloniaProperty.Register<ChildWindow, ICommand>(nameof(CloseButtonCommand));



        /// <summary>
        /// Gets or sets CloseButtonCommandParameter.
        /// </summary>
        public object CloseButtonCommandParameter
        {
            get { return (object)GetValue(CloseButtonCommandParameterProperty); }
            set { SetValue(CloseButtonCommandParameterProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="CloseButtonCommandParameter"/> property.
        /// </summary>
        public static readonly StyledProperty<object> CloseButtonCommandParameterProperty =
            AvaloniaProperty.Register<ChildWindow, object>(nameof(CloseButtonCommandParameter));



        /// <summary>
        /// Gets or sets IsOpen.
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(IsOpen));



        /// <summary>
        /// Gets or sets ChildWindowWidth.
        /// </summary>
        public double ChildWindowWidth
        {
            get { return (double)GetValue(ChildWindowWidthProperty); }
            set { SetValue(ChildWindowWidthProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ChildWindowWidth"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ChildWindowWidthProperty =
            AvaloniaProperty.Register<ChildWindow, double>(nameof(ChildWindowWidth));



        /// <summary>
        /// Gets or sets ChildWindowHeight.
        /// </summary>
        public double ChildWindowHeight
        {
            get { return (double)GetValue(ChildWindowHeightProperty); }
            set { SetValue(ChildWindowHeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ChildWindowHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ChildWindowHeightProperty =
            AvaloniaProperty.Register<ChildWindow, double>(nameof(ChildWindowHeight));



        /// <summary>
        /// Gets or sets ChildWindowImage.
        /// </summary>
        public MessageBoxImage ChildWindowImage
        {
            get { return (MessageBoxImage)GetValue(ChildWindowImageProperty); }
            set { SetValue(ChildWindowImageProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ChildWindowImage"/> property.
        /// </summary>
        public static readonly StyledProperty<MessageBoxImage> ChildWindowImageProperty =
            AvaloniaProperty.Register<ChildWindow, MessageBoxImage>(nameof(ChildWindowImage), defaultValue: MessageBoxImage.None);



        /// <summary>
        /// Gets or sets EnableDropShadow.
        /// </summary>
        public bool EnableDropShadow
        {
            get { return (bool)GetValue(EnableDropShadowProperty); }
            set { SetValue(EnableDropShadowProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="EnableDropShadow"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> EnableDropShadowProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(EnableDropShadow), defaultValue: true);



        /// <summary>
        /// Gets or sets AllowFocusElement.
        /// </summary>
        public bool AllowFocusElement
        {
            get { return (bool)GetValue(AllowFocusElementProperty); }
            set { SetValue(AllowFocusElementProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AllowFocusElement"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> AllowFocusElementProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(AllowFocusElement), defaultValue: true);



        /// <summary>
        /// Gets or sets FocusedElement.
        /// </summary>
        public Control FocusedElement
        {
            get { return (Control)GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="FocusedElement"/> property.
        /// </summary>
        public static readonly StyledProperty<Control> FocusedElementProperty =
            AvaloniaProperty.Register<ChildWindow, Control>(nameof(FocusedElement));



        /// <summary>
        /// Gets or sets GlowBrush.
        /// </summary>
        public IBrush GlowBrush
        {
            get { return (IBrush)GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="GlowBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> GlowBrushProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(GlowBrush), defaultValue: (IBrush)Brushes.Black);



        /// <summary>
        /// Gets or sets NonActiveGlowBrush.
        /// </summary>
        public IBrush NonActiveGlowBrush
        {
            get { return (IBrush)GetValue(NonActiveGlowBrushProperty); }
            set { SetValue(NonActiveGlowBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="NonActiveGlowBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> NonActiveGlowBrushProperty =
            AvaloniaProperty.Register<ChildWindow, IBrush>(nameof(NonActiveGlowBrush), defaultValue: (IBrush)Brushes.Gray);



        /// <summary>
        /// Gets or sets IsAutoCloseEnabled.
        /// </summary>
        public bool IsAutoCloseEnabled
        {
            get { return (bool)GetValue(IsAutoCloseEnabledProperty); }
            set { SetValue(IsAutoCloseEnabledProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsAutoCloseEnabled"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsAutoCloseEnabledProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(IsAutoCloseEnabled));



        /// <summary>
        /// Gets or sets AutoCloseInterval.
        /// </summary>
        public long AutoCloseInterval
        {
            get { return (long)GetValue(AutoCloseIntervalProperty); }
            set { SetValue(AutoCloseIntervalProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="AutoCloseInterval"/> property.
        /// </summary>
        public static readonly StyledProperty<long> AutoCloseIntervalProperty =
            AvaloniaProperty.Register<ChildWindow, long>(nameof(AutoCloseInterval), defaultValue: 5000L);



        /// <summary>
        /// Gets or sets IsWindowHostActive.
        /// </summary>
        public bool IsWindowHostActive
        {
            get { return (bool)GetValue(IsWindowHostActiveProperty); }
            set { SetValue(IsWindowHostActiveProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsWindowHostActive"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsWindowHostActiveProperty =
            AvaloniaProperty.Register<ChildWindow, bool>(nameof(IsWindowHostActive), defaultValue: true);



        /// <summary>
        /// Defines the <see cref="CloseButtonToolTip"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ChildWindow, string> CloseButtonToolTipProperty =
                AvaloniaProperty.RegisterDirect<ChildWindow, string>(
                    nameof(CloseButtonToolTip),
                    o => o.CloseButtonToolTip);

        private string _CloseButtonToolTip;

        /// <summary>
        /// Gets or sets CloseButtonToolTip.
        /// </summary>
        public string CloseButtonToolTip
        {
            get { return _CloseButtonToolTip; }
            set
            {
                SetAndRaise(CloseButtonToolTipProperty, ref _CloseButtonToolTip, value);
            }
        }




        /// <summary>
        /// Defines the <see cref="IsOpenChanged"/> routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> IsOpenChangedEvent =
                    RoutedEvent.Register<ChildWindow, RoutedEventArgs>(nameof(IsOpenChangedEvent), RoutingStrategies.Bubble);


        /// <summary>
        /// Gets or sets IsOpenChanged eventhandler.
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
        /// An event that will be raised when the ChildWindow is closing.
        /// </summary>
        public event EventHandler<CancelEventArgs> Closing;



        /// <summary>
        /// Defines the <see cref="ClosingFinished"/> routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClosingFinishedEvent =
                    RoutedEvent.Register<ChildWindow, RoutedEventArgs>(nameof(ClosingFinishedEvent), RoutingStrategies.Bubble);


        /// <summary>
        /// Gets or sets ClosingFinished eventhandler.
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
        /// Gets the child window result when the dialog will be closed.
        /// </summary>
        public object ChildWindowResult { get; protected set; }

        /// <summary>
        /// Gets the dialog close reason.
        /// </summary>
        public CloseReason ClosedBy { get; private set; } = CloseReason.None;












    }
}
