using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// button with special properties
    /// </summary>
    public class MetroButton : Button
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(MetroButton);

        /// <summary>
        /// get set ContentCharacterCasing
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
            AvaloniaProperty.Register<MetroButton, CharacterCasing>(nameof(ContentCharacterCasing));

        /// <summary>
        /// get set corner radius
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
            AvaloniaProperty.Register<MetroButton, CornerRadius>(nameof(CornerRadius));

        /// <summary>
        /// get/set FocusBorderBrush
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
            AvaloniaProperty.Register<MetroButton, IBrush>(nameof(FocusBorderBrush));

        /// <summary>
        /// get/set FocusBorderThickness
        /// </summary>
        public Thickness FocusBorderThickness
        {
            get { return (Thickness)GetValue(FocusBorderThicknessProperty); }
            set {  SetValue(FocusBorderThicknessProperty, value); }
        }

        /// <summary>
        /// <see cref="FocusBorderThickness"/>
        /// </summary>
        public static readonly StyledProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.Register<MetroButton, Thickness>(nameof(FocusBorderThickness));
    }
}
