using System.Reflection;
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
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<BrushInfo, string>(nameof(Name));

        public PropertyInfo Info
        {
            get { return (PropertyInfo)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        public static readonly StyledProperty<PropertyInfo> InfoProperty =
            AvaloniaProperty.Register<BrushInfo, PropertyInfo>(nameof(Info));

        public object Component
        {
            get { return (object)GetValue(ComponentProperty); }
            set { SetValue(ComponentProperty, value); }
        }

        public static readonly StyledProperty<object> ComponentProperty =
            AvaloniaProperty.Register<BrushInfo, object>(nameof(Component));

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
