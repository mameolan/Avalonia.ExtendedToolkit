using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;


namespace Avalonia.ExampleApp.Views
{
    public class ResizeRotateControlDemoView : UserControl
    {

        public ResizeRotateControlDemoView()
        {
            InitializeComponent();
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
