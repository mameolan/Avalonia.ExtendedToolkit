using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro
    /// <summary>
    /// control with an underlining border
    /// </summary>
    public class Underline : ContentControl
    {
        private const string UnderlineBorderPartName = "PART_UnderlineBorder";

        private Border _underlineBorder;



        /// <summary>
        /// get/sets Placement
        /// </summary>
        public Dock Placement
        {
            get { return (Dock)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        /// <summary>
        /// <see cref="Placement"/>
        /// </summary>
        public static readonly StyledProperty<Dock> PlacementProperty =
            AvaloniaProperty.Register<Underline, Dock>(nameof(Placement));

        /// <summary>
        /// get/sets LineThickness
        /// </summary>
        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        /// <summary>
        /// <see cref="LineThickness"/>
        /// </summary>
        public static readonly StyledProperty<double> LineThicknessProperty =
            AvaloniaProperty.Register<Underline, double>(nameof(LineThickness), defaultValue: 1d);

        /// <summary>
        /// get/sets LineExtent
        /// </summary>
        public double LineExtent
        {
            get { return (double)GetValue(LineExtentProperty); }
            set { SetValue(LineExtentProperty, value); }
        }

        /// <summary>
        /// <see cref="LineExtent"/>
        /// </summary>
        public static readonly StyledProperty<double> LineExtentProperty =
            AvaloniaProperty.Register<Underline, double>(nameof(LineExtent), defaultValue: double.NaN);

        /// <summary>
        /// registers some changed handler
        /// </summary>
        public Underline()
        {
            PlacementProperty.Changed.AddClassHandler<Underline>((o, e) => ApplyBorderProperties(o, e));
            LineThicknessProperty.Changed.AddClassHandler<Underline>((o, e) => ApplyBorderProperties(o, e));
            LineExtentProperty.Changed.AddClassHandler<Underline>((o, e) => ApplyBorderProperties(o, e));
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            ApplyBorderProperties();
        }

        /// <summary>
        /// gets the underline border from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            _underlineBorder = e.NameScope.Find<Border>(UnderlineBorderPartName);


            ApplyBorderProperties();
        }

        private void ApplyBorderProperties(Underline o, AvaloniaPropertyChangedEventArgs e)
        {
            o.ApplyBorderProperties();
        }

        /// <summary>
        /// updates the underline by placement
        /// </summary>
        internal void ApplyBorderProperties()
        {
            if (_underlineBorder == null)
            {
                return;
            }

            // TabControl tabControl = this.TryFindParent<TabControl>();
            TabItem tabItem = this.TryFindParent<TabItem>();
            double width = double.NaN;
            double height = double.NaN;
#warning Underline: here is a sizing workaround
            if (tabItem != null)
            {

                if (tabItem.DesiredSize.Width > 0)
                {
                    width = tabItem.DesiredSize.Width - LineExtent;
                }
                else
                {
                    width = tabItem.MinWidth- LineExtent;
                }
                if (tabItem.DesiredSize.Height > 0)
                {
                    height = tabItem.DesiredSize.Height - LineExtent;
                }
                else
                {
                    height = tabItem.MinHeight - LineExtent;
                }

            }


            _underlineBorder.Height = double.NaN;
            _underlineBorder.Width = double.NaN;
            _underlineBorder.BorderThickness = new Thickness(0);

            switch (Placement)
            {
                case Dock.Left:
                    _underlineBorder.Width = LineExtent;
                    _underlineBorder.Height = height;
                    _underlineBorder.BorderThickness = new Thickness(0d, 0d, LineThickness, 0d);
                    VerticalAlignment = Layout.VerticalAlignment.Stretch;
                    HorizontalAlignment = Layout.HorizontalAlignment.Left;
                    break;

                case Dock.Top:
                    //little error somehow border is one pixel too height
                    _underlineBorder.Height = LineExtent;
                    _underlineBorder.Width = width;
                    _underlineBorder.BorderThickness = new Thickness(0d, LineThickness, 0d, 0d);
                    VerticalAlignment = Layout.VerticalAlignment.Top;
                    HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
                    break;

                case Dock.Right:
                    _underlineBorder.Width = LineExtent;
                    _underlineBorder.Height = height;
                    _underlineBorder.BorderThickness = new Thickness(0d, 0d, LineThickness, 0d);
                    VerticalAlignment = Layout.VerticalAlignment.Stretch;
                    HorizontalAlignment = Layout.HorizontalAlignment.Right;
                    break;

                case Dock.Bottom:
                    _underlineBorder.Height = LineExtent;
                    _underlineBorder.Width = width;
                    _underlineBorder.BorderThickness = new Thickness(0d, 0d, 0d, LineThickness);
                    VerticalAlignment = Layout.VerticalAlignment.Bottom;
                    HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }



            InvalidateVisual();
            InvalidateMeasure();
            InvalidateArrange();




        }
    }
}
