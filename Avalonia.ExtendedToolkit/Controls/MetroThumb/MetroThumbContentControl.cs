using System;
using System.Diagnostics;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// The MetroThumbContentControl control can be used 
    /// for titles or something else and 
    /// enables basic drag movement functionality.
    /// </summary>
    public class MetroThumbContentControl : ContentControlEx, IMetroThumb
    {
        /// <summary>
        /// style key for this control
        /// </summary>
        public new Type StyleKey => typeof(MetroThumbContentControl);

        private Point startDragPoint;
        private PixelPoint startDragScreenPoint;
        private PixelPoint? oldDragScreenPoint;

        /// <summary>
        /// <see cref="DragStarted"/>
        /// </summary>
        public static readonly RoutedEvent<VectorEventArgs> DragStartedEvent =
            RoutedEvent.Register<MetroThumbContentControl, VectorEventArgs>(nameof(DragStartedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/set drag started event
        /// </summary>
        public event EventHandler<VectorEventArgs> DragStarted
        {
            add
            {
                AddHandler(DragStartedEvent, value);
            }
            remove
            {
                RemoveHandler(DragStartedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="DragDelta"/>
        /// </summary>
        public static readonly RoutedEvent<VectorEventArgs> DragDeltaEvent =
            RoutedEvent.Register<MetroThumbContentControl, VectorEventArgs>(nameof(DragDeltaEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/set drag delta event
        /// </summary>
        public event EventHandler<VectorEventArgs> DragDelta
        {
            add
            {
                AddHandler(DragDeltaEvent, value);
            }
            remove
            {
                RemoveHandler(DragDeltaEvent, value);
            }
        }

        /// <summary>
        /// <see cref="DragCompleted"/>
        /// </summary>
        public static readonly RoutedEvent<VectorEventArgs> DragCompletedEvent =
            RoutedEvent.Register<MetroThumbContentControl, VectorEventArgs>(nameof(DragCompletedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/set dragcompleted event
        /// </summary>
        public event EventHandler<VectorEventArgs> DragCompleted
        {
            add
            {
                AddHandler(DragCompletedEvent, value);
            }
            remove
            {
                RemoveHandler(DragCompletedEvent, value);
            }
        }
        public event EventHandler<RoutedEventArgs> DoubleTapped;

        /// <summary>
        /// Indicates that the left mouse button is pressed and is over the MetroThumbContentControl.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        /// <summary>
        /// <see cref="IsDragging"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsDraggingProperty =
            AvaloniaProperty.Register<MetroThumbContentControl, bool>(nameof(IsDragging));

        /// <summary>
        /// cancels the drag
        /// </summary>
        public void CancelDragAction()
        {
            if (!this.IsDragging)
            {
                return;
            }

            //if (this.IsMouseCaptured)
            //{
            //    this.ReleaseMouseCapture();
            //}

            this.ClearValue(IsDraggingProperty);
            var horizontalChange = this.oldDragScreenPoint.Value.X - this.startDragScreenPoint.X;
            var verticalChange = this.oldDragScreenPoint.Value.Y - this.startDragScreenPoint.Y;

            var args = new VectorEventArgs()
            {
                Handled = true,
                RoutedEvent = DragCompletedEvent,
                Vector = new Vector(horizontalChange, verticalChange)
            };

            this.RaiseEvent(args);
        }

        /// <summary>
        /// handels mouse capturing on pressed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            var properties = e.GetCurrentPoint(this).Properties;

            if (properties.IsLeftButtonPressed == false)
                return;

            if (!this.IsDragging)
            {
                e.Handled = true;
                try
                {
                    // focus me
                    this.Focus();
                    // now capture the mouse for the drag action
                    //this.CaptureMouse();
                    e.Pointer.Capture(this);
                    // so now we are in dragging mode
                    this.SetValue(IsDraggingProperty, true);
                    // get the mouse points
                    this.startDragPoint = e.GetPosition(this);
                    this.oldDragScreenPoint = this.startDragScreenPoint = this.PointToScreen(this.startDragPoint);

                    var args = new VectorEventArgs()
                    {
                        RoutedEvent = DragStartedEvent,
                        Vector = new Vector(this.startDragPoint.X, this.startDragPoint.Y)
                    };

                    this.RaiseEvent(args);
                }
                catch (Exception exception)
                {
                    Trace.TraceError($"{this}: Something went wrong here: {exception} {Environment.NewLine} {exception.StackTrace}");
                    this.CancelDragAction();
                }
            }

            base.OnPointerPressed(e);
        }

        /// <summary>
        /// handels pointer capturing on release
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            if (/*e.Pointer.Captured.*/this.IsDragging)
            {
                e.Handled = true;
                // now we are in normal mode
                this.ClearValue(IsDraggingProperty);
                // release the captured mouse
                e.Pointer.Capture(null);
                //this.ReleaseMouseCapture();
                // get the current mouse position and call the completed event with the horizontal/vertical change
                PixelPoint currentMouseScreenPoint = this.PointToScreen(e.GetPosition(this));
                var horizontalChange = currentMouseScreenPoint.X - this.startDragScreenPoint.X;
                var verticalChange = currentMouseScreenPoint.Y - this.startDragScreenPoint.Y;

                var args = new VectorEventArgs()
                {
                    Handled = false,
                    RoutedEvent = DragCompletedEvent,
                    Vector = new Vector(horizontalChange, verticalChange)
                };

                this.RaiseEvent(args);
            }
        }

        /// <summary>
        /// cancels drag action
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
        {
            // Cancel the drag action if we lost capture
            MetroThumbContentControl thumb = (MetroThumbContentControl)e.Source;
            if (e.Pointer.Captured != thumb)
            {
                thumb.CancelDragAction();
            }
        }

        /// <summary>
        /// handels movement on dragging
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);

            if (!this.IsDragging)
            {
                return;
            }

            if (oldDragScreenPoint.HasValue)
            {
                Point currentDragPoint = e.GetPosition(this);
                // Get client point and convert it to screen point
                PixelPoint currentDragScreenPoint = this.PointToScreen(currentDragPoint);
                if (currentDragScreenPoint != this.oldDragScreenPoint)
                {
                    this.oldDragScreenPoint = currentDragScreenPoint;
                    e.Handled = true;
                    var horizontalChange = currentDragPoint.X - this.startDragPoint.X;
                    var verticalChange = currentDragPoint.Y - this.startDragPoint.Y;

                    var ev = new VectorEventArgs
                    {
                        RoutedEvent = DragDeltaEvent,
                        Vector = new Vector(horizontalChange, verticalChange)
                    };

                    this.RaiseEvent(ev);
                }
            }
            else
            {
                this.ClearValue(IsDraggingProperty);
                this.startDragPoint = new Point(0, 0);
            }
        }

        //private void ReleaseCurrentDevice()
        //{
        //    if (this.currentDevice != null)
        //    {
        //        // Set the reference to null so that we don't re-capture in the OnLostTouchCapture() method
        //        var temp = this.currentDevice;
        //        this.currentDevice = null;
        //        //this.ReleaseTouchCapture(temp);
        //    }
        //}

        //private void CaptureCurrentDevice(TouchEventArgs e)
        //{
        //    bool gotTouch = this.CaptureTouch(e.TouchDevice);
        //    if (gotTouch)
        //    {
        //        this.currentDevice = e.TouchDevice;
        //    }
        //}
    }
}
