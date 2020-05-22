using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Specifies a property name presenter.
    /// </summary>
    public sealed class PropertyNameTextBlock : TextBox
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(PropertyNameTextBlock);

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNameTextBlock"/> class.
        /// </summary>
        public PropertyNameTextBlock()
        {
            IsReadOnly = true;
            //TextTrimming = TextTrimming.CharacterEllipsis;
            //TextWrapping = Media.TextWrapping.NoWrap;
            TextAlignment = Media.TextAlignment.Right;
            HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
            VerticalAlignment = Layout.VerticalAlignment.Center;
            ClipToBounds = true;

            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.None);
        }
    }
}
