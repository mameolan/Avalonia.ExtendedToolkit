using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Controlz.Controls;
using Avalonia.Controlz.EventArgs;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Diagnostics;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from:
    //https://www.codeproject.com/Articles/42849/Making-a-Drop-Down-Style-Custom-Color-Picker-in-WP

    /// <summary>
    /// controls which hold framework colors selection and
    /// a custom color selection
    /// </summary>
    public partial class ColorSelector : TemplatedControl
    {

        /// <summary>
        /// sets the custom color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CustomColor = Color.FromArgb((byte)_sliderAlpha.Value, CustomColor.R, CustomColor.G, CustomColor.B);

            Debug.WriteLine($"Slider Value: {_sliderAlpha.Value}");

        }

        /// <summary>
        /// sets the custom colors to the system color section
        /// </summary>
        private void InitialWork()
        {
            //.edited
            (_defaultPicker.ItemsSource as AvaloniaList<object>).Clear();
            CustomColors customColors = new CustomColors();
            foreach (var item in customColors.SelectableColors)
            {
                //.edited
                (_defaultPicker.ItemsSource as AvaloniaList<object>).Add(item);
            }
            _defaultPicker.SelectionChanged += DefaultPicker_SelectionChanged;
        }

        /// <summary>
        /// sets the <see cref="CustomColor"/> if
        /// the selecteditem is not null
        /// and fires the <see cref="DefaultPickerColorChangedEvent"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_defaultPicker.SelectedItem != null)
            {
                CustomColor = (Color)_defaultPicker.SelectedItem;
            }
            RaiseEvent(new RoutedEventArgs(DefaultPickerColorChangedEvent));
        }

        /// <summary>
        /// just sets the is <see cref="_isMouseDownOverEllipse"/> flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EllipsePointer_MouseLeftButtonUp(object sender, PointerReleasedEventArgs e)
        {
            if (e.GetCurrentPoint(_ellipsePointer).Properties.IsLeftButtonPressed == false)
            {
                _isMouseDownOverEllipse = false;
            }
        }

        /// <summary>
        /// just sets is handled to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EllipsePointer_MouseLeftButtonDown(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(_ellipsePointer).Properties.IsLeftButtonPressed)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// calls <see cref="ChangeColor(PointerEventArgs)"/> is
        /// <see cref="_isMouseDownOverEllipse"/> is true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EllipsePointer_MouseMove(object sender, PointerEventArgs e)
        {
            if (_isMouseDownOverEllipse)
            {
                ChangeColor(e);
            }
            e.Handled = true;
        }

        /// <summary>
        /// is the left button is pressed e.handled is set to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasColor_MouseLeftButtonUp(object sender, PointerReleasedEventArgs e)
        {
            if (e.GetCurrentPoint(_ellipsePointer).Properties.IsLeftButtonPressed == false)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// calls <see cref="ChangeColor(PointerEventArgs)"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasColor_MouseLeftButtonDown(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(_ellipsePointer).Properties.IsLeftButtonPressed)
            {
                ChangeColor(e);
                e.Handled = true;
            }
        }

        /// <summary>
        /// handles the txtAll key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAll_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(((TextBox)sender).Text)) return;

                    string allTextResult = string.Empty;
                    CustomColor = ColorPickerHelper.MakeColorFromHex(sender, CustomColor, out allTextResult);
                    _txtAll.Text = allTextResult;

                    Reposition();
                }
                catch
                {
                }
            }
            else if (e.Key == Key.Tab)
            {
                _txtAlpha.Focus();
            }

            string input = e.Key.ToString().Substring(1);
            if (string.IsNullOrEmpty(input))
            {
                input = e.Key.ToString();
            }
            if (input == "3" && _shift == true)
            {
                input = "#";
            }

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                _shift = true;
            }
            else
            {
                _shift = false;
            }

            if (!(input == "#" || (input[0] >= 'A' && input[0] <= 'F') || (input[0] >= 'a' && input[0] <= 'F') || (input[0] >= '0' && input[0] <= '9')))
                e.Handled = true;
            if (input.Length > 1)
                e.Handled = true;
        }

        /// <summary>
        /// handles the txtAll key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlue_KeyDown(object sender, KeyEventArgs e)
        {
            NumericValidation(e);
            if (e.Key == Key.Tab)
            {
                _txtAll.Focus();
            }
        }

        /// <summary>
        /// handles the txtGreen key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGreen_KeyDown(object sender, KeyEventArgs e)
        {
            NumericValidation(e);
            if (e.Key == Key.Tab)
            {
                _txtBlue.Focus();
            }
        }

        /// <summary>
        /// handles the txtRed key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRed_KeyDown(object sender, KeyEventArgs e)
        {
            NumericValidation(e);
            if (e.Key == Key.Tab)
            {
                _txtGreen.Focus();
            }
        }

        /// <summary>
        /// handles the txtAlpha key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAlpha_KeyDown(object sender, KeyEventArgs e)
        {
            NumericValidation(e);

            if (e.Key == Key.Tab)
            {
                _txtRed.Focus();
            }
        }

        /// <summary>
        /// handles the txtAll textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAll_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(((TextBox)sender).Text)) return;

                string allTextResult = string.Empty;
                CustomColor = ColorPickerHelper.MakeColorFromHex(sender, CustomColor, out allTextResult);
                _txtAll.Text = allTextResult;
                Reposition();
            }
            catch
            {
            }
        }

        /// <summary>
        /// handles the txtBlue textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlue_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(((TextBox)sender).Text)) return;
                int val = Convert.ToInt32(((TextBox)sender).Text);
                if (val > 255)

                    ((TextBox)sender).Text = "255";
                else
                {
                    byte byteValue = Convert.ToByte(((TextBox)sender).Text);
                    CustomColor =
                       Color.FromArgb(
                                _customColor.A,
                                CustomColor.R,
                                CustomColor.G,
                                byteValue
                           );
                    Reposition();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handles the txtGreen textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGreen_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(((TextBox)sender).Text)) return;
                int val = Convert.ToInt32(((TextBox)sender).Text);
                if (val > 255)

                    ((TextBox)sender).Text = "255";
                else
                {
                    byte byteValue = Convert.ToByte(((TextBox)sender).Text);
                    CustomColor =
                       Color.FromArgb(
                                CustomColor.A,
                                CustomColor.R,
                                byteValue,
                                CustomColor.B);
                    Reposition();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handles the txtRed textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRed_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(((TextBox)sender).Text)) return;
                int val = Convert.ToInt32(((TextBox)sender).Text);
                if (val > 255)

                    ((TextBox)sender).Text = "255";
                else
                {
                    byte byteValue = Convert.ToByte(((TextBox)sender).Text);
                    CustomColor =
                       Color.FromArgb(
                           CustomColor.A,
                           byteValue,
                           CustomColor.G,
                           CustomColor.B);


                    Reposition();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handles the txtAlpha textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAlpha_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            try
            {
                if (string.IsNullOrEmpty(textBox.Text)) return;
                int val = Convert.ToInt32(textBox.Text);
                if (val > 255)

                    ((TextBox)sender).Text = "255";
                else
                {
                    byte byteValue = Convert.ToByte(textBox.Text);
                    CustomColor =
                       Color.FromArgb(
                                byteValue,
                                CustomColor.R,
                                CustomColor.G,
                                CustomColor.B);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// calls <see cref="MovePointerDuringReposition(int, int)"/> if the color is similr
        /// </summary>
        private void Reposition()
        {
            for (int i = 0; i < _canvasColor.Width; i++)
            {
                bool flag = false;
                for (int j = 0; j < _canvasColor.Height; j++)
                {
                    try
                    {
                        Color Colorfromimagepoint = ColorPickerHelper.GetColorFromImage(_image, i, j);
                        if (ColorPickerHelper.SimmilarColor(Colorfromimagepoint, _customColor))
                        {
                            MovePointerDuringReposition(i, j);
                            flag = true;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
                if (flag) break;
            }
        }

        /// <summary>
        /// tries the color from the color-image
        /// </summary>
        /// <param name="e"></param>
        private void ChangeColor(PointerEventArgs e)
        {
            try
            {
                CustomColor = ColorPickerHelper.
                    GetColorFromImage(_image,
                    (int)e.GetPosition(_canvasColor).X,
                    (int)e.GetPosition(_canvasColor).Y);

                MovePointer(e);
            }
            catch
            {
            }
        }

        /// <summary>
        /// moves the <see cref="_ellipsePointer"/>
        /// </summary>
        /// <param name="e"></param>
        private void MovePointer(PointerEventArgs e)
        {
            _ellipsePointer.SetValue(Canvas.LeftProperty, (double)(e.GetPosition(_canvasColor).X - 5));
            _ellipsePointer.SetValue(Canvas.TopProperty, (double)(e.GetPosition(_canvasColor).Y - 5));
            _canvasColor.InvalidateVisual();
        }

        /// <summary>
        /// moves the <see cref="_ellipsePointer"/>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void MovePointerDuringReposition(int i, int j)
        {
            _ellipsePointer.SetValue(Canvas.LeftProperty, (double)(i - 3));
            _ellipsePointer.SetValue(Canvas.TopProperty, (double)(j - 3));
            _ellipsePointer.InvalidateVisual();
            _canvasColor.InvalidateVisual();
        }

        /// <summary>
        /// updates all controls
        /// </summary>
        private void UpdatePreview()
        {
            _borderPreview.Background = new SolidColorBrush(CustomColor);

            AlphaValue = CustomColor.A;

            _txtAlpha.Text = CustomColor.A.ToString();
            string alphaHex = CustomColor.A.ToString("X").PadLeft(2, '0');

            RedValue = CustomColor.R;

            _txtRed.Text = CustomColor.R.ToString();
            string redHex = CustomColor.R.ToString("X").PadLeft(2, '0');

            GreenValue = CustomColor.G;

            _txtGreen.Text = CustomColor.G.ToString();
            string greenHex = CustomColor.G.ToString("X").PadLeft(2, '0');

            BlueValue = CustomColor.B;

            _txtBlue.Text = CustomColor.B.ToString();
            string blueHex = CustomColor.B.ToString("X").PadLeft(2, '0');

            _txtAll.Text = String.Format("#{0}{1}{2}{3}",
            alphaHex, redHex,
            greenHex, blueHex);

            _sliderAlpha.Value = CustomColor.A;
        }

        /// <summary>
        /// validtes the input if entre
        /// is pressed the <see cref="CustomColor"/>
        /// is updated
        /// </summary>
        /// <param name="e"></param>
        private void NumericValidation(KeyEventArgs e)
        {
            string input = e.Key.ToString().Substring(1);
            try
            {
                if (e.Key == Key.Enter)
                {
                    CustomColor = ColorPickerHelper.MakeColorFromRGB(
                        _txtAlpha.Text,
                        _txtRed.Text,
                        _txtGreen.Text,
                        _txtBlue.Text
                        );

                    //CustomColor = ColorPickerHelper.MakeColorFromRGB(
                    //    _alphaValue,
                    //    _redValue,
                    //    _greenValue,
                    //    _blueValue
                    //    );

                    Reposition();
                }
                int inputDigit = Int32.Parse(input);
            }
            catch
            {
                e.Handled = true;
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            Expander epDefaultcolor = e.NameScope.Find<Expander>(PART_DefaultColorSection);
            Expander epCustomcolor = e.NameScope.Find<Expander>(PART_CustomColorSection);

            epDefaultcolor.PropertyChanged += (o, e) =>
            {
                if (e.Property == Expander.IsExpandedProperty)
                {
                    if (epDefaultcolor.IsExpanded == true && epCustomcolor.IsExpanded == true)
                    {
                        epCustomcolor.IsExpanded = false;
                    }
                }
            };

            epCustomcolor.PropertyChanged += (o, e) =>
            {
                if (e.Property == Expander.IsExpandedProperty)
                {
                    if (epCustomcolor.IsExpanded == true && epDefaultcolor.IsExpanded == true)
                    {
                        epDefaultcolor.IsExpanded = false;
                    }

                    if (epCustomcolor.IsExpanded == true && _sliderAlpha.Value == 0)
                    {
                        UpdatePreview();

                    }

                }
            };

            _image = e.NameScope.Find<Image>(PART_Image);

            _txtAlpha = e.NameScope.Find<TextBox>(PART_NumericAlpha);
            _txtRed = e.NameScope.Find<TextBox>(PART_NumericRed);
            _txtGreen = e.NameScope.Find<TextBox>(PART_NumericGreen);
            _txtBlue = e.NameScope.Find<TextBox>(PART_NumericBlue);
            _txtAll = e.NameScope.Find<TextBox>(PART_TxtAll);

            //_numericAlpha = e.NameScope.Find<NumericUpDown>(PART_NumericAlpha);
            //_numericRed = e.NameScope.Find<NumericUpDown>(PART_NumericRed);
            //_numericGreen = e.NameScope.Find<NumericUpDown>(PART_NumericGreen);
            //_numericBlue = e.NameScope.Find<NumericUpDown>(PART_NumericBlue);

            _borderPreview = e.NameScope.Find<Border>(PART_Preview);
            _sliderAlpha = e.NameScope.Find<Slider>(PART_SliderAlpha);
            //have to set this here because in xaml this does not work
            _sliderAlpha.Minimum = 0;
            _sliderAlpha.Maximum = 255;


            _canvasColor = e.NameScope.Find<Canvas>(PART_CanvasColor);
            _ellipsePointer = e.NameScope.Find<Ellipse>(PART_Pointer);

            _defaultPicker = e.NameScope.Find<ListBox>(PART_DefaultPicker);

            _txtAlpha.LostFocus += txtAlpha_TextChanged;
            _txtAlpha.KeyDown += txtAlpha_KeyDown;

            _txtRed.LostFocus += txtRed_TextChanged;
            _txtRed.KeyDown += txtRed_KeyDown;

            _txtGreen.LostFocus += txtGreen_TextChanged;
            _txtGreen.KeyDown += txtGreen_KeyDown;

            _txtBlue.LostFocus += txtBlue_TextChanged;
            _txtBlue.KeyDown += txtBlue_KeyDown;

            _txtAll.LostFocus += txtAll_TextChanged;
            _txtAll.KeyDown += txtAll_KeyDown;

            _canvasColor.PointerPressed += CanvasColor_MouseLeftButtonDown;
            _canvasColor.PointerReleased += CanvasColor_MouseLeftButtonUp;

            _ellipsePointer.PointerMoved += EllipsePointer_MouseMove;
            _ellipsePointer.PointerPressed += EllipsePointer_MouseLeftButtonDown;
            _ellipsePointer.PointerReleased += EllipsePointer_MouseLeftButtonUp;

            //_sliderAlpha.ValueChanged += SliderAlpha_ValueChanged;
            _sliderAlpha.PropertyChanged += (o, e) =>
            {
                if (e.Property == Slider.ValueProperty)
                {
                    SliderAlpha_ValueChanged(_sliderAlpha,
                        new RoutedPropertyChangedEventArgs<double>(-1d,
                        _sliderAlpha.Value));
                }
            };



            InitialWork();
        }
    }
}
