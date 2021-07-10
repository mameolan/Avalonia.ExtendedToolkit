using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class TagItem
    {
        private const string TxtInput = "txtInput";
        private const string ButtonClose = "ButtonClose";

        private AutoCompleteBox _txtInput;

        private string valueBeforeEditing = "";
        private bool isEscapeClicked;

        private Button _buttonClose;

        internal Action<TagItem> AcceptEdit;

        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Defines the Text property.
        /// </summary>
        public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<TabControl, string>(nameof(Text));

        /// <summary>
        /// Gets or sets ShowCloseButton.
        /// </summary>
        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        /// <summary>
        /// Defines the ShowCloseButton property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowCloseButtonProperty =
        AvaloniaProperty.Register<TagItem, bool>(nameof(ShowCloseButton), defaultValue: true);

        /// <summary>
        /// Gets or sets Selectable.
        /// </summary>
        public bool Selectable
        {
            get { return (bool)GetValue(SelectableProperty); }
            set { SetValue(SelectableProperty, value); }
        }

        /// <summary>
        /// Defines the Selectable property.
        /// </summary>
        public static readonly StyledProperty<bool> SelectableProperty =
        AvaloniaProperty.Register<TagItem, bool>(nameof(Selectable), defaultValue: true);

        /// <summary>
        /// Gets or sets IsInEditMode.
        /// </summary>
        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        /// <summary>
        /// Defines the IsInEditMode property.
        /// </summary>
        public static readonly StyledProperty<bool> IsInEditModeProperty =
        AvaloniaProperty.Register<TagItem, bool>(nameof(IsInEditMode));

        /// <summary>
        /// Gets or sets HorizontalContentAlignment.
        /// </summary>
        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the HorizontalContentAlignment property.
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty =
        AvaloniaProperty.Register<TagItem, HorizontalAlignment>(nameof(HorizontalContentAlignment));

        /// <summary>
        /// Gets or sets VerticalContentAlignment.
        /// </summary>
        public VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the VerticalContentAlignment property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty =
        AvaloniaProperty.Register<TagItem, VerticalAlignment>(nameof(VerticalContentAlignment));

        /// <summary>
        /// Gets or sets IsSelected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Defines the IsSelected property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<TagItem, bool>(nameof(IsSelected), defaultValue: false);

        /// <summary>
        /// Defines the Selected routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> SelectedEvent =
        RoutedEvent.Register<TagItem, RoutedEventArgs>(nameof(SelectedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets Selected eventhandler.
        /// </summary>
        public event EventHandler Selected
        {
            add
            {
                AddHandler(SelectedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedEvent, value);
            }
        }

        /// <summary>
        /// Defines the Closing routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClosingEvent =
        RoutedEvent.Register<TagItem, RoutedEventArgs>(nameof(ClosingEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets Closing eventhandler.
        /// </summary>
        public event EventHandler Closing
        {
            add
            {
                AddHandler(ClosingEvent, value);
            }
            remove
            {
                RemoveHandler(ClosingEvent, value);
            }
        }

        /// <summary>
        /// Defines the Closed routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClosedEvent =
        RoutedEvent.Register<TagItem, RoutedEventArgs>(nameof(ClosedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets Closed eventhandler.
        /// </summary>
        public event EventHandler Closed
        {
            add
            {
                AddHandler(ClosedEvent, value);
            }
            remove
            {
                RemoveHandler(ClosedEvent, value);
            }
        }

        /// <summary>
        /// command which is exceuted after the close event is fired
        /// </summary>
        /// <value></value>
        internal ICommand CloseCommand { get; private set; }
    }
}
