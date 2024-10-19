using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// A breadcrumb button is part of a BreadcrumbItem and contains 
    /// a header and a dropdown button.
    /// </summary>
    public partial class BreadcrumbButton
    {
        private ContextMenu contextMenu;
        private Control dropDownBtn;
        private Control dropPanel;
        private bool isPressed = false;

        private const string partMenu = "PART_Menu";
        private const string partToggle = "PART_Toggle";
        private const string partButton = "PART_button";
        private const string partDropDown = "PART_DropDown";

        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(BreadcrumbButton);

        /// <summary>
        /// gets the OpenOverflowCommand
        /// </summary>
        public ICommand OpenOverflowCommand { get; private set; }

        /// <summary>
        /// gets the SelectCommand
        /// </summary>
        public ICommand SelectCommand { get; private set; }

        /// <summary>
        /// Gets or sets the Image of the BreadcrumbButton.
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
            AvaloniaProperty.Register<BreadcrumbButton, Bitmap>(nameof(Image));

        /// <summary>
        /// returns HasImage
        /// </summary>
        public bool HasImage
        {
            get { return (bool)GetValue(HasImageProperty); }
            internal set { SetValue(HasImageProperty, value); }
        }

        /// <summary>
        /// <see cref="HasImage"/>
        /// </summary>
        public static readonly StyledProperty<bool> HasImageProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(HasImage));

        /// <summary>
        /// Gets or sets the selectedItem.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedItem"/>
        /// </summary>
        public static readonly StyledProperty<object> SelectedItemProperty =
            AvaloniaProperty.Register<BreadcrumbButton, object>(nameof(SelectedItem));

        /// <summary>
        /// Gets or sets the ButtonMode for the BreadcrumbButton.
        /// </summary>
        public ButtonMode Mode
        {
            get { return (ButtonMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        /// <summary>
        /// <see cref="Mode"/>
        /// </summary>
        public static readonly StyledProperty<ButtonMode> ModeProperty =
            AvaloniaProperty.Register<BreadcrumbButton, ButtonMode>(nameof(Mode), defaultValue: ButtonMode.Breadcrumb);

        /// <summary>
        /// Gets or sets whether the button is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            internal set { SetValue(IsPressedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsPressed"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsPressedProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsPressed));

        /// <summary>
        /// Gets or sets whether the drop down button is pressed.
        /// </summary>
        public bool IsDropDownPressed
        {
            get { return (bool)GetValue(IsDropDownPressedProperty); }
            internal set { SetValue(IsDropDownPressedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsDropDownPressed"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsDropDownPressedProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsDropDownPressed));

        /// <summary>
        /// Gets or sets whether the drop down button is visible.
        /// </summary>
        public bool IsDropDownVisible
        {
            get { return (bool)GetValue(IsDropDownVisibleProperty); }
            set { SetValue(IsDropDownVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsDropDownVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsDropDownVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsDropDownVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets the DataTemplate for the drop down items.
        /// </summary>
        public IDataTemplate DropDownContentTemplate
        {
            get { return (IDataTemplate)GetValue(DropDownContentTemplateProperty); }
            set { SetValue(DropDownContentTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="DropDownContentTemplate"/>
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> DropDownContentTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbButton, IDataTemplate>(nameof(DropDownContentTemplate));

        /// <summary>
        /// Gets or sets whether the button is visible.
        /// </summary>
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsButtonVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsButtonVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the Image is visible
        /// </summary>
        public bool IsImageVisible
        {
            get { return (bool)GetValue(IsImageVisibleProperty); }
            set { SetValue(IsImageVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsImageVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsImageVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(IsImageVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether to use visual background style on MouseOver and/or MouseDown.
        /// </summary>
        public bool EnableVisualButtonStyle
        {
            get { return (bool)GetValue(EnableVisualButtonStyleProperty); }
            set { SetValue(EnableVisualButtonStyleProperty, value); }
        }

        /// <summary>
        /// <see cref="EnableVisualButtonStyle"/>
        /// </summary>
        public static readonly StyledProperty<bool> EnableVisualButtonStyleProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(EnableVisualButtonStyle), defaultValue: true);

        /// <summary>
        /// returns true if items.count > 0
        /// </summary>
        public bool HasItems
        {
            get { return (bool)GetValue(HasItemsProperty); }
            private set { SetValue(HasItemsProperty, value); }
        }

        /// <summary>
        /// <see cref="HasItems"/>
        /// </summary>
        public static readonly StyledProperty<bool> HasItemsProperty =
            AvaloniaProperty.Register<BreadcrumbButton, bool>(nameof(HasItems));

        /// <summary>
        /// <see cref="SelectedItemChanged"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> SelectedItemChangedEvent =
                    RoutedEvent.Register<BreadcrumbButton, RoutedEventArgs>(nameof(OnSelectedItemChanged), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the SelectedItem is changed.
        /// </summary>
        public event EventHandler SelectedItemChanged
        {
            add
            {
                AddHandler(SelectedItemChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedItemChangedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="Click"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
                    RoutedEvent.Register<BreadcrumbButton, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the Button is clicked.
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
    }
}
