using Avalonia;
using Avalonia.Controls;
using Avalonia.ExampleApp.Model.PropertyGrid_DialogEditor;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class PropertyGridExample_DialogEditor : UserControl
    {
        public PropertyGridExample_DialogEditor()
        {
            this.InitializeComponent();

            //PropertyGrid propertyGrid = this.Find<PropertyGrid>("propertyGrid");
            //propertyGrid.SelectedObject = new BusinessObject();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
