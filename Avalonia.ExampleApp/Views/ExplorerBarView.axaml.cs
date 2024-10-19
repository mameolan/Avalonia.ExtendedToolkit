using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;


namespace Avalonia.ExampleApp.Views
{
    public partial class ExplorerBarView : UserControl
    {
        private BreadcrumbBar _breadcrumbBar;

        public ExplorerBarView()
        {
            this.InitializeComponent();

            //this.Find<Button>("btnBreadAnimate").Click += RefreshClick;
            //_breadcrumbBar = this.Find<BreadcrumbBar>("breadcrumb");

            //_breadcrumbBar.PopulateItems += BreadcrumbBar_PopulateItems;
        }

        private void BreadcrumbBar_PopulateItems(object sender, BreadcrumbItemEventArgs e)
        {
            List<BreadcrumbItem> items = e.Item.Items.OfType<BreadcrumbItem>().ToList();
            if(items.Count==0)
            {
                var trace = e.Item.Header;
                items.Add(BreadcrumbItem.CreateItem(trace));
                //.edited
                e.Item.ItemsSource = items;
            }
            
        }

        private void RefreshClick(object sender, Interactivity.RoutedEventArgs e)
        {
            



        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
