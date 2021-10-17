using System.Collections;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// A control which shows its item in a popup
    /// the selected item can displayed in a seperate content
    /// <see cref="DropDownButtonContentDisplayMode"/>
    /// </summary>
    public class DropDownButton : TemplatedControl
    {
        private static string PART_PopupMenu = "PART_PopupMenu";
        private static string PART_PopUpButton = "PART_PopUpButton";
        private static string PART_ListBox = "PART_ListBox";

        private ListBox _listBox;
        private Popup _popupMenu;
        private ToggleButton _popupButton;

        /// <summary>
        /// Gets or sets ClickCommand.
        /// </summary>
        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        /// <summary>
        /// Defines the ClickCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> ClickCommandProperty =
        AvaloniaProperty.Register<DropDownButton, ICommand>(nameof(ClickCommand));

        /// <summary>
        /// Gets or sets ClickCommandParameter.
        /// </summary>
        public object ClickCommandParameter
        {
            get { return (object)GetValue(ClickCommandParameterProperty); }
            set { SetValue(ClickCommandParameterProperty, value); }
        }

        /// <summary>
        /// Defines the ClickCommandParameter property.
        /// </summary>
        public static readonly StyledProperty<object> ClickCommandParameterProperty =
        AvaloniaProperty.Register<DropDownButton, object>(nameof(ClickCommandParameter));

        /// <summary>
        /// Gets or sets SelectedItem.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Defines the SelectedItem property.
        /// </summary>
        public static readonly StyledProperty<object> SelectedItemProperty =
        AvaloniaProperty.Register<DropDownButton, object>(nameof(SelectedItem), defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets ItemTemplate.
        /// </summary>
        public IDataTemplate ItemTemplate
        {
            get { return (IDataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Defines the ItemTemplate property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ItemTemplateProperty =
        AvaloniaProperty.Register<DropDownButton, IDataTemplate>(nameof(ItemTemplate));

        /// <summary>
        /// Gets or sets DisplayTemplate.
        /// </summary>
        public IDataTemplate DisplayTemplate
        {
            get { return (IDataTemplate)GetValue(DisplayTemplateProperty); }
            set { SetValue(DisplayTemplateProperty, value); }
        }

        /// <summary>
        /// Defines the DisplayTemplate property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> DisplayTemplateProperty =
        AvaloniaProperty.Register<DropDownButton, IDataTemplate>(nameof(DisplayTemplate));

        /// <summary>
        /// Defines the Items property.
        /// </summary>
        public static readonly StyledProperty<IEnumerable> ItemsProperty =
        AvaloniaProperty.Register<DropDownButton, IEnumerable>(nameof(Items));

        /// <summary>
        /// Gets or sets DisplayMode.
        /// </summary>
        public DropDownButtonContentDisplayMode DisplayMode
        {
            get { return (DropDownButtonContentDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets IsPopupOpen.
        /// </summary>
        public bool IsPopupOpen
        {
            get { return (bool)GetValue(IsPopupOpenProperty); }
            private set { SetValue(IsPopupOpenProperty, value); }
        }

        /// <summary>
        /// Defines the IsPopupOpen property.
        /// </summary>
        public static readonly StyledProperty<bool> IsPopupOpenProperty =
        AvaloniaProperty.Register<DropDownButton, bool>(nameof(IsPopupOpen));

        /// <summary>
        /// Defines the DisplayMode property.
        /// </summary>
        public static readonly StyledProperty<DropDownButtonContentDisplayMode> DisplayModeProperty =
        AvaloniaProperty.Register<DropDownButton, DropDownButtonContentDisplayMode>(nameof(DisplayMode), defaultValue: DropDownButtonContentDisplayMode.Button);

        /// <summary>
        /// resolves the template of the control
        /// </summary>

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _popupMenu = e.NameScope.Find<Popup>(PART_PopupMenu);
            _popupButton = e.NameScope.Find<ToggleButton>(PART_PopUpButton);
            _listBox = e.NameScope.Find<ListBox>(PART_ListBox);

            Binding binding = new Binding();
            binding.Source = _listBox;
            binding.Mode = BindingMode.TwoWay;
            binding.Path = SelectedItemProperty.Name;
            this.Bind(SelectedItemProperty, binding);

            binding = new Binding();
            binding.Source = _popupMenu;
            binding.Mode = BindingMode.TwoWay;
            binding.Path = Popup.IsOpenProperty.Name;
            this.Bind(IsPopupOpenProperty, binding);

            if (ClickCommand != null &&
                (DisplayMode != DropDownButtonContentDisplayMode.NoContent
                || DisplayMode != DropDownButtonContentDisplayMode.Content)
                )
            {
                _popupButton.IsEnabled = ClickCommand.CanExecute(null);
                ClickCommand.CanExecuteChanged += (o, e) =>
                {
                    _popupButton.IsEnabled = ClickCommand.CanExecute(null);
                };
            }

            _popupButton.Checked += (o, e) =>
              {
                  if (_popupButton.IsChecked == true)
                  {
                      _popupMenu.Open();
                  }
              };

            _popupMenu.Closed += (o, e) =>
            {
                _popupButton.IsChecked = false;
                PseudoClasses.Remove(":menuOpen");
            };

            _listBox.SelectionChanged += (o, e) =>
            {
                _popupMenu.Close();
            };
        }
    }
}
