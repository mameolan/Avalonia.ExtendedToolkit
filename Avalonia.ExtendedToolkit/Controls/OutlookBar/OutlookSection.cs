using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    public class OutlookSection : HeaderedContentControl
    {
        public Type StyleKey => typeof(OutlookSection);


        /// <summary>
        /// Gets or sets the OutlookBar to which this Section is assigned.
        /// </summary>
        internal OutlookBar OutlookBar { get; set; }

        public IImage Image
        {
            get { return (IImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly StyledProperty<IImage> ImageProperty =
            AvaloniaProperty.Register<OutlookSection, IImage>(nameof(Image));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<OutlookSection, bool>(nameof(IsSelected));

        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            set { SetValue(IsMaximizedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsMaximizedProperty =
            AvaloniaProperty.Register<OutlookSection, bool>(nameof(IsMaximized), defaultValue: true);

        public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
                    RoutedEvent.Register<OutlookSection, RoutedEventArgs>(nameof(ClickEvent), RoutingStrategies.Bubble);

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

        protected virtual void OnSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (OutlookBar == null)
                return;

            if (newValue) OutlookBar.SelectedSection = this;

            PseudoClasses.Set(":checked", newValue);
        }

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

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            Button button=e.NameScope.Find<Button>("button");
            button.Click += buttonClickedEvent;
        }
    }
}
