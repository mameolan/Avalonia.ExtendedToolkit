using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //Originally from http://xamlcoder.com/blog/2010/11/04/creating-a-metro-ui-style-control/

    /// <summary>
    /// metro content control
    /// </summary>
    public class MetroContentControl : ContentControl
    {
#warning finish implementation
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(MetroContentControl);

        private bool transitionLoaded;
        private Grid _root;

        /// <summary>
        /// get/set MetroContentControlState
        /// </summary>
        public MetroContentControlState MetroContentControlState
        {
            get { return (MetroContentControlState)GetValue(MetroContentControlStateProperty); }
            set { SetValue(MetroContentControlStateProperty, value); }
        }

        /// <summary>
        /// <see cref="MetroContentControlState"/>
        /// </summary>
        public static readonly StyledProperty<MetroContentControlState> MetroContentControlStateProperty =
            AvaloniaProperty.Register<MetroContentControl, MetroContentControlState>(nameof(MetroContentControlState));

        /// <summary>
        /// get/sets ReverseTransition
        /// </summary>
        public bool ReverseTransition
        {
            get { return (bool)GetValue(ReverseTransitionProperty); }
            set { SetValue(ReverseTransitionProperty, value); }
        }

        /// <summary>
        /// <see cref="ReverseTransition"/>
        /// </summary>
        public static readonly StyledProperty<bool> ReverseTransitionProperty =
            AvaloniaProperty.Register<MetroContentControl, bool>(nameof(ReverseTransition));

        /// <summary>
        /// get/sets TransitionsEnabled
        /// </summary>
        public bool TransitionsEnabled
        {
            get { return (bool)GetValue(TransitionsEnabledProperty); }
            set { SetValue(TransitionsEnabledProperty, value); }
        }

        /// <summary>
        /// <see cref="TransitionsEnabled"/>
        /// </summary>
        public static readonly StyledProperty<bool> TransitionsEnabledProperty =
            AvaloniaProperty.Register<MetroContentControl, bool>(nameof(TransitionsEnabled), defaultValue: true);

        /// <summary>
        /// get/sets OnlyLoadTransition
        /// </summary>
        public bool OnlyLoadTransition
        {
            get { return (bool)GetValue(OnlyLoadTransitionProperty); }
            set { SetValue(OnlyLoadTransitionProperty, value); }
        }

        /// <summary>
        /// <see cref="OnlyLoadTransition"/>
        /// </summary>
        public static readonly StyledProperty<bool> OnlyLoadTransitionProperty =
            AvaloniaProperty.Register<MetroContentControl, bool>(nameof(OnlyLoadTransition));

        /// <summary>
        /// <see cref="TransitionCompleted"/>
        /// </summary>
        public static readonly RoutedEvent TransitionCompletedEvent =
            RoutedEvent.Register<MetroContentControl, RoutedPropertyChangedEventArgs<object>>(nameof(TransitionCompleted), RoutingStrategies.Bubble);

        /// <summary>
        /// get/set TransitionCompleted
        /// </summary>
        public event EventHandler TransitionCompleted
        {
            add { this.AddHandler(TransitionCompletedEvent, value); }
            remove { this.RemoveHandler(TransitionCompletedEvent, value); }
        }

        /// <summary>
        /// registers some changed handlers
        /// </summary>
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

        /// <summary>
        /// reloads the states
        /// </summary>
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

        /// <summary>
        /// gets the root grid 
        /// </summary>
        /// <param name="e"></param>
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
