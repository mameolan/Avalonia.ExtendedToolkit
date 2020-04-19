using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ReactiveUI;

namespace Avalonia.ExampleApp.Model
{
    public class Human : ReactiveObject
    {
        private string _name= "John";

        [NotifyParentProperty(true)]
        public string Name
        {
            get { return _name; }
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);

            }
        }

        private string _surname= "Doe";

        public string Surname
        {
            get { return _surname; }
            set
            {
                this.RaiseAndSetIfChanged(ref _surname, value);
            }
        }




    }
}
