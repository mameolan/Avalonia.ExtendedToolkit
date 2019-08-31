using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Commanding
{
    /// <summary>
    /// CommandBinding - Command-EventHandlers map
    ///         CommandBinding acts like a map for EventHandlers and Commands. 
    ///         PreviewExecute/Execute, PreviewCanExecute/CanExecute handlers 
    ///         can be added at CommandBinding which will exist at Element level 
    ///         in the form of a Collection and will be invoked when the system 
    ///         is routing the corresponding RoutedEvents.
    /// </summary>
    public class CommandBinding
    {
        #region Constructors

        /// <summary>
        ///     Default Constructor - required to allow creation from markup
        /// </summary>
        public CommandBinding()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command associated with this binding.</param>
        public CommandBinding(ICommand command)
            : this(command, null, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command associated with this binding.</param>
        /// <param name="executed">Handler associated with executing the command.</param>
        public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed)
            : this(command, executed, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command associated with this binding.</param>
        /// <param name="executed">Handler associated with executing the command.</param>
        /// <param name="canExecute">Handler associated with determining if the command can execute.</param>
        public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            _command = command;

            if (executed != null)
            {
                Executed += executed;
                
            }
            if (canExecute != null)
            {
                CanExecute += canExecute;
                
            }

            if(_command is RoutedCommand)
            {
                _command.CanExecuteChanged += (o, e) =>
                {
                    CanExecute(o, new CanExecuteRoutedEventArgs(_command, e));
                };
            }

            


        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Command associated with this binding
        /// </summary>
        public ICommand Command
        {
            get
            {
                return _command;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _command = value;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Called before the command is executed.
        /// </summary>
        //public event ExecutedRoutedEventHandler PreviewExecuted;

        /// <summary>
        ///     Called when the command is executed.
        /// </summary>
        public event ExecutedRoutedEventHandler Executed;

        /// <summary>
        ///     Called before determining if the command can be executed.
        /// </summary>
        //public event CanExecuteRoutedEventHandler PreviewCanExecute;

        /// <summary>
        ///     Called to determine if the command can be executed.
        /// </summary>
        public event CanExecuteRoutedEventHandler CanExecute;

        #endregion

        #region Implementation

        /// <summary>
        ///     Calls the CanExecute or PreviewCanExecute event based on the event argument's RoutedEvent.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        internal void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!e.Handled)
            {
                //if (e.RoutedEvent == CommandManager.CanExecuteEvent)
                {
                    if (CanExecute != null)
                    {
                        CanExecute(sender, e);
                        if (e.CanExecute)
                        {
                            e.Handled = true;
                        }
                    }
                    else if (!e.CanExecute)
                    {
                        // If there is an Executed handler, then the command can be executed.
                        if (Executed != null)
                        {
                            e.CanExecute = true;
                            e.Handled = true;
                        }
                    }
                }
                //else // e.RoutedEvent == CommandManager.PreviewCanExecuteEvent
                //{
                //    if (PreviewCanExecute != null)
                //    {
                //        PreviewCanExecute(sender, e);
                //        if (e.CanExecute)
                //        {
                //            e.Handled = true;
                //        }
                //    }
                //}
            }
        }

        internal bool CheckCanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            CanExecuteRoutedEventArgs canExecuteArgs = new CanExecuteRoutedEventArgs(e.Command, e.Parameter);
            canExecuteArgs.RoutedEvent = e.RoutedEvent;

            // Since we don't actually raise this event, we have to explicitly set the source.
            canExecuteArgs.Source = e.Source;
            //canExecuteArgs.OverrideSource(e.Source);

            OnCanExecute(sender, canExecuteArgs);

            return canExecuteArgs.CanExecute;
        }

        /// <summary>
        ///     Calls Executed or PreviewExecuted based on the event argument's RoutedEvent.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        internal void OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!e.Handled)
            {
                //if (e.RoutedEvent == CommandManager.ExecutedEvent)
                {
                    if (Executed != null)
                    {
                        if (CheckCanExecute(sender, e))
                        {
                            Executed(sender, e);
                            e.Handled = true;
                        }
                    }
                }
                //else // e.RoutedEvent == CommandManager.PreviewExecutedEvent
                //{
                //    if (PreviewExecuted != null)
                //    {
                //        if (CheckCanExecute(sender, e))
                //        {
                //            PreviewExecuted(sender, e);
                //            e.Handled = true;
                //        }
                //    }
                //}
            }
        }

        internal void FireCanExecute()
        {
            OnCanExecute(this, new CanExecuteRoutedEventArgs(Command, null));

            if(Command is RoutedCommand)
            {
                (Command as RoutedCommand).CanExecute(null);
            }
            
        }

        #endregion

        #region Data

        private ICommand _command;

        #endregion
    }
}
