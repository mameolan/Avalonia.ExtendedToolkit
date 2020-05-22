using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro
    /// <summary>
    /// Button with a title
    /// </summary>
    public class Tile : MetroButton
    {
        private const string PseudoClassIsNullMouseOverBorderBrush = ":IsNullMouseOverBorderBrush";

        /// <summary>
        /// get/sets Title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// <see cref="Title"/>
        /// </summary>
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<Tile, string>(nameof(Title));

        /// <summary>
        /// get/sets HorizontalTitleAlignment
        /// </summary>
        public HorizontalAlignment HorizontalTitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalTitleAlignmentProperty); }
            set { SetValue(HorizontalTitleAlignmentProperty, value); }
        }

        /// <summary>
        /// <see cref="HorizontalTitleAlignment"/>
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> HorizontalTitleAlignmentProperty =
            AvaloniaProperty.Register<Tile, HorizontalAlignment>(nameof(HorizontalTitleAlignment)
                , defaultValue: HorizontalAlignment.Left);

        /// <summary>
        /// get/set VerticalTitleAlignment
        /// </summary>
        public VerticalAlignment VerticalTitleAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalTitleAlignmentProperty); }
            set { SetValue(VerticalTitleAlignmentProperty, value); }
        }

        /// <summary>
        /// <see cref="VerticalTitleAlignment"/>
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> VerticalTitleAlignmentProperty =
            AvaloniaProperty.Register<Tile, VerticalAlignment>(nameof(VerticalTitleAlignment)
                , defaultValue: VerticalAlignment.Bottom);

        /// <summary>
        /// get/sets Count
        /// </summary>
        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        /// <summary>
        /// <see cref="Count"/>
        /// </summary>
        public static readonly StyledProperty<string> CountProperty =
            AvaloniaProperty.Register<Tile, string>(nameof(Count));

        /// <summary>
        /// get/sets KeepDragging
        /// </summary>
        public bool KeepDragging
        {
            get { return (bool)GetValue(KeepDraggingProperty); }
            set { SetValue(KeepDraggingProperty, value); }
        }

        /// <summary>
        /// <see cref="KeepDragging"/>
        /// </summary>
        public static readonly StyledProperty<bool> KeepDraggingProperty =
            AvaloniaProperty.Register<Tile, bool>(nameof(KeepDragging));

        /// <summary>
        /// get/sets TiltFactor
        /// </summary>
        public int TiltFactor
        {
            get { return (int)GetValue(TiltFactorProperty); }
            set { SetValue(TiltFactorProperty, value); }
        }

        /// <summary>
        /// <see cref="TiltFactor"/>
        /// </summary>
        public static readonly StyledProperty<int> TiltFactorProperty =
            AvaloniaProperty.Register<Tile, int>(nameof(TiltFactor), defaultValue: 5);

        /// <summary>
        /// get/sets TitleFontSize
        /// </summary>
        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        /// <summary>
        /// <see cref="TitleFontSize"/>
        /// </summary>
        public static readonly StyledProperty<double> TitleFontSizeProperty =
            AvaloniaProperty.Register<Tile, double>(nameof(TitleFontSize), defaultValue: 16d);

        /// <summary>
        /// get/sets CountFontSize
        /// </summary>
        public double CountFontSize
        {
            get { return (double)GetValue(CountFontSizeProperty); }
            set { SetValue(CountFontSizeProperty, value); }
        }

        /// <summary>
        /// <see cref="CountFontSize"/>
        /// </summary>
        public static readonly StyledProperty<double> CountFontSizeProperty =
            AvaloniaProperty.Register<Tile, double>(nameof(CountFontSize), defaultValue: 28d);

        /// <summary>
        /// get/sets MouseOverBorderBrush
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
            AvaloniaProperty.Register<Tile, IBrush>(nameof(MouseOverBorderBrush));

        /// <summary>
        /// add MouseOverBorderBrush changed handler 
        /// </summary>
        public Tile()
        {
            PseudoClasses.Add(PseudoClassIsNullMouseOverBorderBrush);
            MouseOverBorderBrushProperty.Changed.AddClassHandler<Tile>((o, e) => OnMouseOverBorderBrushChanged(o, e));
        }

        /// <summary>
        /// adds/remove PseudoClassIsNullMouseOverBorderBrush
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
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
