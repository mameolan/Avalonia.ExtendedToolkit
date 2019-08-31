using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Behaviours
{
    public class ButtonBehaviour: Behavior<Button>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.PropertyChanged += Button_PropertyChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PropertyChanged -= Button_PropertyChanged;
            base.OnDetaching();
        }


        private void Button_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.Property.Name==nameof(AssociatedObject.Command))
            {
                if(e.NewValue is ICommand)
                {
                    AssociatedObject.Command.CanExecuteChanged += Command_CanExecuteChanged;
                }

            }
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            bool value= AssociatedObject.Command.CanExecute(null);

            AssociatedObject.IsEnabled = value;
        }
    }
}
