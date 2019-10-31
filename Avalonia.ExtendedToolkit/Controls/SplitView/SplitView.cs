using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.LogicalTree;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class SplitView: ContentControl
    {
        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }

        public static readonly AvaloniaProperty CompactPaneLengthProperty =
            AvaloniaProperty.Register<SplitView, double>(nameof(CompactPaneLength), defaultValue:0d);

        //public object Content
        //{
        //    get { return (object)GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}

        //public static readonly AvaloniaProperty ContentProperty =
        //    AvaloniaProperty.Register<SplitView, object>(nameof(Content));




        internal string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }


        internal static readonly AvaloniaProperty StateProperty =
            AvaloniaProperty.Register<SplitView, string>(nameof(State));




        public SplitViewDisplayMode DisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        public static readonly AvaloniaProperty DisplayModeProperty =
            AvaloniaProperty.Register<SplitView, SplitViewDisplayMode>(nameof(DisplayMode), defaultValue: SplitViewDisplayMode.Overlay);

        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        public static readonly AvaloniaProperty IsPaneOpenProperty =
            AvaloniaProperty.Register<SplitView, bool>(nameof(IsPaneOpen));

        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }

        public static readonly AvaloniaProperty OpenPaneLengthProperty =
            AvaloniaProperty.Register<SplitView, double>(nameof(OpenPaneLength), defaultValue: 0d);

        public object Pane
        {
            get { return (object)GetValue(PaneProperty); }
            set { SetValue(PaneProperty, value); }
        }

        public static readonly AvaloniaProperty PaneProperty =
            AvaloniaProperty.Register<SplitView, object>(nameof(Pane));

        public IBrush PaneBackground
        {
            get { return (IBrush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }

        public static readonly AvaloniaProperty PaneBackgroundProperty =
            AvaloniaProperty.Register<SplitView, IBrush>(nameof(PaneBackground));

        public IBrush PaneForeground
        {
            get { return (IBrush)GetValue(PaneForegroundProperty); }
            set { SetValue(PaneForegroundProperty, value); }
        }

        public static readonly AvaloniaProperty PaneForegroundProperty =
            AvaloniaProperty.Register<SplitView, IBrush>(nameof(PaneForeground));

        public SplitViewPanePlacement PanePlacement
        {
            get { return (SplitViewPanePlacement)GetValue(PanePlacementProperty); }
            set { SetValue(PanePlacementProperty, value); }
        }

        public static readonly AvaloniaProperty PanePlacementProperty =
            AvaloniaProperty.Register<SplitView, SplitViewPanePlacement>(nameof(PanePlacement), defaultValue: SplitViewPanePlacement.Left);

        public SplitViewTemplateSettings TemplateSettings
        {
            get { return (SplitViewTemplateSettings)GetValue(TemplateSettingsProperty); }
            set { SetValue(TemplateSettingsProperty, value); }
        }

        public static readonly AvaloniaProperty TemplateSettingsProperty =
            AvaloniaProperty.Register<SplitView, SplitViewTemplateSettings>(nameof(TemplateSettings));

        private Rectangle lightDismissLayer;
        private RectangleGeometry paneClipRectangle;

        /// <summary>
        ///     Occurs when the <see cref="SplitView" /> pane is closed.
        /// </summary>
        public event EventHandler PaneClosed;

        /// <summary>
        ///     Occurs when the <see cref="SplitView" /> pane is closing.
        /// </summary>
        public event EventHandler<SplitViewPaneClosingEventArgs> PaneClosing;

        public SplitView()
        {
            CompactPaneLengthProperty.Changed.AddClassHandler<SplitView>((o, e) => OnMetricsChanged(o, e));
            DisplayModeProperty.Changed.AddClassHandler<SplitView>((o, e) => OnStateChanged(o, e));
            IsPaneOpenProperty.Changed.AddClassHandler<SplitView>((o, e) => OnIsPaneOpenChanged(o, e));
            OpenPaneLengthProperty.Changed.AddClassHandler<SplitView>((o, e) => OnMetricsChanged(o, e));
            PaneProperty.Changed.AddClassHandler<SplitView>((o, e) => UpdateLogicalChild(o, e));
            PanePlacementProperty.Changed.AddClassHandler<SplitView>((o, e) => OnStateChanged(o, e));
            TemplateSettings = new SplitViewTemplateSettings(this);

            WidthProperty.Changed.AddClassHandler<SplitView>((o, e) => OnSizeChanged(o, e));
            HeightProperty.Changed.AddClassHandler<SplitView>((o, e) => OnSizeChanged(o, e));
        }

        private void OnSizeChanged(SplitView o, AvaloniaPropertyChangedEventArgs e)
        {
            if (this.paneClipRectangle != null)
            {
                this.paneClipRectangle.Rect = new Rect(0, 0, this.OpenPaneLength, (double)this.Height);
            }
        }

        protected virtual void ChangeVisualState(bool animated = true, bool reset = false)
        {
            if (this.paneClipRectangle != null)
            {
                this.paneClipRectangle.Rect = new Rect(0, 0, this.OpenPaneLength, (double)this.Height); // We could also use ActualHeight and subscribe to the SizeChanged property
            }

            State = string.Empty;
            if (this.IsPaneOpen)
            {
                State += "Open";
                switch (this.DisplayMode)
                {
                    case SplitViewDisplayMode.CompactInline:
                        State += "Inline";
                        break;

                    default:
                        State += this.DisplayMode.ToString();
                        break;
                }

                State += this.PanePlacement.ToString();
            }
            else
            {
                State += "Closed";
                if (this.DisplayMode == SplitViewDisplayMode.CompactInline
                    || this.DisplayMode == SplitViewDisplayMode.CompactOverlay)
                {
                    State += "Compact";
                    State += this.PanePlacement.ToString();
                }
            }

            //if (reset)
            //{
            //    VisualStateManager.GoToState(this, "None", animated);
            //}

            //VisualStateManager.GoToState(this, state, animated);
        }

        private void OnIsPaneOpenChanged(SplitView sender, AvaloniaPropertyChangedEventArgs e)
        {
            var newValue = (bool)e.NewValue;
            var oldValue = (bool)e.OldValue;

            if (sender == null
                || newValue == oldValue)
                return;

            if (newValue)
                sender.ChangeVisualState(); // Open pane
            else
                sender.OnIsPaneOpenChanged(); // Close pane
        }

        private void OnStateChanged(SplitView sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender?.ChangeVisualState();
        }

        private void OnMetricsChanged(SplitView sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender?.TemplateSettings?.Update();
            sender?.ChangeVisualState(true, true);
            throw new NotImplementedException();
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            this.paneClipRectangle = e.NameScope.Find("PaneClipRectangle") as RectangleGeometry;

            this.lightDismissLayer = e.NameScope.Find("LightDismissLayer") as Rectangle;
            if (this.lightDismissLayer != null)
            {
                this.lightDismissLayer.Tapped += OnLightDismiss;
            }

            //this.ExecuteWhenLoaded(() =>
            //{
                this.TemplateSettings.Update();
                this.ChangeVisualState(false);
            //});
        }

        protected IAvaloniaList<ILogical> LogicalChildren
        {
            get
            {
                AvaloniaList<ILogical> children = new AvaloniaList<ILogical>();

                if (this.Pane != null)
                {
                    children.Add(this.Pane as ILogical);
                }

                if (this.Content != null)
                {
                    children.Add(this.Content as ILogical);
                }

                return children;
            }
        }

        private void OnLightDismiss(object sender, Interactivity.RoutedEventArgs e)
        {
            this.SetValue(IsPaneOpenProperty, false);
        }

        private static void UpdateLogicalChild(SplitView splitView, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is StyledElement oldChild)
            {
                splitView.LogicalChildren.Remove(oldChild);
            }

            if (e.NewValue is StyledElement newChild)
            {
                splitView.LogicalChildren.Add(newChild);
                newChild.DataContext = splitView.DataContext;
            }
        }

        protected virtual void OnIsPaneOpenChanged()
        {
            var cancel = false;
            if (this.PaneClosing != null)
            {
                var args = new SplitViewPaneClosingEventArgs();
                foreach (var paneClosingDelegates in this.PaneClosing.GetInvocationList())
                {
                    var eventHandler = paneClosingDelegates as EventHandler<SplitViewPaneClosingEventArgs>;
                    if (eventHandler == null)
                        continue;

                    eventHandler(this, args);
                    if (args.Cancel)
                    {
                        cancel = true;
                        break;
                    }
                }
            }
            if (!cancel)
            {
                this.ChangeVisualState();
                this.PaneClosed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                this.SetValue(IsPaneOpenProperty, false);
            }
        }
    }
}