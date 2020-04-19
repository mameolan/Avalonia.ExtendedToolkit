using System;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing;
using Avalonia.Input;
using Avalonia.Interactivity;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Simple Expression Blend like double editor.
    /// </summary>
    public class DoubleEditor : TemplatedControl
    {
        public Type StyleKey => typeof(DoubleEditor);

        private Point _dragStartPoint;
        private Point _lastDragPoint;

        private double _changeValue;
        private double _changeOffset;
        private bool _isMouseDown;
        private KeyModifiers _currentKeyModifiers;
        private const double DragTolerance = 2.0;

        public ICommand Increase { get; }

        public ICommand Decrease { get; }

        //[TypeConverter(typeof(LengthConverter))]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<DoubleEditor, double>(nameof(Value), defaultValue: 0d);

        public double SmallChange
        {
            get { return (double)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }

        public static readonly StyledProperty<double> SmallChangeProperty =
            AvaloniaProperty.Register<DoubleEditor, double>(nameof(SmallChange), defaultValue: 1.0d);

        public double LargeChange
        {
            get { return (double)GetValue(LargeChangeProperty); }
            set { SetValue(LargeChangeProperty, value); }
        }

        public static readonly StyledProperty<double> LargeChangeProperty =
            AvaloniaProperty.Register<DoubleEditor, double>(nameof(LargeChange), defaultValue: 1.0d);

        public double DefaultChange
        {
            get { return (double)GetValue(DefaultChangeProperty); }
            set { SetValue(DefaultChangeProperty, value); }
        }

        public static readonly StyledProperty<double> DefaultChangeProperty =
            AvaloniaProperty.Register<DoubleEditor, double>(nameof(DefaultChange), defaultValue: 1.0d);

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly StyledProperty<double> MinimumProperty =
            AvaloniaProperty.Register<DoubleEditor, double>(nameof(Minimum), defaultValue: 0.0d);

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly StyledProperty<double> MaximumProperty =
            AvaloniaProperty.Register<DoubleEditor, double>(nameof(Maximum), defaultValue: double.MaxValue);

        public int MaxPrecision
        {
            get { return (int)GetValue(MaxPrecisionProperty); }
            set { SetValue(MaxPrecisionProperty, value); }
        }

        public static readonly StyledProperty<int> MaxPrecisionProperty =
            AvaloniaProperty.Register<DoubleEditor, int>(nameof(MaxPrecision), defaultValue: 0);

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public static readonly StyledProperty<bool> IsDraggingProperty =
            AvaloniaProperty.Register<DoubleEditor, bool>(nameof(IsDragging));

        public PropertyDescriptor PropertyDescriptor
        {
            get { return (PropertyDescriptor)GetValue(PropertyDescriptorProperty); }
            set { SetValue(PropertyDescriptorProperty, value); }
        }

        public static readonly StyledProperty<PropertyDescriptor> PropertyDescriptorProperty =
            AvaloniaProperty.Register<DoubleEditor, PropertyDescriptor>(nameof(PropertyDescriptor));

        public static readonly RoutedEvent<RoutedEventArgs> PropertyEditingStartedEvent =
                    RoutedEvent.Register<DoubleEditor, RoutedEventArgs>
            (nameof(PropertyEditingStartedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property editing is started.
        /// </summary>
        public event EventHandler PropertyEditingStarted
        {
            add
            {
                AddHandler(PropertyEditingStartedEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyEditingStartedEvent, value);
            }
        }

        public static readonly RoutedEvent<RoutedEventArgs> PropertyEditingFinishedEvent =
                    RoutedEvent.Register<DoubleEditor, RoutedEventArgs>(nameof(PropertyEditingFinishedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property editing is finished.
        /// </summary>
        public event EventHandler PropertyEditingFinished
        {
            add
            {
                AddHandler(PropertyEditingFinishedEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyEditingFinishedEvent, value);
            }
        }

        public DoubleEditor()
        {
            Increase = ReactiveCommand.Create<object>(x => OnIncrease(x)
            , outputScheduler: RxApp.MainThreadScheduler);

            Decrease = ReactiveCommand.Create<object>(x => OnDecrease(x)
            , outputScheduler: RxApp.MainThreadScheduler);

            ValueProperty.Changed.AddClassHandler<DoubleEditor>((o, e) => ValueChanged(o, e));
            MinimumProperty.Changed.AddClassHandler<DoubleEditor>((o, e) => OnMinimumChanged(o, e));
            MaximumProperty.Changed.AddClassHandler<DoubleEditor>((o, e) => OnMaximumChanged(o, e));
            IsDraggingProperty.Changed.AddClassHandler<DoubleEditor>((o, e) => OnIsDraggingChanged(o, e));
        }

        /// <summary>
        /// Invoked when an unhandled attached event reaches an element in its route
        /// that is derived from this class.
        /// Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The that contains the event data. This event data reports details
        /// about the mouse button that was pressed and the handled state.</param>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            var prop = e.GetCurrentPoint(this).Properties;

            if(prop.IsLeftButtonPressed)
            {
                _isMouseDown = true;
                _dragStartPoint = e.GetPosition(this);

                Focus();
                e.Pointer.Capture(this);

                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when an unhandled attached event reaches an element in its route
        /// that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The that contains the event data.</param>
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);

            Point position = e.GetPosition(this);
            Vector vector = position - _dragStartPoint;

            if (_isMouseDown)
            {
                if (!IsDragging)
                {
                    if (vector.Length > DragTolerance)
                    {
                        IsDragging = true;
                        e.Handled = true;

                        _dragStartPoint = position;

                        _lastDragPoint = _dragStartPoint;
                        _changeValue = Value;
                        _changeOffset = 0;
                    }
                }
                else
                {
                    Vector offset = position - _lastDragPoint;
                    double offsetLength = Math.Round(offset.Length);

                    if (offsetLength != 0)
                    {
                        CalculateValue((offset.X > offset.Y) ? offsetLength : -offsetLength);
                        _lastDragPoint = position;
                    }
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when an unhandled routed event reaches an element in its route that is
        /// derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The that contains the event data. The event data reports that
        /// the mouse button was released.</param>
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);
            if (IsDragging || _isMouseDown)
            {
                e.Handled = true;
                IsDragging = false;
                _isMouseDown = false;
            }

            e.Pointer.Capture(null);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            _currentKeyModifiers = e.KeyModifiers;
        }

        private void OnIsDraggingChanged(DoubleEditor doubleEditor, AvaloniaPropertyChangedEventArgs e)
        {
            doubleEditor.OnIsDraggingChanged();
        }

        private void OnMaximumChanged(DoubleEditor doubleEditor, AvaloniaPropertyChangedEventArgs e)
        {
        }

        private void OnMinimumChanged(DoubleEditor doubleEditor, AvaloniaPropertyChangedEventArgs e)
        {
        }

        private void ValueChanged(DoubleEditor doubleEditor, AvaloniaPropertyChangedEventArgs e)
        {
            if (!doubleEditor.IsInitialized)
                return;

            doubleEditor.Value = doubleEditor.EnforceLimitsAndPrecision((double)e.NewValue);
        }

        protected virtual void OnIsDraggingChanged()
        {
            if (IsDragging)
            {
                OnPropertyEditingStarted();
            }
            else
            {
                OnPropertyEditingFinished();
            }
        }

        private void OnDecrease(object x)
        {
            _changeValue = Value;
            _changeOffset = 0;
            CalculateValue(-DefaultChange);
        }

        private void OnIncrease(object x)
        {
            _changeValue = Value;
            _changeOffset = 0;
            CalculateValue(DefaultChange);
        }

        private void CalculateValue(double chageValue)
        {
            //
            // Calculate the base ammount of chage based on...
            //
            // On Mouse Click & Control Key Press
            if ((_currentKeyModifiers & KeyModifiers.Control) != KeyModifiers.None)
            {
                chageValue *= SmallChange;
            }
            // On Mouse Click & Shift Key Press
            else if ((_currentKeyModifiers & KeyModifiers.Shift) != KeyModifiers.None)
            {
                chageValue *= LargeChange;
            }
            else
            {
                chageValue *= DefaultChange;
            }

            _changeOffset += chageValue;
            double newValue = _changeValue + _changeOffset;
            //
            // Make sure the change is line up with Max/Min Limits and set the precission as specified.
            Value = EnforceLimitsAndPrecision(newValue);

            return;
        }

        private double EnforceLimitsAndPrecision(double value)
        {
            return Math.Round(Math.Max(Minimum, Math.Min(Maximum, value)), MaxPrecision);
        }

        /// <summary>
        /// Raises the <see cref="PropertyEditingStarted"/> event.
        /// </summary>
        protected virtual void OnPropertyEditingStarted()
        {
            RaiseEvent(new PropertyEditingEventArgs(PropertyEditingStartedEvent, this, PropertyDescriptor));
        }

        /// <summary>
        /// Raises the <see cref="PropertyEditingFinished"/> event.
        /// </summary>
        protected virtual void OnPropertyEditingFinished()
        {
            RaiseEvent(new PropertyEditingEventArgs(PropertyEditingFinishedEvent, this, PropertyDescriptor));
        }
    }
}
