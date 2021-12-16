using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace Avalonia.ExtendedToolkit.Controls
{
    //base on http://timokorinth.de/creating-range-slider-wpf-silverlight/

    /// <summary>
    /// contol which has two two slider for managing an upper and lower value
    /// </summary>
    public class RangeSlider : TemplatedControl
    {
        private static readonly string PART_ProgressBorder = "PART_ProgressBorder";
        private static readonly string PART_LowerSlider = "PART_LowerSlider";
        private static readonly string PART_UpperSlider = "PART_UpperSlider";

        private Border _progressBorder;
        private Slider _lowerSlider;
        private Slider _upperSlider;

        /// <summary>
        /// Gets or sets Minimum.
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Defines the Minimum property.
        /// </summary>
        public static readonly StyledProperty<double> MinimumProperty =
        AvaloniaProperty.Register<RangeSlider, double>(nameof(Minimum), 0d);

        /// <summary>
        /// Gets or sets LowerValue.
        /// </summary>
        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        /// <summary>
        /// Defines the LowerValue property.
        /// </summary>
        public static readonly StyledProperty<double> LowerValueProperty =
        AvaloniaProperty.Register<RangeSlider, double>(nameof(LowerValue), defaultValue: 10d);

        /// <summary>
        /// Gets or sets UpperValue.
        /// </summary>
        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        /// <summary>
        /// Defines the UpperValue property.
        /// </summary>
        public static readonly StyledProperty<double> UpperValueProperty =
        AvaloniaProperty.Register<RangeSlider, double>(nameof(UpperValue), defaultValue: 90d);

        /// <summary>
        /// Gets or sets Maximum.
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Defines the Maximum property.
        /// </summary>
        public static readonly StyledProperty<double> MaximumProperty =
        AvaloniaProperty.Register<RangeSlider, double>(nameof(Maximum), defaultValue: 100d);

        /// <summary>
        /// Gets or sets DisableLowerValue.
        /// </summary>
        public bool DisableLowerValue
        {
            get { return (bool)GetValue(DisableLowerValueProperty); }
            set { SetValue(DisableLowerValueProperty, value); }
        }

        /// <summary>
        /// Defines the DisableLowerValue property.
        /// </summary>
        public static readonly StyledProperty<bool> DisableLowerValueProperty =
        AvaloniaProperty.Register<RangeSlider, bool>(nameof(DisableLowerValue));

        /// <summary>
        /// Gets or sets Orientation.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Defines the Orientation property.
        /// </summary>
        public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<RangeSlider, Orientation>(nameof(Orientation), defaultValue: Orientation.Horizontal);

        /// <summary>
        /// registers changed handlers
        /// </summary>
        public RangeSlider()
        {
            MinimumProperty.Changed.AddClassHandler<RangeSlider>((o, e) => ValueChanged(o, e));
            LowerValueProperty.Changed.AddClassHandler<RangeSlider>((o, e) => ValueChanged(o, e));
            UpperValueProperty.Changed.AddClassHandler<RangeSlider>((o, e) => ValueChanged(o, e));
            MaximumProperty.Changed.AddClassHandler<RangeSlider>((o, e) => ValueChanged(o, e));
            DisableLowerValueProperty.Changed.AddClassHandler<RangeSlider>((o, e) => DisabledLowerValueChanged(o, e));
            OrientationProperty.Changed.AddClassHandler<RangeSlider>((o, e) => OrientationChanged(o, e));
            this.LayoutUpdated += new EventHandler(RangeSlider_LayoutUpdated);
        }

        private void OrientationChanged(RangeSlider rangeSlider, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is Orientation orientation)
            {
                rangeSlider.PseudoClasses.Set(":vertical", orientation == Orientation.Vertical);
                rangeSlider.PseudoClasses.Set(":horizontal", orientation == Orientation.Horizontal);

                // SetProgressBorder();
                // SetLowerValueVisibility();
            }
        }

        private void RangeSlider_LayoutUpdated(object sender, EventArgs e)
        {
            if (_progressBorder == null || _lowerSlider == null || _upperSlider == null)
            {
                return;
            }

            SetProgressBorder();
            SetLowerValueVisibility();
        }

        private void DisabledLowerValueChanged(RangeSlider rangeSlider, object e)
        {
            rangeSlider.SetLowerValueVisibility();
        }

        private void ValueChanged(RangeSlider rangeSlider, AvaloniaPropertyChangedEventArgs e)
        {
            if (_upperSlider == null || _lowerSlider == null)
            {
                return;
            }

            if (e.Property == RangeSlider.LowerValueProperty)
            {
                rangeSlider._upperSlider.Value = Math.Max(rangeSlider._upperSlider.Value, rangeSlider._lowerSlider.Value);
            }
            else if (e.Property == RangeSlider.UpperValueProperty)
            {
                rangeSlider._lowerSlider.Value = Math.Min(rangeSlider._upperSlider.Value, rangeSlider._lowerSlider.Value);
            }
            rangeSlider.SetProgressBorder();
        }

        private void SetLowerValueVisibility()
        {
            if (DisableLowerValue)
            {
                _lowerSlider.IsVisible = false;
            }
            else
            {
                _lowerSlider.IsVisible = true;
            }
        }

        private void SetProgressBorder()
        {
            double lowerPoint = 0;
            double upperPoint = 0;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    lowerPoint = (this.DesiredSize.Width * (LowerValue - Minimum)) / (Maximum - Minimum);
                    upperPoint = (this.DesiredSize.Width * (UpperValue - Minimum)) / (Maximum - Minimum);
                    upperPoint = this.DesiredSize.Width - upperPoint;
                    _progressBorder.Margin = new Thickness(lowerPoint, 0, upperPoint, 0);
                    break;

                case Orientation.Vertical:
                    lowerPoint = (this.DesiredSize.Height * (LowerValue - Minimum)) / (Maximum - Minimum);
                    upperPoint = (this.DesiredSize.Height * (UpperValue - Minimum)) / (Maximum - Minimum);
                    upperPoint = this.DesiredSize.Height - upperPoint;
                    _progressBorder.Margin = new Thickness(0, upperPoint, 0, lowerPoint);
                    break;
            }
        }

        /// <summary>
        /// resolves the template and sets the progressborder
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _progressBorder = e.NameScope.Find<Border>(PART_ProgressBorder);
            _lowerSlider = e.NameScope.Find<Slider>(PART_LowerSlider);
            _upperSlider = e.NameScope.Find<Slider>(PART_UpperSlider);
            SetProgressBorder();
            SetLowerValueVisibility();
        }
    }
}
