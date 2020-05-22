using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public partial class SplitView : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(SplitView);

        /// <summary>
        /// Gets or sets the width of the <see cref="SplitView" /> pane in its compact display mode.
        /// </summary>
        /// <returns>
        /// The width of the pane in it's compact display mode. The default is 48 device-independent pixel (DIP) (defined
        /// by the SplitViewCompactPaneThemeLength resource).
        /// </returns>
        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="CompactPaneLength"/>
        /// </summary>
        public static readonly StyledProperty<double> CompactPaneLengthProperty =
            AvaloniaProperty.Register<SplitView, double>(nameof(CompactPaneLength), defaultValue: 0d);

        /// <summary>
        /// state for Animations
        /// (not original)
        /// </summary>
        internal string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        /// <summary>
        /// <see cref="State"/>
        /// </summary>
        internal static readonly StyledProperty<string> StateProperty =
            AvaloniaProperty.Register<SplitView, string>(nameof(State));

        /// <summary>
        /// Gets of sets a value that specifies how the pane and content areas of a <see cref="SplitView" /> are shown.
        /// </summary>
        /// <returns>
        /// A value of the enumeration that specifies how the pane and content areas
        /// of a <see cref="SplitView" /> are
        /// shown. The default is <see cref="SplitViewDisplayMode.Overlay" />.
        /// </returns>
        public SplitViewDisplayMode DisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        /// <summary>
        /// <see cref="DisplayMode"/>
        /// </summary>
        public static readonly StyledProperty<SplitViewDisplayMode> DisplayModeProperty =
            AvaloniaProperty.Register<SplitView, SplitViewDisplayMode>(nameof(DisplayMode), defaultValue: SplitViewDisplayMode.Overlay);

        /// <summary>
        /// Gets or sets a value that specifies whether the <see cref="SplitView" /> pane
        /// is expanded to its full width.
        /// </summary>
        /// <returns>true if the pane is expanded to its full width; otherwise, false. The default is true.</returns>
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        /// <summary>
        /// <see cref="IsPaneOpen"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsPaneOpenProperty =
            AvaloniaProperty.Register<SplitView, bool>(nameof(IsPaneOpen), defaultValue: true);

        /// <summary>
        /// Gets or sets the width of the <see cref="SplitView" /> pane when it's fully expanded.
        /// </summary>
        /// <returns>
        /// The width of the <see cref="SplitView" /> pane when it's fully expanded.
        /// The default is 320 device-independent
        /// pixel (DIP).
        /// </returns>
        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }

        /// <summary>
        /// <see cref="OpenPaneLength"/>
        /// </summary>
        public static readonly StyledProperty<double> OpenPaneLengthProperty =
            AvaloniaProperty.Register<SplitView, double>(nameof(OpenPaneLength), defaultValue: 0d);

        /// <summary>
        /// Gets or sets the contents of the pane of a <see cref="SplitView" />.
        /// </summary>
        /// <returns>The contents of the pane of a <see cref="SplitView" />. The default is null.</returns>
        public object Pane
        {
            get { return (object)GetValue(PaneProperty); }
            set { SetValue(PaneProperty, value); }
        }

        /// <summary>
        /// <see cref="Pane"/>
        /// </summary>
        public static readonly StyledProperty<object> PaneProperty =
            AvaloniaProperty.Register<SplitView, object>(nameof(Pane));

        /// <summary>
        /// Gets or sets the Brush to apply to the background of the <see cref="Pane" /> area of the control.
        /// </summary>
        /// <returns>The Brush to apply to the background of the <see cref="Pane" />
        /// area of the control.</returns>
        public IBrush PaneBackground
        {
            get { return (IBrush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }

        /// <summary>
        /// <see cref="PaneBackground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> PaneBackgroundProperty =
            AvaloniaProperty.Register<SplitView, IBrush>(nameof(PaneBackground));

        /// <summary>
        /// Gets or sets the Brush to apply to the foreground of the <see cref="Pane" /> area of the control.
        /// </summary>
        /// <returns>The Brush to apply to the background of the
        /// <see cref="Pane" /> area of the control.</returns>
        public IBrush PaneForeground
        {
            get { return (IBrush)GetValue(PaneForegroundProperty); }
            set { SetValue(PaneForegroundProperty, value); }
        }

        /// <summary>
        /// <see cref="PaneForeground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> PaneForegroundProperty =
            AvaloniaProperty.Register<SplitView, IBrush>(nameof(PaneForeground));

        /// <summary>
        /// Gets or sets a value that specifies whether the pane is shown on the right or left side of the
        /// <see cref="SplitView" />.
        /// </summary>
        /// <returns>
        /// A value of the enumeration that specifies whether the pane is shown on the
        /// right or left side of the
        /// <see cref="SplitView" />. The default is <see cref="SplitViewPanePlacement.Left" />.
        /// </returns>
        public SplitViewPanePlacement PanePlacement
        {
            get { return (SplitViewPanePlacement)GetValue(PanePlacementProperty); }
            set { SetValue(PanePlacementProperty, value); }
        }

        /// <summary>
        /// <see cref="PanePlacement"/>
        /// </summary>
        public static readonly StyledProperty<SplitViewPanePlacement> PanePlacementProperty =
            AvaloniaProperty.Register<SplitView, SplitViewPanePlacement>(nameof(PanePlacement), defaultValue: SplitViewPanePlacement.Left);

        /// <summary>
        /// Gets an object that provides calculated values that can be referenced
        /// as TemplateBinding sources when defining
        /// templates for a <see cref="SplitView" /> control.
        /// </summary>
        /// <returns>An object that provides calculated values for templates.</returns>
        public SplitViewTemplateSettings TemplateSettings
        {
            get { return (SplitViewTemplateSettings)GetValue(TemplateSettingsProperty); }
            set { SetValue(TemplateSettingsProperty, value); }
        }

        /// <summary>
        /// <see cref="TemplateSettings"/>
        /// </summary>
        public static readonly StyledProperty<SplitViewTemplateSettings> TemplateSettingsProperty =
            AvaloniaProperty.Register<SplitView, SplitViewTemplateSettings>(nameof(TemplateSettings));

        private Rectangle lightDismissLayer;
        private RectangleGeometry paneClipRectangle;

        /// <summary>
        ///     Occurs when the <see cref="SplitView" /> pane is closed.
        /// </summary>
        public event EventHandler PaneClosed;

        /// <summary>
        ///     Occurs when the <see cref="SplitView" /> pane is closing.
        /// </summary>
        public event EventHandler<SplitViewPaneClosingEventArgs> PaneClosing;
    }
}
