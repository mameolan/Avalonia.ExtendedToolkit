using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ToggleSwitchButton : ToggleButton
    {
        private const string PART_OffSwitch = "PART_OffSwitch";
        private const string PART_BackgroundTranslate = "PART_BackgroundTranslate";
        private const string PART_DraggingThumb = "PART_DraggingThumb";
        private const string PART_SwitchTrack = "PART_SwitchTrack";
        private const string PART_ThumbIndicator = "PART_ThumbIndicator";
        private const string PART_ThumbTranslate = "PART_ThumbTranslate";

        private TranslateTransform _BackgroundTranslate;
        private Thumb _DraggingThumb;
        private Grid _SwitchTrack;
        private IControl _ThumbIndicator;
        private TranslateTransform _ThumbTranslate;

        public IStyle Style
        {
            get { return (IStyle)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        public static readonly StyledProperty<IStyle> StyleProperty =
            AvaloniaProperty.Register<ToggleSwitchButton, IStyle>(nameof(Style));

        public IBrush OnSwitchBrush
        {
            get { return (IBrush)GetValue(OnSwitchBrushProperty); }
            set { SetValue(OnSwitchBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> OnSwitchBrushProperty =
            AvaloniaProperty.Register<ToggleSwitchButton, IBrush>(nameof(OnSwitchBrush));

        public IBrush OffSwitchBrush
        {
            get { return (IBrush)GetValue(OffSwitchBrushProperty); }
            set { SetValue(OffSwitchBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> OffSwitchBrushProperty =
            AvaloniaProperty.Register<ToggleSwitchButton, IBrush>(nameof(OffSwitchBrush));

        public IBrush ThumbIndicatorBrush
        {
            get { return (IBrush)GetValue(ThumbIndicatorBrushProperty); }
            set { SetValue(ThumbIndicatorBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> ThumbIndicatorBrushProperty =
            AvaloniaProperty.Register<ToggleSwitchButton, IBrush>(nameof(ThumbIndicatorBrush));

        public IBrush ThumbIndicatorDisabledBrush
        {
            get { return (IBrush)GetValue(ThumbIndicatorDisabledBrushProperty); }
            set { SetValue(ThumbIndicatorDisabledBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> ThumbIndicatorDisabledBrushProperty =
            AvaloniaProperty.Register<ToggleSwitchButton, IBrush>(nameof(ThumbIndicatorDisabledBrush));

        public double ThumbIndicatorWidth
        {
            get { return (double)GetValue(ThumbIndicatorWidthProperty); }
            set { SetValue(ThumbIndicatorWidthProperty, value); }
        }

        public static readonly StyledProperty<double> ThumbIndicatorWidthProperty =
            AvaloniaProperty.Register<ToggleSwitchButton, double>(nameof(ThumbIndicatorWidth), defaultValue: 13d);

        static ToggleSwitchButton()
        {
            StyleProperty.Changed.AddClassHandler<ToggleSwitchButton>((o, e) => StyleChanged(o, e));
        }

        public ToggleSwitchButton()
        {
            IsCheckedProperty.Changed.AddClassHandler<ToggleSwitchButton>((o, e) => IsCheckedChanged(o, e));
        }

        private static void StyleChanged(ToggleSwitchButton o, AvaloniaPropertyChangedEventArgs e)
        {
            IStyle style = e.NewValue as IStyle;
            if (style != null)
            {
                o.Styles.Add(style);
            }
        }

        private void IsCheckedChanged(ToggleSwitchButton o, AvaloniaPropertyChangedEventArgs e)
        {
            UpdateThumb();
        }

        private Animation.Animation _thumbAnimation;

        private void UpdateThumb()
        {
            if (_ThumbTranslate != null && _SwitchTrack != null && _ThumbIndicator != null)
            {
                double destination = IsChecked.GetValueOrDefault() ? Width - (_SwitchTrack.Margin.Left + _SwitchTrack.Margin.Right + _ThumbIndicator.Width + _ThumbIndicator.Margin.Left + _ThumbIndicator.Margin.Right) : 0;

                _thumbAnimation = new Animation.Animation();
                //_thumbAnimation.To = destination;
                _thumbAnimation.Duration = TimeSpan.FromMilliseconds(500);
                //_thumbAnimation.Easing = new ExponentialEase() { Exponent = 9 };
                _thumbAnimation.FillMode = Animation.FillMode.Backward;
                _thumbAnimation.PropertyChanged += (o, e) =>
                {
                    //if(e.Property.Name== nameof(Animation.Animation.)
                };
                //_ThumbTranslate.

                //AnimationTimeline currentAnimation = _thumbAnimation;
                //_thumbAnimation.Completed += (sender, e) => {
                //    if (_thumbAnimation != null && currentAnimation == _thumbAnimation)
                //    {
                //        _ThumbTranslate.X = destination;
                //        _thumbAnimation = null;
                //    }
                //};
                //_ThumbTranslate.BeginAnimation(TranslateTransform.XProperty, _thumbAnimation);
            }
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            Rectangle rectangle = e.NameScope.Find<Rectangle>(PART_OffSwitch);

            _BackgroundTranslate = rectangle.RenderTransform as TranslateTransform; //e.NameScope.Find<TranslateTransform>(PART_BackgroundTranslate);
            _DraggingThumb = e.NameScope.Find<Thumb>(PART_DraggingThumb);
            _SwitchTrack = e.NameScope.Find<Grid>(PART_SwitchTrack);
            _ThumbIndicator = e.NameScope.Find<IControl>(PART_ThumbIndicator);
            _ThumbTranslate = _ThumbIndicator.RenderTransform as TranslateTransform; //e.NameScope.Find<TranslateTransformExt>(PART_ThumbTranslate);

            if (_ThumbIndicator != null && _ThumbTranslate != null && _BackgroundTranslate != null)
            {
                Binding translationBinding;
                translationBinding = new Binding("X");
                translationBinding.Source = _ThumbTranslate;
                _BackgroundTranslate.Bind(TranslateTransform.XProperty, translationBinding);
            }
            if (_DraggingThumb != null && _ThumbIndicator != null && _ThumbTranslate != null)
            {
                _DraggingThumb.DragStarted -= _DraggingThumb_DragStarted;
                _DraggingThumb.DragDelta -= _DraggingThumb_DragDelta;
                _DraggingThumb.DragCompleted -= _DraggingThumb_DragCompleted;
                _DraggingThumb.DragStarted += _DraggingThumb_DragStarted;
                _DraggingThumb.DragDelta += _DraggingThumb_DragDelta;
                _DraggingThumb.DragCompleted += _DraggingThumb_DragCompleted;
                if (_SwitchTrack != null)
                {
                    _SwitchTrack.PropertyChanged += SwitchTrack_PropertyChanged;
                }
            }

            UpdateThumb();
        }

        private void SwitchTrack_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(_SwitchTrack.Width) || e.Property.Name == nameof(_SwitchTrack.Height))
            {
                if (_ThumbTranslate != null && _SwitchTrack != null && _ThumbIndicator != null)
                {
                    double destination = IsChecked.GetValueOrDefault() ? Width -
                        (_SwitchTrack.Margin.Left + _SwitchTrack.Margin.Right +
                        _ThumbIndicator.Width + _ThumbIndicator.Margin.Left + _ThumbIndicator.Margin.Right) : 0;
                    _ThumbTranslate.X = destination;
                }
            }
        }

        private double? _lastDragPosition;
        private bool _isDragging;

        private void _DraggingThumb_DragCompleted(object sender, VectorEventArgs e)
        {
            _lastDragPosition = null;
            if (!_isDragging)
            {
                OnClick();
            }
            else if (_ThumbTranslate != null && _SwitchTrack != null)
            {
                if (!IsChecked.GetValueOrDefault() && _ThumbTranslate.X + 6.5 >= _SwitchTrack.Width / 2)
                {
                    OnClick();
                }
                else if (IsChecked.GetValueOrDefault() && _ThumbTranslate.X + 6.5 <= _SwitchTrack.Width / 2)
                {
                    OnClick();
                }
                UpdateThumb();
            }
            _isDragging = false;
        }

        private void _DraggingThumb_DragDelta(object sender, VectorEventArgs e)
        {
            if (_DraggingThumb.IsPointerOver == false)
            {
                _lastDragPosition = null;
                _isDragging = false;

                return;
            }

            if (_lastDragPosition.HasValue)
            {
                if (Math.Abs(e.Vector.X) > 3)
                    _isDragging = true;
                if (_SwitchTrack != null && _ThumbIndicator != null)
                {
                    double lastDragPosition = _lastDragPosition.Value;
                    _ThumbTranslate.X = Math.Min(Width - (_SwitchTrack.Margin.Left + _SwitchTrack.Margin.Right + _ThumbIndicator.Width + _ThumbIndicator.Margin.Left + _ThumbIndicator.Margin.Right), Math.Max(0, lastDragPosition + e.Vector.X));
                }
            }
        }

        private void _DraggingThumb_DragStarted(object sender, VectorEventArgs e)
        {
            if (_DraggingThumb.IsPointerOver == false)
            {
                _lastDragPosition = null;
                _isDragging = false;
                return;
            }

            if (_ThumbTranslate != null)
            {
                //_ThumbTranslate.BeginAnimation(TranslateTransform.XProperty, null);
                double destination = IsChecked.GetValueOrDefault() ? Width - (_SwitchTrack.Margin.Left + _SwitchTrack.Margin.Right + _ThumbIndicator.Width + _ThumbIndicator.Margin.Left + _ThumbIndicator.Margin.Right) : 0;
                _ThumbTranslate.X = destination;
                _thumbAnimation = null;
            }
            _lastDragPosition = _ThumbTranslate.X;
            _isDragging = false;
        }
    }
}