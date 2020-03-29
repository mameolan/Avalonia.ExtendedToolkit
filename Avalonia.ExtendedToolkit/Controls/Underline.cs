using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public class Underline: ContentControl
    {
        public const string UnderlineBorderPartName = "PART_UnderlineBorder";
        private Border _underlineBorder;

        public Dock Placement
        {
            get { return (Dock)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly StyledProperty<Dock> PlacementProperty =
            AvaloniaProperty.Register<Underline, Dock>(nameof(Placement));

        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        public static readonly StyledProperty<double> LineThicknessProperty =
            AvaloniaProperty.Register<Underline, double>(nameof(LineThickness),defaultValue: 1d);

        public double LineExtent
        {
            get { return (double)GetValue(LineExtentProperty); }
            set { SetValue(LineExtentProperty, value); }
        }

        public static readonly StyledProperty<double> LineExtentProperty =
            AvaloniaProperty.Register<Underline, double>(nameof(LineExtent),defaultValue: double.NaN);

        public Underline()
        {
            PlacementProperty.Changed.AddClassHandler<Underline>((o, e) => ApplyBorderProperties(o, e));
            LineThicknessProperty.Changed.AddClassHandler<Underline>((o, e) => ApplyBorderProperties(o, e));
            LineExtentProperty.Changed.AddClassHandler<Underline>((o, e) => ApplyBorderProperties(o, e));
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            _underlineBorder = e.NameScope.Find<Border>(UnderlineBorderPartName);
            ApplyBorderProperties();
        }

        private void ApplyBorderProperties(Underline o, AvaloniaPropertyChangedEventArgs e)
        {
            o.ApplyBorderProperties();
        }

        private void ApplyBorderProperties()
        {
            if (this._underlineBorder == null)
            {
                return;
            }

            this._underlineBorder.Height = Double.NaN;
            this._underlineBorder.Width = Double.NaN;
            this._underlineBorder.BorderThickness = new Thickness();
            switch (this.Placement)
            {
                case Dock.Left:
                    this._underlineBorder.Width = this.LineExtent;
                    this._underlineBorder.BorderThickness = new Thickness(this.LineThickness, 0d, 0d, 0d);
                    break;

                case Dock.Top:
                    this._underlineBorder.Height = this.LineExtent;
                    this._underlineBorder.BorderThickness = new Thickness(0d, this.LineThickness, 0d, 0d);
                    break;

                case Dock.Right:
                    this._underlineBorder.Width = this.LineExtent;
                    this._underlineBorder.BorderThickness = new Thickness(0d, 0d, this.LineThickness, 0d);
                    break;

                case Dock.Bottom:
                    this._underlineBorder.Height = this.LineExtent;
                    this._underlineBorder.BorderThickness = new Thickness(0d, 0d, 0d, this.LineThickness);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.InvalidateVisual();
        }
    }
}
