using Avalonia.Input;
using System;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuItem: HamburgerMenuItemBase, ICommandSource
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly StyledProperty<string> LabelProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, string>(nameof(Label));

        public Type TargetPageType
        {
            get { return (Type)GetValue(TargetPageTypeProperty); }
            set { SetValue(TargetPageTypeProperty, value); }
        }

        public static readonly StyledProperty<Type> TargetPageTypeProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, Type>(nameof(TargetPageType));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, ICommand>(nameof(Command));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly StyledProperty<object> CommandParameterProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, object>(nameof(CommandParameter));

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public static readonly StyledProperty<IInputElement> CommandTargetProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, IInputElement>(nameof(CommandTarget));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> IsEnabledProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, bool>(nameof(IsEnabled), defaultValue:true);

        public object ToolTip
        {
            get { return (object)GetValue(ToolTipProperty); }
            set { SetValue(ToolTipProperty, value); }
        }

        public static readonly StyledProperty<object> ToolTipProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, object>(nameof(ToolTip));

        public HamburgerMenuItem()
        {
            CommandProperty.Changed.AddClassHandler<HamburgerMenuItem>((o, e) => OnCommandChanged(o, e));
            IsEnabledProperty.Changed.AddClassHandler<HamburgerMenuItem>((o,e)=> IsEnabledChanged(o, e));
        }

        /// <summary>
        /// Executes the command which can be set by the user.
        /// </summary>
        public void RaiseCommand()
        {
            CommandHelpers.ExecuteCommandSource((ICommandSource)this);
        }

        private void IsEnabledChanged(HamburgerMenuItem o, AvaloniaPropertyChangedEventArgs e)
        {
            if ((e.NewValue as bool?) == IsEnabled)
                return;

            IsEnabled = CanExecute;
        }

        private void OnCommandChanged(HamburgerMenuItem o, AvaloniaPropertyChangedEventArgs e)
        {
            OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                this.UnhookCommand(oldCommand);
            }
            if (newCommand != null)
            {
                this.HookCommand(newCommand);
            }
        }

        private void UnhookCommand(ICommand command)
        {
            //CanExecuteChangedEventManager.RemoveHandler(command, new EventHandler<EventArgs>(this.OnCanExecuteChanged));
            this.UpdateCanExecute();
        }

        private void HookCommand(ICommand command)
        {
            //CanExecuteChangedEventManager.AddHandler(command, new EventHandler<EventArgs>(this.OnCanExecuteChanged));
            this.UpdateCanExecute();
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            this.UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            if (this.Command != null)
            {
                this.CanExecute = CommandHelpers.CanExecuteCommandSource(this);
            }
            else
            {
                this.CanExecute = true;
            }
        }

        private bool canExecute = true;

        private bool CanExecute
        {
            get
            {
                return this.canExecute;
            }
            set
            {
                if (value == this.canExecute)
                {
                    return;
                }
                this.canExecute = value;
                //this.OnPropertyChanged(new AvaloniaPropertyChangedEventArgs(this, IsEnabledProperty, null, IsEnabled, Data.BindingPriority.LocalValue));
                this.OnPropertyChanged<bool>(IsEnabledProperty, !IsEnabled, IsEnabled, Data.BindingPriority.LocalValue);
                
                //this.CoerceValue(IsEnabledProperty);
            }
        }
    }
}