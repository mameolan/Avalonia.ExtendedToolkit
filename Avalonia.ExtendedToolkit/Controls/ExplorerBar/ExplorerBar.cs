using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/jogibear9988/OdysseyWPF.git

    /// <summary>
    /// explorerbar implementation
    /// </summary>
    public partial class ExplorerBar : ItemsControl
    {
        /// <summary>
        /// The default value for the <see cref="ItemsControl.ItemsPanel"/> property.
        /// </summary>
        private static readonly FuncTemplate<Panel> DefaultPanel =
            new FuncTemplate<Panel>(() => new VirtualizingStackPanel());

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(ExplorerBar);

        static ExplorerBar()
        {
            ItemsPanelProperty.OverrideDefaultValue<ExplorerBar>(DefaultPanel);
        }
    }
}
