using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// button with a menu
    /// </summary>
    public class DropDownButton : ItemsControl
    {
        private Button clickButton;
        private ContextMenu menu;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(DropDownButton);

        /// <summary>
        /// Indicates whether the Menu is visible.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsExpanded"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<DropDownButton, bool>(nameof(IsExpanded));

        /// <summary>
        /// Gets or sets an extra tag.
        /// </summary>
        public object ExtraTag
        {
            get { return (object)GetValue(ExtraTagProperty); }
            set { SetValue(ExtraTagProperty, value); }
        }

        /// <summary>
        /// <see cref="ExtraTag"/>
        /// </summary>
        public static readonly StyledProperty<object> ExtraTagProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(ExtraTag));

        /// <summary>
        /// Gets or sets the dimension of children stacking.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// <see cref="Orientation"/>
        /// </summary>
        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<DropDownButton, Orientation>(nameof(Orientation));

        /// <summary>
        ///  Gets or sets the Content used to generate the icon part.
        /// </summary>
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// <see cref="Icon"/>
        /// </summary>
        public static readonly StyledProperty<object> IconProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(Icon));

        /// <summary>
        /// Gets or sets the ContentTemplate used to display the content of the icon part.
        /// </summary>
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="IconTemplate"/>
        /// </summary>
        public static readonly StyledProperty<DataTemplate> IconTemplateProperty =
            AvaloniaProperty.Register<DropDownButton, DataTemplate>(nameof(IconTemplate));

        /// <summary>
        /// Get or sets the Command property.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Command"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<DropDownButton, ICommand>(nameof(Command));

        /// <summary>
        /// Gets or sets the target element on which to fire the command.
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        /// <summary>
        /// <see cref="CommandTarget"/>
        /// </summary>
        public static readonly StyledProperty<IInputElement> CommandTargetProperty =
            AvaloniaProperty.Register<DropDownButton, IInputElement>(nameof(CommandTarget));

        /// <summary>
        /// Reflects the parameter to pass to the CommandProperty upon execution.
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// <see cref="CommandParameter"/>
        /// </summary>
        public static readonly StyledProperty<object> CommandParameterProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(CommandParameter));

        /// <summary>
        /// Gets or sets the Content of this control..
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// <see cref="Content"/>
        /// </summary>
        public static readonly StyledProperty<object> ContentProperty =
            AvaloniaProperty.Register<DropDownButton, object>(nameof(Content));

        /// <summary>
        /// ContentTemplate is the template used to display the content of the control.
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentTemplate"/>
        /// </summary>
        public static readonly StyledProperty<DataTemplate> ContentTemplateProperty =
            AvaloniaProperty.Register<DropDownButton, DataTemplate>(nameof(ContentTemplate));

        //ContentTemplateSelector missing

        /// <summary>
        /// ContentStringFormat is the format used to display the content of the control as a string
        /// </summary>
        /// <remarks>
        /// This property is ignored if <seealso cref="ContentTemplate"/> is set.
        /// </remarks>
        public string ContentStringFormat
        {
            get { return (string)GetValue(ContentStringFormatProperty); }
            set { SetValue(ContentStringFormatProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentStringFormat"/>
        /// </summary>
        public static readonly StyledProperty<string> ContentStringFormatProperty =
            AvaloniaProperty.Register<DropDownButton, string>(nameof(ContentStringFormat));

        /// <summary>
        /// Gets/sets the button style.
        /// </summary>
        public IStyle ButtonStyle
        {
            get { return (IStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="ButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> ButtonStyleProperty =
            AvaloniaProperty.Register<DropDownButton, IStyle>(nameof(ButtonStyle));

        /// <summary>
        /// Gets/sets the menu style.
        /// </summary>
        public IStyle MenuStyle
        {
            get { return (IStyle)GetValue(MenuStyleProperty); }
            set { SetValue(MenuStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="MenuStyle"/>
        /// </summary>
        public static readonly StyledProperty<IStyle> MenuStyleProperty =
            AvaloniaProperty.Register<DropDownButton, IStyle>(nameof(MenuStyle));

        /// <summary>
        /// Gets/sets the brush of the button arrow icon.
        /// </summary>
        public IBrush ArrowBrush
        {
            get { return (IBrush)GetValue(ArrowBrushProperty); }
            set { SetValue(ArrowBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="ArrowBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> ArrowBrushProperty =
            AvaloniaProperty.Register<DropDownButton, IBrush>(nameof(ArrowBrush));

        /// <summary>
        /// Gets/sets the brush of the button arrow icon if the mouse is over the drop down button.
        /// </summary>
        public IBrush ArrowMouseOverBrush
        {
            get { return (IBrush)GetValue(ArrowMouseOverBrushProperty); }
            set { SetValue(ArrowMouseOverBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="ArrowMouseOverBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> ArrowMouseOverBrushProperty =
            AvaloniaProperty.Register<DropDownButton, IBrush>(nameof(ArrowMouseOverBrush));

        /// <summary>
        /// Gets/sets the brush of the button arrow icon if the arrow button is pressed.
        /// </summary>
        public IBrush ArrowPressedBrush
        {
            get { return (IBrush)GetValue(ArrowPressedBrushProperty); }
            set { SetValue(ArrowPressedBrushProperty, value); }
        }

        /// <summary>
        /// <see cref="ArrowPressedBrush"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> ArrowPressedBrushProperty =
            AvaloniaProperty.Register<DropDownButton, IBrush>(nameof(ArrowPressedBrush));

        /// <summary>
        /// Gets/sets the visibility of the button arrow icon.
        /// </summary>
        public bool IsArrowVisible
        {
            get { return (bool)GetValue(IsArrowVisibleProperty); }
            set { SetValue(IsArrowVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsArrowVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsArrowVisibleProperty =
            AvaloniaProperty.Register<DropDownButton, bool>(nameof(IsArrowVisible), defaultValue: true);

        /// <summary>
        /// cornerradius of this control
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// <see cref="CornerRadius"/>
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<DropDownButton, CornerRadius>(nameof(CornerRadius));

        /// <summary>
        /// CharacterCasing of this control
        /// </summary>
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentCharacterCasing"/>
        /// </summary>
        public static readonly StyledProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<DropDownButton, CharacterCasing>(nameof(ContentCharacterCasing));

        /// <summary>
        /// <see cref="Click"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
                    RoutedEvent.Register<DropDownButton, RoutedEventArgs>(nameof(ClickEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// handles the button click event
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
        /// initialize IsExpanded, MenuStyle changed events
        /// </summary>
        public DropDownButton()
        {
            IsExpandedProperty.Changed.AddClassHandler<DropDownButton>((o, e) => IsExpandedPropertyChangedCallback(o, e));
            MenuStyleProperty.Changed.AddClassHandler<DropDownButton>((o, e) => MenuStyleChanged(o, e));
        }

        /// <summary>
        /// sets the MenuStyle if menu is not null
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void MenuStyleChanged(DropDownButton o, AvaloniaPropertyChangedEventArgs e)
        {
            if (menu == null)
                return;

            IStyle menuStyle = e.NewValue as IStyle;
            if (menuStyle == null)
                return;

            menu.Styles.Add(menuStyle);
        }

        /// <summary>
        /// raises the clickevent
        /// opens the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// sets the placement of the menu
        /// </summary>
        /// <param name="contextMenu"></param>
        protected virtual void SetContextMenuPlacementTarget(ContextMenu contextMenu)
        {
            if (this.clickButton != null)
            {
                contextMenu.SetValue(Popup.PlacementTargetProperty, clickButton);
                //contextMenu.PlacementTarget = this.clickButton;
            }
        }

        /// <summary>
        /// gets some controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            this.clickButton = this.EnforceInstance<Button>(e, "PART_Button");
            this.menu = this.EnforceInstance<ContextMenu>(e, "PART_Menu");

            this.InitializeVisualElementsContainer();
            if (this.menu != null && this.Items != null /*&& this.ItemsSource == null*/)
            {
                var list = new AvaloniaList<object>();
                foreach (var newItem in this.Items)
                {
                    this.TryRemoveVisualFromOldTree(newItem);
                    list.Add(newItem);
                }
                menu.Items = list;
            }
            this.RaisePropertyChanged(MenuStyleProperty, null, (IStyle)MenuStyle);
            //RaisePropertyChanged<IStyle>(MenuStyleProperty, null, MenuStyle);
            //this.OnPropertyChanged<IStyle>(MenuStyleProperty, null, new Data.BindingValue<IStyle>( MenuStyle), Data.BindingPriority.Style);
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

        /// <summary>
        /// sets the events for this control
        /// </summary>
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

        /// <summary>
        /// Get element from name. If it exist then element instance return, if not, new will be created
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        private T EnforceInstance<T>(TemplateAppliedEventArgs e, string partName) where T : class, new()
        {
            T element = e.NameScope.Find<T>(partName) ?? new T();
            return element;
        }

        /// <summary>
        /// somehow not executed !!!!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
