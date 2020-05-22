using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// Provides calculated values that can be referenced as TemplatedParent
    /// sources when defining templates for a <see cref="SplitView" />.
    /// Not intended for general use.
    /// </summary>
    public class SplitViewTemplateSettings : AvaloniaObject
    {
        /// <summary>
        /// Gets the <see cref="SplitView.CompactPaneLength" /> value as a GridLength.
        /// </summary>
        public GridLength CompactPaneGridLength
        {
            get { return (GridLength)GetValue(CompactPaneGridLengthProperty); }
            set { SetValue(CompactPaneGridLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="CompactPaneGridLength"/>
        /// </summary>
        public static readonly StyledProperty<GridLength> CompactPaneGridLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, GridLength>(nameof(CompactPaneGridLength));

        /// <summary>
        /// Gets the negative of the <see cref="SplitView.OpenPaneLength" /> value.
        /// </summary>
        public double NegativeOpenPaneLength
        {
            get { return (double)GetValue(NegativeOpenPaneLengthProperty); }
            set { SetValue(NegativeOpenPaneLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="NegativeOpenPaneLength"/>
        /// </summary>
        public static readonly StyledProperty<double> NegativeOpenPaneLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>(nameof(NegativeOpenPaneLength), defaultValue: 0d);

        /// <summary>
        /// Gets the negative of the value calculated by subtracting the
        /// <see cref="SplitView.CompactPaneLength" /> value from
        /// the <see cref="SplitView.OpenPaneLength" /> value.
        /// </summary>
        public double NegativeOpenPaneLengthMinusCompactLength
        {
            get { return (double)GetValue(NegativeOpenPaneLengthMinusCompactLengthProperty); }
            set { SetValue(NegativeOpenPaneLengthMinusCompactLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="NegativeOpenPaneLengthMinusCompactLength"/>
        /// </summary>
        public static readonly StyledProperty<double> NegativeOpenPaneLengthMinusCompactLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>
            (nameof(NegativeOpenPaneLengthMinusCompactLength), defaultValue: 0d);

        /// <summary>
        /// Gets the <see cref="SplitView.OpenPaneLength" /> value as a GridLength.
        /// </summary>
        public GridLength OpenPaneGridLength
        {
            get { return (GridLength)GetValue(OpenPaneGridLengthProperty); }
            set { SetValue(OpenPaneGridLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="OpenPaneGridLength"/>
        /// </summary>
        public static readonly StyledProperty<GridLength> OpenPaneGridLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, GridLength>(nameof(OpenPaneGridLength));

        /// <summary>
        /// Gets the <see cref="SplitView.OpenPaneLength" /> value.
        /// </summary>
        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="OpenPaneLength"/>
        /// </summary>
        public static readonly StyledProperty<double> OpenPaneLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>(nameof(OpenPaneLength)
                , defaultValue: 0d);

        /// <summary>
        /// Gets a value calculated by subtracting the <see cref="SplitView.CompactPaneLength" />
        /// value from the <see cref="SplitView.OpenPaneLength" /> value.
        /// </summary>
        public double OpenPaneLengthMinusCompactLength
        {
            get { return (double)GetValue(OpenPaneLengthMinusCompactLengthProperty); }
            set { SetValue(OpenPaneLengthMinusCompactLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="OpenPaneLengthMinusCompactLength"/>
        /// </summary>
        public static readonly StyledProperty<double> OpenPaneLengthMinusCompactLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>(nameof(OpenPaneLengthMinusCompactLength), defaultValue: 0d);

        internal SplitView Owner { get; }

        internal SplitViewTemplateSettings(SplitView owner)
        {
            this.Owner = owner;
            this.Update();
        }

        internal void Update()
        {
            this.CompactPaneGridLength = new GridLength(this.Owner.CompactPaneLength, GridUnitType.Pixel);
            this.OpenPaneGridLength = new GridLength(this.Owner.OpenPaneLength, GridUnitType.Pixel);

            this.OpenPaneLength = this.Owner.OpenPaneLength;
            this.OpenPaneLengthMinusCompactLength = this.Owner.OpenPaneLength - this.Owner.CompactPaneLength;

            this.NegativeOpenPaneLength = -this.OpenPaneLength;
            this.NegativeOpenPaneLengthMinusCompactLength = -this.OpenPaneLengthMinusCompactLength;
        }
    }
}
