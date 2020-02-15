using Avalonia.Layout;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class Tile : MetroButton
    {
        private string PseudoClassIsNullMouseOverBorderBrush = ":IsNullMouseOverBorderBrush";

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<Tile, string>(nameof(Title));



        public HorizontalAlignment HorizontalTitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalTitleAlignmentProperty); }
            set { SetValue(HorizontalTitleAlignmentProperty, value); }
        }


        public static readonly StyledProperty<HorizontalAlignment> HorizontalTitleAlignmentProperty =
            AvaloniaProperty.Register<Tile, HorizontalAlignment>(nameof(HorizontalTitleAlignment)
                , defaultValue: HorizontalAlignment.Left);



        public VerticalAlignment VerticalTitleAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalTitleAlignmentProperty); }
            set { SetValue(VerticalTitleAlignmentProperty, value); }
        }


        public static readonly StyledProperty<VerticalAlignment> VerticalTitleAlignmentProperty =
            AvaloniaProperty.Register<Tile, VerticalAlignment>(nameof(VerticalTitleAlignment)
                , defaultValue: VerticalAlignment.Bottom);



        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }


        public static readonly StyledProperty<string> CountProperty =
            AvaloniaProperty.Register<Tile, string>(nameof(Count));



        public bool KeepDragging
        {
            get { return (bool)GetValue(KeepDraggingProperty); }
            set { SetValue(KeepDraggingProperty, value); }
        }


        public static readonly StyledProperty<bool> KeepDraggingProperty =
            AvaloniaProperty.Register<Tile, bool>(nameof(KeepDragging));



        public int TiltFactor
        {
            get { return (int)GetValue(TiltFactorProperty); }
            set { SetValue(TiltFactorProperty, value); }
        }


        public static readonly StyledProperty<int> TiltFactorProperty =
            AvaloniaProperty.Register<Tile, int>(nameof(TiltFactor), defaultValue: 5);



        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }


        public static readonly StyledProperty<double> TitleFontSizeProperty =
            AvaloniaProperty.Register<Tile, double>(nameof(TitleFontSize), defaultValue: 16d);



        public double CountFontSize
        {
            get { return (double)GetValue(CountFontSizeProperty); }
            set { SetValue(CountFontSizeProperty, value); }
        }


        public static readonly StyledProperty<double> CountFontSizeProperty =
            AvaloniaProperty.Register<Tile, double>(nameof(CountFontSize), defaultValue: 28d);




        public IBrush MouseOverBorderBrush
        {
            get { return (IBrush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }


        public static readonly StyledProperty<IBrush> MouseOverBorderBrushProperty =
            AvaloniaProperty.Register<Tile, IBrush>(nameof(MouseOverBorderBrush));

        public Tile()
        {
            PseudoClasses.Add(PseudoClassIsNullMouseOverBorderBrush);
            MouseOverBorderBrushProperty.Changed.AddClassHandler<Tile>((o, e) => OnMouseOverBorderBrushChanged(o, e));

        }

        private void OnMouseOverBorderBrushChanged(Tile o, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.NewValue==null)
            {
                o.PseudoClasses.Add(PseudoClassIsNullMouseOverBorderBrush);
            }
            else
            {
                o.PseudoClasses.Remove(PseudoClassIsNullMouseOverBorderBrush);
            }
        }
    }
}
