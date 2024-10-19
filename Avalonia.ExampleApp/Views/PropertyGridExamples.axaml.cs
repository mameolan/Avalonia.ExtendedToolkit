using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExampleApp.Model;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Views
{
    public partial class PropertyGridExamples : UserControl
    {
        public PropertyGridExamples()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    

}
