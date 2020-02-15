using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// should be remove if avalonia supports naming of rowdefinition
    /// </summary>
    public class RowDefinitionExt:RowDefinition
    {
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly AvaloniaProperty<string> NameProperty =
            AvaloniaProperty.Register<RowDefinitionExt, string>(nameof(Name));
    }
}