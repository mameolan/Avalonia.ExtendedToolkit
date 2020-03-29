using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Originally from http://xamlcoder.com/blog/2010/11/04/creating-a-metro-ui-style-control/
    /// </summary>
    public class MetroContentControl : ContentControl
    {
        public Type StyleKey => typeof(MetroContentControl);

        private bool transitionLoaded;
        private Grid _root;

        public MetroContentControlState MetroContentControlState
        {
            get { return (MetroContentControlState)GetValue(MetroContentControlStateProperty); }
            set { SetValue(MetroContentControlStateProperty, value); }
        }

        public static readonly StyledProperty<MetroContentControlState> MetroContentControlStateProperty =
            AvaloniaProperty.Register<MetroContentControl, MetroContentControlState>(nameof(MetroContentControlState));

        public bool ReverseTransition
        {
            get { return (bool)GetValue(ReverseTransitionProperty); }
            set { SetValue(ReverseTransitionProperty, value); }
        }

        public static readonly StyledProperty<bool> ReverseTransitionProperty =
            AvaloniaProperty.Register<MetroContentControl, bool>(nameof(ReverseTransition));

        public bool TransitionsEnabled
        {
            get { return (bool)GetValue(TransitionsEnabledProperty); }
            set { SetValue(TransitionsEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> TransitionsEnabledProperty =
            AvaloniaProperty.Register<MetroContentControl, bool>(nameof(TransitionsEnabled), defaultValue: true);

        public bool OnlyLoadTransition
        {
            get { return (bool)GetValue(OnlyLoadTransitionProperty); }
            set { SetValue(OnlyLoadTransitionProperty, value); }
        }

        public static readonly StyledProperty<bool> OnlyLoadTransitionProperty =
            AvaloniaProperty.Register<MetroContentControl, bool>(nameof(OnlyLoadTransition));

        public static readonly RoutedEvent TransitionCompletedEvent =
            RoutedEvent.Register<MetroContentControl, RoutedPropertyChangedEventArgs<object>>(nameof(TransitionCompleted), RoutingStrategies.Bubble);

        public event EventHandler TransitionCompleted
        {
            add { this.AddHandler(TransitionCompletedEvent, value); }
            remove { this.RemoveHandler(TransitionCompletedEvent, value); }
        }

        public MetroContentControl()
        {
            IsVisibleProperty.Changed.AddClassHandler<MetroContentControl>((o, e) => MetroContentControlIsVisibleChanged(o, e));
            MetroContentControlStateProperty.Changed.AddClassHandler<MetroContentControl>((o, e) => MetroContentControlStateChanged(o, e));

            this.TemplateApplied += MetroContentControl_TemplateApplied;
        }

        private void MetroContentControl_TemplateApplied(object sender, TemplateAppliedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                if (!transitionLoaded)
                {
                    this.SetStoryboardEvents();
                    transitionLoaded = this.OnlyLoadTransition;
                    //VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
                    MetroContentControlState = ReverseTransition ? MetroContentControlState.AfterLoadedReverse : MetroContentControlState.AfterLoaded;
                }
                //IsVisibleChanged -= MetroContentControlIsVisibleChanged;
                //IsVisibleChanged += MetroContentControlIsVisibleChanged;
            }
            else
            {
                //var root = (Grid)GetTemplateChild("root");

                //var root= this.Find<Grid>("root");

                if (_root != null)
                {
                    _root.Opacity = 1.0;
                    var transform = ((TranslateTransform)_root.RenderTransform);
                    //if (transform)
                    {
                        var modifiedTransform = transform;//.Clone();
                        modifiedTransform.X = 0;
                        _root.RenderTransform = modifiedTransform;
                    }
                    //else
                    //{
                    //    transform.X = 0;
                    //}
                }
            }
        }

        private void MetroContentControlStateChanged(MetroContentControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.NewValue is MetroContentControlState)
            {
                MetroContentControlState state = (MetroContentControlState)e.NewValue;
                switch (state)
                {
                    case MetroContentControlState.BeforeLoaded:
                        break;

                    case MetroContentControlState.AfterUnLoadedReverse:
                        break;

                    case MetroContentControlState.AfterUnLoaded:
                        break;

                    case MetroContentControlState.AfterLoadedReverse:
                    case MetroContentControlState.AfterLoaded:
                        AfterLoadedStoryboardCompleted(this, EventArgs.Empty);
                        break;
                 }
            }
        }

        private void MetroContentControlIsVisibleChanged(MetroContentControl sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (TransitionsEnabled && !transitionLoaded)
            {
                if (!IsVisible)
                {
                    MetroContentControlState = ReverseTransition ? MetroContentControlState.AfterUnLoadedReverse : MetroContentControlState.AfterUnLoaded;
                    //   VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                }
                else
                {
                    MetroContentControlState = ReverseTransition ? MetroContentControlState.AfterLoadedReverse : MetroContentControlState.AfterUnLoaded;
                    //   VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
                }
            }
        }

        private void MetroContentControlUnloaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                //this.UnsetStoryboardEvents();
                if (transitionLoaded)
                {
                    //    VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                    MetroContentControlState = ReverseTransition ? MetroContentControlState.AfterUnLoadedReverse : MetroContentControlState.AfterUnLoaded;
                }
                //IsVisibleChanged -= MetroContentControlIsVisibleChanged;
            }
        }

        public void Reload()
        {
            if (!TransitionsEnabled || transitionLoaded) return;

            if (ReverseTransition)
            {
                MetroContentControlState = MetroContentControlState.BeforeLoaded;
                MetroContentControlState = MetroContentControlState.AfterUnLoadedReverse;
                //VisualStateManager.GoToState(this, "BeforeLoaded", true);
                //VisualStateManager.GoToState(this, "AfterUnLoadedReverse", true);
            }
            else
            {
                MetroContentControlState = MetroContentControlState.BeforeLoaded;
                MetroContentControlState = MetroContentControlState.AfterLoaded;
                //VisualStateManager.GoToState(this, "BeforeLoaded", true);
                //VisualStateManager.GoToState(this, "AfterLoaded", true);
            }
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            _root = e.NameScope.Find<Grid>("root");
            base.OnTemplateApplied(e);
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();

        //    afterLoadedStoryboard = this.GetTemplateChild("AfterLoadedStoryboard") as Storyboard;
        //    afterLoadedReverseStoryboard = this.GetTemplateChild("AfterLoadedReverseStoryboard") as Storyboard;
        //}

        private void AfterLoadedStoryboardCompleted(object sender, System.EventArgs e)
        {
            if (transitionLoaded)
            {
                this.UnsetStoryboardEvents();
            }
            this.InvalidateVisual();
            this.RaiseEvent(new RoutedEventArgs(TransitionCompletedEvent));
        }

        private void SetStoryboardEvents()
        {
            //if (this.afterLoadedStoryboard != null)
            //{
            //    this.afterLoadedStoryboard.Completed += this.AfterLoadedStoryboardCompleted;
            //}
            //if (this.afterLoadedReverseStoryboard != null)
            //{
            //    this.afterLoadedReverseStoryboard.Completed += this.AfterLoadedStoryboardCompleted;
            //}
        }

        private void UnsetStoryboardEvents()
        {
            //if (this.afterLoadedStoryboard != null)
            //{
            //    this.afterLoadedStoryboard.Completed -= this.AfterLoadedStoryboardCompleted;
            //}
            //if (this.afterLoadedReverseStoryboard != null)
            //{
            //    this.afterLoadedReverseStoryboard.Completed -= this.AfterLoadedStoryboardCompleted;
            //}
        }
    }
}
