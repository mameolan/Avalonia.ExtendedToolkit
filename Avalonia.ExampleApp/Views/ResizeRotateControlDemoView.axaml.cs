using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;


namespace Avalonia.ExampleApp.Views
{
    public partial class ResizeRotateControlDemoView : UserControl
    {

        public ResizeRotateControlDemoView()
        {
            InitializeComponent();
            this.Find<ResizeRotateControl>("resizeRotateControl").PositionChanged+=(o,e)=>
            {
                Console.WriteLine(e);
            };
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
