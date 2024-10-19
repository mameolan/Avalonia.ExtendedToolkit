using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// An image control which sets the <see cref="GeometryFillBrush"/>
    /// to a <see cref="DrawingImage"/> geometries.
    /// </summary>
    public class ImageExt : Image
    {
        /// <summary>
        /// Style key for this control
        /// </summary>
        public Type StyleKey => typeof(Image);

        /// <summary>
        /// Gets or sets Foreground.
        /// </summary>
        public IBrush GeometryFillBrush
        {
            get { return (IBrush)GetValue(GeometryFillBrushProperty); }
            set { SetValue(GeometryFillBrushProperty, value); }
        }

        /// <summary>
        /// Defines the Foreground property.
        /// </summary>
        public static readonly StyledProperty<IBrush> GeometryFillBrushProperty =
        AvaloniaProperty.Register<ImageExt, IBrush>(nameof(GeometryFillBrush));

        static ImageExt()
        {
            GeometryFillBrushProperty.Changed.AddClassHandler<ImageExt>((o, e) => OnGeometryFillBrushChanged(o, e));
            //.obsolete, OnSourceChanged not needed, GeometryFillBrushProperty is styled now
            //SourceProperty.Changed.AddClassHandler<ImageExt>((o, e) => OnSourceChanged(o, e));
        }

        //private static void OnSourceChanged(ImageExt o, object e)
        //{
        //.obsolete
        //o.RaisePropertyChanged<IBrush>(GeometryFillBrushProperty, null, o.GeometryFillBrush);
        //}

        /// <summary>
        /// updates the brush of the geometry
        /// </summary>
        private static void OnGeometryFillBrushChanged(ImageExt o, AvaloniaPropertyChangedEventArgs e)
        {
            var brush = e.NewValue as IBrush;
            var drawingImage = o.Source as DrawingImage;

            if (drawingImage != null && brush != null)
            {
                if (drawingImage.Drawing is DrawingGroup group)
                {
                    foreach (var item in group.Children)
                    {
                        if (item is GeometryDrawing geometryDrawing)
                        {
                            geometryDrawing.Brush = brush;
                        }
                        else if (item is GlyphRunDrawing glyphRunDrawing)
                        {
                            glyphRunDrawing.Foreground = brush;
                        }
                        else
                        {
                            Debug.WriteLine($"The type {item} needs to be added to the {nameof(ImageExt)} control. If a brush property is available.");
                        }
                    }
                }
            }

            //refresh must be done overwise the old color is still displayed somehow.
            o.InvalidateVisual();
        }
    }
}
