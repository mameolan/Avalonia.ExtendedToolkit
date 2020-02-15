using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using Avalonia.Threading;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class BusyIndicator : ContentControl
    {
        /// <summary>
        /// Timer used to delay the initial display and avoid flickering.
        /// </summary>
        private DispatcherTimer _displayAfterTimer = new DispatcherTimer();

        public BusyIndicator()
        {
            _displayAfterTimer.Tick += DisplayAfterTimerElapsed;
            IsBusyProperty.Changed.AddClassHandler<BusyIndicator>((o, e) => OnIsBusyChanged(o, e));
        }

        private void OnIsBusyChanged(BusyIndicator o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnIsBusyChanged(e);
        }

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



        public bool IsContentVisible
        {
            get { return (bool)GetValue(IsContentVisibleProperty); }
            set { SetValue(IsContentVisibleProperty, value); }
        }


        public static readonly AvaloniaProperty<bool> IsContentVisibleProperty =
            AvaloniaProperty.Register<BusyIndicator, bool>(nameof(IsContentVisible));




        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly AvaloniaProperty<bool> IsBusyProperty =
            AvaloniaProperty.Register<BusyIndicator, bool>(nameof(IsBusy));

        public object BusyContent
        {
            get { return (object)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        public static readonly AvaloniaProperty<object> BusyContentProperty =
            AvaloniaProperty.Register<BusyIndicator, object>(nameof(BusyContent));

        public DataTemplate BusyContentTemplate
        {
            get { return (DataTemplate)GetValue(BusyContentTemplateProperty); }
            set { SetValue(BusyContentTemplateProperty, value); }
        }

        public static readonly AvaloniaProperty<DataTemplate> BusyContentTemplateProperty =
            AvaloniaProperty.Register<BusyIndicator, DataTemplate>(nameof(BusyContentTemplate));

        public TimeSpan DisplayAfter
        {
            get { return (TimeSpan)GetValue(DisplayAfterProperty); }
            set { SetValue(DisplayAfterProperty, value); }
        }

        public static readonly AvaloniaProperty<TimeSpan> DisplayAfterProperty =
            AvaloniaProperty.Register<BusyIndicator, TimeSpan>(nameof(DisplayAfter),
                defaultValue: TimeSpan.FromSeconds(0.1));

        public Control FocusAfterBusy
        {
            get { return (Control)GetValue(FocusAfterBusyProperty); }
            set { SetValue(FocusAfterBusyProperty, value); }
        }

        public static readonly AvaloniaProperty<Control> FocusAfterBusyProperty =
            AvaloniaProperty.Register<BusyIndicator, Control>(nameof(FocusAfterBusy));

        public IStyle OverlayStyle
        {
            get { return (IStyle)GetValue(OverlayStyleProperty); }
            set { SetValue(OverlayStyleProperty, value); }
        }

        public static readonly AvaloniaProperty<IStyle> OverlayStyleProperty =
            AvaloniaProperty.Register<BusyIndicator, IStyle>(nameof(OverlayStyle));

        public IStyle ProgressBarStyle
        {
            get { return (IStyle)GetValue(ProgressBarStyleProperty); }
            set { SetValue(ProgressBarStyleProperty, value); }
        }

        public static readonly AvaloniaProperty<IStyle> ProgressBarStyleProperty =
            AvaloniaProperty.Register<BusyIndicator, IStyle>(nameof(ProgressBarStyle));

        private void DisplayAfterTimerElapsed(object sender, EventArgs e)
        {
            _displayAfterTimer.Stop();
            IsContentVisible = true;
        }

    }
}