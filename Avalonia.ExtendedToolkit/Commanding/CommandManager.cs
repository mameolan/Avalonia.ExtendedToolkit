using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Commanding
{
    public class CommandManager
    {
        private static List<CommandBinding> commandBindings = new List<CommandBinding>();

        public static event EventHandler RequerySuggested;




        public static void InvalidateRequerySuggested()
        {
            if (RequerySuggested != null)
                RequerySuggested(null, EventArgs.Empty);

            commandBindings.ForEach(x => x.FireCanExecute()); 
        }

        internal static void AddCommandBinding(CommandBinding commandBinding)
        {
            commandBindings.Add(commandBinding);
        }

        internal static void RemoveCommandBinding(CommandBinding commandBinding)
        {
            commandBindings.Remove(commandBinding);
        }
    }
}
