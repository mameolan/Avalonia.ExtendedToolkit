using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;

namespace Avalonia.ExampleApp.Model.PropertyGrid_Proxy
{
    public class TextBoxProxy : AvaloniaObject, IDisposable
    {
        private TextBox component;



        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }


        public static readonly StyledProperty<double> WidthProperty =
            AvaloniaProperty.Register<TextBoxProxy, double>(nameof(Width));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<TextBoxProxy, string>(nameof(Text));

        public TextBoxProxy(TextBox component)
        {
            this.component = component;

            CaptureComponent(this.component);

            TextProperty.Changed.AddClassHandler<TextBoxProxy>((o, e) => OnTextChanged(o, e));





        }

        private void OnTextChanged(TextBoxProxy textBoxProxy, AvaloniaPropertyChangedEventArgs e)
        {
            textBoxProxy.component.PropertyChanged -= Component_PropertyChanged;
            component.Text = e.NewValue as string;
            textBoxProxy.component.PropertyChanged += Component_PropertyChanged;
        }

        private void CaptureComponent(TextBox component)
        {
            component.PropertyChanged += Component_PropertyChanged;
        }

        

        private void Component_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == TextBox.WidthProperty)
            {
                Width = component.Width;
            }
            else if (e.Property == TextBox.TextProperty)
            {
                Text = component.Text;
            }
        }


        private bool disposed = false;

        ~TextBoxProxy()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                ReleaseComponent(component);
            }

            disposed = true;
        }

        private void ReleaseComponent(TextBox component)
        {
            component.PropertyChanged -= Component_PropertyChanged;
        }
    }
}
