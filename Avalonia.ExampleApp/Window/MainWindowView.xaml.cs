using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public class MainWindowView : UserControl
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
