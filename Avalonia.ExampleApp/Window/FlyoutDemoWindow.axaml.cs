using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Avalonia.ExampleApp.Window
{
    public partial class FlyoutDemoWindow : MetroWindow
    {
        public new Type StyleKey => typeof(MetroWindow);


        public FlyoutDemoWindow()
        {
            this.InitializeComponent();
            Button btn = this.FindControl<Button>("btnShowFirst");
            btn.Click += BtnShowFirst_Click;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void BtnShowFirst_Click(object sender, Interactivity.RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index)
        {

            //if (_metroWindow == null)
            //{
            //    _metroWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow as MetroWindow;
            //}


            var flyout = Flyouts.Items.OfType<ExtendedToolkit.Controls.Flyout>().ToList()[index];

            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;

        }


    }
}
