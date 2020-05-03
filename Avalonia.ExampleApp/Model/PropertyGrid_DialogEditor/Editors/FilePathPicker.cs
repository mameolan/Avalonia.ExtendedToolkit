using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Input;

namespace Avalonia.ExampleApp.Model
{
    public class FilePathPicker : Editor
    {
        public FilePathPicker()
        {
            //little ugly search for the resource
            var mainWindow = ApplicationExtension.GetMainWindow();

            if (mainWindow != null)
            {
                object res = null;
                foreach (var item in mainWindow.FindChildren<UserControl>(true))
                {
                    if (item.TryFindResource(LocalResources.FilePathPickerEditorKey, out res))
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

            openFileDialog.Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter()
                {
                    Name="Image Files (*.jpg, *.png, *.bmp)"
                    , Extensions=new List<string>(){"jpg", "png", "bmp" } 
                }
            
            };

            var mainWindow = ApplicationExtension.GetMainWindow();

            openFileDialog.ShowAsync(mainWindow).ContinueWith(x =>
            {
                if (x.IsFaulted == false)
                {

                    string result = x.Result.FirstOrDefault();

                    if (string.IsNullOrEmpty(result) == false)
                        propertyValue.StringValue = result;
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());



        }



    }
}
