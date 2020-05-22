using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// entry of a <see cref="WindowCommands"/>
    /// </summary>
    public class WindowCommandsItem : ContentControl
    {
        private const string PART_ContentPresenter = "PART_ContentPresenter";
        private const string PART_Separator = "PART_Separator";

        /// <summary>
        /// style key for this control
        /// </summary>
        public Type StyleKey => typeof(WindowCommandsItem);

        /// <summary>
        /// Gets or sets the value indicating whether to show the separator.
        /// </summary>
        public bool IsSeparatorVisible
        {
            get { return (bool)GetValue(IsSeparatorVisibleProperty); }
            set { SetValue(IsSeparatorVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsSeparatorVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsSeparatorVisibleProperty =
            AvaloniaProperty.Register<WindowCommandsItem, bool>(nameof(IsSeparatorVisible), defaultValue: true);

        /// <summary>
        /// get/sets ParentWindowCommands
        /// </summary>
        public WindowCommands ParentWindowCommands
        {
            get { return (WindowCommands)GetValue(ParentWindowCommandsProperty); }
            set { SetValue(ParentWindowCommandsProperty, value); }
        }

        /// <summary>
        /// <see cref="ParentWindowCommands"/>
        /// </summary>
        public static readonly StyledProperty<WindowCommands> ParentWindowCommandsProperty =
            AvaloniaProperty.Register<WindowCommandsItem, WindowCommands>(nameof(ParentWindowCommands));

        /// <summary>
        /// tries to get ItemsControlFromItemContainer
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
#warning does this work?
            var windowCommands = ItemsControlExtensions.ItemsControlFromItemContainer(this) as WindowCommands;
            this.SetValue(WindowCommandsItem.ParentWindowCommandsProperty, windowCommands);
        }
    }
}
