using System;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Avalonia.Controlz.Controls
{
    /// <summary>
    /// GroupBox Control class
    /// </summary>
    public class GroupBox : HeaderedContentControl
    {
        /// <summary>
        /// override some metadata:
        /// Focusable: false
        /// TabNavigation: None
        /// </summary>
        static GroupBox()
        {
            FocusableProperty.OverrideMetadata<GroupBox>(new StyledPropertyMetadata<bool>(false));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata<GroupBox>
                (new StyledPropertyMetadata<KeyboardNavigationMode>(KeyboardNavigationMode.None));
        }
    }
}
