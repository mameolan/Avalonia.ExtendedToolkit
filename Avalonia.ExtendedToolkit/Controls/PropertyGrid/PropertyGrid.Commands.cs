using System.Windows.Input;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    public partial class PropertyGrid
    {
        public ICommand ResetFilterCommand
        {
            get { return (ICommand)GetValue(ResetFilterCommandProperty); }
            private set { SetValue(ResetFilterCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ResetFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ResetFilterCommand));

        public ICommand ReloadCommand
        {
            get { return (ICommand)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ReloadCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ReloadCommand));

        public ICommand ShowReadOnlyPropertiesCommand
        {
            get { return (ICommand)GetValue(ShowReadOnlyPropertiesCommandProperty); }
            set { SetValue(ShowReadOnlyPropertiesCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ShowReadOnlyPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowReadOnlyPropertiesCommand));

        public ICommand HideReadOnlyPropertiesCommand
        {
            get { return (ICommand)GetValue(HideReadOnlyPropertiesCommandProperty); }
            set { SetValue(HideReadOnlyPropertiesCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> HideReadOnlyPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(HideReadOnlyPropertiesCommand));

        public ICommand ToggleReadOnlyPropertiesCommand
        {
            get { return (ICommand)GetValue(ToggleReadOnlyPropertiesCommandProperty); }
            set { SetValue(ToggleReadOnlyPropertiesCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ToggleReadOnlyPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ToggleReadOnlyPropertiesCommand));

        public ICommand ShowAttachedPropertiesCommand
        {
            get { return (ICommand)GetValue(ShowAttachedPropertiesCommandProperty); }
            set { SetValue(ShowAttachedPropertiesCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ShowAttachedPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowAttachedPropertiesCommand));

        public ICommand HideAttachedPropertiesCommand
        {
            get { return (ICommand)GetValue(HideAttachedPropertiesCommandProperty); }
            set { SetValue(HideAttachedPropertiesCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> HideAttachedPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(HideAttachedPropertiesCommand));

        public ICommand ToggleAttachedPropertiesCommand
        {
            get { return (ICommand)GetValue(ToggleAttachedPropertiesCommandProperty); }
            set { SetValue(ToggleAttachedPropertiesCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ToggleAttachedPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ToggleAttachedPropertiesCommand));

        public ICommand ShowFilterCommand
        {
            get { return (ICommand)GetValue(ShowFilterCommandProperty); }
            set { SetValue(ShowFilterCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ShowFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowFilterCommand));

        public ICommand HideFilterCommand
        {
            get { return (ICommand)GetValue(HideFilterCommandProperty); }
            set { SetValue(HideFilterCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> HideFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(HideFilterCommand));

        public ICommand ToggleFilterCommand
        {
            get { return (ICommand)GetValue(ToggleFilterCommandProperty); }
            set { SetValue(ToggleFilterCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ToggleFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ToggleFilterCommand));

        public ICommand ShowDialogEditorCommand
        {
            get { return (ICommand)GetValue(ShowDialogEditorCommandProperty); }
            set { SetValue(ShowDialogEditorCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> ShowDialogEditorCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowDialogEditorCommand));

        /// <summary>
        /// initialize the commands
        /// </summary>
        protected void InitializeCommandBindings()
        {
            ResetFilterCommand = ReactiveCommand.Create(() => OnResetFilterCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ReloadCommand= ReactiveCommand.Create(() => OnReloadCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ShowReadOnlyPropertiesCommand = ReactiveCommand.Create(() => OnShowReadOnlyPropertiesCommand(), outputScheduler: RxApp.MainThreadScheduler);
            HideReadOnlyPropertiesCommand = ReactiveCommand.Create(() => OnHideReadOnlyPropertiesCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ToggleReadOnlyPropertiesCommand = ReactiveCommand.Create(() => OnToggleReadOnlyPropertiesCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ShowAttachedPropertiesCommand = ReactiveCommand.Create(() => OnShowAttachedPropertiesCommand(), outputScheduler: RxApp.MainThreadScheduler);
            HideAttachedPropertiesCommand = ReactiveCommand.Create(() => OnHideAttachedPropertiesCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ToggleAttachedPropertiesCommand = ReactiveCommand.Create(() => OnToggleAttachedPropertiesCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ShowFilterCommand = ReactiveCommand.Create(() => OnShowFilterCommand(), outputScheduler: RxApp.MainThreadScheduler);
            HideFilterCommand = ReactiveCommand.Create(() => OnHideFilterCommand(), outputScheduler: RxApp.MainThreadScheduler);
            ToggleFilterCommand= ReactiveCommand.Create(() => OnToggleFilterCommand(), outputScheduler: RxApp.MainThreadScheduler);

            ShowDialogEditorCommand = ReactiveCommand.Create<object>(x => OnShowDialogEditor(x), outputScheduler: RxApp.MainThreadScheduler);
        }

        private void OnShowDialogEditor(object sender)
        {
            var value = sender as PropertyItemValue;
            if (value == null)
                return;

            var property = value.ParentProperty;
            if (property == null)
                return;

            var editor = property.Editor;
            // TODO: Finish DialogTemplate implementation
            if (editor != null)// && editor.HasDialogTemplate)
                editor.ShowDialog(value, this);
        }

        private void OnToggleFilterCommand()
        {
            PropertyFilterIsVisible = !PropertyFilterIsVisible;
        }

        private void OnHideFilterCommand()
        {
            PropertyFilterIsVisible = false;
        }

        private void OnShowFilterCommand()
        {
            PropertyFilterIsVisible= true;
        }

        private void OnToggleAttachedPropertiesCommand()
        {
            ShowAttachedProperties = !ShowAttachedProperties;
        }

        private void OnHideAttachedPropertiesCommand()
        {
            ShowAttachedProperties = false;
        }

        private void OnShowAttachedPropertiesCommand()
        {
            ShowAttachedProperties = true;
        }

        private void OnToggleReadOnlyPropertiesCommand()
        {
            ShowReadOnlyProperties = !ShowReadOnlyProperties;
        }

        private void OnHideReadOnlyPropertiesCommand()
        {
            ShowReadOnlyProperties = true;
        }

        private void OnShowReadOnlyPropertiesCommand()
        {
            ShowReadOnlyProperties = true;
        }

        private void OnReloadCommand()
        {
            DoReload();
        }

        private void OnResetFilterCommand()
        {
            PropertyFilter = string.Empty;
        }
    }
}
