using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class SplitViewTemplateSettings : AvaloniaObject
    {
        public GridLength CompactPaneGridLength
        {
            get { return (GridLength)GetValue(CompactPaneGridLengthProperty); }
            set { SetValue(CompactPaneGridLengthProperty, value); }
        }

        public static readonly StyledProperty<GridLength> CompactPaneGridLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, GridLength>(nameof(CompactPaneGridLength));

        public double NegativeOpenPaneLength
        {
            get { return (double)GetValue(NegativeOpenPaneLengthProperty); }
            set { SetValue(NegativeOpenPaneLengthProperty, value); }
        }

        public static readonly StyledProperty<double> NegativeOpenPaneLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>(nameof(NegativeOpenPaneLength), defaultValue: 0d);

        public double NegativeOpenPaneLengthMinusCompactLength
        {
            get { return (double)GetValue(NegativeOpenPaneLengthMinusCompactLengthProperty); }
            set { SetValue(NegativeOpenPaneLengthMinusCompactLengthProperty, value); }
        }

        public static readonly StyledProperty<double> NegativeOpenPaneLengthMinusCompactLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>(nameof(NegativeOpenPaneLengthMinusCompactLength), defaultValue: 0d);

        public GridLength OpenPaneGridLength
        {
            get { return (GridLength)GetValue(OpenPaneGridLengthProperty); }
            set { SetValue(OpenPaneGridLengthProperty, value); }
        }

        public static readonly StyledProperty<GridLength> OpenPaneGridLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, GridLength>(nameof(OpenPaneGridLength));

        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }

        public static readonly StyledProperty<double> OpenPaneLengthProperty =
            AvaloniaProperty.Register<SplitViewTemplateSettings, double>(nameof(OpenPaneLength), defaultValue: 0d);

        public double OpenPaneLengthMinusCompactLength
        {
            get { return (double)GetValue(OpenPaneLengthMinusCompactLengthProperty); }
            set { SetValue(OpenPaneLengthMinusCompactLengthProperty, value); }
        }

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