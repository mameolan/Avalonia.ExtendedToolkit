using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public class ChildWindowView : UserControl
    {
        public Grid RootGrid { get; }

        private ChildWindow child01;

        public ChildWindowView()
        {
            this.InitializeComponent();
            RootGrid = this.FindControl<Grid>("RootGrid");
            child01 = this.FindControl<ChildWindow>("child01");
            child01.Closing += Child01_OnClosing;

            this.FindControl<Button>("btnFirstTest").Click += FirstTest_OnClick;
            this.FindControl<Button>("btnCloseMe").Click += CloseMeButton_Click;
            this.FindControl<Button>("btnSecTest").Click += SecTest_OnClick;


        }

        private async void SecTest_OnClick(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button button)
            {
                MetroWindow metroWindow = this.TryFindParent<MetroWindow>();

                var testWindow= new TestChildWindow();
                
                // This dialog should be displayed only once!
                // So disable the button while the dialog is open.
                button.IsEnabled = false;
                await metroWindow.ShowChildWindowAsync(testWindow, this.RootGrid);

                //this.RootGrid.Children.Add(testWindow);
                //testWindow.IsOpen = true;


                button.IsEnabled = true;
            }
        }

        private void CloseMeButton_Click(object sender, RoutedEventArgs e)
        {
            this.child01.IsOpen = false;
        }

        private void FirstTest_OnClick(object sender, RoutedEventArgs e)
        {
            this.child01.IsOpen=!this.child01.IsOpen;
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
