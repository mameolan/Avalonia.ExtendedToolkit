using System;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// toggle button with special properties
    /// </summary>
    public class MetroToggleButton : ToggleButton
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(MetroToggleButton);

        /// <summary>
        /// get/sets corner radius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// <see cref="CornerRadius"/>
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<MetroToggleButton, CornerRadius>(nameof(CornerRadius));

        /// <summary>
        /// get7sets FocusBorderThickness
        /// </summary>
        public Thickness FocusBorderThickness
        {
            get { return (Thickness)GetValue(FocusBorderThicknessProperty); }
            set { SetValue(FocusBorderThicknessProperty, value); }
        }

        /// <summary>
        /// <see cref="FocusBorderThickness"/>
        /// </summary>
        public static readonly StyledProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.Register<MetroToggleButton, Thickness>(nameof(FocusBorderThickness));

        /// <summary>
        /// get/sets FocusBorderBrush
        /// </summary>
        public IBrush FocusBorderBrush
        {
            get { return (IBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="FocusBorderBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroToggleButton, IBrush>(nameof(FocusBorderBrush));

        /// <summary>
        /// gets/sets ContentCharacterCasing
        /// </summary>
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentCharacterCasing"/>
        /// </summary>
        public static readonly StyledProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<MetroToggleButton, CharacterCasing>(nameof(ContentCharacterCasing));
    }
}
