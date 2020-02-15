using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class WindowCommandsItem : ContentControl
    {
        private const string PART_ContentPresenter = "PART_ContentPresenter";
        private const string PART_Separator = "PART_Separator";

        public bool IsSeparatorVisible
        {
            get { return (bool)GetValue(IsSeparatorVisibleProperty); }
            set { SetValue(IsSeparatorVisibleProperty, value); }
        }

        public static readonly StyledProperty<bool> IsSeparatorVisibleProperty =
            AvaloniaProperty.Register<WindowCommandsItem, bool>(nameof(IsSeparatorVisible), defaultValue: true);

        public WindowCommands ParentWindowCommands
        {
            get { return (WindowCommands)GetValue(ParentWindowCommandsProperty); }
            set { SetValue(ParentWindowCommandsProperty, value); }
        }

        public static readonly StyledProperty<WindowCommands> ParentWindowCommandsProperty =
            AvaloniaProperty.Register<WindowCommandsItem, WindowCommands>(nameof(ParentWindowCommands));

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            var windowCommands = ItemsControlExtensions.ItemsControlFromItemContainer(this) as WindowCommands;
            this.SetValue(WindowCommandsItem.ParentWindowCommandsProperty, windowCommands);
        }
    }
}