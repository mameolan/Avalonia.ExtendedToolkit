using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class TabItemExt : TabItem
    {
        private static readonly string PART_Underline = "Underline";
        private static readonly string PART_ContentSite = "ContentSite";

        private static readonly string Part_ContentGrid = "contentGrid";

        private static readonly string PART_ContentLeftCol = "PART_ContentLeftCol";
        private static readonly string PART_ContentRightCol = "PART_ContentRightCol";
        private static readonly string PART_ContentTopRow = "PART_ContentTopRow";
        private static readonly string PART_ContentBottomRow = "PART_ContentBottomRow";


        private static readonly string PseudoClass_UnderlinePlacement_NotSet = ":underlinePlacement_NotSet";
        private static readonly string PseudoClass_UnderlinePlacement_Set = ":underlinePlacement_Set";

        private List<string> underlinedPseudoClasses = new List<string>
        {
            PseudoClass_UnderlinePlacement_NotSet,
            PseudoClass_UnderlinePlacement_Set

        };
        private Underline _underline;
        private ContentControlEx _contentSite;
        private ColumnDefinitionExt _leftColumn;
        private ColumnDefinitionExt _rightColumn;
        private RowDefinitionExt _bottomRow;
        private RowDefinitionExt _topRow;
        private bool _isTemplateApplied;


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
        AvaloniaProperty.Register<TabItemExt, FontFamily>(nameof(HeaderFontFamily), defaultValue: FontFamily.Default);

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
        AvaloniaProperty.Register<TabItemExt, double>(nameof(HeaderFontSize), defaultValue: 12);

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
        AvaloniaProperty.Register<TabItemExt, FontWeight>(nameof(HeaderFontWeight), defaultValue: FontWeight.Normal);

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
        AvaloniaProperty.Register<TabItemExt, Dock?>(nameof(UnderlinePlacement));




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
        AvaloniaProperty.Register<TabItemExt, IBrush>(nameof(UnderlineBrush));

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
        AvaloniaProperty.Register<TabItemExt, IBrush>(nameof(UnderlineMouseOverBrush));

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
        AvaloniaProperty.Register<TabItemExt, IBrush>(nameof(UnderlineMouseOverSelectedBrush));

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
        AvaloniaProperty.Register<TabItemExt, IBrush>(nameof(UnderlineSelectedBrush));

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
        AvaloniaProperty.Register<TabItemExt, UnderlinedType>(nameof(Underlined), defaultValue: UnderlinedType.None);

        public TabItemExt()
        {
            UnderlinePlacementProperty.Changed.AddClassHandler<TabItemExt>((o, e) => OnUnderlinePlacementChanged(o, e));
            UnderlinedProperty.Changed.AddClassHandler<TabItemExt>((o, e) => OnUnderlinedChanged(o, e));
        }



        private void OnUnderlinedChanged(TabItemExt tabItem, AvaloniaPropertyChangedEventArgs e)
        {
            if (_underline != null)
            {
                _underline.ApplyBorderProperties();
            }
        }

        private void OnUnderlinePlacementChanged(TabItemExt tabItem, AvaloniaPropertyChangedEventArgs e)
        {
            underlinedPseudoClasses.ForEach(item => tabItem.PseudoClasses.Remove(item));

            if (e.NewValue == null)
            {
                tabItem.PseudoClasses.Add(PseudoClass_UnderlinePlacement_NotSet);
            }
            else
            {
                tabItem.PseudoClasses.Add(PseudoClass_UnderlinePlacement_Set);
                Dock dock = (Dock)e.NewValue;

                if (_underline != null)
                {
                    _underline.Placement = dock;
                }
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {

            _underline = e.NameScope.Find<Underline>(PART_Underline);
            _contentSite = e.NameScope.Find<ContentControlEx>(PART_ContentSite);

            Grid grid = e.NameScope.Find<Grid>(Part_ContentGrid);
            _leftColumn = grid.ColumnDefinitions.OfType<ColumnDefinitionExt>().FirstOrDefault(x => x.Name == PART_ContentLeftCol);
            _rightColumn = grid.ColumnDefinitions.OfType<ColumnDefinitionExt>().FirstOrDefault(x => x.Name == PART_ContentRightCol);
            //_leftColumn = e.NameScope.Find<ColumnDefinitionExt>(PART_ContentLeftCol);
            //_rightColumn = e.NameScope.Find<ColumnDefinitionExt>(PART_ContentRightCol);
            _bottomRow = grid.RowDefinitions.OfType<RowDefinitionExt>().FirstOrDefault(x => x.Name == PART_ContentBottomRow);
            //_bottomRow = e.NameScope.Find<RowDefinitionExt>(PART_ContentBottomRow);
            _topRow = grid.RowDefinitions.OfType<RowDefinitionExt>().FirstOrDefault(x => x.Name == PART_ContentTopRow);
            //_topRow = e.NameScope.Find<RowDefinitionExt>(PART_ContentTopRow);
            _isTemplateApplied = true;

            base.OnApplyTemplate(e);
        }

        public override void Render(DrawingContext context)
        {
            if (_isTemplateApplied == false)
            {
                base.Render(context);

            }

            switch (TabStripPlacement)
            {
                case Dock.Left:
                    Grid.SetColumn(_contentSite, 0);
                    Grid.SetRow(_contentSite, 0);
                    _bottomRow.Height = GridLength.Auto;
                    _leftColumn.Width = GridLength.Parse("*");
                    _rightColumn.Width = GridLength.Auto;
                    _topRow.Height = GridLength.Parse("*");
                    break;
                case Dock.Top:
                    Grid.SetColumn(_contentSite, 0);
                    Grid.SetRow(_contentSite, 0);
                    _bottomRow.Height = GridLength.Auto;
                    _leftColumn.Width = GridLength.Parse("*");
                    _rightColumn.Width = GridLength.Auto;
                    _topRow.Height = GridLength.Parse("*");
                    break;
                case Dock.Right:
                    Grid.SetColumn(_contentSite, 1);
                    Grid.SetRow(_contentSite, 0);
                    _bottomRow.Height = GridLength.Auto;
                    _leftColumn.Width = GridLength.Auto;
                    _rightColumn.Width = GridLength.Parse("*");
                    _topRow.Height = GridLength.Parse("*");
                    break;
                case Dock.Bottom:
                    Grid.SetColumn(_contentSite, 0);
                    Grid.SetRow(_contentSite, 1);
                    _bottomRow.Height = GridLength.Parse("*");
                    _leftColumn.Width = GridLength.Parse("*");
                    _rightColumn.Width = GridLength.Auto;
                    _topRow.Height = GridLength.Auto;
                    break;
            }
            
            if (UnderlinePlacement == null)
            {
                switch (TabStripPlacement)
                {
                    case Dock.Top:
                        Grid.SetColumn(_underline, 0);
                        Grid.SetRow(_underline, 1);
                        break;
                    case Dock.Bottom:
                        Grid.SetColumn(_underline, 0);
                        Grid.SetRow(_underline, 0);
                        break;
                    case Dock.Left:
                        Grid.SetColumn(_underline, 0);
                        Grid.SetRow(_underline, 0);
                        break;
                    case Dock.Right:
                        Grid.SetColumn(_underline, 0);
                        Grid.SetRow(_underline, 0);
                        break;
                }
            }
            else
            {
                switch (UnderlinePlacement.Value)
                {
                    case Dock.Top:
                        switch (TabStripPlacement)
                        {
                            case Dock.Top:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Bottom:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Left:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Right:
                                Grid.SetColumn(_underline, 1);
                                Grid.SetRow(_underline, 0);
                                break;
                        }
                        break;
                    case Dock.Bottom:
                        switch (TabStripPlacement)
                        {
                            case Dock.Top:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 1);
                                break;
                            case Dock.Bottom:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 1);
                                break;
                            case Dock.Left:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 1);
                                break;
                            case Dock.Right:
                                Grid.SetColumn(_underline, 1);
                                Grid.SetRow(_underline, 1);
                                break;
                        }
                        break;
                    case Dock.Left:
                        switch (TabStripPlacement)
                        {
                            case Dock.Top:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Bottom:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 1);
                                break;
                            case Dock.Left:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Right:
                                Grid.SetColumn(_underline, 1);
                                Grid.SetRow(_underline, 0);
                                break;
                        }
                        break;
                    case Dock.Right:
                        switch (TabStripPlacement)
                        {
                            case Dock.Top:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Bottom:
                                Grid.SetColumn(_underline, 1);
                                Grid.SetRow(_underline, 1);
                                break;
                            case Dock.Left:
                                Grid.SetColumn(_underline, 0);
                                Grid.SetRow(_underline, 0);
                                break;
                            case Dock.Right:
                                Grid.SetColumn(_underline, 1);
                                Grid.SetRow(_underline, 0);
                                break;
                        }
                        break;
                    
                }

            }



            base.Render(context);

        }




    }
}
