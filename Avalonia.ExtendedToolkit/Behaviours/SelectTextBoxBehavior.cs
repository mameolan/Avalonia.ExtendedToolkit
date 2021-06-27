using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using System.Reactive.Linq;
using System;


namespace Avalonia.ExtendedToolkit.Behaviours
{
    /// <summary>
    /// select behaviour of a textbox
    /// </summary>
    public class SelectTextBoxBehavior : Behavior<TextBox>
    {

        /// <summary>
        /// Gets or sets IsSelectAll.
        /// </summary>
        public bool IsSelectAll
        {
            get { return (bool)GetValue(IsSelectAllProperty); }
            set { SetValue(IsSelectAllProperty, value); }
        }

        /// <summary>
        /// Defines the IsSelectAll property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectAllProperty =
        AvaloniaProperty.Register<SelectTextBoxBehavior, bool>(nameof(IsSelectAll), defaultValue: true);
        private IDisposable _disposable;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += AssociatedObjectGotFocus;
            //AssociatedObject.PointerPressed += AssociatedObjectPointerPressed;
            AssociatedObject.DoubleTapped += OnTapped;
            AssociatedObject.AttachedToVisualTree += OnAttachedToVisualTree;

        }

        private void AssociatedObjectPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            SelectBehavior();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= AssociatedObjectGotFocus;
            AssociatedObject.DoubleTapped -= OnTapped;
            AssociatedObject.AttachedToVisualTree += OnAttachedToVisualTree;
            _disposable?.Dispose();
        }

        private void OnAttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            AssociatedObject.Focus();
        }

        private void OnTapped(object sender, RoutedEventArgs e)
        {
            SelectBehavior();
        }

        private void AssociatedObjectPointerPressed(object sender, PointerPressedEventArgs e)
        {
            SelectBehavior();
        }

        private void AssociatedObjectGotFocus(object sender, GotFocusEventArgs e)
        {

            SelectBehavior();
        }

        private void SelectBehavior()
        {

            if (AssociatedObject.Text == null)
            {

                _disposable = AssociatedObject.GetObservable(TextBox.TextProperty)
                            .Subscribe(
                                x =>
                                {
                                    if (string.IsNullOrEmpty(x) == false)
                                    {
                                        SelectBehavior();
                                        _disposable?.Dispose();
                                        _disposable = null;
                                    }

                                });



                return;
            }

            if (IsSelectAll)
            {
                AssociatedObject.SelectionStart = 0;
                AssociatedObject.SelectionEnd = AssociatedObject.Text.Length;
            }
            else
            {
                AssociatedObject.SelectionStart = AssociatedObject.Text.Length;
                AssociatedObject.SelectionEnd = AssociatedObject.Text.Length;
            }
        }
    }
}