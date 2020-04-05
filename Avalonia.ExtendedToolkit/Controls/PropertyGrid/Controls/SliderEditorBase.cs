using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls
{
    /// <summary>
    /// Base class for Slider-like editor implementations.
    /// </summary>
    public abstract class SliderEditorBase : Slider
    {
        public static readonly RoutedEvent<PropertyEditingEventArgs> PropertyEditingStartedEvent =
                    RoutedEvent.Register<SliderEditorBase, PropertyEditingEventArgs>
            (nameof(PropertyEditingStartedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property editing started.
        /// </summary>
        public event PropertyEditingEventHandler PropertyEditingStarted
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

        public static readonly RoutedEvent<PropertyEditingEventArgs> PropertyEditingFinishedEvent =
                    RoutedEvent.Register<SliderEditorBase, PropertyEditingEventArgs>
            (nameof(PropertyEditingFinishedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property editing is finished.
        /// </summary>
        public event PropertyEditingEventHandler PropertyEditingFinished
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

        public PropertyDescriptor PropertyDescriptor
        {
            get { return (PropertyDescriptor)GetValue(PropertyDescriptorProperty); }
            set { SetValue(PropertyDescriptorProperty, value); }
        }

        public static readonly StyledProperty<PropertyDescriptor> PropertyDescriptorProperty =
            AvaloniaProperty.Register<SliderEditorBase, PropertyDescriptor>(nameof(PropertyDescriptor));

        public SliderEditorBase()
        {
            PropertyGrid.PropertyEditingStartedEvent.AddClassHandler(
                typeof(SliderEditorBase), (o,e)=>
                {
                    PropertyEditingEventArgs args = e as PropertyEditingEventArgs;

                    var evt = new PropertyEditingEventArgs(PropertyEditingStartedEvent,
                        this, args.PropertyDescriptor);

                    RaiseEvent(evt);
                });

            PropertyGrid.PropertyEditingFinishedEvent.AddClassHandler(
                typeof(SliderEditorBase), (o, e) =>
                {
                    PropertyEditingEventArgs args = e as PropertyEditingEventArgs;

                    var evt = new PropertyEditingEventArgs(PropertyEditingFinishedEvent,
                        this, args.PropertyDescriptor);

                    RaiseEvent(evt);
                });
        }

        protected override void OnThumbDragStarted(VectorEventArgs e)
        {
            OnPropertyEditingStarted();
            base.OnThumbDragStarted(e);
        }

        protected override void OnThumbDragCompleted(VectorEventArgs e)
        {
            OnPropertyEditingFinished();
            base.OnThumbDragCompleted(e);
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
