using ReactiveUI;

namespace Avalonia.ExampleApp.Model
{
    public class ComplexProperty : ReactiveObject
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private int _age;

        public int Age
        {
            get { return _age; }
            set
            {
                this.RaiseAndSetIfChanged(ref _age, value);
            }
        }

        public override string ToString()
        {
            return (string.IsNullOrEmpty(_name))
                        ? "(noname)"
                        : _name;
        }
    }
}
