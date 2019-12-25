using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controlz.Helper;
using Avalonia.LogicalTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.ExtendedToolkit.Controls.Dialogs
{
    public class BaseMetroDialog : ContentControl
    {
        /// <summary>
        /// Gets the window that owns the current Dialog IF AND ONLY IF the dialog is shown externally.
        /// </summary>
        protected internal Window ParentDialogWindow { get; internal set; }

        /// <summary>
        /// Gets the window that owns the current Dialog IF AND ONLY IF the dialog is shown inside of a window.
        /// </summary>
        protected internal MetroWindow OwningWindow { get; internal set; }


        public GridLength DialogContentMargin
        {
            get { return (GridLength)GetValue(DialogContentMarginProperty); }
            set { SetValue(DialogContentMarginProperty, value); }
        }


        public static readonly AvaloniaProperty DialogContentMarginProperty =
            AvaloniaProperty.Register<BaseMetroDialog, GridLength>(nameof(DialogContentMargin)
                , defaultValue: new GridLength(25, GridUnitType.Star));



        public GridLength DialogContentWidth
        {
            get { return (GridLength)GetValue(DialogContentWidthProperty); }
            set { SetValue(DialogContentWidthProperty, value); }
        }


        public static readonly AvaloniaProperty DialogContentWidthProperty =
            AvaloniaProperty.Register<BaseMetroDialog, GridLength>(nameof(DialogContentWidth)
                , defaultValue: new GridLength(50, GridUnitType.Star));



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly AvaloniaProperty TitleProperty =
            AvaloniaProperty.Register<BaseMetroDialog, string>(nameof(Title));


        /// <summary>
        /// Gets or sets the content above the dialog.
        /// </summary>
        public object DialogTop
        {
            get { return (object)GetValue(DialogTopProperty); }
            set { SetValue(DialogTopProperty, value); }
        }


        public static readonly AvaloniaProperty DialogTopProperty =
            AvaloniaProperty.Register<BaseMetroDialog, object>(nameof(DialogTop));



        public object DialogBottom
        {
            get { return (object)GetValue(DialogBottomProperty); }
            set { SetValue(DialogBottomProperty, value); }
        }


        public static readonly AvaloniaProperty DialogBottomProperty =
            AvaloniaProperty.Register<BaseMetroDialog, object>(nameof(DialogBottom));




        public double DialogTitleFontSize
        {
            get { return (double)GetValue(DialogTitleFontSizeProperty); }
            set { SetValue(DialogTitleFontSizeProperty, value); }
        }


        public static readonly AvaloniaProperty DialogTitleFontSizeProperty =
            AvaloniaProperty.Register<BaseMetroDialog, double>(nameof(DialogTitleFontSize), defaultValue: 26D);



        public double DialogMessageFontSize
        {
            get { return (double)GetValue(DialogMessageFontSizeProperty); }
            set { SetValue(DialogMessageFontSizeProperty, value); }
        }


        public static readonly AvaloniaProperty DialogMessageFontSizeProperty =
            AvaloniaProperty.Register<BaseMetroDialog, double>(nameof(DialogMessageFontSize), defaultValue: 15D);


        public MetroDialogSettings DialogSettings { get; private set; }


        static BaseMetroDialog()
        {
            DialogTopProperty.Changed.AddClassHandler<BaseMetroDialog>((o, e) => UpdateLogicalChild(o, e));
            DialogBottomProperty.Changed.AddClassHandler<BaseMetroDialog>((o, e) => UpdateLogicalChild(o, e));
        }


        protected BaseMetroDialog(MetroWindow owningWindow, MetroDialogSettings settings)
        {
            this.Initialize(owningWindow, settings);
        }

        protected BaseMetroDialog()
            : this(null, new MetroDialogSettings())
        {
        }

        private static void UpdateLogicalChild(BaseMetroDialog dialog
            , AvaloniaPropertyChangedEventArgs e)
        {

            if (e.OldValue is StyledElement)
            {
                dialog.LogicalChildren.Remove(e.OldValue as StyledElement);
            }

            if (e.NewValue is StyledElement)
            {
                StyledElement newChild = e.NewValue as StyledElement;
                dialog.LogicalChildren.Add(newChild);
                newChild.DataContext = dialog.DataContext;
            }
        }

        protected IAvaloniaList<ILogical> LogicalChildren
        {
            get
            {
                AvaloniaList<ILogical> children = new AvaloniaList<ILogical>();

                if (this.DialogTop is ILogical)
                {
                    children.Add(this.DialogTop as ILogical);
                }

                if (this.Content is ILogical)
                {
                    children.Add(this.Content as ILogical);
                }

                if (this.DialogBottom is ILogical)
                {
                    children.Add(this.DialogBottom as ILogical);
                }
                return children;
            }
        }

        /// <summary>
        /// With this method it's possible to return your own settings in a custom dialog.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected virtual MetroDialogSettings ConfigureSettings(MetroDialogSettings settings)
        {
            return settings;
        }

        private void Initialize(MetroWindow owningWindow, MetroDialogSettings settings)
        {
            this.OwningWindow = owningWindow;
            this.DialogSettings = this.ConfigureSettings(settings ?? /*(owningWindow?.MetroDialogOptions  ??*/  new MetroDialogSettings());

            //if (this.DialogSettings?.CustomResourceDictionary != null)
            //{
            //    this.Resources.MergedDictionaries.Add(this.DialogSettings.CustomResourceDictionary);
            //}

            this.HandleThemeChange();

            this.Initialized += BaseMetroDialog_Initialized;

            //this.Loaded += this.BaseMetroDialogLoaded;
            //this.Unloaded += this.BaseMetroDialogUnloaded;
        }

        private void BaseMetroDialog_Initialized(object sender, EventArgs e)
        {
            ThemeManager.Instance.IsThemeChanged -= ThemeManagerIsThemeChanged;
            ThemeManager.Instance.IsThemeChanged += ThemeManagerIsThemeChanged;
            OnLoaded();
        }

        private void ThemeManagerIsThemeChanged(object sender, OnThemeChangedEventArgs e)
        {
            this.HandleThemeChange();
        }

        private static object TryGetResource(Theme theme, string key)
        {
            if (theme == null)
            {
                // nothing to do here, we can't found an app style (make sure all custom themes are added!)
                return null;
            }

            object themeResource = theme.Resources.FindResource(key);

            return themeResource;
        }



        internal void HandleThemeChange()
        {
            var theme = DetectTheme(this);

            if (Design.IsDesignMode || theme == null)
                return;

            if (this.DialogSettings != null)
            {
                switch (this.DialogSettings.ColorScheme)
                {
                    case MetroDialogColorScheme.Theme:
                        ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles, theme);
                        this.SetValue(BackgroundProperty, TryGetResource(theme, "MahApps.Brushes.WhiteColor"));
                        this.SetValue(ForegroundProperty, TryGetResource(theme, "MahApps.Brushes.Black"));
                        break;
                    case MetroDialogColorScheme.Inverted:
                        theme = ThemeManager.Instance.GetInverseTheme(theme);
                        if (theme == null)
                        {
                            throw new InvalidOperationException("The inverse dialog theme only works if the window theme abides the naming convention. " +
                                                                "See ThemeManager.GetInverseAppTheme for more infos");
                        }

                        ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles, theme);
                        this.SetValue(BackgroundProperty, TryGetResource(theme, "MahApps.Brushes.WhiteColor"));
                        this.SetValue(ForegroundProperty, TryGetResource(theme, "MahApps.Brushes.Black"));
                        break;
                    case MetroDialogColorScheme.Accented:
                        ThemeManager.Instance.ApplyThemeResourcesFromTheme(this.Styles, theme);
                        this.SetValue(BackgroundProperty, TryGetResource(theme, "MahApps.Brushes.Highlight"));
                        this.SetValue(ForegroundProperty, TryGetResource(theme, "MahApps.Brushes.IdealForeground"));
                        break;
                }
            }

            if (this.ParentDialogWindow != null)
            {
                this.ParentDialogWindow.SetValue(BackgroundProperty, this.Background);
                var glowBrush = TryGetResource(theme, "MahApps.Brushes.Accent");
                if (glowBrush != null)
                {
                    this.ParentDialogWindow.SetValue(MetroWindow.GlowBrushProperty, glowBrush);
                }
            }



        }

        /// <summary>
        /// This is called in the loaded event.
        /// </summary>
        protected virtual void OnLoaded()
        {
            // nothing here
        }

        private static Theme DetectTheme(BaseMetroDialog dialog)
        {
            if (dialog == null)
            {
                return null;
            }

            // first look for owner
            var window = dialog.OwningWindow ?? dialog.TryFindParent<MetroWindow>();
            var theme = window != null ? ThemeManager.Instance.DetectTheme(window) : null;
            if (theme != null)
            {
                return theme;
            }

            // second try, look for main window
            if (Application.Current != null)
            {
                var mainWindow = (Application.Current as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MetroWindow;
                theme = mainWindow != null ? ThemeManager.Instance.DetectTheme(mainWindow) : null;
                if (theme != null)
                {
                    return theme;
                }

                // oh no, now look at application resource
                theme = ThemeManager.Instance.DetectTheme(Application.Current);
                if (theme != null)
                {
                    return theme;
                }
            }

            return null;
        }

        /// <summary>
        /// Waits for the dialog to become ready for interaction.
        /// </summary>
        /// <returns>A task that represents the operation and it's status.</returns>
        public Task WaitForLoadAsync()
        {
            //this.Dispatcher.VerifyAccess();

            if (this.IsInitialized) return new Task(() => { });

            if (!this.DialogSettings.AnimateShow)
            {
                this.Opacity = 1.0; //skip the animation
            }

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            //RoutedEventHandler handler = null;
            //handler = (sender, args) =>
            //{
            //    this.Loaded -= handler;

            //    this.Focus();

            //    tcs.TrySetResult(null);
            //};

            //this.Loaded += handler;

            return tcs.Task;
        }

        /// <summary>
        /// Requests an externally shown Dialog to close. Will throw an exception if the Dialog is inside of a MetroWindow.
        /// </summary>
        public Task RequestCloseAsync()
        {
            if (this.OnRequestClose())
            {
                // Technically, the Dialog is /always/ inside of a MetroWindow.
                // If the dialog is inside of a user-created MetroWindow, not one created by the external dialog APIs.
                if (this.ParentDialogWindow == null)
                {
                    // this is very bad, or the user called the close event before we can do this
                    if (this.OwningWindow == null)
                    {
                        Trace.TraceWarning($"{this}: Can not request async closing, because the OwningWindow is already null. This can maybe happen if the dialog was closed manually.");
                        return Task.Factory.StartNew(() => { });
                    }

                    // This is from a user-created MetroWindow
                    //return this.OwningWindow.HideMetroDialogAsync(this);
                }

                // This is from a MetroWindow created by the external dialog APIs.
                return this.WaitForCloseAsync()
                           .ContinueWith(x => { /*this.ParentDialogWindow.Invoke(() => {*/ this.ParentDialogWindow.Close(); /*});*/ });
            }

            return Task.Factory.StartNew(() => { });
        }

        protected internal virtual void OnShown()
        {
        }

        protected internal virtual void OnClose()
        {
            // this is only set when a dialog is shown (externally) in it's OWN window.
            this.ParentDialogWindow?.Close();
        }

        /// <summary>
        /// A last chance virtual method for stopping an external dialog from closing.
        /// </summary>
        /// <returns></returns>
        protected internal virtual bool OnRequestClose()
        {
            return true; //allow the dialog to close.
        }

        /// <summary>
        /// Waits until this dialog gets unloaded.
        /// </summary>
        /// <returns></returns>
        public Task WaitUntilUnloadedAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            ///this.Unloaded += (s, e) => { tcs.TrySetResult(null); };

            return tcs.Task;
        }

        public Task WaitForCloseAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            if (this.DialogSettings.AnimateHide)
            {
                //Storyboard closingStoryboard = this.TryFindResource("MahApps.Storyboard.DialogClose") as Storyboard;

                //if (closingStoryboard == null)
                //{
                //    throw new InvalidOperationException("Unable to find the dialog closing storyboard. Did you forget to add BaseMetroDialog.xaml to your merged dictionaries?");
                //}

                //EventHandler handler = null;
                //handler = (sender, args) =>
                //{
                //    closingStoryboard.Completed -= handler;

                //    tcs.TrySetResult(null);
                //};

                //closingStoryboard = closingStoryboard.Clone();

                //closingStoryboard.Completed += handler;

                //closingStoryboard.Begin(this);
            }
            else
            {
                this.Opacity = 0.0;
                tcs.TrySetResult(null); //skip the animation
            }

            return tcs.Task;
        }




    }
}
