using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ProgressRing : TemplatedControl
    {
        private const string PseudoClass_Large = ":large";
        private const string PseudoClass_Small = ":small";
        private const string PseudoClass_Active = ":active";
        private const string PseudoClass_Inactive = ":inactive";



        private List<Action> _deferredActions = new List<Action>();

        public double BindableWidth
        {
            get { return (double)GetValue(BindableWidthProperty); }
            set { SetValue(BindableWidthProperty, value); }
        }


        public static readonly AvaloniaProperty<double> BindableWidthProperty =
            AvaloniaProperty.Register<ProgressRing, double>(nameof(BindableWidth));



        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }


        public static readonly AvaloniaProperty<bool> IsActiveProperty =
            AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsActive), defaultValue: true);



        public bool IsLarge
        {
            get { return (bool)GetValue(IsLargeProperty); }
            set { SetValue(IsLargeProperty, value); }
        }


        public static readonly AvaloniaProperty<bool> IsLargeProperty =
            AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsLarge), defaultValue: true);



        public double MaxSideLength
        {
            get { return (double)GetValue(MaxSideLengthProperty); }
            set { SetValue(MaxSideLengthProperty, value); }
        }


        public static readonly AvaloniaProperty<double> MaxSideLengthProperty =
            AvaloniaProperty.Register<ProgressRing, double>(nameof(MaxSideLength), defaultValue: default(double));




        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }


        public static readonly AvaloniaProperty<double> EllipseDiameterProperty =
            AvaloniaProperty.Register<ProgressRing, double>(nameof(EllipseDiameter));



        public Thickness EllipseOffset
        {
            get { return (Thickness)GetValue(EllipseOffsetProperty); }
            set { SetValue(EllipseOffsetProperty, value); }
        }


        public static readonly AvaloniaProperty<Thickness> EllipseOffsetProperty =
            AvaloniaProperty.Register<ProgressRing, Thickness>(nameof(EllipseOffset));



        public double EllipseDiameterScale
        {
            get { return (double)GetValue(EllipseDiameterScaleProperty); }
            set { SetValue(EllipseDiameterScaleProperty, value); }
        }


        public static readonly AvaloniaProperty<double> EllipseDiameterScaleProperty =
            AvaloniaProperty.Register<ProgressRing, double>(nameof(EllipseDiameterScale), defaultValue: 1D);





        static ProgressRing()
        {
            BindableWidthProperty.Changed.AddClassHandler<ProgressRing>((o, e) => BindableWidthCallback(o, e));
            IsActiveProperty.Changed.AddClassHandler<ProgressRing>((o, e) => IsActiveChanged(o, e));
            IsVisibleProperty.Changed.AddClassHandler<ProgressRing>((o, e) => IsVisibleChanged(o, e));
            WidthProperty.Changed.AddClassHandler<ProgressRing>((o, e) => OnSizeChanged(o, e));
            HeightProperty.Changed.AddClassHandler<ProgressRing>((o, e) => OnSizeChanged(o, e));
            IsLargeProperty.Changed.AddClassHandler<ProgressRing>((o, e) => IsLargeChangedCallback(o, e));

            
        }

        private static void IsLargeChangedCallback(ProgressRing ring, AvaloniaPropertyChangedEventArgs e)
        {
            ring.UpdateLargeState();
        }

        private static void OnSizeChanged(ProgressRing ring, AvaloniaPropertyChangedEventArgs e)
        {
            ring.SetValue(BindableWidthProperty, ring.Width);
        }

        private static void IsVisibleChanged(ProgressRing ring, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                if ((bool)e.NewValue == false)
                {
                    ring.SetValue(ProgressRing.IsActiveProperty, false);
                }
                else
                {
                    ring.SetValue(ProgressRing.IsActiveProperty, true);
                }

            }
        }

        private static void IsActiveChanged(ProgressRing ring, AvaloniaPropertyChangedEventArgs e)
        {
            //if(e.NewValue is bool)
            //    ring.IsVisible = (bool)e.NewValue;
            ring.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            this.PseudoClasses.Remove(PseudoClass_Inactive);
            this.PseudoClasses.Remove(PseudoClass_Active);
            Action action;

            this.PseudoClasses.Set(PseudoClass_Active, IsActive);
            this.PseudoClasses.Set(PseudoClass_Inactive, IsActive == false);


            //if (IsActive)
            //{  /*action = () =>*/
            //    this.PseudoClasses.Add(PseudoClass_Active);

            //}
            //else
            ///*action = () =>*/
            //{
            //    this.PseudoClasses.Add(PseudoClass_Inactive);

            //}

            //if (_deferredActions != null)
            //    _deferredActions.Add(action);
            //else
            //    action();
        }


        private static void BindableWidthCallback(ProgressRing ring, AvaloniaPropertyChangedEventArgs e)
        {
            //var action = new Action(() =>
            //{

                ring.SetEllipseDiameter((double)e.NewValue);
                ring.SetEllipseOffset((double)e.NewValue);
                ring.SetMaxSideLength((double)e.NewValue);
            //});

            //if (ring._deferredActions != null)
            //    ring._deferredActions.Add(action);
            //else
            //    action();

        }

        private void SetMaxSideLength(double width)
        {
            SetValue(MaxSideLengthProperty, width <= 20 ? 20 : width);
        }

        private void SetEllipseDiameter(double width)
        {
            SetValue(EllipseDiameterProperty, (width / 8) * EllipseDiameterScale);
        }

        private void SetEllipseOffset(double width)
        {
            SetValue(EllipseOffsetProperty, new Thickness(0, width / 2, 0, 0));
        }

        private void UpdateLargeState()
        {
            this.PseudoClasses.Remove(PseudoClass_Large);
            this.PseudoClasses.Remove(PseudoClass_Small);

            Action action;


            this.PseudoClasses.Set(PseudoClass_Large, IsLarge);
            this.PseudoClasses.Set(PseudoClass_Small, IsLarge==false);
            //if (IsLarge)
            //    /*action = () =>*/ this.PseudoClasses.Add(PseudoClass_Large);
            //else
            //    /*action = () =>*/ this.PseudoClasses.Add(PseudoClass_Small);

            //if (_deferredActions != null)
            //    _deferredActions.Add(action);
            //else
            //    action();
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            //make sure the states get updated
            UpdateLargeState();
            UpdateActiveState();
            if (_deferredActions != null)
            {
                foreach (var action in _deferredActions)
                {
                    action();
                }
            }
            _deferredActions = null;
            
        }


    }
}
