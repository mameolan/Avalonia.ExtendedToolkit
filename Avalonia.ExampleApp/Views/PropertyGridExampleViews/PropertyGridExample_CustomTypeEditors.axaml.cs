using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ExampleApp.Model.PropertyGrid_CustomTypeEditors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExampleApp.Views
{
    public partial class PropertyGridExample_CustomTypeEditors : UserControl
    {
        static Random random = new Random();
        private CategoryItem _tempItem;
        private int _tempIndex;
        readonly BusinessObject bo;
        private readonly PropertyGrid propertyGrid1;

        public PropertyGridExample_CustomTypeEditors()
        {
            this.InitializeComponent();

            bo = new BusinessObject();

            propertyGrid1 = this.Find<PropertyGrid>("propertyGrid");
            propertyGrid1.SelectedObjectsChanged += PropertyGrid_SelectedObjectsChanged;
            propertyGrid1.SelectedObject = bo;
            propertyGrid1.PropertyValueChanged += PropertyGrid_PropertyValueChanged;

            Button btnTest = this.Find<Button>("btnTest");
            btnTest.Click += BtnTest_Click;

            Button btnSwitchObjects = this.Find<Button>("btnSwitchObjects");
            btnSwitchObjects.Click += BtnSwitchObjects_Click;

            Button btnSelectMultiple = this.Find<Button>("btnSelectMultiple");
            btnSelectMultiple.Click += BtnSelectMultiple_Click;


        }

        private void BtnSelectMultiple_Click(object sender, Interactivity.RoutedEventArgs e)
        {
            object[] objects = new object[]
            {
                new BusinessObject { Name = "AC DC", Integer1 = 10 },
                new BusinessObject { Name = "Marilyn Manson", Integer1 = 10 },
                new BusinessObject { Name = "Charles Darvin", Integer1 = 10 }
            };

            this.propertyGrid1.SelectedObjects = objects;
        }

        private void BtnSwitchObjects_Click(object sender, Interactivity.RoutedEventArgs e)
        {
            propertyGrid1.SelectedObject = new BusinessObject
            {
                Name = Path.GetRandomFileName(),
                Password = Path.GetRandomFileName(),
                RegisteredDate = DateTime.Now,
                Integer1 = random.Next(1000),
                Integer2 = random.Next(1000),
                Integer3 = random.Next(1000),
                Integer4 = random.Next(1000),
                Attachment = Path.GetRandomFileName()
            };
        }

        private void BtnTest_Click(object sender, Interactivity.RoutedEventArgs e)
        {
            PropertyItem prop = propertyGrid1.Properties["Name"];
            if (prop != null)
            {
                //workaround because setting isbrowsable is not working right now
                var result= propertyGrid1.Categories.FirstOrDefault(x => x.Properties.Contains(prop));

                if(result!=null)
                {
                    _tempItem = result;
                    _tempIndex = propertyGrid1.Categories.IndexOf(result);
                    propertyGrid.Categories.Remove(result);
                }
                else 
                {
                    propertyGrid.Categories.Insert(_tempIndex, _tempItem);
                }

                

                //prop.IsReadOnly = !prop.IsReadOnly;
                //prop.IsReadOnly = !prop.IsReadOnly;

                // propertyGrid.ReloadCommand.Execute(null);
            }
        }

        private void PropertyGrid_PropertyValueChanged(object sender, System.EventArgs e)
        {
            
        }

        private void PropertyGrid_SelectedObjectsChanged(object sender, System.EventArgs e)
        {
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
