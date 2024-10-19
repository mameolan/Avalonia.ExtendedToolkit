using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// should be remove if avalonia supports naming of rowdefinition
    /// </summary>
    public class RowDefinitionExt: RowDefinition
    {
        /// <summary>
        /// get/set Name
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// <see cref="Name"/>
        /// </summary>
        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<RowDefinitionExt, string>(nameof(Name));
    }
}
