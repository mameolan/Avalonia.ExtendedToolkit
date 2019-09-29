using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class WindowButtonCommands : ContentControl, INotifyPropertyChanged
    {
        public event ClosingWindowEventHandler ClosingWindow;
        public delegate void ClosingWindowEventHandler(object sender, ClosingWindowEventHandlerArgs args);



        public IStyle LightMinButtonStyle
        {
            get { return (IStyle)GetValue(LightMinButtonStyleProperty); }
            set { SetValue(LightMinButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty LightMinButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(LightMinButtonStyle));



        public IStyle LightMaxButtonStyle
        {
            get { return (IStyle)GetValue(LightMaxButtonStyleProperty); }
            set { SetValue(LightMaxButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty LightMaxButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(LightMaxButtonStyle));



        public IStyle LightCloseButtonStyle
        {
            get { return (IStyle)GetValue(LightCloseButtonStyleProperty); }
            set { SetValue(LightCloseButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty LightCloseButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(LightCloseButtonStyle));



        public IStyle DarkMinButtonStyle
        {
            get { return (IStyle)GetValue(DarkMinButtonStyleProperty); }
            set { SetValue(DarkMinButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty DarkMinButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(DarkMinButtonStyle));



        public IStyle DarkMaxButtonStyle
        {
            get { return (IStyle)GetValue(DarkMaxButtonStyleProperty); }
            set { SetValue(DarkMaxButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty DarkMaxButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(DarkMaxButtonStyle));



        public IStyle DarkCloseButtonStyle
        {
            get { return (IStyle)GetValue(DarkCloseButtonStyleProperty); }
            set { SetValue(DarkCloseButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty DarkCloseButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(DarkCloseButtonStyle));



        public WindowCommandTheme Theme
        {
            get { return (WindowCommandTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }


        public static readonly AvaloniaProperty ThemeProperty =
            AvaloniaProperty.Register<WindowButtonCommands, WindowCommandTheme>(nameof(Theme), WindowCommandTheme.Light);



        public string Minimize
        {
            get { return (string)GetValue(MinimizeProperty); }
            set { SetValue(MinimizeProperty, value); }
        }


        public static readonly AvaloniaProperty MinimizeProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Minimize));



        public string Maximize
        {
            get { return (string)GetValue(MaximizeProperty); }
            set { SetValue(MaximizeProperty, value); }
        }


        public static readonly AvaloniaProperty MaximizeProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Maximize));



        public string Close
        {
            get { return (string)GetValue(CloseProperty); }
            set { SetValue(CloseProperty, value); }
        }


        public static readonly AvaloniaProperty CloseProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Close));



        public string Restore
        {
            get { return (string)GetValue(RestoreProperty); }
            set { SetValue(RestoreProperty, value); }
        }


        public static readonly AvaloniaProperty RestoreProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Restore));




        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            private set { SetValue(IsCloseButtonEnabledProperty, value); }
        }


        public static readonly AvaloniaProperty IsCloseButtonEnabledProperty =
            AvaloniaProperty.Register<WindowButtonCommands, bool>(nameof(IsCloseButtonEnabled));



        public bool IsAnyDialogOpen
        {
            get { return (bool)GetValue(IsAnyDialogOpenProperty); }
            private set { SetValue(IsAnyDialogOpenProperty, value); }
        }


        public static readonly AvaloniaProperty IsAnyDialogOpenProperty =
            AvaloniaProperty.Register<WindowButtonCommands, bool>(nameof(IsAnyDialogOpen));



        public bool IsCloseButtonEnabledWithDialog
        {
            get { return (bool)GetValue(IsCloseButtonEnabledWithDialogProperty); }
            private set { SetValue(IsCloseButtonEnabledWithDialogProperty, value); }
        }


        public static readonly AvaloniaProperty IsCloseButtonEnabledWithDialogProperty =
            AvaloniaProperty.Register<WindowButtonCommands, bool>(nameof(IsCloseButtonEnabledWithDialog));




        public WindowState WindowState
        {
            get { return (WindowState)GetValue(WindowStateProperty); }
            set { SetValue(WindowStateProperty, value); }
        }


        public static readonly AvaloniaProperty WindowStateProperty =
            AvaloniaProperty.Register<WindowButtonCommands, WindowState>(nameof(WindowState));



        public WindowButtonCommands()
        {
            ThemeProperty.Changed.AddClassHandler<WindowButtonCommands>((o, e) => OnThemeChanged(o, e));
        }

        private void OnThemeChanged(WindowButtonCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is WindowCommandTheme)
            {
                WindowCommandTheme windowCommandTheme = (WindowCommandTheme)e.NewValue;

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

        protected void OnClosingWindow(ClosingWindowEventHandlerArgs args)
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
            var closingWindowEventHandlerArgs = new ClosingWindowEventHandlerArgs();
            OnClosingWindow(closingWindowEventHandlerArgs);

            if (closingWindowEventHandlerArgs.Cancelled)
            {
                return;
            }

            if (null == this.ParentWindow) return;
            this.ParentWindow.Close();
        }

        private MetroWindow _parentWindow;

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

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void RaisePropertyChanged(string propertyName = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
