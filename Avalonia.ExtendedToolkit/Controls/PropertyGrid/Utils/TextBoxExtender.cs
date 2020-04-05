using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Provides additional facilities for TextBox control.
    /// </summary>
    public static class TextBoxExtender
    {
        #region CommitOnEnter

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
            
            if (wasBound)
                textBox.KeyUp -= TextBoxCommitValue;

            if (needToBind)
                textBox.KeyUp += TextBoxCommitValue;
        }

        private static void TextBoxCommitValue(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null)
                return;

            if ((e.Key == Key.Enter))
            {
#warning todo how to this in avalonia?
                //BindingExpression expression = textbox.GetBindingExpression(TextBox.TextProperty);
                //if (expression != null)
                //    expression.UpdateSource();
                e.Handled = true;
            }
        }

        #endregion CommitOnEnter

        #region CommitOnTyping

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

            if (wasBound)
                textBox.KeyUp -= TextBoxCommitValueWhileTyping;

            if (needToBind)
                textBox.KeyUp += TextBoxCommitValueWhileTyping;
        }

        private static void TextBoxCommitValueWhileTyping(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Escape)//keep the escape for the roolback active
            {
                var textbox = sender as TextBox;
                if (textbox == null)
                    return;
                //BindingExpression expression = textbox.GetBindingExpression(TextBox.TextProperty);
                //if (expression != null)
                //    expression.UpdateSource();
                e.Handled = true;
            }
        }

        #endregion CommitOnTyping

        #region RollbackOnEscape

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
                //BindingExpression expression = textbox.GetBindingExpression(TextBox.TextProperty);
                //if (expression != null)
                //    expression.UpdateTarget();
                e.Handled = true;
            }
        }

        #endregion RollbackOnEscape

        #region SelectAllOnFocus

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

            //if (!tb.IsKeyboardFocusWithin) //todo: don't now right now how to check
            {
                e.Handled = true;
                tb.Focus();
            }
        }

        #endregion SelectAllOnFocus

        static TextBoxExtender()
        {
            CommitOnEnterProperty.Changed.AddClassHandler<TextBox>((o, e) => OnCommitOnEnterChanged(o, e));
            CommitOnTypingProperty.Changed.AddClassHandler<TextBox>((o, e) => OnCommitOnTypingChanged(o, e));
            RollbackOnEscapeProperty.Changed.AddClassHandler<TextBox>((o, e) => OnRollbackOnEscapeChanged(o, e));
            SelectAllOnFocusProperty.Changed.AddClassHandler<TextBox>((o, e) => OnSelectAllOnFocusChanged(o, e));
        }
    }
}
