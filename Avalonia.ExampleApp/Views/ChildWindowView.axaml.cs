using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class ChildWindowView : UserControl
    {
        private ChildWindow _child01;
        TestChildWindow _testWindow;

        public ChildWindowView()
        {
            this.InitializeComponent();

            _child01 = this.FindControl<ChildWindow>("child01");
            _child01.Closing += Child01_OnClosing;

            _testWindow = this.FindControl<TestChildWindow>("testView");


            this.FindControl<Button>("btnFirstTest").Click += FirstTest_OnClick;
            this.FindControl<Button>("btnCloseMe").Click += CloseMeButton_Click;
            this.FindControl<Button>("btnSecTest").Click += SecTest_OnClick;

        }

        private void SecTest_OnClick(object sender, RoutedEventArgs e)
        {
            _testWindow.Child.IsOpen = true;
        }

        private void CloseMeButton_Click(object sender, RoutedEventArgs e)
        {
            _child01.IsOpen = false;
        }
        private void FirstTest_OnClick(object sender, RoutedEventArgs e)
        {
            _child01.IsOpen = !_child01.IsOpen;
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Child01_OnClosing(object sender, CancelEventArgs e)
        {
            //e.Cancel = true; // don't close
        }



    }
}
