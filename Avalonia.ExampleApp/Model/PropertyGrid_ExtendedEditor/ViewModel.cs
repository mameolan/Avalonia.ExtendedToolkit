using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Avalonia.ExampleApp.Model.PropertyGrid_ExtendedEditor
{
    public class ViewModel : ReactiveObject
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { this.RaiseAndSetIfChanged(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { this.RaiseAndSetIfChanged(ref _lastName, value); }
        }

        private string _details;

        public string Details
        {
            get { return _details; }
            set { this.RaiseAndSetIfChanged(ref _details, value); }
        }


    }
}
