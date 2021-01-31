using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/HandyOrg/HandyControl

    /// <summary>
    /// represents an item in the step bar
    /// </summary>
    public class StepBarItem : ContentControl
    {
        //!!!remember to change the name if classes change

        private const string StepBar_Dock_Top_Class = "StepBarItemHorizontalTop";
        private const string StepBar_Dock_Left_Class = "StepBarItemVerticalLeft";

        private const string StepBar_Dock_Bottom_Class = "StepBarItemHorizontalBottom";
        private const string StepBar_Dock_Right_Class = "StepBarItemVerticalRight";

        private Dock? _lastDock;

        private List<string> pseudoClassesToClear = new List<string>
                    {   StepBar_Dock_Top_Class,
                        StepBar_Dock_Left_Class,
                        StepBar_Dock_Bottom_Class,
                        StepBar_Dock_Right_Class
                    };

        /// <summary>
        /// Gets or sets Index.
        /// </summary>
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        /// <summary>
        /// Defines the Index property.
        /// </summary>
        public static readonly StyledProperty<int> IndexProperty =
        AvaloniaProperty.Register<StepBarItem, int>(nameof(Index), defaultValue: -1);

        /// <summary>
        /// Gets or sets Status.
        /// </summary>
        public StepStatus Status
        {
            get { return (StepStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        /// <summary>
        /// Defines the Status property.
        /// </summary>
        public static readonly StyledProperty<StepStatus> StatusProperty =
        AvaloniaProperty.Register<StepBarItem, StepStatus>(nameof(Status), defaultValue: StepStatus.Waiting);

        /// <summary>
        /// Gets or sets ContentBorderBackground.
        /// </summary>
        public IBrush ContentBorderBackground
        {
            get { return (IBrush)GetValue(ContentBorderBackgroundProperty); }
            set { SetValue(ContentBorderBackgroundProperty, value); }
        }

        /// <summary>
        /// Defines the ContentBorderBackground property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ContentBorderBackgroundProperty =
        AvaloniaProperty.Register<StepBarItem, IBrush>(nameof(ContentBorderBackground));

        /// <summary>
        /// adds the pseudo class by <see cref="StepBar.Dock"/> property
        /// </summary>
        /// <param name="context"></param>
        public override void Render(DrawingContext context)
        {
            StepBar stepBar = Parent as StepBar;
            Dock currentDock = stepBar.Dock;

            if (_lastDock == null || _lastDock.Value != currentDock)
            {
                pseudoClassesToClear.ForEach(x => Classes.Remove(x));

                switch (currentDock)
                {
                    case Dock.Top:
                        PseudoClasses.Add(StepBar_Dock_Top_Class);
                        break;

                    case Dock.Left:
                        PseudoClasses.Add(StepBar_Dock_Left_Class);
                        break;

                    case Dock.Bottom:
                        PseudoClasses.Add(StepBar_Dock_Bottom_Class);
                        break;

                    case Dock.Right:
                        PseudoClasses.Add(StepBar_Dock_Right_Class);
                        break;
                }
                _lastDock = currentDock;
            }

            base.Render(context);
        }
    }
}
