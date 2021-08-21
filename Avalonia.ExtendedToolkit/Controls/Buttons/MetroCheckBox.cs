using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// checkbox with special properties
    /// </summary>
    public class MetroCheckBox : CheckBox
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(MetroCheckBox);

        /// <summary>
        /// get/set FocusBorderBrush
        /// </summary>
        public IBrush FocusBorderBrush
        {
            get { return (IBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="FocusBorderBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroCheckBox, IBrush>(nameof(FocusBorderBrush));

        /// <summary>
        /// get/set MouseOverBorderBrush
        /// </summary>
        public IBrush MouseOverBorderBrush
        {
            get { return (IBrush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="MouseOverBorderBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> MouseOverBorderBrushProperty =
            AvaloniaProperty.Register<MetroCheckBox, IBrush>(nameof(MouseOverBorderBrush));

        /// <summary>
        /// get/set IsIndeterminate
        /// </summary>
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        /// <summary>
        /// <see cref="IsIndeterminate"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsIndeterminateProperty =
            AvaloniaProperty.Register<MetroCheckBox, bool>(nameof(IsIndeterminate));

        /// <summary>
        /// get/set FlowDirection
        /// </summary>
        public FlowDirection FlowDirection
        {
            get { return (FlowDirection)GetValue(FlowDirectionProperty); }
            set { SetValue(FlowDirectionProperty, value); }
        }

        /// <summary>
        /// <see cref="FlowDirection"/>
        /// </summary>
        public static readonly StyledProperty<FlowDirection> FlowDirectionProperty =
            AvaloniaProperty.Register<MetroCheckBox, FlowDirection>(nameof(FlowDirection));

        /// <summary>
        /// ContentDirection
        /// </summary>
        public FlowDirection ContentDirection
        {
            get { return (FlowDirection)GetValue(ContentDirectionProperty); }
            set { SetValue(ContentDirectionProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentDirection"/>
        /// </summary>
        public static readonly StyledProperty<FlowDirection> ContentDirectionProperty =
            AvaloniaProperty.Register<MetroCheckBox, FlowDirection>(nameof(ContentDirection));

        private Path _checkBoxPath;
        private Rectangle _indeterminateCheck;

        /// <summary>
        /// adds listener for
        /// IsChecked, IsIndeterminate
        /// </summary>
        static MetroCheckBox()
        {
            IsCheckedProperty.Changed.AddClassHandler<MetroCheckBox>((o, e) => OnIsCheckChanged(o, e));
            IsIndeterminateProperty.Changed.AddClassHandler<MetroCheckBox>((o, e) => OnIsIndeterminateChanged(o, e));
        }

        private static void OnIsIndeterminateChanged(MetroCheckBox metroCheckBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (metroCheckBox._indeterminateCheck != null)
            {
                metroCheckBox._indeterminateCheck.Opacity = e.NewValue != null && (bool)e.NewValue ? 1 : 0;
            }
        }

        private static void OnIsCheckChanged(MetroCheckBox metroCheckBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (metroCheckBox._checkBoxPath != null)
            {
                metroCheckBox._checkBoxPath.Opacity = e.NewValue != null && (bool)e.NewValue ? 1 : 0;

                if (e.NewValue == null&& metroCheckBox.IsThreeState)
                {
                    metroCheckBox._checkBoxPath.Opacity = 0;
                }
            }

            if (metroCheckBox.IsThreeState)
            {
                metroCheckBox.IsIndeterminate = e.NewValue == null;
            }
        }

        /// <summary>
        /// gets _checkBoxPath, _indeterminateCheck
        /// initilaize default value
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            _checkBoxPath = e.NameScope.Find<Path>("checkBox");
            _indeterminateCheck = e.NameScope.Find<Rectangle>("IndeterminateCheck");

            //set init value
            bool? isChecked = IsChecked;
            if(isChecked.HasValue)
            {
                IsChecked = null;
            }
            else
            {
                IsChecked = false;
            }

            IsChecked = isChecked;

            base.OnApplyTemplate(e);
        }
    }
}
