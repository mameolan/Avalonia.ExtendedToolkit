using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Model
{
    /// <summary>
    /// This class acts as a pre-processor for properties shown in WPG.
    /// </summary>
    public class BrushProxy : AvaloniaObject
    {
        public string CurrentComponent { get; set; }

        [Category("Brushes")]
        public BrushInfo[] ObjectBrushes
        {
            get { return (BrushInfo[])GetValue(ObjectBrushesProperty); }
            set { SetValue(ObjectBrushesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ObjectBrushes property.
        /// </summary>
        public static readonly StyledProperty<BrushInfo[]> ObjectBrushesProperty =
            AvaloniaProperty.Register<BrushProxy, BrushInfo[]>(nameof(ObjectBrushes));

        protected virtual void OnObjectBrushesChanged(BrushProxy o, AvaloniaPropertyChangedEventArgs e)
        {
        }

        public BrushProxy(object component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            ObjectBrushesProperty.Changed.AddClassHandler<BrushProxy>((o, e) => OnObjectBrushesChanged(o, e));

            CurrentComponent = component.ToString();

            CaptureComponent(component);
        }

        /// <summary>
        /// Captures the component's properties of interest.
        /// </summary>
        /// <param name="component">The component.</param>
        private void CaptureComponent(object component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            var props = component.GetType().GetProperties();
            //take everything inheriting from a Brush type
            //You could filter out on the basis of a name or what not.
            var brs = from p in props where typeof(IBrush).IsAssignableFrom(p.PropertyType) select p;
            if (brs.Count() == 0)
                return;
            var list = new List<BrushInfo>();
            foreach (var info in brs)
            {
                list.Add(new BrushInfo { Name = info.Name, ObjectBrush = info.GetValue(component, null) as IBrush, Info = info, Component = component });
            }
            ObjectBrushes = list.ToArray();
        }
    }
}
