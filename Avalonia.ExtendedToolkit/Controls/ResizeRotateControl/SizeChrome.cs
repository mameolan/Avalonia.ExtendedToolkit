using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1

    /// <summary>
    /// control which displays the current size of the content
    /// </summary>
    public class SizeChrome : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(SizeChrome);

        public SizeChrome()
        {
            WidthProperty.Changed.AddClassHandler<SizeChrome>((o,e)=> OnCanvasRightChanged(o,e));
            HeightProperty.Changed.AddClassHandler<SizeChrome>((o,e)=> OnCanvasRightChanged(o,e));
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            

        }

        private void OnCanvasRightChanged(SizeChrome o, AvaloniaPropertyChangedEventArgs e)
        {
            
        }
    }
}
