using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from: 
    //https://www.codeproject.com/Articles/42849/Making-a-Drop-Down-Style-Custom-Color-Picker-in-WP


    /// <summary>
    ///  color picker control
    /// </summary>
    public class ColorPicker : TemplatedControl
    {
        private const string PART_Button = "PART_Button";
        private const string PART_ColorSelector = "PART_ColorSelector";
        private const string PART_PopupMenu = "PART_PopupMenu";

        private bool _isContexMenuOpened = false;

        private ToggleButton button;
        private ColorSelector colorSelector;
        private Popup popupMenu;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey { get { return typeof(ColorPicker); } }

        /// <summary>
        /// Defines the <see cref="SelectedColorChanged"/> routed event.
        /// </summary>
        public static readonly RoutedEvent<ColorRoutedEventArgs> SelectedColorChangedEvent =
                    RoutedEvent.Register<ColorPicker, ColorRoutedEventArgs>(nameof(SelectedColorChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets SelectedColorChanged eventhandler.
        /// </summary>
        public event EventHandler<ColorRoutedEventArgs> SelectedColorChanged
        {
            add
            {
                AddHandler(SelectedColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedColorChangedEvent, value);
            }
        }

        /// <summary>
        /// Defines the <see cref="HexValue"/> direct property.
        /// </summary>
        public static readonly DirectProperty<ColorPicker, string> HexValueProperty =
                AvaloniaProperty.RegisterDirect<ColorPicker, string>(
                    nameof(HexValue),
                    o => o.HexValue);

        private string _hexValue;

        /// <summary>
        /// Gets or sets HexValue.
        /// </summary>
        public string HexValue
        {
            get { return _hexValue; }
            internal set
            {
                SetAndRaise(HexValueProperty, ref _hexValue, value);
            }
        }

        /// <summary>
        /// Gets or sets PreviewColorBrush.
        /// </summary>
        public IBrush PreviewColorBrush
        {
            get { return (IBrush)GetValue(PreviewColorBrushProperty); }
            set { SetValue(PreviewColorBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="PreviewColorBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> PreviewColorBrushProperty =
            AvaloniaProperty.Register<ColorPicker, IBrush>(nameof(PreviewColorBrush));

        /// <summary>
        /// Defines the <see cref="SelectedColor"/> direct property.
        /// </summary>
        internal static readonly DirectProperty<ColorPicker, Color> SelectedColorProperty =
                AvaloniaProperty.RegisterDirect<ColorPicker, Color>(
                    nameof(SelectedColor),
                    o => o.SelectedColor);

        private Color _selectedColor;

        /// <summary>
        /// Gets or sets SelectedColor.
        /// </summary>
        internal Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                SetAndRaise(SelectedColorProperty, ref _selectedColor, value);
            }
        }

        /// <summary>
        /// opens the popup menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_isContexMenuOpened)
            {
                if (popupMenu != null && popupMenu.IsOpen == false)
                {
                    popupMenu.PlacementTarget = button;
                    popupMenu.PlacementMode = PlacementMode.Bottom;
                    popupMenu.Open();
                }
            }
        }

        /// <summary>
        /// fires the <see cref="SelectedColorChangedEvent"/>
        /// sets the <see cref="PreviewColorBrush"/> and
        /// the <see cref="HexValue"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupMenu_Closed(object sender, EventArgs e)
        {
            if (!popupMenu.IsOpen && colorSelector?.CustomColor != null)
            {
                RaiseEvent(new ColorRoutedEventArgs(colorSelector.CustomColor, SelectedColorChangedEvent));

                PreviewColorBrush = new SolidColorBrush(colorSelector.CustomColor);
                HexValue = string.Format("#{0}", colorSelector.CustomColor.ToString().Substring(1));
            }
            _isContexMenuOpened = false;
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            button = e.NameScope.Find<ToggleButton>(PART_Button);
            button.Click += Button_Click;

            colorSelector = e.NameScope.Find<ColorSelector>(PART_ColorSelector);
            colorSelector.DefaultPickerColorChanged += (o, e) =>
            {
                if (popupMenu?.IsOpen == true)
                {
                    popupMenu.Close();
                }
            };

            popupMenu = e.NameScope.Find<Popup>(PART_PopupMenu);
            popupMenu.Opened += (o, e) =>
            {
               _isContexMenuOpened = true;
            };
            popupMenu.Closed += PopupMenu_Closed;
        }
    }
}
