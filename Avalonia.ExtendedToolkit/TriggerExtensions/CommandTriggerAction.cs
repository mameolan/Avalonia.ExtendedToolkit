using System;
using System.Windows.Input;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.TriggerExtensions
{
    /// <summary>
    /// This CommandTriggerAction can be used to bind any event on any FrameworkElement to an <see cref="ICommand" />.
    /// This trigger can only be attached to a FrameworkElement or a class deriving from FrameworkElement.
    ///
    /// This class is inspired from Laurent Bugnion and his EventToCommand.
    /// <web>http://www.mvvmlight.net</web>
    /// <license> See license.txt in this solution or http://www.galasoft.ch/license_MIT.txt </license>
    /// </summary>
    public class CommandTriggerAction :  TriggerAction<InputElement>
    {
        /// <summary>
        /// get/set command
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Command"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<CommandTriggerAction, ICommand>(nameof(Command));

        /// <summary>
        /// get/sets CommandParameter
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// <see cref="CommandParameter"/>
        /// </summary>
        public static readonly StyledProperty<object> CommandParameterProperty =
            AvaloniaProperty.Register<CommandTriggerAction, object>(nameof(CommandParameter));

        /// <summary>
        /// registers <see cref="CommandParameter"/>
        /// and <see cref="Command"/> changed event
        /// </summary>
        public CommandTriggerAction()
        {
            CommandParameterProperty.Changed.AddClassHandler<CommandTriggerAction>((o, e) => OnCommandParameterChanged(o, e));
            CommandProperty.Changed.AddClassHandler<CommandTriggerAction>((o, e) => OnCommandChanged(o, e));
        }

        private void OnCommandChanged(CommandTriggerAction commandTriggerAction, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= commandTriggerAction.OnCommandCanExecuteChanged;
            }

            var command = (ICommand)e.NewValue;
            if (command != null)
            {
                command.CanExecuteChanged += commandTriggerAction.OnCommandCanExecuteChanged;
            }

            commandTriggerAction.EnableDisableElement();
        }

        private void OnCommandParameterChanged(CommandTriggerAction commandTriggerAction, AvaloniaPropertyChangedEventArgs e)
        {
            commandTriggerAction.EnableDisableElement();
        }

        /// <summary>
        /// calls EnableDisableElement
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
           this.EnableDisableElement();
        }

        /// <summary>
        /// invokes the command by parameter
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject == null || (this.AssociatedObject != null && !this.AssociatedObject.IsEnabled))
            {
                return;
            }

            var command = this.Command;
            if (command != null)
            {
                var commandParameter = this.GetCommandParameter();
                if (command.CanExecute(commandParameter))
                {
                    command.Execute(commandParameter);
                }
            }
        }

        /// <summary>
        /// return the <see cref="CommandParameter"/> if not null
        /// or returns <see cref="TriggerAction.AssociatedObject"/>
        /// </summary>
        /// <returns></returns>
        protected virtual object GetCommandParameter()
        {
            return this.CommandParameter ?? this.AssociatedObject;
        }

        private void EnableDisableElement()
        {
            if (this.AssociatedObject == null)
            {
                return;
            }

            var command = this.Command;
            this.AssociatedObject.SetValue(InputElement.IsEnabledProperty, command == null || command.CanExecute(this.GetCommandParameter()));
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.EnableDisableElement();
        }
    }
}
