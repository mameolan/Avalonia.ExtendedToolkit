using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public class FlyoutDemo : UserControl
    {
        private MetroWindow _metroWindow;

        public FlyoutDemo()
        {
            this.InitializeComponent();

            Button btn=this.FindControl<Button>("btnShowFirst");
            btn.Click += BtnShowFirst_Click;
            
        }

        private void BtnShowFirst_Click(object sender, Interactivity.RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index)
        {

            if(_metroWindow==null)
            {


                _metroWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow as MetroWindow; 
            }


            var flyout = _metroWindow.Flyouts.Items.OfType<Flyout>().ToList()[index];

            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
