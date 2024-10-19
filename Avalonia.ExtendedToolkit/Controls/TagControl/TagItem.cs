using System;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Interactivity;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// represents an item in the tagcontrol
    /// base on the work of: https://github.com/niieani/TokenizedInputCs
    /// </summary>
    public partial class TagItem : TemplatedControl, ISelectable
    {
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

        internal static TagItem CreateTagItem(
                        object dataContext,
                        EventHandler closedEvent,
                        EventHandler selectedEvent,
                        EventHandler<AvaloniaPropertyChangedEventArgs> propertyChangedEvent,
                        Action<TagItem> acceptEdit,
                        Thickness margin)
        {
            TagItem tagItem = new TagItem();
            Binding binding = new Binding();
            binding.Source = dataContext;
            tagItem.Bind(TagItem.TextProperty, binding, BindingPriority.LocalValue);
            tagItem.Closed += closedEvent;
            tagItem.Selected += selectedEvent;
            tagItem.PropertyChanged += propertyChangedEvent;
            tagItem.AcceptEdit += acceptEdit;
            tagItem.Margin = margin;
            return tagItem;
        }

        internal void UnregisterEvents(
                        EventHandler closedEvent,
                        EventHandler selectedEvent,
                        EventHandler<AvaloniaPropertyChangedEventArgs> propertyChangedEvent,
                        Action<TagItem> acceptEdit)
        {
            Closed -= closedEvent;
            Selected -= selectedEvent;
            PropertyChanged -= propertyChangedEvent;
            AcceptEdit -= acceptEdit;
        }

        /// <summary>
        /// sets <see cref="IsInEditMode"/> to true
        /// </summary>
        private void OnDoubleTapped(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
            IsInEditMode = true;
            ParentControl.SelectedItem = this.Text;
        }

        /// <summary>
        /// raises the closing event and the closed event
        /// </summary>
        private void ExecuteCloseCommand()
        {
            this.IsSelected = true;

            RaiseEvent(new RoutedEventArgs(ClosingEvent, this));

            RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
        }

        /// <summary>
        /// raises the SelectedEvent
        /// and sets the IsReadonly flag
        /// </summary>
        private void IsSelectedChanged(TagItem tagControl, AvaloniaPropertyChangedEventArgs e)
        {
            tagControl.RaiseEvent(new RoutedEventArgs(SelectedEvent, tagControl));
        }

        /// <summary>
        /// unregisters the events for the <see cref="AutoCompleteBox"/>
        /// </summary>
        private void UnregisterEvents()
        {
            if (_txtInput != null)
            {
                _txtInput.SelectionChanged -= txtInput_SetSelection;
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
                _txtInput.SelectionChanged += txtInput_SetSelection;
                _txtInput.KeyDown += OnInputKeyUp;
                _txtInput.GotFocus += txtInput_GotFocus;
                _txtInput.LostFocus += txtInput_LostFocus;
            }
        }

        /// <summary>
        /// handels the key return for the <see cref="AutoCompleteBox"/>
        /// since the keyup is not handled by the control somehow (?)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.Return && _txtInput != null)
            {
                OnInputKeyUp(_txtInput, e);
            }
        }

        /// <summary>
        /// resolves the controls from the template
        /// </summary>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UnregisterEvents();
            _txtInput = e.NameScope.Find<AutoCompleteBox>(TxtInput);
            _buttonClose = e.NameScope.Find<Button>(ButtonClose);

            RegisterEvents();
            _txtInput?.Focus();
        }

        /// <summary>
        /// manage the availability of the TagItem
        /// </summary>
        private void txtInput_LostFocus(object sender, RoutedEventArgs e)
        {
            var parent = ParentControl;
            if (string.IsNullOrEmpty(Text) == false)
            {
                if (parent.ContainsTagText(this, Text) && string.IsNullOrEmpty(valueBeforeEditing))
                {
                    parent.RemoveTag(this, true); // do not raise RemoveTag event
                }
                else if (parent.ContainsTagText(this, Text) && string.IsNullOrEmpty(valueBeforeEditing) == false)
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

        /// <summary>
        /// sets the focus to the <see cref="AutoCompleteBox"/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            e.Handled = true;
            _txtInput?.Focus();
        }

        /// <summary>
        /// sets the previous text to an internal variable
        /// </summary>
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
            var parent = ParentControl;
            switch (e.Key)
            {
                case Key.Tab:
                case Key.Enter:
                    // accept tag
                    IsSelected = false;
                    IsInEditMode = false;

                    if (!string.IsNullOrWhiteSpace(Text))
                    {
                        if (parent.ContainsTagText(this, Text))
                        {
                            break;
                        }

                        AcceptEdit?.Invoke(this);
                        
                        parent.CreateNewTagItem();
                        
                    }
                    else
                    {
                        parent.Focus();
                    }
                    e.Handled = true;
                    parent.Focus();
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
        private TagControl ParentControl
        {
            get { return this.TryFindParent<TagControl>(); }
        }

        private void txtInput_SetSelection(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(e);
        }

        static TagItem()
        {
            SelectableMixin.Attach<TagItem>(IsSelectedProperty);
        }
    }
}
