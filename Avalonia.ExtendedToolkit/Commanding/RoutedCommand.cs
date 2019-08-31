using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Commanding
{
    public class RoutedCommand: RoutedEvent, ICommand
    {
        private event EventHandler CanExecuteChangedInternal;
        public Action<object> execute;
        public Predicate<object> canExecute;


        public RoutedCommand(string name, RoutingStrategies routingStrategies, Type eventArgsType, Type ownerType)
            :base(name, routingStrategies, eventArgsType, ownerType)
        {

        }

        public RoutedCommand(string name, RoutingStrategies routingStrategies,  Type ownerType)
            : base(name, routingStrategies, typeof(RoutedEventArgs), ownerType)
        {
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }
    }
}
