using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroListBoxItem : ListBoxItem
    {
        public MetroListBoxItem()
        {
            
        }


        public IBrush ActiveSelectionBackgroundBrush
        {
            get { return (IBrush)GetValue(ActiveSelectionBackgroundBrushProperty); }
            set { SetValue(ActiveSelectionBackgroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty ActiveSelectionBackgroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(ActiveSelectionBackgroundBrush));



        public IBrush ActiveSelectionForegroundBrush
        {
            get { return (IBrush)GetValue(ActiveSelectionForegroundBrushProperty); }
            set { SetValue(ActiveSelectionForegroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty ActiveSelectionForegroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(ActiveSelectionForegroundBrush));



        public IBrush DisabledForegroundBrush
        {
            get { return (IBrush)GetValue(DisabledForegroundBrushProperty); }
            set { SetValue(DisabledForegroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty DisabledForegroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(DisabledForegroundBrush));



        public IBrush DisabledSelectedBackgroundBrush
        {
            get { return (IBrush)GetValue(DisabledSelectedBackgroundBrushProperty); }
            set { SetValue(DisabledSelectedBackgroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty DisabledSelectedBackgroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(DisabledSelectedBackgroundBrush));



        public IBrush DisabledSelectedForegroundBrush
        {
            get { return (IBrush)GetValue(DisabledSelectedForegroundBrushProperty); }
            set { SetValue(DisabledSelectedForegroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty DisabledSelectedForegroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(DisabledSelectedForegroundBrush));



        public IBrush HoverBackgroundBrush
        {
            get { return (IBrush)GetValue(HoverBackgroundBrushProperty); }
            set { SetValue(HoverBackgroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty HoverBackgroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(HoverBackgroundBrush));



        public IBrush HoverSelectedBackgroundBrush
        {
            get { return (IBrush)GetValue(HoverSelectedBackgroundBrushProperty); }
            set { SetValue(HoverSelectedBackgroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty HoverSelectedBackgroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(HoverSelectedBackgroundBrush));



        public IBrush SelectedBackgroundBrush
        {
            get { return (IBrush)GetValue(SelectedBackgroundBrushProperty); }
            set { SetValue(SelectedBackgroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty SelectedBackgroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(SelectedBackgroundBrush));



        public IBrush SelectedForegroundBrush
        {
            get { return (IBrush)GetValue(SelectedForegroundBrushProperty); }
            set { SetValue(SelectedForegroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty SelectedForegroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(SelectedForegroundBrush));




        public IBrush DisabledBackgroundBrush
        {
            get { return (IBrush)GetValue(DisabledBackgroundBrushProperty); }
            set { SetValue(DisabledBackgroundBrushProperty, value); }
        }


        public static readonly AvaloniaProperty DisabledBackgroundBrushProperty =
            AvaloniaProperty.Register<MetroListBoxItem, IBrush>(nameof(DisabledBackgroundBrush));




    }
}
