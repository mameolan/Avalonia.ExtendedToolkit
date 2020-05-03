using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;

namespace Avalonia.ExampleApp.Model.PropertyGrid_DialogEditor
{
    public class BusinessObject : AvaloniaObject
    {


        public static readonly DirectProperty<BusinessObject, string> NameProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, string>(
                    nameof(Name),
                    o => o.Name);

        private string _name;

        [PropertyOrder(0)]
        public string Name
        {
            get { return _name; }
            set { SetAndRaise(NameProperty, ref _name, value); }
        }

        [PropertyOrder(1)]
        public string Description { get; }



        public static readonly DirectProperty<BusinessObject, string> DescriptionExProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, string>(
                    nameof(DescriptionEx),
                    o => o.DescriptionEx);

        private string _DescriptionEx;

        [PropertyOrder(2)]
        public string DescriptionEx
        {
            get { return _DescriptionEx; }
            set { SetAndRaise(DescriptionExProperty, ref _DescriptionEx, value); }
        }



        public static readonly DirectProperty<BusinessObject, string> PhotoPathProperty =
                AvaloniaProperty.RegisterDirect<BusinessObject, string>(
                    nameof(PhotoPath),
                    o => o.PhotoPath);

        private string _PhotoPath;

        [PropertyEditor(typeof(FilePathPicker))]
        [PropertyOrder(3)]
        public string PhotoPath
        {
            get { return _PhotoPath; }
            set { SetAndRaise(PhotoPathProperty, ref _PhotoPath, value); }
        }





    }
}
