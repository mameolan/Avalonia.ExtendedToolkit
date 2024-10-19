using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// a decorator with an animation
    /// does not work.
    /// </summary>
    public class AnimationDecorator : Decorator
    {
        private double targetHeight = 0;
        private bool animating = false;

        /// <summary>
        /// Specify whether to apply opactiy animation.
        /// </summary>
        public bool OpacityAnimation
        {
            get { return (bool)GetValue(OpacityAnimationProperty); }
            set { SetValue(OpacityAnimationProperty, value); }
        }

        /// <summary>
        /// <see cref="OpacityAnimation"/>
        /// </summary>
        public static readonly StyledProperty<bool> OpacityAnimationProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(OpacityAnimation), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the decorator is expanded or collapsed.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsExpanded"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(IsExpanded), defaultValue: true);

        /// <summary>
        /// Specify whether to apply animation when IsExpanded is changed.
        /// </summary>
        public IAnimation HeightAnimation
        {
            get { return (IAnimation)GetValue(HeightAnimationProperty); }
            set { SetValue(HeightAnimationProperty, value); }
        }

        /// <summary>
        /// <see cref="HeightAnimation"/>
        /// </summary>
        public static readonly StyledProperty<IAnimation> HeightAnimationProperty =
            AvaloniaProperty.Register<AnimationDecorator, IAnimation>(nameof(HeightAnimation));

        /// <summary>
        /// Gets or sets the duration for the animation.
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// <see cref="Duration"/>
        /// </summary>
        public static readonly StyledProperty<TimeSpan> DurationProperty =
            AvaloniaProperty.Register<AnimationDecorator, TimeSpan>(
                nameof(Duration), defaultValue: TimeSpan.FromMilliseconds(250));

        /// <summary>
        /// Gets or sets the Opacity for animation.
        /// This dependency property can be used to modify the opacity of an outer control.
        /// </summary>
        public double AnimationOpacity
        {
            get { return (double)GetValue(AnimationOpacityProperty); }
            set { SetValue(AnimationOpacityProperty, value); }
        }

        /// <summary>
        /// <see cref="AnimationOpacity"/>
        /// </summary>
        public static readonly StyledProperty<double> AnimationOpacityProperty =
            AvaloniaProperty.Register<AnimationDecorator, double>(nameof(AnimationOpacity), defaultValue: (double)1.0);

        /// <summary>
        /// A helper value for the current state while in animation.
        /// </summary>
        public double HeightOffset
        {
            get { return (double)GetValue(HeightOffsetProperty); }
            set { SetValue(HeightOffsetProperty, value); }
        }

        /// <summary>
        /// <see cref="HeightOffset"/>
        /// </summary>
        public static readonly StyledProperty<double> HeightOffsetProperty =
            AvaloniaProperty.Register<AnimationDecorator, double>(nameof(HeightOffset), defaultValue: 0.0d);

        /// <summary>
        /// A helper value for the current state while in animation.
        /// </summary>
        public double YOffset
        {
            get { return (double)GetValue(YOffsetProperty); }
            set { SetValue(YOffsetProperty, value); }
        }

        /// <summary>
        /// <see cref="YOffset"/>
        /// </summary>
        public static readonly StyledProperty<double> YOffsetProperty =
            AvaloniaProperty.Register<AnimationDecorator, double>(nameof(YOffset), defaultValue: 0.0d);

        /// <summary>
        /// get/sets CanAnimate 
        /// </summary>
        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        /// <summary>
        /// <see cref="CanAnimate"/>
        /// </summary>
        public static readonly StyledProperty<bool> CanAnimateProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(CanAnimate), defaultValue: true);

        /// <summary>
        /// get/sets AnimateOnContentHeightChanged
        /// </summary>
        public bool AnimateOnContentHeightChanged
        {
            get { return (bool)GetValue(AnimateOnContentHeightChangedProperty); }
            set { SetValue(AnimateOnContentHeightChangedProperty, value); }
        }

        /// <summary>
        /// <see cref="AnimateOnContentHeightChanged"/>
        /// </summary>
        public static readonly StyledProperty<bool> AnimateOnContentHeightChangedProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(AnimateOnContentHeightChanged), defaultValue: true);

        /// <summary>
        /// add IsExpanded listener
        /// </summary>
        public AnimationDecorator()
        {
            ClipToBounds = true;
            IsExpandedProperty.Changed.AddClassHandler<AnimationDecorator>((o, e) => IsExpandedChanged(o, e));
        }

        private void IsExpandedChanged(AnimationDecorator expander, AvaloniaPropertyChangedEventArgs e)
        {
            bool expanded = (bool)e.NewValue;
            if (expander.CanAnimate)
            {
                Task.Factory.StartNew(() =>
                {
                    while (animating)
                        Thread.Sleep(10);
                }).ContinueWith(x =>
                {
                    expander.AnimateExpandedChanged(expanded);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                expander.UnanimatedExpandedChanged(expanded);
            }
        }

        private void UnanimatedExpandedChanged(bool expanded)
        {
            if (Child != null)
            {
                YOffset = expanded ? 0 : -Child.DesiredSize.Height;
            }
        }

        private async void AnimateExpandedChanged(bool expanded)
        {
            if (Child != null)
            {
                animating = true;

                if (YOffset > 0)
                    YOffset = 0;
                if (-YOffset > Child.DesiredSize.Height)
                    YOffset = -Child.DesiredSize.Height;
                Animation.Animation animation = HeightAnimation as Animation.Animation;
                if (animation == null)
                    animation = CreateAnimation();

                animation.FillMode = expanded ? FillMode.Forward : FillMode.Backward;

                double val = expanded ? 0 : -Child.DesiredSize.Height;

                KeyFrame keyFrame = null;

                if (expanded)
                {
                    keyFrame = new KeyFrame();
                    keyFrame.Cue = new Cue(0);
                    keyFrame.Setters.Add(new Setter(AnimationDecorator.YOffsetProperty, YOffset));
                    animation.Children.Add(keyFrame);
                    keyFrame = new KeyFrame();
                    keyFrame.Cue = new Cue(1);
                    keyFrame.Setters.Add(new Setter(AnimationDecorator.YOffsetProperty, val));
                    animation.Children.Add(keyFrame);
                }
                else
                {
                    keyFrame = new KeyFrame();
                    keyFrame.Cue = new Cue(1);
                    keyFrame.Setters.Add(new Setter(AnimationDecorator.YOffsetProperty, val));
                    animation.Children.Add(keyFrame);

                    keyFrame = new KeyFrame();
                    keyFrame.Cue = new Cue(0);
                    keyFrame.Setters.Add(new Setter(AnimationDecorator.YOffsetProperty, 1d));
                    animation.Children.Add(keyFrame);
                }

                //animation.From = null;
                //animation.To =
                //animation.Completed += new EventHandler(animation_Completed);

                //this.BeginAnimation(AnimationDecorator.YOffsetProperty, animation);

                if (OpacityAnimation)
                {
                    val = expanded ? 1 : 0;

                    keyFrame.Setters.Add(new Setter(AnimationDecorator.AnimationOpacityProperty, val));
                    animation.Children.Add(keyFrame);
                    //this.BeginAnimation(AnimationDecorator.AnimationOpacityProperty, animation);
                }
                //.edited, null arg removal
                await Task.WhenAll(new Task[] { animation.RunAsync(this) }).ContinueWith(x =>
                {
                    animating = false;
                });
            }
            else
            {
                YOffset = int.MinValue;
            }
        }

        private Animation.Animation CreateAnimation()
        {
            Animation.Animation animation = new Animation.Animation();

            //animation.DecelerationRatio = 0.8;
            animation.Duration = Duration;
            return animation;
        }

        /// <summary>
        /// Perform the animation when the child's height has changed.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private double AnimatedResize(Double h)
        {
            double delta = targetHeight - h;
            Animation.Animation animation = HeightAnimation as Animation.Animation;
            if (animation == null)
                animation = CreateAnimation();
            targetHeight = h;
            //animation.From = delta;
            //animation.To = 0;
            //this.BeginAnimation(AnimationDecorator.HeightOffsetProperty, animation);
            animation.FillMode = FillMode.Backward;
            KeyFrame keyFrame = new KeyFrame();
            keyFrame.Cue = new Cue(0);
            keyFrame.Setters.Add(new Setter(AnimationDecorator.HeightOffsetProperty, delta));
            animation.Children.Add(keyFrame);
            keyFrame = new KeyFrame();
            keyFrame.Cue = new Cue(1);
            keyFrame.Setters.Add(new Setter(AnimationDecorator.HeightOffsetProperty, 0));
            animation.Children.Add(keyFrame);
            //.edited, CLock removal
            animation.RunAsync(this);
            return delta;
        }

        /// <summary>
        /// measure the decorated control
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            if (Child == null)
                return new Size(0, 0);
            Size size;
            if (double.IsInfinity(constraint.Height))
            {
                Child.Measure(new Size(constraint.Width, double.PositiveInfinity));
                double childHeight = Child.DesiredSize.Height;
                double deltaHeight = 0;
                if (AnimateOnContentHeightChanged && IsInitialized && IsVisible && CanAnimate)
                {
                    if (targetHeight != childHeight)
                    {
                        deltaHeight = AnimatedResize(childHeight);
                        if (animating)
                        {
                            AnimateExpandedChanged(IsExpanded);
                        }
                    }
                }
                else
                    targetHeight = childHeight;

                double w = IsExpanded ? Child.DesiredSize.Width : 0;
                size = new Size(w, Math.Max(0d, childHeight + YOffset + HeightOffset + deltaHeight));
            }
            else
            {
                size = base.MeasureOverride(constraint);
            }
            if (Child != null)
            {
                (Child as Control).IsEnabled = size.Height > 0;
            }
            if (size.Height == 0)
                AnimationOpacity = 0;
            return size;
        }

        /// <summary>
        /// arranges the decorated control
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (Child == null)
                return arrangeSize;

            Child.Arrange(new Rect(0d, YOffset, arrangeSize.Width, Child.DesiredSize.Height));
            Double h = Math.Max(0, Child.DesiredSize.Height + YOffset);
            return new Size(arrangeSize.Width, h);
        }
    }
}
