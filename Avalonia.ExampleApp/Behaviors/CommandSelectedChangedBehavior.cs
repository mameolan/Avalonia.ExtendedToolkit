using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.ExampleApp.Behaviors
{
    public class CommandSelectedChangedBehavior : Behavior<ComboBox>
    {
        public object Command { get; set; }

        public object CommandParameter { get; set; }

        protected override void OnAttached()
        {
            this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICommand command = Command as ICommand;

            if (command != null && command.CanExecute(CommandParameter))
            {
                command.Execute(CommandParameter);
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }
    }
}
