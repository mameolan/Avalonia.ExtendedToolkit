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
        /// <summary>
        /// resets the filter
        /// </summary>
        public ICommand ResetFilterCommand
        {
            get { return (ICommand)GetValue(ResetFilterCommandProperty); }
            private set { SetValue(ResetFilterCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ResetFilterCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ResetFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ResetFilterCommand));

        /// <summary>
        /// reloads the propertygrid
        /// </summary>
        public ICommand ReloadCommand
        {
            get { return (ICommand)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ReloadCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ReloadCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ReloadCommand));

        /// <summary>
        /// gets ShowReadOnlyProperties
        /// </summary>
        public ICommand ShowReadOnlyPropertiesCommand
        {
            get { return (ICommand)GetValue(ShowReadOnlyPropertiesCommandProperty); }
            set { SetValue(ShowReadOnlyPropertiesCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowReadOnlyPropertiesCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ShowReadOnlyPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowReadOnlyPropertiesCommand));

        /// <summary>
        /// gets HideReadOnlyPropertiesCommand
        /// </summary>
        public ICommand HideReadOnlyPropertiesCommand
        {
            get { return (ICommand)GetValue(HideReadOnlyPropertiesCommandProperty); }
            set { SetValue(HideReadOnlyPropertiesCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="HideReadOnlyPropertiesCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> HideReadOnlyPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(HideReadOnlyPropertiesCommand));

        /// <summary>
        /// gets ToggleReadOnlyPropertiesCommand
        /// </summary>
        public ICommand ToggleReadOnlyPropertiesCommand
        {
            get { return (ICommand)GetValue(ToggleReadOnlyPropertiesCommandProperty); }
            set { SetValue(ToggleReadOnlyPropertiesCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ToggleReadOnlyPropertiesCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ToggleReadOnlyPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ToggleReadOnlyPropertiesCommand));

        /// <summary>
        /// gets ShowAttachedPropertiesCommand
        /// </summary>
        public ICommand ShowAttachedPropertiesCommand
        {
            get { return (ICommand)GetValue(ShowAttachedPropertiesCommandProperty); }
            set { SetValue(ShowAttachedPropertiesCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowAttachedPropertiesCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ShowAttachedPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowAttachedPropertiesCommand));

        /// <summary>
        /// gets HideAttachedPropertiesCommand
        /// </summary>
        public ICommand HideAttachedPropertiesCommand
        {
            get { return (ICommand)GetValue(HideAttachedPropertiesCommandProperty); }
            set { SetValue(HideAttachedPropertiesCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="HideAttachedPropertiesCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> HideAttachedPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(HideAttachedPropertiesCommand));

        /// <summary>
        /// gets ToggleAttachedPropertiesCommand
        /// </summary>
        public ICommand ToggleAttachedPropertiesCommand
        {
            get { return (ICommand)GetValue(ToggleAttachedPropertiesCommandProperty); }
            set { SetValue(ToggleAttachedPropertiesCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ToggleAttachedPropertiesCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ToggleAttachedPropertiesCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ToggleAttachedPropertiesCommand));

        /// <summary>
        /// get ShowFilterCommand
        /// </summary>
        public ICommand ShowFilterCommand
        {
            get { return (ICommand)GetValue(ShowFilterCommandProperty); }
            set { SetValue(ShowFilterCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowFilterCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ShowFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ShowFilterCommand));

        /// <summary>
        /// get HideFilterCommand
        /// </summary>
        public ICommand HideFilterCommand
        {
            get { return (ICommand)GetValue(HideFilterCommandProperty); }
            set { SetValue(HideFilterCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="HideFilterCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> HideFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(HideFilterCommand));

        /// <summary>
        /// get ToggleFilterCommand
        /// </summary>
        public ICommand ToggleFilterCommand
        {
            get { return (ICommand)GetValue(ToggleFilterCommandProperty); }
            set { SetValue(ToggleFilterCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ToggleFilterCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ToggleFilterCommandProperty =
            AvaloniaProperty.Register<PropertyGrid, ICommand>(nameof(ToggleFilterCommand));

        /// <summary>
        /// gets ShowDialogEditorCommand
        /// </summary>
        public ICommand ShowDialogEditorCommand
        {
            get { return (ICommand)GetValue(ShowDialogEditorCommandProperty); }
            set { SetValue(ShowDialogEditorCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowDialogEditorCommand"/>
        /// </summary>
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
