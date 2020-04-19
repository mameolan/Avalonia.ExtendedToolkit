using System.ComponentModel;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Model
{
    /// <summary>
    /// Sample multi-brush object.
    /// </summary>
    public class MultiBrushObject1:AvaloniaObject
    {
        /// <summary>
        /// Gets or sets the BorderBrush property.  This dependency property
        /// indicates ....
        /// </summary>
        [Category("Brushes")]
        public IBrush BorderBrush
        {
            get { return (IBrush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> BorderBrushProperty =
            AvaloniaProperty.Register<MultiBrushObject1, IBrush>(nameof(BorderBrush));

        /// <summary>
        /// Gets or sets the Background property.  This dependency property
        /// indicates ....
        /// </summary>
        [Category("Brushes")]
        public IBrush Background
        {
            get { return (IBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> BackgroundProperty =
            AvaloniaProperty.Register<MultiBrushObject1, IBrush>(nameof(Background));
    }
}
