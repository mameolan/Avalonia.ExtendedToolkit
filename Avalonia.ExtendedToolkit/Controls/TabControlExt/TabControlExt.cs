using System;
using System.Linq;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Media;


namespace Avalonia.ExtendedToolkit.Controls
{
    public class TabControlExt : TabControl
    {
        private static readonly string PART_MainGrid = "mainGrid";
        private static readonly string PART_HeaderPanelGrid = "HeaderPanelGrid";
        private static readonly string PART_Underline = "Underline";
        private static readonly string PART_ItemsPresenter = "PART_ItemsPresenter";
        private static readonly string PART_ContentPanel = "ContentPanel";



        private static readonly string PART_ColumnDefinition0 = "ColumnDefinition0";
        private static readonly string PART_ColumnDefinition1 = "ColumnDefinition1";
        private static readonly string PART_RowDefinition0 = "RowDefinition0";
        private static readonly string PART_RowDefinition1 = "RowDefinition1";

        private Grid _mainGrid;
        private ColumnDefinitionExt _columnDefinition0;
        private ColumnDefinitionExt _columnDefinition1;
        private RowDefinitionExt _rowDefinition0;
        private RowDefinitionExt _rowDefinition1;
        private Grid _headerPanelGrid;
        private Underline _underline;
        private Border _contentPanel;




        /// <summary>
        /// Gets or sets HeaderFontFamily.
        /// </summary>
        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }

        /// <summary>
        /// Defines the HeaderFontFamily property.
        /// </summary>
        public static readonly StyledProperty<FontFamily> HeaderFontFamilyProperty =
        AvaloniaProperty.Register<TabControlExt, FontFamily>(nameof(HeaderFontFamily), defaultValue: FontFamily.Default);

        /// <summary>
        /// Gets or sets HeaderFontWeight.
        /// </summary>
        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }

        /// <summary>
        /// Defines the HeaderFontWeight property.
        /// </summary>
        public static readonly StyledProperty<FontWeight> HeaderFontWeightProperty =
        AvaloniaProperty.Register<TabControlExt, FontWeight>(nameof(HeaderFontWeight), defaultValue: FontWeight.Normal);

        /// <summary>
        /// Gets or sets HeaderFontSize.
        /// </summary>
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        /// <summary>
        /// Defines the HeaderFontSize property.
        /// </summary>
        public static readonly StyledProperty<double> HeaderFontSizeProperty =
        AvaloniaProperty.Register<TabControlExt, double>(nameof(HeaderFontSize), defaultValue: 12);

        /// <summary>
        /// Gets or sets UnderlinePlacement.
        /// </summary>
        public Dock? UnderlinePlacement
        {
            get { return (Dock?)GetValue(UnderlinePlacementProperty); }
            set { SetValue(UnderlinePlacementProperty, value); }
        }

        /// <summary>
        /// Defines the UnderlinePlacement property.
        /// </summary>
        public static readonly StyledProperty<Dock?> UnderlinePlacementProperty =
        AvaloniaProperty.Register<TabControlExt, Dock?>(nameof(UnderlinePlacement), defaultValue: null);


        /// <summary>
        /// Gets or sets UnderlineBrush.
        /// </summary>
        public IBrush UnderlineBrush
        {
            get { return (IBrush)GetValue(UnderlineBrushProperty); }
            set { SetValue(UnderlineBrushProperty, value); }
        }

        /// <summary>
        /// Defines the UnderlineBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> UnderlineBrushProperty =
        AvaloniaProperty.Register<TabControlExt, IBrush>(nameof(UnderlineBrush));

        /// <summary>
        /// Gets or sets UnderlineMouseOverBrush.
        /// </summary>
        public IBrush UnderlineMouseOverBrush
        {
            get { return (IBrush)GetValue(UnderlineMouseOverBrushProperty); }
            set { SetValue(UnderlineMouseOverBrushProperty, value); }
        }

        /// <summary>
        /// Defines the UnderlineMouseOverBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> UnderlineMouseOverBrushProperty =
        AvaloniaProperty.Register<TabControlExt, IBrush>(nameof(UnderlineMouseOverBrush));

        /// <summary>
        /// Gets or sets UnderlineMouseOverSelectedBrush.
        /// </summary>
        public IBrush UnderlineMouseOverSelectedBrush
        {
            get { return (IBrush)GetValue(UnderlineMouseOverSelectedBrushProperty); }
            set { SetValue(UnderlineMouseOverSelectedBrushProperty, value); }
        }

        /// <summary>
        /// Defines the UnderlineMouseOverSelectedBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> UnderlineMouseOverSelectedBrushProperty =
        AvaloniaProperty.Register<TabControlExt, IBrush>(nameof(UnderlineMouseOverSelectedBrush));

        /// <summary>
        /// Gets or sets UnderlineSelectedBrush.
        /// </summary>
        public IBrush UnderlineSelectedBrush
        {
            get { return (IBrush)GetValue(UnderlineSelectedBrushProperty); }
            set { SetValue(UnderlineSelectedBrushProperty, value); }
        }

        /// <summary>
        /// Defines the UnderlineSelectedBrush property.
        /// </summary>
        public static readonly StyledProperty<IBrush> UnderlineSelectedBrushProperty =
        AvaloniaProperty.Register<TabControlExt, IBrush>(nameof(UnderlineSelectedBrush));

        /// <summary>
        /// Gets or sets Underlined.
        /// </summary>
        public UnderlinedType Underlined
        {
            get { return (UnderlinedType)GetValue(UnderlinedProperty); }
            set { SetValue(UnderlinedProperty, value); }
        }

        /// <summary>
        /// Defines the Underlined property.
        /// </summary>
        public static readonly StyledProperty<UnderlinedType> UnderlinedProperty =
        AvaloniaProperty.Register<TabControlExt, UnderlinedType>(nameof(Underlined), defaultValue: UnderlinedType.None);



        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new TabItemExtContainerGenerator(this);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            // _mainGrid = e.NameScope.Find<Grid>(PART_MainGrid);

            // _columnDefinition0 = _mainGrid.ColumnDefinitions.
            // OfType<ColumnDefinitionExt>()
            // .FirstOrDefault(x => x.Name == PART_ColumnDefinition0);

            // _columnDefinition1 = _mainGrid.ColumnDefinitions.
            // OfType<ColumnDefinitionExt>()
            // .FirstOrDefault(x => x.Name == PART_ColumnDefinition1);

            // _rowDefinition0 = _mainGrid.RowDefinitions.
            // OfType<RowDefinitionExt>()
            // .FirstOrDefault(x => x.Name == PART_RowDefinition0);

            // _rowDefinition1 = _mainGrid.RowDefinitions.
            // OfType<RowDefinitionExt>()
            // .FirstOrDefault(x => x.Name == PART_RowDefinition1);

            _headerPanelGrid = e.NameScope.Find<Grid>(PART_HeaderPanelGrid);

            _underline = e.NameScope.Find<Underline>(PART_Underline);

            _contentPanel = e.NameScope.Find<Border>(PART_ContentPanel);

            InvalidateVisual();

            base.OnApplyTemplate(e);


        }

        // protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        // {
        //     base.OnAttachedToVisualTree(e);
        //     foreach(var items in this.Items.OfType<TabItemExt>())
        //     {
        //         items.InvalidateVisual();
        //     }
        // }

        // public TabControlExt()
        // {
        //     ThemeManager.Instance.IsThemeChanged+=(o, e)=>{
        //         RaisePropertyChanged(UnderlinedProperty, null, Underlined);
        //     };
        // }





        // public override void Render(DrawingContext context)
        // {
        //     switch (TabStripPlacement)
        //     {

        //         case Dock.Left:
        //             //todo correct me

        //             _columnDefinition0.Width= GridLength.Auto;
        //             _columnDefinition1.Width= GridLength.Parse("*");

        //             _rowDefinition0.Height=GridLength.Parse("*");
        //             _rowDefinition1.Height=new GridLength(0);

        //             Grid.SetColumn(_contentPanel, 1);
        //             Grid.SetRow(_contentPanel, 0);

        //             Grid.SetColumn(_headerPanelGrid, 0);
        //             Grid.SetRow(_headerPanelGrid, 0);  
        //             break;
        //         case Dock.Top:
        //              _columnDefinition0.Width= new GridLength();
        //              _columnDefinition1.Width= new GridLength(0);

        //             _rowDefinition0.Height=GridLength.Auto;
        //             _rowDefinition1.Height=GridLength.Parse("*");

        //             Grid.SetColumn(_contentPanel, 0);
        //             Grid.SetRow(_contentPanel, 1);

        //             Grid.SetColumn(_headerPanelGrid, 0);
        //             Grid.SetRow(_headerPanelGrid, 0);
        //             break;

        //         case Dock.Right:
        //             _columnDefinition0.Width= GridLength.Parse("*");
        //             _columnDefinition1.Width= GridLength.Auto;

        //             _rowDefinition0.Height=GridLength.Parse("*");
        //             _rowDefinition1.Height=new GridLength(0);

        //             Grid.SetColumn(_contentPanel, 0);
        //             Grid.SetRow(_contentPanel, 0);

        //             Grid.SetColumn(_headerPanelGrid, 1);
        //             Grid.SetRow(_headerPanelGrid, 0);  
        //             break;
        //         case Dock.Bottom:
        //             _columnDefinition0.Width= new GridLength();
        //             _columnDefinition1.Width= new GridLength(0);

        //             _rowDefinition0.Height=GridLength.Parse("*");
        //             _rowDefinition1.Height=GridLength.Auto;

        //             Grid.SetColumn(_contentPanel, 0);
        //             Grid.SetRow(_contentPanel, 0);

        //             Grid.SetColumn(_headerPanelGrid, 0);
        //             Grid.SetRow(_headerPanelGrid, 1);
        //             break;



        // public override void Render(DrawingContext context)
        // {
        //     switch (TabStripPlacement)
        //     {

        //         case Dock.Left:
        //             //todo correct me

        //             _columnDefinition0.Width= GridLength.Auto;
        //             _columnDefinition1.Width= GridLength.Parse("*");

        //             _rowDefinition0.Height=GridLength.Parse("*");
        //             _rowDefinition1.Height=new GridLength(0);

        //             Grid.SetColumn(_contentPanel, 1);
        //             Grid.SetRow(_contentPanel, 0);

        //             Grid.SetColumn(_headerPanelGrid, 0);
        //             Grid.SetRow(_headerPanelGrid, 0);  
        //             break;
        //         case Dock.Top:
        //              _columnDefinition0.Width= new GridLength();
        //              _columnDefinition1.Width= new GridLength(0);

        //             _rowDefinition0.Height=GridLength.Auto;
        //             _rowDefinition1.Height=GridLength.Parse("*");

        //             Grid.SetColumn(_contentPanel, 0);
        //             Grid.SetRow(_contentPanel, 1);

        //             Grid.SetColumn(_headerPanelGrid, 0);
        //             Grid.SetRow(_headerPanelGrid, 0);
        //             break;

        //         case Dock.Right:
        //             _columnDefinition0.Width= GridLength.Parse("*");
        //             _columnDefinition1.Width= GridLength.Auto;

        //             _rowDefinition0.Height=GridLength.Parse("*");
        //             _rowDefinition1.Height=new GridLength(0);

        //             Grid.SetColumn(_contentPanel, 0);
        //             Grid.SetRow(_contentPanel, 0);

        //             Grid.SetColumn(_headerPanelGrid, 1);
        //             Grid.SetRow(_headerPanelGrid, 0);  
        //             break;
        //         case Dock.Bottom:
        //             _columnDefinition0.Width= new GridLength();
        //             _columnDefinition1.Width= new GridLength(0);

        //             _rowDefinition0.Height=GridLength.Parse("*");
        //             _rowDefinition1.Height=GridLength.Auto;

        //             Grid.SetColumn(_contentPanel, 0);
        //             Grid.SetRow(_contentPanel, 0);

        //             Grid.SetColumn(_headerPanelGrid, 0);
        //             Grid.SetRow(_headerPanelGrid, 1);
        //             break;

        //     }
        //     base.Render(context);
        // }









    }
}
