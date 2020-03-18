using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Styling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class AnimationDecorator : Decorator
    {
        private double targetHeight = 0;
        private bool animating = false;

        public bool OpacityAnimation
        {
            get { return (bool)GetValue(OpacityAnimationProperty); }
            set { SetValue(OpacityAnimationProperty, value); }
        }

        public static readonly StyledProperty<bool> OpacityAnimationProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(OpacityAnimation), defaultValue: true);

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(IsExpanded), defaultValue: true);

        public IAnimation HeightAnimation
        {
            get { return (IAnimation)GetValue(HeightAnimationProperty); }
            set { SetValue(HeightAnimationProperty, value); }
        }

        public static readonly StyledProperty<IAnimation> HeightAnimationProperty =
            AvaloniaProperty.Register<AnimationDecorator, IAnimation>(nameof(HeightAnimation));

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly StyledProperty<TimeSpan> DurationProperty =
            AvaloniaProperty.Register<AnimationDecorator, TimeSpan>(
                nameof(Duration), defaultValue: TimeSpan.FromMilliseconds(250));

        public double AnimationOpacity
        {
            get { return (double)GetValue(AnimationOpacityProperty); }
            set { SetValue(AnimationOpacityProperty, value); }
        }

        public static readonly StyledProperty<double> AnimationOpacityProperty =
            AvaloniaProperty.Register<AnimationDecorator, double>(nameof(AnimationOpacity), defaultValue: (double)1.0);

        public double HeightOffset
        {
            get { return (double)GetValue(HeightOffsetProperty); }
            set { SetValue(HeightOffsetProperty, value); }
        }

        public static readonly StyledProperty<double> HeightOffsetProperty =
            AvaloniaProperty.Register<AnimationDecorator, double>(nameof(HeightOffset), defaultValue: 0.0d);

        public double YOffset
        {
            get { return (double)GetValue(YOffsetProperty); }
            set { SetValue(YOffsetProperty, value); }
        }

        public static readonly StyledProperty<double> YOffsetProperty =
            AvaloniaProperty.Register<AnimationDecorator, double>(nameof(YOffset), defaultValue: 0.0d);

        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        public static readonly StyledProperty<bool> CanAnimateProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(CanAnimate), defaultValue: true);

        public bool AnimateOnContentHeightChanged
        {
            get { return (bool)GetValue(AnimateOnContentHeightChangedProperty); }
            set { SetValue(AnimateOnContentHeightChangedProperty, value); }
        }

        public static readonly StyledProperty<bool> AnimateOnContentHeightChangedProperty =
            AvaloniaProperty.Register<AnimationDecorator, bool>(nameof(AnimateOnContentHeightChanged), defaultValue: true);

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

                if (YOffset > 0) YOffset = 0;
                if (-YOffset > Child.DesiredSize.Height) YOffset = -Child.DesiredSize.Height;
                Animation.Animation animation = HeightAnimation as Animation.Animation;
                if (animation == null) animation = CreateAnimation();

                animation.FillMode = expanded? FillMode.Forward: FillMode.Backward;

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

                await Task.WhenAll(new Task[] { animation.RunAsync(this) }).ContinueWith(x=>
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
            if (animation == null) animation = CreateAnimation();
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

            animation.RunAsync(this);
            return delta;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (Child == null) return new Size(0, 0);
            Size size;
            if (double.IsInfinity(constraint.Height))
            {
                Child.Measure(new Size(constraint.Width, Double.PositiveInfinity));
                Double childHeight = Child.DesiredSize.Height;
                Double deltaHeight = 0;
                if (this.AnimateOnContentHeightChanged && this.IsInitialized && IsVisible && CanAnimate)
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
                else targetHeight = childHeight;

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
            if (size.Height == 0) this.AnimationOpacity = 0;
            return size;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (Child == null) return arrangeSize;

            Child.Arrange(new Rect(0d, YOffset, arrangeSize.Width, Child.DesiredSize.Height));
            Double h = Math.Max(0, Child.DesiredSize.Height + YOffset);
            return new Size(arrangeSize.Width, h);
        }
    }
}