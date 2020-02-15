using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// radiobutton with special properties
    /// </summary>
    public class MetroRadioButton: RadioButton
    {
        public IBrush FocusBorderBrush
        {
            get { return (IBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroRadioButton, IBrush>(nameof(FocusBorderBrush));

        public IBrush MouseOverBorderBrush
        {
            get { return (IBrush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> MouseOverBorderBrushProperty =
            AvaloniaProperty.Register<MetroRadioButton, IBrush>(nameof(MouseOverBorderBrush));

        public FlowDirection ContentDirection
        {
            get { return (FlowDirection)GetValue(ContentDirectionProperty); }
            set { SetValue(ContentDirectionProperty, value); }
        }

        public static readonly StyledProperty<FlowDirection> ContentDirectionProperty =
            AvaloniaProperty.Register<MetroRadioButton, FlowDirection>(nameof(ContentDirection));

        private Ellipse _checkedEllipse;

        public MetroRadioButton()
        {
            IsCheckedProperty.Changed.AddClassHandler<MetroRadioButton>((o, e) => OnIsCheckChanged(o, e));
        }

        private void OnIsCheckChanged(MetroRadioButton metroCheckBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (metroCheckBox._checkedEllipse != null)
            {
                metroCheckBox._checkedEllipse.Opacity = e.NewValue != null && (bool)e.NewValue ? 1 : 0;

                if (e.NewValue == null && metroCheckBox.IsThreeState)
                {
                    metroCheckBox._checkedEllipse.Opacity = 0;
                }
            }
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            _checkedEllipse = e.NameScope.Find<Ellipse>("Checked1");

            //set init value
            bool? isChecked = IsChecked;
            if (isChecked.HasValue)
            {
                IsChecked = null;
            }
            else
            {
                IsChecked = false;
            }

            IsChecked = isChecked;

            base.OnTemplateApplied(e);
        }
    }
}