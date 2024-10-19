using Avalonia;
using Avalonia.Controls;
using Avalonia.ExampleApp.Model.PropertyGrid_Proxy;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class PropertyGridExample_ProxyExample : UserControl
    {
        public PropertyGridExample_ProxyExample()
        {
            this.InitializeComponent();

            PropertyGrid propertyGrid = this.Find<PropertyGrid>("propertyGrid");
            TextBox textBox = this.Find<TextBox>("TargetToProxy");

            propertyGrid.SelectedObject = new TextBoxProxy(textBox);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
