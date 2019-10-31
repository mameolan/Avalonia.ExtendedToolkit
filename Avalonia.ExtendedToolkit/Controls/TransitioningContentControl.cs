using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class TransitioningContentControl : ContentControl
    {
        //internal const string PresentationGroup = "PresentationStates";
        //internal const string NormalState = "Normal";
        internal const string PreviousContentPresentationSitePartName = "PreviousContentPresentationSite";
        internal const string CurrentContentPresentationSitePartName = "CurrentContentPresentationSite";

        private ContentPresenter currentContentPresentationSite;
        private ContentPresenter previousContentPresentationSite;
        private bool allowIsTransitioningPropertyWrite;

        public event EventHandler TransitionCompleted;

        public const TransitionType DefaultTransitionState = TransitionType.Default;



        public bool IsTransitioning
        {
            get { return (bool)GetValue(IsTransitioningProperty); }
            private set 
            {

                this.allowIsTransitioningPropertyWrite = true;
                SetValue(IsTransitioningProperty, value);
                this.allowIsTransitioningPropertyWrite = false;
            }
        }


        public static readonly AvaloniaProperty IsTransitioningProperty =
            AvaloniaProperty.Register<TransitioningContentControl, bool>(nameof(IsTransitioning));



        public TransitionType Transition
        {
            get { return (TransitionType)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }


        public static readonly AvaloniaProperty TransitionProperty =
            AvaloniaProperty.Register<TransitioningContentControl, TransitionType>(nameof(Transition), defaultValue: TransitionType.Default);



        public bool RestartTransitionOnContentChange
        {
            get { return (bool)GetValue(RestartTransitionOnContentChangeProperty); }
            set { SetValue(RestartTransitionOnContentChangeProperty, value); }
        }


        public static readonly AvaloniaProperty RestartTransitionOnContentChangeProperty =
            AvaloniaProperty.Register<TransitioningContentControl, bool>(nameof(RestartTransitionOnContentChange));



        public string CustomVisualStatesName
        {
            get { return (string)GetValue(CustomVisualStatesNameProperty); }
            set { SetValue(CustomVisualStatesNameProperty, value); }
        }


        public static readonly AvaloniaProperty CustomVisualStatesNameProperty =
            AvaloniaProperty.Register<TransitioningContentControl, string>(nameof(CustomVisualStatesName), defaultValue: "CustomTransition");




        public TransitioningContentControl()
        {
            IsTransitioningProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnIsTransitioningPropertyChanged(o, e));
            TransitionProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnTransitionChanged(o, e));
            RestartTransitionOnContentChangeProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnRestartTransitionOnContentChangePropertyChanged(o, e));
            ContentProperty.Changed.AddClassHandler<TransitioningContentControl>((o, e) => OnContentChangeChanged(o, e));
        }

        private void OnContentChangeChanged(TransitioningContentControl o, AvaloniaPropertyChangedEventArgs e)
        {
            this.StartTransition(e.OldValue, e.NewValue);
        }

        private void StartTransition(object oldContent, object newContent)
        {
            // both presenters must be available, otherwise a transition is useless.
            if (this.currentContentPresentationSite != null && this.previousContentPresentationSite != null)
            {
                if (this.RestartTransitionOnContentChange)
                {
                    //this.CurrentTransition.Completed -= this.OnTransitionCompleted;
                }

                this.currentContentPresentationSite.SetValue(ContentPresenter.ContentProperty, newContent);
                this.previousContentPresentationSite.SetValue(ContentPresenter.ContentProperty, oldContent);

                // and start a new transition
                if (!this.IsTransitioning || this.RestartTransitionOnContentChange)
                {
                    //if (this.RestartTransitionOnContentChange)
                    //{
                    //    this.CurrentTransition.Completed += this.OnTransitionCompleted;
                    //}

                    this.IsTransitioning = true;
                    //VisualStateManager.GoToState(this, NormalState, false);
                    //VisualStateManager.GoToState(this, this.GetTransitionName(this.Transition), true);
                }
            }
        }

        private void OnRestartTransitionOnContentChangePropertyChanged(TransitioningContentControl o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnRestartTransitionOnContentChangeChanged((bool)e.OldValue, (bool)e.NewValue);
        }

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

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {

            if (this.IsTransitioning)
            {
                this.AbortTransition();
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

            this.previousContentPresentationSite = e.NameScope.Find(PreviousContentPresentationSitePartName) as ContentPresenter;
            this.currentContentPresentationSite = e.NameScope.Find(CurrentContentPresentationSitePartName) as ContentPresenter;

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

        public void ReloadTransition()
        {
            // both presenters must be available, otherwise a transition is useless.
            if (this.currentContentPresentationSite != null && this.previousContentPresentationSite != null)
            {
                if (this.RestartTransitionOnContentChange)
                {
         //           this.CurrentTransition.Completed -= this.OnTransitionCompleted;
                }

                if (!this.IsTransitioning || this.RestartTransitionOnContentChange)
                {
                    if (this.RestartTransitionOnContentChange)
                    {
           //             this.CurrentTransition.Completed += this.OnTransitionCompleted;
                    }

                    this.IsTransitioning = true;
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

        public void AbortTransition()
        {
            // go to normal state and release our hold on the old content.
            //VisualStateManager.GoToState(this, NormalState, false);
            this.IsTransitioning = false;
            this.previousContentPresentationSite?.SetValue(ContentPresenter.ContentProperty, null);
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
                    return this.CustomVisualStatesName;
            }
        }
    }
}
