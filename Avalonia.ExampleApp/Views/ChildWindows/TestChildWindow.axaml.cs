using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class TestChildWindow : UserControl
    {
        public ChildWindow Child{get;}

        public TestChildWindow()
        {
            this.InitializeComponent();
            this.ApplyTemplate();

            this.FindControl<Button>("btnCloseSec").Click += CloseSec_OnClick;

            Child=this.FindControl<ChildWindow>("child");
        }

        private void CloseSec_OnClick(object sender, EventArgs args)
        {
            Child.Close();
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
