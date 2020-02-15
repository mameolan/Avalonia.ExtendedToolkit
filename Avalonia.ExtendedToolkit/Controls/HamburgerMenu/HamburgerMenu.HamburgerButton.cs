using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class HamburgerMenu
    {


        public double HamburgerWidth
        {
            get { return (double)GetValue(HamburgerWidthProperty); }
            set { SetValue(HamburgerWidthProperty, value); }
        }


        public static readonly AvaloniaProperty<double> HamburgerWidthProperty =
            AvaloniaProperty.Register<HamburgerMenu, double>(nameof(HamburgerWidth), defaultValue: 48.0);



        public double HamburgerHeight
        {
            get { return (double)GetValue(HamburgerHeightProperty); }
            set { SetValue(HamburgerHeightProperty, value); }
        }


        public static readonly AvaloniaProperty<double> HamburgerHeightProperty =
            AvaloniaProperty.Register<HamburgerMenu, double>(nameof(HamburgerHeight), defaultValue: 48.0);



        public Thickness HamburgerMargin
        {
            get { return (Thickness)GetValue(HamburgerMarginProperty); }
            set { SetValue(HamburgerMarginProperty, value); }
        }


        public static readonly AvaloniaProperty<Thickness> HamburgerMarginProperty =
            AvaloniaProperty.Register<HamburgerMenu, Thickness>(nameof(HamburgerMargin));



        public bool HamburgerIsVisible
        {
            get { return (bool)GetValue(HamburgerIsVisibleProperty); }
            set { SetValue(HamburgerIsVisibleProperty, value); }
        }


        public static readonly AvaloniaProperty<bool> HamburgerIsVisibleProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(HamburgerIsVisible), defaultValue: true);



        public IStyle HamburgerButtonStyle
        {
            get { return (IStyle)GetValue(HamburgerButtonStyleProperty); }
            set { SetValue(HamburgerButtonStyleProperty, value); }
        }


        public static readonly AvaloniaProperty<IStyle> HamburgerButtonStyleProperty =
            AvaloniaProperty.Register<HamburgerMenu, IStyle>(nameof(HamburgerButtonStyle));



        public DataTemplate HamburgerButtonTemplate
        {
            get { return (DataTemplate)GetValue(HamburgerButtonTemplateProperty); }
            set { SetValue(HamburgerButtonTemplateProperty, value); }
        }


        public static readonly AvaloniaProperty<DataTemplate> HamburgerButtonTemplateProperty =
            AvaloniaProperty.Register<HamburgerMenu, DataTemplate>(nameof(HamburgerButtonTemplate));



        public DataTemplate HamburgerMenuHeaderTemplate
        {
            get { return (DataTemplate)GetValue(HamburgerMenuHeaderTemplateProperty); }
            set { SetValue(HamburgerMenuHeaderTemplateProperty, value); }
        }


        public static readonly AvaloniaProperty<DataTemplate> HamburgerMenuHeaderTemplateProperty =
            AvaloniaProperty.Register<HamburgerMenu, DataTemplate>(nameof(HamburgerMenuHeaderTemplate));




    }
}
