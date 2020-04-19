using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid;
using Avalonia.Media;
using ReactiveUI;

namespace Avalonia.ExampleApp.Model
{
    [CategoryOrder("Text", 0)]
    [CategoryOrder("Misc", 1)]
    public class BusinessObject : ReactiveObject
    {
        private string _name = "TestValue";

        public string Name
        {
            get { return _name; }
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private FontFamily _fontFamily = new FontFamily("Arial");

        [Category("Text")]
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set
            {
                this.RaiseAndSetIfChanged(ref _fontFamily, value);
            }
        }

        private double _fontSize = 8;

        [Category("Text")]
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                this.RaiseAndSetIfChanged(ref _fontSize, value);
            }
        }

        private FontWeight _fontWeight = FontWeight.Bold;

        [Category("Text")]
        public FontWeight FontWeight
        {
            get { return _fontWeight; }
            set
            {
                this.RaiseAndSetIfChanged(ref _fontWeight, value);
            }
        }

        private FontStyle _fontStyle = FontStyle.Italic;

        [Category("Text")]
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set
            {
                this.RaiseAndSetIfChanged(ref _fontStyle, value);
            }
        }







    }
}
