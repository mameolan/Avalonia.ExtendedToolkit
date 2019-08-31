using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroCheckBox : CheckBox
    {
        public SolidColorBrush FocusBorderBrush
        {
            get { return (SolidColorBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        public static readonly AvaloniaProperty FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroCheckBox, SolidColorBrush>(nameof(FocusBorderBrush));




        public SolidColorBrush MouseOverBorderBrush
        {
            get { return (SolidColorBrush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value);  }
        }


        public static readonly AvaloniaProperty MouseOverBorderBrushProperty =
            AvaloniaProperty.Register<MetroCheckBox, SolidColorBrush>(nameof(MouseOverBorderBrush));

        

        public bool IsIndeterminate
        {

            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }


        public static readonly AvaloniaProperty IsIndeterminateProperty =
            AvaloniaProperty.Register<MetroCheckBox, bool>(nameof(IsIndeterminate));



        public FlowDirection FlowDirection
        {
            get { return (FlowDirection)GetValue(FlowDirectionProperty); }
            set { SetValue(FlowDirectionProperty, value); }
        }


        public static readonly AvaloniaProperty FlowDirectionProperty =
            AvaloniaProperty.Register<MetroCheckBox, FlowDirection>(nameof(FlowDirection));




        public MetroCheckBox()
        {
            IsCheckedProperty.Changed.AddClassHandler<MetroCheckBox>((o, e) => OnIsCheckChanged(o, e));
        }

        private void OnIsCheckChanged(MetroCheckBox o, AvaloniaPropertyChangedEventArgs e)
        {
            //IsIndeterminate = e.NewValue == null;
        }
    }
}
