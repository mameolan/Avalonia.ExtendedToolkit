using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public partial class WindowButtonCommands : ContentControl
    {
        /// <summary>
        /// registeres Theme changed event
        /// </summary>
        public WindowButtonCommands()
        {
            ThemeProperty.Changed.AddClassHandler<WindowButtonCommands>((o, e) => OnThemeChanged(o, e));
        }

        private void OnThemeChanged(WindowButtonCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            if(LightCloseButtonStyle==null|| LightMaxButtonStyle==null|| LightMinButtonStyle==null
                || DarkCloseButtonStyle==null|| DarkMaxButtonStyle==null|| DarkMinButtonStyle==null)
            {
                return;
            }

            if (e.NewValue is WindowCommandTheme)
            {
                WindowCommandTheme windowCommandTheme = (WindowCommandTheme)e.NewValue;

                close.Styles.Remove(LightCloseButtonStyle);
                close.Styles.Remove(DarkCloseButtonStyle);
                max.Styles.Remove(LightMaxButtonStyle);
                max.Styles.Remove(DarkMaxButtonStyle);
                min.Styles.Remove(LightMinButtonStyle);
                min.Styles.Remove(DarkMinButtonStyle);

                switch (windowCommandTheme)
                {
                    case WindowCommandTheme.Light:
                        close.Styles.Add(LightCloseButtonStyle);
                        max.Styles.Add(LightMaxButtonStyle);
                        min.Styles.Add(LightMinButtonStyle);
                        break;

                    case WindowCommandTheme.Dark:
                        close.Styles.Add(DarkCloseButtonStyle);
                        max.Styles.Add(DarkMaxButtonStyle);
                        min.Styles.Add(DarkMinButtonStyle);
                        break;
                }
            }
        }

        private Button min;
        private Button max;
        private Button close;

        /// <summary>
        /// gets the controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            close = e.NameScope.Find<Button>("PART_Close");
            if (close != null)
            {
                close.Click += CloseClick;
            }

            max = e.NameScope.Find<Button>("PART_Max");
            if (max != null)
            {
                max.Click += MaximizeClick;
            }

            min = e.NameScope.Find<Button>("PART_Min");
            if (min != null)
            {
                min.Click += MinimizeClick;
            }
        }

        /// <summary>
        /// execute ClosingWindow event if registered
        /// </summary>
        /// <param name="args"></param>
        protected void OnClosingWindow(ClosingWindowEventArgs args)
        {
            var handler = ClosingWindow;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            if (null == this.ParentWindow) return;

            this.ParentWindow.WindowState = WindowState.Minimized;

            //ControlzEx.Windows.Shell.SystemCommands.MinimizeWindow(this.ParentWindow);
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (null == this.ParentWindow) return;
            if (this.ParentWindow.WindowState == WindowState.Maximized)
            {
                this.ParentWindow.WindowState = WindowState.Normal;

                //  ControlzEx.Windows.Shell.SystemCommands.RestoreWindow(this.ParentWindow);
            }
            else
            {
                this.ParentWindow.WindowState = WindowState.Maximized;
                //ControlzEx.Windows.Shell.SystemCommands.MaximizeWindow(this.ParentWindow);
            }
        }

#pragma warning restore 618

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var closingWindowEventHandlerArgs = new ClosingWindowEventArgs();
            OnClosingWindow(closingWindowEventHandlerArgs);

            if (closingWindowEventHandlerArgs.Cancelled)
            {
                return;
            }

            if (null == this.ParentWindow) return;
            this.ParentWindow.Close();
        }

        private MetroWindow _parentWindow;

        /// <summary>
        /// returns the parent MetroWindow
        /// </summary>
        public MetroWindow ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                if (Equals(_parentWindow, value))
                {
                    return;
                }

                if (_parentWindow != null)
                {
                    _parentWindow.PropertyChanged -= ParentWindow_PropertyChanged;
                }

                _parentWindow = value;
                //this.OnPropertyChanged("ParentWindow");

                _parentWindow.PropertyChanged += ParentWindow_PropertyChanged;
            }
        }

        private void ParentWindow_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (WindowState != _parentWindow.WindowState)
            {
                WindowState = _parentWindow.WindowState;
            }
            if (IsAnyDialogOpen != _parentWindow.IsAnyDialogOpen)
            {
                IsAnyDialogOpen = _parentWindow.IsAnyDialogOpen;
            }
            if (IsCloseButtonEnabled != _parentWindow.IsCloseButtonEnabled)
            {
                IsCloseButtonEnabled = _parentWindow.IsCloseButtonEnabled;
            }

            //IsCloseButtonEnabledWithDialog = _parentWindow.IsCloseButtonEnabledWithDialog;
        }
    }
}
