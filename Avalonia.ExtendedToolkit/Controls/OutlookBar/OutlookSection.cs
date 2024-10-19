using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// Outlook Section
    /// </summary>
    public class OutlookSection : HeaderedContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(OutlookSection);

        /// <summary>
        /// Gets or sets the OutlookBar to which this Section is assigned.
        /// </summary>
        internal OutlookBar OutlookBar { get; set; }

        /// <summary>
        /// get/sets the image
        /// </summary>
        public Bitmap Image
        {
            get { return (Bitmap)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// <see cref="Image"/>
        /// </summary>
        public static readonly StyledProperty<Bitmap> ImageProperty =
            AvaloniaProperty.Register<OutlookSection, Bitmap>(nameof(Image));

        /// <summary>
        /// get/sets IsSelected
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsSelected"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<OutlookSection, bool>(nameof(IsSelected));

        /// <summary>
        /// get/sets IsMaximized
        /// </summary>
        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            set { SetValue(IsMaximizedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsMaximized"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsMaximizedProperty =
            AvaloniaProperty.Register<OutlookSection, bool>(nameof(IsMaximized), defaultValue: true);

        /// <summary>
        /// <see cref="Click"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
                    RoutedEvent.Register<OutlookSection, RoutedEventArgs>(nameof(ClickEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/set Click event
        /// </summary>
        public event EventHandler Click
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        /// <summary>
        /// sets the SelectedSection to the OutlookBar
        /// and set the pseudo class
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (OutlookBar == null)
                return;

            if (newValue) OutlookBar.SelectedSection = this;

            PseudoClasses.Set(":checked", newValue);
        }

        /// <summary>
        /// registers changed handlers
        /// </summary>
        public OutlookSection()
        {
            //AddHandler(Button.PointerPressedEvent, buttonClickedEvent);
            IsSelectedProperty.Changed.AddClassHandler<OutlookSection>((o, e) => IsSelectedPropertyChanged(o, e));
        }

        private void IsSelectedPropertyChanged(OutlookSection o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnSelectedPropertyChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void buttonClickedEvent(object sender, RoutedEventArgs e)
        {
            OutlookBar bar = OutlookBar;
            ToggleButton b = e.Source as ToggleButton;
            if (b != null) b.IsChecked = true;
            if (bar != null)
            {
                bar.SelectedSection = this;
            }
            OnClick();
        }

        private void OnClick()
        {
            this.RaiseEvent(new RoutedEventArgs(OutlookSection.ClickEvent));
        }

        /// <summary>
        /// gets the buttom from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            Button button=e.NameScope.Find<Button>("button");
            button.Click += buttonClickedEvent;
        }
    }
}
