using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/HandyOrg/HandyControl.git

    /// <summary>
    /// holds a collection of <see cref="RateItem"/>
    /// </summary>
    public class RatingControl : RegularItemsControl
    {
        private bool _updateItems;

        private bool _isLoaded;

        /// <summary>
        /// Gets or sets AllowHalf.
        /// </summary>
        public bool AllowHalf
        {
            get { return (bool)GetValue(AllowHalfProperty); }
            set { SetValue(AllowHalfProperty, value); }
        }

        /// <summary>
        /// Defines the AllowHalf property.
        /// </summary>
        public static readonly StyledProperty<bool> AllowHalfProperty =
        AvaloniaProperty.Register<RatingControl, bool>(nameof(AllowHalf), defaultValue: false);

        /// <summary>
        /// Gets or sets AllowClear.
        /// </summary>
        public bool AllowClear
        {
            get { return (bool)GetValue(AllowClearProperty); }
            set { SetValue(AllowClearProperty, value); }
        }

        /// <summary>
        /// Defines the AllowClear property.
        /// </summary>
        public static readonly StyledProperty<bool> AllowClearProperty =
        AvaloniaProperty.Register<RatingControl, bool>(nameof(AllowClear), defaultValue: true);

        /// <summary>
        /// Gets or sets Icon.
        /// </summary>
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Defines the Icon property.
        /// </summary>
        public static readonly StyledProperty<Geometry> IconProperty =
        AvaloniaProperty.Register<RatingControl, Geometry>(nameof(Icon));

        /// <summary>
        /// Gets or sets Count.
        /// </summary>
        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        /// <summary>
        /// Defines the Count property.
        /// </summary>
        public static readonly StyledProperty<int> CountProperty =
        AvaloniaProperty.Register<RatingControl, int>(nameof(Count), defaultValue: 5);

        /// <summary>
        /// Gets or sets DefaultValue.
        /// </summary>
        public double DefaultValue
        {
            get { return (double)GetValue(DefaultValueProperty); }
            set { SetValue(DefaultValueProperty, value); }
        }

        /// <summary>
        /// Defines the DefaultValue property.
        /// </summary>
        public static readonly StyledProperty<double> DefaultValueProperty =
        AvaloniaProperty.Register<RatingControl, double>(nameof(DefaultValue), defaultValue: 0.0d);

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Defines the Value property.
        /// </summary>
        public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<RatingControl, double>(nameof(Value), defaultValue: 0.0d, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Defines the Text property.
        /// </summary>
        public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<RatingControl, string>(nameof(Text));

        /// <summary>
        /// Gets or sets ShowText.
        /// </summary>
        public bool ShowText
        {
            get { return (bool)GetValue(ShowTextProperty); }
            set { SetValue(ShowTextProperty, value); }
        }

        /// <summary>
        /// Defines the ShowText property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowTextProperty =
        AvaloniaProperty.Register<RatingControl, bool>(nameof(ShowText));

        /// <summary>
        /// Gets or sets IsReadOnly.
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Defines the IsReadOnly property.
        /// </summary>
        public static readonly StyledProperty<bool> IsReadOnlyProperty =
        AvaloniaProperty.Register<RatingControl, bool>(nameof(IsReadOnly), defaultValue: false);

        /// <summary>
        /// Defines the ValueChanged routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<double>> ValueChangedEvent =
        RoutedEvent.Register<RatingControl, RoutedPropertyChangedEventArgs<double>>(nameof(ValueChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets ValueChanged eventhandler.
        /// </summary>
        public event EventHandler ValueChanged
        {
            add
            {
                AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ValueChangedEvent, value);
            }
        }

        /// <summary>
        /// updates the items
        /// </summary>
        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {
            RaiseEvent(e);
            UpdateItems();
        }

        /// <summary>
        /// sets the Value from the selected item
        /// </summary>
        private void RateItemValueChanged(object sender, RoutedEventArgs e)
        {
            Value = (from RateItem item in Items.OfType<RateItem>()
                     where item.IsSelected
                     select item.IsHalf ? 0.5 : 1)
                    .Sum();
        }

        /// <summary>
        /// manage the selection and ishalf
        /// </summary>
        private void RateItemSelectedChanged(object sender, RoutedEventArgs e)
        {
            var items = Items as AvaloniaList<object>;
            if (e.Source is RateItem rateItem)
            {
                var index = rateItem.Index;
                for (var i = 0; i < index; i++)
                {
                    if (items[i] is RateItem item)
                    {
                        item.IsSelected = true;
                        item.IsHalf = false;
                    }
                }

                for (var i = index; i < Count; i++)
                {
                    if (items[i] is RateItem item)
                    {
                        item.IsSelected = false;
                        item.IsHalf = false;
                    }
                }
            }
        }

        /// <summary>
        /// adds the <see cref="RateItem"/> by <see cref="Count"/>
        /// </summary>
        private void OnApplyTemplateInternal()
        {
            AvaloniaList<object> items = Items as AvaloniaList<object>;

            if (items == null)
            {
                return;
            }

            items.Clear();

            for (var i = 1; i <= Count; i++)
            {
                items.Add(RateItem.Create(this, i));
            }
        }

        /// <summary>
        /// updates the items
        /// </summary>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            if (!_isLoaded)
            {
                _updateItems = true;
                OnApplyTemplateInternal();
                _updateItems = false;
            }

            

            base.OnApplyTemplate(e);
        }

        /// <summary>
        /// updates the items
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerExited(PointerEventArgs e)
        {
            base.OnPointerExited(e);
            UpdateItems();
        }

        /// <summary>
        /// updates the items state
        /// </summary>
        protected override void UpdateItems()
        {
            if (!_isLoaded || !_updateItems)
                return;
            var count = (int)Value;

            var items = Items as AvaloniaList<object>;

            for (var i = 0; i < count; i++)
            {
                if (items[i] is RateItem rateItem)
                {
                    rateItem.IsSelected = true;
                    rateItem.IsHalf = false;
                }
            }

            if (Value > count)
            {
                if (items[count] is RateItem rateItem)
                {
                    rateItem.IsSelected = true;
                    rateItem.IsHalf = true;
                }

                count += 1;
            }

            for (var i = count; i < Count; i++)
            {
                if (items[i] is RateItem rateItem)
                {
                    rateItem.IsSelected = false;
                    rateItem.IsHalf = false;
                }
            }
        }

        /// <summary>
        /// sets the Value to the DefaultValue
        /// </summary>
        public void Reset()
        {
            Value = DefaultValue;
        }

        /// <summary>
        /// adds handler for the RateItem listens to the value changed
        /// </summary>
        public RatingControl()
        {
            AddHandler(RateItem.SelectedChangedEvent, RateItemSelectedChanged);
            AddHandler(RateItem.ValueChangedEvent, RateItemValueChanged);

            ValueProperty.Changed.AddClassHandler<RatingControl>((o, e) => OnValueChanged(o, e));
            AllowHalfProperty.Changed.AddClassHandler<RatingControl>((o, e) => OnAllowHaveChanged(o, e));

            IsVisibleProperty.Changed.AddClassHandler<RatingControl>((o, e) => 
            {

                if (Design.IsDesignMode)
                {
                    return;
                }

                _updateItems = false;
                OnApplyTemplateInternal();
                _updateItems = true;
                UpdateItems();

                if (_isLoaded)
                {
                    return;
                }

                _isLoaded = true;

                if (Value <= 0)
                {
                    if (DefaultValue > 0)
                    {
                        Value = DefaultValue;
                        UpdateItems();
                    }
                }
                else
                {
                    UpdateItems();
                }


            });

        }

        /// <summary>
        /// sets <see cref="AllowHalf"/> to the items
        /// </summary>
        private void OnAllowHaveChanged(RatingControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool allowHalf)
            {
                AvaloniaList<object> items = Items as AvaloniaList<object>;

                foreach (var item in items.OfType<RateItem>())
                {
                    item.AllowHalf = allowHalf;
                }
            }
        }

        /// <summary>
        /// raises the OnValueChanged event
        /// </summary>
        private void OnValueChanged(RatingControl ratingControl, AvaloniaPropertyChangedEventArgs e)
        {
            if ((e.NewValue is double) == false)
            {
                return;
            }

            var eventArgs = new RoutedPropertyChangedEventArgs<double>
            (double.MinValue,
             (double)e.NewValue,
             ValueChangedEvent);

            ratingControl.OnValueChanged(eventArgs);
        }
    }
}
