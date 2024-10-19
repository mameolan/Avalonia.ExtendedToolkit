using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controlz.Helper;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using System;
using System.Diagnostics;

namespace Avalonia.Controlz.Controls
{
    
    // This source file is adapted from the Windows Presentation Foundation project.
    // (https://github.com/dotnet/wpf/)
    //
    // Licensed to The Avalonia Project under MIT License, courtesy of The .NET Foundation.
    

    /// <summary>
    /// TickBar is an element that use for drawing Slider's Ticks.
    /// </summary>
    public class TickBar : Control
    {
        /// <summary>
        /// fill brush
        /// </summary>
        public IBrush Fill
        {
            get { return (IBrush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Fill"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> FillProperty =
            AvaloniaProperty.Register<TickBar, IBrush>(nameof(Fill));

        /// <summary>
        /// Logical position where the Minimum Tick will be drawn
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Minimum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MinimumProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(Minimum), defaultValue: 0d);

        //RangeBaseEx.MinimumProperty.AddOwner<TickBar>(x => x.Minimum,
        //    (x, y) => x.Minimum = y, unsetValue: 0.0);

        /// <summary>
        /// Logical position where the Maximum Tick will be drawn
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Maximum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MaximumProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(Maximum), defaultValue: 0d);

        //RangeBase.MaximumProperty.AddOwner<TickBar>(x => x.Maximum,
        //    (x, y) => x.Maximum = y, unsetValue: 100.0);

        /// <summary>
        /// Logical position where the SelectionStart Tick will be drawn
        /// </summary>
        public double SelectionStart
        {
            get { return (double)GetValue(SelectionStartProperty); }
            set { SetValue(SelectionStartProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectionStart"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SelectionStartProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(SelectionStart), defaultValue: -1.0d);

        //slider does not have a selectionstart property

        /// <summary>
        /// Logical position where the SelectionEnd Tick will be drawn
        /// </summary>
        public double SelectionEnd
        {
            get { return (double)GetValue(SelectionEndProperty); }
            set { SetValue(SelectionEndProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectionEnd"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SelectionEndProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(SelectionEnd), defaultValue: -1.0d);

        /// <summary>
        /// IsSelectionRangeEnabled specifies whether to draw SelectionStart Tick and SelectionEnd Tick or not.
        /// </summary>
        public bool IsSelectionRangeEnabled
        {
            get { return (bool)GetValue(IsSelectionRangeEnabledProperty); }
            set { SetValue(IsSelectionRangeEnabledProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsSelectionRangeEnabled"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectionRangeEnabledProperty =
            AvaloniaProperty.Register<TickBar, bool>(nameof(IsSelectionRangeEnabled));

        /// <summary>
        /// TickFrequency property defines how the tick will be drawn.
        /// </summary>
        public double TickFrequency
        {
            get { return (double)GetValue(TickFrequencyProperty); }
            set { SetValue(TickFrequencyProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="TickFrequency"/> property.
        /// </summary>
        public static readonly StyledProperty<double> TickFrequencyProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(TickFrequency), defaultValue: 0d);

        //Slider.TickFrequencyProperty.AddOwner<TickBar>();

        /// <summary>
        /// The Ticks property contains collection of value of type Double which
        /// are the logical positions use to draw the ticks.
        /// The property value is a <see cref="DoubleCollection" />.
        /// </summary>
        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)GetValue(TicksProperty); }
            set { SetValue(TicksProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Ticks"/> property.
        /// </summary>
        public static readonly StyledProperty<DoubleCollection> TicksProperty =
            AvaloniaProperty.Register<TickBar, DoubleCollection>(nameof(Ticks));

        /// <summary>
        /// The IsDirectionReversed property defines the direction of value incrementation.
        /// By default, if Tick's orientation is Horizontal, ticks will be drawn from left to right.
        /// (And, bottom to top for Vertical orientation).
        /// If IsDirectionReversed is 'true' the direction of the drawing will be in opposite direction.
        /// Ticks property contains collection of value of type Double which
        /// </summary>
        public bool IsDirectionReversed
        {
            get { return (bool)GetValue(IsDirectionReversedProperty); }
            set { SetValue(IsDirectionReversedProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsDirectionReversed"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsDirectionReversedProperty =
            AvaloniaProperty.Register<TickBar, bool>(nameof(IsDirectionReversed));

        //Track.IsDirectionReversedProperty.AddOwner<TickBar>();

        /// <summary>
        /// Placement property specified how the Tick will be placed.
        /// This property affects the way ticks are drawn.
        /// This property has type of <see cref="TickBarPlacement" />.
        /// </summary>
        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Placement"/> property.
        /// </summary>
        public static readonly StyledProperty<TickBarPlacement> PlacementProperty =
            AvaloniaProperty.Register<TickBar, TickBarPlacement>(nameof(Placement),
                defaultValue: TickBarPlacement.Top);

        /// <summary>
        /// TickBar will use ReservedSpaceProperty for left and right spacing (for horizontal orientation) or
        /// tob and bottom spacing (for vertical orienation).
        /// The space on both sides of TickBar is half of specified ReservedSpace.
        /// This property has type of <see cref="double" />.
        /// </summary>
        public double ReservedSpace
        {
            get { return (double)GetValue(ReservedSpaceProperty); }
            set { SetValue(ReservedSpaceProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ReservedSpace"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ReservedSpaceProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(ReservedSpace), defaultValue: 0d);

        /// <summary>
        /// Get /sets VisualXSnappingGuidelines
        /// </summary>
        public DoubleCollection VisualXSnappingGuidelines
        {
            get { return (DoubleCollection)GetValue(VisualXSnappingGuidelinesProperty); }
            set { SetValue(VisualXSnappingGuidelinesProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="VisualXSnappingGuidelines"/> property.
        /// </summary>
        public static readonly StyledProperty<DoubleCollection> VisualXSnappingGuidelinesProperty =
            AvaloniaProperty.Register<TickBar, DoubleCollection>(nameof(VisualXSnappingGuidelines));

        /// <summary>
        /// Get /sets VisualYSnappingGuidelines
        /// </summary>
        public DoubleCollection VisualYSnappingGuidelines
        {
            get { return (DoubleCollection)GetValue(VisualYSnappingGuidelinesProperty); }
            set { SetValue(VisualYSnappingGuidelinesProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="VisualYSnappingGuidelines"/> property.
        /// </summary>
        public static readonly StyledProperty<DoubleCollection> VisualYSnappingGuidelinesProperty =
            AvaloniaProperty.Register<TickBar, DoubleCollection>(nameof(VisualYSnappingGuidelines));

        private Size controlSize = new Size();

        /// <summary>
        /// sets the control size
        /// it's more a hack
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var result = base.ArrangeOverride(finalSize);

            if (DoubleUtil.IsDoubleFinite(finalSize.Width))
                controlSize = finalSize;
            return result;
        }

        /// <summary>
        /// sets the <see cref="Layout.Layoutable.Width"/>
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (DoubleUtil.IsDoubleFinite(Width) == false)
            {
                Width = controlSize.Width;
                return new Size(controlSize.Width, Height);
            }

            var result = base.MeasureOverride(availableSize);
            return result;
        }

        /// <summary>
        /// sets the tick values
        /// </summary>
        /// <param name="dc"></param>
        public override void Render(DrawingContext dc)
        {
            if (DoubleUtil.IsDoubleFinite(Width) == false)
            {
                this.Width = controlSize.Width;
                //base.Render(dc);
                //return;
            }

            Size size = new Size(Width, Height);
            double range = Maximum - Minimum;
            double tickLen = 0.0d;  // Height for Primary Tick (for Mininum and Maximum value)
            double tickLen2;        // Height for Secondary Tick
            double logicalToPhysical = 1.0;
            double progression = 1.0d;
            Point startPoint = new Point(0d, 0d);
            Point endPoint = new Point(0d, 0d);

            // Take Thumb size in to account
            double halfReservedSpace = ReservedSpace * 0.5;

            switch (Placement)
            {
                case TickBarPlacement.Top:
                    if (DoubleUtil.GreaterThanOrClose(ReservedSpace, size.Width))
                    {
                        return;
                    }
                    size = new Size(size.Width - ReservedSpace, size.Height);
                    tickLen = -size.Height;
                    startPoint = new Point(halfReservedSpace, size.Height);
                    endPoint = new Point(halfReservedSpace + size.Width, size.Height);
                    logicalToPhysical = size.Width / range;
                    progression = 1;
                    break;

                case TickBarPlacement.Bottom:
                    if (DoubleUtil.GreaterThanOrClose(ReservedSpace, size.Width))
                    {
                        return;
                    }
                    size = new Size(size.Width - ReservedSpace, size.Height);
                    tickLen = size.Height;
                    startPoint = new Point(halfReservedSpace, 0d);
                    endPoint = new Point(halfReservedSpace + size.Width, 0d);
                    logicalToPhysical = size.Width / range;
                    progression = 1;
                    break;

                case TickBarPlacement.Left:
                    if (DoubleUtil.GreaterThanOrClose(ReservedSpace, size.Height))
                    {
                        return;
                    }
                    size = new Size(size.Width, size.Height - ReservedSpace);
                    tickLen = -size.Width;
                    startPoint = new Point(size.Width, size.Height + halfReservedSpace);
                    endPoint = new Point(size.Width, halfReservedSpace);
                    logicalToPhysical = size.Height / range * -1;
                    progression = -1;
                    break;

                case TickBarPlacement.Right:
                    if (DoubleUtil.GreaterThanOrClose(ReservedSpace, size.Height))
                    {
                        return;
                    }
                    size = new Size(size.Width, size.Height - ReservedSpace);
                    tickLen = size.Width;
                    startPoint = new Point(0d, size.Height + halfReservedSpace);
                    endPoint = new Point(0d, halfReservedSpace);
                    logicalToPhysical = size.Height / range * -1;
                    progression = -1;
                    break;
            };

            tickLen2 = tickLen * 0.75;

            // Invert direciton of the ticks
            if (IsDirectionReversed)
            {
                progression = -progression;
                logicalToPhysical *= -1;

                // swap startPoint & endPoint
                Point pt = startPoint;
                startPoint = endPoint;
                endPoint = pt;
            }

            Pen pen = new Pen(Fill, 1.0d);

            bool snapsToDevicePixels = false;//SnapsToDevicePixels
            DoubleCollection xLines = snapsToDevicePixels ? new DoubleCollection() : null;
            DoubleCollection yLines = snapsToDevicePixels ? new DoubleCollection() : null;

            // Is it Vertical?
            if ((Placement == TickBarPlacement.Left) || (Placement == TickBarPlacement.Right))
            {
                // Reduce tick interval if it is more than would be visible on the screen
                double interval = TickFrequency;
                if (interval > 0.0)
                {
                    double minInterval = (Maximum - Minimum) / size.Height;
                    if (interval < minInterval)
                    {
                        interval = minInterval;
                    }
                }

                // Draw Min & Max tick
                dc.DrawLine(pen, startPoint, new Point(startPoint.X + tickLen, startPoint.Y));
                dc.DrawLine(pen, new Point(startPoint.X, endPoint.Y),
                                 new Point(startPoint.X + tickLen, endPoint.Y));

                if (snapsToDevicePixels)
                {
                    xLines.Add(startPoint.X);
                    yLines.Add(startPoint.Y - 0.5);
                    xLines.Add(startPoint.X + tickLen);
                    yLines.Add(endPoint.Y - 0.5);
                    xLines.Add(startPoint.X + tickLen2);
                }

                // This property is rarely set so let's try to avoid the GetValue
                // caching of the mutable default value
                DoubleCollection ticks = null;

                if (GetValue(TicksProperty)
                    != null)
                {
                    ticks = Ticks;
                }

                // Draw ticks using specified Ticks collection
                if ((ticks != null) && (ticks.Count > 0))
                {
                    for (int i = 0; i < ticks.Count; i++)
                    {
                        if (DoubleUtil.LessThanOrClose(ticks[i], Minimum) || DoubleUtil.GreaterThanOrClose(ticks[i], Maximum))
                        {
                            continue;
                        }

                        double adjustedTick = ticks[i] - Minimum;

                        double y = adjustedTick * logicalToPhysical + startPoint.Y;
                        dc.DrawLine(pen,
                            new Point(startPoint.X, y),
                            new Point(startPoint.X + tickLen2, y));

                        if (snapsToDevicePixels)
                        {
                            yLines.Add(y - 0.5);
                        }
                    }
                }
                // Draw ticks using specified TickFrequency
                else if (interval > 0.0)
                {
                    for (double i = interval; i < range; i += interval)
                    {
                        double y = i * logicalToPhysical + startPoint.Y;

                        dc.DrawLine(pen,
                            new Point(startPoint.X, y),
                            new Point(startPoint.X + tickLen2, y));

                        if (snapsToDevicePixels)
                        {
                            yLines.Add(y - 0.5);
                        }
                    }
                }

                // Draw Selection Ticks
                if (IsSelectionRangeEnabled)
                {
                    double y0 = (SelectionStart - Minimum) * logicalToPhysical + startPoint.Y;
                    Point pt0 = new Point(startPoint.X, y0);
                    Point pt1 = new Point(startPoint.X + tickLen2, y0);
                    Point pt2 = new Point(startPoint.X + tickLen2, y0 + Math.Abs(tickLen2) * progression);

                    //PathSegment[] segments = new PathSegment[] {
                    //    //new LineSegment(pt2, true),
                    //    //new LineSegment(pt0, true),
                    //    new LineSegment{Point=pt2 },
                    //    new LineSegment{Point=pt0 },
                    //};

                    PathSegments segments = new PathSegments();
                    segments.Add(new LineSegment { Point = pt2 });
                    segments.Add(new LineSegment { Point = pt0 });

                    PathGeometry geo = new PathGeometry { Figures = new PathFigures { new PathFigure { StartPoint = pt1, Segments = segments, IsClosed = true } } };

                    dc.DrawGeometry(Fill, pen, geo);

                    y0 = (SelectionEnd - Minimum) * logicalToPhysical + startPoint.Y;
                    pt0 = new Point(startPoint.X, y0);
                    pt1 = new Point(startPoint.X + tickLen2, y0);
                    pt2 = new Point(startPoint.X + tickLen2, y0 - Math.Abs(tickLen2) * progression);

                    segments = new PathSegments();
                    segments.Add(new LineSegment { Point = pt2 });
                    segments.Add(new LineSegment { Point = pt0 });

                    geo = new PathGeometry { Figures = new PathFigures { new PathFigure { StartPoint = pt1, Segments = segments, IsClosed = true } } };
                    dc.DrawGeometry(Fill, pen, geo);
                }
            }
            else  // Placement == Top || Placement == Bottom
            {
                // Reduce tick interval if it is more than would be visible on the screen
                double interval = TickFrequency;
                if (interval > 0.0)
                {
                    double minInterval = (Maximum - Minimum) / size.Width;
                    if (interval < minInterval)
                    {
                        interval = minInterval;
                    }
                }

                // Draw Min & Max tick
                dc.DrawLine(pen, startPoint, new Point(startPoint.X, startPoint.Y + tickLen));
                dc.DrawLine(pen, new Point(endPoint.X, startPoint.Y),
                                 new Point(endPoint.X, startPoint.Y + tickLen));

                if (snapsToDevicePixels)
                {
                    xLines.Add(startPoint.X - 0.5);
                    yLines.Add(startPoint.Y);
                    xLines.Add(startPoint.X - 0.5);
                    yLines.Add(endPoint.Y + tickLen);
                    yLines.Add(endPoint.Y + tickLen2);
                }

                // This property is rarely set so let's try to avoid the GetValue
                // caching of the mutable default value
                DoubleCollection ticks = null;

                if (GetValue(TicksProperty)
                    != null)
                {
                    ticks = Ticks;
                }

                // Draw ticks using specified Ticks collection
                if ((ticks != null) && (ticks.Count > 0))
                {
                    for (int i = 0; i < ticks.Count; i++)
                    {
                        if (DoubleUtil.LessThanOrClose(ticks[i], Minimum) || DoubleUtil.GreaterThanOrClose(ticks[i], Maximum))
                        {
                            continue;
                        }
                        double adjustedTick = ticks[i] - Minimum;

                        double x = adjustedTick * logicalToPhysical + startPoint.X;
                        dc.DrawLine(pen,
                            new Point(x, startPoint.Y),
                            new Point(x, startPoint.Y + tickLen2));

                        if (snapsToDevicePixels)
                        {
                            xLines.Add(x - 0.5);
                        }
                    }
                }
                // Draw ticks using specified TickFrequency
                else if (interval > 0.0)
                {
                    for (double i = interval; i < range; i += interval)
                    {
                        double x = i * logicalToPhysical + startPoint.X;
                        dc.DrawLine(pen,
                            new Point(x, startPoint.Y),
                            new Point(x, startPoint.Y + tickLen2));

                        if (snapsToDevicePixels)
                        {
                            xLines.Add(x - 0.5);
                        }
                    }
                }

                // Draw Selection Ticks
                if (IsSelectionRangeEnabled)
                {
                    double x0 = (SelectionStart - Minimum) * logicalToPhysical + startPoint.X;
                    Point pt0 = new Point(x0, startPoint.Y);
                    Point pt1 = new Point(x0, startPoint.Y + tickLen2);
                    Point pt2 = new Point(x0 + Math.Abs(tickLen2) * progression, startPoint.Y + tickLen2);

                    //PathSegment[] segments = new PathSegment[] {
                    //    new LineSegment(pt2, true),
                    //    new LineSegment(pt0, true),
                    //};

                    PathSegments segments = new PathSegments();
                    segments.Add(new LineSegment { Point = pt2 });
                    segments.Add(new LineSegment { Point = pt0 });

                    PathGeometry geo = new PathGeometry { Figures = new PathFigures { new PathFigure { StartPoint = pt1, Segments = segments, IsClosed = true } } };

                    dc.DrawGeometry(Fill, pen, geo);

                    x0 = (SelectionEnd - Minimum) * logicalToPhysical + startPoint.X;
                    pt0 = new Point(x0, startPoint.Y);
                    pt1 = new Point(x0, startPoint.Y + tickLen2);
                    pt2 = new Point(x0 - Math.Abs(tickLen2) * progression, startPoint.Y + tickLen2);

                    segments = new PathSegments();
                    segments.Add(new LineSegment { Point = pt2 });
                    segments.Add(new LineSegment { Point = pt0 });

                    geo = new PathGeometry { Figures = new PathFigures { new PathFigure { StartPoint = pt1, Segments = segments, IsClosed = true } } };
                    dc.DrawGeometry(Fill, pen, geo);
                }
            }

            if (snapsToDevicePixels)
            {
                xLines.Add(Width);
                yLines.Add(Height);
                VisualXSnappingGuidelines = xLines;
                VisualYSnappingGuidelines = yLines;
            }
            return;
        }

        //private void BindToTemplatedParent(AvaloniaProperty target, AvaloniaProperty source)
        //{
        //    //if (!HasNonDefaultValue(target))
        //    {
        //        //Binding binding = new Binding();
        //        //binding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
        //        //binding.Path = source.p//new PropertyPath(source);
        //        //SetBinding(target, binding);
        //        try
        //        {
        //            var sourceBinding = this.GetSubject(source);

        //            InstancedBinding instancedBinding = new InstancedBinding(sourceBinding, BindingMode.TwoWay, BindingPriority.TemplatedParent, true);
        //            BindingOperations.Apply(this, target, instancedBinding, TemplatedParent);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.Message);
        //        }
        //        //Bind(target, ObservableEx.SingleValue(source));
        //    }
        //}

        /// <summary>
        /// gets the parent <see cref="SliderEx"/>
        /// and the Track
        /// </summary>
        public override void ApplyTemplate()
        {
            base.ApplyTemplate();

            SliderEx parent = TemplatedParent as SliderEx;
            Track track = parent?.FindChild<Track>();

            if (parent != null)
            {
                UpdateProperties(parent);
                parent.PropertyChanged += Parent_PropertyChanged;

                if (track != null)
                {
                    track.PropertyChanged += Track_PropertyChanged;
                    UpdateProperties(track);
                }

                //BindToTemplatedParent(TicksProperty, SliderEx.TicksProperty);
                //BindToTemplatedParent(TickFrequencyProperty, SliderEx.TickFrequencyProperty);
                //BindToTemplatedParent(IsSelectionRangeEnabledProperty, SliderEx.IsSelectionRangeEnabledProperty);
                //BindToTemplatedParent(SelectionStartProperty, SliderEx.SelectionStartProperty);
                //BindToTemplatedParent(SelectionEndProperty, SliderEx.SelectionEndProperty);
                //BindToTemplatedParent(MinimumProperty, SliderEx.MinimumProperty);
                //BindToTemplatedParent(MaximumProperty, SliderEx.MaximumProperty);
                //BindToTemplatedParent(IsDirectionReversedProperty, Track.IsDirectionReversedProperty);

                //if (/*!HasNonDefaultValue(ReservedSpaceProperty) &&*/ track != null)
                //{
                //    Binding binding = new Binding();
                //    //binding.Source = track.Thumb;

                //    if (parent.Orientation == Layout.Orientation.Horizontal)
                //    {
                //        //binding.Path = new PropertyPath(Thumb.WidthProperty);
                //        Bind(ReservedSpaceProperty, ObservableEx.SingleValue(Thumb.WidthProperty));
                //    }
                //    else
                //    {
                //        //binding.Path = new PropertyPath(Thumb.HeightProperty);
                //        Bind(ReservedSpaceProperty, ObservableEx.SingleValue(Thumb.HeightProperty));
                //    }

                //    //SetBinding(ReservedSpaceProperty, binding);
                //}
            }
        }

        private void Track_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            UpdateProperties(sender as Track);
        }

        private void UpdateProperties(Track track)
        {
            SliderEx parent = TemplatedParent as SliderEx;
            IsDirectionReversed = track.IsDirectionReversed;

            if (track.Thumb != null)
            {
                if (parent.Orientation == Layout.Orientation.Horizontal)
                {
                    ReservedSpace = track.Thumb.Width;
                }
                else
                {
                    ReservedSpace = track.Thumb.Height;
                }
            }
        }

        private void UpdateProperties(SliderEx parent)
        {
            //Width = parent.Width;
            Ticks = parent.Ticks;
            TickFrequency = parent.TickFrequency;
            IsSelectionRangeEnabled = parent.IsSelectionRangeEnabled;
            SelectionStart = parent.SelectionStart;
            SelectionEnd = parent.SelectionEnd;
            Minimum = parent.Minimum;
            Maximum = parent.Maximum;

            //Track  track= parent.FindChild<Track>(true);
            //if (track?.Thumb != null)
            //{
            //    if (parent.Orientation == Layout.Orientation.Horizontal)
            //    {
            //        ReservedSpace = track.Thumb.Width;
            //    }
            //    else
            //    {
            //        ReservedSpace = track.Thumb.Height;
            //    }
            //}
        }

        private void Parent_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            SliderEx parent = sender as SliderEx;
            if (parent != null)
            {
                UpdateProperties(parent);
            }
        }

        static TickBar()
        {
            WidthProperty.OverrideDefaultValue<TickBar>(100);
            HeightProperty.OverrideDefaultValue<TickBar>(1);

            IsVisibleProperty.Changed.AddClassHandler<TickBar>((o, e) => OnIsVivibleChanged(o, e));
            PlacementProperty.Changed.AddClassHandler<TickBar>((o, e) => OnPlacementChanged(o, e));
            //AffectsRender<TickBar>(FillProperty, IsVisibleProperty, PlacementProperty);
            //AffectsArrange<TickBar>(IsVisibleProperty);
        }

        private static void OnPlacementChanged(TickBar o, AvaloniaPropertyChangedEventArgs e)
        {
            o.InvalidateArrange();
        }

        private static void OnIsVivibleChanged(TickBar o, AvaloniaPropertyChangedEventArgs e)
        {
            if ((e.NewValue as bool?) == true)
            {
                o.InvalidateVisual();
            }
        }

        private void TemplatedParent_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
        }
    }
}
