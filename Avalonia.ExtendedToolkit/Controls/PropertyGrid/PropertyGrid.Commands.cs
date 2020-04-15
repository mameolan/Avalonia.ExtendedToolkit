using System.Windows.Input;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    public partial class PropertyGrid
    {
        public ICommand ResetFilterCommand { get; private set; }
        public ICommand ReloadCommand { get; private set; }
        public ICommand ShowReadOnlyPropertiesCommand { get; private set; }
        public ICommand HideReadOnlyPropertiesCommand { get; private set; }
        public ICommand ToggleReadOnlyPropertiesCommand { get; private set; }
        public ICommand ShowAttachedPropertiesCommand { get; private set; }
        public ICommand HideAttachedPropertiesCommand { get; private set; }
        public ICommand ToggleAttachedPropertiesCommand { get; private set; }
        public ICommand ShowFilterCommand { get; private set; }
        public ICommand HideFilterCommand { get; private set; }
        public ICommand ToggleFilterCommand { get; private set; }
        public ICommand ShowDialogEditorCommand { get; private set; }

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
