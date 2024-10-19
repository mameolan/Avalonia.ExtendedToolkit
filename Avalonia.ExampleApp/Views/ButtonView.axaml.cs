using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class ButtonView : UserControl
    {
        private Badged _badged;
        private int _clickCounter = 1;


        public ButtonView()
        {
            this.InitializeComponent();
            _badged = this.FindControl<Badged>("CountingBadge");
            this.FindControl<Button>("btnClickMe").Click += (o, e) => 
            {
                _badged.Badge = _clickCounter++;
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
