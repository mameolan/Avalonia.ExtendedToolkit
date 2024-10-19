using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExampleApp.Model;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Views
{
    public partial class PropertyGridExample_BrushCategory : UserControl
    {
        private PropertyGrid _propertyGrid;

        public PropertyGridExample_BrushCategory()
        {
            this.InitializeComponent();

            _propertyGrid = this.Find<PropertyGrid>("propertyGrid");
            _propertyGrid.SelectedObject = new BrushProxy(new MultiBrushObject1 { Background = Brushes.DarkCyan, BorderBrush = Brushes.Violet });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    

}
