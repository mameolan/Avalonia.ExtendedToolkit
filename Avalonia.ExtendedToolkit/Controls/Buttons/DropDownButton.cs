using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class DropDownButton : ItemsControl
    {
        private Button clickButton;
        private ContextMenu menu;

        public static RoutedEvent<RoutedEventArgs> ClickEvent =
                    RoutedEvent.Register<DropDownButton, RoutedEventArgs>(nameof(ClickEvent), RoutingStrategies.Bubble);

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

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly AvaloniaProperty IsExpandedProperty =
            AvaloniaProperty.Register<DropDownButton, bool>(nameof(IsExpanded));

        public object ExtraTag
        {
            get { return (object)GetValue(ExtraTagProperty); }
            set { SetValue(ExtraTagProperty, value); }
        }

        public static readonly AvaloniaProperty ExtraTagProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(ExtraTag));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly AvaloniaProperty OrientationProperty =
            AvaloniaProperty.Register<DropDownButton, Orientation>(nameof(Orientation));

        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly AvaloniaProperty IconProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(Icon));

        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        public static readonly AvaloniaProperty IconTemplateProperty =
            AvaloniaProperty.Register<DropDownButton, DataTemplate>(nameof(IconTemplate));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly AvaloniaProperty CommandProperty =
            AvaloniaProperty.Register<DropDownButton, ICommand>(nameof(Command));

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public static readonly AvaloniaProperty CommandTargetProperty =
            AvaloniaProperty.Register<DropDownButton, IInputElement>(nameof(CommandTarget));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly AvaloniaProperty CommandParameterProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(CommandParameter));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly AvaloniaProperty ContentProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(Content));

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly AvaloniaProperty ContentTemplateProperty =
            AvaloniaProperty.Register<DropDownButton, DataTemplate>(nameof(ContentTemplate));

        //ContentTemplateSelector missing

        public string ContentStringFormat
        {
            get { return (string)GetValue(ContentStringFormatProperty); }
            set { SetValue(ContentStringFormatProperty, value); }
        }

        public static readonly AvaloniaProperty ContentStringFormatProperty =
            AvaloniaProperty.Register<DropDownButton, string>(nameof(ContentStringFormat));

        public IStyle ButtonStyle
        {
            get { return (IStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        public static readonly AvaloniaProperty ButtonStyleProperty =
            AvaloniaProperty.Register<DropDownButton, IStyle>(nameof(ButtonStyle));

        public IStyle MenuStyle
        {
            get { return (IStyle)GetValue(MenuStyleProperty); }
            set { SetValue(MenuStyleProperty, value); }
        }

        public static readonly AvaloniaProperty MenuStyleProperty =
            AvaloniaProperty.Register<DropDownButton, IStyle>(nameof(MenuStyle));

        public IBrush ArrowBrush
        {
            get { return (IBrush)GetValue(ArrowBrushProperty); }
            set { SetValue(ArrowBrushProperty, value); }
        }

        public static readonly AvaloniaProperty ArrowBrushProperty =
            AvaloniaProperty.Register<DropDownButton, IBrush>(nameof(ArrowBrush));

        public IBrush ArrowMouseOverBrush
        {
            get { return (IBrush)GetValue(ArrowMouseOverBrushProperty); }
            set { SetValue(ArrowMouseOverBrushProperty, value); }
        }

        public static readonly AvaloniaProperty ArrowMouseOverBrushProperty =
            AvaloniaProperty.Register<DropDownButton, IBrush>(nameof(ArrowMouseOverBrush));

        public IBrush ArrowPressedBrush
        {
            get { return (IBrush)GetValue(ArrowPressedBrushProperty); }
            set { SetValue(ArrowPressedBrushProperty, value); }
        }

        public static readonly AvaloniaProperty ArrowPressedBrushProperty =
            AvaloniaProperty.Register<DropDownButton, IBrush>(nameof(ArrowPressedBrush));

        public bool IsArrowVisible
        {
            get { return (bool)GetValue(IsArrowVisibleProperty); }
            set { SetValue(IsArrowVisibleProperty, value); }
        }

        public static readonly AvaloniaProperty IsArrowVisibleProperty =
            AvaloniaProperty.Register<DropDownButton, bool>(nameof(IsArrowVisible));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly AvaloniaProperty CornerRadiusProperty =
            AvaloniaProperty.Register<DropDownButton, CornerRadius>(nameof(CornerRadius));

        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty ContentCharacterCasingProperty =
            AvaloniaProperty.Register<DropDownButton, CharacterCasing>(nameof(ContentCharacterCasing));

        public DropDownButton()
        {
            IsExpandedProperty.Changed.AddClassHandler<DropDownButton>((o, e) => IsExpandedPropertyChangedCallback(o, e));
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.SetValue(IsExpandedProperty, true);
            e.RoutedEvent = ClickEvent;
            this.RaiseEvent(e);

            if (menu.IsOpen)
                menu.Close();

            //if (menu.Parent == null)
            {
                menu.Open(this);
            }
        }

        private void IsExpandedPropertyChangedCallback(DropDownButton dropDownButton, AvaloniaPropertyChangedEventArgs e)
        {
            //dropDownButton.SetContextMenuPlacementTarget(dropDownButton.menu);
            //if (e.NewValue is bool)
            //{
            //    bool value = (bool)e.NewValue;

            //    if (value && dropDownButton.menu?.IsOpen == false)
            //    {
            //        //dropDownButton.ContextMenu.Open(this);
            //        dropDownButton.menu?.Open(dropDownButton);

            //    }
            //}
        }

        protected virtual void SetContextMenuPlacementTarget(ContextMenu contextMenu)
        {
            if (this.clickButton != null)
            {
                contextMenu.SetValue(Popup.PlacementTargetProperty, clickButton);
                //contextMenu.PlacementTarget = this.clickButton;
            }
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            this.clickButton = this.EnforceInstance<Button>(e, "PART_Button");
            this.menu = this.EnforceInstance<ContextMenu>(e, "PART_Menu");

            this.InitializeVisualElementsContainer();
            if (this.menu != null && this.Items != null /*&& this.ItemsSource == null*/)
            {
                var list= new AvaloniaList<object>();
                foreach (var newItem in this.Items)
                {
                    this.TryRemoveVisualFromOldTree(newItem);
                    list.Add(newItem);
                }
                menu.Items = list;
            }

            base.OnTemplateApplied(e);
        }

        private void TryRemoveVisualFromOldTree(object newItem)
        {
            //var visual = newItem as Visual;
            //if (visual != null)
            //{
            //    var ve = LogicalTree.LogicalExtensions.GetLogicalParent(visual) as AvaloniaObject
            //        ?? VisualTree.VisualExtensions.GetVisualParent(visual) as AvaloniaObject;

            //    if (Equals(ve))
            //    {
            //        this.LogicalChildren.Remove(visual);
            //        this.VisualChildren.Remove(visual);
            //    }

            //}
        }

        private void InitializeVisualElementsContainer()
        {
            this.PointerReleased -= DropDownButtonMouseRightButtonUp;
            this.clickButton.Click -= ButtonClick;
            this.PointerReleased += DropDownButtonMouseRightButtonUp;
            this.clickButton.Click += ButtonClick;
        }

        private void DropDownButtonMouseRightButtonUp(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                e.Handled = true;
            }
        }

        private T EnforceInstance<T>(TemplateAppliedEventArgs e, string partName) where T : class, new()
        {
            T element = e.NameScope.Find<T>(partName) ?? new T();
            return element;
        }

        protected override void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.ItemsCollectionChanged(sender, e);
            if (this.menu == null /*|| this.ItemsSource != null || this.menu.ItemsSource != null*/)
            {
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (var newItem in e.NewItems)
                        {
                            this.TryRemoveVisualFromOldTree(newItem);
                            this.menu.Items.OfType<object>().ToList().Add(newItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var oldItem in e.OldItems)
                        {
                            this.menu.Items.OfType<object>().ToList().Remove(oldItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems != null)
                    {
                        foreach (var oldItem in e.OldItems)
                        {
                            this.menu.Items.OfType<object>().ToList().Remove(oldItem);
                        }
                    }
                    if (e.NewItems != null)
                    {
                        foreach (var newItem in e.NewItems)
                        {
                            this.TryRemoveVisualFromOldTree(newItem);
                            this.menu.Items.OfType<object>().ToList().Add(newItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    if (this.Items != null)
                    {
                        this.menu.Items.OfType<object>().ToList().Clear();
                        foreach (var newItem in this.Items)
                        {
                            this.TryRemoveVisualFromOldTree(newItem);
                            this.menu.Items.OfType<object>().ToList().Add(newItem);
                        }
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}