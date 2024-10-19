using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    // http://davidowens.wordpress.com/2009/02/18/wpf-search-text-box/
    /// <summary>
    /// Property search box control.
    /// </summary>
    public class SearchTextBox : TextBox
    {
        private const string DefaultLabelText = "Search";

        private readonly DispatcherTimer _searchEventDelayTimer;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(SearchTextBox);

        /// <summary>
        /// Gets or sets the search mode.
        /// </summary>
        public SearchMode SearchMode
        {
            get { return (SearchMode)GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }

        /// <summary>
        /// <see cref="SearchMode"/>
        /// </summary>
        public static readonly StyledProperty<SearchMode> SearchModeProperty =
            AvaloniaProperty.Register<SearchTextBox, SearchMode>(nameof(SearchMode)
                , defaultValue: SearchMode.Instant);

        /// <summary>
        /// Gets a value indicating whether this instance has text.
        /// </summary>
        /// <value><c>true</c> if this instance has text;
        ///     otherwise, <c>false</c>.</value>
        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            set { SetValue(HasTextProperty, value); }
        }

        /// <summary>
        /// <see cref="HasText"/>
        /// </summary>
        public static readonly StyledProperty<bool> HasTextProperty =
            AvaloniaProperty.Register<SearchTextBox, bool>(nameof(HasText));

        /// <summary>
        /// Gets or sets a value indicating whether mouse left button is down.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if mouse left button is down; otherwise, <c>false</c>.
        /// </value>
        public bool IsMouseLeftButtonDown
        {
            get { return (bool)GetValue(IsMouseLeftButtonDownProperty); }
            set { SetValue(IsMouseLeftButtonDownProperty, value); }
        }

        /// <summary>
        /// <see cref="IsMouseLeftButtonDown"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsMouseLeftButtonDownProperty =
            AvaloniaProperty.Register<SearchTextBox, bool>(nameof(IsMouseLeftButtonDown));

        /// <summary>
        /// Gets or sets the search event time delay.
        /// </summary>
        /// <value>The search event time delay.</value>
        public TimeSpan SearchEventTimeDelay
        {
            get { return (TimeSpan)GetValue(SearchEventTimeDelayProperty); }
            set { SetValue(SearchEventTimeDelayProperty, value); }
        }

        /// <summary>
        /// <see cref="SearchEventTimeDelay"/>
        /// </summary>
        public static readonly StyledProperty<TimeSpan> SearchEventTimeDelayProperty =
            AvaloniaProperty.Register<SearchTextBox, TimeSpan>(nameof(SearchEventTimeDelay)
                , defaultValue: TimeSpan.FromMilliseconds(500));


        /// <summary>
        /// <see cref="Search"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> SearchEvent =
                    RoutedEvent.Register<SearchTextBox, RoutedEventArgs>(nameof(SearchEvent),
                        RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when search is performed.
        /// </summary>
        public event EventHandler Search
        {
            add
            {
                AddHandler(SearchEvent, value);
            }
            remove
            {
                RemoveHandler(SearchEvent, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchTextBox"/> class.
        /// </summary>
        public SearchTextBox()
        {
            _searchEventDelayTimer = new DispatcherTimer { Interval = SearchEventTimeDelay };
            _searchEventDelayTimer.Tick += OnSeachEventDelayTimerTick;

            SearchEventTimeDelayProperty.Changed.AddClassHandler<SearchTextBox>(((o, e) => OnSearchEventTimeDelayChanged(o, e)));
            TextProperty.Changed.AddClassHandler<SearchTextBox>(((o, e) => OnTextChanged(o, e)));
        }

        /// <summary>
        /// Is called when content in this editing control changes.
        /// </summary>
        private void OnTextChanged(SearchTextBox searchTextBox, AvaloniaPropertyChangedEventArgs e)
        {
            HasText = Text.Length != 0;

            if (SearchMode == SearchMode.Instant)
            {
                _searchEventDelayTimer.Stop();
                _searchEventDelayTimer.Start();
            }
        }

        private void OnSeachEventDelayTimerTick(object o, EventArgs e)
        {
            _searchEventDelayTimer.Stop();
            RaiseSearchEvent();
        }

        private void OnSearchEventTimeDelayChanged(SearchTextBox searchTextBox, AvaloniaPropertyChangedEventArgs e)
        {
            if (searchTextBox != null)
            {
                searchTextBox._searchEventDelayTimer.Interval = ((TimeSpan)e.NewValue);
                searchTextBox._searchEventDelayTimer.Stop();
            }
        }

        /// <summary>
        /// gets the border from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            var iconBorder = e.NameScope.Find<Border>("PART_SearchIconBorder");
            if (iconBorder != null)
            {
                iconBorder.PointerPressed += IconBorderMouseLeftButtonDown;
                iconBorder.PointerReleased += IconBorderMouseLeftButtonUp;
                iconBorder.PointerExited += IconBorderMouseLeave;
                ;
            }
            base.OnApplyTemplate(e);
        }

        private void IconBorderMouseLeave(object sender, Input.PointerEventArgs e)
        {
            IsMouseLeftButtonDown = false;
        }

        private void IconBorderMouseLeftButtonUp(object sender, Input.PointerReleasedEventArgs e)
        {
            var prop = e.GetCurrentPoint(sender as Visual).Properties;
            if (IsMouseLeftButtonDown==false)
            {
                return;
            }

            if (!IsMouseLeftButtonDown)
                return;

            if (HasText && SearchMode == SearchMode.Instant)
            {
                Text = "";
            }

            if (HasText && SearchMode == SearchMode.Delayed)
            {
                RaiseSearchEvent();
            }

            IsMouseLeftButtonDown = false;
        }

        private void IconBorderMouseLeftButtonDown(object sender, Input.PointerPressedEventArgs e)
        {
            var prop = e.GetCurrentPoint(sender as Visual).Properties;
            if (prop.IsLeftButtonPressed)
            {
                IsMouseLeftButtonDown = true;
            }
            else
            {
                IsMouseLeftButtonDown = false;
            }
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="InputElement.KeyDown"/>
        /// attached routed event reaches an element derived from this class in its route.
        /// Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">Provides data about the event.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape && SearchMode == SearchMode.Instant)
            {
                Text = "";
            }
            else if ((e.Key == Key.Return || e.Key == Key.Enter) &&
                SearchMode == SearchMode.Delayed)
            {
                RaiseSearchEvent();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void RaiseSearchEvent()
        {
            var args = new RoutedEventArgs(SearchEvent);
            RaiseEvent(args);
        }

        /// <summary>
        /// Called when one or more of the avalonia properties
        /// that exist on the element have had their effective values changed.
        /// </summary>
        /// <param name="e">Arguments for the associated event.</param>
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if(e.Property== IsVisibleProperty)
            {
                Text = string.Empty;
            }

            base.OnPropertyChanged(e);
        }
    }
}
