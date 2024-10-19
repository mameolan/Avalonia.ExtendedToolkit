using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class SplitViewExamples : UserControl
    {
        private ExtendedToolkit.Controls.SplitView _splitView;

        public SplitViewExamples()
        {
            this.InitializeComponent();

            _splitView= this.FindControl<ExtendedToolkit.Controls.SplitView>("SimpleSplitview"); /*Splitview_PaneClosing*/
            // The Tag is used to handle closing
            _splitView.Tag = false;
            _splitView.PaneClosing += SplitView_PaneClosing;

        }

        private void SplitView_PaneClosing(object sender, ExtendedToolkit.Controls.SplitViewPaneClosingEventArgs e)
        {
            var splitView = sender as ExtendedToolkit.Controls.SplitView;

            if (splitView == null)
                return;

            e.Cancel = (bool)splitView.Tag;


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
