using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class TabControlExample : UserControl
    {
        
        public TabControlExample()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
