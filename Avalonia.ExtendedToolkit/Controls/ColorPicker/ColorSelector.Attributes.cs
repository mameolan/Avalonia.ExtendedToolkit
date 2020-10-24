using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Controlz.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class ColorSelector : TemplatedControl
    {
        private const string PART_Image = "PART_Image";
        private const string PART_NumericAlpha = "PART_NumericAlpha";
        private const string PART_NumericRed = "PART_NumericRed";
        private const string PART_NumericGreen = "PART_NumericGreen";
        private const string PART_NumericBlue = "PART_NumericBlue";
        private const string PART_TxtAll = "PART_TxtAll";
        private const string PART_Preview = "PART_Preview";
        private const string PART_CanvasColor = "PART_CanvasColor";
        private const string PART_Pointer = "PART_Pointer";
        private const string PART_SliderAlpha = "PART_SliderAlpha";
        private const string PART_DefaultPicker = "PART_DefaultPicker";
        private const string PART_DefaultColorSection = "PART_DefaultColorSection";
        private const string PART_CustomColorSection = "PART_CustomColorSection";

        private bool _isMouseDownOverEllipse = false;
        private bool _shift = false;

        private Image _image;
        private TextBox _txtAlpha;
        private TextBox _txtRed;
        private TextBox _txtGreen;
        private TextBox _txtBlue;

        //NumericUpDown _numericAlpha;
        //NumericUpDown _numericRed;
        //NumericUpDown _numericGreen;
        //NumericUpDown _numericBlue;


        private TextBox _txtAll;
        private Border _borderPreview;
        private Canvas _canvasColor;
        private Ellipse _ellipsePointer;
        private Slider _sliderAlpha;
        private ListBox _defaultPicker;

        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey { get { return typeof(ColorSelector); } }

        /// <summary>
        /// Defines the <see cref="CustomColor"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ColorSelector, Color> CustomColorProperty =
                AvaloniaProperty.RegisterDirect<ColorSelector, Color>(
                    nameof(CustomColor),
                    o => o.CustomColor);

        private Color _customColor = Colors.Transparent;

        /// <summary>
        /// Gets or sets CustomColor.
        /// </summary>
        public Color CustomColor
        {
            get { return _customColor; }
            set
            {
                SetAndRaise(CustomColorProperty, ref _customColor, value);
                UpdatePreview();
            }
        }



        /// <summary>
        /// Defines the <see cref="AlphaValue"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ColorSelector, uint> AlphaValueProperty =
                AvaloniaProperty.RegisterDirect<ColorSelector, uint>(
                    nameof(AlphaValue),
                    o => o.AlphaValue);

        private uint _alphaValue;

        /// <summary>
        /// Gets or sets AlphaValue.
        /// </summary>
        public uint AlphaValue
        {
            get { return _alphaValue; }
            set
            {
                SetAndRaise(AlphaValueProperty, ref _alphaValue, value);
            }
        }



        /// <summary>
        /// Defines the <see cref="RedValue"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ColorSelector, uint> RedValueProperty =
                AvaloniaProperty.RegisterDirect<ColorSelector, uint>(
                    nameof(RedValue),
                    o => o.RedValue);

        private uint _redValue;

        /// <summary>
        /// Gets or sets RedValue.
        /// </summary>
        public uint RedValue
        {
            get { return _redValue; }
            set
            {
                SetAndRaise(RedValueProperty, ref _redValue, value);
            }
        }



        /// <summary>
        /// Defines the <see cref="GreenValue"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ColorSelector, uint> GreenValueProperty =
                AvaloniaProperty.RegisterDirect<ColorSelector, uint>(
                    nameof(GreenValue),
                    o => o.GreenValue);

        private uint _greenValue;

        /// <summary>
        /// Gets or sets GreenValue.
        /// </summary>
        public uint GreenValue
        {
            get { return _greenValue; }
            set
            {
                SetAndRaise(GreenValueProperty, ref _greenValue, value);
            }
        }



        /// <summary>
        /// Defines the <see cref="BlueValue"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ColorSelector, uint> BlueValueProperty =
                AvaloniaProperty.RegisterDirect<ColorSelector, uint>(
                    nameof(BlueValue),
                    o => o.BlueValue);

        private uint _blueValue;

        /// <summary>
        /// Gets or sets BlueValue.
        /// </summary>
        public uint BlueValue
        {
            get { return _blueValue; }
            set
            {
                SetAndRaise(BlueValueProperty, ref _blueValue, value);
            }
        }









        /// <summary>
        /// Defines the <see cref="DefaultPickerColorChanged"/> routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> DefaultPickerColorChangedEvent =
                    RoutedEvent.Register<ColorSelector, RoutedEventArgs>(nameof(DefaultPickerColorChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets DefaultPickerColorChanged eventhandler.
        /// </summary>
        public event EventHandler DefaultPickerColorChanged
        {
            add
            {
                AddHandler(DefaultPickerColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(DefaultPickerColorChangedEvent, value);
            }
        }
    }
}
