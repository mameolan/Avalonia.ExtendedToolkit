using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp
{
    public class PropertyGridExample_CustomTypeEditors : UserControl
    {
        public PropertyGridExample_CustomTypeEditors()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
