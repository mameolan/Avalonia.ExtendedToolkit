using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    internal static class CommandHelpers
    {
        internal static bool CanExecuteCommandSource(ICommandSource commandSource)
        {
            var command = commandSource.Command;
            if (command == null)
            {
                return false;
            }
            var commandParameter = commandSource.CommandParameter ?? commandSource;
            //var routedCommand = command as RoutedCommand;
            //if (routedCommand != null)
            //{
            //    var target = commandSource.CommandTarget ?? commandSource as IInputElement;
            //    return routedCommand.CanExecute(commandParameter, target);
            //}
            return command.CanExecute(commandParameter);
        }

        internal static void ExecuteCommandSource(ICommandSource commandSource)
        {
            CriticalExecuteCommandSource(commandSource);
        }

        internal static void CriticalExecuteCommandSource(ICommandSource commandSource)
        {
            var command = commandSource.Command;
            if (command == null)
            {
                return;
            }
            var commandParameter = commandSource.CommandParameter ?? commandSource;
            //var routedCommand = command as RoutedCommand;
            //if (routedCommand != null)
            //{
            //    var target = commandSource.CommandTarget ?? commandSource as IInputElement;
            //    if (routedCommand.CanExecute(commandParameter, target))
            //    {
            //        routedCommand.Execute(commandParameter, target);
            //    }
            //}
            //else
            {
                if (command.CanExecute(commandParameter))
                {
                    command.Execute(commandParameter);
                }
            }
        }
    }
}
