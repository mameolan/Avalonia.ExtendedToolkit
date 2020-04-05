using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Specifies a property name presenter.
    /// </summary>
    public sealed class PropertyNameTextBlock : TextBlock
    {
        public Type StyleKey => typeof(PropertyNameTextBlock);

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNameTextBlock"/> class.
        /// </summary>
        public PropertyNameTextBlock()
        {
            //TextTrimming = TextTrimming.CharacterEllipsis;
            TextWrapping = Media.TextWrapping.NoWrap;
            TextAlignment = Media.TextAlignment.Right;
            VerticalAlignment = Layout.VerticalAlignment.Center;
            ClipToBounds = true;

            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.None);
        }
    }
}
