using System;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// TransitioningContentControl
    /// </summary>
    public class TransitioningContentControl : ContentControl
    {
#warning finish implementation
        //internal const string PresentationGroup = "PresentationStates";
        //internal const string NormalState = "Normal";
        internal const string PreviousContentPresentationSitePartName = "PreviousContentPresentationSite";

        internal const string CurrentContentPresentationSitePartName = "CurrentContentPresentationSite";

        private ContentPresenter currentContentPresentationSite;
        private ContentPresenter previousContentPresentationSite;
        private bool allowIsTransitioningPropertyWrite;

        /// <summary>
        /// TransitionCompleted event
        /// </summary>
        public event EventHandler TransitionCompleted;

        /// <summary>
        /// Default TransitionType
        /// </summary>
        public const TransitionType DefaultTransitionState = TransitionType.Default;

        /// <summary>
        /// get IsTransitioning
        /// </summary>
        public bool IsTransitioning
        {
            get { return (bool)GetValue(IsTransitioningProperty); }
            private set
            {
                allowIsTransitioningPropertyWrite = true;
                SetValue(IsTransitioningProperty, value);
                allowIsTransitioningPropertyWrite = false;
            }
        }

        /// <summary>
        /// <see cref="IsTransitioning"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsTransitioningProperty =
            AvaloniaProperty.Register<TransitioningContentControl, bool>(nameof(IsTransitioning));

        /// <summary>
        /// get/set Transition state
        /// </summary>
        public TransitionType Transition
        {
            get { return (TransitionType)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        /// <summary>
        /// <see cref="Transition"/>
        /// </summary>
        public static readonly StyledProperty<TransitionType> TransitionProperty =
            AvaloniaProperty.Register<TransitioningContentControl, TransitionType>(nameof(Transition), defaultValue: TransitionType.Default);

        /// <summary>
        /// get/sets RestartTransitionOnContentChange
        /// </summary>
        public bool RestartTransitionOnContentChange
        {
            get { return (bool)GetValue(RestartTransitionOnContentChangeProperty); }
            set { SetValue(RestartTransitionOnContentChangeProperty, value); }
        }

        /// <summary>
        /// <see cref="RestartTransitionOnContentChange"/>
        /// </summary>
        public static readonly StyledProperty<bool> RestartTransitionOnContentChangeProperty =
            AvaloniaProperty.Register<TransitioningContentControl, bool>(nameof(RestartTransitionOnContentChange));

        /// <summary>
        /// get set CustomVisualStatesName
        /// </summary>
        internal string CustomVisualStatesName
        {
            get { return (string)GetValue(CustomVisualStatesNameProperty); }
            set { SetValue(CustomVisualStatesNameProperty, value); }
        }

        /// <summary>
        /// <see cref="CustomVisualStatesName"/>
        /// </summary>
        internal static readonly StyledProperty<string> CustomVisualStatesNameProperty =
            AvaloniaProperty.Register<TransitioningContentControl, string>(nameof(CustomVisualStatesName), defaultValue: "CustomTransition");

        /// <summary>
        /// registers some changed listeners
        /// </summary>
        public TransitioningContentControl()
        {
            IsTransitioningProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnIsTransitioningPropertyChanged(o, e));
            TransitionProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnTransitionChanged(o, e));
            RestartTransitionOnContentChangeProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnRestartTransitionOnContentChangePropertyChanged(o, e));
            ContentProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnContentChangeChanged(o, e));
        }

        private void OnContentChangeChanged(TransitioningContentControl o, AvaloniaPropertyChangedEventArgs e)
        {
            StartTransition(e.OldValue, e.NewValue);
        }

        private void StartTransition(object oldContent, object newContent)
        {
            // both presenters must be available, otherwise a transition is useless.
            if (currentContentPresentationSite != null && previousContentPresentationSite != null)
            {
                if (RestartTransitionOnContentChange)
                {
                    //this.CurrentTransition.Completed -= this.OnTransitionCompleted;
                }

                currentContentPresentationSite.SetValue(ContentPresenter.ContentProperty, newContent);
                previousContentPresentationSite.SetValue(ContentPresenter.ContentProperty, oldContent);

                // and start a new transition
                if (!IsTransitioning || RestartTransitionOnContentChange)
                {
                    //if (this.RestartTransitionOnContentChange)
                    //{
                    //    this.CurrentTransition.Completed += this.OnTransitionCompleted;
                    //}

                    IsTransitioning = true;
                    //VisualStateManager.GoToState(this, NormalState, false);
                    //VisualStateManager.GoToState(this, this.GetTransitionName(this.Transition), true);
                }
            }
        }

        private void OnRestartTransitionOnContentChangePropertyChanged(TransitioningContentControl o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnRestartTransitionOnContentChangeChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        /// <summary>
        /// overriden control can add functionality
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnRestartTransitionOnContentChangeChanged(bool oldValue, bool newValue)
        {
        }

        private void OnTransitionChanged(TransitioningContentControl source, AvaloniaPropertyChangedEventArgs e)
        {
            var oldTransition = (TransitionType)e.OldValue;
            var newTransition = (TransitionType)e.NewValue;

            if (source.IsTransitioning)
            {
                source.AbortTransition();
            }

            //// find new transition
            //Storyboard newStoryboard = source.GetStoryboard(newTransition);

            //// unable to find the transition.
            //if (newStoryboard == null)
            //{
            //    // could be during initialization of xaml that presentationgroups was not yet defined
            //    if (VisualStates.TryGetVisualStateGroup(source, PresentationGroup) == null)
            //    {
            //        // will delay check
            //        source.CurrentTransition = null;
            //    }
            //    else
            //    {
            //        // revert to old value
            //        source.SetValue(TransitionProperty, oldTransition);

            //        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Temporary removed exception message", newTransition));
            //    }
            //}
            //else
            //{
            //    source.CurrentTransition = newStoryboard;
            //}
        }

        private void OnIsTransitioningPropertyChanged(TransitioningContentControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (!o.allowIsTransitioningPropertyWrite)
            {
                o.IsTransitioning = (bool)e.OldValue;
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// gets the controls of the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            if (IsTransitioning)
            {
                AbortTransition();
            }

            //if (this.CustomVisualStates != null && this.CustomVisualStates.Any())
            //{
            //    var presentationGroup = VisualStates.TryGetVisualStateGroup(this, PresentationGroup);
            //    if (presentationGroup != null)
            //    {
            //        foreach (var state in this.CustomVisualStates)
            //        {
            //            presentationGroup.States.Add(state);
            //        }
            //    }
            //}

            base.OnTemplateApplied(e);

            previousContentPresentationSite = e.NameScope.Find(PreviousContentPresentationSitePartName) as ContentPresenter;
            currentContentPresentationSite = e.NameScope.Find(CurrentContentPresentationSitePartName) as ContentPresenter;

            // hookup currenttransition
            //Storyboard transition = this.GetStoryboard(this.Transition);
            //this.CurrentTransition = transition;
            //if (transition == null)
            //{
            //    var invalidTransition = this.Transition;
            //    // revert to default
            //    this.Transition = DefaultTransitionState;

            //    throw new MahAppsException($"'{invalidTransition}' transition could not be found!");
            //}

            //VisualStateManager.GoToState(this, NormalState, false);
        }

        /// <summary>
        /// reloads the transaction
        /// </summary>
        public void ReloadTransition()
        {
            // both presenters must be available, otherwise a transition is useless.
            if (currentContentPresentationSite != null && previousContentPresentationSite != null)
            {
                if (RestartTransitionOnContentChange)
                {
         //           this.CurrentTransition.Completed -= this.OnTransitionCompleted;
                }

                if (!IsTransitioning || RestartTransitionOnContentChange)
                {
                    if (RestartTransitionOnContentChange)
                    {
           //             this.CurrentTransition.Completed += this.OnTransitionCompleted;
                    }

                    IsTransitioning = true;
                    //VisualStateManager.GoToState(this, NormalState, false);
                    //VisualStateManager.GoToState(this, this.GetTransitionName(this.Transition), true);
                }
            }
        }

        //private void OnTransitionCompleted(object sender, EventArgs e)
        //{
        //    var clockGroup = sender as ClockGroup;
        //    this.AbortTransition();
        //    if (clockGroup == null || clockGroup.CurrentState == ClockState.Stopped)
        //    {
        //        this.TransitionCompleted?.Invoke(this, new RoutedEventArgs());
        //    }
        //}

        /// <summary>
        /// aborts the transaction
        /// </summary>
        public void AbortTransition()
        {
            // go to normal state and release our hold on the old content.
            //VisualStateManager.GoToState(this, NormalState, false);
            IsTransitioning = false;
            previousContentPresentationSite?.SetValue(ContentPresenter.ContentProperty, null);
        }

        //private Storyboard GetStoryboard(TransitionType newTransition)
        //{
        //    VisualStateGroup presentationGroup = VisualStates.TryGetVisualStateGroup(this, PresentationGroup);
        //    Storyboard newStoryboard = null;
        //    if (presentationGroup != null)
        //    {
        //        var transitionName = this.GetTransitionName(newTransition);
        //        newStoryboard = presentationGroup.States
        //                                         .OfType<VisualState>()
        //                                         .Where(state => state.Name == transitionName)
        //                                         .Select(state => state.Storyboard)
        //                                         .FirstOrDefault();
        //    }

        //    return newStoryboard;
        //}

        private string GetTransitionName(TransitionType transition)
        {
            switch (transition)
            {
                default:
                case TransitionType.Default:
                    return "DefaultTransition";

                case TransitionType.Normal:
                    return "Normal";

                case TransitionType.Up:
                    return "UpTransition";

                case TransitionType.Down:
                    return "DownTransition";

                case TransitionType.Right:
                    return "RightTransition";

                case TransitionType.RightReplace:
                    return "RightReplaceTransition";

                case TransitionType.Left:
                    return "LeftTransition";

                case TransitionType.LeftReplace:
                    return "LeftReplaceTransition";

                case TransitionType.Custom:
                    return CustomVisualStatesName;
            }
        }
    }
}
