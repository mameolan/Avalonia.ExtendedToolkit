using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using ReactiveUI;
using System;
using System.Linq;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// represents an item in the tagcontrol
    /// </summary>
    public class TagItem : TemplatedControl, ISelectable
    {
        private const string TxtInput = "txtInput";
        private const string ButtonClose = "ButtonClose";

        private AutoCompleteBox _txtInput;

        private string valueBeforeEditing = "";
        private bool isEscapeClicked;
        private Button _buttonClose;

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

        /// <summary>
        /// registers IsSelected changed
        /// LostFocus, and creates the CloseCommand
        /// </summary>
        public TagItem()
        {
            IsSelectedProperty.Changed.AddClassHandler<TagItem>((o, e) => IsSelectedChanged(o, e));

            CloseCommand = ReactiveCommand.Create(() => ExecuteCloseCommand(), outputScheduler: RxApp.MainThreadScheduler);

            DoubleTapped += OnDoubleTapped;
        }

        /// <summary>
        /// sets <see cref="IsInEditMode"/> to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoubleTapped(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
            IsInEditMode = true;
        }

        /// <summary>
        /// raises the closing event and the closed event
        /// </summary>
        private void ExecuteCloseCommand()
        {
            RaiseEvent(new RoutedEventArgs(ClosingEvent, this));

            RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
        }

        /// <summary>
        /// raises the SelectedEvent
        /// and sets the IsReadonly flag
        /// </summary>
        /// <param name="tagControl"></param>
        /// <param name="e"></param>
        private void IsSelectedChanged(TagItem tagControl, AvaloniaPropertyChangedEventArgs e)
        {
            tagControl.RaiseEvent(new RoutedEventArgs(SelectedEvent, tagControl));
        }

        /// <summary>
        /// on left click the IsSelected flag is set
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed && Selectable)
            {
                IsSelected = !IsSelected;

                if (IsSelected == false)
                {
                    IsInEditMode = false;
                    this.Focus();
                }
            }
        }

        /// <summary>
        /// unregisters the events for the <see cref="AutoCompleteBox"/>
        /// </summary>
        private void UnregisterEvents()
        {
            if (_txtInput != null)
            {
                _txtInput.KeyDown -= OnInputKeyUp;
                _txtInput.GotFocus -= txtInput_GotFocus;
                _txtInput.LostFocus -= txtInput_LostFocus;
            }
        }

        /// <summary>
        /// registers the events for the <see cref="AutoCompleteBox"/>
        /// </summary>
        private void RegisterEvents()
        {
            if (_txtInput != null)
            {
                _txtInput.KeyDown += OnInputKeyUp;
                _txtInput.GotFocus += txtInput_GotFocus;
                _txtInput.LostFocus += txtInput_LostFocus;
            }
        }

        /// <summary>
        /// resolves the controls from the template
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            UnregisterEvents();
            _txtInput = e.NameScope.Find<AutoCompleteBox>(TxtInput);
            _buttonClose = e.NameScope.Find<Button>(ButtonClose);
            RegisterEvents();
            _txtInput?.Focus();
        }

        private void txtInput_LostFocus(object sender, RoutedEventArgs e)
        {
            var parent = GetParent();
            if (string.IsNullOrEmpty(Text) == false)
            {
                if (IsDuplicate(parent, Text) && string.IsNullOrEmpty(valueBeforeEditing))
                {
                    parent.RemoveTag(this, true); // do not raise RemoveTag event
                }
                else if (IsDuplicate(parent, Text) && valueBeforeEditing != "")
                {
                    Text = valueBeforeEditing;
                }
                else if (isEscapeClicked)
                {
                    this.Text = valueBeforeEditing;
                    if (Text == null)
                    {
                        parent?.RemoveTag(this, true);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(valueBeforeEditing))
                {
                    this.Text = valueBeforeEditing;
                }
                else if (isEscapeClicked && string.IsNullOrEmpty(this.Text))
                {
                    parent?.RemoveTag(this, true);
                }
            }
            isEscapeClicked = false;
        }

        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            e.Handled = true;
            _txtInput?.Focus();
        }

        /// <summary>
        /// sets the previous text to an internal variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput_GotFocus(object sender, GotFocusEventArgs e)
        {
            valueBeforeEditing = _txtInput.Text;
            e.Handled = true;
        }

        /// <summary>
        /// handels the key input if a TagItem i.e. reset it's value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputKeyUp(object sender, KeyEventArgs e)
        {
            var parent = GetParent();
            switch (e.Key)
            {
                case Key.Tab:
                case Key.Enter:
                    // accept tag

                    if (!string.IsNullOrWhiteSpace(Text))
                    {
                        if (IsDuplicate(parent, Text))
                        {
                            break;
                        }
                    }
                    else
                    {
                        parent.Focus();
                    }
                    e.Handled = true;
                    IsInEditMode = false;
                    IsSelected = false;
                    IsSelected = true;
                    break;

                case Key.Escape: // reject tag
                    isEscapeClicked = true;
                    IsInEditMode = false;
                    parent.Focus();
                    break;
            }
        }

        /// <summary>
        /// gets the parent control
        /// </summary>
        /// <returns></returns>
        private TagControl GetParent()
        {
            return Parent as TagControl;
        }

        /// <summary>
        /// checks for duplicated items
        /// </summary>
        /// <param name="tagControl"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        private static bool IsDuplicate(TagControl tagControl, string compareTo)
        {
            var duplicateCount = (from TagItem item in tagControl.GetTagItems()
                                  where item.Text.ToLower() == compareTo.ToLower()
                                  select item).Count();
            if (duplicateCount > 1)
                return true;

            return false;
        }
    }
}