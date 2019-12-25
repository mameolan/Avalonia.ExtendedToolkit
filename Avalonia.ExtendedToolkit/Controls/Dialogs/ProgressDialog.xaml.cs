using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Threading;

namespace Avalonia.ExtendedToolkit.Controls.Dialogs
{
    public class ProgressDialog : BaseMetroDialog
    {
        internal ProgressBar PART_ProgressBar;
        internal Button PART_NegativeButton;

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }


        public static readonly AvaloniaProperty MessageProperty =
            AvaloniaProperty.Register<ProgressDialog, string>(nameof(Message));




        public bool IsCancelable
        {
            get { return (bool)GetValue(IsCancelableProperty); }
            set { SetValue(IsCancelableProperty, value); }
        }


        public static readonly AvaloniaProperty IsCancelableProperty =
            AvaloniaProperty.Register<ProgressDialog, bool>(nameof(IsCancelable));



        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }


        public static readonly AvaloniaProperty NegativeButtonTextProperty =
            AvaloniaProperty.Register<ProgressDialog, string>(nameof(NegativeButtonText));



        public IBrush ProgressBarForeground
        {
            get { return (IBrush)GetValue(ProgressBarForegroundProperty); }
            set { SetValue(ProgressBarForegroundProperty, value); }
        }


        public static readonly AvaloniaProperty ProgressBarForegroundProperty =
            AvaloniaProperty.Register<ProgressDialog, IBrush>(nameof(ProgressBarForeground));

        internal ProgressDialog()
            : this(null)
        {
        }

        internal ProgressDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }

        internal ProgressDialog(MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            this.InitializeComponent();
            PART_ProgressBar= this.FindControl<ProgressBar>(nameof(PART_ProgressBar));
            PART_NegativeButton = this.FindControl<Button>(nameof(PART_NegativeButton));
        }

        protected override void OnLoaded()
        {
            this.NegativeButtonText = this.DialogSettings.NegativeButtonText;
            //this.SetResourceReference(ProgressBarForegroundProperty, this.DialogSettings.ColorScheme == MetroDialogColorScheme.Theme ? "MahApps.Brushes.Accent" : "MahApps.Brushes.Black");
        }

        internal CancellationToken CancellationToken => this.DialogSettings.CancellationToken;

        internal double Minimum
        {
            get { return this.PART_ProgressBar.Minimum; }
            set { this.PART_ProgressBar.Minimum = value; }
        }

        internal double Maximum
        {
            get { return this.PART_ProgressBar.Maximum; }
            set { this.PART_ProgressBar.Maximum = value; }
        }

        internal double ProgressValue
        {
            get { return this.PART_ProgressBar.Value; }
            set
            {
                this.PART_ProgressBar.IsIndeterminate = false;
                this.PART_ProgressBar.Value = value;
                this.PART_ProgressBar.ApplyTemplate();
            }
        }

        internal void SetIndeterminate()
        {
            this.PART_ProgressBar.IsIndeterminate = true;
        }







        //public ProgressDialog()
        //{
        //    this.InitializeComponent();
        //}

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
