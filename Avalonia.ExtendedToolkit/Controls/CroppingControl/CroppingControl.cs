using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;
using Splat;
using SkiaSharp;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// control wich cuts a part of a picture
    /// </summary>
    public partial class CroppingControl : TemplatedControl
    {
        /// <summary>
        /// registers required changed handler
        /// </summary>
        public CroppingControl()
        {
            ImagePathProperty.Changed.AddClassHandler<CroppingControl>((o, e) => OnImagePathChanged(o, e));

            CroppedImageProperty.Changed.AddClassHandler<CroppingControl>((o, e) => OnCroppedImageChanged(o, e));

            ZoomFactorProperty.Changed.AddClassHandler<CroppingControl>((o, e) => OnZoomFactorChanged(o, e));

            var canIncrementZoomExecute = this.WhenAnyValue(x => x.ZoomMaximum,
                x => x.CanDoPictureModification).Select(x => x.Item1 <= ZoomMaximum && x.Item2 == true);
            ZoomIncrementCommand = ReactiveCommand.Create(() => ExecuteZoomIncrementCommand()
            , canIncrementZoomExecute);

            var canDecrementZoomExecute = this.WhenAnyValue(x => x.ZoomMinimum, x => x.CanDoPictureModification).
                Select(x => x.Item1 >= ZoomMinimum && x.Item2 == true);
            ZoomDecrementCommand = ReactiveCommand.Create(() => ExecuteZoomDecrementCommand(), canDecrementZoomExecute);

            var canSaveImageExecute = this.WhenAnyValue(x => x.CroppedImage).Select(x => x != null);
            SaveCommand = ReactiveCommand.Create(() => ExecuteSaveCommand(), canSaveImageExecute);

            OpenImageCommand = ReactiveCommand.Create(() => ExecuteOpenImageCommand(), outputScheduler: RxApp.MainThreadScheduler);

            var canPictureManipulationExecute = this.WhenAnyValue(x => x.CanDoPictureModification).Select(x => x == true);
            PictureManipulationCommand = ReactiveCommand.Create<object>((x) =>
            ExecutePictureManipulationCommand(x), canPictureManipulationExecute);

            var canExecuteCancel = this.WhenAnyValue(x => x.CroppedImage).Select(x => x != null);
            CancelCommand = ReactiveCommand.Create(() => ExecuteCancelCommand(), canExecuteCancel);
        }

        /// <summary>
        /// calls <see cref="Reset()"/>
        /// </summary>
        private void ExecuteCancelCommand()
        {
            Reset();
        }

        /// <summary>
        /// Rotates or flips the image
        /// </summary>
        /// <param name="x"></param>
        private void ExecutePictureManipulationCommand(object x)
        {
            ItemVM itemVM = x as ItemVM;
            if (itemVM.Data is RotateType rotateType)
            {
                Bitmap bitmap = _image.Source as Bitmap;

                _image.Source = bitmap.Rotate(rotateType);

                _adornerCanvas.Width = bitmap.PixelSize.Width;
                _adornerCanvas.Height = bitmap.PixelSize.Height;
            }
            else if (itemVM.Data is FipType flipType)
            {
                IImage bitmap = _image.Source;
                _image.Source = bitmap.Flip(flipType);
            }
        }

        /// <summary>
        /// saves the cropped image to the filesystem.
        /// </summary>
        private void ExecuteSaveCommand()
        {
            var target = CroppedImage;// as BitmapExt;

            IFileDialogService service = Locator.Current.GetService<IFileDialogService>();
            Window window = ApplicationExtension.GetMainWindow();

            service?.OpenSaveFileDialog(filters: FileFilterBuilder.Setup().WithImageFilter().Build()
                                        , defaultExtension: FileFilter.PNG_Extension)
                                .ContinueWith(x =>
                                {
                                    string savePath = x.Result;
                                    try
                                    {
                                        target.Save(savePath);
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine(ex.Message);
                                    }
                                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// decrememts the zoom factor
        /// </summary>
        private void ExecuteZoomDecrementCommand()
        {
            if (_zoomSlider == null)
                return;
            double tempZoomFactor = ZoomFactor;
            tempZoomFactor -= _zoomSlider.SmallChange;
            if (tempZoomFactor >= MinZoomFactor)
            {
                ZoomFactor = tempZoomFactor;
            }
        }

        /// <summary>
        /// increments the zoom factor
        /// </summary>
        private void ExecuteZoomIncrementCommand()
        {
            if (_zoomSlider == null)
                return;

            ZoomFactor += _zoomSlider.SmallChange;
        }

        /// <summary>
        /// shows hides the ellipse
        /// </summary>
        private void OnCurrentCroppingTypeChanged(CroppingControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is ItemVM itemVM)
            {
                CroppingType croppingType = (CroppingType)itemVM.Data;
                switch (croppingType)
                {
                    case CroppingType.Circle:
                        RubberBandEllipse.IsVisible = true;
                        break;

                    case CroppingType.Rectangle:
                        RubberBandEllipse.IsVisible = false;
                        break;
                }
            }
        }

        /// <summary>
        /// changes the image by the given zoom factor
        /// </summary>
        private void OnZoomFactorChanged(CroppingControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is double zoomFactor && _image.Source != null)
            {
                zoomFactor = Math.Max(zoomFactor / ZoomDevidedFactor, MinZoomFactor);

                _image.Source = _image.Source.Zoom(zoomFactor); //bitmap.Zoom(zoomFactor);

                UpdateAdornerSize();
            }
        }

        /// <summary>
        /// sets <see cref="CanDoPictureModification"/> to true
        /// if a picture is loaden and no <see cref="CroppedImage"/> exists
        /// else to false.
        /// </summary>
        private void OnCroppedImageChanged(CroppingControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue == null && File.Exists(ImagePath))
            {
                CanDoPictureModification = true;
            }
            else
            {
                CanDoPictureModification = false;
            }
        }

        /// <summary>
        /// opens an image from the filesystem
        /// </summary>
        private void ExecuteOpenImageCommand()
        {
            IFileDialogService service = Locator.Current.GetService<IFileDialogService>();

            service?.OpenFileDialog(initialFileName: ImagePath,
                    filters: FileFilterBuilder.Setup().WithImageFilter().Build())
                    .ContinueWith(x =>
                    {
                        ImagePath = x.Result?.FirstOrDefault();
                    }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// if key esc is pressed set <see cref="CroppedImage"/> to null
        /// and removes the <see cref="RubberBand"/> from the canvas.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape && RubberBand != null)
            {
                Reset();
            }
        }

        /// <summary>
        /// removes the rubberband, background rectangle and resets the croppedImage
        /// </summary>
        private void Reset()
        {
            if (_adornerCanvas == null || _backgroundRectangle == null)
            {
                return;
            }

            _adornerCanvas.Children.Remove(RubberBand);
            _adornerCanvas.Children.Remove(_backgroundRectangle);

            _backgroundRectangle.Width = RubberBand.Width = RubberBand.MinWidth;
            _backgroundRectangle.Height = RubberBand.Height = RubberBand.MinHeight;

            CroppedImage = null;
            _mouseLeftDownPoint = null;
        }

        /// <summary>
        /// calls <see cref="UpdateCroppedImage"/>
        /// </summary>
        private void RubberBand_MoveFinished(object sender, VectorEventArgs e)
        {
            //Debug.WriteLine($"Moved vector { e.Vector}");

            UpdateCroppedImage();
        }

        /// <summary>
        /// creates an new cropped bitmap from
        /// the <see cref="RubberBand"/> coordinates.
        /// </summary>
        private void UpdateCroppedImage()
        {
            if (RubberBand != null)
            {
                CroppingType croppingType = (CroppingType)CurrentCroppingType.Data;
                double croppingLeft = 0;
                double croppingTop = 0;
                double croppingWidth = 0;
                double croppingHeight = 0;

                switch (croppingType)
                {
                    case CroppingType.Circle:
                        croppingLeft = Canvas.GetLeft(RubberBand as AvaloniaObject);
                        croppingTop = Canvas.GetTop(RubberBand as AvaloniaObject);
                        croppingWidth = RubberBand.Width;
                        croppingHeight = RubberBand.Height;
                        break;

                    case CroppingType.Rectangle:
                        croppingLeft = Canvas.GetLeft(RubberBand as AvaloniaObject);
                        croppingTop = Canvas.GetTop(RubberBand as AvaloniaObject);
                        croppingWidth = RubberBand.Width;
                        croppingHeight = RubberBand.Height;
                        break;
                }

                _adornerCanvas.Children.Remove(_backgroundRectangle);
                Canvas.SetLeft(_backgroundRectangle, RubberBand.OuterRectLeft);
                Canvas.SetTop(_backgroundRectangle, RubberBand.OuterRectTop);

                int index = _adornerCanvas.Children.IndexOf(RubberBand);
                if (index > 0)
                {
                    _adornerCanvas.Children.Insert(index--, _backgroundRectangle);
                }

                var croppingBounds = new Rect((int)croppingLeft, (int)croppingTop, (int)croppingWidth, (int)croppingHeight);
                //Debug.WriteLine($"Cropping Area: X: {croppingLeft} Y: {croppingTop} Width: {croppingWidth} Height: {croppingHeight}");
                CroppedImage = _image.Source.CreateCroppedBitmap((float)croppingLeft, (float)croppingTop, (float)croppingWidth, (float)croppingHeight, croppingType);
            }
        }

        /// <summary>
        /// rembers the current mouse position
        /// </summary>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            if (string.IsNullOrEmpty(ImagePath) || CanDoPictureModification == false)
            {
                e.Handled = true;
                return;
            }

            if (e.GetCurrentPoint(_adornerCanvas).Properties.IsLeftButtonPressed && _isMouseCaptured == false)
            {
                _mouseLeftDownPoint = e.GetPosition(_adornerCanvas);
                if (_image.Bounds.Contains(_mouseLeftDownPoint.Value) == false)
                {
                    _mouseLeftDownPoint = null;
                    e.Handled = true;
                    return;
                }

                Debug.WriteLine($"mouseLeftDownPoint X: {_mouseLeftDownPoint.Value.X} Y: {_mouseLeftDownPoint.Value.Y} ");
                e.Pointer.Capture(_adornerCanvas);
                _isMouseCaptured = true;
            }
        }

        /// <summary>
        /// calls <see cref="UpdateCroppedImage"/>
        /// </summary>
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            if (string.IsNullOrEmpty(ImagePath))
            {
                return;
            }

            if (_isMouseCaptured == true && RubberBand != null)
            {
                e.Pointer.Capture(null);
                _isMouseCaptured = false;

                UpdateCroppedImage();
            }
        }

        /// <summary>
        /// sets the position of the <see cref="RubberBand"/>
        /// </summary>
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (string.IsNullOrEmpty(ImagePath))
            {
                return;
            }

            if (_isMouseCaptured && _mouseLeftDownPoint.HasValue)
            {
                Point currentPoint = e.GetPosition(_adornerCanvas);

                if (_adornerCanvas.Children.Contains(RubberBand) == false)
                {
                    _adornerCanvas.Children.Add(RubberBand);
                }

                double width = Math.Abs(_mouseLeftDownPoint.Value.X - currentPoint.X);
                double height = Math.Abs(_mouseLeftDownPoint.Value.Y - currentPoint.Y);
                double left = Math.Min(_mouseLeftDownPoint.Value.X, currentPoint.X);
                double top = Math.Min(_mouseLeftDownPoint.Value.Y, currentPoint.Y);

                if (RubberBand.AllowDragOutOfView == false)
                {
                    var bouncedControl = RubberBand.BouncedControl;

                    var controlBounds = bouncedControl.Bounds;

                    var rect = new Rect(new Point(left, top), RubberBand.DesiredSize);

                    if (controlBounds.Contains(rect) == false)
                    {
                        return;
                    }
                }

                RubberBand.Width = width;
                RubberBand.Height = height;

                _backgroundRectangle.Width = RubberBand.DesiredSize.Width;
                _backgroundRectangle.Height = RubberBand.DesiredSize.Height;

                Canvas.SetLeft(RubberBand, left);
                Canvas.SetTop(RubberBand, top);

                Canvas.SetLeft(_backgroundRectangle, left);
                Canvas.SetTop(_backgroundRectangle, top);
            }
        }

        /// <summary>
        /// calls <see cref="UpdateCroppedImage"/>
        /// </summary>
        private void RubberBand_ResizeFinished(object sender, EventArgs e)
        {
            UpdateCroppedImage();

            _backgroundRectangle.Width = RubberBand.DesiredSize.Width;
            _backgroundRectangle.Height = RubberBand.DesiredSize.Height;
        }

        /// <summary>
        /// Loads the image, clear the canvas childrem, resets the mouse position and sets the initial zoomfactor
        /// </summary>
        private void OnImagePathChanged(CroppingControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is string path && File.Exists(path))
            {
                var bitmap = new Bitmap(path);
                _image.Source = bitmap;
                _image.IsVisible = true;

                _adornerCanvas.Children.Clear();
                _adornerCanvas.Width = bitmap.PixelSize.Width;
                _adornerCanvas.Height = bitmap.PixelSize.Height;
                _adornerCanvas.Children.Add(_image);
                _adornerCanvas.IsEnabled = true;

                _contentControl.Content = _adornerCanvas;

                _mouseLeftDownPoint = null;
                CanDoPictureModification = true;
                ZoomFactor = 50.0;
                this.Focus();
            }
        }

        /// <summary>
        /// for updating width and height
        /// </summary>
        private void UpdateAdornerSize()
        {
            if (_image.DesiredSize.Width > 0)
            {
                _adornerCanvas.Width = _image.DesiredSize.Width;
            }

            if (_image.DesiredSize.Height > 0)
            {
                _adornerCanvas.Height = _image.DesiredSize.Height;
            }
        }

        /// <summary>
        /// resolves all needed controls
        /// and initializes the ItemsVms
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _zoomSlider = e.NameScope.Find<Slider>(PART_ZoomSlider);
            _adornerCanvas = new Canvas();
            _contentControl = e.NameScope.Find<ContentControl>(PART_ContentControl);

            Binding binding = null;
            if (RubberBandEllipse == null)
            {
                RubberBandEllipse = new Ellipse
                {
                    Fill = Brushes.Transparent,
                    StrokeThickness = 1,
                    IsVisible = false,
                    Margin = new Thickness(2)
                };

                binding = new Binding();
                binding.Source = this;
                binding.Path = RotateResizeBorderBrushProperty.Name;
                binding.Mode = BindingMode.OneWay;
                RubberBandEllipse.Bind(Ellipse.StrokeProperty, binding);
            }

            binding = new Binding();
            binding.Source = this;
            binding.Path = RotateResizeBackgroundBrushProperty.Name;
            binding.Mode = BindingMode.OneWay;
            _backgroundRectangle.Bind(Rectangle.FillProperty, binding);

            binding = new Binding();
            binding.Source = this;
            binding.Path = BackgroundRectangleOpacityProperty.Name;
            binding.Mode = BindingMode.OneWay;
            _backgroundRectangle.Bind(Rectangle.OpacityProperty, binding);

            binding = new Binding();
            binding.Source = this;
            binding.Path = ShowBackgroundProperty.Name;
            binding.Mode = BindingMode.OneWay;
            _backgroundRectangle.Bind(Rectangle.IsVisibleProperty, binding);

            if (RubberBand == null)
            {
                RubberBand = new ResizeRotateControl();
                //RubberBand.BorderBrush = RotateResizeBorderBrush;
                RubberBand.BouncedControl = _image;
                RubberBand.IsRotationEnabled = false;
                RubberBand.CanResize = true;
                RubberBand.ShowAlwaysSizing = false;
                RubberBand.Content = RubberBandEllipse;

                binding = new Binding();
                binding.Source = this;
                binding.Path = RotateResizeBorderBrushProperty.Name;
                binding.FallbackValue = RotateResizeBorderBrush;
                binding.Mode = BindingMode.TwoWay;

                RubberBand.Bind(ResizeRotateControl.OuterRectStrokeBrushProperty, binding);

                RubberBand.Moved += RubberBand_MoveFinished;
                RubberBand.Resized += RubberBand_ResizeFinished;
            }

            FlipItems = new ObservableCollection<ItemVM>();

            object image = null;
            this.TryFindResource("FlipVertical_Icon", out image);
            FlipItems.Add(_flipVertical = new ItemVM { Text = "Flip Vertical", Image = image as IImage, Data = FipType.Vertical });
            this.TryFindResource("FlipHorizontal_Icon", out image);
            FlipItems.Add(_fliphorizontal = new ItemVM { Text = "Flip Horizontal", Image = image as IImage, Data = FipType.Horizontal });

            CroppingTypes = new ObservableCollection<ItemVM>();
            this.TryFindResource("Rectangle_Icon", out image);
            CroppingTypes.Add(_croppingTypeRectangle = new ItemVM { Text = "Rectangle", Image = image as IImage, Data = CroppingType.Rectangle });
            this.TryFindResource("Circle_Icon", out image);
            CroppingTypes.Add(_croppingTypeCircle = new ItemVM { Text = "Circle", Image = image as IImage, Data = CroppingType.Circle });
            CurrentCroppingType = CroppingTypes.FirstOrDefault();
            CurrentCroppingTypeProperty.Changed.AddClassHandler<CroppingControl>((o, e) => OnCurrentCroppingTypeChanged(o, e));

            RotateItems = new ObservableCollection<ItemVM>();
            this.TryFindResource("RotateLeft_Icon", out image);
            RotateItems.Add(_rotateLeftItem = new ItemVM { Text = "Rotate Left", Image = image as IImage, Data = RotateType.LeftHandedRotation });
            this.TryFindResource("RotateRight_Icon", out image);
            RotateItems.Add(_rotateRightItem = new ItemVM { Text = "Rotate Right", Image = image as IImage, Data = RotateType.RightHandedRotation });
        }
    }
}
