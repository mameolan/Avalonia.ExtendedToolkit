using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/xceedsoftware/wpftoolkit

    /// <summary>
    /// A control to provide a visual indicator when an application is busy.
    /// </summary>
    public class BusyIndicator : ContentControl
    {
        /// <summary>
        /// Timer used to delay the initial display and avoid flickering.
        /// </summary>
        private DispatcherTimer _displayAfterTimer = new DispatcherTimer();

        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(BusyIndicator);

        /// <summary>
        /// Gets or sets a value indicating whether the BusyContent is visible.
        /// </summary>
        public bool IsContentVisible
        {
            get { return (bool)GetValue(IsContentVisibleProperty); }
            private set { SetValue(IsContentVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsContentVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsContentVisibleProperty =
            AvaloniaProperty.Register<BusyIndicator, bool>(nameof(IsContentVisible));

        /// <summary>
        /// Gets or sets a value indicating whether the busy indicator should show.
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// <see cref="IsBusy"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsBusyProperty =
            AvaloniaProperty.Register<BusyIndicator, bool>(nameof(IsBusy));

        /// <summary>
        /// Gets or sets a value indicating the busy content to display to the user.
        /// </summary>
        public object BusyContent
        {
            get { return (object)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        /// <summary>
        /// <see cref="BusyContent"/>
        /// </summary>
        public static readonly StyledProperty<object> BusyContentProperty =
            AvaloniaProperty.Register<BusyIndicator, object>(nameof(BusyContent));

        /// <summary>
        /// Gets or sets a value indicating the template to use for displaying the busy content to the user.
        /// </summary>
        public DataTemplate BusyContentTemplate
        {
            get { return (DataTemplate)GetValue(BusyContentTemplateProperty); }
            set { SetValue(BusyContentTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="BusyContentTemplate"/>
        /// </summary>
        public static readonly StyledProperty<DataTemplate> BusyContentTemplateProperty =
            AvaloniaProperty.Register<BusyIndicator, DataTemplate>(nameof(BusyContentTemplate));

        /// <summary>
        /// Gets or sets a value indicating how long to delay before displaying the busy content.
        /// </summary>
        public TimeSpan DisplayAfter
        {
            get { return (TimeSpan)GetValue(DisplayAfterProperty); }
            set { SetValue(DisplayAfterProperty, value); }
        }

        /// <summary>
        /// <see cref="DisplayAfter"/>
        /// </summary>
        public static readonly StyledProperty<TimeSpan> DisplayAfterProperty =
            AvaloniaProperty.Register<BusyIndicator, TimeSpan>(nameof(DisplayAfter),
                defaultValue: TimeSpan.FromSeconds(0.1));

        /// <summary>
        /// Gets or sets a Control that should get the focus when the busy indicator disapears.
        /// </summary>
        public Control FocusAfterBusy
        {
            get { return (Control)GetValue(FocusAfterBusyProperty); }
            set { SetValue(FocusAfterBusyProperty, value); }
        }

        /// <summary>
        /// <see cref="FocusAfterBusy"/>
        /// </summary>
        public static readonly StyledProperty<Control> FocusAfterBusyProperty =
            AvaloniaProperty.Register<BusyIndicator, Control>(nameof(FocusAfterBusy));

        /// <summary>
        /// Gets or sets a value indicating the style to use for the overlay.
        /// </summary>
        public IStyle OverlayStyle
        {
            get { return (IStyle)GetValue(OverlayStyleProperty); }
            set { SetValue(OverlayStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="OverlayStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> OverlayStyleProperty =
            AvaloniaProperty.Register<BusyIndicator, IStyle>(nameof(OverlayStyle));

        /// <summary>
        /// Gets or sets a value indicating the style to use for the progress bar.
        /// </summary>
        public IStyle ProgressBarStyle
        {
            get { return (IStyle)GetValue(ProgressBarStyleProperty); }
            set { SetValue(ProgressBarStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="ProgressBarStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> ProgressBarStyleProperty =
            AvaloniaProperty.Register<BusyIndicator, IStyle>(nameof(ProgressBarStyle));

        /// <summary>
        /// init _displayAfterTimer.Tick
        /// registered IsBusy
        /// </summary>
        public BusyIndicator()
        {
            _displayAfterTimer.Tick += DisplayAfterTimerElapsed;
            IsBusyProperty.Changed.AddClassHandler<BusyIndicator>((o, e) => OnIsBusyChanged(o, e));
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnIsBusyChanged(BusyIndicator o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnIsBusyChanged(e);
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="e"></param>
        private void OnIsBusyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (IsBusy)
            {
                if (DisplayAfter.Equals(TimeSpan.Zero))
                {
                    // Go visible now
                    IsContentVisible = true;
                }
                else
                {
                    // Set a timer to go visible
                    _displayAfterTimer.Interval = DisplayAfter;
                    _displayAfterTimer.Start();
                }
            }
            else
            {
                // No longer visible
                _displayAfterTimer.Stop();
                IsContentVisible = false;

                if (this.FocusAfterBusy != null)
                {
                    //this.FocusAfterBusy.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                    //{
                    //    this.FocusAfterBusy.Focus();
                    //}
                    //));
                    this.FocusAfterBusy.Focus();
                }
            }
        }

        /// <summary>
        /// Handler for the DisplayAfterTimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayAfterTimerElapsed(object sender, EventArgs e)
        {
            _displayAfterTimer.Stop();
            IsContentVisible = true;
        }
    }
}
