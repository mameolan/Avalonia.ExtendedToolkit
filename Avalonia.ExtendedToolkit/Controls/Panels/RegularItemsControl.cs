namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/HandyOrg/HandyControl.git

    /// <summary>
    /// panel with itemheigh width
    /// </summary>
    public class RegularItemsControl : SimpleItemsControl
    {
        /// <summary>
        /// Gets or sets ItemWidth.
        /// </summary>
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        /// <summary>
        /// Defines the ItemWidth property.
        /// </summary>
        public static readonly StyledProperty<double> ItemWidthProperty =
        AvaloniaProperty.Register<RegularItemsControl, double>(nameof(ItemWidth), defaultValue: 200d);

        /// <summary>
        /// Gets or sets ItemHeight.
        /// </summary>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        /// <summary>
        /// Defines the ItemHeight property.
        /// </summary>
        public static readonly StyledProperty<double> ItemHeightProperty =
        AvaloniaProperty.Register<RegularItemsControl, double>(nameof(ItemHeight), defaultValue: 200d);

        /// <summary>
        /// Gets or sets ItemMargin.
        /// </summary>
        public Thickness ItemMargin
        {
            get { return (Thickness)GetValue(ItemMarginProperty); }
            set { SetValue(ItemMarginProperty, value); }
        }

        /// <summary>
        /// Defines the ItemMargin property.
        /// </summary>
        public static readonly StyledProperty<Thickness> ItemMarginProperty =
        AvaloniaProperty.Register<RegularItemsControl, Thickness>(nameof(ItemMargin));
    }
}
