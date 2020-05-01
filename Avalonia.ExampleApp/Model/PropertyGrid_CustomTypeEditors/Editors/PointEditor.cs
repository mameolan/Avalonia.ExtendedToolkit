using System;
using Avalonia.Controlz.Controls;

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
                , defaultValue:new Point()
                , validate: (o, e) => { return OnEditValuePropertyCoerceValue(o, e); });

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
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (_isUpdating)
                return;

            base.OnValueChanged(oldValue, newValue);

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
                    break;

                case PointDisplayMember.Y:
                    Value = EditValue.Y;
                    break;

                default:
                    Value = 0;
                    break;
            }

            _isUpdating = false;
        }
    }
}
