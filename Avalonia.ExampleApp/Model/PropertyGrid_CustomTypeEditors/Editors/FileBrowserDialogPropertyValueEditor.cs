using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ExampleApp.Model.PropertyGrid_CustomTypeEditors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;

namespace Avalonia.ExampleApp.Model
{
    public class FileBrowserDialogPropertyValueEditor : Editor
    {
        public FileBrowserDialogPropertyValueEditor()
        {
            //little ugly search for the resource
            var mainWindow = ApplicationExtension.GetMainWindow();

            if(mainWindow!=null)
            {
                object res = null;
                foreach(var item in mainWindow.FindChildren<UserControl>(true))
                {
                    if (item.TryFindResource(LocalResources.FileBrowserEditorKey, out res))
                        break;
                }


                InlineTemplate = res;


            }
        }

        

        public override void ShowDialog(PropertyItemValue propertyValue, IInputElement commandSource)
        {
            if (propertyValue == null)
                return;
            if (propertyValue.ParentProperty.IsReadOnly)
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AllowMultiple = false;

            var property = propertyValue.ParentProperty;
            if (property != null)
            {
                var optionsAttribute = (OpenFileDialogOptionsAttribute)property.Attributes[typeof(OpenFileDialogOptionsAttribute)];
                if (optionsAttribute != null)
                    optionsAttribute.ConfigureDialog(openFileDialog);
            }

            var mainWindow = ApplicationExtension.GetMainWindow();

            openFileDialog.ShowAsync(mainWindow).ContinueWith(x => 
            {
                if(x.IsFaulted==false)
                {

                    string result = x.Result.FirstOrDefault();

                    if (string.IsNullOrEmpty(result) == false)
                        propertyValue.StringValue = result;
                }


                

            }, TaskScheduler.FromCurrentSynchronizationContext());

            
        }
    }
}
