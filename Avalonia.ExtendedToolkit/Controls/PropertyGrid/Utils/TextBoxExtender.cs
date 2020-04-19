using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using ReactiveHistory;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Provides additional facilities for TextBox control.
    /// </summary>
    public static class TextBoxExtender
    {
        private static CompositeDisposable _disposable = null;
        static StackHistory _stackHistory = new StackHistory();
        static Subject<string> _subject = null;
        static List<string> _myHistory = new List<string>();


        public static readonly AttachedProperty<bool> CommitOnEnterProperty =
            AvaloniaProperty.RegisterAttached<TextBox, bool>("CommitOnEnter", typeof(TextBoxExtender));

        public static bool GetCommitOnEnter(TextBox element)
        {
            return element.GetValue(CommitOnEnterProperty);
        }

        public static void SetCommitOnEnter(TextBox element, bool value)
        {
            element.SetValue(CommitOnEnterProperty, value);
        }

        private static void OnCommitOnEnterChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (textBox == null)
                return;

            var wasBound = (bool)(e.OldValue);
            var needToBind = (bool)(e.NewValue);

            _disposable?.Dispose();
            _disposable = null;
            _subject?.Dispose();
            _subject = null;
            _myHistory.Clear();

            if (wasBound)
            {
                textBox.KeyUp -= TextBoxCommitValue;
                textBox.PropertyChanged -= TextBox_PropertyChanged;
            }


            if (needToBind)
            {
                _subject = new Subject<string>();
                _disposable = new CompositeDisposable();
                textBox.KeyUp += TextBoxCommitValue;
                textBox.PropertyChanged += TextBox_PropertyChanged;
            }
        }

        private static void TextBox_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if(e.Property== TextBox.TextProperty)
            {
                string oldText = e.OldValue as string == null ? string.Empty : (string)e.OldValue;
                string newText = e.NewValue as string == null ? string.Empty : (string)e.NewValue;
                _subject.AsObservable().ObserveWithHistory(x => _myHistory.Add(oldText), newText, _stackHistory);
                _stackHistory.Snapshot(() => 
                {
                    textBox.PropertyChanged -= TextBox_PropertyChanged;
                    //don't allow to switchback to initial value
                    if(string.IsNullOrEmpty(oldText)==false)
                    {
                        textBox.Text = oldText;
                    }
                    
                    
                    textBox.SelectionStart=textBox.SelectionEnd = textBox.Text.Length + 1;
                    textBox.PropertyChanged += TextBox_PropertyChanged;
                }, 
                () => 
                {
                    textBox.PropertyChanged -= TextBox_PropertyChanged;
                    textBox.Text = newText;
                    textBox.PropertyChanged += TextBox_PropertyChanged;
                });
            }
            
        }



        private static void TextBoxCommitValue(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null)
                return;

            if ((e.Key == Key.Enter))
            {
                //do nothing?
                //or set the last entry of the history
                e.Handled = true;
            }
        }

        public static readonly AttachedProperty<bool> CommitOnTypingProperty =
            AvaloniaProperty.RegisterAttached<TextBox, bool>("CommitOnTyping", typeof(TextBoxExtender));

        public static bool GetCommitOnTyping(TextBox element)
        {
            return element.GetValue(CommitOnTypingProperty);
        }

        public static void SetCommitOnTyping(TextBox element, bool value)
        {
            element.SetValue(CommitOnTypingProperty, value);
        }

        private static void OnCommitOnTypingChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (textBox == null)
                return;

            var wasBound = (bool)(e.OldValue);
            var needToBind = (bool)(e.NewValue);

            _disposable?.Dispose();
            _disposable = null;
            _subject?.Dispose();
            _subject = null;
            _myHistory.Clear();



            if (wasBound)
            {
                textBox.PropertyChanged -= TextBox_PropertyChanged;
                textBox.KeyUp -= TextBoxCommitValueWhileTyping;
            }

            if (needToBind)
            {
                _subject = new Subject<string>();
                _disposable = new CompositeDisposable();
                textBox.PropertyChanged += TextBox_PropertyChanged;
                textBox.KeyUp += TextBoxCommitValueWhileTyping;

            }
        }

        private static void TextBoxCommitValueWhileTyping(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Escape)//keep the escape for the roolback active
            {
                var textbox = sender as TextBox;
                if (textbox == null)
                    return;
                //e.Handled = true;
            }
        }

        public static readonly AttachedProperty<bool> RollbackOnEscapeProperty =
            AvaloniaProperty.RegisterAttached<TextBox, bool>("RollbackOnEscape", typeof(TextBoxExtender));

        public static bool GetRollbackOnEscape(TextBox element)
        {
            return element.GetValue(RollbackOnEscapeProperty);
        }

        public static void SetRollbackOnEscape(TextBox element, bool value)
        {
            element.SetValue(RollbackOnEscapeProperty, value);
        }

        private static void OnRollbackOnEscapeChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (textBox == null)
                return;

            var wasBound = (bool)(e.OldValue);
            var needToBind = (bool)(e.NewValue);

            if (wasBound)
                textBox.KeyUp -= TextBoxRollbackValue;

            if (needToBind)
                textBox.KeyUp += TextBoxRollbackValue;
        }

        private static void TextBoxRollbackValue(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null)
                return;

            if (e.Key == Key.Escape)
            {
                var result = _stackHistory.Undo();

                e.Handled = true;
            }
        }

        public static readonly AttachedProperty<bool> SelectAllOnFocusProperty =
            AvaloniaProperty.RegisterAttached<TextBox, bool>("SelectAllOnFocus", typeof(TextBoxExtender));

        public static bool GetSelectAllOnFocus(TextBox element)
        {
            return element.GetValue(SelectAllOnFocusProperty);
        }

        public static void SetSelectAllOnFocus(TextBox element, bool value)
        {
            element.SetValue(SelectAllOnFocusProperty, value);
        }

        private static void OnSelectAllOnFocusChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (textBox == null)
                return;

            var wasBound = (bool)(e.OldValue);
            var needToBind = (bool)(e.NewValue);

            if (wasBound)
                UnhookEvents(textBox);

            if (needToBind)
                HookEvents(textBox);
        }

        private static void HookEvents(TextBox box)
        {
            box.PointerPressed += HandleGotFocus;
            //box.GotKeyboardFocus += HandleGotFocus;
            box.PointerPressed += SelectivelyIgnoreMouseButton;
        }

        private static void UnhookEvents(TextBox box)
        {
            box.PointerPressed -= HandleGotFocus;

            //box.GotKeyboardFocus -= HandleGotFocus;
            box.PointerPressed -= SelectivelyIgnoreMouseButton;
        }

        private static void HandleGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                //emulates selectall function
                tb.SelectionStart = 0;
                int maxLength = tb.Text != null ? tb.Text.Length : 0;
                tb.SelectionEnd = maxLength;
            }
        }

        private static void SelectivelyIgnoreMouseButton(object sender, PointerPressedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb == null)
                return;

            var prop = e.GetCurrentPoint(tb).Properties;
            if (prop.IsLeftButtonPressed == false)
                return;

            //if (!tb.IsKeyboardFocusWithin) //todo: don't know right now how to check
            {
                e.Handled = true;
                tb.Focus();
            }
        }

        static TextBoxExtender()
        {
            CommitOnEnterProperty.Changed.AddClassHandler<TextBox>((o, e) => OnCommitOnEnterChanged(o, e));
            CommitOnTypingProperty.Changed.AddClassHandler<TextBox>((o, e) => OnCommitOnTypingChanged(o, e));
            RollbackOnEscapeProperty.Changed.AddClassHandler<TextBox>((o, e) => OnRollbackOnEscapeChanged(o, e));
            SelectAllOnFocusProperty.Changed.AddClassHandler<TextBox>((o, e) => OnSelectAllOnFocusChanged(o, e));
        }
    }
}
