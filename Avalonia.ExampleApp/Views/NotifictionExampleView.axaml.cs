using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ExtendedToolkit.Controls;
using System;

namespace Avalonia.ExampleApp.Views
{
    public partial class NotifictionExampleView : UserControl
    {
        public NotifictionExampleView()
        {
            InitializeComponent();
            DataContext = this;


            this.FindControl<Button>("ButtonInfo").Click += ButtonInfoOnClick;
            this.FindControl<Button>("ButtonWarning").Click += ButtonWarningOnClick;
            this.FindControl<Button>("ButtonError").Click += ButtonErrorOnClick;
            this.FindControl<Button>("ButtonInfoDelay").Click += ButtonInfoDelayOnClick;
            this.FindControl<Button>("ButtonAdditionalContent").Click += ButtonAdditionalContentOnClick;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        private void ButtonInfoDelayOnClick(object sender, RoutedEventArgs e)
        {
            Manager
                .CreateMessage()
                .Accent("#1751C3")
                .Animates(true)
                .AnimationInDuration(0.75)
                .AnimationOutDuration(2)
                .Background("#333333")
                .HasBadge("Info")
                .HasMessage("Update will be installed on next application restart. This message will be dismissed after 5 seconds.")
                .Dismiss().WithButton("Update now", button => { Console.WriteLine("Update now"); })
                .Dismiss().WithButton("Release notes", button => { Console.WriteLine("Release notes"); })
                .Dismiss().WithDelay(TimeSpan.FromSeconds(5))
                .Queue();
        }

        private void ButtonErrorOnClick(object sender, RoutedEventArgs e)
        {
            Manager
                .CreateMessage()
                .Accent("#F15B19")
                .Background("#F15B19")
                .HasHeader("Lost connection to server")
                .HasMessage("Reconnecting...")
                .WithOverlay(new ProgressBar
                {
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 1,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255)),
                    Background = Brushes.Transparent,
                    IsIndeterminate = true,
                    IsHitTestVisible = false
                })
                .Queue(); 
        }

        private void ButtonInfoOnClick(object sender, RoutedEventArgs e)
        {
            Manager
                .CreateMessage()
                .MaxItems(3)
                .Accent("#1751C3")
                .Background("#333333")
                .HasBadge("Info")
                .HasMessage("Update will be installed on next application restart.")
                .Dismiss().WithButton("Update now", button => { Console.WriteLine("Update now"); })
                .Dismiss().WithButton("Release notes", button => { Console.WriteLine("Release notes"); })
                .Dismiss().WithButton("Later", button => { Console.WriteLine("Later"); })
                .Queue();
        }

        private void ButtonWarningOnClick(object sender, RoutedEventArgs e)
        {
            Manager
                .CreateMessage()
                .Accent("#E0A030")
                .Background("#333333")
                .HasBadge("Warn")
                .HasHeader("Error")
                .HasMessage("Failed to retrieve data.")
                .WithButton("Try again", button => { Console.WriteLine("Try again"); })
                .Dismiss().WithButton("Ignore", button => { Console.WriteLine("Ignore"); })
                .Queue();
        }

        private void ButtonAdditionalContentOnClick(object sender, RoutedEventArgs e)
        {
            Manager
                .CreateMessage()
                .Accent("#1751C3")
                .Background("#333333")
                .Foreground("#333333")
                .HasBadge("Info")
                .HasHeader("Header")
                .HasMessage("This is the message!")
                .WithAdditionalContent(ContentLocation.Top, new Border
                {
                    Height = 25,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Brushes.Red
                })
                .WithAdditionalContent(ContentLocation.Bottom, new Border
                {
                    Height = 25,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Brushes.Green
                })
                .WithAdditionalContent(ContentLocation.Left, new Border
                {
                    Width = 25,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Brushes.Yellow
                })
                .WithAdditionalContent(ContentLocation.Right, new Border
                {
                    Width = 25,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Brushes.Violet
                })
                .WithAdditionalContent(ContentLocation.Main, new Border
                {
                    MinHeight = 50,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Brushes.Orange
                })
                .WithAdditionalContent(ContentLocation.AboveBadge, new Border
                {
                    Height = 40,
                    Width = 40,
                    Background = Brushes.Indigo
                })
                .Dismiss().WithButton("Dismiss", button => { Console.WriteLine("Dismiss"); })
                .Queue();
        }


        

        /// <summary>
        /// Gets the notification message manager.
        /// </summary>
        /// <value>
        /// The notification message manager.
        /// </value>
        public INotificationMessageManager Manager { get; } = new NotificationMessageManager();

    }
}
