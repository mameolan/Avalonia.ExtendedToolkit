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

            ChnageBusyState();
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            ChnageBusyState();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the BusyContent is visible.
        /// </summary>
        protected bool IsContentVisible
        {
            get;
            set;
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly AvaloniaProperty IsBusyProperty =
            AvaloniaProperty.Register<BusyIndicator, bool>(nameof(IsBusy));

        public object BusyContent
        {
            get { return (object)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        public static readonly AvaloniaProperty BusyContentProperty =
            AvaloniaProperty.Register<BusyIndicator, object>(nameof(BusyContent));

        public DataTemplate BusyContentTemplate
        {
            get { return (DataTemplate)GetValue(BusyContentTemplateProperty); }
            set { SetValue(BusyContentTemplateProperty, value); }
        }

        public static readonly AvaloniaProperty BusyContentTemplateProperty =
            AvaloniaProperty.Register<BusyIndicator, DataTemplate>(nameof(BusyContentTemplate));

        public BusyStatus BusyStatus
        {
            get { return (BusyStatus)GetValue(BusyStatusProperty); }
            set { SetValue(BusyStatusProperty, value); }
        }

        public static readonly AvaloniaProperty BusyStatusProperty =
            AvaloniaProperty.Register<BusyIndicator, BusyStatus>(nameof(BusyStatus));

        public VisibilityState VisibilityState
        {
            get { return (VisibilityState)GetValue(VisibilityStateProperty); }
            set { SetValue(VisibilityStateProperty, value); }
        }

        public static readonly AvaloniaProperty VisibilityStateProperty =
            AvaloniaProperty.Register<BusyIndicator, VisibilityState>(nameof(VisibilityState));

        public TimeSpan DisplayAfter
        {
            get { return (TimeSpan)GetValue(DisplayAfterProperty); }
            set { SetValue(DisplayAfterProperty, value); }
        }

        public static readonly AvaloniaProperty DisplayAfterProperty =
            AvaloniaProperty.Register<BusyIndicator, TimeSpan>(nameof(DisplayAfter),
                defaultValue: TimeSpan.FromSeconds(0.1));

        public Control FocusAfterBusy
        {
            get { return (Control)GetValue(FocusAfterBusyProperty); }
            set { SetValue(FocusAfterBusyProperty, value); }
        }

        public static readonly AvaloniaProperty FocusAfterBusyProperty =
            AvaloniaProperty.Register<BusyIndicator, Control>(nameof(FocusAfterBusy));

        public Style OverlayStyle
        {
            get { return (Style)GetValue(OverlayStyleProperty); }
            set { SetValue(OverlayStyleProperty, value); }
        }

        public static readonly AvaloniaProperty OverlayStyleProperty =
            AvaloniaProperty.Register<BusyIndicator, Style>(nameof(OverlayStyle));

        public Style ProgressBarStyle
        {
            get { return (Style)GetValue(ProgressBarStyleProperty); }
            set { SetValue(ProgressBarStyleProperty, value); }
        }

        public static readonly AvaloniaProperty ProgressBarStyleProperty =
            AvaloniaProperty.Register<BusyIndicator, Style>(nameof(ProgressBarStyle));

        private void DisplayAfterTimerElapsed(object sender, EventArgs e)
        {
            _displayAfterTimer.Stop();
            IsContentVisible = true;
            ChnageBusyState();
        }

        protected virtual void ChnageBusyState()
        {
            BusyStatus = IsBusy ? BusyStatus.Busy : BusyStatus.Idle;
            VisibilityState = IsContentVisible ? VisibilityState.Visible : VisibilityState.Hidden;
        }
    }
}