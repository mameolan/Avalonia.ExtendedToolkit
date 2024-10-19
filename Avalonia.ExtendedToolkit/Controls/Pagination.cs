using System;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/HandyOrg/HandyControl.git

    /// <summary>
    /// control for navigation through a collection
    /// </summary>
    public class Pagination : TemplatedControl
    {
        private const string ElementButtonLeft = "PART_ButtonLeft";
        private const string ElementButtonRight = "PART_ButtonRight";
        private const string ElementButtonFirst = "PART_ButtonFirst";
        private const string ElementMoreLeft = "PART_MoreLeft";
        private const string ElementPanelMain = "PART_PanelMain";
        private const string ElementMoreRight = "PART_MoreRight";
        private const string ElementButtonLast = "PART_ButtonLast";

        private Button _buttonLeft;
        private Button _buttonRight;
        private RadioButton _buttonFirst;
        private Layoutable _moreLeft;
        private Panel _panelMain;
        private Layoutable _moreRight;
        private RadioButton _buttonLast;

        private bool _appliedTemplate;

        /// <summary>
        /// Gets or sets HighlightBorderBrush.
        /// </summary>
        public IBrush HighlightBorderBrush
        {
            get { return (IBrush)GetValue(HighlightBorderBrushProperty); }
            set { SetValue(HighlightBorderBrushProperty, value); }
        }

        /// <summary>
        /// Defines the HighlightBorderBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HighlightBorderBrushProperty =
        AvaloniaProperty.Register<Pagination, IBrush>(nameof(HighlightBorderBrush));

        /// <summary>
        /// Gets or sets HighlightBackground.
        /// </summary>
        public IBrush HighlightBackground
        {
            get { return (IBrush)GetValue(HighlightBackgroundProperty); }
            set { SetValue(HighlightBackgroundProperty, value); }
        }

        /// <summary>
        /// Defines the HighlightBackground property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HighlightBackgroundProperty =
        AvaloniaProperty.Register<Pagination, IBrush>(nameof(HighlightBackground));

        /// <summary>
        /// Gets or sets HighlightForeground.
        /// </summary>
        public IBrush HighlightForeground
        {
            get { return (IBrush)GetValue(HighlightForegroundProperty); }
            set { SetValue(HighlightForegroundProperty, value); }
        }

        /// <summary>
        /// Defines the HighlightForeground property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HighlightForegroundProperty =
        AvaloniaProperty.Register<Pagination, IBrush>(nameof(HighlightForeground));

        /// <summary>
        /// Gets or sets CornerRadius.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Defines the CornerRadius property.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
        AvaloniaProperty.Register<Pagination, CornerRadius>(nameof(CornerRadius));


        /// <summary>
        /// Gets or sets MaxPageCount.
        /// </summary>
        public int MaxPageCount
        {
            get { return (int)GetValue(MaxPageCountProperty); }
            set { SetValue(MaxPageCountProperty, value); }
        }

        /// <summary>
        /// Defines the MaxPageCount property.
        /// </summary>
        public static readonly StyledProperty<int> MaxPageCountProperty =
        AvaloniaProperty.Register<Pagination, int>(nameof(MaxPageCount), defaultValue: 1,
            coerce: (o, e) => MaxPageCountValidate(o, e));

        private static int MaxPageCountValidate(AvaloniaObject o, int value)
        {
            var intValue = (int)value;
            if (intValue < 1)
            {
                return 1;
            }
            return intValue;
        }

        /// <summary>
        /// Gets or sets DataCountPerPage.
        /// </summary>
        public int DataCountPerPage
        {
            get { return (int)GetValue(DataCountPerPageProperty); }
            set { SetValue(DataCountPerPageProperty, value); }
        }

        /// <summary>
        /// Defines the DataCountPerPage property.
        /// </summary>
        public static readonly StyledProperty<int> DataCountPerPageProperty =
        AvaloniaProperty.Register<Pagination, int>(nameof(DataCountPerPage), defaultValue: 20
            , coerce: (o, e) => DataCountPerPageValidate(o, e));

        private static int DataCountPerPageValidate(AvaloniaObject pagination, int value)
        {
            var intValue = (int)value;
            if (intValue < 1)
            {
                return 1;
            }
            return intValue;
        }

        /// <summary>
        /// Gets or sets PageIndex.
        /// </summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        /// <summary>
        /// Defines the PageIndex property.
        /// </summary>
        public static readonly StyledProperty<int> PageIndexProperty =
        AvaloniaProperty.Register<Pagination, int>(nameof(PageIndex), defaultValue: 1,
            coerce: (o, e) => PageIndexValidate(o, e));

        private static int PageIndexValidate(AvaloniaObject o, int value)
        {
            Pagination pagination = o as Pagination;

            var intValue = (int)value;
            if (intValue < 0)
            {
                return 0;
            }
            if (intValue > pagination.MaxPageCount)
            {
                return pagination.MaxPageCount;
            }
            return intValue;
        }

        /// <summary>
        /// Gets or sets MaxPageInterval.
        /// </summary>
        public int MaxPageInterval
        {
            get { return (int)GetValue(MaxPageIntervalProperty); }
            set { SetValue(MaxPageIntervalProperty, value); }
        }

        /// <summary>
        /// Defines the MaxPageInterval property.
        /// </summary>
        public static readonly StyledProperty<int> MaxPageIntervalProperty =
        AvaloniaProperty.Register<Pagination, int>(nameof(MaxPageInterval), defaultValue: 3);





        /// <summary>
        /// Defines the PageUpdated routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<int>> PageUpdatedEvent =
        RoutedEvent.Register<Pagination, RoutedPropertyChangedEventArgs<int>>(nameof(PageUpdatedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets PageUpdated eventhandler.
        /// </summary>
        public event EventHandler<RoutedPropertyChangedEventArgs<int>> PageUpdated
        {
            add
            {
                AddHandler(PageUpdatedEvent, value);
            }
            remove
            {
                RemoveHandler(PageUpdatedEvent, value);
            }
        }

        /// <summary>
        /// Gets or sets PageUpdatedCommand.
        /// </summary>
        public ICommand PageUpdatedCommand
        {
            get { return (ICommand)GetValue(PageUpdatedCommandProperty); }
            set { SetValue(PageUpdatedCommandProperty, value); }
        }

        /// <summary>
        /// Defines the PageUpdatedCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> PageUpdatedCommandProperty =
        AvaloniaProperty.Register<Pagination, ICommand>(nameof(PageUpdatedCommand));



        /// <summary>
        /// Defines the PreviousCommand direct property.
        /// </summary>
        public static readonly DirectProperty<Pagination, ICommand> PreviousCommandProperty =
        AvaloniaProperty.RegisterDirect<Pagination, ICommand>(
        nameof(PreviousCommand),
        o => o.PreviousCommand);

        private ICommand _previousCommand;

        /// <summary>
        /// Gets or sets PreviousCommand.
        /// </summary>
        public ICommand PreviousCommand
        {
            get { return _previousCommand; }
            private set
            {
                SetAndRaise(PreviousCommandProperty, ref _previousCommand, value);
            }
        }

        /// <summary>
        /// Defines the NextCommand direct property.
        /// </summary>
        public static readonly DirectProperty<Pagination, ICommand> NextCommandProperty =
        AvaloniaProperty.RegisterDirect<Pagination, ICommand>(
        nameof(NextCommand),
        o => o.NextCommand);

        private ICommand _nextCommand;

        /// <summary>
        /// Gets or sets NextCommand.
        /// </summary>
        public ICommand NextCommand
        {
            get { return _nextCommand; }
            private set
            {
                SetAndRaise(NextCommandProperty, ref _nextCommand, value);
            }
        }

        /// <summary>
        /// Defines the SelectedCommand direct property.
        /// </summary>
        public static readonly DirectProperty<Pagination, ICommand> SelectedCommandProperty =
        AvaloniaProperty.RegisterDirect<Pagination, ICommand>(
        nameof(SelectedCommand),
        o => o.SelectedCommand);

        private ICommand _selectedCommand;

        /// <summary>
        /// Gets or sets SelectedCommand.
        /// </summary>
        public ICommand SelectedCommand
        {
            get { return _selectedCommand; }
            private set
            {
                SetAndRaise(SelectedCommandProperty, ref _selectedCommand, value);
            }
        }

        /// <summary>
        /// adds somr changed listeners
        /// and registers some commands
        /// </summary>
        public Pagination()
        {
            MaxPageCountProperty.Changed.AddClassHandler<Pagination>((o, e) => OnMaxPageCountChanged(o, e));
            DataCountPerPageProperty.Changed.AddClassHandler<Pagination>((o, e) => OnDataCountPerPageChanged(o, e));
            PageIndexProperty.Changed.AddClassHandler<Pagination>((o, e) => OnPageIndexChanged(o, e));
            MaxPageIntervalProperty.Changed.AddClassHandler<Pagination>((o, e) => OnMaxPageIntervalChanged(o, e));

            PreviousCommand = ReactiveCommand.Create(ExecutePreviousCommand, outputScheduler: RxApp.MainThreadScheduler);
            NextCommand = ReactiveCommand.Create(ExecuteNextCommand, outputScheduler: RxApp.MainThreadScheduler);
            SelectedCommand = ReactiveCommand.Create<object>((x) => ExecuteSelectedCommand(x), outputScheduler: RxApp.MainThreadScheduler);

        }

        private void ExecuteSelectedCommand(object sender)
        {
            if (!(sender is RadioButton button))
            {
                return;
            }

            if (button.IsChecked == false)
            {
                return;
            }

            int result = 0;
            if (int.TryParse(button.Content.ToString(), out result))
            {
                PageIndex = result;
            }


        }

        private void ExecuteNextCommand()
        {
            PageIndex++;
        }

        private void ExecutePreviousCommand()
        {
            PageIndex--;
        }

        private void OnMaxPageIntervalChanged(Pagination pagination, AvaloniaPropertyChangedEventArgs e)
        {
            pagination?.Update();
        }

        private void OnPageIndexChanged(Pagination pagination, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is int value)
            {
                pagination.Update();

                pagination.RaiseEvent(new RoutedPropertyChangedEventArgs<int>(value, value, PageUpdatedEvent));

                pagination?.PageUpdatedCommand?.Execute(value);

            }
        }

        private void OnDataCountPerPageChanged(Pagination pagination, AvaloniaPropertyChangedEventArgs e)
        {
            pagination.Update();
        }

        private void OnMaxPageCountChanged(Pagination pagination, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is int value)
            {
                if (pagination.PageIndex > pagination.MaxPageCount)
                {
                    pagination.PageIndex = pagination.MaxPageCount;
                }

                pagination.IsVisible = (value > 1);
                pagination.Update();
            }
        }

        /// <summary>
        /// resolves some controls from the style
        /// </summary>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            _appliedTemplate = false;
            base.OnApplyTemplate(e);
            _buttonLeft = e.NameScope.Find<Button>(ElementButtonLeft);
            _buttonRight = e.NameScope.Find<Button>(ElementButtonRight);
            _buttonFirst = e.NameScope.Find<RadioButton>(ElementButtonFirst);
            _moreLeft = e.NameScope.Find<Layoutable>(ElementMoreLeft);
            _panelMain = e.NameScope.Find<Panel>(ElementPanelMain);
            _moreRight = e.NameScope.Find<Layoutable>(ElementMoreRight);
            _buttonLast = e.NameScope.Find<RadioButton>(ElementButtonLast);

            CheckNull();

            _buttonFirst.Checked += (o, e) =>
            {
                SelectedCommand.Execute(_buttonFirst);
            };

            _buttonLast.Checked += (o, e) =>
            {
                SelectedCommand.Execute(_buttonLast);
            };


            _appliedTemplate = true;
            Update();
        }

        private void CheckNull()
        {
            if (_buttonLeft == null || _buttonRight == null || _buttonFirst == null ||
                _moreLeft == null || _panelMain == null || _moreRight == null ||
                _buttonLast == null)
                throw new Exception();
        }

        private void Update()
        {
            if (!_appliedTemplate)
                return;
            _buttonLeft.IsEnabled = PageIndex > 1;
            _buttonRight.IsEnabled = PageIndex < MaxPageCount;
            if (MaxPageInterval == 0)
            {
                _buttonFirst.IsVisible = false;
                _buttonLast.IsVisible = false;
                _moreLeft.IsVisible = false;
                _moreRight.IsVisible = false;
                _panelMain.Children.OfType<RadioButton>().ForEach(button => button.Checked -= OnRadioButtonChecked);
                _panelMain.Children.Clear();
                var selectButton = CreateButton(PageIndex);
                _panelMain.Children.Add(selectButton);
                selectButton.IsChecked = true;
                return;
            }
            _buttonFirst.IsVisible = true;
            _buttonLast.IsVisible = true;
            _moreLeft.IsVisible = true;
            _moreRight.IsVisible = true;


            if (MaxPageCount == 1)
            {
                _buttonLast.IsVisible = false;
            }
            else
            {
                _buttonLast.IsVisible = true;
                _buttonLast.Content = MaxPageCount.ToString();
            }


            var right = MaxPageCount - PageIndex;
            var left = PageIndex - 1;
            _moreRight.IsVisible = (right > MaxPageInterval);
            _moreLeft.IsVisible = (left > MaxPageInterval);
            _panelMain.Children.OfType<RadioButton>().ForEach(button => button.Checked -= OnRadioButtonChecked);
            _panelMain.Children.Clear();
            if (PageIndex > 1 && PageIndex < MaxPageCount)
            {
                var selectButton = CreateButton(PageIndex);
                _panelMain.Children.Add(selectButton);
                selectButton.IsChecked = true;
            }
            else if (PageIndex == 1)
            {
                _buttonFirst.IsChecked = true;
            }
            else
            {
                _buttonLast.IsChecked = true;
            }

            var sub = PageIndex;
            for (var i = 0; i < MaxPageInterval - 1; i++)
            {
                if (--sub > 1)
                {
                    _panelMain.Children.Insert(0, CreateButton(sub));
                }
                else
                {
                    break;
                }
            }
            var add = PageIndex;
            for (var i = 0; i < MaxPageInterval - 1; i++)
            {
                if (++add < MaxPageCount)
                {
                    _panelMain.Children.Add(CreateButton(add));
                }
                else
                {
                    break;
                }
            }
        }







        private RadioButton CreateButton(int page)
        {
            RadioButton button = new RadioButton();
            button.Classes.Add("PaginationButtonStyle");
            button.Content = page.ToString();
            button.Checked += OnRadioButtonChecked;
            return button;
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            SelectedCommand?.Execute(sender);
        }


    }
}
