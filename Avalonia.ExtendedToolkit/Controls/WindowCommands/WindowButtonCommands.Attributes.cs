using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// window commands control
    /// </summary>
    public partial class WindowButtonCommands : ContentControl//, INotifyPropertyChanged
    {
        /// <summary>
        /// ClosingWindow event
        /// </summary>
        public event ClosingWindowEventHandler ClosingWindow;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(WindowButtonCommands);

        /// <summary>
        /// ClosingWindowEventHandler delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void ClosingWindowEventHandler(object sender, ClosingWindowEventArgs args);

        /// <summary>
        /// Gets or sets the value indicating current light style for the minimize button.
        /// </summary>
        public IStyle LightMinButtonStyle
        {
            get { return (IStyle)GetValue(LightMinButtonStyleProperty); }
            set { SetValue(LightMinButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="LightMinButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> LightMinButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(LightMinButtonStyle));

        /// <summary>
        /// Gets or sets the value indicating current light style for the maximize button.
        /// </summary>
        public IStyle LightMaxButtonStyle
        {
            get { return (IStyle)GetValue(LightMaxButtonStyleProperty); }
            set { SetValue(LightMaxButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="LightMaxButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> LightMaxButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(LightMaxButtonStyle));

        /// <summary>
        /// Gets or sets the value indicating current light style for the close button.
        /// </summary>
        public IStyle LightCloseButtonStyle
        {
            get { return (IStyle)GetValue(LightCloseButtonStyleProperty); }
            set { SetValue(LightCloseButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="LightCloseButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> LightCloseButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(LightCloseButtonStyle));

        /// <summary>
        /// Gets or sets the value indicating current dark style for the minimize button.
        /// </summary>
        public IStyle DarkMinButtonStyle
        {
            get { return (IStyle)GetValue(DarkMinButtonStyleProperty); }
            set { SetValue(DarkMinButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="DarkMinButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> DarkMinButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(DarkMinButtonStyle));

        /// <summary>
        /// Gets or sets the value indicating current dark style for the maximize button.
        /// </summary>
        public IStyle DarkMaxButtonStyle
        {
            get { return (IStyle)GetValue(DarkMaxButtonStyleProperty); }
            set { SetValue(DarkMaxButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="DarkMaxButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> DarkMaxButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(DarkMaxButtonStyle));

        /// <summary>
        /// Gets or sets the value indicating current dark style for the close button.
        /// </summary>
        public IStyle DarkCloseButtonStyle
        {
            get { return (IStyle)GetValue(DarkCloseButtonStyleProperty); }
            set { SetValue(DarkCloseButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="DarkCloseButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> DarkCloseButtonStyleProperty =
            AvaloniaProperty.Register<WindowButtonCommands, IStyle>(nameof(DarkCloseButtonStyle));

        /// <summary>
        /// Gets or sets the value indicating current theme.
        /// </summary>
        public WindowCommandTheme Theme
        {
            get { return (WindowCommandTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        /// <summary>
        /// <see cref="Theme"/>
        /// </summary>
        public static readonly StyledProperty<WindowCommandTheme> ThemeProperty =
            AvaloniaProperty.Register<WindowButtonCommands, WindowCommandTheme>(nameof(Theme), WindowCommandTheme.Light);

        /// <summary>
        /// Gets or sets the minimize button tooltip.
        /// </summary>
        public string Minimize
        {
            get { return (string)GetValue(MinimizeProperty); }
            set { SetValue(MinimizeProperty, value); }
        }

        /// <summary>
        /// <see cref="Minimize"/>
        /// </summary>
        public static readonly StyledProperty<string> MinimizeProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Minimize));

        /// <summary>
        /// Gets or sets the maximize button tooltip.
        /// </summary>
        public string Maximize
        {
            get { return (string)GetValue(MaximizeProperty); }
            set { SetValue(MaximizeProperty, value); }
        }

        /// <summary>
        /// <see cref="Maximize"/>
        /// </summary>
        public static readonly StyledProperty<string> MaximizeProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Maximize));

        /// <summary>
        /// Gets or sets the close button tooltip.
        /// </summary>
        public string Close
        {
            get { return (string)GetValue(CloseProperty); }
            set { SetValue(CloseProperty, value); }
        }

        /// <summary>
        /// <see cref="Close"/>
        /// </summary>
        public static readonly StyledProperty<string> CloseProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Close));

        /// <summary>
        /// Gets or sets the restore button tooltip.
        /// </summary>
        public string Restore
        {
            get { return (string)GetValue(RestoreProperty); }
            set { SetValue(RestoreProperty, value); }
        }

        /// <summary>
        /// <see cref="Restore"/>
        /// </summary>
        public static readonly StyledProperty<string> RestoreProperty =
            AvaloniaProperty.Register<WindowButtonCommands, string>(nameof(Restore));

        /// <summary>
        /// get IsCloseButtonEnabled
        /// </summary>
        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            private set { SetValue(IsCloseButtonEnabledProperty, value); }
        }

        /// <summary>
        /// <see cref="IsCloseButtonEnabled"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsCloseButtonEnabledProperty =
            AvaloniaProperty.Register<WindowButtonCommands, bool>(nameof(IsCloseButtonEnabled));

        /// <summary>
        /// get IsAnyDialogOpen
        /// </summary>
        public bool IsAnyDialogOpen
        {
            get { return (bool)GetValue(IsAnyDialogOpenProperty); }
            private set { SetValue(IsAnyDialogOpenProperty, value); }
        }

        /// <summary>
        /// <see cref="IsAnyDialogOpen"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsAnyDialogOpenProperty =
            AvaloniaProperty.Register<WindowButtonCommands, bool>(nameof(IsAnyDialogOpen));

        /// <summary>
        /// gets IsCloseButtonEnabledWithDialog
        /// </summary>
        public bool IsCloseButtonEnabledWithDialog
        {
            get { return (bool)GetValue(IsCloseButtonEnabledWithDialogProperty); }
            private set { SetValue(IsCloseButtonEnabledWithDialogProperty, value); }
        }

        /// <summary>
        /// <see cref="IsCloseButtonEnabledWithDialog"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsCloseButtonEnabledWithDialogProperty =
            AvaloniaProperty.Register<WindowButtonCommands, bool>(nameof(IsCloseButtonEnabledWithDialog));

        /// <summary>
        /// get/sets WindowState
        /// </summary>
        public WindowState WindowState
        {
            get { return (WindowState)GetValue(WindowStateProperty); }
            set { SetValue(WindowStateProperty, value); }
        }

        /// <summary>
        /// <see cref="WindowState"/>
        /// </summary>
        public static readonly StyledProperty<WindowState> WindowStateProperty =
            AvaloniaProperty.Register<WindowButtonCommands, WindowState>(nameof(WindowState));
    }
}
