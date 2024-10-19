using System;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/HandyOrg/HandyControl.git

    /// <summary>
    /// holds a collection of StepBarItems
    /// </summary>
    public class StepBar : ItemsControl
    {
        private const string ElementProgressBarBack = "PART_ProgressBarBack";
        private ProgressBar _progressBarBack;
        private int _oriStepIndex = -1;
        private Size _finalSize;

        /// <summary>
        /// Defines the StepChanged routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgsOfT<int>> StepChangedEvent =
        RoutedEvent.Register<StepBar, RoutedEventArgsOfT<int>>(nameof(StepChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets StepChanged eventhandler.
        /// </summary>
        public event EventHandler<RoutedEventArgsOfT<int>> StepChanged
        {
            add
            {
                AddHandler(StepChangedEvent, value);
            }
            remove
            {
                RemoveHandler(StepChangedEvent, value);
            }
        }

        /// <summary>
        /// Gets or sets StepIndex.
        /// </summary>
        public int StepIndex
        {
            get { return (int)GetValue(StepIndexProperty); }
            set { SetValue(StepIndexProperty, value); }
        }

        /// <summary>
        /// Defines the StepIndex property.
        /// </summary>
        public static readonly StyledProperty<int> StepIndexProperty =
        AvaloniaProperty.Register<StepBar, int>(nameof(StepIndex), defaultValue: 0,
        coerce: (o, e) => { return CoerceStepIndex(o, e); });

        private static int CoerceStepIndex(AvaloniaObject ctrl, int stepIndex)
        {
            StepBar stepBar = ctrl as StepBar;
            int itemsCount = stepBar.Items.OfType<object>().Count();

            if (itemsCount == 0 && stepIndex > 0)
            {
                stepBar._oriStepIndex = stepIndex;
                return 0;
            }

            return stepIndex < 0
                ? 0
                : stepIndex >= itemsCount
                    ? itemsCount == 0 ? 0 : itemsCount - 1
                    : stepIndex;
        }

        /// <summary>
        /// Gets or sets Dock.
        /// </summary>
        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        /// <summary>
        /// Defines the Dock property.
        /// </summary>
        public static readonly StyledProperty<Dock> DockProperty =
        AvaloniaProperty.Register<StepBar, Dock>(nameof(Dock), defaultValue: Dock.Top);

        /// <summary>
        /// Gets or sets ProgressBackground.
        /// </summary>
        public IBrush ProgressBackground
        {
            get { return (IBrush)GetValue(ProgressBackgroundProperty); }
            set { SetValue(ProgressBackgroundProperty, value); }
        }

        /// <summary>
        /// Defines the ProgressBackground property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ProgressBackgroundProperty =
        AvaloniaProperty.Register<StepBar, IBrush>(nameof(ProgressBackground));

        /// <summary>
        /// Gets or sets ProgressForeground.
        /// </summary>
        public IBrush ProgressForeground
        {
            get { return (IBrush)GetValue(ProgressForegroundProperty); }
            set { SetValue(ProgressForegroundProperty, value); }
        }

        /// <summary>
        /// Defines the ProgressForeground property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ProgressForegroundProperty =
        AvaloniaProperty.Register<StepBar, IBrush>(nameof(ProgressForeground));

        /// <summary>
        /// gets the final size
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            _finalSize = finalSize;
            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// command which moves to next step
        /// </summary>
        public ICommand NextCommand { get; set; }

        /// <summary>
        /// command which moves to previous step
        /// </summary>
        public ICommand PreviousCommand { get; set; }

        /// <summary>
        /// registers events and commands
        /// </summary>
        public StepBar()
        {
            NextCommand = ReactiveCommand.Create(() => Next(), outputScheduler: RxApp.MainThreadScheduler);
            PreviousCommand = ReactiveCommand.Create(() => Prev(), outputScheduler: RxApp.MainThreadScheduler);
            //.orig
            //ItemContainerGenerator.Materialized += ItemContainerGenerator_StatusChanged;
            StepIndexProperty.Changed.AddClassHandler<StepBar>((o, e) => OnStepIndexChanged(o, e));
        }

        /// <summary>
        /// returns <see cref="ItemContainerGenerator"/> of type <see cref="StepBarItem"/>
        /// </summary>
        /// <returns></returns>
        //.orig
        //protected override ItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    return new ItemContainerGenerator<StepBarItem>(
        //        this,
        //        StepBarItem.ContentProperty,
        //        StepBarItem.ContentTemplateProperty);
        //}

        /// <summary>
        /// sets the step index of the items and property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //.orig
        //private void ItemContainerGenerator_StatusChanged(object sender, ItemContainerEventArgs e)
        //{
        //    var count = Items.OfType<StepBarItem>().Count();

        //    if (count <= 0)
        //        return;

        //    for (var i = 0; i < count; i++)
        //    {
        //        if (ItemContainerGenerator.ContainerFromIndex(i) is StepBarItem stepBarItem)
        //        {
        //            stepBarItem.Index = i + 1;
        //        }
        //    }

        //    if (_oriStepIndex > 0)
        //    {
        //        StepIndex = _oriStepIndex;
        //        _oriStepIndex = -1;
        //    }
        //    else
        //    {
        //        OnStepIndexChanged(StepIndex);
        //    }
        //}

        /// <summary>
        /// calls <see cref="OnStepIndexChanged(int)"/>
        /// </summary>
        /// <param name="stepBar"></param>
        /// <param name="e"></param>
        private void OnStepIndexChanged(StepBar stepBar, AvaloniaPropertyChangedEventArgs e)
        {
            stepBar.OnStepIndexChanged((int)e.NewValue);
        }

        /// <summary>
        /// sets the status of the items by stepindex
        /// </summary>
        /// <param name="stepIndex"></param>
        private void OnStepIndexChanged(int stepIndex)
        {
            for (var i = 0; i < stepIndex; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is StepBarItem stepItemFinished)
                {
                    stepItemFinished.Status = StepStatus.Complete;
                }
            }

            for (var i = stepIndex + 1; i < Items.OfType<object>().Count(); i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is StepBarItem stepItemFinished)
                {
                    stepItemFinished.Status = StepStatus.Waiting;
                }
            }

            if (ItemContainerGenerator.ContainerFromIndex(stepIndex) is StepBarItem stepItemSelected)
                stepItemSelected.Status = StepStatus.UnderWay;

            RaiseEvent(new RoutedEventArgsOfT<int>(StepChangedEvent, this)
            {
                Info = stepIndex
            });

            UpdateProgressBar();
        }

        /// <summary>
        /// increments the <see cref="StepIndex"/>
        /// </summary>
        public void Next() => StepIndex++;

        /// <summary>
        /// decrements the <see cref="StepIndex"/>
        /// </summary>
        public void Prev() => StepIndex--;

        /// <summary>
        /// gets the progressbar from style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _progressBarBack = e.NameScope.Find<ProgressBar>(ElementProgressBarBack);
        }

        /// <summary>
        /// sets the progress Maximum and value
        /// </summary>
        private void UpdateProgressBar()
        {
            var width = _finalSize.Width;
            var height = _finalSize.Height;

            var colCount = Items.OfType<StepBarItem>().Count();

            if (_progressBarBack == null || colCount <= 0)
            {
                return;
            }

            _progressBarBack.Maximum = colCount - 1;
            _progressBarBack.Value = StepIndex;

            if (Dock == Dock.Top || Dock == Dock.Bottom)
            {
                _progressBarBack.Width = (colCount - 1) * (width / colCount);
            }
            else
            {
                _progressBarBack.Height = (colCount - 1) * (height / colCount);
            }
        }

        /// <summary>
        /// calls <see cref="UpdateProgressBar"/>
        /// </summary>
        /// <param name="context"></param>
        public override void Render(DrawingContext context)
        {
            base.Render(context);

            UpdateProgressBar();
        }
    }
}
