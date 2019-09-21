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
            //ControlzEx.Windows.Shell.SystemCommands.MinimizeWindow(this.ParentWindow);
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (null == this.ParentWindow) return;
            if (this.ParentWindow.WindowState == WindowState.Maximized)
            {
              //  ControlzEx.Windows.Shell.SystemCommands.RestoreWindow(this.ParentWindow);
            }
            else
            {
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
                _parentWindow = value;
                //this.OnPropertyChanged("ParentWindow");
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void RaisePropertyChanged(string propertyName = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
