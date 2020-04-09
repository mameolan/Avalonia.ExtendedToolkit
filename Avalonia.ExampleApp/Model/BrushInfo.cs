using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Model
{
    /// <summary>
    /// Utility class to pass the property info to the datatemplate.
    /// The crucial method is the passing of the new values (set in WPG) 
    /// via the PropertyInfo to the edited object.
    /// </summary>
    public class BrushInfo : AvaloniaObject
    {
        public string Name { get; set; }
        public PropertyInfo Info { get; set; }
        public object Component { get; set; }
        
        public IBrush ObjectBrush
        {
            get { return (IBrush)GetValue(ObjectBrushProperty); }
            set { SetValue(ObjectBrushProperty, value); }
        }


        public static readonly StyledProperty<IBrush> ObjectBrushProperty =
            AvaloniaProperty.Register<BrushInfo, IBrush>(nameof(ObjectBrush), defaultValue: Brushes.Transparent);

        public BrushInfo()
        {
            ObjectBrushProperty.Changed.AddClassHandler<BrushInfo>((o, e) => OnObjectBrushChanged(o, e));
        }

        private void OnObjectBrushChanged(BrushInfo o, AvaloniaPropertyChangedEventArgs e)
        {
            if (Info != null)
            {
                Info.SetValue(Component, e.NewValue, null);
            }
        }
    }
}
