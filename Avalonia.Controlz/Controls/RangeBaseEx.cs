using Avalonia.Controls.Primitives;
using Avalonia.Controlz.EventArgs;
using Avalonia.Interactivity;
using System;

namespace Avalonia.Controlz.Controls
{
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //
    // Licensed to The Avalonia Project under MIT License, courtesy of The .NET Foundation.

    /// <summary>
    /// Range base with extended properties.
    /// Used by the <see cref="SliderEx"/>
    /// </summary>
    public class RangeBaseEx : RangeBase
    {
        /// <summary>
        /// Registered the ValueChangedEvent
        /// </summary>
        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<double>> ValueChangedEvent =
                    RoutedEvent.Register<RangeBaseEx, RoutedPropertyChangedEventArgs<double>>(nameof(ValueChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Get/sets the ValueChanged eventhandler
        /// </summary>
        public event EventHandler<RoutedPropertyChangedEventArgs<double>> ValueChanged
        {
            add
            {
                AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ValueChangedEvent, value);
            }
        }

        /// <summary>
        /// sets the changed handler for
        /// MinimumProperty, MaximumProperty, ValueProperty 
        /// </summary>
        public RangeBaseEx()
        {
            MinimumProperty.Changed.AddClassHandler<RangeBaseEx>((o, e) => OnMinimumChanged(o, e));
            MaximumProperty.Changed.AddClassHandler<RangeBaseEx>((o, e) => OnMaximumChanged(o, e));
            ValueProperty.Changed.AddClassHandler<RangeBaseEx>((o, e) => OnValueChanged(o, e));
        }

        private void OnValueChanged(RangeBaseEx ctrl, AvaloniaPropertyChangedEventArgs e)
        {
            //animationpeer missing ...
            ctrl.OnValueChanged((double)e.OldValue, (double)e.NewValue);
            

        }

        private void OnMaximumChanged(RangeBaseEx ctrl, AvaloniaPropertyChangedEventArgs e)
        {
            //animationpeer missing ...
            ctrl.OnMaximumChanged((double)e.OldValue, (double)e.NewValue);
        }

        /// <summary>
        ///     This method is invoked when the Maximum property changes.
        /// </summary>
        /// <param name="oldMaximum">The old value of the Maximum property.</param>
        /// <param name="newMaximum">The new value of the Maximum property.</param>
        protected virtual void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
        }

        private void OnMinimumChanged(RangeBaseEx ctrl, AvaloniaPropertyChangedEventArgs e)
        {
            //animationpeer missing ...

            ctrl.OnMinimumChanged((double)e.OldValue, (double)e.NewValue);
        }

        /// <summary>
        ///     This method is invoked when the Minimum property changes.
        /// </summary>
        /// <param name="oldMinimum">The old value of the Minimum property.</param>
        /// <param name="newMinimum">The new value of the Minimum property.</param>
        protected virtual void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
        }

        /// <summary>
        ///     This method is invoked when the Value property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Value property.</param>
        /// <param name="newValue">The new value of the Value property.</param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            RaiseEvent(new RoutedPropertyChangedEventArgs<double>(oldValue, newValue, ValueChangedEvent));
        }
    }
}
