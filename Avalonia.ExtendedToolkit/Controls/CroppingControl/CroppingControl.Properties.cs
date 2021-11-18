using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class CroppingControl : TemplatedControl
    {
        private static readonly string PART_ScrollViewer = "PART_ScrollViewer";
        private static readonly double ZoomDevidedFactor = 50.0;
        private static readonly double MinZoomFactor = 0.01;
        private bool _isMouseCaptured;
        private Point? _mouseLeftDownPoint = null;


        private Image _image = new Image
        {
            Stretch = Stretch.UniformToFill,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
        };

        private Grid _contentGrid;
        private Slider _zoomSlider;
        private Canvas _adornerCanvas;
        private ScrollViewer _scrollViewer;
        private ItemVM _flipVertical;

        private ItemVM _fliphorizontal;
        private ItemVM _croppingTypeRectangle;
        private ItemVM _croppingTypeCircle;
        private ItemVM _rotateLeftItem;
        private ItemVM _rotateRightItem;

        Rectangle _backgroundRectangle = new Rectangle
        {
            Fill = Brushes.Gray,
        };

        private ResizeRotateControl RubberBand { get; set; }
        private Ellipse RubberBandEllipse { get; set; }

        /// <summary>
        /// Gets or sets ImagePath.
        /// </summary>
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        /// <summary>
        /// Defines the ImagePath property.
        /// </summary>
        public static readonly StyledProperty<string> ImagePathProperty =
        AvaloniaProperty.Register<CroppingControl, string>(nameof(ImagePath));

        /// <summary>
        /// Gets or sets CroppedImage.
        /// </summary>
        public IImage CroppedImage
        {
            get { return (IImage)GetValue(CroppedImageProperty); }
            private set { SetValue(CroppedImageProperty, value); }
        }

        /// <summary>
        /// Defines the CroppedImage property.
        /// </summary>
        public static readonly StyledProperty<IImage> CroppedImageProperty =
        AvaloniaProperty.Register<CroppingControl, IImage>(nameof(CroppedImage));

        /// <summary>
        /// Gets or sets ImageInputBytes.
        /// </summary>
        public byte[] ImageInputBytes
        {
            get { return (byte[])GetValue(ImageInputBytesProperty); }
            set { SetValue(ImageInputBytesProperty, value); }
        }

        /// <summary>
        /// Defines the ImageInputBytes property.
        /// </summary>
        public static readonly StyledProperty<byte[]> ImageInputBytesProperty =
        AvaloniaProperty.Register<CroppingControl, byte[]>(nameof(ImageInputBytes), defaultValue: null);

        /// <summary>
        /// Gets or sets ImageOutputBytes.
        /// </summary>
        public byte[] ImageOutputBytes
        {
            get { return (byte[])GetValue(ImageOutputBytesProperty); }
            set { SetValue(ImageOutputBytesProperty, value); }
        }

        /// <summary>
        /// Defines the ImageOutputBytes property.
        /// </summary>
        public static readonly StyledProperty<byte[]> ImageOutputBytesProperty =
        AvaloniaProperty.Register<CroppingControl, byte[]>(nameof(ImageOutputBytes), defaultValue: null);

        /// <summary>
        /// Gets or sets SaveCommand.
        /// </summary>
        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            private set { SetValue(SaveCommandProperty, value); }
        }

        /// <summary>
        /// Defines the SaveCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> SaveCommandProperty =
        AvaloniaProperty.Register<CroppingControl, ICommand>(nameof(SaveCommand));

        /// <summary>
        /// Gets or sets OpenImageCommand.
        /// </summary>
        public ICommand OpenImageCommand
        {
            get { return (ICommand)GetValue(OpenImageCommandProperty); }
            private set { SetValue(OpenImageCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets RotateResizeBorderBrush.
        /// </summary>
        public IBrush RotateResizeBorderBrush
        {
            get { return (IBrush)GetValue(RotateResizeBorderBrushProperty); }
            set { SetValue(RotateResizeBorderBrushProperty, value); }
        }

        /// <summary>
        /// Defines the RotateResizeBorderBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> RotateResizeBorderBrushProperty =
        AvaloniaProperty.Register<CroppingControl, IBrush>(nameof(RotateResizeBorderBrush));

        /// <summary>
        /// Defines the OpenImageCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> OpenImageCommandProperty =
        AvaloniaProperty.Register<CroppingControl, ICommand>(nameof(OpenImageCommand));

        /// <summary>
        /// Gets or sets CanDoPictureModification.
        /// </summary>
        public bool CanDoPictureModification
        {
            get { return (bool)GetValue(CanDoPictureModificationProperty); }
            set { SetValue(CanDoPictureModificationProperty, value); }
        }

        /// <summary>
        /// Defines the CanDoPictureModification property.
        /// </summary>
        public static readonly StyledProperty<bool> CanDoPictureModificationProperty =
        AvaloniaProperty.Register<CroppingControl, bool>(nameof(CanDoPictureModification), defaultValue: false);

        /// <summary>
        /// Gets or sets ZoomIncrementCommand.
        /// </summary>
        public ICommand ZoomIncrementCommand
        {
            get { return (ICommand)GetValue(ZoomIncrementCommandProperty); }
            set { SetValue(ZoomIncrementCommandProperty, value); }
        }

        /// <summary>
        /// Defines the ZoomIncrementCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> ZoomIncrementCommandProperty =
        AvaloniaProperty.Register<CroppingControl, ICommand>(nameof(ZoomIncrementCommand));

        /// <summary>
        /// Gets or sets ZoomDecrementCommand.
        /// </summary>
        public ICommand ZoomDecrementCommand
        {
            get { return (ICommand)GetValue(ZoomDecrementCommandProperty); }
            set { SetValue(ZoomDecrementCommandProperty, value); }
        }

        /// <summary>
        /// Defines the ZoomDecrementCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> ZoomDecrementCommandProperty =
        AvaloniaProperty.Register<CroppingControl, ICommand>(nameof(ZoomDecrementCommand));

        /// <summary>
        /// Gets or sets ZoomFactor.
        /// </summary>
        public double ZoomFactor
        {
            get { return (double)GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        }

        /// <summary>
        /// Defines the ZoomFactor property.
        /// </summary>
        public static readonly StyledProperty<double> ZoomFactorProperty =
        AvaloniaProperty.Register<CroppingControl, double>(nameof(ZoomFactor), defaultValue: 1.0);

        //public ItemVM FlipVerticalVM{get;set;}=new ItemVM{Text="Flip Vertical",Image};
        /// <summary>
        /// Gets or sets FlipItems.
        /// </summary>
        public ObservableCollection<ItemVM> FlipItems
        {
            get { return (ObservableCollection<ItemVM>)GetValue(FlipItemsProperty); }
            set { SetValue(FlipItemsProperty, value); }
        }

        /// <summary>
        /// Defines the FlipItems property.
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<ItemVM>> FlipItemsProperty =
        AvaloniaProperty.Register<CroppingControl, ObservableCollection<ItemVM>>(nameof(FlipItems));

        /// <summary>
        /// Gets or sets CroppingTypes.
        /// </summary>
        public ObservableCollection<ItemVM> CroppingTypes
        {
            get { return (ObservableCollection<ItemVM>)GetValue(CroppingTypesProperty); }
            set { SetValue(CroppingTypesProperty, value); }
        }

        /// <summary>
        /// Defines the CroppingTypes property.
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<ItemVM>> CroppingTypesProperty =
        AvaloniaProperty.Register<CroppingControl, ObservableCollection<ItemVM>>(nameof(CroppingTypes));

        /// <summary>
        /// Gets or sets CurrentCroppingType.
        /// </summary>
        public ItemVM CurrentCroppingType
        {
            get { return (ItemVM)GetValue(CurrentCroppingTypeProperty); }
            set { SetValue(CurrentCroppingTypeProperty, value); }
        }

        /// <summary>
        /// Defines the CurrentCroppingType property.
        /// </summary>
        public static readonly StyledProperty<ItemVM> CurrentCroppingTypeProperty =
        AvaloniaProperty.Register<CroppingControl, ItemVM>(nameof(CurrentCroppingType));

        /// <summary>
        /// Gets or sets RotateItems.
        /// </summary>
        public ObservableCollection<ItemVM> RotateItems
        {
            get { return (ObservableCollection<ItemVM>)GetValue(RotateItemsProperty); }
            set { SetValue(RotateItemsProperty, value); }
        }

        /// <summary>
        /// Defines the RotateItems property.
        /// </summary>
        public static readonly StyledProperty<ObservableCollection<ItemVM>> RotateItemsProperty =
        AvaloniaProperty.Register<CroppingControl, ObservableCollection<ItemVM>>(nameof(RotateItems));

        /// <summary>
        /// Gets or sets PictureManipulationCommand.
        /// </summary>
        public ICommand PictureManipulationCommand
        {
            get { return (ICommand)GetValue(PictureManipulationCommandProperty); }
            set { SetValue(PictureManipulationCommandProperty, value); }
        }

        /// <summary>
        /// Defines the PictureManipulationCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> PictureManipulationCommandProperty =
        AvaloniaProperty.Register<CroppingControl, ICommand>(nameof(PictureManipulationCommand));



        /// <summary>
        /// Gets or sets ZoomMaximum.
        /// </summary>
        public double ZoomMaximum
        {
            get { return (double)GetValue(ZoomMaximumProperty); }
            set { SetValue(ZoomMaximumProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ZoomMaximum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ZoomMaximumProperty =
            AvaloniaProperty.Register<CroppingControl, double>(nameof(ZoomMaximum), defaultValue: 200);



        /// <summary>
        /// Gets or sets ZoomMinimum.
        /// </summary>
        public double ZoomMinimum
        {
            get { return (double)GetValue(ZoomMinimumProperty); }
            set { SetValue(ZoomMinimumProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ZoomMinimum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ZoomMinimumProperty =
            AvaloniaProperty.Register<CroppingControl, double>(nameof(ZoomMinimum), defaultValue: 1d);





        /// <summary>
        /// Gets or sets RotateResizeBackgroundBrush.
        /// </summary>
        public IBrush RotateResizeBackgroundBrush
        {
            get { return (IBrush)GetValue(RotateResizeBackgroundBrushProperty); }
            set { SetValue(RotateResizeBackgroundBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="RotateResizeBackgroundBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> RotateResizeBackgroundBrushProperty =
            AvaloniaProperty.Register<CroppingControl, IBrush>(nameof(RotateResizeBackgroundBrush));



        /// <summary>
        /// Gets or sets CancelCommand.
        /// </summary>
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="CancelCommand"/> property.
        /// </summary>
        public static readonly StyledProperty<ICommand> CancelCommandProperty =
            AvaloniaProperty.Register<CroppingControl, ICommand>(nameof(CancelCommand));




        /// <summary>
        /// Gets or sets BackgroundRectangleOpacity.
        /// </summary>
        public double BackgroundRectangleOpacity
        {
            get { return (double)GetValue(BackgroundRectangleOpacityProperty); }
            set { SetValue(BackgroundRectangleOpacityProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="BackgroundRectangleOpacity"/> property.
        /// </summary>
        public static readonly StyledProperty<double> BackgroundRectangleOpacityProperty =
            AvaloniaProperty.Register<CroppingControl, double>(nameof(BackgroundRectangleOpacity), defaultValue: 0.5d);







    }
}
