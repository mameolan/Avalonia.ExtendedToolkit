using System;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.LogicalTree;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    ///     Represents a container with two views; one view for the main content and another
    ///     view that is typically used for
    ///     navigation commands.
    /// </summary>
    public partial class SplitView : ContentControl
    {
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

            var state = string.Empty;
            if (this.IsPaneOpen)
            {
                state += "Open";
                switch (this.DisplayMode)
                {
                    case SplitViewDisplayMode.CompactInline:
                        state += "Inline";
                        break;

                    default:
                        state += this.DisplayMode.ToString();
                        break;
                }

                state += this.PanePlacement.ToString();
            }
            else
            {
                state += "Closed";
                if (this.DisplayMode == SplitViewDisplayMode.CompactInline
                    || this.DisplayMode == SplitViewDisplayMode.CompactOverlay)
                {
                    state += "Compact";
                    state += this.PanePlacement.ToString();
                }
            }

            state += "Storyboard";

            if (reset)
            {
                //VisualStateManager.GoToState(this, "None", animated);
                State = "None";
            }

            SetValue(StateProperty, state);
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
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            Grid grid = e.NameScope.Find<Grid>("PaneRoot");

            this.paneClipRectangle = grid.Clip as RectangleGeometryExt; // .<RectangleGeometryExt>("PaneClipRectangle"); //

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
