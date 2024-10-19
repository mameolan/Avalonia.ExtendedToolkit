using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Model.PropertyGrid_CustomTypeEditors
{
    //===============================================================
    // Uncomment the attributes below to show the Name property only
    //===============================================================
    //[BrowsableProperty(BrowsablePropertyAttribute.All, false)]
    //[BrowsableProperty("Name", true)]

    //===============================================================
    // Uncomment the attributes below to show "Display" category only
    //===============================================================
    //[BrowsableCategory(BrowsableCategoryAttribute.All, false)]
    //[BrowsableCategory("Display", true)]
    public class BusinessObject : AvaloniaObject
    {
        private const string CustomCategory = "Custom Int32 editor with PropertyOrder";

        [Category("Account information")]
        [ParenthesizePropertyName(true)]
        [Description("This property is marked with ParenthesizePropertyName attribute...")]
        //[Browsable(false)]
        public DateTime RegisteredDate
        {
            get { return (DateTime)GetValue(RegisteredDateProperty); }
            set { SetValue(RegisteredDateProperty, value); }
        }

        public static readonly StyledProperty<DateTime> RegisteredDateProperty =
            AvaloniaProperty.Register<BusinessObject, DateTime>(nameof(RegisteredDate)
                , defaultValue: DateTime.Now);

        [Category("Account information")]
        [DisplayName("Login")]
        [Description("This property supports both Inline and Extended value editors")]
        //[Browsable(false)]
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<BusinessObject, string>(nameof(Name)
                , defaultValue: "NoName");

        [Category("Account information")]
        [Description("Type your password here")]
        [MergableProperty(false)]
        //[Browsable(false)]
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly StyledProperty<string> PasswordProperty =
            AvaloniaProperty.Register<BusinessObject, string>(nameof(Password));

        [Description("Choose file to be attached.")]
        [PropertyEditor(typeof(FileBrowserDialogPropertyValueEditor))]
        [OpenFileDialogOptions("Text Files (*.txt)|*.txt", Title = "Select a text file")]
        public string Attachment
        {
            get { return (string)GetValue(AttachmentProperty); }
            set { SetValue(AttachmentProperty, value); }
        }

        public static readonly StyledProperty<string> AttachmentProperty =
            AvaloniaProperty.Register<BusinessObject, string>(nameof(Attachment), defaultValue: string.Empty);

        [Description("Choose value between 0 and 50 here.")]
        [NumberRange(0, 50, 1)]
        public double Range
        {
            get { return (double)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }

        public static readonly StyledProperty<double> RangeProperty =
            AvaloniaProperty.Register<BusinessObject, double>(nameof(Range)
                , defaultValue: 0.0);

        [Category("Display")]
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public static readonly StyledProperty<Thickness> BorderThicknessProperty =
            AvaloniaProperty.Register<BusinessObject, Thickness>(nameof(BorderThickness),
                defaultValue: new Thickness(1));

        [Category("Display")]
        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly StyledProperty<double> WidthProperty =
            AvaloniaProperty.Register<BusinessObject, double>(nameof(Width)
                , defaultValue: 20.0);

        [Category("Display")]
        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        public static readonly StyledProperty<Dock> DockProperty =
            AvaloniaProperty.Register<BusinessObject, Dock>(nameof(Dock)
               );

        public static readonly DirectProperty<BusinessObject, FontFamily> FontFamilyPlainProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, FontFamily>(
                    nameof(FontFamilyPlain),
                    o => o.FontFamilyPlain);

        private FontFamily _fontFamilyPlain = new FontFamily("Andalus");

        public FontFamily FontFamilyPlain
        {
            get { return _fontFamilyPlain; }
            set { SetAndRaise(FontFamilyPlainProperty, ref _fontFamilyPlain, value); }
        }

        public FontFamily DefaultFontFamily
        {
            get { return (FontFamily)GetValue(DefaultFontFamilyProperty); }
            set { SetValue(DefaultFontFamilyProperty, value); }
        }

        public static readonly StyledProperty<FontFamily> DefaultFontFamilyProperty =
            AvaloniaProperty.Register<BusinessObject, FontFamily>(nameof(DefaultFontFamily));

        public static readonly DirectProperty<BusinessObject, ComplexProperty> ComplexPropertyProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, ComplexProperty>(
                    nameof(ComplexProperty),
                    o => o.ComplexProperty);

        private ComplexProperty _complexProperty = new ComplexProperty();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ComplexProperty ComplexProperty
        {
            get { return _complexProperty; }
            set { SetAndRaise(ComplexPropertyProperty, ref _complexProperty, value); }
        }

        public static readonly DirectProperty<BusinessObject, int> Integer1Property =
                AvaloniaProperty.RegisterDirect<BusinessObject, int>(
                    nameof(Integer1),
                    o => o.Integer1);

        private int _integer1;

        [Category(CustomCategory)]
        [PropertyOrder(3)]
        public int Integer1
        {
            get { return _integer1; }
            set { SetAndRaise(Integer1Property, ref _integer1, value); }
        }

        public static readonly DirectProperty<BusinessObject, int> Integer2Property =
                AvaloniaProperty.RegisterDirect<BusinessObject, int>(
                    nameof(Integer2),
                    o => o.Integer2);

        private int _integer2;

        [Category(CustomCategory)]
        [PropertyOrder(2)]
        public int Integer2
        {
            get { return _integer2; }
            set { SetAndRaise(Integer2Property, ref _integer2, value); }
        }

        public static readonly DirectProperty<BusinessObject, int> Integer3Property =
                AvaloniaProperty.RegisterDirect<BusinessObject, int>(
                    nameof(Integer3),
                    o => o.Integer3);

        private int _integer3;

        [Category(CustomCategory)]
        [PropertyOrder(1)]
        public int Integer3
        {
            get { return _integer3; }
            set { SetAndRaise(Integer3Property, ref _integer3, value); }
        }

        public static readonly DirectProperty<BusinessObject, int> Integer4Property =
                AvaloniaProperty.RegisterDirect<BusinessObject, int>(
                    nameof(Integer4),
                    o => o.Integer4);

        private int _integer4;

        [Category(CustomCategory)]
        [PropertyOrder(0)]
        public int Integer4
        {
            get { return _integer4; }
            set { SetAndRaise(Integer4Property, ref _integer4, value); }
        }

        public VideoDevices Camera
        {
            get { return (VideoDevices)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public static readonly StyledProperty<VideoDevices> CameraProperty =
            AvaloniaProperty.Register<BusinessObject, VideoDevices>(nameof(Camera)
                , defaultValue: VideoDevices.UNSPECIFIED
                , coerce: (o, e) => { return ValidateDevice(o, e); }
                );

        private static VideoDevices ValidateDevice(AvaloniaObject o, VideoDevices e)
        {
            if (IsDeviceLegal(e) == false)
            {
                //show message
            }

            return e;
        }

        public static readonly DirectProperty<BusinessObject, double> DoubleBlendProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, double>(
                    nameof(DoubleBlend),
                    o => o.DoubleBlend);

        private double _doubleBlend;

        [Description("Drag the value with the mouse or click the middle of the control to enable keyboard")]
        [Category("Double editors")]
        [DisplayName("Double (Blend)")]
        public double DoubleBlend
        {
            get { return _doubleBlend; }
            private set { SetAndRaise(DoubleBlendProperty, ref _doubleBlend, value); }
        }

        public static readonly DirectProperty<BusinessObject, double> DoubleCommonProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, double>(
                    nameof(DoubleCommon),
                    o => o.DoubleCommon);

        private double _doubleCommon;

        [Category("Double editors")]
        [Description("Default (Texbox-based) editor is used for this property")]
        [DisplayName("Double (Common)")]
        public double DoubleCommon
        {
            get { return _doubleCommon; }
            private set { SetAndRaise(DoubleCommonProperty, ref _doubleCommon, value); }
        }

        [Category("Text")]
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly StyledProperty<FontFamily> FontFamilyProperty =
            AvaloniaProperty.Register<BusinessObject, FontFamily>(nameof(FontFamily)
                , defaultValue: TemplatedControl.FontFamilyProperty.GetDefaultValue(typeof(TemplatedControl)));

        [Category("Text")]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly StyledProperty<double> FontSizeProperty =
            AvaloniaProperty.Register<BusinessObject, double>(nameof(FontSize)
                , defaultValue: TemplatedControl.FontSizeProperty.GetDefaultValue(typeof(TemplatedControl))
                );

        [Category("Text")]
        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public static readonly StyledProperty<FontWeight> FontWeightProperty =
            AvaloniaProperty.Register<BusinessObject, FontWeight>(nameof(FontWeight)
                , defaultValue: TemplatedControl.FontWeightProperty.GetDefaultValue(typeof(TemplatedControl))
                );

        [Category("Text")]
        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public static readonly StyledProperty<FontStyle> FontStyleProperty =
           AvaloniaProperty.Register<BusinessObject, FontStyle>(nameof(FontStyle)
               , defaultValue: TemplatedControl.FontStyleProperty.GetDefaultValue(typeof(TemplatedControl))
               );

        public string Percentage
        {
            get { return (string)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public static readonly StyledProperty<string> PercentageProperty =
            AvaloniaProperty.Register<BusinessObject, string>(nameof(Percentage));

        [Category("Display")]
        public Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }


        public static readonly StyledProperty<Point> PointProperty =
            AvaloniaProperty.Register<BusinessObject, Point>(nameof(Point), defaultValue:new Point(0,0));






        public IBrush BackgroundBrush { get; set; }

        public BusinessObject()
        {
            //set init values
            Dock = Dock.Bottom;
            Percentage = "10";
            DefaultFontFamily = FontFamily = new FontFamily("Arial");
            BackgroundBrush = Brushes.AliceBlue;

        }

        private static bool IsDeviceLegal(VideoDevices device)
        {
            return device != VideoDevices.LOGITECH_PRO_5000;
        }
    }
}
