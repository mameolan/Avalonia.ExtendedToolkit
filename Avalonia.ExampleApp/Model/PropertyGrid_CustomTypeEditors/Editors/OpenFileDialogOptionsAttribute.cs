using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace Avalonia.ExampleApp.Model
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OpenFileDialogOptionsAttribute : Attribute
    {
        public bool AllowMultiple { get; set; }
        public string Directory { get; set; }
        public List<FileDialogFilter> Filters { get; set; }
        public string InitialFileName { get; set; }
        public string Title { get; set; }
        public string DefaultExt { get; set; }

        public OpenFileDialogOptionsAttribute(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                throw new ArgumentNullException("filter");

            string[] filters = filter.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            string filtername = string.Empty;
            string correctedFilter = "*";
            
            if(filters.Length>1)
            {
                filtername = filters.First();
                correctedFilter = filters.Last().Replace("*.", string.Empty);
            }
            else
            {
                filtername= correctedFilter = filters.FirstOrDefault()?.Replace("*.", string.Empty);
            }


            ReadConfiguration(null);
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name=filtername,
                    Extensions =new List<string> { correctedFilter }
                }
            };
        }

        public bool HasTitle
        {
            get { return !string.IsNullOrEmpty(Title); }
        }

        public bool HasDefaultExt
        {
            get { return !string.IsNullOrEmpty(DefaultExt); }
        }

        public virtual void ReadConfiguration(OpenFileDialog dialog)
        {
            if (dialog == null)
                dialog = new OpenFileDialog();

            AllowMultiple = dialog.AllowMultiple;
            Directory = dialog.Directory;
            Filters = dialog.Filters;
            InitialFileName = dialog.InitialFileName;
            Title = dialog.Title;
        }

        public virtual void ConfigureDialog(OpenFileDialog dialog)
        {
            if (dialog == null)
                dialog = new OpenFileDialog();

            dialog.AllowMultiple = AllowMultiple;
            dialog.Directory = Directory;
            dialog.Filters = Filters;
            dialog.InitialFileName = InitialFileName;
            if (HasTitle)
                dialog.Title = Title;
        }
    }
}
