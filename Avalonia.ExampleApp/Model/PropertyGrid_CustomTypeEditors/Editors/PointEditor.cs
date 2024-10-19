using System;
using Avalonia.Controlz.Controls;
using Avalonia.Controlz.EventArgs;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;

namespace Avalonia.ExampleApp.Model
{
    public class PointEditor : SliderEx
    {
        private bool _isUpdating;
        

        public Point EditValue
        {
            get { return (Point)GetValue(EditValueProperty); }
            set { SetValue(EditValueProperty, value); }
        }

        public static readonly StyledProperty<Point> EditValueProperty =
            AvaloniaProperty.Register<PointEditor, Point>(nameof(EditValue)
                , defaultValue: new Point()
                //, validate: (o, e) => { return OnEditValuePropertyCoerceValue(o, e); }
                , defaultBindingMode: Data.BindingMode.OneWay
                );

        private static Point OnEditValuePropertyCoerceValue(PointEditor o, Point e)
        {
            return e;
        }

        public PointDisplayMember DisplayMember
        {
            get { return (PointDisplayMember)GetValue(DisplayMemberProperty); }
            set { SetValue(DisplayMemberProperty, value); }
        }

        public static readonly StyledProperty<PointDisplayMember> DisplayMemberProperty =
            AvaloniaProperty.Register<PointEditor, PointDisplayMember>(nameof(DisplayMember)
                , defaultValue: PointDisplayMember.None);

        public PointEditor()
        {
            EditValueProperty.Changed.AddClassHandler<PointEditor>((o, e) => OnEditValuePropertyChanged(o, e));
            IsDirectionReversed = false;
            IsMoveToPointEnabled = true;
            IsSnapToTickEnabled = false;

            //ValueProperty.Changed.AddClassHandler<PointEditor>((o, e) => OnSliderValueChanged(o, e));

            ValueChanged += PointEditor_ValueChanged;

        }

        private void PointEditor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            switch (DisplayMember)
            {
                case PointDisplayMember.X:
                    EditValue = new Point(Math.Round(Value, AutoToolTipPrecision), EditValue.Y);
                    break;

                case PointDisplayMember.Y:
                    EditValue = new Point(EditValue.X, Math.Round(Value, AutoToolTipPrecision));
                    break;

                default:
                    break;
            }

            _isUpdating = false;
            //.obsolete, EditValueProperty is styled now
            //RaisePropertyChanged(EditValueProperty, new Point(), EditValue);
        }

        private void OnSliderValueChanged(PointEditor o, AvaloniaPropertyChangedEventArgs e)
        {
            if (_isUpdating)
                return;

            switch (DisplayMember)
            {
                case PointDisplayMember.X:
                    EditValue = new Point(Math.Round(Value, AutoToolTipPrecision), EditValue.Y);
                    break;

                case PointDisplayMember.Y:
                    EditValue = new Point(EditValue.X, Math.Round(Value, AutoToolTipPrecision));
                    break;

                default:
                    break;
            }


        }

        private void OnEditValuePropertyChanged(PointEditor editor, object e)
        {
            editor.UpdateValue();
        }

        

        private void UpdateValue()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;
            
            switch (DisplayMember)
            {
                case PointDisplayMember.X:
                    Value = EditValue.X;
                    //EditValue 
                    break;

                case PointDisplayMember.Y:
                    Value = EditValue.Y;
                    //EditValue = new Point(EditValue.X, Value);
                    break;

                default:
                    Value = 0;
                    
                    
                    break;
            }

            _isUpdating = false;
        }
    }
}
