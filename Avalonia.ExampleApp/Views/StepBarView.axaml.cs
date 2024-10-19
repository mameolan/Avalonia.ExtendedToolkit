using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class StepBarView : UserControl
    {
        public StepBarView()
        {
            this.InitializeComponent();
        }

        
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}
