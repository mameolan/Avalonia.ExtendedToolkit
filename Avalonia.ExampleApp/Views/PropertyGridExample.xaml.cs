using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExampleApp.Model;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Views
{
    public class PropertyGridExample : UserControl
    {
        private PropertyGrid _propertyGrid;

        public PropertyGridExample()
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

    /// <summary>
    /// Sample dataset for brushes. 
    /// You should really use a custom brush editor (e.g. from Telerik) but it all depends on what your aims are, of course.
    /// </summary>
    public class BrushList : ObservableCollection<string>
    {

        public BrushList()
        {

            Add("#FFFFFFFF");
            Add("#FF000000");
            Add("#FF142233");
            Add("#FFABEF14");

        }

    }

}
