using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// should be remove if avalonia supports naming of ColumnDefinition
    /// </summary>
    public class ColumnDefinitionExt: ColumnDefinition
    {
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly AvaloniaProperty<string> NameProperty =
            AvaloniaProperty.Register<ColumnDefinitionExt, string>(nameof(Name));
    }
}